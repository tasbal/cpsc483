namespace ReLive
{
    partial class reLiveMain
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fileList = new System.Windows.Forms.ComboBox();
            this.directoryBrowse = new System.Windows.Forms.Button();
            this.launchSite = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 290);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // fileList
            // 
            this.fileList.FormattingEnabled = true;
            this.fileList.Location = new System.Drawing.Point(326, 52);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(272, 21);
            this.fileList.TabIndex = 1;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(326, 14);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(269, 32);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse for Image Directory";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            // 
            // launchSite
            // 
            this.launchSite.Location = new System.Drawing.Point(326, 80);
            this.launchSite.Name = "launchSite";
            this.launchSite.Size = new System.Drawing.Size(269, 32);
            this.launchSite.TabIndex = 2;
            this.launchSite.Text = "Launch Picasaweb";
            this.launchSite.UseVisualStyleBackColor = true;
            this.launchSite.Click += new System.EventHandler(this.launchSite_Click);
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 332);
            this.Controls.Add(this.directoryBrowse);
            this.Controls.Add(this.launchSite);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.pictureBox1);
            this.Name = "reLiveMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox fileList;
        private System.Windows.Forms.Button directoryBrowse;
        private System.Windows.Forms.Button launchSite;
    }
}

