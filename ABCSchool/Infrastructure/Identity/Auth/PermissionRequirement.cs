using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Identity.Auth;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; set; }
}
