using MediatR;

namespace EmsisoftTest.Api.Handlers;

public class GenerateHashesRequest : IRequest<Unit>
{
    public GenerateHashesRequest(int count)
    {
        Count = count;
    }

    public int Count { get; }
}