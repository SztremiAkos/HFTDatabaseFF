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
        public RestCollection<Cleaner> Cleaners { get; set; }
        public RestCollection<Course> Courses { get; set; }
        public List<Polimorph> tables { get; set; }
        //public string TeacherVis { get { return teacherVis; } set { OnPropertyChanged("TeacherVis"); } }
        //private string teacherVis;
        //public string StudVis { get { return studVis; } set { OnPropertyChanged("StudVis"); } }
        //private string studVis;
        //public string CleanerVis { get; set; }
        //public string CourseVis { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        private object selectedObject;
        private object selectedTable;
        public object SelectedTable
        {
            get { return selectedTable; }
            set
            {
                if (value != null)
                {
                    selectedTable = value;
                    OnPropertyChanged();
                }
                if (value is RestCollection<Teacher>)
                {

                }
                else if (value is RestCollection<Student>)
                {

                }
            }
        }
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (value != null)
                {
                    if (value is Student)
                    {
                        selectedObject = new Student()
                        {
                            Firstname = (value as Student).Firstname,
                            LastName = (value as Student).LastName,


                        };
                    }
                    else if (value is Teacher)
                    {
                        selectedObject = new Teacher()
                        {
                            LastName = (value as Teacher).LastName,
                            Firstname = (value as Teacher).Firstname,
                            Age = (value as Teacher).Age,
                            Salary = (value as Teacher).Salary,
                            Courses = (value as Teacher).Courses,
                            TeacherId = (value as Teacher).TeacherId
                        };
                        ;
                    }
                    else if (value is Course)
                    {
                        selectedObject = new Course()
                        {
                            Credits = (value as Course).Credits,
                            Length = (value as Course).Length,
                            Location = (value as Course).Location,
                            Title = (value as Course).Title
                        };
                    }
                    else if (value is Cleaner)
                    {
                        selectedObject = new Cleaner()
                        {
                            Position = (value as Cleaner).Position,
                            Salary = (value as Cleaner).Salary,

                        };
                    }
                    ;
                    OnPropertyChanged();
                    ;
                    //(RemoveTeacherCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                ;


            }
        }

        public ICommand AddTeacherCommand { get; set; }
        public ICommand RemoveTeacherCommand { get; set; }
        public ICommand UpdateTeacherCommand { get; set; }
        public TeacherWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                tables = new List<Polimorph>();
                Teachers = new RestCollection<Teacher>("http://localhost:6157/", "teacher");
                Students = new RestCollection<Student>("http://localhost:6157/", "student");
                Cleaners = new RestCollection<Cleaner>("http://localhost:6157/", "cleaner");
                Courses = new RestCollection<Course>("http://localhost:6157/", "course");
                tables.Add(Students);
                tables.Add(Teachers);
                tables.Add(Courses);

                tables.Add(Cleaners);
                ;
                //AddTeacherCommand = new RelayCommand(() =>
                //{
                //    Teachers.Add(new Teacher()
                //    {
                //        Age = selectedTeacher.Age,
                //        LastName = selectedTeacher.LastName,
                //        Firstname = selectedTeacher.Firstname,
                //        Salary= selectedTeacher.Salary,
                //        Courses= selectedTeacher.Courses,
                //    });

                //});

                //RemoveTeacherCommand = new RelayCommand(() =>
                //{
                //    Teachers.Delete(selectedTeacher.TeacherId);
                //},
                //() =>
                //{
                //    return SelectedTeacher != null;
                //});
                UpdateTeacherCommand = new RelayCommand(() =>
                {
                    if (SelectedObject is Teacher)
                    {

                        Teachers.Update((SelectedObject as Teacher));
                    }
                    else if (SelectedObject is Student)
                    {
                        MessageBox.Show("not implemented yet");
                    }
                    else if (SelectedObject is Cleaner)
                    {

                    }
                    else if (SelectedObject is Course)
                    {

                    }

                });
                SelectedObject = new Teacher();
            }
        }
    }
}
