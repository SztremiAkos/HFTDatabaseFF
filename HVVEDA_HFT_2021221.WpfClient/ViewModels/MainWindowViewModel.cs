using HVVEDA_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    public  class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Teacher> Teachers { get; set; }
        public RestCollection<Student> Students { get; set; }
        public RestCollection<Cleaner> Cleaners { get; set; }
        public RestCollection<Course> Courses { get; set; }
        public List<Polimorph> tables { get; set; }
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
                            StudentID = (value as Student).StudentID,


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
                            Title = (value as Course).Title,
                            CourseID = (value as Course).CourseID
                        };
                    }
                    else if (value is Cleaner)
                    {
                        selectedObject = new Cleaner()
                        {
                            FirstName = (value as Cleaner).FirstName,
                            Position = (value as Cleaner).Position,
                            Salary = (value as Cleaner).Salary,
                            CleanerId = (value as Cleaner).CleanerId

                        };
                    }
                    ;
                    OnPropertyChanged();
                    (RemoveTeacherCommand as RelayCommand).NotifyCanExecuteChanged();
                }


            }
        }


        public ICommand AddTeacherCommand { get; set; }
        public ICommand RemoveTeacherCommand { get; set; }
        public ICommand UpdateTeacherCommand { get; set; }
        public MainWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                tables = new List<Polimorph>();
                Teachers = new RestCollection<Teacher>("http://localhost:6157/", "teacher");
                Students = new RestCollection<Student>("http://localhost:6157/", "student");
                Cleaners = new RestCollection<Cleaner>("http://localhost:6157/", "cleaner");
                Courses = new RestCollection<Course>("http://localhost:6157/", "course");
                tables.Add(Teachers);
                tables.Add(Students);
                tables.Add(Courses);
                tables.Add(Cleaners);
                ;
                AddTeacherCommand = new RelayCommand(() =>
                {
                    if (SelectedObject is Teacher) //Done
                    {
                        var teacher = (Teacher)SelectedObject;
                        Teachers.Add(new Teacher()
                        {
                            Age = teacher.Age,
                            LastName = teacher.LastName,
                            Firstname = teacher.Firstname,
                            Salary = teacher.Salary,
                            Courses = teacher.Courses,
                        });
                        ;
                    }
                    else if (SelectedObject is Student) // Works
                    {
                        var student = (Student)SelectedObject;
                        Students.Add(new Student()
                        {
                            LastName = student.LastName,
                            Firstname = student.Firstname,

                        });
                        ;
                    }
                    else if (SelectedObject is Cleaner)
                    {
                        var cleaner = (Cleaner)SelectedObject;
                        Cleaners.Add(new Cleaner()
                        {
                            FirstName = cleaner.FirstName,
                            Location = cleaner.Location,
                            Position = cleaner.Position,
                            Salary = cleaner.Salary,
                        });
                        ;
                    }
                    else if (SelectedObject is Course)
                    {
                        var course = (Course)selectedObject;
                        ;
                        Courses.Add(new Course()
                        {
                            Title = course.Title,
                            Location = course.Location,
                            Length = course.Length,
                        });
                        ;
                    }

                });

                RemoveTeacherCommand = new RelayCommand(() =>
                {
                    if (SelectedObject is Teacher)
                    {

                        Teachers.Delete((SelectedObject as Teacher).TeacherId);
                    }
                    else if (SelectedObject is Student)
                    {
                        Students.Delete((SelectedObject as Student).StudentID);
                    }
                    else if (SelectedObject is Cleaner)
                    {
                        Cleaners.Delete((SelectedObject as Cleaner).CleanerId);
                    }
                    else if (SelectedObject is Course)
                    {
                        Courses.Delete((SelectedObject as Course).CourseID);
                    }
                },
                () =>
                {
                    return SelectedObject != null;
                });
                UpdateTeacherCommand = new RelayCommand(() =>
                {
                    if (SelectedObject is Teacher)
                    {

                        Teachers.Update((SelectedObject as Teacher));
                    }
                    else if (SelectedObject is Student)
                    {
                        Students.Update(SelectedObject as Student);
                    }
                    else if (SelectedObject is Cleaner)
                    {
                        Cleaners.Update(SelectedObject as Cleaner);
                    }
                    else if (SelectedObject is Course)
                    {
                        Courses.Update(SelectedObject as Course);
                    }

                });
            }
        }
    }
}
