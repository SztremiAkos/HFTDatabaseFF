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
    internal class StudentWindowViewModel : ObservableRecipient
    {
        public RestCollection<Student> Students { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                if (value != null)
                {

                    selectedStudent = new Student()
                    {
                        LastName = value.LastName,
                        Firstname = value.Firstname,
                        StudentID = value.StudentID,
                        
                    };
                    OnPropertyChanged();
                    (RemoveStudentCommand as RelayCommand).NotifyCanExecuteChanged();
                }


            }
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand RemoveStudentCommand { get; set; }
        public ICommand UpdateStudentCommand { get; set; }
        public StudentWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Students = new RestCollection<Student>("http://localhost:6157/", "student");
                AddStudentCommand = new RelayCommand(() =>
                {
                    Students.Add(new Student()
                    {
                        LastName = selectedStudent.LastName,
                        Firstname = selectedStudent.Firstname,
                    });

                });

                RemoveStudentCommand = new RelayCommand(() =>
                {
                    Students.Delete(selectedStudent.StudentID);
                },
                () =>
                {
                    return selectedStudent != null;
                });
                UpdateStudentCommand = new RelayCommand(() =>
                {
                    Students.Update(SelectedStudent);
                });
                SelectedStudent = new Student();
            }
        }
    }
}
