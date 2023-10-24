namespace Entities.DataTransferObjects
{
    public class SchoolForCreationDto
    {
        public string NameAndNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IEnumerable<ClassroomForCreationDto> Classrooms { get; set; }


    }
}
