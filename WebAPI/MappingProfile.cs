using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<School, SchoolDto>().ForMember(s => s.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.City, x.Country)));
            CreateMap<Classroom, ClassroomDto>();
        }
    }
}
