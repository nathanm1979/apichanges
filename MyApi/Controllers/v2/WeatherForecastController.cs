// MyApi/Controllers/v2/WeatherForecastController.cs
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WeatherForecastController : ControllerBase
{
    // v2 implementation with an added property
}
