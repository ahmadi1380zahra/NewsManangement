using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.Tags
{
    public class TagEntityMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_=>_.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

            builder.HasOne(_ => _.Category)
                .WithMany(_ => _.Tags)
                .HasForeignKey(_ => _.CategoryId).IsRequired();
        }
    }
}
