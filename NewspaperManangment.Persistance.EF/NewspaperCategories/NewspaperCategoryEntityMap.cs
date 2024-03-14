using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.NewspaperCategories
{
    public class NewspaperCategoryEntityMap : IEntityTypeConfiguration<NewspaperCategory>
    {
        public void Configure(EntityTypeBuilder<NewspaperCategory> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

          
            builder.HasOne(_ => _.Newspaper)
           .WithMany(_ => _.NewspaperCategories)
            .HasForeignKey(_ => _.NewspaperId)
            .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(_ => _.Category)
                .WithMany(_ => _.NewspaperCategories)
                .HasForeignKey(_ => _.CategoryId).IsRequired();
        }
    }
}
