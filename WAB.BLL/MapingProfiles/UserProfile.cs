using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.DAL.Entities;

namespace WAB.BLL.MapingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDtoRead, User>();
        CreateMap<User, UserDtoRead>();
    }
}