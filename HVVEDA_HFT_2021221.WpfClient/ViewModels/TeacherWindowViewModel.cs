using HVVEDA_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HVVEDA_HFT_2021221.WpfClient.ViewModels
{
    internal class TeacherWindowViewModel : ObservableRecipient
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
        private Teacher selectedTeacher;

        public Teacher SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                if (value!=null)
                {
                    selectedTeacher = new Teacher()
                    {
                        LastName = value.LastName,
                        Firstname = value.Firstname,
                        Age = value.Age,
                        Salary = value.Salary,
                        Courses = value.Courses,
                    };
                    OnPropertyChanged();
                    (RemoveTeacherCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                
            }
        }

        public ICommand AddTeacherCommand { get; set; }
        public ICommand RemoveTeacherCommand { get; set; }
        public ICommand UpdateTeacherCommand { get; set; }
        public TeacherWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Teachers = new RestCollection<Teacher>("http://localhost:6157/", "teacher");
                AddTeacherCommand = new RelayCommand(() =>
                {
                    Teachers.Add(SelectedTeacher);

                });

                RemoveTeacherCommand = new RelayCommand(() =>
                {
                    Teachers.Delete(SelectedTeacher.TeacherId);
                },
                () =>
                {
                    return SelectedTeacher != null;
                });
                UpdateTeacherCommand = new RelayCommand(() =>
                {
                    Teachers.Update(SelectedTeacher);
                });
                SelectedTeacher = new Teacher();
            }
        }
    }
}
