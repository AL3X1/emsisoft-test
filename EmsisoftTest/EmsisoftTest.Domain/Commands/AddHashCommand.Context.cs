using EmsisoftTest.Infrastructure.Interfaces;

namespace EmsisoftTest.Domain.Commands;

public class AddHashContext : ICommandContext
{
    public AddHashContext(string hash)
    {
        Hash = hash;
    }

    public string Hash { get; }
}