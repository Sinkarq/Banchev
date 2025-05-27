using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.Identity.Login;

public sealed class LoginInputModel
{
    [Required(ErrorMessage = "Попълнете потребителското име.")]
    public string? Username { get; set; }
    
    [Required(ErrorMessage = "Попълнете паролата.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}