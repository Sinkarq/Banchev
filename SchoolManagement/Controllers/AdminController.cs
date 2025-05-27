using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Controllers;

public class AdminController : Controller
{
    [HttpGet("/Admin")]
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