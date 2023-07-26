using AutoMapper;
using WAB.BLL.DTO.DtoRead;
using WAB.BLL.Exceptions;
using WAB.BLL.Services.Abstract;
using WAB.DAL.Repositories.Abstract;

namespace WAB.BLL.Services;

public class TransactionService : TransactionBaseService
{
    public TransactionService(ITransactionRepository context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<ICollection<TransactionDtoRead>> GetTransactions()
    {
        var transactions = await Context.GetAll();
        return Mapper.Map<ICollection<TransactionDtoRead>>(transactions);
    }

    public override async Task<TransactionDtoRead> GetTransactionById(int id)
    {
        var transaction = await Context.Get(id);
        if (transaction is null) throw new ObjectDoesNotExistException("Transaction with this id doesn't exist");
        return Mapper.Map<TransactionDtoRead>(transaction);
    }

    public override async Task<ICollection<TransactionDtoRead>> GetTransactionsByUserId(int id)
    {
        var transactions = await ((ITransactionRepository) Context).GetTransactionsByUserId(id);
        return Mapper.Map<ICollection<TransactionDtoRead>>(transactions);
    }
}