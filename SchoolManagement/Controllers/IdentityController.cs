using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Controllers;

public class IdentityController : Controller
{
    [HttpGet("/Login")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return this.View();
    }
}