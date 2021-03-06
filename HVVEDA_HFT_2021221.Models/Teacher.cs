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
    [Table("Teachers")]
    public class Teacher
    {
        public Teacher()
        {
            this.Courses = new List<Course>();
        }

        // 1 teacher -> 1 course
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public int Age { get; set; }
        public string LastName { get; set; }
        public string Name { get { return Firstname + " " + LastName; } }
        public int? Salary { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

        public override string ToString()
        {
            return "\n>>Name: " + Firstname + " " + LastName + "\n\t>>ID: " + TeacherId + "\n\t>>Salary: " + Salary + "\n\t>>Age: " + Age;
        }
    }
}
