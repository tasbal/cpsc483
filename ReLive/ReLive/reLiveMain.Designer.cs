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
            this.AlbumPicture = new System.Windows.Forms.PictureBox();
            this.albumPreviewLabel = new System.Windows.Forms.Label();
            this.mapLinkLabel = new System.Windows.Forms.LinkLabel();
            this.fileBrowser = new System.Windows.Forms.WebBrowser();
            this.explorerText = new System.Windows.Forms.TextBox();
            this.loginLink = new System.Windows.Forms.LinkLabel();
            this.picasaLink = new System.Windows.Forms.LinkLabel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.calendarPanel = new System.Windows.Forms.Panel();
            this.albumCalendar = new System.Windows.Forms.MonthCalendar();
            this.calendarLabel = new System.Windows.Forms.Label();
            this.retrieveSD = new System.Windows.Forms.Button();
            this.tabViewer = new System.Windows.Forms.TabControl();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.uploadProgress = new System.Windows.Forms.ProgressBar();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.delayLabel = new System.Windows.Forms.Label();
            this.delayBox = new System.Windows.Forms.ComboBox();
            this.faceLabel = new System.Windows.Forms.Label();
            this.faceEnabled = new System.Windows.Forms.RadioButton();
            this.faceDisabled = new System.Windows.Forms.RadioButton();
            this.minDistance = new System.Windows.Forms.Label();
            this.distanceBox = new System.Windows.Forms.MaskedTextBox();
            this.feetLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.previewPanel.SuspendLayout();
            this.calendarPanel.SuspendLayout();
            this.tabViewer.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(390, 12);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(50, 20);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            // 
            // uploadDir
            // 
            this.uploadDir.Location = new System.Drawing.Point(446, 12);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(130, 20);
            this.uploadDir.TabIndex = 2;
            this.uploadDir.Text = "Upload Image Directory";
            this.uploadDir.UseVisualStyleBackColor = true;
            this.uploadDir.Click += new System.EventHandler(this.uploadDir_Click);
            // 
            // AlbumPicture
            // 
            this.AlbumPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AlbumPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AlbumPicture.Location = new System.Drawing.Point(0, 19);
            this.AlbumPicture.Name = "AlbumPicture";
            this.AlbumPicture.Size = new System.Drawing.Size(240, 160);
            this.AlbumPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.AlbumPicture.TabIndex = 5;
            this.AlbumPicture.TabStop = false;
            // 
            // albumPreviewLabel
            // 
            this.albumPreviewLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.albumPreviewLabel.AutoSize = true;
            this.albumPreviewLabel.Location = new System.Drawing.Point(83, 3);
            this.albumPreviewLabel.Name = "albumPreviewLabel";
            this.albumPreviewLabel.Size = new System.Drawing.Size(77, 13);
            this.albumPreviewLabel.TabIndex = 7;
            this.albumPreviewLabel.Text = "Album Preview";
            // 
            // mapLinkLabel
            // 
            this.mapLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapLinkLabel.AutoSize = true;
            this.mapLinkLabel.Location = new System.Drawing.Point(92, 182);
            this.mapLinkLabel.Name = "mapLinkLabel";
            this.mapLinkLabel.Size = new System.Drawing.Size(54, 13);
            this.mapLinkLabel.TabIndex = 8;
            this.mapLinkLabel.TabStop = true;
            this.mapLinkLabel.Text = "View Map";
            this.mapLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mapLinkLabel.Visible = false;
            this.mapLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mapLinkLabel_LinkClicked);
            // 
            // fileBrowser
            // 
            this.fileBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileBrowser.Location = new System.Drawing.Point(3, 35);
            this.fileBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.fileBrowser.Name = "fileBrowser";
            this.fileBrowser.Size = new System.Drawing.Size(576, 426);
            this.fileBrowser.TabIndex = 9;
            this.fileBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.fileBrowser_DocumentCompleted);
            // 
            // explorerText
            // 
            this.explorerText.Location = new System.Drawing.Point(8, 12);
            this.explorerText.Name = "explorerText";
            this.explorerText.Size = new System.Drawing.Size(376, 20);
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
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.previewPanel);
            this.rightPanel.Controls.Add(this.calendarPanel);
            this.rightPanel.Controls.Add(this.retrieveSD);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(590, 13);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(240, 511);
            this.rightPanel.TabIndex = 13;
            // 
            // previewPanel
            // 
            this.previewPanel.Controls.Add(this.mapLinkLabel);
            this.previewPanel.Controls.Add(this.albumPreviewLabel);
            this.previewPanel.Controls.Add(this.AlbumPicture);
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewPanel.Location = new System.Drawing.Point(0, 204);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(240, 221);
            this.previewPanel.TabIndex = 13;
            // 
            // calendarPanel
            // 
            this.calendarPanel.Controls.Add(this.albumCalendar);
            this.calendarPanel.Controls.Add(this.calendarLabel);
            this.calendarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.calendarPanel.Location = new System.Drawing.Point(0, 0);
            this.calendarPanel.Name = "calendarPanel";
            this.calendarPanel.Size = new System.Drawing.Size(240, 204);
            this.calendarPanel.TabIndex = 12;
            // 
            // albumCalendar
            // 
            this.albumCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.albumCalendar.Location = new System.Drawing.Point(31, 43);
            this.albumCalendar.MaxSelectionCount = 1;
            this.albumCalendar.Name = "albumCalendar";
            this.albumCalendar.TabIndex = 10;
            this.albumCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.albumCalendar_DateChanged);
            // 
            // calendarLabel
            // 
            this.calendarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.calendarLabel.AutoSize = true;
            this.calendarLabel.Location = new System.Drawing.Point(46, 21);
            this.calendarLabel.Name = "calendarLabel";
            this.calendarLabel.Size = new System.Drawing.Size(145, 13);
            this.calendarLabel.TabIndex = 11;
            this.calendarLabel.Text = "Browse Web Albums by Date";
            // 
            // retrieveSD
            // 
            this.retrieveSD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.retrieveSD.Location = new System.Drawing.Point(49, 462);
            this.retrieveSD.Name = "retrieveSD";
            this.retrieveSD.Size = new System.Drawing.Size(154, 37);
            this.retrieveSD.TabIndex = 9;
            this.retrieveSD.Text = "Upload Pictures From SD Card";
            this.retrieveSD.UseVisualStyleBackColor = true;
            this.retrieveSD.Click += new System.EventHandler(this.retrieveSD_Click);
            // 
            // tabViewer
            // 
            this.tabViewer.Controls.Add(this.imageTab);
            this.tabViewer.Controls.Add(this.settingsTab);
            this.tabViewer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabViewer.Location = new System.Drawing.Point(0, 34);
            this.tabViewer.Name = "tabViewer";
            this.tabViewer.SelectedIndex = 0;
            this.tabViewer.Size = new System.Drawing.Size(590, 490);
            this.tabViewer.TabIndex = 14;
            // 
            // imageTab
            // 
            this.imageTab.BackColor = System.Drawing.SystemColors.Control;
            this.imageTab.Controls.Add(this.uploadProgress);
            this.imageTab.Controls.Add(this.fileBrowser);
            this.imageTab.Controls.Add(this.uploadDir);
            this.imageTab.Controls.Add(this.directoryBrowse);
            this.imageTab.Controls.Add(this.explorerText);
            this.imageTab.Location = new System.Drawing.Point(4, 22);
            this.imageTab.Name = "imageTab";
            this.imageTab.Padding = new System.Windows.Forms.Padding(3);
            this.imageTab.Size = new System.Drawing.Size(582, 464);
            this.imageTab.TabIndex = 0;
            this.imageTab.Text = "Image Browser";
            // 
            // uploadProgress
            // 
            this.uploadProgress.Location = new System.Drawing.Point(446, 12);
            this.uploadProgress.Name = "uploadProgress";
            this.uploadProgress.Size = new System.Drawing.Size(130, 20);
            this.uploadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.uploadProgress.TabIndex = 11;
            this.uploadProgress.Visible = false;
            // 
            // settingsTab
            // 
            this.settingsTab.BackColor = System.Drawing.SystemColors.Control;
            this.settingsTab.Controls.Add(this.feetLabel);
            this.settingsTab.Controls.Add(this.distanceBox);
            this.settingsTab.Controls.Add(this.minDistance);
            this.settingsTab.Controls.Add(this.faceDisabled);
            this.settingsTab.Controls.Add(this.faceEnabled);
            this.settingsTab.Controls.Add(this.faceLabel);
            this.settingsTab.Controls.Add(this.delayBox);
            this.settingsTab.Controls.Add(this.delayLabel);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(582, 464);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Camera Settings";
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Location = new System.Drawing.Point(19, 32);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(109, 13);
            this.delayLabel.TabIndex = 0;
            this.delayLabel.Text = "Time Delay (Minutes):";
            // 
            // delayBox
            // 
            this.delayBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.delayBox.FormattingEnabled = true;
            this.delayBox.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.delayBox.Location = new System.Drawing.Point(134, 29);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(50, 21);
            this.delayBox.TabIndex = 2;
            // 
            // faceLabel
            // 
            this.faceLabel.AutoSize = true;
            this.faceLabel.Location = new System.Drawing.Point(19, 96);
            this.faceLabel.Name = "faceLabel";
            this.faceLabel.Size = new System.Drawing.Size(83, 13);
            this.faceLabel.TabIndex = 3;
            this.faceLabel.Text = "Face Detection:";
            // 
            // faceEnabled
            // 
            this.faceEnabled.AutoSize = true;
            this.faceEnabled.Location = new System.Drawing.Point(108, 94);
            this.faceEnabled.Name = "faceEnabled";
            this.faceEnabled.Size = new System.Drawing.Size(64, 17);
            this.faceEnabled.TabIndex = 4;
            this.faceEnabled.TabStop = true;
            this.faceEnabled.Text = "Enabled";
            this.faceEnabled.UseVisualStyleBackColor = true;
            // 
            // faceDisabled
            // 
            this.faceDisabled.AutoSize = true;
            this.faceDisabled.Location = new System.Drawing.Point(178, 94);
            this.faceDisabled.Name = "faceDisabled";
            this.faceDisabled.Size = new System.Drawing.Size(66, 17);
            this.faceDisabled.TabIndex = 5;
            this.faceDisabled.TabStop = true;
            this.faceDisabled.Text = "Disabled";
            this.faceDisabled.UseVisualStyleBackColor = true;
            // 
            // minDistance
            // 
            this.minDistance.AutoSize = true;
            this.minDistance.Location = new System.Drawing.Point(19, 65);
            this.minDistance.Name = "minDistance";
            this.minDistance.Size = new System.Drawing.Size(96, 13);
            this.minDistance.TabIndex = 6;
            this.minDistance.Text = "Minimum Distance:";
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(134, 62);
            this.distanceBox.Mask = "00000";
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceBox.Size = new System.Drawing.Size(38, 20);
            this.distanceBox.TabIndex = 7;
            this.distanceBox.ValidatingType = typeof(int);
            // 
            // feetLabel
            // 
            this.feetLabel.AutoSize = true;
            this.feetLabel.Location = new System.Drawing.Point(171, 65);
            this.feetLabel.Name = "feetLabel";
            this.feetLabel.Size = new System.Drawing.Size(28, 13);
            this.feetLabel.TabIndex = 8;
            this.feetLabel.Text = "Feet";
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 524);
            this.Controls.Add(this.tabViewer);
            this.Controls.Add(this.rightPanel);
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
            this.rightPanel.ResumeLayout(false);
            this.previewPanel.ResumeLayout(false);
            this.previewPanel.PerformLayout();
            this.calendarPanel.ResumeLayout(false);
            this.calendarPanel.PerformLayout();
            this.tabViewer.ResumeLayout(false);
            this.imageTab.ResumeLayout(false);
            this.imageTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button directoryBrowse;
        private System.Windows.Forms.Button uploadDir;
        private System.Windows.Forms.PictureBox AlbumPicture;
        private System.Windows.Forms.Label albumPreviewLabel;
        private System.Windows.Forms.LinkLabel mapLinkLabel;
        private System.Windows.Forms.WebBrowser fileBrowser;
        private System.Windows.Forms.TextBox explorerText;
        private System.Windows.Forms.LinkLabel loginLink;
        private System.Windows.Forms.LinkLabel picasaLink;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.TabControl tabViewer;
        private System.Windows.Forms.TabPage imageTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button retrieveSD;
        private System.Windows.Forms.ProgressBar uploadProgress;
        private System.Windows.Forms.MonthCalendar albumCalendar;
        private System.Windows.Forms.Label calendarLabel;
        private System.Windows.Forms.Panel calendarPanel;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.ComboBox delayBox;
        private System.Windows.Forms.Label minDistance;
        private System.Windows.Forms.RadioButton faceDisabled;
        private System.Windows.Forms.RadioButton faceEnabled;
        private System.Windows.Forms.Label faceLabel;
        private System.Windows.Forms.Label feetLabel;
        private System.Windows.Forms.MaskedTextBox distanceBox;
    }
}

