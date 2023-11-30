using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Mappings
{
    public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        private const string TABLE_NAME = "Projects";

        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.UserId)
            .IsRequired();
        }
    }
}
