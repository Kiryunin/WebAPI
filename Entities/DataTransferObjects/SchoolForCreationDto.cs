using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class SchoolForCreationDto
    {
        [Required(ErrorMessage = "School name and number is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string NameAndNumber { get; set; }
        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Country { get; set; }
        public IEnumerable<ClassroomForCreationDto> Classrooms { get; set; }


    }
}
