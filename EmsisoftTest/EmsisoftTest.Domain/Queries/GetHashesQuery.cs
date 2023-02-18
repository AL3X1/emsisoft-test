using EmsisoftTest.Data.Entities;
using EmsisoftTest.Data.Interfaces;
using EmsisoftTest.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmsisoftTest.Domain.Queries;

public class GetHashesQuery : IQuery<GetHashesQueryContext, List<HashEntity>>
{
    private readonly IRuntimeRepository _runtimeRepository;

    public GetHashesQuery(IRuntimeRepository runtimeRepository)
    {
        _runtimeRepository = runtimeRepository;
    }

    public Task<List<HashEntity>> QueryAsync(GetHashesQueryContext context)
    {
        return _runtimeRepository
            .Query<HashEntity>()
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }
}