using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using System.IO.Ports;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace S2S_Ground_Station
{
    public partial class operationMain : Form
    {

        public commandList commandList;

        public inputDialogBeacon inputDialogBeacon;

        public inputDialogCamera inputDialogCamera;

        public inputDialogADCS inputDialogADCS;

        public inputDialogEPDM inputDialogEPDM;

        public inputDialogHK inputDialogHK;

        public sendDialogDigipeaterCMD sendDialogDigipeaterCMD;

        private bool _telemetryFlag = true;  // Backing field

        private bool _digipeaterFlag = false;  // Backing field


        public bool TELEMETRY_FLAG
        {
            set
            {
                _telemetryFlag = value;
            }
            get
            {
                return _telemetryFlag;
            }
        }

        public bool DIGIPEATER_FLAG
        {
            set
            {
                _digipeaterFlag = value;
            }
            get
            {
                return _digipeaterFlag;

            }

        }

        public string CommandString
        {
            set
            {
                command_HEX.Text = value;
            }
            get
            {
                return command_HEX.Text;
            }
        }

        public string CommentString
        {
            set
            {
                comments.Text = value;
            }
            get
            {
                return comments.Text;
            }
        }

        private SerialPort RadioPort = new SerialPort();

        public string call_sign;

        public string savingRootPath;
        public string rootPath;
        public DateTime dtAppStart = DateTime.UtcNow;

        public string freqPath;

        public double latitude, longitude, altitude;
        public double azHomeValue, elHomeValue;

        private bool RadioPortConnected = false;

        private bool freqSelected = false;

        List<string> upFreqList = new List<string>();
        public string upFreq;

        private delegate void AddRxDataDelegate(string data);

        int receivedPacketNumber = 0;

        // Declare a buffer to accumulate received data
        private List<byte> receivedBuffer = new List<byte>();

        // List to store complete messages
        private List<byte[]> completeMessages = new List<byte[]>();

        public MaskedTextBox TextBoxCallSign
        {
            get { return mTBoxCallsign; }
        }

        public operationMain()
        {
            InitializeComponent();
            RadioPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            commandList = new commandList();
            commandList.formMain = this;
            commandList.Show();

            updateSavingFolderPath();

        }
        private void setSatelliteComboList()
        {
            //Parse tle from file
            if (!System.IO.File.Exists(freqPath))
            {
                MessageBox.Show("Frequency Fiile doesn't exist!");
                return;
            }

            satelliteNameComboBox.DataSource = null;
            satelliteNameComboBox.Items.Clear();

            using (var parser = new TextFieldParser(freqPath))
            {
                if (parser != null)
                {

                    parser.Delimiters = new string[] { "," };

                    // Skip the header row
                    parser.ReadLine();

                    while (!parser.EndOfData)
                    {
                        var fields = parser.ReadFields();

                        if (fields != null)
                        {
                            // Add only the first column (fields[0]) to the ComboBox
                            satelliteNameComboBox.Items.Add(fields[0]);

                            // Check if the value in fields[3] is a valid number
                            if (double.TryParse(fields[3], out double frequencyValue))
                            {
                                // Divide the value by 1,000,000 and assign to the label
                                upFreq = (frequencyValue / 1000000.0).ToString("F6");
                                upFreqList.Add(upFreq);
                            }
                        }
                    }
                }
            }
        }

        private void updateSavingFolderPath()
        {
            rootPath = Properties.Settings.Default.saveFilePath;

            savingRootPath = System.IO.Path.Combine(rootPath, dtAppStart.ToString("yyyyMMdd"));

            try
            {
                if (!Directory.Exists(rootPath))
                {
                    Directory.CreateDirectory(rootPath);
                }
                if (!Directory.Exists(savingRootPath))
                {
                    Directory.CreateDirectory(savingRootPath);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // disable transmit button
            transmitButton.Enabled = false;

            // when program is loaded the groundstation is in telemetry mode
            radio_button_telemetry.Checked = false;
            radio_button_digipeater.Checked = false;

            radio_button_telemetry.Enabled = false;
            radio_button_digipeater.Enabled = false;

            // when program is loaded the com port are disabled
            radioPortComboBox.Enabled = false;
            radioBaudComboBox.Enabled = false;
            radioButtonConnect.Enabled = false;

            //reset the path variable
            savingFolderPathTextBox.Text = null;
            savingFolderPathTextBox.Text = savingRootPath;

            //disable reveived data and save
            saveReceiveButton.Enabled = false;

            radioPortComboBox.Items.Clear();

            //Add port list to each COM port ComboBox
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                radioPortComboBox.Items.Add(port);
            }

            //enabling all
            radioPortComboBox.Enabled = true;
            radioBaudComboBox.Enabled = true;
            radioButtonConnect.Enabled = true;

            if (radioPortComboBox.Items.Count >= 1)
            {
                radioPortComboBox.SelectedIndex = 0;
            }

            freqPathTextBox.Text = null;
            //        freqPath = Properties.Settings.Default.freqPath;
            //  freqPathTextBox.Text = freqPath;

            latitudeTextBox.Text = null; longitudeTextBox.Text = null; altitudeTextBox.Text = null;

            latitudeTextBox.Text = Properties.Settings.Default.latitude.ToString("0.0000");
            longitudeTextBox.Text = Properties.Settings.Default.longitude.ToString("0.0000");
            altitudeTextBox.Text = Properties.Settings.Default.altitude.ToString("0.0");

            latitude = Convert.ToDouble(latitudeTextBox.Text);
            longitude = Convert.ToDouble(longitudeTextBox.Text);
            altitude = Convert.ToDouble(altitudeTextBox.Text);

            azHomeValue = Properties.Settings.Default.azHome;
            elHomeValue = Properties.Settings.Default.elHome;

            azimuthLabel.Text = azHomeValue.ToString(".0");
            elevationLabel.Text = elHomeValue.ToString(".0");

            mTBoxCallsign.Enabled = false;
            call_sign = Properties.Settings.Default.callsign;
            mTBoxCallsign.Text = call_sign;
        }

        private void transmitButton_Click(object sender, EventArgs e)
        {
            transmitAsync();
        }

        public async void transmitAsync()
        {

            // Disable the button
            transmitButton.Enabled = false;

            // Run a task with a delay (e.g., 2000 ms delay)
            await Task.Run(async () =>
            {
                // Wait for (777 milliseconds) prevent multiple tx at a time
                await Task.Delay(777);
            });

            string command = command_HEX.Text;
            string comment = comments.Text;

            var baudrate = radioPortComboBox.Text;

            if (radio_button_telemetry.Checked == true)
            {
                if (Regex.IsMatch(command, ".. .. .. .. .. .. .. .. .. .. .. .. .."))
                {
                    Console.WriteLine("13 bytes command");
                    command = command.Substring(0, 38);
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                   "The command length is invalid",
                    "Enter valid command.",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);
                    Console.WriteLine("Dialog");
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (command == "00 00 00 00 00 00 00 00 00 00 00 00 00")
                {
                    DialogResult result = MessageBox.Show(
                        "The command is all '00'\n",
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
                else
                {
                    if (RadioPort.IsOpen)
                    {

                        if (TELEMETRY_FLAG == true && DIGIPEATER_FLAG == false)
                        {

                            if (!string.IsNullOrEmpty(mTBoxCallsign.Text))
                            {
                                var temp_command = command + " " + mTBoxCallsign.Text + " 7e ";

                                if (temp_command.Length != 51)
                                {
                                    temp_command = temp_command.PadRight(51, '0');
                                }

                                //   var temp_command = command;
                                RadioPort.WriteLine(temp_command);

                                DateTime dt = DateTime.Now;
                                string time = dt.ToString("yyyy/MM/dd HH:mm:ss");

                                this.rxDataTextBox.Focus();
                                this.rxDataTextBox.AppendText("#(" + time + ")" + " CMD: " + command + "\n\n");
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show(
                                    $"You missed your callsign\n",
                                    "Operation Failed!",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                                Console.WriteLine("Dialog");
                                if (result == DialogResult.Cancel)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                // Enable the button after the delay
                transmitButton.Invoke((Action)(() => transmitButton.Enabled = true));
            }
        }

        // Method to receive data from next Form
        public void ReceiveData(string data)
        {
            if (RadioPort.IsOpen)
            {
                var temp_data = data + " 7e ";

                //var temp_data = data;

                if (temp_data.Length != 51)
                {
                    temp_data = temp_data.PadRight(51, '0');
                }

                RadioPort.WriteLine(temp_data);

            }
        }

        private void ClearRxDataButton_Click(object sender, EventArgs e)
        {
            receivedPacketNumber = 0;
            rxPacketCount.Text = receivedPacketNumber.ToString();
            rxDataTextBox.Text = "";
        }

        private void saveReceiveButton_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(rxDataTextBox.Text))
            {

                string rawRxData = rxDataTextBox.Text;

                DateTime dt = DateTime.UtcNow;
                string time = dt.ToString("yyyyMMdd_HHmmss");
                string folderPath = savingRootPath;
                //string folderPath = System.IO.Path.Combine(savingRootPath, satFolderName);
                string fileName;
                string absolutePath;

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                fileName = time + "_LOG_raw.txt";
                absolutePath = System.IO.Path.Combine(folderPath, fileName);
                StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                sw.Write(rawRxData);
                sw.Close();

                ClearRxDataButton_Click(sender, e);
            }
            else
            {
                DialogResult result = MessageBox.Show(
                         $"Save Operation Invalid \n",
                         "Empty Field",
                         MessageBoxButtons.OKCancel,
                         MessageBoxIcon.Stop,
                         MessageBoxDefaultButton.Button1);
                Console.WriteLine("Dialog");
                if (result == DialogResult.Cancel)
                {
                    return;

                }
            }
        }

        private void radioButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                radio_button_telemetry.Checked = true;

                if (!RadioPortConnected)
                {
                    RadioPort.PortName = radioPortComboBox.Text;
                    RadioPort.BaudRate = Convert.ToInt32(radioBaudComboBox.SelectedItem);
                    RadioPort.DataBits = 8;
                    RadioPort.Parity = Parity.None;
                    RadioPort.StopBits = StopBits.One;
                    RadioPort.RtsEnable = true;
                    RadioPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                    RadioPort.Open();
                    RadioPortConnected = true;
                    radioPortComboBox.Enabled = false;

                    radio_button_telemetry.Enabled = true;
                    radio_button_digipeater.Enabled = true;

                    radioBaudComboBox.Enabled = false;
                    radioButtonConnect.Text = "Disconnect";
                }
                else
                {
                    RadioPort.Close();
                    RadioPortConnected = false;
                    radioPortComboBox.Enabled = true;

                    radio_button_telemetry.Enabled = false;
                    radio_button_digipeater.Enabled = false;

                    radioBaudComboBox.Enabled = true;
                    radioButtonConnect.Text = "Connect";
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(
                               $"Problem in connection to {radioPortComboBox.Text}\n {ex.Message}",
                               "Port not found",
                               MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2);
                Console.WriteLine("Dialog");
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            finally
            {
                if (RadioPortConnected && freqSelected && radio_button_telemetry != null && radio_button_telemetry.Checked)
                {
                    transmitButton.Enabled = true;
                }
                else
                {
                    transmitButton.Enabled = false;
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            radioPortComboBox.DataSource = null; // Unbind the data source
            radioPortComboBox.Items.Clear();

            if (!RadioPortConnected)
            {
                radioPortComboBox.Items.Clear();
                //add port list into each COM Port ComboBox
                string[] ports = SerialPort.GetPortNames();
                radioPortComboBox.Items.Clear();

                foreach (string port in ports)
                {
                    radioPortComboBox.Items.Add(port);
                }

                //Number of COM port is more than 1 -> Radio COM port is enabled
                if (radioPortComboBox.Items.Count >= 1)
                {
                    radioPortComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                   "Please Disconnect all COM-PORT",
                   $"COM port Busy!",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button2);
                Console.WriteLine("Dialog");
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void latitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (latitudeTextBox.Text == "")
            {
                return;
            }
            try
            {
                latitude = Convert.ToDouble(latitudeTextBox.Text);
                Properties.Settings.Default.latitude = latitude;
                Properties.Settings.Default.Save();
            }
            catch
            {
                latitudeTextBox.Text = latitude.ToString("0.0000");
            }
        }

        private void longitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (longitudeTextBox.Text == "")
            {
                return;
            }
            try
            {
                longitude = Convert.ToDouble(longitudeTextBox.Text);
                Properties.Settings.Default.longitude = longitude;
                Properties.Settings.Default.Save();
            }
            catch
            {
                longitudeTextBox.Text = longitude.ToString("0.0000");
            }
        }

        private void altitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (altitudeTextBox.Text == "")
            {
                return;
            }
            try
            {
                altitude = Convert.ToDouble(altitudeTextBox.Text);
                Properties.Settings.Default.altitude = Convert.ToDouble(altitudeTextBox.Text);
                Properties.Settings.Default.Save();
            }
            catch
            {
                altitudeTextBox.Text = altitude.ToString("0.0");
            }
        }

        private void selectSavingFolderPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "Select saving folder";
            //fbd.SelectedPath = savingFolderPathTextBox.Text;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);
                var selPath = fbd.SelectedPath;
                savingRootPath = selPath;
                savingFolderPathTextBox.Text = System.IO.Path.Combine(selPath, dtAppStart.ToString("yyyyMMdd"));
                Properties.Settings.Default.saveFilePath = savingRootPath;
                Properties.Settings.Default.Save();
            }
        }

        private void selecteFreqPathButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog class instance
            OpenFileDialog ofd = new OpenFileDialog();

            //specify the first file name
            //specify the string display in the file name
            ofd.FileName = "FreqList.csv";
            //specify the choices that appears in the File Type
            //if specified all files will be displayed
            ofd.Filter = "CSV file(*.csv)|*.*";
            //set the title
            ofd.Title = "Select the frequency list file";
            //the current directory before closing the dialog box
            ofd.RestoreDirectory = true;

            //display dialog
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //dipslay the selected file name when OK button is clicked
                Console.WriteLine(ofd.FileName);
                freqPath = ofd.FileName;
                freqPathTextBox.Text = freqPath;
                Properties.Settings.Default.freqPath = freqPath;
                Properties.Settings.Default.Save();
                setSatelliteComboList();
            }
        }

        private void satelliteNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected index in the ComboBox
            int selectedIndex = satelliteNameComboBox.SelectedIndex;

            // Check if the selected index is valid (not -1)
            if (selectedIndex != -1)
            {
                // Select the corresponding index in the uList (the data is already linked)
                string selectedValueFromList = upFreqList[selectedIndex];

                freqLabel.Text = selectedValueFromList;
                freqSelected = true;
            }

            if (RadioPortConnected && freqSelected && radio_button_telemetry != null && radio_button_telemetry.Checked)
            {
                transmitButton.Enabled = true;
            }
            else
            {
                transmitButton.Enabled = false;
            }
        }

        private void AddRxData(string data)
        {
            this.rxDataTextBox.Focus();
            this.rxDataTextBox.AppendText(data);
            receivedPacketNumber += (data.Split('\n').Length - 1);
            rxPacketCount.Text = receivedPacketNumber.ToString();
        }

        private void AddRxDPData(string data)
        {
            sendDialogDigipeaterCMD.RichTextBox_DigipeaterForm.Focus();
            sendDialogDigipeaterCMD.RichTextBox_DigipeaterForm.AppendText(data);
        }


        private void DataReceived(object Sender, SerialDataReceivedEventArgs e)
        {

            if (TELEMETRY_FLAG == true && DIGIPEATER_FLAG == false)
            {
                AddRxDataDelegate add = new AddRxDataDelegate(AddRxData);
                byte[] SerialGetData = new byte[RadioPort.BytesToRead];
                string ReceivedDataString = string.Empty;

                try
                {
                    RadioPort.Read(SerialGetData, 0, SerialGetData.Length);
                    updateSaveReceiveButton();

                    for (int i = 0; i < SerialGetData.Length; i++)
                    {
                        byte EachData = SerialGetData[i];
                        ReceivedDataString += string.Format("{0:x2} ", EachData);
                    }
                }

                catch
                {
                    //Protect error
                }
                finally
                {

                    DateTime dt = DateTime.Now;
                    string time = dt.ToString("yyyy/MM/dd HH:mm:ss");

                    this.rxDataTextBox.Invoke(add, ReceivedDataString);
                    ReceivedDataString = string.Empty;
                    SerialGetData = new byte[0];
                    this.rxDataTextBox.Invoke(add, "\n");
                }
            }
            else if (TELEMETRY_FLAG == false && DIGIPEATER_FLAG == true)
            {
                AddRxDataDelegate add_dp = new AddRxDataDelegate(AddRxDPData);
                byte[] SerialGetData_DP = new byte[RadioPort.BytesToRead];
                string ReceivedDataString_DP = string.Empty;

                try
                {
                    RadioPort.Read(SerialGetData_DP, 0, SerialGetData_DP.Length);

                    for (int i = 0; i < SerialGetData_DP.Length; i++)
                    {
                        byte EachData = SerialGetData_DP[i];
                        ReceivedDataString_DP += string.Format("{0:x2} ", EachData);
                    }
                }

                catch
                {
                    //Protect error
                }
                finally
                {
                    DateTime dt = DateTime.Now;
                    string time = dt.ToString("yyyy/MM/dd HH:mm:ss");

                    // Remove spaces to ensure the string has only hex characters
                    ReceivedDataString_DP = ReceivedDataString_DP.Replace(" ", "");
                    string asciiString = "";

                    // Loop through each pair of characters (each byte) in the string
                    for (int i = 0; i < ReceivedDataString_DP.Length; i += 2)
                    {
                        // Extract the byte (2 characters at a time) and convert to byte
                        byte currentByte = Convert.ToByte(ReceivedDataString_DP.Substring(i, 2), 16);

                        // Convert the byte to its ASCII character if it's printable
                        if (currentByte >= 32 && currentByte <= 126) // ASCII printable range
                        {
                            char asciiChar = Convert.ToChar(currentByte);
                            asciiString += asciiChar; // Append the character to the string
                            Console.WriteLine($"Original byte: 0x{currentByte:X2}, ASCII: {asciiChar}");
                        }
                        else
                        {
                            Console.WriteLine($"Original byte: 0x{currentByte:X2}, Non-printable");
                        }
                    }

                    sendDialogDigipeaterCMD.RichTextBox_DigipeaterForm.Invoke(add_dp, asciiString);
                    asciiString = string.Empty;
                    SerialGetData_DP = new byte[0];
                    sendDialogDigipeaterCMD.RichTextBox_DigipeaterForm.Invoke(add_dp, "\n");
                }
            }
        }


        private void updateSaveReceiveButton()
        {
            // Check if invocation is required
            if (saveReceiveButton.InvokeRequired)
            {
                // Use Invoke to marshal the call to the UI thread
                saveReceiveButton.Invoke(new MethodInvoker(updateSaveReceiveButton));
            }
            else
            {
                // Safe to update the control directly
                saveReceiveButton.Enabled = true; // Example action
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            ClearRxDataButton_Click(sender, e);
        }

        private void button_bea_parser_Click(object sender, EventArgs e)
        {
            if (inputDialogBeacon == null || inputDialogBeacon.IsDisposed)
            {
                inputDialogBeacon = new inputDialogBeacon();
                inputDialogBeacon.Show();
            }
            else
            {
                inputDialogBeacon.BringToFront();
            }
        }

        private void button_camera_parser_Click(object sender, EventArgs e)
        {
            if (inputDialogCamera == null || inputDialogCamera.IsDisposed)
            {
                inputDialogCamera = new inputDialogCamera();
                inputDialogCamera.Show();
            }
            else
            {
                inputDialogCamera.BringToFront();
            }
        }

        private void button_adcs_parser_Click(object sender, EventArgs e)
        {
            if (inputDialogADCS == null || inputDialogADCS.IsDisposed)
            {
                inputDialogADCS = new inputDialogADCS();
                inputDialogADCS.Show();
            }
            else
            {
                inputDialogADCS.BringToFront();
            }
        }

        private void button_epdm_parser_Click(object sender, EventArgs e)
        {
            if (inputDialogEPDM == null || inputDialogEPDM.IsDisposed)
            {
                inputDialogEPDM = new inputDialogEPDM();
                inputDialogEPDM.Show();
            }
            else
            {
                inputDialogEPDM.BringToFront();
            }
        }

        private void button_hk_data_parser_Click(object sender, EventArgs e)
        {
            if (inputDialogHK == null || inputDialogHK.IsDisposed)
            {
                inputDialogHK = new inputDialogHK();
                inputDialogHK.Show();
            }
            else
            {
                inputDialogHK.BringToFront();
            }
        }

        private void button_com_refresh_com_port_Click(object sender, EventArgs e)
        {
            Console.WriteLine("refersh clicked");

            try
            {

                if (RadioPortConnected)
                {
                    RadioPort.Close();
                    RadioPortConnected = false;
                    radioPortComboBox.Enabled = true;
                    radioBaudComboBox.Enabled = true;
                    radioButtonConnect.Text = "Connect";

                    RadioPort.PortName = radioPortComboBox.Text;
                    RadioPort.BaudRate = Convert.ToInt32(radioBaudComboBox.SelectedItem);
                    RadioPort.DataBits = 8;
                    RadioPort.Parity = Parity.None;
                    RadioPort.StopBits = StopBits.One;
                    RadioPort.RtsEnable = true;
                    RadioPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                    RadioPort.Open();
                    RadioPortConnected = true;
                    radioPortComboBox.Enabled = false;
                    radioBaudComboBox.Enabled = false;
                    radioButtonConnect.Text = "Disconnect";
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                                   $"COM Port not connected: {radioPortComboBox.Text}\n",
                                   "Referesh Failed.",
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button2);
                    Console.WriteLine("Dialog");
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void radio_button_telemetry_CheckedChanged(object sender, EventArgs e)
        {

            transmitButton.Enabled = false;
            if (RadioPortConnected && freqSelected && radio_button_telemetry != null && radio_button_telemetry.Checked)
            {
                if (sendDialogDigipeaterCMD != null)
                {
                    sendDialogDigipeaterCMD.Close();
                }

                transmitButton.Enabled = true;

                TELEMETRY_FLAG = true;
                DIGIPEATER_FLAG = false;

                ReceiveData("MODETELE");

            }
            else if (RadioPortConnected && freqSelected && radio_button_digipeater != null && radio_button_digipeater.Checked)
            {
                transmitButton.Enabled = false;
            }
        }

        private void radio_button_digipeater_CheckedChanged(object sender, EventArgs e)
        {
            transmitButton.Enabled = false;
            if (RadioPortConnected && freqSelected && radio_button_digipeater != null && radio_button_digipeater.Checked)
            {
                if (sendDialogDigipeaterCMD == null || sendDialogDigipeaterCMD.IsDisposed)
                {
                    sendDialogDigipeaterCMD = new sendDialogDigipeaterCMD(this);
                    sendDialogDigipeaterCMD.Show();

                }
                else
                {
                    sendDialogDigipeaterCMD.BringToFront();

                }

                transmitButton.Enabled = false;
            }
            else if (RadioPortConnected && freqSelected && radio_button_digipeater != null && !radio_button_digipeater.Checked)
            {
                transmitButton.Enabled = true;
            }
        }

        private void radio_button_digipeater_MouseClick(object sender, MouseEventArgs e)
        {
            transmitButton.Enabled = false;
            if (RadioPortConnected && freqSelected && radio_button_digipeater != null && radio_button_digipeater.Checked)
            {
                if (sendDialogDigipeaterCMD == null || sendDialogDigipeaterCMD.IsDisposed)
                {
                    sendDialogDigipeaterCMD = new sendDialogDigipeaterCMD(this);
                    sendDialogDigipeaterCMD.Show();
                }
                else
                {
                    sendDialogDigipeaterCMD.BringToFront();
                }

                transmitButton.Enabled = false;
            }
            else if (RadioPortConnected && freqSelected && radio_button_digipeater != null && !radio_button_digipeater.Checked)
            {
                transmitButton.Enabled = true;
            }
        }

        private void button_call_sign_insert_Click(object sender, EventArgs e)
        {
            if (mTBoxCallsign.Enabled == false)
            {
                mTBoxCallsign.Enabled = true;
            }
            else if (mTBoxCallsign.Enabled == true)
            {
                mTBoxCallsign.Enabled = false;
            }
        }

        private void mTBoxCallsign_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.callsign = mTBoxCallsign.Text;
            Properties.Settings.Default.Save();

            try
            {
               
       

                if (sendDialogDigipeaterCMD == null || sendDialogDigipeaterCMD.IsDisposed)
                {
                   
                }
                else
                {
                    sendDialogDigipeaterCMD.MaskedTextBox_SourceCallSign.Text = Properties.Settings.Default.callsign;
                }
            }
            catch (Exception ex)
            {

            }

         
        }
    }
}
