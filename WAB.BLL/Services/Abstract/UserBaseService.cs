using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.DAL.Entities;
using WAB.DAL.Repositories.Abstract;

namespace WAB.BLL.Services.Abstract;

public abstract class UserBaseService : BaseService<User>
{
    protected UserBaseService(IUserRepository context, IMapper mapper) : base(context, mapper)
    {
    }

    public abstract Task<ICollection<UserDtoRead>> GetUsers();

    public abstract Task<UserDtoRead> GetUserById(int id);

    // public abstract Task<UserDTORead> CreateUser(UserDTOWrite user);
    //
    // public abstract Task<UserDTORead> UpdateUser(UserDTORead user);
    //
    // public abstract Task DeleteUser(int id);
}