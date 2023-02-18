using MediatR;

namespace EmsisoftTest.Api.Handlers.GenerateHash;

public class GenerateHashesRequest : IRequest<Unit>
{
    public GenerateHashesRequest(int count)
    {
        Count = count;
    }

    public int Count { get; }
}