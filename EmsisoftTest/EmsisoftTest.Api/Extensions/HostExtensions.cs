using EmsisoftTest.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Api.Extensions;

public static class HostExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}