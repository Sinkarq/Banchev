using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.Identity.Login;

namespace SchoolManagement.Controllers;

public class IdentityController : Controller
{
    private readonly SignInManager<ApplicationUser> signInManager;
    public IdentityController(SignInManager<ApplicationUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    [HttpGet("/Login")]
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

        if (result.Succeeded)
        {
            return this.RedirectToAction("Index", "Admin");
        }
        
        return this.Ok();
    }
}