using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangment.Entities;

namespace NewspaperManangment.Persistance.EF.Comments;

public class CommentEntityMap:IEntityTypeConfiguration<Entities.Comment>
{
    public void Configure(EntityTypeBuilder<Entities.Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Id).ValueGeneratedOnAdd();
        builder.Property(_ => _.comment).HasMaxLength(255).IsRequired();

        builder.HasOne(_ => _.TheNew)
            .WithMany(_=>_.Comments)
            .HasForeignKey(_ => _.TheNewId).IsRequired();
        builder.HasOne(_ => _.Comment1)
            .WithMany(_ => _.theNew_Comments)
            .HasForeignKey(_ => _.ReplyId).IsRequired(false);
    }
}