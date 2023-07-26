using Microsoft.EntityFrameworkCore;
using WAB.DAL.Entities;

namespace WAB.DAL.Context;

public class WabContext : DbContext
{
    public WabContext(DbContextOptions<WabContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Configure();
        modelBuilder.SeedData();
    }
}