using FunctionTestHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace FunctionApp.Tests.Integration
{
    public class HttpFastEndToEndTests : EndToEndClassScopedTestsBase<FastTestFixture>
    {
        private readonly ITestOutputHelper output;
        public HttpFastEndToEndTests(FastTestFixture fixture, ITestOutputHelper output) : base(fixture)
        {
            this.output = output;
        }

        [Fact]
        public async Task HttpTrigger_ValidBody()
        {
            var input = new JObject
            {
                { "name", "Jeff" }
            };
            string key = await Fixture.Host.GetMasterKeyAsync();
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format($"http://localhost/api/HttpTrigger?code={key}")),
                Method = HttpMethod.Post,
                Content = new StringContent(input.ToString())
            };
            HttpResponseMessage response = await Fixture.Host.HttpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string body = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("Hello, Jeff", body);
            output.WriteLine(Fixture.Host.GetLog());
        }
    }

    public class HttpEndToEndTests : EndToEndCollectionScopedTestsBase<TestFixture>
    {
        private readonly ITestOutputHelper output;
        public HttpEndToEndTests(TestFixture fixture, ITestOutputHelper output) : base(fixture)
        {
            this.output = output;
        }

        [Fact]
        public async Task HttpTrigger_ValidBody()
        {
            var input = new JObject
            {
                { "name", "Jeff" }
            };
            string key = await Fixture.Host.GetMasterKeyAsync();
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format($"http://localhost/api/HttpTrigger?code={key}")),
                Method = HttpMethod.Post,
                Content = new StringContent(input.ToString())
            };
            HttpResponseMessage response = await Fixture.Host.HttpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string body = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("Hello, Jeff", body);
            output.WriteLine(Fixture.Host.GetLog());
        }
    }
}
