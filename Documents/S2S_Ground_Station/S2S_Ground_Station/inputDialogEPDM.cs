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

    public partial class inputDialogEPDM : Form
    {

        public string savingEPDMRootPath;
        public string EPDMRootPath;

        public DateTime dtAppStart = DateTime.UtcNow;

        public inputDialogEPDM()
        {
            InitializeComponent();
        }

        private void button_cancel_epdm_Click(object sender, EventArgs e)
        {
            rTBox_epdm_raw_data.Text = "";
            this.Close();
        }

        private void button_epdm_parser_ok_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(rTBox_epdm_raw_data.Text))
            {
                try
                {
                    string rawEPDMRx = rTBox_epdm_raw_data.Text;
                    rawEPDMRx = rawEPDMRx.Replace(" ", "").Replace("\r", "").Replace("\t", "").Replace("\n", "");

                    string sequenceToExclude = "729c64a66440e0729c64a664406103f00b";

                    // Define the pattern: sequenceToExclude followed by exactly 1 byte (2 hex characters)
                    string pattern = sequenceToExclude + "[0-9a-fA-F]{2}";   //skip upto packettype 0b and packet_number

                    string result;

                    if (Regex.IsMatch(rawEPDMRx, pattern))
                    {
                        // Exclude the sequence from the original string
                        rawEPDMRx = Regex.Replace(rawEPDMRx, pattern, "");
                        result = rawEPDMRx;
                    }
                    else
                    {
                        result = rawEPDMRx;
                    }

                    Console.WriteLine(result);

                    FolderBrowserDialog fbd = new FolderBrowserDialog();

                    fbd.Description = "Select saving folder for EPDM";

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        Console.WriteLine(fbd.SelectedPath);
                        savingEPDMRootPath = fbd.SelectedPath;
                        Properties.Settings.Default.savePathEPDM = savingEPDMRootPath;
                        Properties.Settings.Default.Save();


                        EPDMRootPath = Properties.Settings.Default.savePathEPDM;

                        savingEPDMRootPath = System.IO.Path.Combine(EPDMRootPath, dtAppStart.ToString("yyyyMMdd"));

                        if (!Directory.Exists(savingEPDMRootPath))
                        {
                            Directory.CreateDirectory(savingEPDMRootPath);
                        }

                        string rawRxData = result;

                        DateTime dt = DateTime.UtcNow;
                        string time = dt.ToString("yyyyMMdd_HHmmss");
                        string folderPath = savingEPDMRootPath;
                        string fileName;
                        string absolutePath;

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        fileName = time + "_EPDM_raw.txt";
                        absolutePath = System.IO.Path.Combine(folderPath, fileName);
                        StreamWriter sw = new StreamWriter(absolutePath, false, Encoding.ASCII);
                        sw.Write(rawRxData);
                        sw.Close();

                        rTBox_epdm_raw_data.Text = "";
                        this.Close();

                        DialogResult result_2 = MessageBox.Show(
                              $"EPDM Data Stored",
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
                          $"EPDM Operation Invalid \n {ex.Message}",
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
                         $"EPDM Operation Invalid \n",
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
