using WAB.DAL.Entities;

namespace WAB.DAL.Repositories.Abstract;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<ICollection<Transaction>> GetTransactionsByUserId(int userId);
    Task<ICollection<Transaction>> GetLastNTransactionsByUserId(int userId, int n);
}