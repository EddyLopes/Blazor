using Application.Exceptions;
using Application.Features.Identity.Users;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
using Infrastructure.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IMultiTenantContextAccessor<ABCSchoolTenatInfo> _tenantInfoContextAccessor;

    public UserService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        IMultiTenantContextAccessor<ABCSchoolTenatInfo> tenantInfoContextAccessor)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
        _tenantInfoContextAccessor = tenantInfoContextAccessor;
    }

    public async Task<string> ActivateOrDeactivateAsync(string userId, bool activation)
    {
        var user = await GetUserAsync(userId);

        user.IsActive = activation;
        
        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded)
        {
            throw new IdentityException(IdentityHelper.GetIdentityResultErrorDescriptions(result));
        }
        
        return userId;
    }

    public async Task<string> AssignRolesAsync(string userId, UserRolesRequest request)
    {
        var user = await GetUserAsync(userId);
        if(await _userManager.IsInRoleAsync(user, RoleConstants.Admin)
            && request.UserRoles.Any(x => !x.IsAssigned && x.Name == RoleConstants.Admin))
        {
            if(user.Email == TenancyConstants.Root.Email)
            {
                if(_tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Id == TenancyConstants.Root.Id)
                {
                    throw new ConflictException(["Not allowed to remove Admin role for a Root Tenant User."]);
                }
            }
            else
            {

            }
        }
    }

    public Task<string> ChangePasswordAsync(ChangePassworRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResponse>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse> GetByIdAsync(string userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetUserPermissionsAsync(string userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserRoleResponse>> GetUserRolesAsync(string userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailTokenAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPermissionsAssignedAsync(string userId, string permission, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }

    private async Task<ApplicationUser> GetUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(["User does not exist."]);

        return user;
    }
}
