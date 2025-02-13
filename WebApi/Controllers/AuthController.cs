using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(AccountService accountService):ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest registerRequest)
    {
        accountService.Register(registerRequest.Email, registerRequest.Password);
        return Ok();
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest registerRequest)
    {
        var token = accountService.Login(registerRequest.Email, registerRequest.Password);
        // так можна добавити токен в куки
        // HttpContext.Response.Cookies.Append("token", token, new CookieOptions(){ HttpOnly = true});
        return Ok(token);
    }
}