using Microsoft.AspNetCore.Authorization;

namespace WebApi.BLL;

public class PermissionRequirements: IAuthorizationRequirement
{
    public PermissionRequirements(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; set; }
}