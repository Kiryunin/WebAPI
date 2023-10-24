namespace Entities.RequestFeatures
{
    public class ClassroomParameters: RequestParameters
    {
        public uint MinNuberOfSeats { get; set; }
        public uint MaxNuberOfSeats { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxNuberOfSeats > MinNuberOfSeats;
    }
}
