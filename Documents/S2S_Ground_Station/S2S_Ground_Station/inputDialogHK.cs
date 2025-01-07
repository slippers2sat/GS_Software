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
    public partial class inputDialogHK : Form
    {
        public string savingHKRootPath;
        public string HKRootPath;

        public DateTime dtAppStart = DateTime.UtcNow;
        public inputDialogHK()
        {
            InitializeComponent();
        }

        private void button_hk_cancel_Click(object sender, EventArgs e)
        {
            rTBox_hk_raw_data.Text = "";
            this.Close();
        }

        private void button_hk_parser_ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rTBox_hk_raw_data.Text))
            {
                try
                {
                    string rawHKRx = rTBox_hk_raw_data.Text;
                    rawHKRx = rawHKRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

                    string sequenceToExclude = "729c64a66440e0729c64a664406103f00d";

                    // Define the pattern: sequenceToExclude followed by exactly 1 byte (2 hex characters)
                    string pattern = sequenceToExclude + "[0-9a-fA-F]{2}";

                    string result;

                    if (Regex.IsMatch(rawHKRx, pattern))
                    {
                        // Exclude the sequence from the original string
                        rawHKRx = Regex.Replace(rawHKRx, pattern, "");
                        result = rawHKRx;
                    }
                    else
                    {
                        result = rawHKRx;
                    }

                    Console.WriteLine(result);

                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    fbd.Description = "Select saving folder for Housekeeping Data";

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        Console.WriteLine(fbd.SelectedPath);
                        savingHKRootPath = fbd.SelectedPath;
                        Properties.Settings.Default.savePathHK = savingHKRootPath;
                        Properties.Settings.Default.Save();

                        HKRootPath = Properties.Settings.Default.savePathHK;

                        savingHKRootPath = System.IO.Path.Combine(HKRootPath, dtAppStart.ToString("yyyyMMdd"));

                        if (!Directory.Exists(savingHKRootPath))
                        {
                            Directory.CreateDirectory(savingHKRootPath);
                        }

                        string rawRxData = result;

                        DateTime dt = DateTime.UtcNow;
                        string time = dt.ToString("yyyyMMdd_HHmmss");
                        string folderPath = savingHKRootPath;
                        string fileName;
                        string absolutePath;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        fileName = time + "_HK_raw.txt";
                        absolutePath = System.IO.Path.Combine(folderPath, fileName);
                        StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                        sw.Write(rawRxData);
                        sw.Close();

                        rTBox_hk_raw_data.Text = "";
                        this.Close();

                        DialogResult result_2 = MessageBox.Show(
                             $"HK Data Stored",
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
                           $"HK Operation Invalid \n {ex.Message}",
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
                         $"HK Operation Invalid \n",
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
    }
}
