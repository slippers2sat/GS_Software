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
    public partial class inputDialogADCS : Form
    {

        public string savingADCSRootPath;
        public string ADCSRootPath;

        public DateTime dtAppStart = DateTime.UtcNow;
        public inputDialogADCS()
        {
            InitializeComponent();
        }

        private void button_cancel_adcs_Click(object sender, EventArgs e)
        {
            rTBox_adsc_raw_data.Text = "";
            this.Close();
        }

        private void button_adcs_paraser_ok_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(rTBox_adsc_raw_data.Text))
            {
                try
                {
                    string rawADCSRx = rTBox_adsc_raw_data.Text;
                    rawADCSRx = rawADCSRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

                    string sequenceToExclude = "729c64a66440e0729c64a664406103f00d";

                    // Define the pattern: sequenceToExclude followed by exactly 1 byte (2 hex characters)
                    string pattern = sequenceToExclude + "[0-9a-fA-F]{2}";

                    string result;

                    if (Regex.IsMatch(rawADCSRx, pattern))
                    {
                        // Exclude the sequence from the original string
                        rawADCSRx = Regex.Replace(rawADCSRx, pattern, "");
                        result = rawADCSRx;
                    }
                    else
                    {
                        result = rawADCSRx;
                    }

                    Console.WriteLine(result);

                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    fbd.Description = "Select saving folder for ADCS";

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        Console.WriteLine(fbd.SelectedPath);
                        savingADCSRootPath = fbd.SelectedPath;
                        Properties.Settings.Default.savePathADCS = savingADCSRootPath;
                        Properties.Settings.Default.Save();

                        ADCSRootPath = Properties.Settings.Default.savePathADCS;

                        savingADCSRootPath = System.IO.Path.Combine(ADCSRootPath, dtAppStart.ToString("yyyyMMdd"));

                        if (!Directory.Exists(savingADCSRootPath))
                        {
                            Directory.CreateDirectory(savingADCSRootPath);
                        }

                        string rawRxData = result;

                        DateTime dt = DateTime.UtcNow;
                        string time = dt.ToString("yyyyMMdd_HHmmss");
                        string folderPath = savingADCSRootPath;
                        string fileName;
                        string absolutePath;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        fileName = time + "_ADCS_raw.txt";
                        absolutePath = System.IO.Path.Combine(folderPath, fileName);
                        StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                        sw.Write(rawRxData);
                        sw.Close();

                        rTBox_adsc_raw_data.Text = "";
                        this.Close();

                        DialogResult result_2 = MessageBox.Show(
                             $"ADCS Data Stored",
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
                           $"ADCS Operation Invalid \n {ex.Message}",
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
                         $"ADCS Operation Invalid \n",
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
