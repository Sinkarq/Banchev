using Domain;

namespace SchoolManagement.Services.Students;

public interface IStudentsService
{
    List<Student> GetStudents();
    Student GetStudentById(string studentId);
    void AddStudent(Student student);
    void UpdateStudent(Student student);
    void DeleteStudent(string studentId);
}