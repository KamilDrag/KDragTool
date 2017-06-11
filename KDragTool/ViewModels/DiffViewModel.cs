using KDragTool.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDragTool.ViewModels
{
    public class DiffViewModel : INotifyPropertyChanged
    {
        public DiffViewModel(string firstFile="", string secondFile="")
        {
            FirstFile = firstFile;
            SecondFile = SecondFile;
            
            OpenFirstFile = new RelayCommand(x => { FirstFile = OpenFile(); });
            OpenSecondFile = new RelayCommand(x => { SecondFile = OpenFile(); });
            ExploreToCommand = new RelayCommand(x => { ExploreTo(x.ToString()); });
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        public ICommand OpenFirstFile { get; set; }
        public ICommand OpenSecondFile { get; set; }
        public ICommand ExploreToCommand { get; set; }

        private string firstFile = string.Empty;
        public string FirstFile
        {
            get
            {
                return firstFile;
            }
            set
            {
                firstFile = value;
                OnPropertyChanged("FirstFile");
                OnUpdateFilePath();
            }
        }

        private string secondFile = string.Empty;
        public string SecondFile
        {
            get
            {
                return secondFile;
            }
            set
            {
                secondFile = value;
                OnPropertyChanged("SecondFile");
                OnUpdateFilePath();
            }
        }

        public ObservableCollection<TextLine> LeftDiff
        {
            get
            {
                return leftDiff;
            }

            set
            {
                leftDiff = value;
                OnPropertyChanged("LeftDiff");
            }
        }

        private ObservableCollection<TextLine> leftDiff;

        public ObservableCollection<TextLine> RightDiff
        {
            get
            {
                return rigthDiff;
            }

            set
            {
                rigthDiff = value;
                OnPropertyChanged("RightDiff");
            }
        }

        private ObservableCollection<TextLine> rigthDiff;



        virtual protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        
        private string OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = string.IsNullOrWhiteSpace(FirstFile) ?
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : FirstFile;
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : string.Empty;
        }

        private void OnUpdateFilePath()
        {
            if(File.Exists(FirstFile) && File.Exists(SecondFile))
            {
                LeftDiff = new ObservableCollection<TextLine>(Diff.MakeUnorderedLinesDiff(FirstFile, SecondFile));
                RightDiff = new ObservableCollection<TextLine>(Diff.MakeUnorderedLinesDiff(SecondFile, FirstFile));
            }
        }

        private void ExploreTo(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            
            string argument = "/select, \"" + path + "\"";
            Process.Start("explorer.exe", argument);
        }
    }
}
