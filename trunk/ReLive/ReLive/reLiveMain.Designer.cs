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
            this.formatSD = new System.Windows.Forms.Button();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.albumLabel = new System.Windows.Forms.LinkLabel();
            this.calendarPanel = new System.Windows.Forms.Panel();
            this.albumCalendar = new System.Windows.Forms.MonthCalendar();
            this.calendarLabel = new System.Windows.Forms.Label();
            this.retrieveSD = new System.Windows.Forms.Button();
            this.tabViewer = new System.Windows.Forms.TabControl();
            this.imageTab = new System.Windows.Forms.TabPage();
            this.backButton = new System.Windows.Forms.Button();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.reLiveImage = new System.Windows.Forms.PictureBox();
            this.schedulerGroup = new System.Windows.Forms.GroupBox();
            this.endLabel = new System.Windows.Forms.Label();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.startLabel = new System.Windows.Forms.Label();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.schedulerCheck = new System.Windows.Forms.CheckBox();
            this.schedulerLabel = new System.Windows.Forms.Label();
            this.distanceBox = new System.Windows.Forms.TextBox();
            this.haloCheck = new System.Windows.Forms.CheckBox();
            this.haloEnable = new System.Windows.Forms.Label();
            this.writeConfig = new System.Windows.Forms.Button();
            this.gpsGroup = new System.Windows.Forms.GroupBox();
            this.haloDistanceBox = new System.Windows.Forms.TextBox();
            this.viewHalo = new System.Windows.Forms.Button();
            this.haloFeetLabel = new System.Windows.Forms.Label();
            this.haloDistanceLabel = new System.Windows.Forms.Label();
            this.lngLabel = new System.Windows.Forms.Label();
            this.lngBox = new System.Windows.Forms.TextBox();
            this.latLabel = new System.Windows.Forms.Label();
            this.latBox = new System.Windows.Forms.TextBox();
            this.haloSearchGroup = new System.Windows.Forms.GroupBox();
            this.haloDescriptionLabel = new System.Windows.Forms.Label();
            this.haloDescription = new System.Windows.Forms.TextBox();
            this.zipBox = new System.Windows.Forms.TextBox();
            this.stateBox = new System.Windows.Forms.ComboBox();
            this.cityBox = new System.Windows.Forms.TextBox();
            this.streetBox = new System.Windows.Forms.TextBox();
            this.zipLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.cityLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.geoCode = new System.Windows.Forms.Button();
            this.minFeetLabel = new System.Windows.Forms.Label();
            this.minDistance = new System.Windows.Forms.Label();
            this.delayBox = new System.Windows.Forms.ComboBox();
            this.delayLabel = new System.Windows.Forms.Label();
            this.uploadProgress = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AlbumPicture)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.previewPanel.SuspendLayout();
            this.calendarPanel.SuspendLayout();
            this.tabViewer.SuspendLayout();
            this.imageTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reLiveImage)).BeginInit();
            this.schedulerGroup.SuspendLayout();
            this.gpsGroup.SuspendLayout();
            this.haloSearchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryBrowse
            // 
            this.directoryBrowse.Location = new System.Drawing.Point(475, 9);
            this.directoryBrowse.Name = "directoryBrowse";
            this.directoryBrowse.Size = new System.Drawing.Size(50, 25);
            this.directoryBrowse.TabIndex = 2;
            this.directoryBrowse.Text = "Browse";
            this.directoryBrowse.UseVisualStyleBackColor = true;
            this.directoryBrowse.Click += new System.EventHandler(this.directoryBrowse_Click);
            // 
            // uploadDir
            // 
            this.uploadDir.Location = new System.Drawing.Point(531, 9);
            this.uploadDir.Name = "uploadDir";
            this.uploadDir.Size = new System.Drawing.Size(51, 25);
            this.uploadDir.TabIndex = 2;
            this.uploadDir.Text = "Upload";
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
            this.mapLinkLabel.Location = new System.Drawing.Point(60, 182);
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
            this.fileBrowser.IsWebBrowserContextMenuEnabled = false;
            this.fileBrowser.Location = new System.Drawing.Point(3, 35);
            this.fileBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.fileBrowser.Name = "fileBrowser";
            this.fileBrowser.ScriptErrorsSuppressed = true;
            this.fileBrowser.Size = new System.Drawing.Size(576, 426);
            this.fileBrowser.TabIndex = 9;
            this.fileBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.fileBrowser_DocumentCompleted);
            // 
            // explorerText
            // 
            this.explorerText.Location = new System.Drawing.Point(8, 12);
            this.explorerText.Name = "explorerText";
            this.explorerText.Size = new System.Drawing.Size(412, 20);
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
            this.rightPanel.Controls.Add(this.formatSD);
            this.rightPanel.Controls.Add(this.previewPanel);
            this.rightPanel.Controls.Add(this.calendarPanel);
            this.rightPanel.Controls.Add(this.retrieveSD);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(590, 13);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(240, 511);
            this.rightPanel.TabIndex = 13;
            // 
            // formatSD
            // 
            this.formatSD.Location = new System.Drawing.Point(49, 474);
            this.formatSD.Name = "formatSD";
            this.formatSD.Size = new System.Drawing.Size(156, 28);
            this.formatSD.TabIndex = 14;
            this.formatSD.Text = "Format SD Card";
            this.formatSD.UseVisualStyleBackColor = true;
            this.formatSD.Click += new System.EventHandler(this.formatSD_Click);
            // 
            // previewPanel
            // 
            this.previewPanel.Controls.Add(this.albumLabel);
            this.previewPanel.Controls.Add(this.mapLinkLabel);
            this.previewPanel.Controls.Add(this.albumPreviewLabel);
            this.previewPanel.Controls.Add(this.AlbumPicture);
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewPanel.Location = new System.Drawing.Point(0, 204);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(240, 221);
            this.previewPanel.TabIndex = 13;
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Location = new System.Drawing.Point(120, 182);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(62, 13);
            this.albumLabel.TabIndex = 9;
            this.albumLabel.TabStop = true;
            this.albumLabel.Text = "View Album";
            this.albumLabel.Visible = false;
            this.albumLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.albumLabel_LinkClicked);
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
            this.albumCalendar.Location = new System.Drawing.Point(27, 43);
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
            this.retrieveSD.Location = new System.Drawing.Point(49, 431);
            this.retrieveSD.Name = "retrieveSD";
            this.retrieveSD.Size = new System.Drawing.Size(154, 37);
            this.retrieveSD.TabIndex = 9;
            this.retrieveSD.Text = "Sync Pictures From SD Card";
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
            this.imageTab.Controls.Add(this.backButton);
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
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(426, 9);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(43, 25);
            this.backButton.TabIndex = 11;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // settingsTab
            // 
            this.settingsTab.BackColor = System.Drawing.SystemColors.Control;
            this.settingsTab.Controls.Add(this.reLiveImage);
            this.settingsTab.Controls.Add(this.schedulerGroup);
            this.settingsTab.Controls.Add(this.schedulerCheck);
            this.settingsTab.Controls.Add(this.schedulerLabel);
            this.settingsTab.Controls.Add(this.distanceBox);
            this.settingsTab.Controls.Add(this.haloCheck);
            this.settingsTab.Controls.Add(this.haloEnable);
            this.settingsTab.Controls.Add(this.writeConfig);
            this.settingsTab.Controls.Add(this.gpsGroup);
            this.settingsTab.Controls.Add(this.haloSearchGroup);
            this.settingsTab.Controls.Add(this.minFeetLabel);
            this.settingsTab.Controls.Add(this.minDistance);
            this.settingsTab.Controls.Add(this.delayBox);
            this.settingsTab.Controls.Add(this.delayLabel);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(582, 464);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Camera Settings";
            // 
            // reLiveImage
            // 
            this.reLiveImage.Image = ((System.Drawing.Image)(resources.GetObject("reLiveImage.Image")));
            this.reLiveImage.Location = new System.Drawing.Point(236, 30);
            this.reLiveImage.Name = "reLiveImage";
            this.reLiveImage.Size = new System.Drawing.Size(340, 184);
            this.reLiveImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.reLiveImage.TabIndex = 39;
            this.reLiveImage.TabStop = false;
            // 
            // schedulerGroup
            // 
            this.schedulerGroup.Controls.Add(this.endLabel);
            this.schedulerGroup.Controls.Add(this.endTime);
            this.schedulerGroup.Controls.Add(this.startLabel);
            this.schedulerGroup.Controls.Add(this.startTime);
            this.schedulerGroup.Location = new System.Drawing.Point(6, 119);
            this.schedulerGroup.Name = "schedulerGroup";
            this.schedulerGroup.Size = new System.Drawing.Size(215, 85);
            this.schedulerGroup.TabIndex = 38;
            this.schedulerGroup.TabStop = false;
            this.schedulerGroup.Text = "Scheduler Settings";
            this.schedulerGroup.Visible = false;
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(15, 60);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(32, 13);
            this.endLabel.TabIndex = 38;
            this.endLabel.Text = "Stop:";
            // 
            // endTime
            // 
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endTime.Location = new System.Drawing.Point(75, 53);
            this.endTime.Name = "endTime";
            this.endTime.ShowUpDown = true;
            this.endTime.Size = new System.Drawing.Size(100, 20);
            this.endTime.TabIndex = 39;
            this.endTime.ValueChanged += new System.EventHandler(this.endTime_ValueChanged);
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(15, 25);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(32, 13);
            this.startLabel.TabIndex = 0;
            this.startLabel.Text = "Start:";
            // 
            // startTime
            // 
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTime.Location = new System.Drawing.Point(75, 18);
            this.startTime.Name = "startTime";
            this.startTime.ShowUpDown = true;
            this.startTime.Size = new System.Drawing.Size(100, 20);
            this.startTime.TabIndex = 37;
            this.startTime.ValueChanged += new System.EventHandler(this.startTime_ValueChanged);
            // 
            // schedulerCheck
            // 
            this.schedulerCheck.AutoSize = true;
            this.schedulerCheck.Location = new System.Drawing.Point(135, 96);
            this.schedulerCheck.Name = "schedulerCheck";
            this.schedulerCheck.Size = new System.Drawing.Size(65, 17);
            this.schedulerCheck.TabIndex = 36;
            this.schedulerCheck.Text = "Enabled";
            this.schedulerCheck.UseVisualStyleBackColor = true;
            this.schedulerCheck.CheckedChanged += new System.EventHandler(this.schedulerCheck_CheckedChanged);
            // 
            // schedulerLabel
            // 
            this.schedulerLabel.AutoSize = true;
            this.schedulerLabel.Location = new System.Drawing.Point(20, 96);
            this.schedulerLabel.Name = "schedulerLabel";
            this.schedulerLabel.Size = new System.Drawing.Size(58, 13);
            this.schedulerLabel.TabIndex = 35;
            this.schedulerLabel.Text = "Scheduler:";
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(135, 60);
            this.distanceBox.MaxLength = 7;
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceBox.Size = new System.Drawing.Size(50, 20);
            this.distanceBox.TabIndex = 34;
            this.distanceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.distanceBox.TextChanged += new System.EventHandler(this.distanceBox_TextChanged);
            // 
            // haloCheck
            // 
            this.haloCheck.AutoSize = true;
            this.haloCheck.Location = new System.Drawing.Point(137, 207);
            this.haloCheck.Name = "haloCheck";
            this.haloCheck.Size = new System.Drawing.Size(65, 17);
            this.haloCheck.TabIndex = 33;
            this.haloCheck.Text = "Enabled";
            this.haloCheck.UseVisualStyleBackColor = true;
            this.haloCheck.CheckedChanged += new System.EventHandler(this.haloCheck_CheckedChanged);
            // 
            // haloEnable
            // 
            this.haloEnable.AutoSize = true;
            this.haloEnable.Location = new System.Drawing.Point(21, 207);
            this.haloEnable.Name = "haloEnable";
            this.haloEnable.Size = new System.Drawing.Size(76, 13);
            this.haloEnable.TabIndex = 32;
            this.haloEnable.Text = "Location Halo:";
            // 
            // writeConfig
            // 
            this.writeConfig.Location = new System.Drawing.Point(479, 426);
            this.writeConfig.Name = "writeConfig";
            this.writeConfig.Size = new System.Drawing.Size(97, 32);
            this.writeConfig.TabIndex = 30;
            this.writeConfig.Text = "Save Config";
            this.writeConfig.UseVisualStyleBackColor = true;
            this.writeConfig.Click += new System.EventHandler(this.writeConfig_Click);
            // 
            // gpsGroup
            // 
            this.gpsGroup.Controls.Add(this.haloDistanceBox);
            this.gpsGroup.Controls.Add(this.viewHalo);
            this.gpsGroup.Controls.Add(this.haloFeetLabel);
            this.gpsGroup.Controls.Add(this.haloDistanceLabel);
            this.gpsGroup.Controls.Add(this.lngLabel);
            this.gpsGroup.Controls.Add(this.lngBox);
            this.gpsGroup.Controls.Add(this.latLabel);
            this.gpsGroup.Controls.Add(this.latBox);
            this.gpsGroup.Location = new System.Drawing.Point(227, 230);
            this.gpsGroup.Name = "gpsGroup";
            this.gpsGroup.Size = new System.Drawing.Size(215, 175);
            this.gpsGroup.TabIndex = 29;
            this.gpsGroup.TabStop = false;
            this.gpsGroup.Text = "Halo Settings";
            this.gpsGroup.Visible = false;
            // 
            // haloDistanceBox
            // 
            this.haloDistanceBox.Location = new System.Drawing.Point(70, 97);
            this.haloDistanceBox.MaxLength = 7;
            this.haloDistanceBox.Name = "haloDistanceBox";
            this.haloDistanceBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.haloDistanceBox.Size = new System.Drawing.Size(50, 20);
            this.haloDistanceBox.TabIndex = 35;
            this.haloDistanceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.haloDistanceBox.TextChanged += new System.EventHandler(this.haloDistanceBox_TextChanged);
            // 
            // viewHalo
            // 
            this.viewHalo.Location = new System.Drawing.Point(70, 123);
            this.viewHalo.Name = "viewHalo";
            this.viewHalo.Size = new System.Drawing.Size(50, 25);
            this.viewHalo.TabIndex = 28;
            this.viewHalo.Text = "View";
            this.viewHalo.UseVisualStyleBackColor = true;
            this.viewHalo.Click += new System.EventHandler(this.viewHalo_Click);
            // 
            // haloFeetLabel
            // 
            this.haloFeetLabel.AutoSize = true;
            this.haloFeetLabel.Location = new System.Drawing.Point(125, 100);
            this.haloFeetLabel.Name = "haloFeetLabel";
            this.haloFeetLabel.Size = new System.Drawing.Size(39, 13);
            this.haloFeetLabel.TabIndex = 26;
            this.haloFeetLabel.Text = "Meters";
            // 
            // haloDistanceLabel
            // 
            this.haloDistanceLabel.AutoSize = true;
            this.haloDistanceLabel.Location = new System.Drawing.Point(6, 100);
            this.haloDistanceLabel.Name = "haloDistanceLabel";
            this.haloDistanceLabel.Size = new System.Drawing.Size(42, 13);
            this.haloDistanceLabel.TabIndex = 25;
            this.haloDistanceLabel.Text = "Range:";
            // 
            // lngLabel
            // 
            this.lngLabel.AutoSize = true;
            this.lngLabel.Location = new System.Drawing.Point(6, 72);
            this.lngLabel.Name = "lngLabel";
            this.lngLabel.Size = new System.Drawing.Size(57, 13);
            this.lngLabel.TabIndex = 24;
            this.lngLabel.Text = "Longitude:";
            // 
            // lngBox
            // 
            this.lngBox.Location = new System.Drawing.Point(70, 71);
            this.lngBox.Name = "lngBox";
            this.lngBox.Size = new System.Drawing.Size(120, 20);
            this.lngBox.TabIndex = 23;
            // 
            // latLabel
            // 
            this.latLabel.AutoSize = true;
            this.latLabel.Location = new System.Drawing.Point(6, 43);
            this.latLabel.Name = "latLabel";
            this.latLabel.Size = new System.Drawing.Size(48, 13);
            this.latLabel.TabIndex = 22;
            this.latLabel.Text = "Latitude:";
            // 
            // latBox
            // 
            this.latBox.Location = new System.Drawing.Point(70, 40);
            this.latBox.Name = "latBox";
            this.latBox.Size = new System.Drawing.Size(120, 20);
            this.latBox.TabIndex = 21;
            // 
            // haloSearchGroup
            // 
            this.haloSearchGroup.Controls.Add(this.haloDescriptionLabel);
            this.haloSearchGroup.Controls.Add(this.haloDescription);
            this.haloSearchGroup.Controls.Add(this.zipBox);
            this.haloSearchGroup.Controls.Add(this.stateBox);
            this.haloSearchGroup.Controls.Add(this.cityBox);
            this.haloSearchGroup.Controls.Add(this.streetBox);
            this.haloSearchGroup.Controls.Add(this.zipLabel);
            this.haloSearchGroup.Controls.Add(this.stateLabel);
            this.haloSearchGroup.Controls.Add(this.cityLabel);
            this.haloSearchGroup.Controls.Add(this.streetLabel);
            this.haloSearchGroup.Controls.Add(this.geoCode);
            this.haloSearchGroup.Location = new System.Drawing.Point(6, 230);
            this.haloSearchGroup.Name = "haloSearchGroup";
            this.haloSearchGroup.Size = new System.Drawing.Size(215, 176);
            this.haloSearchGroup.TabIndex = 28;
            this.haloSearchGroup.TabStop = false;
            this.haloSearchGroup.Text = "Halo Location Search";
            this.haloSearchGroup.Visible = false;
            // 
            // haloDescriptionLabel
            // 
            this.haloDescriptionLabel.AutoSize = true;
            this.haloDescriptionLabel.Location = new System.Drawing.Point(8, 43);
            this.haloDescriptionLabel.Name = "haloDescriptionLabel";
            this.haloDescriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.haloDescriptionLabel.TabIndex = 22;
            this.haloDescriptionLabel.Text = "Description:";
            // 
            // haloDescription
            // 
            this.haloDescription.Location = new System.Drawing.Point(75, 40);
            this.haloDescription.Name = "haloDescription";
            this.haloDescription.Size = new System.Drawing.Size(135, 20);
            this.haloDescription.TabIndex = 21;
            // 
            // zipBox
            // 
            this.zipBox.Location = new System.Drawing.Point(75, 147);
            this.zipBox.Name = "zipBox";
            this.zipBox.Size = new System.Drawing.Size(65, 20);
            this.zipBox.TabIndex = 18;
            this.zipBox.TextChanged += new System.EventHandler(this.zipBox_TextChanged);
            // 
            // stateBox
            // 
            this.stateBox.AutoCompleteCustomSource.AddRange(new string[] {
            "AL",
            "AK",
            "NZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WA-DC",
            "WV",
            "WI",
            "WY"});
            this.stateBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.stateBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.stateBox.FormattingEnabled = true;
            this.stateBox.Items.AddRange(new object[] {
            "AL",
            "AK",
            "NZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WA-DC",
            "WV",
            "WI",
            "WY"});
            this.stateBox.Location = new System.Drawing.Point(75, 120);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(65, 21);
            this.stateBox.TabIndex = 17;
            // 
            // cityBox
            // 
            this.cityBox.Location = new System.Drawing.Point(75, 94);
            this.cityBox.Name = "cityBox";
            this.cityBox.Size = new System.Drawing.Size(135, 20);
            this.cityBox.TabIndex = 16;
            // 
            // streetBox
            // 
            this.streetBox.Location = new System.Drawing.Point(75, 68);
            this.streetBox.Name = "streetBox";
            this.streetBox.Size = new System.Drawing.Size(135, 20);
            this.streetBox.TabIndex = 15;
            // 
            // zipLabel
            // 
            this.zipLabel.AutoSize = true;
            this.zipLabel.Location = new System.Drawing.Point(15, 150);
            this.zipLabel.Name = "zipLabel";
            this.zipLabel.Size = new System.Drawing.Size(53, 13);
            this.zipLabel.TabIndex = 14;
            this.zipLabel.Text = "Zip Code:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(33, 123);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(35, 13);
            this.stateLabel.TabIndex = 13;
            this.stateLabel.Text = "State:";
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(41, 97);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(27, 13);
            this.cityLabel.TabIndex = 12;
            this.cityLabel.Text = "City:";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(30, 71);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(38, 13);
            this.streetLabel.TabIndex = 11;
            this.streetLabel.Text = "Street:";
            // 
            // geoCode
            // 
            this.geoCode.Location = new System.Drawing.Point(146, 120);
            this.geoCode.Name = "geoCode";
            this.geoCode.Size = new System.Drawing.Size(66, 50);
            this.geoCode.TabIndex = 10;
            this.geoCode.Text = "Get GPS";
            this.geoCode.UseVisualStyleBackColor = true;
            this.geoCode.Click += new System.EventHandler(this.geoCode_Click);
            // 
            // minFeetLabel
            // 
            this.minFeetLabel.AutoSize = true;
            this.minFeetLabel.Location = new System.Drawing.Point(191, 67);
            this.minFeetLabel.Name = "minFeetLabel";
            this.minFeetLabel.Size = new System.Drawing.Size(39, 13);
            this.minFeetLabel.TabIndex = 8;
            this.minFeetLabel.Text = "Meters";
            // 
            // minDistance
            // 
            this.minDistance.AutoSize = true;
            this.minDistance.Location = new System.Drawing.Point(20, 67);
            this.minDistance.Name = "minDistance";
            this.minDistance.Size = new System.Drawing.Size(96, 13);
            this.minDistance.TabIndex = 6;
            this.minDistance.Text = "Minimum Distance:";
            // 
            // delayBox
            // 
            this.delayBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
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
            this.delayBox.Location = new System.Drawing.Point(135, 30);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(50, 21);
            this.delayBox.TabIndex = 2;
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Location = new System.Drawing.Point(20, 38);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(109, 13);
            this.delayLabel.TabIndex = 0;
            this.delayLabel.Text = "Time Delay (Minutes):";
            // 
            // uploadProgress
            // 
            this.uploadProgress.Location = new System.Drawing.Point(175, 30);
            this.uploadProgress.Name = "uploadProgress";
            this.uploadProgress.Size = new System.Drawing.Size(410, 20);
            this.uploadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.uploadProgress.TabIndex = 11;
            this.uploadProgress.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(380, 10);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(12, 13);
            this.progressLabel.TabIndex = 15;
            this.progressLabel.Text = "/";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.progressLabel.Visible = false;
            // 
            // reLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 524);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.uploadProgress);
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
            ((System.ComponentModel.ISupportInitialize)(this.reLiveImage)).EndInit();
            this.schedulerGroup.ResumeLayout(false);
            this.schedulerGroup.PerformLayout();
            this.gpsGroup.ResumeLayout(false);
            this.gpsGroup.PerformLayout();
            this.haloSearchGroup.ResumeLayout(false);
            this.haloSearchGroup.PerformLayout();
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
        private System.Windows.Forms.Label minFeetLabel;
        private System.Windows.Forms.Button geoCode;
        private System.Windows.Forms.ComboBox stateBox;
        private System.Windows.Forms.TextBox cityBox;
        private System.Windows.Forms.TextBox streetBox;
        private System.Windows.Forms.Label zipLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label streetLabel;
        private System.Windows.Forms.TextBox zipBox;
        private System.Windows.Forms.Label latLabel;
        private System.Windows.Forms.TextBox latBox;
        private System.Windows.Forms.Label lngLabel;
        private System.Windows.Forms.TextBox lngBox;
        private System.Windows.Forms.Label haloFeetLabel;
        private System.Windows.Forms.Label haloDistanceLabel;
        private System.Windows.Forms.GroupBox gpsGroup;
        private System.Windows.Forms.GroupBox haloSearchGroup;
        private System.Windows.Forms.Button writeConfig;
        private System.Windows.Forms.CheckBox haloCheck;
        private System.Windows.Forms.Label haloEnable;
        private System.Windows.Forms.Button viewHalo;
        private System.Windows.Forms.TextBox distanceBox;
        private System.Windows.Forms.TextBox haloDistanceBox;
        private System.Windows.Forms.Button formatSD;
        private System.Windows.Forms.Label haloDescriptionLabel;
        private System.Windows.Forms.TextBox haloDescription;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.LinkLabel albumLabel;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.CheckBox schedulerCheck;
        private System.Windows.Forms.Label schedulerLabel;
        private System.Windows.Forms.GroupBox schedulerGroup;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.PictureBox reLiveImage;
    }
}

