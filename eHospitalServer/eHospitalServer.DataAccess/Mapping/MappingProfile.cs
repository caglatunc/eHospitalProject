using AutoMapper;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;

namespace eHospitalServer.DataAccess.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}
