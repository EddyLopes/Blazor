using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tenancy;

public class TenantDbSeeder : ITenantDbSeeder
{
    private readonly TenantDbContext _tenantDbContext;
    private readonly IServiceProvider _serviceProvider;

    public TenantDbSeeder(TenantDbContext tenantDbContext, IServiceProvider serviceProvider)
    {
        _tenantDbContext = tenantDbContext;
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeDatabaseAsync(CancellationToken ct)
    {
        await InitializeDatabaseWithTenantAsync(ct);

        foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(ct))
        {
            await InitializeApplicationDbForTenantAsync(tenant, ct);
        }
    }

    private async Task InitializeDatabaseWithTenantAsync(CancellationToken ct)
    {
        if (await _tenantDbContext.TenantInfo.FindAsync([TenancyConstants.Root.Id], ct) is null)
        {
            var rootTenant = new ABCSchoolTenatInfo
            {
                Id = TenancyConstants.Root.Id,
                Identifier = TenancyConstants.Root.Id,
                Name = TenancyConstants.Root.Name,
                Email = TenancyConstants.Root.Email,
                FirstName = TenancyConstants.FirstName,
                LastName = TenancyConstants.LastName,
                IsActive = true,
                ValidUpTo = DateTime.UtcNow.AddYears(2)
            };

            await _tenantDbContext.TenantInfo.AddAsync(rootTenant, ct);
            await _tenantDbContext.SaveChangesAsync(ct);
        }
    }

    private async Task InitializeApplicationDbForTenantAsync(ABCSchoolTenatInfo currentTenant, CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        _serviceProvider.GetRequiredService<IMultiTenantContextSetter>()
            .MultiTenantContext = new MultiTenantContext<ABCSchoolTenatInfo>()
            {
                TenantInfo = currentTenant,
            };

        await scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>()
            .InitializeDatabaseAsync(ct);
    }
}
