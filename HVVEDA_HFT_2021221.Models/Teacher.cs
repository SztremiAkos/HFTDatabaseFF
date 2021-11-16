using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    [Table("Teachers")]
    public class Teacher
    {

        // 1 teacher -> 1 course
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public int Age { get; set; }
        public string LastName { get; set; }
        public int? Salary { get; set; }

        [NotMapped]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
