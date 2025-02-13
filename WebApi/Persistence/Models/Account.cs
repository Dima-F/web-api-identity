using System.ComponentModel.DataAnnotations;

namespace WebApi.Persistence.Models;

public class Account
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Permission>? Permissions { get; set; }  = [];
}

public class Permission
{
    public string Name { get; set; }
}