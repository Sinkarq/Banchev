using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure;
using SchoolManagement.ViewModels.Admin;

namespace SchoolManagement.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    
    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpGet("/Admin")]
    [CustomAuthorize(Roles = "Admin")]
    public IActionResult Index()
    {
        var users = this.userManager.Users.Include(x => x.Roles)
            .ToList();
        var roles = this.roleManager.Roles.ToList();
        return this.View((users, roles));
    }
    
    [HttpGet("/CreateUser")]
    [CustomAuthorize(Roles = "Admin")]
    public IActionResult CreateUser()
    {
        return this.View();
    }
    
    [HttpPost("/CreateUser")]
    [CustomAuthorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser(CreateUserInputModel inputModel)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(inputModel);
        }

        if (this.userManager.Users.Any(x => x.UserName == inputModel.Username))
        {
            this.ModelState.AddModelError(nameof(inputModel.Username), "Потребителското име вече съществува.");
            return this.View(inputModel);
        }

        var user = new ApplicationUser
        {
            UserName = inputModel.Username
        };
        
        await this.userManager.CreateAsync(user, inputModel.Password);
        var role = inputModel.Role == 1 ? "Student" : "Teacher";
        await this.userManager.AddToRoleAsync(user, role);

        return this.RedirectToAction(nameof(this.Index));
    }
    
    [HttpDelete("/DeleteUser/{id}")]
    [CustomAuthorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await this.userManager.FindByIdAsync(id);

        await this.userManager.DeleteAsync(user);

        return this.RedirectToAction(nameof(this.Index));
    }
}