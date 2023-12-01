using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Mappings
{
    public class HistoricMap : IEntityTypeConfiguration<Historic>
    {
        private const string TABLE_NAME = "Tasks";

        public void Configure(EntityTypeBuilder<Historic> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.ProjectTaskId)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.PropertyName)
                 .IsRequired()
                 .HasMaxLength(20);

            builder.Property(c => c.OldValue)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.NewValue)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
