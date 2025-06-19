using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<SystemCodeDetail> SystemCodeDetails { get; set; }
    public DbSet<SystemCode> SystemCodes { get; set; }
    //public DbSet<UserCreateActivity> UserCreateActivities { get; set; }
    //public DbSet<UserModifiedActivity> UserModifiedActivities { get; set; }
    //public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
    //public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
    //public DbSet<IdentityUserToken<string>> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
               .HasOne(x => x.Gender)
               .WithMany()
               .HasForeignKey(x => x.GenderId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
               .HasOne(x => x.MaritalStatus)
               .WithMany()
               .HasForeignKey(x => x.MaritalStatusId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ApplicationUser>()
               .HasOne(x => x.BloodGroup)
               .WithMany()
               .HasForeignKey(x => x.BloodGroupId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SystemCode>()
               .ToTable(nameof(SystemCode))
               .HasOne(x => x.ModifiedBy)
               .WithMany()
               .HasForeignKey(x => x.ModifiedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SystemCodeDetail>()
               .ToTable(nameof(SystemCodeDetail))
               .HasOne(x => x.ModifiedBy)
               .WithMany()
               .HasForeignKey(x => x.ModifiedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SystemCodeDetail>()
               .HasOne(x => x.SystemCode)
               .WithMany()
               .HasForeignKey(x => x.SystemCodeId)
               .OnDelete(DeleteBehavior.Restrict);


    }
}
