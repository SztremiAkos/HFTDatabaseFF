using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    [Table("Students")]
    public class Student
    {
        public Student()
        {
            this.Courses = new List<Course>();
        }
        //1 student -> n course

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentID { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}
