using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class School
    {
        [Column("SchoolId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "School name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string NameAndNumber { get; set; }
        [Required(ErrorMessage = "School address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte")]
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }

    }
}
