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
            this.uploadDir = new System.Windows.Forms.Button();
            this.launchSite = new System.Windows.Forms.Button();
            this.albumCalendar = new System.Windows.Forms.MonthCalendar();
            this.AlbumPicture = new System.Windows.Forms.PictureBox();
            this.calendarLabel = new System.Windows.Forms.Label();
            this.albumPreviewLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).BeginInit();
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
            this.fileList.Size = new System.Drawing.Size(234, 21);
            this.fileList.TabIndex = 1;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(326, 14);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(231, 32);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse for Image Directory";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            //
            // uploadDir
            //
            
            this.uploadDir.Location = new System.Drawing.Point(580, 14);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(130, 32);
            this.uploadDir.TabIndex = 2;
            this.uploadDir.Text = "Upload Image Directory";
            this.uploadDir.UseVisualStyleBackColor = true;
            this.uploadDir.Click += new System.EventHandler(this.uploadDir_Click);
  
            // 
            // launchSite
            // 
            this.launchSite.Location = new System.Drawing.Point(326, 80);
            this.launchSite.Name = "launchSite";
            this.launchSite.Size = new System.Drawing.Size(231, 32);
            this.launchSite.TabIndex = 2;
            this.launchSite.Text = "Launch Picasaweb";
            this.launchSite.UseVisualStyleBackColor = true;
            this.launchSite.Click += new System.EventHandler(this.launchSite_Click);
            // 
            // albumCalendar
            // 
            this.albumCalendar.Location = new System.Drawing.Point(330, 140);
            this.albumCalendar.MaxSelectionCount = 1;
            this.albumCalendar.Name = "albumCalendar";
            this.albumCalendar.TabIndex = 4;
            this.albumCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.albumCalendar_DateChanged);
            // 
            // AlbumPicture
            // 
            this.AlbumPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AlbumPicture.Location = new System.Drawing.Point(569, 140);
            this.AlbumPicture.Name = "AlbumPicture";
            this.AlbumPicture.Size = new System.Drawing.Size(160, 160);
            this.AlbumPicture.TabIndex = 5;
            this.AlbumPicture.TabStop = false;
            // 
            // calendarLabel
            // 
            this.calendarLabel.AutoSize = true;
            this.calendarLabel.Location = new System.Drawing.Point(373, 118);
            this.calendarLabel.Name = "calendarLabel";
            this.calendarLabel.Size = new System.Drawing.Size(145, 13);
            this.calendarLabel.TabIndex = 6;
            this.calendarLabel.Text = "Browse Web Albums by Date";
            // 
            // albumPreviewLabel
            // 
            this.albumPreviewLabel.AutoSize = true;
            this.albumPreviewLabel.Location = new System.Drawing.Point(616, 118);
            this.albumPreviewLabel.Name = "albumPreviewLabel";
            this.albumPreviewLabel.Size = new System.Drawing.Size(67, 13);
            this.albumPreviewLabel.TabIndex = 7;
            this.albumPreviewLabel.Text = "Album Cover";
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 322);
            this.Controls.Add(this.albumPreviewLabel);
            this.Controls.Add(this.calendarLabel);
            this.Controls.Add(this.AlbumPicture);
            this.Controls.Add(this.albumCalendar);
            this.Controls.Add(this.directoryBrowse);
            this.Controls.Add(this.uploadDir);
            this.Controls.Add(this.launchSite);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.pictureBox1);
            this.Name = "reLiveMain";
            this.Text = "explore. reLive. share.";
            this.Load += new System.EventHandler(this.reLiveMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox fileList;
        private System.Windows.Forms.Button directoryBrowse;
        private System.Windows.Forms.Button uploadDir;
        private System.Windows.Forms.Button launchSite;
        private System.Windows.Forms.MonthCalendar albumCalendar;
        private System.Windows.Forms.PictureBox AlbumPicture;
        private System.Windows.Forms.Label calendarLabel;
        private System.Windows.Forms.Label albumPreviewLabel;
    }
}

