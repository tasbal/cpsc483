namespace reLive
{
    partial class SyncDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncDate));
            this.syncDataPicker = new System.Windows.Forms.DateTimePicker();
            this.syncDateLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // syncDataPicker
            // 
            this.syncDataPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.syncDataPicker.Location = new System.Drawing.Point(54, 28);
            this.syncDataPicker.Name = "syncDataPicker";
            this.syncDataPicker.Size = new System.Drawing.Size(80, 20);
            this.syncDataPicker.TabIndex = 0;
            // 
            // syncDateLabel
            // 
            this.syncDateLabel.AutoSize = true;
            this.syncDateLabel.Location = new System.Drawing.Point(7, 10);
            this.syncDateLabel.Name = "syncDateLabel";
            this.syncDateLabel.Size = new System.Drawing.Size(171, 13);
            this.syncDateLabel.TabIndex = 1;
            this.syncDateLabel.Text = "Please select a date for this album:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(54, 54);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Select";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // SyncDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 89);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.syncDateLabel);
            this.Controls.Add(this.syncDataPicker);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncDate";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Album Date";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker syncDataPicker;
        private System.Windows.Forms.Label syncDateLabel;
        private System.Windows.Forms.Button okButton;
    }
}