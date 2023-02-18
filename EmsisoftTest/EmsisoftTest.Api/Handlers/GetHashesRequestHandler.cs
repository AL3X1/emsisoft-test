using EmsisoftTest.Data.Entities;
using EmsisoftTest.Domain.Models;
using EmsisoftTest.Domain.Queries;
using EmsisoftTest.Infrastructure.Interfaces;
using MediatR;

namespace EmsisoftTest.Api.Handlers;

public class GetHashesRequestHandler : IRequestHandler<GetHashesRequest, HashGetListModel>
{
    private readonly IQueryBuilder _queryBuilder;

    public GetHashesRequestHandler(IQueryBuilder queryBuilder)
    {
        _queryBuilder = queryBuilder;
    }

    public async Task<HashGetListModel> Handle(GetHashesRequest request, CancellationToken cancellationToken)
    {
        var context = new GetHashesQueryContext();
        var entities = await _queryBuilder.AskAsync<GetHashesQueryContext, List<HashEntity>>(context);
        var hashes = entities
            .GroupBy(x => x.CreatedAt.Date)
            .Select(x => new HashGetModel
            {
                Date = x.First().CreatedAt.ToString("yyyy-MM-dd"),
                Count = x.Count()
            })
            .ToArray();

        return new HashGetListModel
        {
            Hashes = hashes
        };
    }
}