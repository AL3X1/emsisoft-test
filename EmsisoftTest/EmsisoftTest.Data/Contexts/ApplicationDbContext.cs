using EmsisoftTest.Data.Entities;
using EmsisoftTest.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<HashEntity> Hashes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        this.IncludeEntityFieldsAudition();
        return base.SaveChangesAsync(cancellationToken);
    }
}