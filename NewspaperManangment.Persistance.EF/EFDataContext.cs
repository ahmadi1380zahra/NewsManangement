using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;


namespace NewspaperManangment.Persistance.EF;

public class EFDataContext : DbContext
{
    public EFDataContext(string connectionString) :
        this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
    { }


    public EFDataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Newspaper> Newspapers { get; set; }
    public DbSet<NewspaperCategory> NewspaperCategories { get; set; }
    public DbSet<TheNew> TheNews { get; set; }
    public DbSet<TheNewTag> TheNewTags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(EFDataContext).Assembly);
    }
}