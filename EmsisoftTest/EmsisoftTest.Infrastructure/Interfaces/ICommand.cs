namespace EmsisoftTest.Infrastructure.Interfaces;

public interface ICommand<in TContext> where TContext : ICommandContext
{
    Task ExecuteAsync(TContext context);
}