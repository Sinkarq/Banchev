namespace Domain;

public sealed class Student 
{
    public Student()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    public string Id { get; set; }
    public string IdentityUserId { get; set; }
    public ApplicationUser IdentityUser { get; set; } = null!;
    public string Name { get; set; }
    
    public HashSet<Grade> Grades { get; set; } = [];
}