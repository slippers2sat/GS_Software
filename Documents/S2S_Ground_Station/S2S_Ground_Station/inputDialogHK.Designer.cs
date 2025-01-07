namespace S2S_Ground_Station
{
    partial class inputDialogHK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inputDialogHK));
            rTBox_hk_raw_data = new RichTextBox();
            button_hk_parser_ok = new Button();
            button_hk_cancel = new Button();
            SuspendLayout();
            // 
            // rTBox_hk_raw_data
            // 
            rTBox_hk_raw_data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rTBox_hk_raw_data.Cursor = Cursors.IBeam;
            rTBox_hk_raw_data.Font = new Font("MS Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rTBox_hk_raw_data.Location = new Point(12, 12);
            rTBox_hk_raw_data.Name = "rTBox_hk_raw_data";
            rTBox_hk_raw_data.Size = new Size(600, 200);
            rTBox_hk_raw_data.TabIndex = 0;
            rTBox_hk_raw_data.Text = "";
            // 
            // button_hk_parser_ok
            // 
            button_hk_parser_ok.Anchor = AnchorStyles.Right;
            button_hk_parser_ok.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_hk_parser_ok.Location = new Point(625, 50);
            button_hk_parser_ok.Name = "button_hk_parser_ok";
            button_hk_parser_ok.Size = new Size(94, 59);
            button_hk_parser_ok.TabIndex = 1;
            button_hk_parser_ok.Text = "Get HK";
            button_hk_parser_ok.UseVisualStyleBackColor = true;
            button_hk_parser_ok.Click += button_hk_parser_ok_Click;
            // 
            // button_hk_cancel
            // 
            button_hk_cancel.Anchor = AnchorStyles.Right;
            button_hk_cancel.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_hk_cancel.Location = new Point(625, 125);
            button_hk_cancel.Name = "button_hk_cancel";
            button_hk_cancel.Size = new Size(94, 59);
            button_hk_cancel.TabIndex = 2;
            button_hk_cancel.Text = "Cancel";
            button_hk_cancel.UseVisualStyleBackColor = true;
            button_hk_cancel.Click += button_hk_cancel_Click;
            // 
            // inputDialogHK
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 221);
            ControlBox = false;
            Controls.Add(button_hk_cancel);
            Controls.Add(button_hk_parser_ok);
            Controls.Add(rTBox_hk_raw_data);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "inputDialogHK";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Input House Keeping Raw Data";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rTBox_hk_raw_data;
        private Button button_hk_parser_ok;
        private Button button_hk_cancel;
    }
}