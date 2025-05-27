using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using SchoolManagement.Infrastructure;
using SchoolManagement.ViewModels.Admin;

namespace SchoolManagement.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;
    private readonly ApplicationDbContext db;
    
    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext db)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.db = db;
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

        var user = new ApplicationUser()
        {
            UserName = inputModel.Username
        };
        var passwordHash = this.userManager.PasswordHasher.HashPassword(user, inputModel.Password);
        user.PasswordHash = passwordHash;
        user.Email = inputModel.Username;
        
        this.db.Users.Add(user);
        await this.db.SaveChangesAsync();
        var role = inputModel.Role == 1 ? "Student" : "Teacher";
        
        var teacherRoleId = await this.db.Roles.FirstOrDefaultAsync(x => x.Name == role);

        if (role == "Student")
        {
            var student = new Student
            {
                IdentityUserId = user.Id,
                Name = inputModel.Username
            };
            await this.db.Students.AddAsync(student);
        }
        

        await this.db.SaveChangesAsync();
        var newUser = this.db.Users.FirstOrDefault(x => x.UserName == inputModel.Username);
        await this.userManager.AddToRoleAsync(newUser, role);
        await this.db.SaveChangesAsync();

        return this.RedirectToAction(nameof(this.Index));
    }
    
    [HttpGet("/DeleteUser/{id}")]
    [CustomAuthorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await this.userManager.FindByIdAsync(id);

        this.db.Grades.Where(x => x.Student.IdentityUserId == id).ExecuteDelete();
        this.db.Students.Where(x => x.IdentityUserId == id).ExecuteDelete();
        await this.userManager.RemoveFromRoleAsync(user, "Student");
        await this.userManager.RemoveFromRoleAsync(user, "Teacher");
        await this.db.SaveChangesAsync();
        await this.userManager.DeleteAsync(user);

        return this.RedirectToAction(nameof(this.Index));
    }
}