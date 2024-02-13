using AutoMapper;
using simple_Web.Domain.Entities;
using simple_Web.Service.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace simple_Web.Configurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AdminRegisterDto, Admin>().ReverseMap();
            CreateMap<AccountRegisterDto, User>().ReverseMap();
        }
    }
}
