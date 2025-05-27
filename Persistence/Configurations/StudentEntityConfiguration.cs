using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{

    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        
        builder.HasKey(s => s.Id);
        
        builder.HasOne(x => x.IdentityUser)
            .WithMany()
            .HasForeignKey(x => x.IdentityUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.Name).IsRequired();
        
        builder.HasMany(s => s.Grades)
            .WithOne(g => g.Student)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}