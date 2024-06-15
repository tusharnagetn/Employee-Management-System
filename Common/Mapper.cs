using AutoMapper;
using Employee_Management_System.DTO;
using Employee_Management_System.Entity;

namespace Employee_Management_System.Common
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<EmployeeBasicDetailsDTO, EmployeeBasicDetails>().ReverseMap();

            CreateMap<EmployeeAdditionalDetailsDTO, EmployeeAdditionalDetails>().ReverseMap();
        }
    }
}
