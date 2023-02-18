using EmsisoftTest.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Processor.Extensions;

public static class HostExtensions
{
    public static async Task ApplyMigrationsAsync(this IHost builder)
    {
        var dbContext = builder.Services.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}