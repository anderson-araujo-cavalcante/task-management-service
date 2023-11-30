using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Mappings
{
    public class ProjectTaskMap : IEntityTypeConfiguration<ProjectTask>
    {
        private const string TABLE_NAME = "Tasks";

        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.ExpirationDate)
                .IsRequired();

            builder.HasOne(u => u.Project)
                .WithMany()
                .HasForeignKey(u => u.ProjectId)
                .IsRequired(true);
        }
    }
}
