using Bogus;
using Microsoft.EntityFrameworkCore;
using WAB.DAL.Entities;

namespace WAB.DAL.Context;

public static class WabContextExtensions
{
    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.CardBalance)
                    .IsRequired()
                    .HasPrecision(10, 2);

                entity.HasMany(e => e.Transactions)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.DailyPoints)
                    .IsRequired()
                    .HasPrecision(10, 2);

                entity.Property(e => e.LastDailyPoints)
                    .IsRequired();
            }
        );

        modelBuilder.Entity<Transaction>(
            entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion<int>(); // Convert enum to int for storage in the database.

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasPrecision(10, 2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.Property(e => e.Pending)
                    .IsRequired();

                entity.Property(e => e.Icon)
                    .HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Transactions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.AuthorizedUser)
                    .WithMany(u => u.AuthorizedTransactions)
                    .HasForeignKey(e => e.AuthorizedUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Add other properties of the Transaction entity if available.
            }
        );
    }

    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var users = GenerateRandomUser();
        var transactions = users.SelectMany(u => u.Transactions).ToList();

        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Transaction>().HasData(transactions);
    }

    private static ICollection<User> GenerateRandomUser(DbContext dbContext, int numberOfUsers = 10)
    {
        var userId = 1;
        var usersFake = new Faker<User>()
            .RuleFor(u => u.Id, f => userId++)
            .RuleFor(u => u.CardBalance, f => f.Finance.Amount(0, 1500))
            .RuleFor(u => u.DailyPoints, f => 0)
            .RuleFor(u => u.LastDailyPoints, f => f.Date.Past())
            .RuleFor(u => u.Transactions,
                f => GenerateRandomTransactions(dbContext,
                    f.Random.Int(5, 15))); // Generate 5 to 15 transactions per user.

        return usersFake.Generate(numberOfUsers); // Generate the specified number of random users.
    }

    private static ICollection<Transaction> GenerateRandomTransactions(DbContext dbContext,
        int numberOfTransactions = 10)
    {
        var transactionsFake = new Faker<Transaction>()
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Amount, f => f.Finance.Amount(0, 1500))
            .RuleFor(t => t.Name, f => f.Finance.AccountName())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence())
            .RuleFor(t => t.Date, f => f.Date.Past())
            .RuleFor(t => t.Pending, f => f.Random.Bool())
            .RuleFor(t => t.Icon, f => f.Image.PicsumUrl())
            .RuleFor(t => t.UserId, f => f.Random.Int(1, 10))
            .RuleFor(t => t.AuthorizedUserId, f => f.Random.Bool() ? f.Random.Int(0, 10) : (int?) null);

        var transactions = transactionsFake.Generate(numberOfTransactions);

        // Assign unique positive Id values for seed data
        var currentMaxId = dbContext.Set<Transaction>().Max(t => (int?) t.Id) ?? 0;
        var nextId = Math.Max(1, currentMaxId + 1);

        foreach (var transaction in transactions) transaction.Id = nextId++;

        return transactions;
    }
}