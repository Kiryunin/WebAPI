using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ClassroomConfiguration: IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
                (
                    new Classroom
                    {
                        Id = Guid.NewGuid(),
                        Type = "Physics classroom",
                        NumberOfSeats = 20,
                        SchoolId = new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be")
                    },
                    new Classroom
                    {
                        Id = Guid.NewGuid(),
                        Type = "Literature classroom",
                        NumberOfSeats = 18,
                        SchoolId = new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be")
                    }, 
                    new Classroom 
                    {
                        Id = Guid.NewGuid(),
                        Type = "Chemistry classroom",
                        NumberOfSeats = 15,
                        SchoolId = new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef")
                    }
                );
        }
    }
}
