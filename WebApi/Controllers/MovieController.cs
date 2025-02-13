using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.BLL;

namespace WebApi.Controllers;

[ApiController]
// [Authorize(Roles = "Admin")]
// [Authorize(Policy = "OnlyForMicrosoft")]
[Route("[controller]")]
public class MovieController: ControllerBase
{
    [HttpGet("all")]
    [Authorize(Permissions.Read)]
    public IActionResult GetMovies()
    {
        return Ok(new List<object>
        {
            new
            {
                Name = "Xena",
                Duration = TimeSpan.FromHours(2)
            },
            new
            {
                Name = "Terminator",
                Duration = TimeSpan.FromHours(3)
            }
        });
    }
    
    [HttpDelete]
    [Authorize(Permissions.Delete)]
    public IActionResult DeleteMovies()
    {
        return Ok();
    }
}