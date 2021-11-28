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
            var teachers = rest.Get<Teacher>("teacher");
            var teacherMenu = new ConsoleMenu(args, level: 1)
                .Add("List Students", () => rest.Get<Student>("student"))
                .Add("Add Student", () => rest.Post<Student>(new Student { LastName = "asd" }, "student"))
                .Add("Update Student", () => rest.Put<Student>(new Student { LastName = "asd2" }, "student"))
                .Add("Delete Student", () => rest.Delete(4, "student"));

            var noncrud_menu = new ConsoleMenu(args, level: 1)
                .Add("ShowTheDirtiestCoursesTeacher", () => rest.GetSingle<Teacher>("stat/TheDirtiestCoursesTeacher"))
                .Add("StudentCountPerCategory", () => rest.Get<List<KeyValuePair<string, int>>>("stat/StudentCountPerCategory"))
                .Add("CleanerNumberPerClassroom", () => rest.Get<IEnumerable<KeyValuePair<string, int>>>("stat/CleanerNumberPerClassroom"))
                .Add("CourseCleaningPrice", () => rest.Get<IEnumerable<KeyValuePair<string, int>>>("stat/CourseCleaningPrice"))
                .Add("TeacherSalaryPerCourse", () => rest.Get<IEnumerable<KeyValuePair<string, int>>>("stat/TeacherSalaryPerCourse"));
            var menu = new ConsoleMenu(args, level: 0)  //0. level, ez a fomenu. Tovabbi menuket is lehet felvenni, de akkor azt fel kell venni es hivogatni, nyilvan mas levellel
                                       .Add("teachers", () => rest.Get<Teacher>("teacher"))  //igy adunk elemet
                                       .Add("noncruds", noncrud_menu.Show)  //igy adunk meg egy elemet
                                       .Add("Exit", () => Environment.Exit(0)) //kilepes
              .Configure(config =>
              {
                  config.Selector = "[SELECTED] ";  //ezt rajuk kivalasztott menuelem ele
                  config.EnableFilter = false;  //prompt
                  config.ClearConsole = false;  //torolje a konzolt ha visszater a menuvel?
                  config.Title = "Main menu";  //menu cime
                  config.EnableWriteTitle = true;  //title-t kirja?
                  config.EnableBreadcrumb = true;  //ha tobbszintu legyen morzsa fomenu>almenu>almenu>sokadikalmenu?
              });

            menu.Show();  //megjeleniteni
            ;
            var dirtyCourse = rest.GetSingle<Student>("stat/TheDirtiestCoursesTeacher");
        }
        static void MainMenu()
        {

        }
        static void StudentMenu()
        {

        }
        static void TeacherMenu()
        {

        }
        static void CleanerMenu()
        {

        }
        static void CourseMenu()
        {

        }
    }
}