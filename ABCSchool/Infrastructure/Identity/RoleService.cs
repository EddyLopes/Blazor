using Application.Exceptions;
using Application.Features.Identity.Roles;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Constants;
using Infrastructure.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IMultiTenantContextAccessor<ABCSchoolTenatInfo> _tenantInfoContextAccessor;

    public RoleService(
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

    public async Task<string> CreateAsync(CreateRoleRequest request)
    {
        var newRole = new ApplicationRole
        {
            Name = request.Name,
            Description = request.Description,
        };

        var result = await _roleManager.CreateAsync(newRole);
        if (!result.Succeeded)
        {
            throw new IdentityException(GetIdentityResultErrorDescriptions(result));
        }

        return newRole.Name;
    }

    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id)
            ?? throw new NotFoundException(["Role does not exist."]);

        if (RoleConstants.IsDefaultRole(role.Name ?? ""))
        {
            throw new ConflictException([$"Not allowed to delete '{role.Name}' role."]);
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name ?? "")).Count > 0)
        {
            throw new ConflictException([$"Not allowed to delete '{role.Name}' role as it is currently assigned to users."]);
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            throw new IdentityException(GetIdentityResultErrorDescriptions(result));
        }

        return role.Name ?? "";
    }

    public async Task<bool> DoesItExistsAsync(string name)
    {
        return await _roleManager.RoleExistsAsync(name);
    }

    public async Task<List<RoleResponse>> GetAllAsync(CancellationToken ct)
    {
        var roles = await _roleManager.Roles.ToListAsync(ct);
        return roles.Adapt<List<RoleResponse>>();
    }

    public async Task<RoleResponse> GetByIdAsync(string id, CancellationToken ct)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(role => role.Id == id, ct)
            ?? throw new NotFoundException(["Role does not exist."]);
        return role.Adapt<RoleResponse>();
    }

    public async Task<RoleResponse> GetRoleWithPermissionsAsync(string id, CancellationToken ct)
    {
        var role = await GetByIdAsync(id, ct);

        role.Permissions = await _context.RoleClaims
            .Where(x => x.RoleId == id && x.ClaimType == ClaimConstants.Permission)
            .Select(x => x.ClaimValue ?? "")
            .ToListAsync(ct);

        return role;
    }

    public async Task<string> UpdateAsync(UpdateRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.Id)
            ?? throw new NotFoundException(["Role does not exist."]);

        if (RoleConstants.IsDefaultRole(role.Name ?? ""))
        {
            throw new ConflictException([$"Changes not allowed on system role '{role.Name}'."]);
        }

        role.Name = request.Name;
        role.Description = request.Description;
        role.NormalizedName = request.Name.ToUpperInvariant();

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            throw new IdentityException(GetIdentityResultErrorDescriptions(result));
        }

        return role.Name;
    }

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId)
            ?? throw new NotFoundException(["Role does not exist."]);

        if (role.Name == RoleConstants.Admin)
        {
            throw new ConflictException([$"Not allowed to change permissions for '{role.Name}' role."]);
        }

        if (_tenantInfoContextAccessor.MultiTenantContext.TenantInfo.Id != TenancyConstants.Root.Id)
        {
            request.NewPermissions.RemoveAll(x => x.StartsWith("Permission.Tenants."));
        }

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        foreach (var claim in currentClaims.Where(c => !request.NewPermissions.Any(p => p == c.Value)))
        {
            var result = await _roleManager.RemoveClaimAsync(role, claim);
            if (!result.Succeeded)
            {
                throw new IdentityException(GetIdentityResultErrorDescriptions(result));
            }
        }

        foreach (var newPermission in request.NewPermissions.Where(p => !currentClaims.Any(c => c.Value == p)))
        {
            await _context.RoleClaims.AddAsync(new ApplicationRoleClaim
            {
                RoleId = role.Id,
                ClaimType = ClaimConstants.Permission,
                ClaimValue = newPermission,
                Description = "",
                Group = ""
            });
        }
        
        await _context.SaveChangesAsync();

        return "Permissions updated successfully.";
    }

    private List<string> GetIdentityResultErrorDescriptions(IdentityResult identityResult)
    {
        var errorDescriptions = new List<string>();

        foreach (var error in identityResult.Errors)
        {
            errorDescriptions.Add(error.Description);
        }

        return errorDescriptions;
    }
}
