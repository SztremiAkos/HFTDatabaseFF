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

    [Table("Courses")]
    public class Course
    {

        //1 course --> 1 Student
        #region keys


        [NotMapped]
        [JsonIgnore]
        public virtual Cleaner Cleaner { get; set; }


        [ForeignKey(nameof(Cleaner))]
        public int CleanerId { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }



        [NotMapped]
        [JsonIgnore]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseID { get; set; } //

        #endregion
        [MaxLength(35)]
        [Required]
        public string Title { get; set; } //

        [MaxLength(15)]
        [Required]
        public string Location { get; set; } //

        [Required]
        public string Type { get; set; }

        [Required]
        public TimeSpan Length { get; set; } 

        [MaxLength(1)]
        public int? Credits { get; set; }
    }
}
