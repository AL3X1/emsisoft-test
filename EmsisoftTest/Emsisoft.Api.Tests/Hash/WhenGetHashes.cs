using System.Net;
using EmsisoftTest.Tests.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Emsisoft.Api.Tests.Hash;

[TestFixture]
public class WhenGetHashes : TestContextBase
{
    [Test]
    public async Task ShouldReturn200()
    {
        await Client
            .GetAsync("/api/hashes")
            .ValidateStatusCode(x => x.ShouldBe(HttpStatusCode.OK));
    }
}