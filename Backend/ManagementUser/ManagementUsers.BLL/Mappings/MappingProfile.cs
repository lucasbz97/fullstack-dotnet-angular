using AutoMapper;

namespace ManagementUsers.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ManagementUsers.DAL.DTOs.Request.UserRequestDTO, ManagementUsers.BLL.Domain.UserModel>().ReverseMap();
            CreateMap<ManagementUsers.DAL.DTOs.Request.DependentRequestDTO, ManagementUsers.BLL.Domain.DependentModel>().ReverseMap();
        }
    }
}
