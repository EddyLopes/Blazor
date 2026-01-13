using Domain.Entities;
using Finbuckle.MultiTenant;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts;

internal class DbConfigurations 
{
    internal class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //builder.ToTable("Users","Identity")
            builder.ToTable("Users")
                   .IsMultiTenant();
        }
    }

    internal class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            //builder.ToTable("Roles","Identity")
            builder.ToTable("Roles")
                   .IsMultiTenant();

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(123);
        }
    } 

    internal class ApplicationRoleClaimConfig : IEntityTypeConfiguration<ApplicationRoleClaim>
    {
        public void Configure(EntityTypeBuilder<ApplicationRoleClaim> builder)
        {
            //builder.ToTable("RoleClaims","Identity")
            builder.ToTable("RoleClaims")
                   .IsMultiTenant();
        }
    }

    internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //builder.ToTable("UserRoles","Identity")
            builder.ToTable("UserRoles")
                   .IsMultiTenant();
        }
    }

    internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            //builder.ToTable("UserClaims","Identity")
            builder.ToTable("UserClaims")
                   .IsMultiTenant();
        }
    }

    internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            //builder.ToTable("UserLogins","Identity")
            builder.ToTable("UserLogins")
                   .IsMultiTenant();
        }
    }

    internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            //builder.ToTable("UserTokens","Identity")
            builder.ToTable("UserTokens")
                   .IsMultiTenant();
        }
    }

    internal class SchoolConfig : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            //builder.ToTable("Schools", "Academics")
            builder.ToTable("Schools")
                   .IsMultiTenant();

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(60);
        }
    }
}
