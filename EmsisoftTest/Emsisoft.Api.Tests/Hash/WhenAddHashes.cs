using System.Net;
using EmsisoftTest.Tests.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Emsisoft.Api.Tests.Hash;

[TestFixture]
public class WhenAddHashes : TestContextBase
{
    [Test]
    public async Task ShouldReturn200()
    {
        await Client
            .PostAsync("/api/hashes")
            .ValidateStatusCode(x => x.ShouldBe(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task WhenCountProvided_ShouldReturn200()
    {
        await Client
            .PostAsync("/api/hashes?count=10")
            .ValidateStatusCode(x => x.ShouldBe(HttpStatusCode.OK));
    }
}