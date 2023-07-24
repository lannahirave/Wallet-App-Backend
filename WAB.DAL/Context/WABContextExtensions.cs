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
                    .WithMany()
                    .HasForeignKey(e => e.AuthorizedUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Add other properties of the Transaction entity if available.
            }
        );
    }
}