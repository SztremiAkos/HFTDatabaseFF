using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    [Table("Cleaners")]
    public class Cleaner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CleanerId { get; set; }

        [Required]
        public string Name { get; set; }
        public int? Salary { get; set; }
        public string Position { get; set; }
        public virtual Course Location { get; set; }

        [ForeignKey(nameof(Location))]
        public virtual int CourseId { get; set; }

    }
}
