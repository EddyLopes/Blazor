using System.Collections.ObjectModel;

namespace Infrastructure.Constants;

public static class SchoolAction
{
    public const string Read = nameof(Read);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string RefreshToken = nameof(RefreshToken);
    public const string UpgradeSubscrition = nameof(UpgradeSubscrition);
}

public static class SchoolFeature
{
    public const string Tenants = nameof(Tenants);
    public const string Users = nameof(Users);
    public const string Roles = nameof(Roles);
    public const string UserRoles = nameof(UserRoles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Schools = nameof(Schools);
    public const string Tokens = nameof(Tokens);
}

public record SchoolPermission(string Action, string Feature, string Description, string Group, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Feature);
    public static string NameFor(string action, string feature) => $"Permission.{feature}.{action}";
}

public static class SchoolPermissions
{
    private static readonly SchoolPermission[] _allPermissions = new[]
    {
        new SchoolPermission(Action: SchoolAction.Create, Feature: SchoolFeature.Tenants, Description: "Create Tenants", Group: "Tenancy", IsRoot: true),
        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.Tenants, Description: "Read Tenants", Group: "Tenancy", IsRoot: true),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.Tenants, Description: "Update Tenants", Group: "Tenancy", IsRoot: true),
        new SchoolPermission(Action: SchoolAction.UpgradeSubscrition, Feature: SchoolFeature.Tenants, Description: "Upgrade Tenant's Subscrition", Group: "Tenancy", IsRoot: true),

        new SchoolPermission(Action: SchoolAction.Create, Feature: SchoolFeature.Users, Description: "Create Users", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.Users, Description: "Update Users", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Delete, Feature: SchoolFeature.Users, Description: "Delete Users", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.Users, Description: "Read Users", Group : "SystemAccess"),

        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.UserRoles, Description: "Read User Roles", Group : "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.UserRoles, Description: "Update User Roles", Group : "SystemAccess"),

        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.Roles, Description: "Read Roles", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Create, Feature: SchoolFeature.Roles, Description: "Create Roles", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.Roles, Description: "Update Roles", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Delete, Feature: SchoolFeature.Roles, Description: "Delete Roles", Group: "SystemAccess"),

        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.RoleClaims, Description: "Read Role Claims/Permissions", Group: "SystemAccess"),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.RoleClaims, Description: "Update Role Claims/Permissions", Group: "SystemAccess"),

        new SchoolPermission(Action: SchoolAction.Read, Feature: SchoolFeature.Schools, Description: "Read Schools", Group: "Academics", IsBasic: true),
        new SchoolPermission(Action: SchoolAction.Create, Feature: SchoolFeature.Schools, Description: "Create Schools", Group: "Academics"),
        new SchoolPermission(Action: SchoolAction.Update, Feature: SchoolFeature.Schools, Description: "Update Schools", Group: "Academics"),
        new SchoolPermission(Action: SchoolAction.Delete, Feature: SchoolFeature.Schools, Description: "Delete Schools", Group: "Academics"),

        new SchoolPermission(Action: SchoolAction.RefreshToken, Feature: SchoolFeature.Tokens, Description: "Generate Refresh Token", Group: "SystemAccess", IsBasic: true)
    };

    public static IReadOnlyList<SchoolPermission> All { get; }
        = new ReadOnlyCollection<SchoolPermission>(_allPermissions);

    public static IReadOnlyList<SchoolPermission> Root { get; }
        = new ReadOnlyCollection<SchoolPermission>(_allPermissions.Where(x => x.IsRoot).ToArray());

    public static IReadOnlyList<SchoolPermission> Admin { get; }
        = new ReadOnlyCollection<SchoolPermission>(_allPermissions.Where(x => !x.IsRoot).ToArray());

    public static IReadOnlyList<SchoolPermission> Basic { get; }
        = new ReadOnlyCollection<SchoolPermission>(_allPermissions.Where(x => x.IsBasic).ToArray());
}
