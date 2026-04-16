using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tenancy.Queries;

public class GetTenantByIdQuery : IRequest<IResponseWrapper>
{
    public string TenantId { get; set; }
}

public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, IResponseWrapper>
{
    private readonly ITenantService _tenantService;

    public GetTenantByIdQueryHandler(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public async Task<IResponseWrapper> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantService.GetTenantByIdAsync(request.TenantId);
        
        if(tenant is null)
        {
            return await ResponseWrapper<TenantResponse>.FailAsync(message: "Tenant not found");
        }

        return await ResponseWrapper<TenantResponse>.SuccessAsync(data: tenant, message: "Tenant found successfully.");
    }
}
