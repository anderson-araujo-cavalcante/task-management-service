using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Data.Mappings
{
    public class TaskCommentMap : IEntityTypeConfiguration<TaskComment>
    {
        private const string TABLE_NAME = "TaskComments";

        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.ProjectTaskId)
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .IsRequired();

            builder.Property(c => c.Comment)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
