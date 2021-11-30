using ConsoleTools;
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

            var studentMenu = new ConsoleMenu(args, level: 1) //DONE
                .Add("List Students", () => List<Student>("student"))
                .Add("Add Student", () => AddStudent())
                .Add("Update Student", () => UpdateStudent())
                .Add("Delete Student", () => Delete<Student>("student"))
            .Add("Exit", ConsoleMenu.Close);
            var teacherMenu = new ConsoleMenu(args, level: 1) //Done
                .Add("List teachers", () => List<Teacher>("teacher"))
                .Add("Add teacher", () => AddTeacher())
                .Add("Update teacher", () => UpdateTeacher())
                .Add("Delete teacher", () => Delete<Teacher>("teacher"))
            .Add("Exit", ConsoleMenu.Close);

            var CourseMenu = new ConsoleMenu(args, level: 1)
                .Add("List courses", () => List<Course>("course"))
                .Add("Add course", () => AddCourse())
                .Add("Update course", () => UpdateCourse())
                .Add("Delete course", () => Delete<Course>("course"))
            .Add("Exit", ConsoleMenu.Close);

            var CleanerMenu = new ConsoleMenu(args, level: 1)
                .Add("List cleaners", () => List<Cleaner>("cleaner"))
                .Add("Add cleaner", () => AddCleaner())
                .Add("Update cleaner", () => UpdateCleaner())
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
            MenuLayout("List");
            var List = rest.Get<T>(path);
            foreach (var item in List)
            {
                Console.WriteLine(item + "\n");
            }
            Console.ReadLine();
        }
        static void Delete<T>(string path)
        {
            MenuLayout("Delete");
            Console.WriteLine("enter the id please: ");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, path);
        }
        static void AddStudent()
        {
            MenuLayout("AddStudent");
            Console.WriteLine("First Name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string lastname = Console.ReadLine();
            rest.Post<Student>(new Student { LastName = firstname, Firstname = lastname }, "student");
            Console.WriteLine("Student has been added...");
            Console.ReadLine();
        }
        static void AddTeacher()
        {
            MenuLayout("AddTeacher");
            Console.WriteLine("First Name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string lastname = Console.ReadLine();
            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Salary:");
            int salary = int.Parse(Console.ReadLine());
            Teacher teacher = new Teacher() { Age = age, Salary = salary, Firstname = firstname, LastName = lastname };
            rest.Post<Teacher>(teacher, "teacher");
            Console.WriteLine("Teacher has been added...");
            Console.ReadLine();
        }
        static void AddCleaner()
        {
            MenuLayout("AddCleaner");
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.Write("Position: ");
            string position = Console.ReadLine();
            Console.Write("Salary:");
            int salary = int.Parse(Console.ReadLine());
            Cleaner cleaner = new Cleaner() { Name = name, Salary = salary, Position = position};
            rest.Post<Cleaner>(cleaner, "cleaner");
            Console.WriteLine("Cleaner has been added...");
            Console.ReadLine();
        }
        static void AddCourse()
        {
            MenuLayout("AddCourse");
            Console.Write("Course Title:");
            string Title = Console.ReadLine();
            Console.Write("Location:");
            string Location = Console.ReadLine();
            Console.Write("Type: ");
            string Type = Console.ReadLine();
            Console.Write("Duration(Form(HH:MM))");
            string Length = Console.ReadLine();
            Console.Write("Credit Amount: ");
            int credits = int.Parse(Console.ReadLine());
            Course course = new Course() { Title = Title, Location = Location, Type = Type, Length = Length ,Credits = credits};
            
            ;
            ;
            rest.Post<Course>(course, "course");
            Console.WriteLine("Course has been added...");
            Console.ReadLine();
        }
        static void UpdateStudent()
        {
            MenuLayout("UpdateStudent");
            Console.Write("Enter the Id: ");
            int id = int.Parse(Console.ReadLine());
            var student = rest.Get<Student>("student");
            var newstud = new Student();
            foreach (var item in student)
            {
                if (item.StudentID == id)
                {
                    newstud = item;
                    break;
                }
            }
            if (newstud.StudentID == id)
            {
                ;
                Console.WriteLine("First Name:");
                string firstname = Console.ReadLine();
                Console.WriteLine("Last Name:");
                string lastname = Console.ReadLine();
                newstud.Firstname = firstname;
                newstud.LastName = lastname;
                rest.Put<Student>(newstud, "student");
                ;
            }
            else
            {
                throw new IndexOutOfRangeException($"There is no {id} in the Students database!");
            }

        }
        static void UpdateTeacher()
        {
            MenuLayout("UpdateTeacher");
            Console.WriteLine("Please enter the teacher's id: ");
            int id = int.Parse(Console.ReadLine());
            var student = rest.Get<Teacher>("teacher");
            var newstud = new Teacher();
            foreach (var item in student)
            {
                if (item.TeacherId == id)
                {
                    newstud = item;
                    break;
                }
            }
            if (newstud.TeacherId == id)
            {

                Console.Clear();
                Console.WriteLine("First Name:");
                string newfirstname = Console.ReadLine();
                Console.WriteLine("Last Name:");
                string newlastname = Console.ReadLine();
                Console.Write("Age: ");
                int newage = int.Parse(Console.ReadLine());
                Console.WriteLine("Salary:");
                int newsalary = int.Parse(Console.ReadLine());
                if (newfirstname != "")
                {
                    newstud.Firstname = newfirstname;
                }
                if (newlastname != "")
                {
                    newstud.LastName = newlastname;
                }
                if (newsalary > 0)
                {
                    newstud.Salary = newsalary;
                }
                if (newage > 0)
                {
                    newstud.Age = newage;
                }
                rest.Put<Teacher>(newstud, "teacher");
            }
            else
            {
                throw new IndexOutOfRangeException($"There is no {id} in the Teachers database!");
            }
        }
        static void UpdateCleaner()
        {
            MenuLayout("UpdateCleaner");
            Console.Write("Enter a valid cleaner id: ");
            int id = int.Parse(Console.ReadLine());
            var cleaner = rest.Get<Cleaner>("cleaner");
            var newcleaner = new Cleaner();
            foreach (var item in cleaner)
            {
                if (item.CleanerId == id)
                {
                    newcleaner = item;
                }
            }
            if (newcleaner.CleanerId == id)
            {
                Console.WriteLine("New Position:");
                newcleaner.Position = Console.ReadLine();
                Console.Write("New Salary");
                newcleaner.Salary = int.Parse(Console.ReadLine());
                rest.Put<Cleaner>(newcleaner, "cleaner");
            }
            else
            {
                throw new IndexOutOfRangeException($"There is no {id} in the Students database!");
            }
        }
        static void UpdateCourse()
        {
            MenuLayout("UpdateCourse");
            Console.WriteLine("Please enter a valid Course id:");
            int id = int.Parse(Console.ReadLine());
            var course = rest.Get<Course>("course");
            var newcourse = new Course();
            foreach (var item in course)
            {
                if (item.CourseID == id)
                {
                    newcourse = item;
                }
            }
            if (newcourse.CourseID == id)
            {
                Console.WriteLine("New Title:");
                newcourse.Title = Console.ReadLine();
                Console.Write("New Credit amount");
                newcourse.Credits = int.Parse(Console.ReadLine());
                Console.WriteLine("New length");
                newcourse.Length = Console.ReadLine();
                Console.WriteLine("new Location");
                newcourse.Location = Console.ReadLine();
                rest.Put<Course>(newcourse, "course");
            }
            else
            {
                throw new IndexOutOfRangeException($"There is no {id} in the Courses database!");
            }
        }
        static void MenuLayout(string menutype)
        {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth + menutype.Length) / 2, Console.CursorTop);
            Console.WriteLine(menutype+"\n");

        }
    }
}