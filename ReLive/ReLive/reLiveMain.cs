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

namespace ReLive
{
    public partial class reLiveMain : Form
    {
        private String googleAuthToken = null;
        private String user = null;
        private PicasaService picasaService = new PicasaService("ReLive");
        private PicasaFeed picasaFeed = null;
        private List<PicasaEntry> albumList = new List<PicasaEntry>();
        MapBrowser mapWindow = new MapBrowser();
        String userPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString();
        private bool calendarChanged = false;

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

        //google date workaround
        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalMilliseconds);
        } 

        private void createNewAlbum(string albumName, string desc, DateTime albumDate)
        {
            if(!checkAlbumExists(albumName))
            {
                Uri feedUri = new Uri(this.picasaFeed.Post);
                AlbumEntry newEntry = new AlbumEntry();
                newEntry.Title.Text = albumName;
                newEntry.Summary.Text = desc;
                PicasaEntry createdEntry = (PicasaEntry)picasaService.Insert(feedUri, newEntry);

                double timestamp = ConvertToUnixTimestamp(albumDate.ToUniversalTime());
                createdEntry.setPhotoExtension("timestamp", timestamp.ToString());
                createdEntry.Update();
            }
        }

        void UpdateStatus(int step, int value)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UpdateStatus), new object[] { step, value });
                return;
            }
            // Must be on the UI thread if we've got this far
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
                    MessageBox.Show("There was a problem reading your metadata file.\nVerify it exists and not currently in use and try again.");
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

                foreach (string tempString in dateArray)
                {
                    albumName += tempString;
                }
                DateTime albumDate = new DateTime(Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]), Int32.Parse(dateArray[2]));

                Uri postUri = new Uri(PicasaQuery.CreatePicasaUri(this.user, albumName));

                createNewAlbum(albumNameFull, desc, albumDate);
                //UpdateStatus(jpgFiles.Length + 1);

                bool validMeta = true;

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
                        PicasaEntry entry = this.picasaService.Insert(postUri, fileStream, "image/jpeg", fileStr) as PicasaEntry;

                        //parse and add metadata
                        if (validMeta)
                        {
                            try
                            {
                                line = sr.ReadLine();
                                data = line.Split(',');
                                entry.Location = new GeoRssWhere();
                                entry.Location.Latitude = Double.Parse(data[0]);
                                entry.Location.Longitude = Double.Parse(data[1]);
                                entry.Summary.Text = data[2]; //time caption below image
                                entry.Media.Keywords.Value = data[3] + "," + data[4]; //tags for face, halo

                                entry.Update();
                            }
                            catch (NullReferenceException)
                            {
                                MessageBox.Show("Metadata file may not have been complete!");
                                validMeta = false;
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
            if (this.googleAuthToken == null)
            {
                GoogleLogin loginDialog = new GoogleLogin(new PicasaService("reLive"));
                loginDialog.ShowDialog();

                this.googleAuthToken = loginDialog.AuthenticationToken;
                this.user = loginDialog.User;

                if (this.googleAuthToken == null)
                    MessageBox.Show("You will not be able to access your web albums without logging in!");
                else
                {
                    picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                    try
                    {
                        Invoke(new MethodInvoker(UpdateAlbumFeed));
                        //UpdateAlbumFeed();
                    }
                    catch (Google.GData.Client.GDataRequestException)
                    {
                        MessageBox.Show("You need to add the Picasaweb Service:\nLogin through your web browser and accept the terms of service.");
                        System.Diagnostics.Process.Start("www.picasaweb.google.com");
                        this.googleAuthToken = null;
                        login();
                    }
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

            //load previous config from SD card
            loadCurrentConfig();
        }

        private void loadCurrentConfig()
        {
            string memCardPath = findSDPath();
            if (memCardPath == "")
                return;
            //array for iteratign through form controls
            Control[] configArray = { delayBox, distanceBox, faceCheck, haloCheck, haloDescription, latBox, lngBox, haloDistanceBox };
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

            string input = sr.ReadLine();
            string[] inputArray;
            //parse file for details and load into form
            if (input != null)
            {
                inputArray = input.Split(',');

                for (int value = 0; value < inputArray.Length; value++)
                {
                    if (value == 2 || value == 3) //special cases for checkboxes
                    {
                        ((CheckBox)configArray[value]).Checked = inputArray[value] == "True";
                        if (value == 3 && inputArray[value] == "False")
                            return;
                    }
                    configArray[value].Text = inputArray[value];
                }
                sr.Close();
            }
        }

        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.albumList.Clear();
            albumCalendar.BoldedDates = null;
            this.AlbumPicture.Image = null;
            this.mapLinkLabel.Hide();

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
            calendarUpdate();
        }

        private void calendarUpdate()
        {
            this.AlbumPicture.Image = null;
            this.mapLinkLabel.Hide();
            //this.AlbumInspector.SelectedObject = null;

            foreach (PicasaEntry entry in this.albumList)
            {
                if (this.albumCalendar.SelectionStart.ToShortDateString().Equals(entry.Published.ToShortDateString()))
                    setSelection(entry);
            }
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
            Stream stream = this.picasaService.Query(new Uri(thumb.Attributes["url"] as string));
            this.AlbumPicture.Image = new Bitmap(stream);
            //this.AlbumInspector.SelectedObject = new AlbumAccessor(entry);
            this.Cursor = Cursors.Default;
            //enable map link only when date selected
            this.mapLinkLabel.Show();
            //enable changing of map browser url temporarily
            mapWindow.albumMap.AllowNavigation = true;
            mapWindow.albumMap.Navigate("http://picasaweb.google.com/" + this.user + "/" + entry.getPhotoExtensionValue("name") + "/photo#map");
        }

        private void mapLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mapWindow.ShowDialog();
        }

        private void fileBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            fileBrowser.Focus();
            explorerText.Text = fileBrowser.Url.LocalPath.ToString();
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
            //hitting enter loads new address
            if (e.KeyChar == (char)13)
            {
                explorerText.Text = explorerText.Text;
                fileBrowser.Url = new Uri(this.explorerText.Text);
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

                if (reliveCnt != 1)
                {
                    //string msg = "Please choose from the list below, the removable to be used. \n";
                    SelectDrive selectWin = new SelectDrive();

                    for (int i = 0; i < rootNum; i++)
                    {
                        string nextMsg = "Drive #" + (int)(i + 1) + ". " + allRemNames[i] + " " + allRemovables[i] + "\n";
                        selectWin.comboBox1.Items.Add(nextMsg);
                    }
                    
                    selectWin.ShowDialog(this); //show selectDrive window with this as parent
                    if (!selectWin.noChoice)
                    {
                        memCardPath = allRemovables[selectWin.choice];
                        driveSelected = true;
                    }
                    else driveSelected = false;
                    //MessageBox.Show(memCardPath + " was chosen to be the used drive.  \nPlease wait for directories to be set up");
                }
                
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
                        //MessageBox.Show(destFile.Name + " exists..skipping.. Please delete to upload.");
                        fileExists = true;
                    }
                }
                
                if (!fileExists)
                {
                    //create the path to where this file should be in destdir
                    tmppath = Path.Combine(destdir, file.Name);

                    //MessageBox.Show("Copying " + tmppath);
                    //copy file to dest dir
                    file.CopyTo(tmppath, true);
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
                tmppath = Path.Combine(destdir, subdir.Name);

                //recursively call this function over and over again
                //with each new dir.
                fileCopy(subdir.FullName, tmppath, recursive);
            }

            //cleanup
            dirs = null;
            dir = null; 
        }

        private void copySubDirs()
        {
            string path = @userPictures + "\\reLive";
            //String msg = "Copying Subdirectories: ";
            string[] subDirs = Directory.GetDirectories(memCardPath);
            /*
            foreach (string subDir in subDirs)
            {
                msg = msg + subDir + "\n";
            }
            MessageBox.Show(msg);
             */
            fileCopy(memCardPath, path, true);  //make third parameter true for recursive copy
            Invoke(new MethodInvoker(resetSync));
            MessageBox.Show("Sync complete!");
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
                if (memCardPath == "") return;
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

                //string output = p.StandardOutput.ReadToEnd();
                //string output1 = output.Substring(0, 115);
                //int nextStart = output.IndexOf("nitializ");
                //string output2 = output.Substring(2030, 2050);

                p.Close();

                MessageBox.Show(pathNoSlash + " formatted to Fat16. \n Format Completed. \n Please wait awhile for directories to be set up.");

                ThreadStart job = new ThreadStart(memCardDirSetup);
                Thread thread = new Thread(job);
                thread.Start();
            }
        }

        private void memCardDirSetup()
        {
            string[] day = new string[31];
            DateTime currTime = DateTime.Now;
            string msg = "";
            day[0] = currTime.ToString("yyyy-MM-dd");
            for (int i = 0; i < 31; i++)
            {
                day[i] = memCardPath + day[i];
                if (i != 30)
                {
                    day[i + 1] = currTime.AddDays(i).ToString("yyyy-MM-dd");
                }
                msg = msg + day[i] + "\n";
            }
            //            MessageBox.Show(msg);

            for (int i = 0; i < 31; i++)
            {

                Directory.CreateDirectory(day[i]);
                //                MessageBox.Show(day[i] + " directory created");
                for (int j = 0; j < 24; j++)
                {
                    Directory.CreateDirectory(day[i] + "\\" + j);
                    //                    MessageBox.Show(day[i] + "\\" + "hour" + j + 1 + " created");

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
            }
            catch (FormatException)
            {
                distanceBox.Text = "";
            }
        }

        private void haloDistanceBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Double.Parse(haloDistanceBox.Text + "0");
            }
            catch (FormatException)
            {
                haloDistanceBox.Text = "";
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
            //c# geocoding example
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

            config.WriteLine(delayBox.Text + "," + distanceBox.Text + "," + faceCheck.Checked + "," +
                haloCheck.Checked + "," + haloDescription.Text + "," + latBox.Text + "," + lngBox.Text + "," + haloDistanceBox.Text);
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
    }
}