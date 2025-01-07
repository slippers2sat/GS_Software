using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace S2S_Ground_Station
{
    public partial class sendDialogDigipeaterCMD : Form
    {

        private operationMain _mainForm;

        public string savingDigipeaterRootPath;
        public string digipeaterRootPath;
        private Label label_destination_call_sign;
        private MaskedTextBox mTBoxDpDestiCallSign;
        private Label label_path_call_sign;
        private MaskedTextBox mTBoxDpPathCallSign;
        private CheckBox cBox_rx_mode;
        public DateTime dtAppStart = DateTime.UtcNow;

        public RichTextBox RichTextBox_DigipeaterForm
        {
            get { return rTBox_digipeater_raw_data; }
        }

        public MaskedTextBox MaskedTextBox_SourceCallSign
        {
            get { return mTBoxDpSourceCallSign; }
        }

        public sendDialogDigipeaterCMD(operationMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;  // Store reference to the main form
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(sendDialogDigipeaterCMD));
            mTBoxDpSourceCallSign = new MaskedTextBox();
            label_source_call_sign = new Label();
            label1 = new Label();
            mTBoxDpMsg = new MaskedTextBox();
            button_send_dp_cmd = new Button();
            rTBox_digipeater_raw_data = new RichTextBox();
            button_clear_dp_rtBox = new Button();
            button_save_dp_rtBox = new Button();
            button_cancel_dp_dialog = new Button();
            pictureBox1 = new PictureBox();
            label_destination_call_sign = new Label();
            mTBoxDpDestiCallSign = new MaskedTextBox();
            label_path_call_sign = new Label();
            mTBoxDpPathCallSign = new MaskedTextBox();
            cBox_rx_mode = new CheckBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // mTBoxDpSourceCallSign
            // 
            mTBoxDpSourceCallSign.BackColor = SystemColors.Window;
            mTBoxDpSourceCallSign.Cursor = Cursors.IBeam;
            mTBoxDpSourceCallSign.Font = new Font("MS Gothic", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mTBoxDpSourceCallSign.Location = new Point(226, 14);
            mTBoxDpSourceCallSign.Mask = ">CCCCCCC";
            mTBoxDpSourceCallSign.Name = "mTBoxDpSourceCallSign";
            mTBoxDpSourceCallSign.Size = new Size(93, 29);
            mTBoxDpSourceCallSign.TabIndex = 0;
            mTBoxDpSourceCallSign.TextChanged += mTBoxDpSourceCallSign_TextChanged;
            // 
            // label_source_call_sign
            // 
            label_source_call_sign.AutoSize = true;
            label_source_call_sign.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_source_call_sign.Location = new Point(12, 18);
            label_source_call_sign.Name = "label_source_call_sign";
            label_source_call_sign.Size = new Size(162, 20);
            label_source_call_sign.TabIndex = 1;
            label_source_call_sign.Text = "Source Call Sign";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 121);
            label1.Name = "label1";
            label1.Size = new Size(88, 20);
            label1.TabIndex = 2;
            label1.Text = "Message";
            // 
            // mTBoxDpMsg
            // 
            mTBoxDpMsg.BackColor = SystemColors.Window;
            mTBoxDpMsg.Cursor = Cursors.IBeam;
            mTBoxDpMsg.Font = new Font("MS Gothic", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mTBoxDpMsg.Location = new Point(116, 116);
            mTBoxDpMsg.Mask = ">CCCCCCCCCCCCCCCCCCCC";
            mTBoxDpMsg.Name = "mTBoxDpMsg";
            mTBoxDpMsg.Size = new Size(235, 29);
            mTBoxDpMsg.TabIndex = 3;
            // 
            // button_send_dp_cmd
            // 
            button_send_dp_cmd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_send_dp_cmd.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_send_dp_cmd.Location = new Point(392, 14);
            button_send_dp_cmd.Name = "button_send_dp_cmd";
            button_send_dp_cmd.Size = new Size(94, 73);
            button_send_dp_cmd.TabIndex = 4;
            button_send_dp_cmd.Text = "SEND";
            button_send_dp_cmd.UseVisualStyleBackColor = true;
            button_send_dp_cmd.Click += button_send_dp_cmd_Click;
            // 
            // rTBox_digipeater_raw_data
            // 
            rTBox_digipeater_raw_data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rTBox_digipeater_raw_data.Location = new Point(16, 151);
            rTBox_digipeater_raw_data.Name = "rTBox_digipeater_raw_data";
            rTBox_digipeater_raw_data.Size = new Size(470, 281);
            rTBox_digipeater_raw_data.TabIndex = 5;
            rTBox_digipeater_raw_data.Text = "";
            // 
            // button_clear_dp_rtBox
            // 
            button_clear_dp_rtBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_clear_dp_rtBox.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_clear_dp_rtBox.Location = new Point(26, 438);
            button_clear_dp_rtBox.Name = "button_clear_dp_rtBox";
            button_clear_dp_rtBox.Size = new Size(94, 37);
            button_clear_dp_rtBox.TabIndex = 6;
            button_clear_dp_rtBox.Text = "Clear";
            button_clear_dp_rtBox.UseVisualStyleBackColor = true;
            button_clear_dp_rtBox.Click += button_clear_dp_rtBox_Click;
            // 
            // button_save_dp_rtBox
            // 
            button_save_dp_rtBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_save_dp_rtBox.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_save_dp_rtBox.Location = new Point(381, 438);
            button_save_dp_rtBox.Name = "button_save_dp_rtBox";
            button_save_dp_rtBox.Size = new Size(94, 37);
            button_save_dp_rtBox.TabIndex = 7;
            button_save_dp_rtBox.Text = "Save";
            button_save_dp_rtBox.UseVisualStyleBackColor = true;
            button_save_dp_rtBox.Click += button_save_dp_rtBox_Click;
            // 
            // button_cancel_dp_dialog
            // 
            button_cancel_dp_dialog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button_cancel_dp_dialog.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_cancel_dp_dialog.Location = new Point(208, 438);
            button_cancel_dp_dialog.Name = "button_cancel_dp_dialog";
            button_cancel_dp_dialog.Size = new Size(94, 37);
            button_cancel_dp_dialog.TabIndex = 8;
            button_cancel_dp_dialog.Text = "Cancel";
            button_cancel_dp_dialog.UseVisualStyleBackColor = true;
            button_cancel_dp_dialog.Click += button_cancel_dp_dialog_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.ErrorImage = Properties.Resources.no_img_found;
            pictureBox1.Image = Properties.Resources.s2s_logo;
            pictureBox1.InitialImage = Properties.Resources.s2s_logo;
            pictureBox1.Location = new Point(327, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(49, 46);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // label_destination_call_sign
            // 
            label_destination_call_sign.AutoSize = true;
            label_destination_call_sign.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_destination_call_sign.Location = new Point(12, 52);
            label_destination_call_sign.Name = "label_destination_call_sign";
            label_destination_call_sign.Size = new Size(202, 20);
            label_destination_call_sign.TabIndex = 11;
            label_destination_call_sign.Text = "Destination Call Sign";
            // 
            // mTBoxDpDestiCallSign
            // 
            mTBoxDpDestiCallSign.BackColor = SystemColors.Window;
            mTBoxDpDestiCallSign.Cursor = Cursors.IBeam;
            mTBoxDpDestiCallSign.Font = new Font("MS Gothic", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mTBoxDpDestiCallSign.Location = new Point(226, 48);
            mTBoxDpDestiCallSign.Mask = ">CCCCCCC";
            mTBoxDpDestiCallSign.Name = "mTBoxDpDestiCallSign";
            mTBoxDpDestiCallSign.Size = new Size(93, 29);
            mTBoxDpDestiCallSign.TabIndex = 10;
            // 
            // label_path_call_sign
            // 
            label_path_call_sign.AutoSize = true;
            label_path_call_sign.Font = new Font("MS UI Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_path_call_sign.Location = new Point(12, 88);
            label_path_call_sign.Name = "label_path_call_sign";
            label_path_call_sign.Size = new Size(138, 20);
            label_path_call_sign.TabIndex = 13;
            label_path_call_sign.Text = "Message Path";
            // 
            // mTBoxDpPathCallSign
            // 
            mTBoxDpPathCallSign.BackColor = SystemColors.Window;
            mTBoxDpPathCallSign.Cursor = Cursors.IBeam;
            mTBoxDpPathCallSign.Enabled = false;
            mTBoxDpPathCallSign.Font = new Font("MS Gothic", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mTBoxDpPathCallSign.Location = new Point(226, 84);
            mTBoxDpPathCallSign.Mask = ">CCCCCCC";
            mTBoxDpPathCallSign.Name = "mTBoxDpPathCallSign";
            mTBoxDpPathCallSign.Size = new Size(93, 29);
            mTBoxDpPathCallSign.TabIndex = 12;
            mTBoxDpPathCallSign.Text = "WIDE1-1";
            // 
            // cBox_rx_mode
            // 
            cBox_rx_mode.AutoSize = true;
            cBox_rx_mode.Location = new Point(368, 122);
            cBox_rx_mode.Name = "cBox_rx_mode";
            cBox_rx_mode.Size = new Size(100, 24);
            cBox_rx_mode.TabIndex = 14;
            cBox_rx_mode.Text = "RX Station";
            cBox_rx_mode.UseVisualStyleBackColor = true;
            cBox_rx_mode.CheckedChanged += cBox_rx_mode_CheckedChanged;
            // 
            // sendDialogDigipeaterCMD
            // 
            BackColor = SystemColors.Menu;
            ClientSize = new Size(498, 485);
            ControlBox = false;
            Controls.Add(cBox_rx_mode);
            Controls.Add(label_path_call_sign);
            Controls.Add(mTBoxDpPathCallSign);
            Controls.Add(label_destination_call_sign);
            Controls.Add(mTBoxDpDestiCallSign);
            Controls.Add(pictureBox1);
            Controls.Add(button_cancel_dp_dialog);
            Controls.Add(button_save_dp_rtBox);
            Controls.Add(button_clear_dp_rtBox);
            Controls.Add(rTBox_digipeater_raw_data);
            Controls.Add(button_send_dp_cmd);
            Controls.Add(mTBoxDpMsg);
            Controls.Add(label1);
            Controls.Add(label_source_call_sign);
            Controls.Add(mTBoxDpSourceCallSign);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "sendDialogDigipeaterCMD";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Digipeater Mode";
            Load += sendDialogDigipeaterCMD_Load;
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private MaskedTextBox mTBoxDpSourceCallSign;
        private Label label1;
        private MaskedTextBox mTBoxDpMsg;
        private Button button_send_dp_cmd;
        private RichTextBox rTBox_digipeater_raw_data;
        private Button button_clear_dp_rtBox;
        private Button button_save_dp_rtBox;
        private Button button_cancel_dp_dialog;
        private PictureBox pictureBox1;
        private Label label_source_call_sign;

        private void button_cancel_dp_dialog_Click(object sender, EventArgs e)
        {
            _mainForm.TELEMETRY_FLAG = true;
            _mainForm.DIGIPEATER_FLAG = false;

            _mainForm.ReceiveData("MODETELE");

            rTBox_digipeater_raw_data.Text = "";
            this.Close();
        }

        private void button_clear_dp_rtBox_Click(object sender, EventArgs e)
        {
            rTBox_digipeater_raw_data.Text = "";
        }

        private void button_save_dp_rtBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rTBox_digipeater_raw_data.Text))
            {
                try
                {
                    string rawDigipeaterRx = rTBox_digipeater_raw_data.Text;

                    string result;

                    result = rawDigipeaterRx;

                    Console.WriteLine(result);

                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    fbd.Description = "Select saving folder for Digipeater";

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        Console.WriteLine(fbd.SelectedPath);
                        savingDigipeaterRootPath = fbd.SelectedPath;
                        Properties.Settings.Default.savePathDigipeater = savingDigipeaterRootPath;
                        Properties.Settings.Default.Save();

                        digipeaterRootPath = Properties.Settings.Default.savePathDigipeater;

                        savingDigipeaterRootPath = System.IO.Path.Combine(digipeaterRootPath, dtAppStart.ToString("yyyyMMdd"));

                        if (!Directory.Exists(savingDigipeaterRootPath))
                        {
                            Directory.CreateDirectory(savingDigipeaterRootPath);
                        }

                        string rawRxData = result;

                        DateTime dt = DateTime.UtcNow;
                        string time = dt.ToString("yyyyMMdd_HHmmss");
                        string folderPath = savingDigipeaterRootPath;
                        string fileName;
                        string absolutePath;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        fileName = time + "_Digipeater_raw.txt";
                        absolutePath = System.IO.Path.Combine(folderPath, fileName);
                        StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                        sw.Write(rawRxData);
                        sw.Close();

                        rTBox_digipeater_raw_data.Text = "";
                        this.Close();

                        DialogResult result_2 = MessageBox.Show(
                             $"Digipeater Data Stored",
                             "Saving completed!",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Information,
                             MessageBoxDefaultButton.Button1);
                        Console.WriteLine("Dialog");

                        _mainForm.TELEMETRY_FLAG = true;
                        _mainForm.DIGIPEATER_FLAG = false;

                        _mainForm.ReceiveData("MODETELE");

                        if (result_2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show(
                           $"Digipeater Operation Invalid \n {ex.Message}",
                           "Please input valid raw data",
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Exclamation,
                           MessageBoxDefaultButton.Button1);
                    Console.WriteLine("Dialog");
                    if (result == DialogResult.Cancel)
                    {
                        return;

                    }
                }
                finally
                {

                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                         $"Digipeater Operation Invalid \n",
                         "Empty Field",
                         MessageBoxButtons.OKCancel,
                         MessageBoxIcon.Exclamation,
                         MessageBoxDefaultButton.Button1);
                Console.WriteLine("Dialog");
                if (result == DialogResult.Cancel)
                {
                    return;

                }
            }
        }

        private void button_send_dp_cmd_Click(object sender, EventArgs e)
        {
            transmitDpAsync();
        }

        public async void transmitDpAsync()
        {

            // Disable the button
            button_send_dp_cmd.Enabled = false;
            // Run a task with a delay (e.g., 2000 ms delay)
            await Task.Run(async () =>
            {
                // Wait for (777 milliseconds) prevent multiple tx at a time
                await Task.Delay(777);
            });

            string source_call_sign = mTBoxDpSourceCallSign.Text;
            string destination_call_sign = mTBoxDpDestiCallSign.Text;
            string call_sign_path = mTBoxDpPathCallSign.Text;
            string message = mTBoxDpMsg.Text;

            if (!string.IsNullOrEmpty(source_call_sign) && !string.IsNullOrEmpty(call_sign_path) && !string.IsNullOrEmpty(message))
            {

                if (string.IsNullOrEmpty(destination_call_sign))
                {
                    destination_call_sign = "NOCALL ";
                }

                //var send_to_radio = System.IO.Path.Combine(destination_call_sign, source_call_sign, call_sign_path, message);
                //  var send_to_radio = source_call_sign + ">" + destination_call_sign + "," + call_sign_path + ":" + message;
                  var send_to_radio = "S2S" + destination_call_sign + ">" + source_call_sign + ","+ call_sign_path + ":" + message;

                _mainForm.ReceiveData(send_to_radio);

                DateTime dt = DateTime.Now;
                string time = dt.ToString("yyyy/MM/dd HH:mm:ss");

                this.rTBox_digipeater_raw_data.Focus();
                this.rTBox_digipeater_raw_data.AppendText("#(" + time + ")" + " DP-CMD:\n" + source_call_sign  + ">" + destination_call_sign + "," + call_sign_path + ":" + message + "\n\n");

            }
            else
            {
                DialogResult result = MessageBox.Show(
                        "Digipeater Command not complete\n",
                        "Invalid Command",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);
                Console.WriteLine("Dialog");
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            // Enable the button after the delay
            button_send_dp_cmd.Invoke((Action)(() => button_send_dp_cmd.Enabled = true));
        }

        public void getReceivedDpPacket(string data)
        {
            DateTime dt = DateTime.Now;
            string time = dt.ToString("yyyy/MM/dd HH:mm:ss");

        }

        private void sendDialogDigipeaterCMD_Load(object sender, EventArgs e)
        {
            mTBoxDpSourceCallSign.Text = Properties.Settings.Default.callsign;
        }

        private void mTBoxDpSourceCallSign_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.callsign = mTBoxDpSourceCallSign.Text;
            Properties.Settings.Default.Save();
            _mainForm.TextBoxCallSign.Text = Properties.Settings.Default.callsign;
        }

        private void cBox_rx_mode_CheckedChanged(object sender, EventArgs e)
        {
            if (cBox_rx_mode.Checked)
            {
                mTBoxDpSourceCallSign.Enabled = false;
                mTBoxDpDestiCallSign.Enabled = false;
                mTBoxDpPathCallSign.Enabled = false;
                mTBoxDpMsg.Enabled = false;
                button_send_dp_cmd.Enabled = false;

                _mainForm.ReceiveData("MODEDIGI");

                _mainForm.TELEMETRY_FLAG = false;
                _mainForm.DIGIPEATER_FLAG = true;
            }
            else
            {
                mTBoxDpSourceCallSign.Enabled = true;
                mTBoxDpDestiCallSign.Enabled = true;
                mTBoxDpPathCallSign.Enabled = true;
                mTBoxDpMsg.Enabled = true;
                button_send_dp_cmd.Enabled = true;

                _mainForm.ReceiveData("MODETELE");

                _mainForm.TELEMETRY_FLAG = true;
                _mainForm.DIGIPEATER_FLAG = false;
            }
        }
    }
}
