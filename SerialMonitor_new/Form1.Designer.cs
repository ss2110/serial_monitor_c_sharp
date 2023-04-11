namespace SerialMonitor_new
{
    partial class SerialMonitor
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btn_Send = new System.Windows.Forms.Button();
            this.tb_Display = new System.Windows.Forms.TextBox();
            this.tb_Send = new System.Windows.Forms.TextBox();
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.combo_baudrate = new System.Windows.Forms.ComboBox();
            this.combo_comx = new System.Windows.Forms.ComboBox();
            this.btn_ClearDisplay = new System.Windows.Forms.Button();
            this.btn_StarStopDisplay = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.combo_DisplayType = new System.Windows.Forms.ComboBox();
            this.combo_SendType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_PeriodicLogSave = new System.Windows.Forms.CheckBox();
            this.cb_RepeativeSend = new System.Windows.Forms.CheckBox();
            this.tb_RepeativeSendInterval = new System.Windows.Forms.TextBox();
            this.timer_RepeativeSend = new System.Windows.Forms.Timer(this.components);
            this.cb_TcpIpConnect = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_selectServerClient = new System.Windows.Forms.ComboBox();
            this.tb_Address = new System.Windows.Forms.TextBox();
            this.tb_PortNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_databit = new System.Windows.Forms.ComboBox();
            this.combo_parity = new System.Windows.Forms.ComboBox();
            this.cb_addCRLF = new System.Windows.Forms.CheckBox();
            this.tb_Header = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_HearControl = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataBits = 7;
            this.serialPort1.PortName = "COM9";
            this.serialPort1.ReadBufferSize = 10240;
            this.serialPort1.WriteBufferSize = 10240;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btn_Send
            // 
            this.btn_Send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Send.Location = new System.Drawing.Point(923, 66);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 40);
            this.btn_Send.TabIndex = 0;
            this.btn_Send.Text = "SEND";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // tb_Display
            // 
            this.tb_Display.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tb_Display.Location = new System.Drawing.Point(12, 112);
            this.tb_Display.MaxLength = 3276700;
            this.tb_Display.Multiline = true;
            this.tb_Display.Name = "tb_Display";
            this.tb_Display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Display.Size = new System.Drawing.Size(986, 437);
            this.tb_Display.TabIndex = 1;
            // 
            // tb_Send
            // 
            this.tb_Send.Location = new System.Drawing.Point(364, 66);
            this.tb_Send.Multiline = true;
            this.tb_Send.Name = "tb_Send";
            this.tb_Send.Size = new System.Drawing.Size(552, 40);
            this.tb_Send.TabIndex = 2;
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(923, 12);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 46);
            this.btn_Open.TabIndex = 3;
            this.btn_Open.Text = "[OPEN]";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(841, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 46);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "[CLOSE]";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // combo_baudrate
            // 
            this.combo_baudrate.FormattingEnabled = true;
            this.combo_baudrate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.combo_baudrate.Location = new System.Drawing.Point(753, 38);
            this.combo_baudrate.Name = "combo_baudrate";
            this.combo_baudrate.Size = new System.Drawing.Size(82, 20);
            this.combo_baudrate.TabIndex = 5;
            // 
            // combo_comx
            // 
            this.combo_comx.FormattingEnabled = true;
            this.combo_comx.Location = new System.Drawing.Point(753, 12);
            this.combo_comx.Name = "combo_comx";
            this.combo_comx.Size = new System.Drawing.Size(82, 20);
            this.combo_comx.TabIndex = 6;
            this.combo_comx.MouseClick += new System.Windows.Forms.MouseEventHandler(this.funcCombo_comxClick);
            // 
            // btn_ClearDisplay
            // 
            this.btn_ClearDisplay.Location = new System.Drawing.Point(96, 12);
            this.btn_ClearDisplay.Name = "btn_ClearDisplay";
            this.btn_ClearDisplay.Size = new System.Drawing.Size(76, 46);
            this.btn_ClearDisplay.TabIndex = 7;
            this.btn_ClearDisplay.Text = "[CLEAR]";
            this.btn_ClearDisplay.UseVisualStyleBackColor = true;
            this.btn_ClearDisplay.Click += new System.EventHandler(this.btn_ClearDisplay_Click);
            // 
            // btn_StarStopDisplay
            // 
            this.btn_StarStopDisplay.Location = new System.Drawing.Point(12, 12);
            this.btn_StarStopDisplay.Name = "btn_StarStopDisplay";
            this.btn_StarStopDisplay.Size = new System.Drawing.Size(78, 46);
            this.btn_StarStopDisplay.TabIndex = 8;
            this.btn_StarStopDisplay.Text = "[START]";
            this.btn_StarStopDisplay.UseVisualStyleBackColor = true;
            this.btn_StarStopDisplay.Click += new System.EventHandler(this.btn_StarStopDisplay_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(178, 12);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 46);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "[SAVE]";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // combo_DisplayType
            // 
            this.combo_DisplayType.FormattingEnabled = true;
            this.combo_DisplayType.Items.AddRange(new object[] {
            "HEX",
            "DEC",
            "ASCII"});
            this.combo_DisplayType.Location = new System.Drawing.Point(579, 40);
            this.combo_DisplayType.Name = "combo_DisplayType";
            this.combo_DisplayType.Size = new System.Drawing.Size(100, 20);
            this.combo_DisplayType.TabIndex = 10;
            this.combo_DisplayType.SelectedIndexChanged += new System.EventHandler(this.combo_DisplayType_SelectedIndexChanged);
            // 
            // combo_SendType
            // 
            this.combo_SendType.FormattingEnabled = true;
            this.combo_SendType.Items.AddRange(new object[] {
            "HEX",
            "DEC",
            "ASCII"});
            this.combo_SendType.Location = new System.Drawing.Point(484, 40);
            this.combo_SendType.Name = "combo_SendType";
            this.combo_SendType.Size = new System.Drawing.Size(89, 20);
            this.combo_SendType.TabIndex = 11;
            this.combo_SendType.SelectedIndexChanged += new System.EventHandler(this.combo_SendType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "Send FORMAT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(577, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Display FORMAT";
            // 
            // cb_PeriodicLogSave
            // 
            this.cb_PeriodicLogSave.AutoSize = true;
            this.cb_PeriodicLogSave.Location = new System.Drawing.Point(258, 14);
            this.cb_PeriodicLogSave.Name = "cb_PeriodicLogSave";
            this.cb_PeriodicLogSave.Size = new System.Drawing.Size(102, 16);
            this.cb_PeriodicLogSave.TabIndex = 13;
            this.cb_PeriodicLogSave.Text = "Periodic Save";
            this.cb_PeriodicLogSave.UseVisualStyleBackColor = true;
            // 
            // cb_RepeativeSend
            // 
            this.cb_RepeativeSend.AutoSize = true;
            this.cb_RepeativeSend.Location = new System.Drawing.Point(366, 19);
            this.cb_RepeativeSend.Name = "cb_RepeativeSend";
            this.cb_RepeativeSend.Size = new System.Drawing.Size(112, 16);
            this.cb_RepeativeSend.TabIndex = 14;
            this.cb_RepeativeSend.Text = "Repeative Send";
            this.cb_RepeativeSend.UseVisualStyleBackColor = true;
            // 
            // tb_RepeativeSendInterval
            // 
            this.tb_RepeativeSendInterval.Location = new System.Drawing.Point(364, 40);
            this.tb_RepeativeSendInterval.Name = "tb_RepeativeSendInterval";
            this.tb_RepeativeSendInterval.Size = new System.Drawing.Size(113, 21);
            this.tb_RepeativeSendInterval.TabIndex = 15;
            // 
            // timer_RepeativeSend
            // 
            this.timer_RepeativeSend.Tick += new System.EventHandler(this.timer_RepeativeSend_Tick);
            // 
            // cb_TcpIpConnect
            // 
            this.cb_TcpIpConnect.AutoSize = true;
            this.cb_TcpIpConnect.Location = new System.Drawing.Point(12, 68);
            this.cb_TcpIpConnect.Name = "cb_TcpIpConnect";
            this.cb_TcpIpConnect.Size = new System.Drawing.Size(70, 16);
            this.cb_TcpIpConnect.TabIndex = 16;
            this.cb_TcpIpConnect.Text = "TCP/IP ";
            this.cb_TcpIpConnect.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "ADDRESS";
            // 
            // combo_selectServerClient
            // 
            this.combo_selectServerClient.FormattingEnabled = true;
            this.combo_selectServerClient.Items.AddRange(new object[] {
            "SERVER",
            "CLIENT"});
            this.combo_selectServerClient.Location = new System.Drawing.Point(12, 86);
            this.combo_selectServerClient.Name = "combo_selectServerClient";
            this.combo_selectServerClient.Size = new System.Drawing.Size(78, 20);
            this.combo_selectServerClient.TabIndex = 22;
            // 
            // tb_Address
            // 
            this.tb_Address.Location = new System.Drawing.Point(96, 85);
            this.tb_Address.Name = "tb_Address";
            this.tb_Address.Size = new System.Drawing.Size(104, 21);
            this.tb_Address.TabIndex = 23;
            // 
            // tb_PortNum
            // 
            this.tb_PortNum.Location = new System.Drawing.Point(206, 85);
            this.tb_PortNum.Name = "tb_PortNum";
            this.tb_PortNum.Size = new System.Drawing.Size(46, 21);
            this.tb_PortNum.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "PORT#";
            // 
            // combo_databit
            // 
            this.combo_databit.FormattingEnabled = true;
            this.combo_databit.Items.AddRange(new object[] {
            "7-bit",
            "8-bit"});
            this.combo_databit.Location = new System.Drawing.Point(685, 12);
            this.combo_databit.Name = "combo_databit";
            this.combo_databit.Size = new System.Drawing.Size(62, 20);
            this.combo_databit.TabIndex = 26;
            this.combo_databit.SelectedIndexChanged += new System.EventHandler(this.combo_databit_SelectedIndexChanged);
            // 
            // combo_parity
            // 
            this.combo_parity.FormattingEnabled = true;
            this.combo_parity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.combo_parity.Location = new System.Drawing.Point(685, 39);
            this.combo_parity.Name = "combo_parity";
            this.combo_parity.Size = new System.Drawing.Size(62, 20);
            this.combo_parity.TabIndex = 27;
            this.combo_parity.SelectedIndexChanged += new System.EventHandler(this.combo_parity_SelectedIndexChanged);
            // 
            // cb_addCRLF
            // 
            this.cb_addCRLF.AutoSize = true;
            this.cb_addCRLF.Location = new System.Drawing.Point(258, 50);
            this.cb_addCRLF.Name = "cb_addCRLF";
            this.cb_addCRLF.Size = new System.Drawing.Size(84, 16);
            this.cb_addCRLF.TabIndex = 28;
            this.cb_addCRLF.Text = "add CR LF";
            this.cb_addCRLF.UseVisualStyleBackColor = true;
            // 
            // tb_Header
            // 
            this.tb_Header.Location = new System.Drawing.Point(258, 85);
            this.tb_Header.Name = "tb_Header";
            this.tb_Header.Size = new System.Drawing.Size(100, 21);
            this.tb_Header.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "Header(hex)";
            // 
            // cb_HearControl
            // 
            this.cb_HearControl.AutoSize = true;
            this.cb_HearControl.Location = new System.Drawing.Point(258, 32);
            this.cb_HearControl.Name = "cb_HearControl";
            this.cb_HearControl.Size = new System.Drawing.Size(104, 16);
            this.cb_HearControl.TabIndex = 31;
            this.cb_HearControl.Text = "Heater Control";
            this.cb_HearControl.UseVisualStyleBackColor = true;
            // 
            // SerialMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 561);
            this.Controls.Add(this.cb_HearControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_Header);
            this.Controls.Add(this.cb_addCRLF);
            this.Controls.Add(this.combo_parity);
            this.Controls.Add(this.combo_databit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_PortNum);
            this.Controls.Add(this.tb_Address);
            this.Controls.Add(this.combo_selectServerClient);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_TcpIpConnect);
            this.Controls.Add(this.tb_RepeativeSendInterval);
            this.Controls.Add(this.cb_RepeativeSend);
            this.Controls.Add(this.cb_PeriodicLogSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combo_SendType);
            this.Controls.Add(this.combo_DisplayType);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_StarStopDisplay);
            this.Controls.Add(this.btn_ClearDisplay);
            this.Controls.Add(this.combo_comx);
            this.Controls.Add(this.combo_baudrate);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Open);
            this.Controls.Add(this.tb_Send);
            this.Controls.Add(this.tb_Display);
            this.Controls.Add(this.btn_Send);
            this.MaximumSize = new System.Drawing.Size(1026, 600);
            this.MinimumSize = new System.Drawing.Size(1026, 600);
            this.Name = "SerialMonitor";
            this.Text = "Serial Monitor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SerialMonitor_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox tb_Display;
        private System.Windows.Forms.TextBox tb_Send;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ComboBox combo_baudrate;
        private System.Windows.Forms.ComboBox combo_comx;
        private System.Windows.Forms.Button btn_ClearDisplay;
        private System.Windows.Forms.Button btn_StarStopDisplay;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox combo_DisplayType;
        private System.Windows.Forms.ComboBox combo_SendType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_PeriodicLogSave;
        private System.Windows.Forms.CheckBox cb_RepeativeSend;
        private System.Windows.Forms.TextBox tb_RepeativeSendInterval;
        private System.Windows.Forms.Timer timer_RepeativeSend;
        private System.Windows.Forms.CheckBox cb_TcpIpConnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combo_selectServerClient;
        private System.Windows.Forms.TextBox tb_Address;
        private System.Windows.Forms.TextBox tb_PortNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_databit;
        private System.Windows.Forms.ComboBox combo_parity;
        private System.Windows.Forms.CheckBox cb_addCRLF;
        private System.Windows.Forms.TextBox tb_Header;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_HearControl;
    }
}

