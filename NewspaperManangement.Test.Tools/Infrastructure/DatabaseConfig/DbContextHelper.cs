using Microsoft.EntityFrameworkCore;

namespace NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig
{
    public static class DbContextHelper
    {
        public static void Save<TDbContext, TEntity>(
      this TDbContext dbContext,
      TEntity entity)
      where TDbContext : DbContext
      where TEntity : class
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }
    }
}
