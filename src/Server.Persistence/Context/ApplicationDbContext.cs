using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Persistence.Identity;
using System;
using DomainUser = Server.Domain.Entities.User;

namespace Server.Persistence.Context;

// Ensure that 'User' inherits from IdentityUser<Guid> and 'UserRole' inherits from IdentityRole<Guid>
public class ApplicationDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public DbSet<DomainUser> UsersDomain { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DomainUser>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
        });

        builder.Entity<User>().ToTable("AuthUsers");
        builder.Entity<UserRole>().ToTable("AuthRoles");

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("AuthUserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("AuthUserLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("AuthUserTokens");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AuthRoleClaims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("AuthUserRoles");
    }
}
