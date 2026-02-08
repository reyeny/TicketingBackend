using Microsoft.EntityFrameworkCore;
using Ticketing.Auth.Infrastructure.Models;

namespace Ticketing.Auth.Infrastructure.Context;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(b =>
        {
            b.ToTable("Users");
            b.HasKey(x => x.Id);

            b.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();
            b.HasIndex(x => x.Email)
                .IsUnique();

            b.Property(x => x.PasswordHash)
                .HasMaxLength(256)
                .IsRequired();

            b.Property(x => x.CreatedAtUts)
                .IsRequired();
        });
    }
}