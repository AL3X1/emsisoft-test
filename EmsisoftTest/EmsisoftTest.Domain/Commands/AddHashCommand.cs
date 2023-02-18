using EmsisoftTest.Data.Entities;
using EmsisoftTest.Data.Interfaces;
using EmsisoftTest.Infrastructure.Interfaces;

namespace EmsisoftTest.Domain.Commands;

public class AddHashCommand : ICommand<AddHashContext>
{
    private readonly IRuntimeRepository _runtimeRepository;

    public AddHashCommand(IRuntimeRepository runtimeRepository)
    {
        _runtimeRepository = runtimeRepository;
    }

    public async Task ExecuteAsync(AddHashContext context)
    {
        var entity = new HashEntity
        {
            Hash = context.Hash
        };

        await _runtimeRepository.AddAsync(entity);
    }
}