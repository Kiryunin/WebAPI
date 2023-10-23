using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasData
                (
                    new School
                    {
                        Id = new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"),
                        NameAndNumber = "Школа №34",
                        Address = "ул. Коваленко, д.13",
                        City = "Саранск",
                        Country = "Россия"
                    },
                    new School
                    {
                        Id = new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"),
                        NameAndNumber = "Школа №11",
                        Address = " б-р Степана Эрьзи, 28A",
                        City = "Саранск",
                        Country = "Россия"
                    }
                );
        }
    }
}
