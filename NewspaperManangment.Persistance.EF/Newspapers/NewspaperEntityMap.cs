﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.Newspapers
{
    public class NewspaperEntityMap : IEntityTypeConfiguration<Newspaper>
    {
        public void Configure(EntityTypeBuilder<Newspaper> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Title).IsRequired().HasMaxLength(50);
            builder.Property(_ => _.PublishDate);

           
        }
    }
}
