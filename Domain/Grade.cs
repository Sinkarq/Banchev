namespace Domain;

public sealed class Grade
{
    public Grade()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    public string Id { get; set; }
    public string StudentId { get; set; }
    public Student Student { get; set; }
    public string CreatedByName { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Subject { get; set; }
    public int Value { get; set; }
}