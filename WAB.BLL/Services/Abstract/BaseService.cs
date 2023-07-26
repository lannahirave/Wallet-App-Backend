using AutoMapper;
using WAB.DAL.Repositories.Abstract;

namespace WAB.BLL.Services.Abstract;

public abstract class BaseService<T> where T : class
{
    private protected readonly IRepository<T> Context;
    private protected readonly IMapper Mapper;

    protected BaseService(IRepository<T> context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
}