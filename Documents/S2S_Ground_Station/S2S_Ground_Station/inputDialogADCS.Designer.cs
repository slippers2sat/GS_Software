namespace S2S_Ground_Station
{
    partial class inputDialogADCS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inputDialogADCS));
            rTBox_adsc_raw_data = new RichTextBox();
            button_adcs_paraser_ok = new Button();
            button_cancel_adcs = new Button();
            SuspendLayout();
            // 
            // rTBox_adsc_raw_data
            // 
            rTBox_adsc_raw_data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rTBox_adsc_raw_data.Cursor = Cursors.IBeam;
            rTBox_adsc_raw_data.Font = new Font("MS Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rTBox_adsc_raw_data.Location = new Point(12, 12);
            rTBox_adsc_raw_data.Name = "rTBox_adsc_raw_data";
            rTBox_adsc_raw_data.Size = new Size(600, 200);
            rTBox_adsc_raw_data.TabIndex = 0;
            rTBox_adsc_raw_data.Text = "";
            // 
            // button_adcs_paraser_ok
            // 
            button_adcs_paraser_ok.Anchor = AnchorStyles.Right;
            button_adcs_paraser_ok.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_adcs_paraser_ok.Location = new Point(625, 50);
            button_adcs_paraser_ok.Name = "button_adcs_paraser_ok";
            button_adcs_paraser_ok.Size = new Size(94, 59);
            button_adcs_paraser_ok.TabIndex = 1;
            button_adcs_paraser_ok.Text = "Get ADCS";
            button_adcs_paraser_ok.UseVisualStyleBackColor = true;
            button_adcs_paraser_ok.Click += button_adcs_paraser_ok_Click;
            // 
            // button_cancel_adcs
            // 
            button_cancel_adcs.Anchor = AnchorStyles.Right;
            button_cancel_adcs.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_cancel_adcs.Location = new Point(625, 125);
            button_cancel_adcs.Name = "button_cancel_adcs";
            button_cancel_adcs.Size = new Size(94, 59);
            button_cancel_adcs.TabIndex = 2;
            button_cancel_adcs.Text = "Cancel";
            button_cancel_adcs.UseVisualStyleBackColor = true;
            button_cancel_adcs.Click += button_cancel_adcs_Click;
            // 
            // inputDialogADCS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 221);
            ControlBox = false;
            Controls.Add(button_cancel_adcs);
            Controls.Add(button_adcs_paraser_ok);
            Controls.Add(rTBox_adsc_raw_data);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "inputDialogADCS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Input ADCS Raw Data";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rTBox_adsc_raw_data;
        private Button button_adcs_paraser_ok;
        private Button button_cancel_adcs;
    }
}