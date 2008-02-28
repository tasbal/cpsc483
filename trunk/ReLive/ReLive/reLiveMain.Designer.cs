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
            this.albumCalendar = new System.Windows.Forms.MonthCalendar();
            this.AlbumPicture = new System.Windows.Forms.PictureBox();
            this.calendarLabel = new System.Windows.Forms.Label();
            this.albumPreviewLabel = new System.Windows.Forms.Label();
            this.mapLinkLabel = new System.Windows.Forms.LinkLabel();
            this.fileBrowser = new System.Windows.Forms.WebBrowser();
            this.explorerText = new System.Windows.Forms.TextBox();
            this.loginLink = new System.Windows.Forms.LinkLabel();
            this.picasaLink = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabViewer = new System.Windows.Forms.TabControl();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.settingsTab = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabViewer.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(289, 3);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(147, 30);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse for Image Directory";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            // 
            // uploadDir
            // 
            this.uploadDir.Location = new System.Drawing.Point(442, 3);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(132, 30);
            this.uploadDir.TabIndex = 2;
            this.uploadDir.Text = "Upload Image Directory";
            this.uploadDir.UseVisualStyleBackColor = true;
            this.uploadDir.Click += new System.EventHandler(this.uploadDir_Click);
            // 
            // albumCalendar
            // 
            this.albumCalendar.Location = new System.Drawing.Point(7, 48);
            this.albumCalendar.MaxSelectionCount = 1;
            this.albumCalendar.Name = "albumCalendar";
            this.albumCalendar.TabIndex = 4;
            this.albumCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.albumCalendar_DateChanged);
            // 
            // AlbumPicture
            // 
            this.AlbumPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AlbumPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AlbumPicture.Location = new System.Drawing.Point(40, 229);
            this.AlbumPicture.Name = "AlbumPicture";
            this.AlbumPicture.Size = new System.Drawing.Size(160, 160);
            this.AlbumPicture.TabIndex = 5;
            this.AlbumPicture.TabStop = false;
            // 
            // calendarLabel
            // 
            this.calendarLabel.AutoSize = true;
            this.calendarLabel.Location = new System.Drawing.Point(48, 26);
            this.calendarLabel.Name = "calendarLabel";
            this.calendarLabel.Size = new System.Drawing.Size(145, 13);
            this.calendarLabel.TabIndex = 6;
            this.calendarLabel.Text = "Browse Web Albums by Date";
            // 
            // albumPreviewLabel
            // 
            this.albumPreviewLabel.AutoSize = true;
            this.albumPreviewLabel.Location = new System.Drawing.Point(82, 213);
            this.albumPreviewLabel.Name = "albumPreviewLabel";
            this.albumPreviewLabel.Size = new System.Drawing.Size(77, 13);
            this.albumPreviewLabel.TabIndex = 7;
            this.albumPreviewLabel.Text = "Album Preview";
            // 
            // mapLinkLabel
            // 
            this.mapLinkLabel.AutoSize = true;
            this.mapLinkLabel.Location = new System.Drawing.Point(93, 402);
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
            this.fileBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileBrowser.Location = new System.Drawing.Point(3, 39);
            this.fileBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.fileBrowser.Name = "fileBrowser";
            this.fileBrowser.Size = new System.Drawing.Size(570, 426);
            this.fileBrowser.TabIndex = 9;
            this.fileBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.fileBrowser_DocumentCompleted);
            // 
            // explorerText
            // 
            this.explorerText.Location = new System.Drawing.Point(6, 13);
            this.explorerText.Name = "explorerText";
            this.explorerText.Size = new System.Drawing.Size(277, 20);
            this.explorerText.TabIndex = 10;
            this.explorerText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.explorerText_Enter);
            // 
            // loginLink
            // 
            this.loginLink.AutoSize = true;
            this.loginLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginLink.Location = new System.Drawing.Point(0, 0);
            this.loginLink.Name = "loginLink";
            this.loginLink.Size = new System.Drawing.Size(133, 13);
            this.loginLink.TabIndex = 11;
            this.loginLink.TabStop = true;
            this.loginLink.Text = "Login or Change Accounts";
            this.loginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginLink_LinkClicked);
            // 
            // picasaLink
            // 
            this.picasaLink.AutoSize = true;
            this.picasaLink.Location = new System.Drawing.Point(139, 0);
            this.picasaLink.Name = "picasaLink";
            this.picasaLink.Size = new System.Drawing.Size(81, 13);
            this.picasaLink.TabIndex = 12;
            this.picasaLink.TabStop = true;
            this.picasaLink.Text = "Visit Picasaweb";
            this.picasaLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.picasaLink_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mapLinkLabel);
            this.panel1.Controls.Add(this.albumPreviewLabel);
            this.panel1.Controls.Add(this.calendarLabel);
            this.panel1.Controls.Add(this.AlbumPicture);
            this.panel1.Controls.Add(this.albumCalendar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(584, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(246, 517);
            this.panel1.TabIndex = 13;
            // 
            // tabViewer
            // 
            this.tabViewer.Controls.Add(this.imageTab);
            this.tabViewer.Controls.Add(this.settingsTab);
            this.tabViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabViewer.Location = new System.Drawing.Point(0, 36);
            this.tabViewer.Name = "tabViewer";
            this.tabViewer.SelectedIndex = 0;
            this.tabViewer.Size = new System.Drawing.Size(584, 494);
            this.tabViewer.TabIndex = 14;
            // 
            // imageTab
            // 
            this.imageTab.Controls.Add(this.fileBrowser);
            this.imageTab.Controls.Add(this.uploadDir);
            this.imageTab.Controls.Add(this.directoryBrowse);
            this.imageTab.Controls.Add(this.explorerText);
            this.imageTab.Location = new System.Drawing.Point(4, 22);
            this.imageTab.Name = "imageTab";
            this.imageTab.Padding = new System.Windows.Forms.Padding(3);
            this.imageTab.Size = new System.Drawing.Size(576, 468);
            this.imageTab.TabIndex = 0;
            this.imageTab.Text = "Image Browser";
            this.imageTab.UseVisualStyleBackColor = true;
            // 
            // settingsTab
            // 
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(576, 468);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Camera Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 530);
            this.Controls.Add(this.tabViewer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picasaLink);
            this.Controls.Add(this.loginLink);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "reLiveMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "explore. reLive. share.";
            this.Load += new System.EventHandler(this.reLiveMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabViewer.ResumeLayout(false);
            this.imageTab.ResumeLayout(false);
            this.imageTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button directoryBrowse;
        private System.Windows.Forms.Button uploadDir;
        private System.Windows.Forms.MonthCalendar albumCalendar;
        private System.Windows.Forms.PictureBox AlbumPicture;
        private System.Windows.Forms.Label calendarLabel;
        private System.Windows.Forms.Label albumPreviewLabel;
        private System.Windows.Forms.LinkLabel mapLinkLabel;
        private System.Windows.Forms.WebBrowser fileBrowser;
        private System.Windows.Forms.TextBox explorerText;
        private System.Windows.Forms.LinkLabel loginLink;
        private System.Windows.Forms.LinkLabel picasaLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabViewer;
        private System.Windows.Forms.TabPage imageTab;
        private System.Windows.Forms.TabPage settingsTab;
    }
}

