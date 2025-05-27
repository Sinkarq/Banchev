using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.ViewModels.Identity.Login;

namespace SchoolManagement.Controllers;

public class IdentityController : Controller
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    
    public IdentityController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet("/Login")]
    [HttpGet("/")]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return this.View();
    }
    
    [HttpPost("/Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginInputModel inputModel)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(inputModel);
        }
        
        var result = await this.signInManager.PasswordSignInAsync(
            inputModel.Username!,
            inputModel.Password!,
            isPersistent: false,
            lockoutOnFailure: false);
        
        if (result.Succeeded is false)
        {
            return this.RedirectToAction(nameof(Login));
        }
        
        var user = await this.userManager.FindByNameAsync(inputModel.Username!);
        if (await this.userManager.IsInRoleAsync(user, "Admin"))
        {
            return this.RedirectToAction("Index", "Admin");
        }
        return this.RedirectToAction("Grades", "Grade");
    }
}