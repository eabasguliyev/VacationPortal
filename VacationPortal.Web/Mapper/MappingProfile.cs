using AutoMapper;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Admin.Models.EmployeeVMs;

namespace VacationPortal.Web.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeVM>();

            CreateMap<EmployeeVM, Employee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
