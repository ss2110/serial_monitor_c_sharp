using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;

namespace SerialMonitor_new
{
    
    public partial class SerialMonitor : Form
    {
        byte[] btempData = new byte[10240];
        byte bPreData = new byte();
        bool bStartDisplayFlag = false;
        Queue<string> receiveData = new Queue<string>();
        string DisplayType;
        string SendType;
        Int64 PeriodicLogSaveCount = new Int64();

        private Object thisLock = new Object();

        //for client socket
        bool bReceiveThreadStop = false;
        static Socket sck;
        static Socket accepted;
        static byte[] Buffer { get; set; }

        IPAddress[] localhost_addr;
        Thread[] thread = new Thread[1];
        bool bThreadStart = false;
        bool bServerStart = false;

        bool bInprogressListen = false;
        bool bAccepted = false;

        public SerialMonitor()
        {
            InitializeComponent();

            combo_DisplayType.Text = combo_DisplayType.Items[2].ToString();
            combo_SendType.Text = combo_SendType.Items[2].ToString();

            combo_comx.Items.Clear();
            foreach (string comport in SerialPort.GetPortNames())
            {
                combo_comx.Items.Add(comport);
            }
            combo_comx.Sorted = true;
            if (combo_comx.Items.Count > 0)
                combo_comx.SelectedIndex = 0;            

            combo_baudrate.SelectedIndex = 5;
            bStartDisplayFlag = true;

            timer1.Interval = 100;
            timer1.Start();

            timer_RepeativeSend.Interval = 1000;
            timer_RepeativeSend.Start();

            tb_RepeativeSendInterval.Text = "1000";

            DisplayType = combo_DisplayType.SelectedItem.ToString();
            SendType = combo_SendType.SelectedItem.ToString();

            this.Text = "Serial Monitor - Data 8bit - Parity None - Stop 1bit - [OFF]Line";
            tb_Send.Text = "Serial Monitor Start! superman7105@naver.com";

            if (Directory.Exists(".\\log") == true)  {
                Debug.Print("[LOG] directory exist");
                System.IO.DirectoryInfo di = new DirectoryInfo(".\\log");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            else {
                Debug.Print("[LOG] directory [NOT] exist");
                Directory.CreateDirectory(".\\log");
            }

            PeriodicLogSaveCount = 0;
            cb_PeriodicLogSave.Checked = true;

            //for tcp server client
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string strHostName = ""; 
            strHostName = System.Net.Dns.GetHostName(); 
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName); 
            localhost_addr = ipEntry.AddressList; 
            tb_Address.Text = localhost_addr[localhost_addr.Length - 1].ToString();
            //tb_Address.Text = localhost_addr[localhost_addr.Length - 2].ToString();

            tb_PortNum.Text = "8888";
            cb_TcpIpConnect.Checked = false;
            combo_selectServerClient.SelectedIndex = 0;//server

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(tb_Address.Text), Convert.ToInt32(tb_PortNum.Text));

            //serial port databits, parity setting
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            combo_databit.SelectedIndex = 1;
            combo_parity.SelectedIndex = 0;
        }

        private void ThreadTCPServerReceive()
        {
            Buffer = new byte[sck.SendBufferSize];

            try
            {
                sck.Bind(new IPEndPoint(IPAddress.Any, Convert.ToInt32(tb_PortNum.Text)));
                bInprogressListen = true;
                bAccepted = false;
                sck.Listen(100);
                accepted = sck.Accept();
                bInprogressListen = false;
                bAccepted = true;

                string strReceive = string.Empty;
                strReceive = "\r\n[Client] Connected...\r\n";
                receiveData.Enqueue(strReceive);

                while (true)
                {
                    if (bStartDisplayFlag == true)
                    {
                        int bytesRead = accepted.Receive(Buffer);

                        strReceive = string.Empty;
                        if (DisplayType == "ASCII")
                        {
                            byte[] formatted = new byte[bytesRead];
                            for (int i = 0; i < bytesRead; ++i)
                            {
                                formatted[i] = Buffer[i];
                            }

                            strReceive = Encoding.UTF8.GetString(formatted);
                            receiveData.Enqueue(strReceive);
                        }
                        else
                        {
                            for (int i = 0; i < bytesRead; ++i)
                            {
                                btempData[i] = Buffer[i];
                            }

                            for (int i = 0; i < bytesRead; i++)
                            {
                                //Line Feed
                                if ((bPreData == 0x03) && (btempData[i] == 0x02))
                                {
                                    strReceive += "\r\n";
                                }

                                if (DisplayType == "HEX")
                                {
                                    if (btempData[i] > 15)
                                    {
                                        strReceive += btempData[i].ToString("X") + " ";
                                    }
                                    else
                                    {
                                        strReceive += "0" + btempData[i].ToString("X") + " ";
                                    }
                                }

                                if (DisplayType == "DEC")
                                {
                                    if (btempData[i] > 100)
                                        strReceive = strReceive + btempData[i].ToString() + " ";
                                    else
                                    {
                                        if (btempData[i] < 10)
                                        {
                                            strReceive = strReceive + "00" + btempData[i].ToString() + " ";
                                        }
                                        else
                                        {
                                            strReceive = strReceive + "0" + btempData[i].ToString() + " ";
                                        }
                                    }
                                }

                                //Line Feed
                                if ((bPreData == 0x0d) && (btempData[i] == 0x0a))
                                {
                                    strReceive += "\r\n";
                                }

                                bPreData = btempData[i];
                            }
                            receiveData.Enqueue(strReceive);
                        }
                    }

                    if (bReceiveThreadStop)
                    {
                        break;
                    }
                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                bServerStart = false;
                string strReceive = "\r\n[Client] Disconnected...\r\n";
                receiveData.Enqueue(strReceive);
                if (bInprogressListen) 
                {
                    sck.Close();
                    bReceiveThreadStop = true;
                }
                else
                {
                    if (bAccepted) 
                    {
                        if (accepted.Connected) accepted.Close();
                    }
                    sck.Close();
                    bReceiveThreadStop = true;
                }
            }
        }
        private void ThreadReceive()
        {
            Buffer = new byte[sck.SendBufferSize];

            while (true)
            {
                try
                {
                    if (bStartDisplayFlag == true)
                    {
                        int bytesRead = sck.Receive(Buffer);

                        string strReceive = string.Empty;
                        if (DisplayType == "ASCII")
                        {
                            byte[] formatted = new byte[bytesRead];
                            for (int i = 0; i < bytesRead; ++i)
                            {
                                formatted[i] = Buffer[i];
                            }

                            strReceive = Encoding.UTF8.GetString(formatted);
                            receiveData.Enqueue(strReceive);
                        }
                        else
                        {
                             for (int i = 0; i < bytesRead; ++i)
                            {
                                btempData[i] = Buffer[i];
                            }

                            for (int i = 0; i < bytesRead; i++)
                            {
                                //Line Feed
                                if ((bPreData == 0x03) && (btempData[i] == 0x02))
                                {
                                    strReceive += "\r\n";
                                }

                                if (DisplayType == "HEX")
                                {
                                    if (btempData[i] > 15)
                                    {
                                        strReceive += btempData[i].ToString("X") + " ";
                                    }
                                    else
                                    {
                                        strReceive += "0" + btempData[i].ToString("X") + " ";
                                    }
                                }

                                if (DisplayType == "DEC")
                                {
                                    if (btempData[i] > 100)
                                        strReceive = strReceive + btempData[i].ToString() + " ";
                                    else
                                    {
                                        if (btempData[i] < 10)
                                        {
                                            strReceive = strReceive + "00" + btempData[i].ToString() + " ";
                                        }
                                        else
                                        {
                                            strReceive = strReceive + "0" + btempData[i].ToString() + " ";
                                        }
                                    }
                                }

                                //Line Feed
                                if ((bPreData == 0x0d) && (btempData[i] == 0x0a))
                                {
                                    strReceive += "\r\n";
                                }

                                bPreData = btempData[i];
                            }
                            receiveData.Enqueue(strReceive);
                        }
                    }

                    if (bReceiveThreadStop)
                    {
                        break;
                    }

                }
                catch// (Exception ex)
                {
                    string strReceive = "\r\n[Server] Disconnected...\r\n";
                    receiveData.Enqueue(strReceive);
                    bReceiveThreadStop = true;
                    if (sck.Connected) sck.Close();
                    break;
                    //MessageBox.Show(ex.ToString());
                    
                }
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            bReceiveThreadStop = false;
            try
            {
                if (cb_TcpIpConnect.Checked) //tcp ip connect
                {
                    if (serialPort1.IsOpen) return;

                    sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(tb_Address.Text), Convert.ToInt32(tb_PortNum.Text));

                    if (combo_selectServerClient.SelectedIndex == 0) //server
                    {
                        CheckForIllegalCrossThreadCalls = false;
                        thread[0] = new Thread(new ThreadStart(ThreadTCPServerReceive));
                        thread[0].Start();
                        receiveData.Enqueue("\r\n[Server] Started...\r\n");
                        bThreadStart = true;
                        bServerStart = true;
                        this.Text = "Serial Monitor - TCP Server Started";
                    }
                    else //client
                    {
                        try
                        {
                            sck.Connect(localEndPoint);
                            receiveData.Enqueue("\r\n[Server] Connected...\r\n");

                            CheckForIllegalCrossThreadCalls = false;
                            thread[0] = new Thread(new ThreadStart(ThreadReceive));
                            thread[0].Start();
                            bThreadStart = true;
                            this.Text = "Serial Monitor - TCP Client Started";

                        }   
                        catch// (Exception ex)
                        {
                            receiveData.Enqueue("\r\n[FAIL] Connection Failed...\r\n");
                            //MessageBox.Show(ex.ToString());
                            bThreadStart = false;
                        }
                    }
                }
                else
                {
                    if (serialPort1.IsOpen) return;
                    serialPort1.BaudRate = int.Parse(combo_baudrate.Text);
                    serialPort1.PortName = combo_comx.Text;
                    serialPort1.Open();
                    this.Text = "Serial Monitor - Data "+ Convert.ToString(serialPort1.DataBits) + "bit - " + serialPort1.Parity.ToString() + "- Stop 1bit - " + (serialPort1.BaudRate).ToString() + "bps" + " - " + serialPort1.PortName + " - [ON]Line";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (thisLock)
                {
                    this.BeginInvoke(new EventHandler(SerialPort1_DataReceived_inMainThread));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SerialPort1_DataReceived_inMainThread(object s, EventArgs e)
        {
            try
            {
                if (bStartDisplayFlag == false) return;

                string strReceive = string.Empty;
                if (DisplayType == "ASCII")
                {
                    strReceive = serialPort1.ReadExisting();
                    receiveData.Enqueue(strReceive); 
                }
                else
                {
                    int ReceiveSize;
                    ReceiveSize = serialPort1.BytesToRead;
                    serialPort1.Read(btempData, 0, ReceiveSize);

                    for (int i = 0; i < ReceiveSize; i++)
                    {
                        //Line Feed
                        if ((bPreData == 0x03) && (btempData[i] == 0x02))
                        {
                            strReceive += "\r\n";
                        }

                        if (DisplayType == "HEX")
                        {
                            if (btempData[i]>15)
                            {
                                strReceive += btempData[i].ToString("X") + " ";
                            }
                            else
                            {
                                strReceive += "0" + btempData[i].ToString("X") + " ";
                            }
                        }
                            
                        if (DisplayType == "DEC")
                        {
                            if (btempData[i] > 100)
                                strReceive = strReceive + btempData[i].ToString() + " ";
                            else
                            {
                                if (btempData[i] < 10)
                                {
                                    strReceive = strReceive + "00" + btempData[i].ToString() + " ";
                                }
                                else
                                {
                                    strReceive = strReceive + "0" + btempData[i].ToString() + " ";
                                }
                            }
                        }

                        //Line Feed
                        if ((bPreData == 0x0d) && (btempData[i] == 0x0a))
                        {
                            strReceive += "\r\n";
                        }

                        bPreData = btempData[i];
                    }
                    receiveData.Enqueue(strReceive);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (cb_TcpIpConnect.Checked)
            {
                if (bServerStart)
                {
                    if (bInprogressListen)
                    {
                        sck.Close();
                        bReceiveThreadStop = true;
                    }
                    else
                    {
                        if (bThreadStart) thread[0].Abort();
                        if (accepted.Connected) accepted.Close();
                        if (sck.Connected) sck.Close();
                        bReceiveThreadStop = true;
                    }
                }

                if (sck.Connected)
                {
                    try
                    {
                        thread[0].Abort();
                        bReceiveThreadStop = true;
                        sck.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                try
                {
                    if (serialPort1.IsOpen == false) return;
                    else
                    {
                        serialPort1.Close();
                        this.Text = "Serial Monitor - Data " + Convert.ToString(serialPort1.DataBits) + "bit - " + serialPort1.Parity.ToString() + "- Stop 1bit - " + (serialPort1.BaudRate).ToString() + "bps" + " - " + serialPort1.PortName + " - [OFF]Line";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (cb_TcpIpConnect.Checked) // Tcp/Ip Send
            {
                if (bServerStart)
                {
                    try
                    {
                        byte[] tempBytes = new byte[200];
                        int numSize;
                        if (tb_Send.Text == "") return;
                        string temp = tb_Send.Text.Trim();

                        BeginInvoke(new Action(() => tb_Display.AppendText("\r\n" + temp)));// + "\r\n")));

                        bPreData = 0x03;

                        if (SendType == "ASCII")
                        {
                            byte[] data = Encoding.UTF8.GetBytes(tb_Send.Text);
                            accepted.Send(data);
                        }
                        else if (SendType == "HEX")
                        {
                            numSize = 0;
                            foreach (string userStr in temp.Split())
                            {
                                tempBytes[numSize] = Convert.ToByte(userStr, 16);
                                Debug.WriteLine(tempBytes[numSize]);
                                numSize += 1;
                            }
                            byte[] formatted = new byte[numSize];
                            for (int i = 0; i < numSize; ++i)
                            {
                                formatted[i] = tempBytes[i];
                            }
                            accepted.Send(formatted);
                        }
                        else if (SendType == "DEC")
                        {
                            numSize = 0;
                            foreach (string userStr in temp.Split())
                            {
                                tempBytes[numSize] = Convert.ToByte(userStr, 10);
                                Debug.WriteLine(tempBytes[numSize]);
                                numSize += 1;
                            }
                            byte[] formatted = new byte[numSize];
                            for (int i = 0; i < numSize; ++i)
                            {
                                formatted[i] = tempBytes[i];
                            }

                            accepted.Send(formatted);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                if (sck.Connected)
                {
                    try
                    {
                        byte[] tempBytes = new byte[200];
                        int numSize;
                        if (tb_Send.Text == "") return;
                        string temp = tb_Send.Text.Trim();

                        BeginInvoke(new Action(() => tb_Display.AppendText("\r\n" + temp)));// + "\r\n")));

                        bPreData = 0x03;

                        if (SendType == "ASCII")
                        {
                            byte[] data = Encoding.UTF8.GetBytes(tb_Send.Text);
                            sck.Send(data);
                        }
                        else if (SendType == "HEX")
                        {
                            numSize = 0;
                            foreach (string userStr in temp.Split())
                            {
                                tempBytes[numSize] = Convert.ToByte(userStr, 16);
                                Debug.WriteLine(tempBytes[numSize]);
                                numSize += 1;
                            }
                            byte[] formatted = new byte[numSize];
                            for (int i = 0; i < numSize; ++i)
                            {
                                formatted[i] = tempBytes[i];
                            }
                            sck.Send(formatted);
                        }
                        else if (SendType == "DEC")
                        {
                            numSize = 0;
                            foreach (string userStr in temp.Split())
                            {
                                tempBytes[numSize] = Convert.ToByte(userStr, 10);
                                Debug.WriteLine(tempBytes[numSize]);
                                numSize += 1;
                            }
                            byte[] formatted = new byte[numSize];
                            for (int i = 0; i < numSize; ++i)
                            {
                                formatted[i] = tempBytes[i];
                            }

                            sck.Send(formatted);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }                
            }
            else //Serial Port Send
            {
                try
                {
                    byte[] tempBytes = new byte[200];
                    byte[] tempbyte1 = new byte[1];
                    byte[] tempbyte2 = new byte[2];
                    bool bIsHeader = false;
                    int[] bCRC = new int[2];
                    int bCRCintValue = 0;
                    byte[] sendbytes = new byte[2];

                    tempbyte1[0] = 0x02;
                    tempbyte2[0] = 0x0D;
                    tempbyte2[1] = 0x0A;

                    int numSize;
                    if (!serialPort1.IsOpen) return;
                    if (tb_Send.Text == "") return;

                    string temp = tb_Send.Text.Trim();
                    string userStr2 = tb_Header.Text.Trim();

                    if (tb_Header.Text=="") bIsHeader = false;
                    else bIsHeader = true;

                    if (bIsHeader) tempbyte1[0] = Convert.ToByte(userStr2, 16);

                    BeginInvoke(new Action(() => tb_Display.AppendText("\r\n" + temp +"\r\n")));// + "\r\n")));

                    bPreData = 0x03;

                    if (SendType == "ASCII")
                    {
                        if (bIsHeader) serialPort1.Write(tempbyte1, 0, 1);
                        serialPort1.Write(temp);

                        if (cb_HearControl.Checked)
                        {
                            byte[] data = Encoding.UTF8.GetBytes(tb_Send.Text);
                            for (int i = 0; i < data.Length; i++)
                            {
                                bCRCintValue = bCRCintValue + data[i];
                            }
                            bCRCintValue = bCRCintValue & 0xFF;
                            bCRC[0] = (bCRCintValue & 0xF0) >> 4;
                            bCRC[1] = bCRCintValue & 0x0F;

                            if (bCRC[0] <= 9) bCRC[0] = 0x30 + bCRC[0];
                            else bCRC[0] = 'A' + bCRC[0] - 10;

                            if (bCRC[1] <= 9) bCRC[1] = 0x30 + bCRC[1];
                            else bCRC[1] = 'A' + bCRC[1] - 10;

                            sendbytes[0] = (byte)bCRC[0];
                            sendbytes[1] = (byte)bCRC[1];
                            serialPort1.Write(sendbytes, 0, 2);
                        }

                        if (cb_addCRLF.Checked) serialPort1.Write(tempbyte2, 0, 2);
                    }
                    else if (SendType == "HEX")
                    {
                        numSize = 0;
                        foreach (string userStr in temp.Split())
                        {
                            tempBytes[numSize] = Convert.ToByte(userStr, 16);
                            Debug.WriteLine(tempBytes[numSize]);
                            numSize += 1;
                        }

                        if (bIsHeader) serialPort1.Write(tempbyte1, 0, 1);
                        serialPort1.Write(tempBytes, 0, numSize);
                        if (cb_addCRLF.Checked) serialPort1.Write(tempbyte2, 0, 2);
                    }
                    else if (SendType == "DEC")
                    {
                        numSize = 0;
                        foreach (string userStr in temp.Split())
                        {
                            tempBytes[numSize] = Convert.ToByte(userStr, 10);
                            Debug.WriteLine(tempBytes[numSize]);
                            numSize += 1;
                        }

                        if (bIsHeader) serialPort1.Write(tempbyte1, 0, 1);
                        serialPort1.Write(tempBytes, 0, numSize);
                        if (cb_addCRLF.Checked) serialPort1.Write(tempbyte2, 0, 2);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btn_ClearDisplay_Click(object sender, EventArgs e)
        {
            tb_Display.Clear();
        }

        private void SerialMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (bInprogressListen)
                {
                    sck.Close();
                    bReceiveThreadStop = true;
                }
                else
                {
                    if (bThreadStart) thread[0].Abort();
                    if (bAccepted)
                    {
                        if (accepted.Connected) accepted.Close();
                    }
                    if (sck.Connected) sck.Close();
                    bReceiveThreadStop = true;
                }

                if (serialPort1 != null && serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
            }
            catch 
            { 
            
            }
        }

        private void btn_StarStopDisplay_Click(object sender, EventArgs e)
        {
            if (bStartDisplayFlag == true) { bStartDisplayFlag = false; btn_StarStopDisplay.Text = "[STOP]"; }
            else { bStartDisplayFlag = true; btn_StarStopDisplay.Text = "[START]"; }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DateTime userDateTime = new DateTime();
            userDateTime = DateTime.Now;
            string UserFileName = ".\\log\\" + userDateTime.ToString("yyyyMMdd_HHmms");
            UserFileName += ".txt";
            Debug.Print(UserFileName);
            StreamWriter file = new StreamWriter(UserFileName);
            file.WriteLine(tb_Display.Text);
            file.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PeriodicLogSaveCount += 1;
            if (PeriodicLogSaveCount>= 6000)
            {
                PeriodicLogSaveCount = 0;
                if (cb_PeriodicLogSave.Checked == true)
                {
                    Debug.Print("Periodic Log Save Enabled");
                    btn_Save_Click(sender, e);
                    btn_ClearDisplay_Click(sender, e);
                }
                else
                {
                    Debug.Print("Periodic Log Save Disabled");
                    btn_ClearDisplay_Click(sender, e);
                }
            }

            try
            {
                string tempStr = string.Empty;
                if (receiveData.Count == 0) return;
                while (receiveData.Count != 0)
                {
                    tempStr += receiveData.Dequeue();
                }
                BeginInvoke(new Action(() => tb_Display.AppendText(tempStr)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void combo_DisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayType = combo_DisplayType.SelectedItem.ToString();
        }

        private void combo_SendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendType = combo_SendType.SelectedItem.ToString();
        }

        private void funcCombo_comxClick(object sender, MouseEventArgs e)
        {
            combo_comx.Items.Clear();
            foreach (string comport in SerialPort.GetPortNames())
            {
                combo_comx.Items.Add(comport);
            }
        }

        private void timer_RepeativeSend_Tick(object sender, EventArgs e)
        {
            if (bThreadStart)
            {
                if (cb_RepeativeSend.Checked)
                {
                    if (bAccepted||sck.Connected)
                    {
                        try
                        {
                            timer_RepeativeSend.Interval = Convert.ToInt32(tb_RepeativeSendInterval.Text);
                            btn_Send_Click(sender, e);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }

            if (serialPort1.IsOpen)
            {
                if (cb_RepeativeSend.Checked)
                {
                    try
                    {
                        timer_RepeativeSend.Interval = Convert.ToInt32(tb_RepeativeSendInterval.Text);
                        btn_Send_Click(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void combo_databit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_databit.SelectedIndex == 0) serialPort1.DataBits = 7;
            if (combo_databit.SelectedIndex == 1) serialPort1.DataBits = 8;
        }

        private void combo_parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_parity.SelectedIndex == 0) serialPort1.Parity = Parity.None;
            if (combo_parity.SelectedIndex == 1) serialPort1.Parity = Parity.Odd;
            if (combo_parity.SelectedIndex == 2) serialPort1.Parity = Parity.Even;
        }

    }
}
