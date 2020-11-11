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
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Path could not be found!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("This path has already been readed!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReadAndCompare()
        {
            FilesAfter.Clear();
            try
            {
                string str = "";
                int i = 0;

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
                        i++;
                    }
                    else if (!(FirstFiles[pair.Key] == pair.Value))
                    {
                        str += pair.Key + "\n";
                        i++;
                    }
                }

                Output.Text = str;
                after.Text = FilesAfter.Count.ToString();
                OutputCount.Text = i.ToString();
            }
            catch (System.UnauthorizedAccessException)
            {

            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("Path could not be found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            FirstFiles.Clear();
            OutputCount.Text = "0";
            Output.Text = "";
            first.Text = "0";
            after.Text = "0";
        }
    }
}
