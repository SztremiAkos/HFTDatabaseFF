using ConsoleTools;
using HVVEDA_HFT_2021221.Data;
using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HVVEDA_HFT_2021221.Client
{
    class Program
    {
        static RestService rest = new RestService("http://localhost:6157");
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);

            var studentMenu = new ConsoleMenu(args, level: 1)
                .Add("List Students", () => List<Student>("student"))
                .Add("Add Student", () => AddStudent())
                .Add("Update Student", () => UpdateStudent())
                .Add("Delete Student", () => Delete<Student>("student"))
            .Add("Exit", ConsoleMenu.Close);
            var teacherMenu = new ConsoleMenu(args, level: 1)
                .Add("List teachers", () => List<Teacher>("teacher"))
                .Add("Add teacher", () => AddStudent())
                .Add("Update teacher", () => UpdateStudent())
                .Add("Delete teacher", () => Delete<Teacher>("teacher"))
            .Add("Exit", ConsoleMenu.Close);

            var CourseMenu = new ConsoleMenu(args, level: 1)
                .Add("List courses", () => List<Course>("course"))
                .Add("Add course", () => AddStudent())
                .Add("Update course", () => UpdateStudent())
                .Add("Delete course", () => Delete<Course>("course"))
            .Add("Exit", ConsoleMenu.Close);

            var CleanerMenu = new ConsoleMenu(args, level: 1)
                .Add("List cleaners", () => List<Cleaner>("cleaner"))
                .Add("Add cleaner", () => AddStudent())
                .Add("Update cleaner", () => UpdateStudent())
                .Add("Delete cleaner", () => Delete<Cleaner>("cleaner"))
            .Add("Exit", ConsoleMenu.Close);


            var noncrud_menu = new ConsoleMenu(args, level: 1)
                .Add("ShowTheDirtiestCoursesTeacher", () => Dirty())
                .Add("StudentCountPerCategory", () => StudentCountPerCategory())
                .Add("CleanerNumberPerClassroom", () => CleanerNumberPerClassroom())
                .Add("CourseCleaningPrice", () => CourseCleaningPrice())
                .Add("TeacherSalaryPerCourse", () => TeacherSalaryPerCourse())
            .Add("Exit", ConsoleMenu.Close);

      
            var menu = new ConsoleMenu(args, level: 0)
                                       .Add("Students", studentMenu.Show)
                                       .Add("Teachers", teacherMenu.Show)
                                       .Add("Courses", CourseMenu.Show)
                                       .Add("Cleaners", CleanerMenu.Show)
                                       .Add("Noncruds", noncrud_menu.Show)  
                                       .Add("Exit", () => Environment.Exit(0)) //kilepes
              .Configure(config =>
              {
                  config.Selector = ">> ";
                  config.EnableFilter = false;  
                  config.ClearConsole = true;  
                  config.Title = "Main menu";  
                  config.EnableWriteTitle = true;
                  config.EnableBreadcrumb = false;
              });

            menu.Show();  
        }
        #region staticNoncrud
        static void Dirty()
        {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "TheDirtiestCoursesTeacher".Length) / 2, Console.CursorTop);
            Console.WriteLine("TheDirtiestCoursesTeacher");
            var Teacher = rest.GetSingle<Teacher>("stat/TheDirtiestCoursesTeacher");
            Console.WriteLine(Teacher.ToString());
            Console.ReadLine();
        }
        static void StudentCountPerCategory()
        {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "StudentCountPerCategory".Length) / 2, Console.CursorTop);
            Console.WriteLine("StudentCountPerCategory");
            var List = rest.Get<KeyValuePair<string, int>>("stat/StudentCountPerCategory");
            foreach (var item in List)
            {
                Console.WriteLine("\t>>Category: " + item.Key + "\n\tCount: " + item.Value + "\n");
            }
            Console.ReadLine();
        }
        static void CleanerNumberPerClassroom()
        {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "CleanerNumberPerClassroom".Length) / 2, Console.CursorTop);
            Console.WriteLine("CleanerNumberPerClassroom");
            var List = rest.Get<KeyValuePair<string, int>>("stat/CleanerNumberPerClassroom");
            foreach (var item in List)
            {
                Console.WriteLine("\t>>Classroom: " + item.Key + "\n\t>>Count: " + item.Value + "\n");
            }
            Console.ReadLine();
        }
        static void CourseCleaningPrice()
        {

            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "CourseCleaningPrice".Length) / 2, Console.CursorTop);
            Console.WriteLine("CourseCleaningPrice");
            var q = rest.Get<KeyValuePair<string, int>>("stat/CourseCleaningPrice");
            foreach (var item in q)
            {
                Console.WriteLine("\t>>Course: " + item.Key + "\n\t>>Price: " + item.Value + "\n");
            }
            Console.ReadLine();
        }
        static void TeacherSalaryPerCourse()
        {

            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "TeacherSalaryPerCourse".Length) / 2, Console.CursorTop);
            Console.WriteLine("TeacherSalaryPerCourse");
            var teacherList = rest.Get<KeyValuePair<string, int>>("stat/TeacherSalaryPerCourse");
            foreach (var item in teacherList)
            {
                Console.WriteLine("\t>>Teacher: " + item.Key + "\n\t>>Salary: " + item.Value + "\n");
            }
            Console.ReadLine();
        }
        #endregion
        static void List<T>(string path)
        {
            var Students = rest.Get<T>(path);
            foreach (var item in Students)
            {
                Console.WriteLine(item + "\n");
            }
            Console.ReadLine();
        }
        static void AddStudent()
        {
            Console.WriteLine("First Name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string lastname = Console.ReadLine();
            rest.Post<Student>(new Student { LastName = firstname, Firstname = lastname }, "student");
            Console.WriteLine("Student has been added...");
            Console.ReadLine();
        }
        static void UpdateStudent()
        {
            int id = int.Parse(Console.ReadLine());
            var student = rest.Get<Student>("student");
            var newstud = new Student();
            foreach (var item in student)
            {
                if (item.StudentID == id)
                {
                    newstud = item;
                }
            }
            if (newstud.StudentID == id)
            {
                Console.WriteLine("First Name:");
                string firstname = Console.ReadLine();
                Console.WriteLine("Last Name:");
                string lastname = Console.ReadLine();
                newstud.Firstname = firstname;
                newstud.LastName = lastname;
                rest.Put<Student>(newstud, "student");
            }
            else
            {
                throw new IndexOutOfRangeException($"There is no {id} in the Students database!");
            }

        }
        static void Delete<T>(string path)
        {
            Console.WriteLine("enter the id please: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, path);
        }
    }
}