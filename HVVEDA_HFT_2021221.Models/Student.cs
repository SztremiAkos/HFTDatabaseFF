using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    [Table("Students")]
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }
        //1 student -> n course

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentID { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        public int CourseCount { get { return Courses.Count; } }

        public override string ToString()
        {
            return ">>Name<<:" + Firstname + " " + LastName + "\n\t>>ID<<:" + StudentID+"\n";
        }

    }
}
