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

namespace S2S_Ground_Station
{
    public partial class inputDialogCamera : Form
    {
        public inputDialogCamera()
        {
            InitializeComponent();
        }


        public string savingCAMRootPath;
        public string CAMRootPAth;

        public DateTime dtAppStart = DateTime.UtcNow;

        Image image;

        byte[] imageBytes;

        private void button_cam_paraser_ok_Click(object sender, EventArgs e)
        {

            try
            {
                string rawCameraRx = rTBox_cam_raw_data.Text;
                rawCameraRx = rawCameraRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

                string sequenceToExclude = "729c64a66440e0729c64a664406103f00c";

                // Define the pattern: sequenceToExclude followed by exactly 1 byte (2 hex characters)
                string pattern = sequenceToExclude + "[0-9a-fA-F]{2}";

                string result;

                if (Regex.IsMatch(rawCameraRx, pattern))
                {
                    // Exclude the sequence from the original string
                    rawCameraRx = Regex.Replace(rawCameraRx, pattern, "");
                    result = rawCameraRx;
                }
                else
                {
                    result = rawCameraRx;
                }

                Console.WriteLine(result);

                try
                {
                    imageBytes = HexStringToByteArray(result);

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        image = Image.FromStream(ms);

                        pictureBox_img.Image = image;

                        button_save_pic.Enabled = true;

                    }

                }
                catch (Exception ex)
                {
                    DialogResult result_1 = MessageBox.Show(
                       $"Parameter is invalid \n {ex.Message}",
                       "Please input valid raw data",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    Console.WriteLine("Dialog");
                    if (result_1 == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show(
                       $"Camera Operation Invalid \n {ex.Message}",
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
                rTBox_cam_raw_data.Text = "";
            }

        }

        private void button_cancel_cam_Click(object sender, EventArgs e)
        {
            rTBox_cam_raw_data.Text = "";
            this.Close();
        }

        private void inputDialogCamera_Load(object sender, EventArgs e)
        {
            button_save_pic.Enabled = false;
        }

        private byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private void button_save_pic_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "Select saving folder for CAM";

            try
            {

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    Console.WriteLine(fbd.SelectedPath);
                    savingCAMRootPath = fbd.SelectedPath;
                    Properties.Settings.Default.savePathADCS = savingCAMRootPath;
                    Properties.Settings.Default.Save();

                    CAMRootPAth = Properties.Settings.Default.savePathADCS;

                    savingCAMRootPath = System.IO.Path.Combine(CAMRootPAth, dtAppStart.ToString("yyyyMMdd"));

                    if (!Directory.Exists(savingCAMRootPath))
                    {
                        Directory.CreateDirectory(savingCAMRootPath);
                    }

                    DateTime dt = DateTime.UtcNow;
                    string time = dt.ToString("yyyyMMdd_HHmmss");
                    string folderPath = savingCAMRootPath;
                    string fileName;
                    string absolutePath;

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // image store

                    // Write the image byte array to a PNG     fileName = time + "_ADCS_raw.txt";

                    fileName = time + "_cam_image.png";
                    absolutePath = System.IO.Path.Combine(folderPath, fileName);
                    File.WriteAllBytes(absolutePath, imageBytes);
                    this.Close();

                    DialogResult result_2 = MessageBox.Show(
                                $"Image Stored",
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
                           $"Camera Operation Invalid \n {ex.Message}",
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
    }
}
