using Microsoft.AspNetCore.Identity;
using WebApi.Persistence;
using WebApi.Persistence.Models;

namespace WebApi.BLL;

public class AccountService(AccountRepository accountRepository, JwtService jwtService)
{
    public void Register(string registerRequestEmail, string registerRequestPassword)
    {
        var account = new Account()
        {
            Email = registerRequestEmail,
            Id = Guid.NewGuid(),
            Permissions = new List<Permission>()
            {
                new()
                {
                    Name = "Read"
                }
            }
        };
        var passHash = new PasswordHasher<Account>().HashPassword(account,registerRequestPassword);
        account.PasswordHash = passHash;
        accountRepository.Add(account);
    }

    public string Login(string registerRequestEmail, string registerRequestPassword)
    {
        var account = accountRepository.GetByEmail(registerRequestEmail);
        if (account == null) throw new ApplicationException("Invalid email or password");
        var result = new PasswordHasher<Account>().VerifyHashedPassword(account, account.PasswordHash, registerRequestPassword);
        if (result == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateToken(account);
        }
        throw new ApplicationException("Wrong email or password");
    }
}