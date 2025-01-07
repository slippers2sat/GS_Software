namespace S2S_Ground_Station
{
    partial class inputDialogEPDM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inputDialogEPDM));
            rTBox_epdm_raw_data = new RichTextBox();
            button_epdm_parser_ok = new Button();
            button_cancel_epdm = new Button();
            SuspendLayout();
            // 
            // rTBox_epdm_raw_data
            // 
            rTBox_epdm_raw_data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rTBox_epdm_raw_data.Cursor = Cursors.IBeam;
            rTBox_epdm_raw_data.Font = new Font("MS Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rTBox_epdm_raw_data.Location = new Point(12, 12);
            rTBox_epdm_raw_data.Name = "rTBox_epdm_raw_data";
            rTBox_epdm_raw_data.Size = new Size(600, 200);
            rTBox_epdm_raw_data.TabIndex = 0;
            rTBox_epdm_raw_data.Text = "";
            // 
            // button_epdm_parser_ok
            // 
            button_epdm_parser_ok.Anchor = AnchorStyles.Right;
            button_epdm_parser_ok.Font = new Font("MS UI Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_epdm_parser_ok.Location = new Point(625, 50);
            button_epdm_parser_ok.Name = "button_epdm_parser_ok";
            button_epdm_parser_ok.Size = new Size(94, 59);
            button_epdm_parser_ok.TabIndex = 1;
            button_epdm_parser_ok.Text = "Get EPDM";
            button_epdm_parser_ok.UseVisualStyleBackColor = true;
            button_epdm_parser_ok.Click += button_epdm_parser_ok_Click;
            // 
            // button_cancel_epdm
            // 
            button_cancel_epdm.Anchor = AnchorStyles.Right;
            button_cancel_epdm.Font = new Font("MS UI Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_cancel_epdm.Location = new Point(625, 125);
            button_cancel_epdm.Name = "button_cancel_epdm";
            button_cancel_epdm.Size = new Size(94, 59);
            button_cancel_epdm.TabIndex = 2;
            button_cancel_epdm.Text = "Cancel";
            button_cancel_epdm.UseVisualStyleBackColor = true;
            button_cancel_epdm.Click += button_cancel_epdm_Click;
            // 
            // inputDialogEPDM
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 221);
            ControlBox = false;
            Controls.Add(button_cancel_epdm);
            Controls.Add(button_epdm_parser_ok);
            Controls.Add(rTBox_epdm_raw_data);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "inputDialogEPDM";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Input EPDM Raw Data";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rTBox_epdm_raw_data;
        private Button button_epdm_parser_ok;
        private Button button_cancel_epdm;
    }
}