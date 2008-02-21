using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace ReLive
{
    public partial class Form1 : Form
    {
        public Form1()
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
            string pathFolder = System.Windows.Forms.Application.StartupPath;
            string path = pathFolder + "\\GMap.html";  //HTML file must be in ReLive\ReLive\bin\Debug
            startIE(path);
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
    }
}