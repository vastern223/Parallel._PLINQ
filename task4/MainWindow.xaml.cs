using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //папка з готовим резюме є біля ехе
    {
        
        List<Resume> resumes = new List<Resume>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\Users";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                fileNameTB.Text = dialog.FileName;
                DirectoryInfo d = new DirectoryInfo(dialog.FileName);
                FileInfo[] Files = d.GetFiles("*.txt", SearchOption.AllDirectories);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {        
            if (fileNameTB.Text != "C:\\")
            {
                string[] fileArray = Directory.GetFiles(fileNameTB.Text, "*.txt", SearchOption.AllDirectories);

                for (int i = 0; i < fileArray.Count(); ++i)
                {
                    Parallel.Invoke(() => FileAnalysis(fileArray[i]));
                }
          
            }
        }

        public void FileAnalysis(string path)
        {
            lock (this)
            {
                string lineStr;
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                   
                    StreamReader streamReader = new StreamReader(fs);
                    char[] wordsSplit = new char[] { ' ' };
                    while (!streamReader.EndOfStream)
                    {
                        lineStr = streamReader.ReadLine();
                        string[] words = lineStr.Split(wordsSplit, StringSplitOptions.RemoveEmptyEntries);
                        resumes.Add(new Resume() { Name = words[0], Surname = words[1], NumberOfYearsOfExperience = int.Parse(words[2]), City = words[3], salaryRequirement = int.Parse(words[4]) });

                    }
                    
                }
              
            }


        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (fileNameTB.Text != "")
            {
                var Resume = resumes.AsParallel().AsOrdered().OrderByDescending(n => n.NumberOfYearsOfExperience).First();
                listMain.Items.Clear();
                listMain.Items.Add(Resume);
            }   
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (fileNameTB.Text != "")
            {
                var Resume = resumes.AsParallel().AsOrdered().OrderBy(n => n.NumberOfYearsOfExperience).First();
                listMain.Items.Clear();
                listMain.Items.Add(Resume);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (fileNameTB.Text != "")
            {
                var Resume = resumes.AsParallel().AsOrdered().OrderBy(n => n.salaryRequirement).First();
                listMain.Items.Clear();
                listMain.Items.Add(Resume);
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (fileNameTB.Text != "")
            {
                var Resume = resumes.AsParallel().AsOrdered().OrderByDescending(n => n.salaryRequirement).First();
                listMain.Items.Clear();
                listMain.Items.Add(Resume);
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (City.Text != "" && fileNameTB.Text != "")
            {
                listMain.Items.Clear();
                string city = City.Text;
                var Res = resumes.AsParallel().AsOrdered().Where(a => a.City == city);
                List<Resume> temp = Res.ToList();

                foreach (var item in temp)
                {
                    listMain.Items.Add(item);
                }
            }
        }
    }
}
