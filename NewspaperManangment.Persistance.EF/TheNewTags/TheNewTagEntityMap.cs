using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.TheNewTags
{
    public class TheNewTagEntityMap : IEntityTypeConfiguration<TheNewTag>
    {
        public void Configure(EntityTypeBuilder<TheNewTag> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.HasOne(_ => _.Tag)
                .WithMany(_ => _.TheNewTags)
                .HasForeignKey(_ => _.TagId)
                .IsRequired();
                
            builder.HasOne(_ => _.TheNew)
                 .WithMany(_ => _.TheNewTags)
                 .HasForeignKey(_ => _.TheNewId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
