namespace ReLive
{
    partial class MapBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapBrowser));
            this.albumMap = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // albumMap
            // 
            this.albumMap.AllowNavigation = false;
            this.albumMap.AllowWebBrowserDrop = false;
            this.albumMap.IsWebBrowserContextMenuEnabled = false;
            this.albumMap.Location = new System.Drawing.Point(-261, -141);
            this.albumMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.albumMap.Name = "albumMap";
            this.albumMap.ScriptErrorsSuppressed = true;
            this.albumMap.ScrollBarsEnabled = false;
            this.albumMap.Size = new System.Drawing.Size(797, 695);
            this.albumMap.TabIndex = 0;
            this.albumMap.Url = new System.Uri("", System.UriKind.Relative);
            this.albumMap.WebBrowserShortcutsEnabled = false;
            this.albumMap.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.albumMap_DocumentCompleted);
            // 
            // MapBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 514);
            this.Controls.Add(this.albumMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MapBrowser";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Album Map";
            this.Load += new System.EventHandler(this.MapBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser albumMap;
    }
}