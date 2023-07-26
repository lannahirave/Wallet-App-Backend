using Microsoft.EntityFrameworkCore;
using WAB.DAL.Context;
using WAB.DAL.Entities;
using WAB.DAL.Repositories.Abstract;

namespace WAB.DAL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly WabContext _context;

    public TransactionRepository(WabContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetAll()
    {
        return await _context.Transactions
            .Include(t => t.User).Include(t => t.AuthorizedUser).ToListAsync();
    }

    public async Task<Transaction?> Get(int id)
    {
        return await _context.Transactions
            .Include(t => t.User).Include(t => t.AuthorizedUser).FirstAsync(x => x.Id == id);
    }

    public async Task<ICollection<Transaction>> GetTransactionsByUserId(int userId)
    {
        return await _context.Transactions
            .Include(t => t.User).Include(t => t.AuthorizedUser).Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<ICollection<Transaction>> GetLastNTransactionsByUserId(int userId, int n)
    {
        return await _context.Transactions
            .Include(t => t.User).Include(t => t.AuthorizedUser).Where(x => x.UserId == userId).Take(n).ToListAsync();
    }
}