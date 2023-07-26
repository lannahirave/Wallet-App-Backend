using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.DAL.Entities;
using WAB.DAL.Repositories.Abstract;

namespace WAB.BLL.Services.Abstract;

public abstract class TransactionBaseService : BaseService<Transaction>
{
    protected TransactionBaseService(ITransactionRepository context, IMapper mapper) : base(context, mapper)
    {
    }

    public abstract Task<ICollection<TransactionDtoRead>> GetTransactions();

    public abstract Task<TransactionDtoRead> GetTransactionById(int id);

    public abstract Task<ICollection<TransactionDtoRead>> GetTransactionsByUserId(int id);

    // public abstract Task<UserDTORead> CreateUser(UserDTOWrite user);
    //
    // public abstract Task<UserDTORead> UpdateUser(UserDTORead user);
    //
    // public abstract Task DeleteUser(int id);
}