using EmsisoftTest.Api;
using EmsisoftTest.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EmsisoftTest.Tests.Infrastructure
{
    public class TestContextBase : WebApplicationFactory<Program>
    {
        protected HttpClient Client { get; set; }

        protected string RandomString => Randomizer.CreateRandomizer().GetString();

        protected IRuntimeRepository Repository { get; set; }

        [OneTimeSetUp]
        public async Task SetUpAsync()
        {
            Repository = Services.GetService<IRuntimeRepository>();
            Client = CreateClient();
        }
    }
}