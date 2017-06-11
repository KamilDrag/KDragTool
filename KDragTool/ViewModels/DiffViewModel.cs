using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }
        public event PropertyChangedEventHandler PropertyChanged = null;

        private string firstFile = "";
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
            }
        }

        private string secondFile = "";
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
            }
        }

        private string leftDiff = "";
        public string LeftDiff
        {
            get
            {
                return leftDiff;
            }

            set
            {
                leftDiff = value;
            }
        }

        private string rightDiff = "";
        public string RightDiff
        {
            get
            {
                return rightDiff;
            }

            set
            {
                rightDiff = value;
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
