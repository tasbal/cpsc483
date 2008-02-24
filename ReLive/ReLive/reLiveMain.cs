using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;

namespace ReLive
{
    public partial class reLiveMain : Form
    {
        private String googleAuthToken = null;
        private String user = null;
        private PicasaService picasaService = new PicasaService("ReLive");
        private PicasaFeed picasaFeed = null;

        public reLiveMain()
        {
            InitializeComponent();
        }

        public void openImage(string path)
        {
            pictureBox1.Image = new Bitmap(path);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex != -1)
                openImage(((imageData)fileList.Items[fileList.SelectedIndex]).filePath);
        }

        private void directoryBrowse_Click(object sender, EventArgs e)
        {
            fileList.Items.Clear();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                populateFileList(fbd.SelectedPath);
            }
        }
        
        //depreciated, easier way to launch browser
        private void startIE(string path)
        {
            // when run from VS.NET
            System.Diagnostics.Process proc = new
            System.Diagnostics.Process();
            proc.StartInfo.FileName = "iexplore";
            proc.StartInfo.Arguments = path;
            proc.Start();
        }
 
        private void launchSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://picasaweb.google.com/"); 
        }

        private void populateFileList(string Path)
        {
            DirectoryInfo dir = new DirectoryInfo(Path);
            FileInfo[] jpgFiles = dir.GetFiles("*.jpg");

            foreach (FileInfo file in jpgFiles)
            {
                imageData i = new imageData(file.Name, file.FullName);
                fileList.Items.Add(i);
            }
        
        }

        private void panelGoogleData_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void reLiveMain_Load(object sender, EventArgs e)
        {
            if (this.googleAuthToken == null)
            {
                GoogleLogin loginDialog = new GoogleLogin(new PicasaService("reLive"));
                loginDialog.ShowDialog();

                this.googleAuthToken = loginDialog.AuthenticationToken;
                this.user = loginDialog.User;

                if (this.googleAuthToken == null)
                    this.Close();

                picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                UpdateAlbumFeed();
            }
        }

        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.AlbumList.Clear();


            query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));

            this.picasaFeed = this.picasaService.Query(query);

            if (this.picasaFeed != null && this.picasaFeed.Entries.Count > 0)
            {
                foreach (PicasaEntry entry in this.picasaFeed.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.Title.Text +
                                    " (" + entry.getPhotoExtensionValue(GPhotoNameTable.NumPhotos) + " )");
                    item.Tag = entry;
                    this.AlbumList.Items.Add(item);
                }
            }
            this.AlbumList.Update();
        }
    }
}