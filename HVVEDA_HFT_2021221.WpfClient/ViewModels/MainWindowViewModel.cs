using HVVEDA_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HVVEDA_HFT_2021221.WpfClient
{
    public  class MainWindowViewModel
    {
        public RestCollection<Teacher> Teachers { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public ICommand TeacherOpenCommand { get; set; }
        public ICommand StudentOpenCommand { get; set; }
        public ICommand CleanerOpenCommand { get; set; }
        public ICommand CourseOpenCommand { get; set; }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
               

                TeacherOpenCommand = new RelayCommand(() =>
                {
                    TeacherWindow tw = new TeacherWindow();
                    tw.ShowDialog();
                });
            }
        }
    }
}
