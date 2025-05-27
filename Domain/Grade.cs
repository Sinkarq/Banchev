namespace Domain;

public sealed class Grade
{
    public Grade()
    {
        this.Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    public string StudentId { get; set; }
    public Student Student { get; set; }
    public string Subject { get; set; }
    public int Value { get; set; }
}