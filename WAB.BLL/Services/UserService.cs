using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.BLL.Exceptions;
using WAB.BLL.Services.Abstract;
using WAB.DAL.Repositories.Abstract;

namespace WAB.BLL.Services;

public class UserService : UserBaseService
{
    public UserService(IUserRepository context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<ICollection<UserDtoRead>> GetUsers()
    {
        var users = await Context.GetAll();
        return Mapper.Map<ICollection<UserDtoRead>>(users);
    }

    public override async Task<UserDtoRead> GetUserById(int id)
    {
        var user = await Context.Get(id);
        if (user is null) throw new ObjectDoesNotExistException("User with this id doesn't exist");
        return Mapper.Map<UserDtoRead>(user);
    }
}