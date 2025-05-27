using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Infrastructure;

namespace SchoolManagement.Controllers;

public class AdminController : Controller
{
    [HttpGet("/Admin")]
    [CustomAuthorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return this.View();
    }
    
    [HttpGet("/CreateUser")]
    public IActionResult CreateUser()
    {
        return this.View();
    }
    
    
}