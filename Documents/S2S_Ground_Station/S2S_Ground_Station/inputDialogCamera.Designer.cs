namespace S2S_Ground_Station
{
    partial class inputDialogCamera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inputDialogCamera));
            rTBox_cam_raw_data = new RichTextBox();
            button_cam_paraser_ok = new Button();
            button_cancel_cam = new Button();
            button_save_pic = new Button();
            groupBox1 = new GroupBox();
            pictureBox_img = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_img).BeginInit();
            SuspendLayout();
            // 
            // rTBox_cam_raw_data
            // 
            rTBox_cam_raw_data.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rTBox_cam_raw_data.Cursor = Cursors.IBeam;
            rTBox_cam_raw_data.Font = new Font("MS Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rTBox_cam_raw_data.Location = new Point(12, 12);
            rTBox_cam_raw_data.Name = "rTBox_cam_raw_data";
            rTBox_cam_raw_data.Size = new Size(600, 200);
            rTBox_cam_raw_data.TabIndex = 0;
            rTBox_cam_raw_data.Text = "";
            // 
            // button_cam_paraser_ok
            // 
            button_cam_paraser_ok.Anchor = AnchorStyles.Right;
            button_cam_paraser_ok.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_cam_paraser_ok.Location = new Point(625, 50);
            button_cam_paraser_ok.Name = "button_cam_paraser_ok";
            button_cam_paraser_ok.Size = new Size(94, 59);
            button_cam_paraser_ok.TabIndex = 1;
            button_cam_paraser_ok.Text = "Get Image";
            button_cam_paraser_ok.UseVisualStyleBackColor = true;
            button_cam_paraser_ok.Click += button_cam_paraser_ok_Click;
            // 
            // button_cancel_cam
            // 
            button_cancel_cam.Anchor = AnchorStyles.Right;
            button_cancel_cam.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_cancel_cam.Location = new Point(625, 125);
            button_cancel_cam.Name = "button_cancel_cam";
            button_cancel_cam.Size = new Size(94, 59);
            button_cancel_cam.TabIndex = 2;
            button_cancel_cam.Text = "Cancel";
            button_cancel_cam.UseVisualStyleBackColor = true;
            button_cancel_cam.Click += button_cancel_cam_Click;
            // 
            // button_save_pic
            // 
            button_save_pic.Anchor = AnchorStyles.Right;
            button_save_pic.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_save_pic.Location = new Point(625, 372);
            button_save_pic.Name = "button_save_pic";
            button_save_pic.Size = new Size(94, 59);
            button_save_pic.TabIndex = 4;
            button_save_pic.Text = "Save";
            button_save_pic.UseVisualStyleBackColor = true;
            button_save_pic.Click += button_save_pic_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox_img);
            groupBox1.Location = new Point(12, 218);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(607, 377);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Image Parser";
            // 
            // pictureBox_img
            // 
            pictureBox_img.ErrorImage = Properties.Resources.no_img_found;
            pictureBox_img.InitialImage = Properties.Resources.s2s_logo;
            pictureBox_img.Location = new Point(6, 26);
            pictureBox_img.Name = "pictureBox_img";
            pictureBox_img.Size = new Size(594, 343);
            pictureBox_img.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_img.TabIndex = 6;
            pictureBox_img.TabStop = false;
            // 
            // inputDialogCamera
            // 
            AllowDrop = true;
            ClientSize = new Size(735, 599);
            ControlBox = false;
            Controls.Add(groupBox1);
            Controls.Add(button_save_pic);
            Controls.Add(button_cancel_cam);
            Controls.Add(button_cam_paraser_ok);
            Controls.Add(rTBox_cam_raw_data);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "inputDialogCamera";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Input Camera Raw Data";
            Load += inputDialogCamera_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_img).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rTBox_cam_raw_data;
        private Button button_cam_paraser_ok;
        private Button button_cancel_cam;
        private Button button_save_pic;
        private GroupBox groupBox1;
        private PictureBox pictureBox_img;
    }
}