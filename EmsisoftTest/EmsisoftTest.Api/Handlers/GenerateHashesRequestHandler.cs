using System.Collections.Concurrent;
using System.Diagnostics;
using EmsisoftTest.Infrastructure.Utils;
using EmsisoftTest.Messaging;
using EmsisoftTest.Messaging.Interfaces;
using MediatR;

namespace EmsisoftTest.Api.Handlers.GenerateHash;

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
        
        // var message = new
        // {
        //     hash = "test"
        // };
        // _messageProducer.Produce(message);

        var timer = Stopwatch.StartNew();
        
        
        // Generated 40000 hashes for 00:02:15.9439786
        for (var i = 0; i < hashCount; i++)
        {
            var hash = EncryptionManager.GetRandomHash();
            _logger.LogInformation($"[{i + 1}] Generated hash - {hash}");
            var messagePayload = new MessagePayload<string>
            {
                Data = hash
            };
            _messageProducer.Produce(messagePayload);
        }
        
        // Generated 40000 hashes for 00:02:47.8014292
        // Parallel.For(0, hashCount, new ParallelOptions {MaxDegreeOfParallelism = 10}, i =>
        // {
        //     var hash = EncryptionManager.GetRandomHash();
        //     _logger.LogInformation($"[{i}] Generated hash - {hash}");
        // });
        //

        // Generated 40000 hashes for 00:02:58.9363682
        // var rangePartitioner = Partitioner.Create(0, hashCount);
        //
        // Parallel.ForEach(rangePartitioner, (range, loopState) =>
        // {
        //     for (var i = range.Item1; i < range.Item2; i++)
        //     {
        //         var hash = EncryptionManager.GetRandomHash();
        //         _logger.LogInformation($"[{i + 1}] Generated hash - {hash}");
        //     }
        // });
        
        timer.Stop();
        
        _logger.LogInformation($"Generated {hashCount} hashes for {timer.Elapsed.ToString()}");
        return new Unit();
    }
}