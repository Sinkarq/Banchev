using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModels.Grade;

public sealed class CreateGradeInputModel
{
    public string StudentId { get; set; } = null!;
    
    [Required(ErrorMessage = "Напишете предмета!")]
    public string Subject { get; set; } = null!;
    
    [Required(ErrorMessage = "Напишете оценката!")]
    public int Value { get; set; }
}