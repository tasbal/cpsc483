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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reLiveMain));
            this.directoryBrowse = new System.Windows.Forms.Button();
            this.uploadDir = new System.Windows.Forms.Button();
            this.launchSite = new System.Windows.Forms.Button();
            this.albumCalendar = new System.Windows.Forms.MonthCalendar();
            this.AlbumPicture = new System.Windows.Forms.PictureBox();
            this.calendarLabel = new System.Windows.Forms.Label();
            this.albumPreviewLabel = new System.Windows.Forms.Label();
            this.mapLinkLabel = new System.Windows.Forms.LinkLabel();
            this.fileBrowser = new System.Windows.Forms.WebBrowser();
            this.explorerText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(434, 16);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(147, 30);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse for Image Directory";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            // 
            // uploadDir
            // 
            this.uploadDir.Location = new System.Drawing.Point(598, 55);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(160, 30);
            this.uploadDir.TabIndex = 2;
            this.uploadDir.Text = "Upload Image Directory";
            this.uploadDir.UseVisualStyleBackColor = true;
            this.uploadDir.Click += new System.EventHandler(this.uploadDir_Click);
            // 
            // launchSite
            // 
            this.launchSite.Location = new System.Drawing.Point(598, 91);
            this.launchSite.Name = "launchSite";
            this.launchSite.Size = new System.Drawing.Size(160, 30);
            this.launchSite.TabIndex = 2;
            this.launchSite.Text = "Launch Picasaweb";
            this.launchSite.UseVisualStyleBackColor = true;
            this.launchSite.Click += new System.EventHandler(this.launchSite_Click);
            // 
            // albumCalendar
            // 
            this.albumCalendar.Location = new System.Drawing.Point(593, 146);
            this.albumCalendar.MaxSelectionCount = 1;
            this.albumCalendar.Name = "albumCalendar";
            this.albumCalendar.TabIndex = 4;
            this.albumCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.albumCalendar_DateChanged);
            // 
            // AlbumPicture
            // 
            this.AlbumPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AlbumPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AlbumPicture.Location = new System.Drawing.Point(598, 326);
            this.AlbumPicture.Name = "AlbumPicture";
            this.AlbumPicture.Size = new System.Drawing.Size(160, 160);
            this.AlbumPicture.TabIndex = 5;
            this.AlbumPicture.TabStop = false;
            // 
            // calendarLabel
            // 
            this.calendarLabel.AutoSize = true;
            this.calendarLabel.Location = new System.Drawing.Point(608, 124);
            this.calendarLabel.Name = "calendarLabel";
            this.calendarLabel.Size = new System.Drawing.Size(145, 13);
            this.calendarLabel.TabIndex = 6;
            this.calendarLabel.Text = "Browse Web Albums by Date";
            // 
            // albumPreviewLabel
            // 
            this.albumPreviewLabel.AutoSize = true;
            this.albumPreviewLabel.Location = new System.Drawing.Point(652, 310);
            this.albumPreviewLabel.Name = "albumPreviewLabel";
            this.albumPreviewLabel.Size = new System.Drawing.Size(67, 13);
            this.albumPreviewLabel.TabIndex = 7;
            this.albumPreviewLabel.Text = "Album Cover";
            // 
            // mapLinkLabel
            // 
            this.mapLinkLabel.AutoSize = true;
            this.mapLinkLabel.Location = new System.Drawing.Point(656, 489);
            this.mapLinkLabel.Name = "mapLinkLabel";
            this.mapLinkLabel.Size = new System.Drawing.Size(54, 13);
            this.mapLinkLabel.TabIndex = 8;
            this.mapLinkLabel.TabStop = true;
            this.mapLinkLabel.Text = "View Map";
            this.mapLinkLabel.Visible = false;
            this.mapLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mapLinkLabel_LinkClicked);
            // 
            // fileBrowser
            // 
            this.fileBrowser.Location = new System.Drawing.Point(13, 52);
            this.fileBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.fileBrowser.Name = "fileBrowser";
            this.fileBrowser.Size = new System.Drawing.Size(568, 450);
            this.fileBrowser.TabIndex = 9;
            this.fileBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.fileBrowser_DocumentCompleted);
            // 
            // explorerText
            // 
            this.explorerText.Location = new System.Drawing.Point(13, 26);
            this.explorerText.Name = "explorerText";
            this.explorerText.Size = new System.Drawing.Size(415, 20);
            this.explorerText.TabIndex = 10;
            this.explorerText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.explorerText_Enter);
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 514);
            this.Controls.Add(this.explorerText);
            this.Controls.Add(this.fileBrowser);
            this.Controls.Add(this.mapLinkLabel);
            this.Controls.Add(this.albumPreviewLabel);
            this.Controls.Add(this.calendarLabel);
            this.Controls.Add(this.AlbumPicture);
            this.Controls.Add(this.albumCalendar);
            this.Controls.Add(this.directoryBrowse);
            this.Controls.Add(this.uploadDir);
            this.Controls.Add(this.launchSite);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "reLiveMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "explore. reLive. share.";
            this.Load += new System.EventHandler(this.reLiveMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button directoryBrowse;
        private System.Windows.Forms.Button uploadDir;
        private System.Windows.Forms.Button launchSite;
        private System.Windows.Forms.MonthCalendar albumCalendar;
        private System.Windows.Forms.PictureBox AlbumPicture;
        private System.Windows.Forms.Label calendarLabel;
        private System.Windows.Forms.Label albumPreviewLabel;
        private System.Windows.Forms.LinkLabel mapLinkLabel;
        private System.Windows.Forms.WebBrowser fileBrowser;
        private System.Windows.Forms.TextBox explorerText;
    }
}

