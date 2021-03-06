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
    [Table("Cleaners")]
    public class Cleaner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CleanerId { get; set; }

        
        public string Name { get { return FirstName; } }

        public string FirstName { get; set; }
        public int Salary { get; set; }
        public string? Position { get; set; }

        [JsonIgnore]
        public virtual Course? Location { get; set; }
        public override string ToString()
        {
            return ">>ID: " + CleanerId + "\n\t>>Name: " + FirstName + "\n\t>>Salary: " + Salary + "\n\t>>Position: " + Position;
        }

    }
}
