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
using System.Runtime.InteropServices;


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
        MapBrowser mapWindow = new MapBrowser();
        String userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
        public bool viewSet = false;

        //file browser view settings example
        private const int LV_VIEW_ICON = 0x0000;
        private const int LVM_SETVIEW = 0x108E;
        private const string ListViewClassName = "SysListView32";
        private static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern bool EnumChildWindows(HandleRef hwndParent, EnumChildrenCallback lpEnumFunc, HandleRef lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(HandleRef hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern uint RealGetWindowClass(IntPtr hwnd, [Out] StringBuilder pszType, uint cchType);
        private delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);
        private HandleRef listViewHandle;

        public reLiveMain()
        {
            InitializeComponent();
        }
        /*
        public void openImage(string path)
        {
            pictureBox1.Image = new Bitmap(path);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex != -1)
                openImage(((imageData)fileList.Items[fileList.SelectedIndex]).filePath);
        }
        */
        private void directoryBrowse_Click(object sender, EventArgs e)
        {
            //fileList.Items.Clear();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                dirPath = fbd.SelectedPath;
                //populateFileList(dirPath);
                fileBrowser.Url = new Uri(dirPath);
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
            dirPath = explorerText.Text;
            if (dirPath != null)
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
                MessageBox.Show("Uploaded Album: " + currDate + " Successfully!");
                UpdateAlbumFeed();
            }
            else
            {
                MessageBox.Show("Please select a directory");
            }

        }
        
        private void launchSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://picasaweb.google.com/"); 
        }
        /*
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
         */

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
            System.IO.Directory.CreateDirectory(@userPictures + "\\reLive");
            //MessageBox.Show(userPictures + "\\reLive");
            fileBrowser.Navigate(userPictures + "\\reLive");
            //set file browser to view large icons
            FindListViewHandle();
            SendMessage(this.listViewHandle, LVM_SETVIEW, LV_VIEW_ICON, 0);
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
            this.mapLinkLabel.Hide();
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
            //enable map link only when date selected
            this.mapLinkLabel.Show();
            //enable changing of map browser url temporarily
            mapWindow.albumMap.AllowNavigation = true;
            mapWindow.albumMap.Url = new Uri("http://picasaweb.google.com/" + 
               this.user + "/" + entry.getPhotoExtensionValue("name") + "/photo#map");
        }

        private void mapLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mapWindow.ShowDialog();
        }

        private void fileBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            fileBrowser.Focus();
            explorerText.Text = fileBrowser.Url.LocalPath.ToString();
            //MessageBox.Show(fileBrowser.Url.ToString());
        }

        //file browser view stuff
        private void FindListViewHandle()
        {
            this.listViewHandle = NullHandleRef;

            EnumChildrenCallback lpEnumFunc = new EnumChildrenCallback(EnumChildren);
            EnumChildWindows(new HandleRef(this.fileBrowser, this.fileBrowser.Handle), lpEnumFunc, NullHandleRef);
        }

        private bool EnumChildren(IntPtr hwnd, IntPtr lparam)
        {
            StringBuilder sb = new StringBuilder(100);
            RealGetWindowClass(hwnd, sb, 100);
            if (sb.ToString() == ListViewClassName) // is this a windows list view?
            {
                // this is a windows list view control
                this.listViewHandle = new HandleRef(null, hwnd);
            }
            return true;
        }

        private void explorerText_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                dirPath = explorerText.Text;
                fileBrowser.Url = new Uri(this.explorerText.Text);
            }
        }
    }
}