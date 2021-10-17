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
        //1 student -> n course
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public int MyProperty { get; set; }

        [NotMapped]
        public virtual Course Course { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
