namespace EmsisoftTest.Infrastructure.Interfaces;

public interface ICommandBuilder
{
    Task ExecuteAsync<TContext>(TContext context) where TContext : ICommandContext;
}