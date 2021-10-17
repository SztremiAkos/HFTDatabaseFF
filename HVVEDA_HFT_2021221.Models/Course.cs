using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    [Table("Courses")]
    public class Course
    {
        //1 course --> n Student
        [Key]
        public int CourseID { get; set; }



        [MaxLength(15)]
        [Required]
        public string Title { get; set; }

        [MaxLength(15)]
        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime Length { get; set; }



        [NotMapped]
        public virtual Student Student { get; set; }

        public int? StudentsCount { get; set; }



        [NotMapped]
        public virtual Teacher Teacher { get; set; }



        [ForeignKey(nameof(Teacher))] public int TeacherID { get; set; }






        [MaxLength(1)]
        public int? Credits { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
        public Course()
        {
            Students = new HashSet<Student>();
        }
    }
}
