using EmsisoftTest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void IncludeEntityFieldsAudition(this DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries()
                .Where(x => x.Entity is IEntityBase &&
                            (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var entityBase = (IEntityBase) entity.Entity;

                if (entity.State == EntityState.Added) entityBase.CreatedAt = DateTime.UtcNow;

                entityBase.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}