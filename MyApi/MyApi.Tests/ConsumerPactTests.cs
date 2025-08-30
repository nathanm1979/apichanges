// MyApi.Tests/ConsumerPactTests.cs
using PactNet.Output.Xunit;
using PactNet.Matchers;
using System.Net.Http;
using Xunit.Abstractions;
using PactNet;

public class ConsumerPactTests
{
    private readonly IPactBuilderV3 _pactBuilder;

    public ConsumerPactTests(ITestOutputHelper output)
    {
        var pact = new PactConfig
        {
            PactDir = "../../../pacts",
            Outputters = new[] { new XunitOutput(output) }
        };
        _pactBuilder = Pact.V3("MyClient", "MyApi", pact);
    }

    [Fact]
    public async Task GetWeatherForecast_WhenCalled_ReturnsExpectedWeather()
    {
        // Arrange
        _pactBuilder
            .UponReceiving("A request for the weather forecast")
            .WithRequest(HttpMethod.Get, "/api/v1/WeatherForecast")
            .WillRespond()
            .WithStatus(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json; charset=utf-8")
            .WithJsonBody(new TypeMatcher(new[] { new { date = "2025-08-30T13:49:00.0000000-04:00", temperatureC = 25, summary = "Cool" } }));

        // Act & Assert
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(ctx.MockServerUri) };
            var response = await httpClient.GetAsync("/api/v1/WeatherForecast");
            // Add custom assertions here if needed
        });
    }
}
