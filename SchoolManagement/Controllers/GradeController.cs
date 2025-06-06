﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using SchoolManagement.Infrastructure;
using SchoolManagement.ViewModels.Grade;

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
                .Include(x => x.Student)
                .ToListAsync();
            list = grades;
        }

        if (userRole == "Teacher")
        {
            var teacherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var grades = await this.dbContext.Grades
                .Where(x => x.CreatedById == teacherId)
                .Include(x => x.Student)
                .ToListAsync();
            list = grades;
        }

        if (userRole == "Student")
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await this.dbContext.Students
                .Where(x => x.IdentityUserId == userId)
                .Select(x => new { x.Id })
                .FirstOrDefaultAsync();
            var grades = await this.dbContext.Grades
                .Where(x => x.StudentId == student.Id)
                .Include(x => x.Student)
                .ToListAsync();
            list = grades;
        }

        return this.View(list);
    }

    [HttpGet("/Grades/Create")]
    [CustomAuthorize(Roles = "Teacher,Admin")]
    public IActionResult CreateGrade()
    {
        return this.View();
    }

    [HttpPost("/Grades/Create")]
    [CustomAuthorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> CreateGrade(CreateGradeInputModel inputModel)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(inputModel);
        }

        var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = await this.dbContext.Users.Where(x => x.Id == id).Select(x => x.UserName).FirstOrDefaultAsync();

        var grade = new Domain.Grade
        {
            StudentId = inputModel.StudentId,
            CreatedById = id,
            CreatedByName = name,
            Value = inputModel.Value,
            Subject = inputModel.Subject,
            CreatedOn = DateTime.Now
        };
        await this.dbContext.Grades.AddAsync(grade);
        await this.dbContext.SaveChangesAsync();

        return this.RedirectToAction(nameof(this.Grades));
    }
    
    [HttpGet("/Grades/Delete/{gradeId}")]
    public async Task<IActionResult> DeleteGrade(string gradeId)
    {
        await this.dbContext.Grades.Where(x => x.Id == gradeId).ExecuteDeleteAsync();

        return this.RedirectToAction(nameof(this.Grades));
    }
}