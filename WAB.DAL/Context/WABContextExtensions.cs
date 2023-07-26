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
            }
        );
    }

    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Create a Faker instance for generating fake data
        var faker = new Faker();

        // Generate random users
        var users = new Faker<User>()
            .RuleFor(u => u.Id, f => f.IndexFaker - 1000) // Use negative values for User Ids
            .RuleFor(u => u.CardBalance, f => f.Random.Decimal(0, 1500))
            .RuleFor(u => u.DailyPoints, f => Math.Round(f.Random.Double(0, 100), 2))
            .RuleFor(u => u.LastDailyPoints, f => f.Date.Past())
            .Generate(10);

        // Add the generated users to the DbContext
        modelBuilder.Entity<User>().HasData(users);

        // Generate random transactions
        var transactions = new Faker<Transaction>()
            .RuleFor(t => t.Id, f => f.IndexFaker - 2000) // Use negative values for Transaction Ids
            .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(t => t.Amount, f => f.Finance.Amount(10, 500))
            .RuleFor(t => t.Name, f => f.Commerce.ProductName())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence(5))
            .RuleFor(t => t.Date, f => f.Date.Past().ToUniversalTime()) // Convert to UTC
            .RuleFor(t => t.Pending, f => f.Random.Bool())
            .RuleFor(t => t.Icon, f => f.Image.PicsumUrl())
            .RuleFor(t => t.UserId, f => f.PickRandom(users).Id) // Assign random UserId to each transaction
            .Generate(400);
        // Assign random AuthorizedUserId to 30% of the transactions
        foreach (var transaction in transactions)
        {
            var hasAuthorizedUser = faker.Random.Bool((float) 0.3);
            if (hasAuthorizedUser) transaction.AuthorizedUserId = users[faker.Random.Int(0, users.Count - 1)].Id;
        }

        // Add the generated transactions to the DbContext
        modelBuilder.Entity<Transaction>().HasData(transactions);
    }
}