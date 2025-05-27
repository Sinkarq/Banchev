using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class GradeEntityConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades");
        
        builder.HasKey(g => g.Id);

        builder.HasOne(x => x.Student)
            .WithMany(x => x.Grades)
            .HasForeignKey(x => x.StudentId);

        builder.Property(x => x.Subject).IsRequired();
        builder.Property(x => x.Value).IsRequired();
    }
}