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
    public class TeacherWindowViewModel : ObservableRecipient
    {
        public RestCollection<Teacher> Teachers { get; set; }
        public RestCollection<Student> Students { get; set; }
        public RestCollection<Cleaner> Clenaers { get; set; }
        public RestCollection<Course> Courses { get; set; }

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
                if (value != null)
                {

                    selectedTeacher = new Teacher()
                    {
                        LastName = value.LastName,
                        Firstname = value.Firstname,
                        Age = value.Age,
                        Salary = value.Salary,
                        Courses = value.Courses,
                        TeacherId = value.TeacherId
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
                    Teachers.Add(new Teacher()
                    {
                        Age = selectedTeacher.Age,
                        LastName = selectedTeacher.LastName,
                        Firstname = selectedTeacher.Firstname,
                        Salary= selectedTeacher.Salary,
                        Courses= selectedTeacher.Courses,
                    });

                });

                RemoveTeacherCommand = new RelayCommand(() =>
                {
                    Teachers.Delete(selectedTeacher.TeacherId);
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
