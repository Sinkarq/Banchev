using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using SchoolManagement.Infrastructure;

namespace SchoolManagement.Controllers;

public class GradeController : Controller
{
    private readonly ApplicationDbContext dbContext;
    public GradeController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("/Grades")]
    [CustomAuthorize(Roles = "Student,Teacher,Admin")]
    public async Task<IActionResult> Grades()
    {
        var userRole = this.User.FindFirstValue(ClaimTypes.Role);
        var list = new List<Domain.Grade>();
        if (userRole == "Admin")
        {
            var grades = await this.dbContext.Grades
                .Where(x => x.StudentId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
            list = grades;
        }
        
        if (userRole == "Teacher")
        {
            var teacherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var grades = await this.dbContext.Grades
                .Where(x => x.CreatedById == teacherId)
                .ToListAsync();
            list = grades;
        }
        
        if (userRole == "Student")
        {
            var studentId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var grades = await this.dbContext.Grades
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
            list = grades;
        }
        
        return this.View(list);
    }
}