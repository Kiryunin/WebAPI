using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Classroom
    {

        [Column("ClassroomId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Classroom name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30  characters.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Number of seats is a required field.")]
        public int NumberOfSeats { get; set; }
        
        [ForeignKey(nameof(School))]
        public Guid SchoolId { get; set; }
        public School School { get; set; }
    }
}
