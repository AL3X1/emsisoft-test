using System.Collections.Concurrent;
using System.Diagnostics;
using EmsisoftTest.Infrastructure.Utils;
using EmsisoftTest.Messaging;
using EmsisoftTest.Messaging.Interfaces;
using MediatR;

namespace EmsisoftTest.Api.Handlers;

public class GenerateHashesRequestHandler : IRequestHandler<GenerateHashesRequest, Unit>
{
    private readonly ILogger<GenerateHashesRequestHandler> _logger;

    private readonly IMessageProducer _messageProducer;

    public GenerateHashesRequestHandler(ILogger<GenerateHashesRequestHandler> logger, IMessageProducer messageProducer)
    {
        _logger = logger;
        _messageProducer = messageProducer;
    }

    public async Task<Unit> Handle(GenerateHashesRequest request, CancellationToken cancellationToken)
    {
        var hashCount = request.Count > 0 ? request.Count : 40000;
        var timer = Stopwatch.StartNew();
        var rangePartitioner = Partitioner.Create(0, hashCount);
        
        Parallel.ForEach(rangePartitioner, (range, _) =>
        {
            for (var i = range.Item1; i < range.Item2; i++)
            {
                var hash = EncryptionManager.GetRandomHash();
                var messagePayload = new MessagePayload<string>
                {
                    Data = hash
                };
                _messageProducer.Produce(messagePayload);
                _logger.LogInformation($"Sent {hash}");
            }
        });
        
        timer.Stop();
        
        _logger.LogInformation($"Generated {hashCount} hashes for {timer.Elapsed.ToString()}");
        return new Unit();
    }
}