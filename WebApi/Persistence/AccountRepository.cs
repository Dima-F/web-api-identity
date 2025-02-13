using WebApi.Persistence.Models;

namespace WebApi.Persistence;

public class AccountRepository
{
    private static IDictionary<string, Account> _accounts = new Dictionary<string, Account>();

    public void Add(Account account)
    {
        _accounts[account.Email] = account;
    }

    public Account? GetByEmail(string email)
    {
        return _accounts.TryGetValue(email, out var account) ? account : null;
    }

    public ICollection<Permission>? GetPermissionByEmail(string email)
    {
        var account = GetByEmail(email);
        return account?.Permissions;
    }
}