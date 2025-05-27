using Domain;

namespace SchoolManagement.Services.Grades;

public interface IGradesService
{
    List<Grade> GetGrades();
    Grade GetGradeById(string gradeId);
    void AddGrade(Grade grade);
    void UpdateGrade(Grade grade);
    void DeleteGrade(string gradeId);
}
