using System;
using System.Threading;
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
using Google.GData.Extensions.Location;
using System.Runtime.InteropServices;

namespace reLive
{
    public partial class reLiveMain : Form
    {
        private String googleAuthToken = null;
        private String user = null;
        private PicasaService picasaService = new PicasaService("ReLive");
        private PicasaFeed picasaFeed = null;
        private List<PicasaEntry> albumList = new List<PicasaEntry>();
        //MapBrowser mapWindow = new MapBrowser();
        String userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
        private bool calendarChanged = false;
        private string curAlbum;
        private string mapUri;

        //file browser view settings
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

        //removable drive information
        private bool driveSelected = false;
        private string memCardPath = "";
        private string[] allRemovables = new string[10];
        private string[] allRemNames = new string[10];

        //threading ui stuff
        delegate void StringParameterDelegate(int step, int value);
        readonly object stateLock = new object();

        public reLiveMain()
        {
            InitializeComponent();
        }

        private void directoryBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                explorerText.Text = fbd.SelectedPath;
                fileBrowser.Url = new Uri(explorerText.Text);
            }
            fbd.Dispose();
        }
          
        private bool checkAlbumExists(string albumName)
        {
            bool albumExists = false;
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(this.user));
            PicasaFeed feed = null;

            try
            {
                feed = picasaService.Query(query);
            }
            catch(Exception)
            {
                return false;
            }

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
            PicasaFeed feed =null;

            try
            {
                feed = picasaService.Query(query);
            }
            catch (Exception)
            {
                return true;
            }

            foreach (PicasaEntry entry in feed.Entries)
            {
                if(entry.Title.Text.Equals(fileName)) fileExists = true;
            }
            return fileExists;
        }

        //google date workaround
        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalMilliseconds);
        } 

        private bool createNewAlbum(string albumName, string desc, DateTime albumDate)
        {
            if(!checkAlbumExists(albumName))
            {
                try
                {
                    Uri feedUri = new Uri(this.picasaFeed.Post);
                    AlbumEntry newEntry = new AlbumEntry();
                    newEntry.Title.Text = albumName;
                    newEntry.Summary.Text = desc;
                    double timestamp = ConvertToUnixTimestamp(albumDate.ToUniversalTime());
                    newEntry.setPhotoExtension("timestamp", timestamp.ToString());
                    PicasaEntry createdEntry = (PicasaEntry)picasaService.Insert(feedUri, newEntry);
                    return false;
                }
                catch (Exception)
                {
                    return true;
                }
            }
            return false;
        }

        void UpdateStatus(int step, int value)
        {
            if (InvokeRequired)
            {
                //We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UpdateStatus), new object[] { step, value });
                return;
            }
            //Must be on the UI thread if we've got this far
            uploadProgress.Step = (uploadProgress.Maximum / value);
            uploadProgress.Visible = true;
            progressLabel.Visible = true;
            uploadProgress.PerformStep();
            progressLabel.Text = "Progress: " + step.ToString() + "/" + value.ToString();
        }

        private void upload()
        {
            if (this.googleAuthToken == null)
                login();
            if (this.googleAuthToken == null) //if service check fails, token set to null so need to check again
                return;

            if (explorerText.Text != null)
            {
                StreamReader sr = null;
                try
                {
                    sr = File.OpenText(explorerText.Text + "\\metadata.txt");
                }
                catch (Exception)
                {
                    MessageBox.Show("There was a problem reading your metadata file.\nVerify you opened an album and try again!");
                    Invoke(new MethodInvoker(resetUpload));
                    return;
                }

                DirectoryInfo dir = new DirectoryInfo(explorerText.Text);
                FileInfo[] jpgFiles = dir.GetFiles("*.jpg");
                DateTime currTime = DateTime.Now;
                string albumNameFull = dir.Name.ToString();
                string currDate = currTime.ToString("yyyy-MM-dd");
                string desc = "Album Added: " + currDate;
                string[] dateArray = albumNameFull.Split('-');
                String albumName = "";
                int currentStep = 0;
                bool validMeta = true;

                foreach (string tempString in dateArray)
                {
                    albumName += tempString;
                }
                DateTime albumDate = new DateTime(Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]), Int32.Parse(dateArray[2]), 12, 0, 0, DateTimeKind.Utc);
                Uri postUri = new Uri(PicasaQuery.CreatePicasaUri(this.user, albumName));

                if (createNewAlbum(albumNameFull, desc, albumDate))
                {
                    Invoke(new MethodInvoker(resetUpload));
                    MessageBox.Show("Please check your internet connection and try again.");
                    return;
                }

                foreach (FileInfo file in jpgFiles)
                {
                    string fileStr = file.FullName;
                    string line = null;
                    string[] data = null;
                    currentStep++;

                    UpdateStatus(currentStep, jpgFiles.Length);

                    if (!checkFileExists(file.Name, albumName))
                    {
                        FileStream fileStream = file.OpenRead();
                        PicasaEntry entry = null;

                        try
                        {   
                            entry = this.picasaService.Insert(postUri, fileStream, "image/jpeg", fileStr) as PicasaEntry;
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Please check your internet connection and try again.");
                            Invoke(new MethodInvoker(resetUpload));
                            return;
                        }

                        //parse and add metadata
                        if (validMeta)
                        {
                            try
                            {
                                line = sr.ReadLine();
                                data = line.Split(',');
                                entry.Location = new GeoRssWhere();
                                entry.Location.Latitude = Double.Parse(data[0]) / 1000000;
                                entry.Location.Longitude = Double.Parse(data[1]) / 1000000;
                                entry.Summary.Text = data[2]; //time caption below image
                                entry.Media.Keywords.Value = data[3]; //tags for halo description

                                entry.Update();
                            }
                            catch (NullReferenceException)
                            {
                                MessageBox.Show("Metadata file may not have been complete!");
                                validMeta = false;
                            }

                            catch (Google.GData.Client.GDataRequestException ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                                MessageBox.Show("There was a problem uploading your metadata to google!");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                MessageBox.Show("There was a problem with your metadata file (index out of range):\n" + line);
                            }
                        }
                    }
                    else
                    {
                        line = sr.ReadLine(); //move line forward if image already uploaded
                    }
                }
                Invoke(new MethodInvoker(resetUpload));

                MessageBox.Show("Uploaded Album: " + albumNameFull + " Successfully!");
                calendarChanged = true;
                Invoke(new MethodInvoker(UpdateAlbumFeed));
            }
            else
            {
                MessageBox.Show("Please select a directory");
            }
        }

        void resetUpload()
        {
            uploadDir.Enabled = true;
            uploadProgress.Visible = false;
            progressLabel.Visible = false;
            uploadProgress.Value = 0;
        }

        void resetSync()
        {
            retrieveSD.Enabled = true;
            formatSD.Enabled = true;
        }

        private void uploadDir_Click(object sender, EventArgs e)
        {
            uploadDir.Enabled = false;
            Thread thread = new Thread(new ThreadStart(upload));
            thread.IsBackground = true;
            thread.Start();
        }

        private void login()
        {
            if (MessageBox.Show("This product is not intended to be used for illegal or malicious reasons.\n\nThis device must be used where it is visible to the public\n\nDo you agree?", "User Agreement", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                this.Close();
            }
            if (this.googleAuthToken == null)
            {  
                GoogleLogin loginDialog = new GoogleLogin(new PicasaService("reLive"));
                loginDialog.ShowDialog();

                this.googleAuthToken = loginDialog.AuthenticationToken;
                this.user = loginDialog.User;

                albumCalendar.BoldedDates = null;
                albumList.Clear();

                if (this.googleAuthToken == null)
                    MessageBox.Show("You will not be able to access your web albums without logging in!");
                else
                {
                    picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                    Invoke(new MethodInvoker(UpdateAlbumFeed));
                }
            }
        }

        private void reLiveMain_Load(object sender, EventArgs e)
        {
            login();
            Directory.CreateDirectory(@userPictures + "\\reLive");
            fileBrowser.Navigate(userPictures + "\\reLive");

            //set file browser to view large icons
            FindListViewHandle();
            SendMessage(this.listViewHandle, LVM_SETVIEW, LV_VIEW_ICON, 0);

            //center calender (fix for different size for xp/vista)
            int x = (calendarPanel.Width - albumCalendar.Width) / 2;
            int y = albumCalendar.Location.Y;
            albumCalendar.Location = new Point(x, y);

            //load previous config from SD card
            loadCurrentConfig();
        }

        private void loadCurrentConfig()
        {
            string memCardPath = findSDPath();
            if (memCardPath == "")
                return;
            //array for iteratign through form controls
            Control[] configArray = { delayBox, distanceBox, schedulerCheck, startTime, startTime, endTime, endTime, haloCheck, haloDescription, latBox, lngBox, haloDistanceBox };
            StreamReader sr;

            if (File.Exists(memCardPath + "\\config.txt"))
            {
                sr = File.OpenText(memCardPath + "\\config.txt");
            }
            else
            {
                MessageBox.Show("No config.txt file detected on SD card.");
                return;
            }

            try
            {
                string input = sr.ReadLine();
                string[] inputArray;
                //parse file for details and load into form
                if (input != null)
                {
                    inputArray = input.Split(',');
                    if (inputArray[1] != "0")
                        distanceCheck.Checked = true;
                    for (int value = 0; value < inputArray.Length; value++)
                    {
                        if (value == 2 || value == 7) //special cases for halo and scheduler check
                        {
                            ((CheckBox)configArray[value]).Checked = inputArray[value] == "True";
                            if (value == 2 && inputArray[value] == "False")
                            {
                                value = 6;
                                continue;
                            }
                            if (value == 7 && inputArray[value] == "False")
                                return;
                        }
                        else if (value == 3 || value == 5)
                        {
                            if (value == 3)
                            {
                                startTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Int32.Parse(inputArray[value]), Int32.Parse(inputArray[value + 1]), 0);
                                value += 1;
                                continue;
                            }
                            if (value == 5)
                            {
                                endTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Int32.Parse(inputArray[value]), Int32.Parse(inputArray[value + 1]), 0);
                                value += 1;
                                continue;
                            }
                        }
                        else
                            configArray[value].Text = inputArray[value];
                    }
                    sr.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem reading your config file, create and save a new one before using your camera!");
            }
        }

        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.albumList.Clear();
            albumCalendar.BoldedDates = null;
            this.AlbumPicture.Image = null;
            this.mapLinkLabel.Hide();
            this.albumLabel.Hide();

            query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));

            try
            {
                this.picasaFeed = this.picasaService.Query(query);
            }
            catch (Google.GData.Client.GDataRequestException)
            {
                MessageBox.Show("You need to add the Picasaweb Service:\nLogin through your web browser and accept the terms of service."
                    + "\nIn addition, please check your internet connection and try again!");
                System.Diagnostics.Process.Start("www.picasaweb.google.com");
                this.googleAuthToken = null;
                login();
                return;
            }
            
            if (this.picasaFeed != null && this.picasaFeed.Entries.Count > 0)
            {
            foreach (PicasaEntry entry in this.picasaFeed.Entries)
            {
                albumList.Add(entry);
                albumCalendar.AddBoldedDate(entry.Published); //adds album dates to calendar as bold entries
            }
            }
            this.albumCalendar.UpdateBoldedDates();
            calendarUpdate();
        }

        private void calendarUpdate()
        {
            this.AlbumPicture.Image = null;
            this.AlbumPicture.ImageLocation = null;
            this.mapLinkLabel.Hide();
            this.albumLabel.Hide();

            foreach (PicasaEntry entry in this.albumList)
            {
                if (this.albumCalendar.SelectionStart.ToShortDateString().Equals(entry.Published.ToShortDateString()))
                    setSelection(entry);
            }
            albumCalendar.Update();//refresh gui element
        }

        private void albumCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (calendarChanged)
            {
                UpdateAlbumFeed();
                calendarChanged = false;
            }
            else
                calendarUpdate();
        }

        private void setSelection(PicasaEntry entry)
        {
            this.Cursor = Cursors.WaitCursor;
            MediaThumbnail thumb = entry.Media.Thumbnails[0];

            this.AlbumPicture.ImageLocation = thumb.Attributes["url"] as string;
            /*
              
            try
            {
                Stream stream = this.picasaService.Query(new Uri(thumb.Attributes["url"] as string));
            }
            catch (Google.GData.Client.GDataRequestException)
            {
                //MessageBox.Show("There was a problem downloading the album thumbnail from Google!");
                this.AlbumPicture.Image = this.AlbumPicture.ErrorImage;
            }
                */

            this.Cursor = Cursors.Default;
            //enable map link only when date selected
            this.mapLinkLabel.Show();
            this.albumLabel.Show();

            //enable changing of map browser url temporarily
            //mapWindow.albumMap.AllowNavigation = true;
            curAlbum = entry.getPhotoExtensionValue("name");
            mapUri = "http://picasaweb.google.com/" + this.user + "/" + entry.getPhotoExtensionValue("name") + "/photo#map";
        }

        private void mapLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //mapWindow.albumMap.Url = new Uri(mapUri);
            System.Diagnostics.Process.Start(mapUri);
            //mapWindow.ShowDialog();
        }

        private void fileBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            fileBrowser.Focus();
            explorerText.Text = fileBrowser.Url.LocalPath.ToString();
        }

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
            //hitting enter loads new address
            if (e.KeyChar == (char)13)
            {
                try
                {
                    fileBrowser.Url = new Uri(this.explorerText.Text);
                }
                catch (Exception)
                {
                    explorerText.Text = fileBrowser.Url.LocalPath.ToString();
                    MessageBox.Show("Please make sure you are trying to access a valid address and try again!");
                }
            }
        }

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.googleAuthToken = null;
            login();
        }

        private void picasaLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://picasaweb.google.com/"); 
        }

        private string findSDPath()
        {
            if (!driveSelected)
            {
                memCardPath = "";
                int rootNum = 0;
                int reliveCnt = 0;
                DriveInfo[] allDrives = DriveInfo.GetDrives();  //get a list of all drives
                foreach (DriveInfo drvInfo in allDrives)        //loop through all drives
                {
                    DirectoryInfo di = drvInfo.RootDirectory;
                    if (drvInfo.DriveType.Equals(DriveType.Removable) && drvInfo.IsReady && drvInfo.VolumeLabel.Equals("RELIVE"))
                    {
                        memCardPath = di.FullName;
                        reliveCnt++;
                    }
                    else if (drvInfo.DriveType.Equals(DriveType.Removable) && !di.FullName.Equals("A:\\") && drvInfo.IsReady)
                    {
                        allRemovables[rootNum] = di.FullName;
                        allRemNames[rootNum] = drvInfo.VolumeLabel;
                        rootNum++;
                    }
                }

                if (reliveCnt != 1 && rootNum != 0)
                {
                    SelectDrive selectWin = new SelectDrive();

                    for (int i = 0; i < rootNum; i++)
                    {
                        string nextMsg = "Drive #" + (int)(i + 1) + ". " + allRemNames[i] + " " + allRemovables[i] + "\n";
                        selectWin.deviceBox.Items.Add(nextMsg);
                    }
                    
                    selectWin.ShowDialog(this); //show selectDrive window with this as parent
                    if (!selectWin.noChoice)
                    {
                        memCardPath = allRemovables[selectWin.choice];
                        driveSelected = true;
                    }

                    else 
                        driveSelected = false;
                }
                else if(reliveCnt == 0 && rootNum == 0)
                    MessageBox.Show("No available drives were found.  One will need to be inserted in order to sync, save config, or format a drive.");
            }
            return memCardPath;
        }

        private void fileCopy(string srcdir, string destdir, bool recursive)
        {
            DirectoryInfo dir;
            DirectoryInfo ddir;
            FileInfo[] files;
            FileInfo[] destFiles;
            DirectoryInfo[] dirs;
            string tmppath;
            bool fileExists = false;

            //determine if the destination directory exists, if not create it
            if (!Directory.Exists(destdir))
            {
                Directory.CreateDirectory(destdir);
            }

            dir = new DirectoryInfo(srcdir);
            ddir = new DirectoryInfo(destdir);

            //if the source dir doesn't exist, throw
            if (!dir.Exists)
            {
                throw new ArgumentException("source dir doesn't exist -> " + srcdir);
            }

            //get all files in the current dir
            files = dir.GetFiles();
            destFiles = ddir.GetFiles();

            //loop through each file
            foreach (FileInfo file in files)
            {
                foreach (FileInfo destFile in destFiles)
                {
                    if (file.Name.Equals(destFile.Name) && !file.Extension.Equals(".txt"))
                    {
                        fileExists = true;
                    }
                }
                
                if (!fileExists)
                {
                    //create the path to where this file should be in destdir
                    tmppath = Path.Combine(destdir, file.Name);

                    //copy file to dest dir
                    if(!file.Name.Equals("config.txt"))
                    {
                      file.CopyTo(tmppath, true);
                      file.Delete();
                    }
                }
                else fileExists = false;
            }

            //cleanup
            destFiles = null;
            files = null;

            //if not recursive, all work is done
            if (!recursive)
            {
                return;
            }

            //otherwise, get dirs
            dirs = dir.GetDirectories();

            //loop through each sub directory in the current dir
            foreach (DirectoryInfo subdir in dirs)
            {
                //create the path to the directory in destdir
                tmppath = destdir;
                //recursively call this function over and over again
                //with each new dir.
                fileCopy(subdir.FullName, tmppath, recursive);
            }

            //cleanup
            dirs = null;
            dir = null; 
        }

        private string getSyncDate()
        {
            SyncDate dateForm = new SyncDate();
            dateForm.ShowDialog();

            return dateForm.getDate().ToString("yyyy-MM-dd");
        }

        private void copySubDirs()
        {
            string home = @userPictures + "\\reLive";
            
            DateTime currTime = DateTime.Now;
            string day = getSyncDate();
            //create folder for current date
            string path = home + "\\" + day;
            if (Directory.Exists(path))
            {
                MessageBox.Show("Album already created, please select a different date.");
                copySubDirs();
                return;
            }
            Directory.CreateDirectory(path);
            //copy metadata.txt  
            try
            {
                File.Copy(memCardPath + "\\metadata.txt", path + "\\metadata.txt");
                string[] subDirs = Directory.GetDirectories(memCardPath);

                fileCopy(memCardPath, path, true);  //make third parameter true for recursive copy
                Invoke(new MethodInvoker(resetSync));
                MessageBox.Show("Sync complete!");
            }
            //If the file copy fails then catch will gain control of the method
            catch (Exception)
            {
                MessageBox.Show("No metadata.txt file found on SD card.  Please include one before a sync.");
                Invoke(new MethodInvoker(resetSync));
            }
        }

        private void retrieveSD_Click(object sender, EventArgs e)
        {
            string defPath = @userPictures + "\\reLive";

            if (memCardPath == "")
            {
                MessageBox.Show("Sorry, but no SD card was detected in the drive.\nPlease select the memory card's drive.");
                memCardPath = findSDPath();
                if (memCardPath == "") return;
            }

            MessageBox.Show("Card Drive detected to be: " + memCardPath + "\nCopying contents to: " + defPath);
            retrieveSD.Enabled = false;
            formatSD.Enabled = false;
            Thread thread = new Thread(new ThreadStart(copySubDirs));
            thread.IsBackground = true;
            thread.Start();
        }

        private void formatSD_Click(object sender, EventArgs e)
        {
            if (memCardPath == "")
            {
                MessageBox.Show("No memory card found, please select a card to be formatted.");
                memCardPath = findSDPath(); //reprompt for SD card
                if (memCardPath == "")
                    return;
            }
            FileInfo memCardInfo = new FileInfo(memCardPath);
            if (memCardInfo.IsReadOnly)
            {
                MessageBox.Show("Current card directory is set to read only.  Please change this and retry the format.");
                return;
            }

            //take off the '\'
            string pathNoSlash = memCardPath.Substring(0, 2);
            
            if (MessageBox.Show("Are you sure you want to format your SD Card?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //set dos process/command
                System.Diagnostics.ProcessStartInfo sinf = new System.Diagnostics.ProcessStartInfo("cmd", @"/c format " + pathNoSlash + " /FS:FAT /V:RELIVE /X /Q");
                sinf.RedirectStandardInput = true;
                sinf.RedirectStandardOutput = true;

                //don't popup with a dos window
                sinf.UseShellExecute = false;
                sinf.CreateNoWindow = true;

                //create and start process
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = sinf;
                p.Start();

                //press enter in the cmd process (it says press enter when ready)
                StreamWriter myStreamWriter = p.StandardInput;
                myStreamWriter.WriteLine();

                myStreamWriter.Close();
                p.Close();

                MessageBox.Show(pathNoSlash + " formatted to Fat16. \n Format Completed. \n Please wait awhile for directories to be set up.");

                ThreadStart job = new ThreadStart(memCardDirSetup);
                Thread thread = new Thread(job);
                thread.Start();
                
            }
        }

        private void memCardDirSetup()
        {
            System.Threading.Thread.Sleep(2000);
            for (int j = 0; j < 24; j++)
            {
                try
                {
                    Directory.CreateDirectory(memCardPath + j.ToString());
                }
                catch (Exception)
                {
                    j -= 1;
                }
            }
            MessageBox.Show("Directories Created");
            //need to catch unauthorizedaccessexception
        }

        private void distanceBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double.Parse(distanceBox.Text + "0");
                if (Double.Parse(distanceBox.Text) < 10 && Double.Parse(distanceBox.Text) != 0)
                    distanceBox.Text = "10";
            }
            catch (FormatException)
            {
                distanceBox.Text = "10";
            }
        }

        private void haloDistanceBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double.Parse(haloDistanceBox.Text + "0");
                if (Double.Parse(haloDistanceBox.Text) < 10)
                    haloDistanceBox.Text = "10";
            }
            catch (FormatException)
            {
                haloDistanceBox.Text = "10";
            }
        }

        private void zipBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(zipBox.Text);
            }
            catch (FormatException)
            {
                zipBox.Text = "";
            }
        }

        private void geoCode_Click(object sender, EventArgs e)
        {
            string lat = "";
            string lng = "";
            string address = streetBox.Text + ", " + cityBox.Text + ", " + stateBox.Text + ", " + zipBox.Text;
 
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                string page = client.DownloadString("http://maps.google.com/maps?q=" + address);
                int begin = page.IndexOf("markers:");
                string str = page.Substring(begin);
                int end = str.IndexOf(",image:");
                str = str.Substring(0, end);

                //Parse out Latitude
                lat = str.Substring(str.IndexOf(",lat:") + 5);
                lat = lat.Substring(0, lat.IndexOf(",lng:"));
                //Parse out Longitude
                lng = str.Substring(str.IndexOf(",lng:") + 5);
                client.Dispose();
            }

            catch (Exception)
            {
                MessageBox.Show("An Error Occured Loading Geocode!\nCheck that a valid address has been entered.", "An Error Occured Loading Geocode!");
            }

            latBox.Text = lat;
            lngBox.Text = lng;
        }

        //validate that all camera settings have been set
        private bool validateSettings()
        {
            string memCardPath = findSDPath();
            if (memCardPath.Equals("")) return false;
            string message = "";
            string[] description = haloDescription.Text.Split(',');
            haloDescription.Text = "";
            foreach(string word in description)
            {
                haloDescription.Text += word;
            }

            if (memCardPath == "")
            {
                MessageBox.Show("Sorry, but no SD card was detected in the drive.\nPlease insert your memory card and try again.");
                return false;
            }
            if(delayBox.Text == "")
            {
                message += "Please specify a minimum time between pictures!\n";
            }
            if(distanceBox.Text == "")
            {
                message += "Please specify a minimum distance!\n";
            }
            if ((latBox.Text == "" || lngBox.Text == "") && haloCheck.Checked)
            {
                message += "Search for a GPS location, or disable the location halo.";
            }
            if (message == "")
                return true;

            MessageBox.Show(message);
            return false;
        }

        private void writeConfig_Click(object sender, EventArgs e)
        {
            if (!validateSettings())
                return;

            string memCardPath = findSDPath();
            if (memCardPath.Equals("")) return;

            TextWriter config;
            try
            {
                config = new StreamWriter(memCardPath + "\\config.txt", false);
            }
            catch(IOException)
            {
                MessageBox.Show("Problem accessing config file, make sure it is not open and try again!");
                return;
            }

            config.WriteLine(delayBox.Text + "," + distanceBox.Text + "," + schedulerCheck.Checked + ","
                + startTime.Value.Hour + "," + startTime.Value.Minute + "," + endTime.Value.Hour + "," 
                + endTime.Value.Minute + "," + haloCheck.Checked + "," + haloDescription.Text + "," 
                + latBox.Text + "," + lngBox.Text + "," + haloDistanceBox.Text);
            config.Close();

            MessageBox.Show("Config written to " + memCardPath + "config.txt");
        }

        private void haloCheck_CheckedChanged(object sender, EventArgs e)
        {
            haloSearchGroup.Visible = !haloSearchGroup.Visible;
            gpsGroup.Visible = !gpsGroup.Visible;
        }

        private void viewHalo_Click(object sender, EventArgs e)
        {
            if ((latBox.Text != "") && (lngBox.Text != ""))
                System.Diagnostics.Process.Start("http://maps.google.com/maps?q=" + latBox.Text + "," + lngBox.Text + "&t=h");
            else
                MessageBox.Show("Search for a location first!");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            fileBrowser.GoBack();
        }

        private void albumLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://picasaweb.google.com/" + this.user + "/" + curAlbum);
        }

        private void schedulerCheck_CheckedChanged(object sender, EventArgs e)
        {
            schedulerGroup.Visible = !schedulerGroup.Visible;
            //set endTime an hour later than start if invalid
            if (startTime.Value >= endTime.Value)
                endTime.Value = startTime.Value.AddHours(1);
            //set startTime an hour earlier than end if invalid
            if (endTime.Value <= startTime.Value)
                startTime.Value = endTime.Value.AddHours(-1);
        }

        private void startTime_ValueChanged(object sender, EventArgs e)
        {
            //set endTime an hour later than start if invalid
            if (startTime.Value >= endTime.Value)
                endTime.Value = startTime.Value.AddHours(1);
        }

        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            //set startTime an hour earlier than end if invalid
            if (endTime.Value <= startTime.Value)
                startTime.Value = endTime.Value.AddHours(-1);
        }

        private void distanceCheck_CheckedChanged(object sender, EventArgs e)
        {
            distanceBox.Visible = !distanceBox.Visible;
            meterLabel.Visible = !meterLabel.Visible;
            if (!distanceCheck.Checked)
                distanceBox.Text = "0";
        }

        private void delayBox_Leave(object sender, EventArgs e)
        {
            try
            {
                Double.Parse(delayBox.Text + "0");
                if (Double.Parse(delayBox.Text) < 1.5)
                    delayBox.Text = "1.5";
            }
            catch (FormatException)
            {
                delayBox.Text = "1.5";
            }
        }
    }
}