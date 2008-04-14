namespace reLive
{
    partial class SelectDrive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDrive));
            this.deviceBox = new System.Windows.Forms.ComboBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deviceBox
            // 
            this.deviceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceBox.FormattingEnabled = true;
            this.deviceBox.Location = new System.Drawing.Point(5, 40);
            this.deviceBox.Name = "deviceBox";
            this.deviceBox.Size = new System.Drawing.Size(230, 21);
            this.deviceBox.TabIndex = 0;
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(72, 67);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(78, 26);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please choose from the list below:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(156, 67);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(79, 26);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SelectDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 116);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.deviceBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SelectDrive";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Removable Devices";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox deviceBox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
    }
}