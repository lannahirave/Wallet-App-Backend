using Microsoft.EntityFrameworkCore;
using WAB.DAL.Context;
using WAB.DAL.Entities;
using WAB.DAL.Repositories.Abstract;

namespace WAB.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WabContext _context;

    public UserRepository(WabContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users
            .Include(u => u.Transactions).ToListAsync();
    }

    public async Task<User?> Get(int id)
    {
        var user = await _context.Users
            .Include(u => u.Transactions).FirstAsync(x => x.Id == id);
        user.CalculateDailyPoints();
        await _context.SaveChangesAsync();
        return user;
    }
}