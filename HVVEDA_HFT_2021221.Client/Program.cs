﻿using HVVEDA_HFT_2021221.Data;
using System;
using System.Linq;

namespace HVVEDA_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CourseContext ctx = new CourseContext();
            Console.WriteLine(ctx.Cleaners.Count());
            Console.WriteLine(ctx.Courses.Count());
            Console.WriteLine(ctx.Students.Count());
            Console.WriteLine(ctx.Teachers.Count());
            ;
        }
    }
}
