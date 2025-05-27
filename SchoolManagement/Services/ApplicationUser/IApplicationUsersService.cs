using Domain;

namespace SchoolManagement.Services.ApplicationUser;

public interface IApplicationUsersService
{
    Domain.ApplicationUser GetById(string userId);
    Domain.ApplicationUser GetUsers();
    Domain.ApplicationUser Create(string username, string password, UserCreationRole role);
    Task Login(string username, string password);

    public enum UserCreationRole
    {
        Student = 1,
        Teacher = 2,
        Administrator = 3
    }
}

