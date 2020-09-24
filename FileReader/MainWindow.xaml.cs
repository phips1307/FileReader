using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FileReader
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, DateTime> FirstFiles = new Dictionary<string, DateTime>();
        Dictionary<string, DateTime> FilesAfter = new Dictionary<string, DateTime>();


        public MainWindow()
        {
            InitializeComponent();
        }

        public void Read()
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(Path.Text);
                FileInfo[] Files = info.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in Files)
                {
                    FirstFiles.Add(file.FullName, file.LastWriteTime);
                }
                first.Text = FirstFiles.Count.ToString();
            }
            catch (System.UnauthorizedAccessException)
            {

            }
        }

        private void ReadAndCompare()
        {
            FilesAfter.Clear();
            try
            {
                string str = "";

                DirectoryInfo info = new DirectoryInfo(Path.Text);
                FileInfo[] Files = info.GetFiles("*.*", SearchOption.AllDirectories);
                foreach (FileInfo file in Files)
                {
                    FilesAfter.Add(file.FullName, file.LastWriteTime);
                }

                foreach (KeyValuePair<string, DateTime> pair in FilesAfter)
                {
                    if (!FirstFiles.ContainsKey(pair.Key))
                    {
                        str += pair.Key + "\n";
                    }
                    else if (!(FirstFiles[pair.Key] == pair.Value))
                    {
                        str += pair.Key + "\n";
                    }
                }

                Output.Text = str;
                after.Text = FilesAfter.Count.ToString();
            }
            catch (System.UnauthorizedAccessException)
            {

            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            ReadAndCompare();
        }
    }
}
