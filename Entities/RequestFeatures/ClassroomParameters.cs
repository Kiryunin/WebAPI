namespace Entities.RequestFeatures
{
    public class ClassroomParameters: RequestParameters
    {
        public ClassroomParameters() 
        {
            OrderBy = "type";
        }
        public uint MinNuberOfSeats { get; set; }
        public uint MaxNuberOfSeats { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxNuberOfSeats > MinNuberOfSeats;
        public string SearchTerm { get; set; }

    }
}
