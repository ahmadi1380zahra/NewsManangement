using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.TheNews
{
    public class TheNewEntityMap : IEntityTypeConfiguration<TheNew>
    {
        public void Configure(EntityTypeBuilder<TheNew> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.Description).HasMaxLength(200).IsRequired();
            builder.Property(_ => _.Rate).IsRequired();
            builder.Property(_ => _.View).IsRequired();

            builder.HasOne(_ => _.Author)
                .WithMany(_ => _.TheNews)
                .HasForeignKey(_ => _.AuthorId)
                .IsRequired();
            builder.HasOne(_ => _.NewspaperCategory)
             .WithMany(_ => _.TheNews)
             .HasForeignKey(_ => _.NewspaperCategoryId)
             .OnDelete(DeleteBehavior.Cascade)
             .IsRequired();
        }
    }
}
