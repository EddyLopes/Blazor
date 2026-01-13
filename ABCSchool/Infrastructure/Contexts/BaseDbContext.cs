using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts;

public abstract class BaseDbContext :
    MultiTenantIdentityDbContext<ApplicationUser,
                                 ApplicationRole,
                                 string,
                                 IdentityUserClaim<string>,
                                 IdentityUserRole<string>,
                                 IdentityUserLogin<string>,
                                 ApplicationRoleClaim,
                                 IdentityUserToken<string>>
{
    private new ABCSchoolTenatInfo TenantInfo { get; set; } 

    protected BaseDbContext(IMultiTenantContextAccessor<ABCSchoolTenatInfo> tenantInfoContextAccessor, DbContextOptions options) 
        : base(tenantInfoContextAccessor, options)
    {
        TenantInfo = tenantInfoContextAccessor.MultiTenantContext.TenantInfo!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if(!string.IsNullOrEmpty(TenantInfo?.ConnectionString))
        {
            optionsBuilder.UseMySql(TenantInfo.ConnectionString, 
                                    ServerVersion.AutoDetect(TenantInfo.ConnectionString), b =>
                                    {
                                        b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                                    });
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
