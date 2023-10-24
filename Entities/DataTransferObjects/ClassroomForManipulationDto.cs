using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class ClassroomForManipulationDto
    {
        [Required(ErrorMessage = "Classroom type is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the T is 30 characters.")]
        public string Type { get; set; }
        [Range(5, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 5")]
        public int NumberOfSeats { get; set; }
    }
}
