using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModels.Admin;

public sealed class CreateUserInputModel
{
    [Required(ErrorMessage = "Попълнете потребителското име.")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Изберете роля.")]
    public int Role { get; set; }
    [Required(ErrorMessage = "Попълнете паролата.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}