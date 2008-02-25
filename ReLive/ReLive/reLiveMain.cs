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
        private List<PicasaEntry> albumList = new List<PicasaEntry>();
        private String dirPath;

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
                dirPath = fbd.SelectedPath;
                populateFileList(dirPath);
            }
        }

        private bool checkAlbumExists(string albumName)
        {
            bool albumExists = false;
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(this.user));

            PicasaFeed feed = picasaService.Query(query);

            foreach (PicasaEntry entry in feed.Entries)
            {
                if (entry.Title.Text.Equals(albumName)) albumExists = true;
            }
            return albumExists;
        }

        private bool checkFileExists(string fileName, string albumName)
        {
            bool fileExists = false;

            PhotoQuery query = new PhotoQuery(PicasaQuery.CreatePicasaUri(this.user, albumName));
            PicasaFeed feed = picasaService.Query(query);

            foreach (PicasaEntry entry in feed.Entries)
            {
                if(entry.Title.Text.Equals(fileName)) fileExists = true;
            }

            return fileExists;
        }

        private void createNewAlbum(string albumName, string desc)
        {
            if(!checkAlbumExists(albumName))
            {
                Uri feedUri = new Uri(this.picasaFeed.Post);
                AlbumEntry newEntry = new AlbumEntry();
                newEntry.Title.Text = albumName;
                newEntry.Summary.Text = desc;

                PicasaEntry createdEntry = (PicasaEntry)picasaService.Insert(feedUri, newEntry);
            }
        }

        private void uploadDir_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] jpgFiles = dir.GetFiles("*.jpg");
            DateTime currTime = DateTime.Now;
            string currDate = currTime.ToString("yyyyMMdd");
            string desc = "Album Created " + currDate;

            createNewAlbum(currDate, desc);

            Uri postUri = new Uri(PicasaQuery.CreatePicasaUri(this.user, currDate));

            foreach (FileInfo file in jpgFiles)
            {
                
                string fileStr = file.FullName;

                if (!checkFileExists(file.Name, currDate))
                {
                    FileStream fileStream = file.OpenRead();
                    PicasaEntry entry = this.picasaService.Insert(postUri, fileStream, "image/jpeg", fileStr) as PicasaEntry;
                }
            }

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
                    //this.Close();
                    MessageBox.Show("You will not be able to access your web albums without logging in!");
                else
                {
                    picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                    UpdateAlbumFeed();
                }
            }
        }

        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.albumList.Clear();

            query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));

            this.picasaFeed = this.picasaService.Query(query);

            if (this.picasaFeed != null && this.picasaFeed.Entries.Count > 0)
            {
                foreach (PicasaEntry entry in this.picasaFeed.Entries)
                {
                    albumList.Add(entry);
                    albumCalendar.AddBoldedDate(entry.Published); //adds album dates to calendar as bold entries
                }
            }
            this.albumCalendar.UpdateBoldedDates();
        }

        private void albumCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.AlbumPicture.Image = null;
            //this.AlbumInspector.SelectedObject = null;

            foreach(PicasaEntry entry in this.albumList)
            {
                if (this.albumCalendar.SelectionStart.ToShortDateString().Equals(entry.Published.ToShortDateString()))
                    setSelection(entry);
            }
        }

        private void setSelection(PicasaEntry entry)
        {
            this.Cursor = Cursors.WaitCursor;
            MediaThumbnail thumb = entry.Media.Thumbnails[0];
            Stream stream = this.picasaService.Query(new Uri(thumb.Attributes["url"] as string));
            this.AlbumPicture.Image = new Bitmap(stream);
            //this.AlbumInspector.SelectedObject = new AlbumAccessor(entry);
            this.Cursor = Cursors.Default;

        }
    }
}