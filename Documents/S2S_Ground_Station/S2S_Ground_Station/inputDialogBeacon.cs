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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace S2S_Ground_Station
{
    public partial class inputDialogBeacon : Form
    {

        public string savingBeaconRootPath;
        public string BeaconRootPath;

        public DateTime dtAppStart = DateTime.UtcNow;

        public inputDialogBeacon()
        {
            InitializeComponent();
        }

        public static string callSignDecoder(string callsign_raw)
        {
            StringBuilder source_callsign_ascii = new StringBuilder(); // Store the decoded ASCII characters

            // Loop through each pair of characters (each byte) in the string
            for (int i = 0; i < callsign_raw.Length; i += 2)
            {
                // Extract the byte (2 characters at a time) and convert to byte
                byte currentByte = Convert.ToByte(callsign_raw.Substring(i, 2), 16);

                // Perform a right shift by 1 bit
                byte shiftedByte = (byte)(currentByte >> 1);

                // Convert the shifted byte to its ASCII character if it's printable
                if (shiftedByte >= 32 && shiftedByte <= 126) // ASCII printable range
                {
                    char asciiChar = Convert.ToChar(shiftedByte);
                    source_callsign_ascii.Append(asciiChar); // Add the character to the StringBuilder
                    Console.WriteLine($"Original byte: 0x{currentByte:X2}, Shifted byte: 0x{shiftedByte:X2}, ASCII: {asciiChar}");
                }
                else
                {
                    Console.WriteLine($"Original byte: 0x{currentByte:X2}, Shifted byte: 0x{shiftedByte:X2}, Non-printable");
                }
            }

            return source_callsign_ascii.ToString(); // Return the full decoded string
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            rTBox_beacon_raw_data.Text = "";
            resetFields();
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

            resetFields();

            try
            {
                string rawBeaconRx = rTBox_beacon_raw_data.Text;
                rawBeaconRx = rawBeaconRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

                var destination_callsign = callSignDecoder(rawBeaconRx.Substring(0, 12));

                var source_callsign = callSignDecoder(rawBeaconRx.Substring(14, 12));

                var control_bit = rawBeaconRx.Substring(28, 2);

                var protocol_bit = rawBeaconRx.Substring(30, 2);

                var beacon_type = rawBeaconRx.Substring(32, 2);

                if (beacon_type.Equals("b1"))
                {
                    getBeaconType1();

                    var head = rawBeaconRx.Substring(34, 2);
                    var type = rawBeaconRx.Substring(36, 2);
                    var tim_day = rawBeaconRx.Substring(38, 2);
                    var tim_hour = rawBeaconRx.Substring(40, 2);

                    var batt_vtg = rawBeaconRx.Substring(42, 2);
                    var batt_curr = rawBeaconRx.Substring(44, 2);
                    var batt_temp = rawBeaconRx.Substring(46, 2);

                    var raw_current = rawBeaconRx.Substring(48, 2);
                    var sol_tot_vtg = rawBeaconRx.Substring(50, 2);
                    var sol_tot_current = rawBeaconRx.Substring(52, 2);

                    var ant_pannel_temp = rawBeaconRx.Substring(54, 2);
                    var bpb_temp = rawBeaconRx.Substring(56, 2);
                    var obc_temp = rawBeaconRx.Substring(58, 2);

                    var sol_y_temp = rawBeaconRx.Substring(60, 2);
                    var sol_y1_temp = rawBeaconRx.Substring(62, 2);
                    var sol_z_temp = rawBeaconRx.Substring(64, 2);
                    var sol_z1_temp = rawBeaconRx.Substring(66, 2);
                    var sol_x_temp = rawBeaconRx.Substring(68, 2);

                    var sol_p1_stat = rawBeaconRx.Substring(70, 1);
                    var sol_p2_stat = rawBeaconRx.Substring(71, 1);
                    var sol_p3_stat = rawBeaconRx.Substring(72, 1);
                    var sol_p4_stat = rawBeaconRx.Substring(73, 1);
                    var sol_p5_stat = rawBeaconRx.Substring(74, 1);

                    var msn_1_stat = rawBeaconRx.Substring(75, 1);
                    var msn_2_stat = rawBeaconRx.Substring(76, 1);
                    var msn_3_stat = rawBeaconRx.Substring(77, 1);

                    var ant_stat = rawBeaconRx.Substring(78, 2);
                    var ul_stat = rawBeaconRx.Substring(80, 2);

                    var oper_mode = rawBeaconRx.Substring(82, 2);
                    var obc_rst_count = rawBeaconRx.Substring(84, 2);
                    var obc_last_reset = rawBeaconRx.Substring(86, 2);

                    var check_sum1 = rawBeaconRx.Substring(88, 2);

                    label_call_sign_ans.Text = source_callsign;
                    label_head_ans.Text = head;
                    label_type_ans.Text = type;
                    label_time_day_ans.Text = tim_day;
                    label_time_hour_ans.Text = tim_hour;

                    label_1_group_box2_ans.Text = batt_vtg;
                    label_2_group_box2_ans.Text = batt_curr;
                    label_3_group_box2_ans.Text = batt_temp;
                    label_4_group_box2_ans.Text = raw_current;

                    label_1_group_box3_ans.Text = sol_tot_vtg;
                    label_2_group_box3_ans.Text = sol_tot_current;
                    label_3_group_box3_ans.Text = ant_pannel_temp;
                    label_4_group_box3_ans.Text = bpb_temp;
                    label_5_group_box3_ans.Text = obc_temp;

                    label_1_group_box4_ans.Text = sol_y_temp;
                    label_2_group_box4_ans.Text = sol_y1_temp;
                    label_3_group_box4_ans.Text = sol_z_temp;
                    label_4_group_box4_ans.Text = sol_z1_temp;
                    label_5_group_box4_ans.Text = sol_x_temp;

                    label_1_group_box5_ans.Text = sol_p1_stat;
                    label_2_group_box5_ans.Text = sol_p2_stat;
                    label_3_group_box5_ans.Text = sol_p3_stat;
                    label_4_group_box5_ans.Text = sol_p4_stat;
                    label_5_group_box5_ans.Text = sol_p5_stat;

                    label_1_group_box7_ans.Text = msn_1_stat;
                    label_2_group_box7_ans.Text = msn_2_stat;
                    label_3_group_box7_ans.Text = msn_3_stat;
                    label_4_group_box7_ans.Text = ant_stat;
                    label_5_group_box7_ans.Text = ul_stat;

                    label_1_group_box6_ans.Text = oper_mode;
                    label_2_group_box6_ans.Text = obc_rst_count;
                    label_3_group_box6_ans.Text = obc_last_reset;
                    label_4_group_box6_ans.Text = obc_last_reset;
                }
                else if (beacon_type.Equals("b2"))
                {
                    getBeaconType2();

                    var head = rawBeaconRx.Substring(34, 2);
                    var type = rawBeaconRx.Substring(36, 2);
                    var tim_day = rawBeaconRx.Substring(38, 2);

                    var sol_p1_vtg = rawBeaconRx.Substring(38, 2);
                    var sol_p2_vtg = rawBeaconRx.Substring(40, 2);
                    var sol_p3_vtg = rawBeaconRx.Substring(42, 2);
                    var sol_p4_vtg = rawBeaconRx.Substring(44, 2);
                    var sol_p5_vtg = rawBeaconRx.Substring(46, 2);

                    var sol_p1_current = rawBeaconRx.Substring(48, 2);
                    var sol_p2_current = rawBeaconRx.Substring(50, 2);
                    var sol_p3_current = rawBeaconRx.Substring(52, 2);
                    var sol_p4_current = rawBeaconRx.Substring(54, 2);
                    var sol_p5_current = rawBeaconRx.Substring(56, 2);

                    var gyro_X = rawBeaconRx.Substring(58, 2);
                    var gyro_Y = rawBeaconRx.Substring(60, 2);
                    var gyro_Z = rawBeaconRx.Substring(62, 2);

                    var acc_X = rawBeaconRx.Substring(64, 2);
                    var acc_Y = rawBeaconRx.Substring(66, 2);
                    var acc_Z = rawBeaconRx.Substring(68, 2);

                    var mag_X = rawBeaconRx.Substring(70, 2);
                    var mag_Y = rawBeaconRx.Substring(72, 2);
                    var mag_Z = rawBeaconRx.Substring(74, 2);

                    //var check_sum2 = rawBeaconRx.Substring(0, Math.Min(20, rawBeaconRx.Length));
                    var check_sum2 = rawBeaconRx.Substring(76,2);

                    label_call_sign_ans.Text = source_callsign;
                    label_head_ans.Text = head;
                    label_type_ans.Text = type;
                    label_time_day_ans.Text = tim_day;

                    label_1_group_box2_ans.Text = sol_p1_vtg;
                    label_2_group_box2_ans.Text = sol_p2_vtg;
                    label_3_group_box2_ans.Text = sol_p3_vtg;
                    label_4_group_box2_ans.Text = sol_p4_vtg;
                    label_5_group_box2_ans.Text = sol_p5_vtg;

                    label_1_group_box3_ans.Text = sol_p1_current;
                    label_2_group_box3_ans.Text = sol_p2_current;
                    label_3_group_box3_ans.Text = sol_p3_current;
                    label_4_group_box3_ans.Text = sol_p4_current;
                    label_5_group_box3_ans.Text = sol_p5_current;

                    label_1_group_box4_ans.Text = gyro_X;
                    label_2_group_box4_ans.Text = gyro_Y;
                    label_3_group_box4_ans.Text = gyro_Z;

                    label_1_group_box5_ans.Text = acc_X;
                    label_2_group_box5_ans.Text = acc_Y;
                    label_3_group_box5_ans.Text = acc_Z;

                    label_1_group_box6_ans.Text = mag_X;
                    label_2_group_box6_ans.Text = mag_Y;
                    label_3_group_box6_ans.Text = mag_Z;
                }

                button_beacon_save.Enabled = true;

            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(
                       $"Beacon Operation Invalid \n {ex.Message}",
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

        private void button_beacon_save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rTBox_beacon_raw_data.Text))
            {
                try
                {
                    string rawBeaconRx = rTBox_beacon_raw_data.Text;
                    rawBeaconRx = rawBeaconRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");


                    string result;

                    result = rawBeaconRx;

                    Console.WriteLine(result);

                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    fbd.Description = "Select saving folder for Beacon";

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        Console.WriteLine(fbd.SelectedPath);
                        savingBeaconRootPath = fbd.SelectedPath;
                        Properties.Settings.Default.savePathBeacon = savingBeaconRootPath;
                        Properties.Settings.Default.Save();

                        BeaconRootPath = Properties.Settings.Default.savePathBeacon;

                        savingBeaconRootPath = System.IO.Path.Combine(BeaconRootPath, dtAppStart.ToString("yyyyMMdd"));

                        if (!Directory.Exists(savingBeaconRootPath))
                        {
                            Directory.CreateDirectory(savingBeaconRootPath);
                        }

                        string rawRxData = result;

                        DateTime dt = DateTime.UtcNow;
                        string time = dt.ToString("yyyyMMdd_HHmmss");
                        string folderPath = savingBeaconRootPath;
                        string fileName;
                        string absolutePath;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        fileName = time + "_Beacon_raw.txt";
                        absolutePath = System.IO.Path.Combine(folderPath, fileName);
                        StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                        sw.Write(rawRxData);
                        sw.Close();

                        rTBox_beacon_raw_data.Text = "";
                        this.Close();

                        DialogResult result_2 = MessageBox.Show(
                             $"Beacon Data Stored",
                             "Saving completed!",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Information,
                             MessageBoxDefaultButton.Button1);
                        Console.WriteLine("Dialog");
                        if (result_2 == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show(
                           $"Beacon Operation Invalid \n {ex.Message}",
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
                         $"Beacon Operation Invalid \n",
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

        private void inputDialogBeacon_Load(object sender, EventArgs e)
        {
            resetFields();
        }

        void resetFields()
        {
            button_beacon_save.Enabled = false;
            label_beacon_type.Text = "XXXX";

            label_call_sign_ans.Text = "XXXX";
            label_head_ans.Text = "XXXX";
            label_type_ans.Text = "XXXX";
            label_time_day_ans.Text = "XXXX";
            label_time_hour_ans.Text = "XXXX";

            label_1_group_box2_ans.Text = "XXXX";
            label_2_group_box2_ans.Text = "XXXX";
            label_3_group_box2_ans.Text = "XXXX";
            label_4_group_box2_ans.Text = "XXXX";
            label_5_group_box2_ans.Text = "XXXX";

            label_1_group_box3_ans.Text = "XXXX";
            label_2_group_box3_ans.Text = "XXXX";
            label_3_group_box3_ans.Text = "XXXX";
            label_4_group_box3_ans.Text = "XXXX";
            label_5_group_box3_ans.Text = "XXXX";

            label_1_group_box4_ans.Text = "XXXX";
            label_2_group_box4_ans.Text = "XXXX";
            label_3_group_box4_ans.Text = "XXXX";
            label_4_group_box4_ans.Text = "XXXX";
            label_5_group_box4_ans.Text = "XXXX";

            label_1_group_box5_ans.Text = "XXXX";
            label_2_group_box5_ans.Text = "XXXX";
            label_3_group_box5_ans.Text = "XXXX";
            label_4_group_box5_ans.Text = "XXXX";
            label_5_group_box5_ans.Text = "XXXX";

            label_1_group_box6_ans.Text = "XXXX";
            label_2_group_box6_ans.Text = "XXXX";
            label_3_group_box6_ans.Text = "XXXX";
            label_4_group_box6_ans.Text = "XXXX";

            label_1_group_box7_ans.Text = "XXXX";
            label_2_group_box7_ans.Text = "XXXX";
            label_3_group_box7_ans.Text = "XXXX";
            label_4_group_box7_ans.Text = "XXXX";
            label_5_group_box7_ans.Text = "XXXX";
        }

        void getBeaconType1()
        {
            label_beacon_type.Text = "BEACON Type 1";

            label_time_hour.Visible = true;
            label_time_hour_ans.Visible = true;

            groupBox9.Text = "Battery Parameter";
            label_1_group_box2.Text = "Battery Voltage";
            label_2_group_box2.Text = "Battery Current";
            label_3_group_box2.Text = "Battery Temperature";
            label_4_group_box2.Text = "Raw Current";
            label_5_group_box2.Visible = false;
            label_5_group_box2_ans.Visible = false;

            groupBox2.Text = "Temperature";
            label_1_group_box3.Text = "Solar Total Voltage";
            label_2_group_box3.Text = "Solar Total Current";
            label_3_group_box3.Text = "Antenna Panel Temp";
            label_4_group_box3.Text = "Back Panel Temp";
            label_5_group_box3.Text = "OBC Temperature";

            groupBox3.Text = "Solar Panel Temperature";
            label_1_group_box4.Text = "Solar Panel +Y Temp";
            label_2_group_box4.Text = "Solar Panel -Y Temp";
            label_3_group_box4.Text = "Solar Panel +Z Temp";
            label_4_group_box4.Visible = true;
            label_4_group_box4_ans.Visible = true;
            label_4_group_box4.Text = "Solar Panel -Z Temp";
            label_5_group_box4.Visible = true;
            label_5_group_box4_ans.Visible = true;
            label_5_group_box4.Text = "Solar Panel -X Temp";

            groupBox5.Text = "Solar Panel Status";
            label_1_group_box5.Text = "Solar Panel +Y Status";
            label_2_group_box5.Text = "Solar Panel -Y Status";
            label_3_group_box5.Text = "Solar Panel +Z Status";
            label_4_group_box5.Visible = true;
            label_4_group_box5_ans.Visible = true;
            label_4_group_box5.Text = "Solar Panel -Z Status";
            label_5_group_box5.Visible = true;
            label_5_group_box5_ans.Visible = true;
            label_5_group_box5.Text = "Solar Panel -X Status";

            groupBox4.Text = "On-Board-Computer";
            label_1_group_box6.Text = "Operation Mode";
            label_2_group_box6.Text = "OBC Reset";
            label_3_group_box6.Text = "Reset Count";
            label_4_group_box6.Text = "Last Reset";

            groupBox6_stats.Visible = true;
        }

        void getBeaconType2()
        {
            label_beacon_type.Text = "BEACON Type 2";

            label_time_hour.Visible = false;
            label_time_hour_ans.Visible = false;

            groupBox9.Text = "Solar Panel Voltage";
            label_1_group_box2.Text = "Solar Panel +Y Voltage";
            label_2_group_box2.Text = "Solar Panel -Y Voltage";
            label_3_group_box2.Text = "Solar Panel +Z Voltage";
            label_4_group_box2.Text = "Solar Panel -Z Voltage";
            label_5_group_box2.Visible = true;
            label_5_group_box2_ans.Visible = true;
            label_5_group_box2.Text = "Solar Panel -X Voltage";

            groupBox2.Text = "Solar Panel Current";
            label_1_group_box3.Text = "Solar Panel +Y Current";
            label_2_group_box3.Text = "Solar Panel -Y Current";
            label_3_group_box3.Text = "Solar Panel +Z Current";
            label_4_group_box3.Text = "Solar Panel -Z Current";
            label_5_group_box3.Text = "Solar Panel -X Current";

            groupBox3.Text = "Gyro";
            label_1_group_box4.Text = "GYRO -X";
            label_2_group_box4.Text = "GYRO -Y";
            label_3_group_box4.Text = "GYRO -Z";
            label_4_group_box4.Visible = false;
            label_4_group_box4_ans.Visible = false;
            label_5_group_box4.Visible = false;
            label_5_group_box4_ans.Visible = false;

            groupBox5.Text = "ACCEL";
            label_1_group_box5.Text = "ACCEL -X";
            label_2_group_box5.Text = "ACCEL -Y";
            label_3_group_box5.Text = "ACCEL -Z";
            label_4_group_box5.Visible = false;
            label_4_group_box5_ans.Visible = false;
            label_5_group_box5.Visible = false;
            label_5_group_box5_ans.Visible = false;

            groupBox4.Text = "MAG";
            label_1_group_box6.Text = "MAG -X";
            label_2_group_box6.Text = "MAG -Y";
            label_3_group_box6.Text = "MAG -Z";
            label_4_group_box6.Visible = false;
            label_4_group_box6_ans.Visible = false;

            groupBox6_stats.Visible = false;
        }
    }
}
