using Application.Features.Tenancy;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tenancy;

public class TenantService : ITenantService
{
    private readonly IMultiTenantStore<ABCSchoolTenatInfo> _tenantStore;
    private readonly ApplicationDbSeeder _dbSeeder;
    private readonly IServiceProvider _serviceProvider;

    public TenantService(IMultiTenantStore<ABCSchoolTenatInfo> tenantStore, ApplicationDbSeeder dbSeeder, IServiceProvider serviceProvider)
    {
        _tenantStore = tenantStore;
        _dbSeeder = dbSeeder;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> ActivateAsync(string id)
    {
        var tenantInDb = await _tenantStore.TryGetAsync(id);
        tenantInDb.IsActive = true;

        await _tenantStore.TryUpdateAsync(tenantInDb);
        return tenantInDb.Identifier;
    }

    public async Task<string> CreateTenantAsync(CreateTenantRequest createTenant, CancellationToken ct)
    {
        var newTenant = new ABCSchoolTenatInfo
        {
            Id = createTenant.Identifier,
            Identifier = createTenant.Identifier,
            Name = createTenant.Name,
            IsActive = createTenant.IsActive,
            ConnectionString = createTenant.ConnectionString,
            Email = createTenant.Email,
            FirstName = createTenant.FirstName,
            LastName = createTenant.LastName,
            ValidUpTo = createTenant.ValidUpTo,
        };

        await _tenantStore.TryAddAsync(newTenant);

        using var scope = _serviceProvider.CreateScope();

        _serviceProvider.GetRequiredService<IMultiTenantContextSetter>()
                        .MultiTenantContext = new MultiTenantContext<ABCSchoolTenatInfo>()
                        {
                            TenantInfo = newTenant
                        };

        await scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>()
                                   .InitializeDatabaseAsync(ct);

        return newTenant.Identifier;
    }

    public async Task<string> DeactivateAsync(string id)
    {
        var tenantInDb = await _tenantStore.TryGetAsync(id);
        tenantInDb.IsActive = false;

        await _tenantStore.TryUpdateAsync(tenantInDb);
        return tenantInDb.Identifier;
    }

    public async Task<TenantResponse> GetTenantByIdAsync(string id)
    {
        var tenantInDb = await _tenantStore.TryGetAsync(id);

        #region Manual mapping
        //var tenantResponse = new TenantResponse
        //{
        //    Identifier = tenantInDb.Identifier,
        //    Name = tenantInDb.Name,
        //    ConnectionString = tenantInDb.ConnectionString,
        //    Email = tenantInDb.Email,
        //    FirstName = tenantInDb.FirstName,
        //    LastName = tenantInDb.LastName,
        //    IsActive = tenantInDb.IsActive,
        //    ValidUpTo = tenantInDb.ValidUpTo
        //};
        #endregion

        //Mapster
        return tenantInDb.Adapt<TenantResponse>();
    }

    public Task<List<TenantResponse>> GetTenantsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateSubscriptionAsync(UpdateTenantSubscriptionRequest updateRequest)
    {
        throw new NotImplementedException();
    }
}
