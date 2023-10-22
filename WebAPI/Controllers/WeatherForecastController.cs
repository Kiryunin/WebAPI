using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("¬от информационное сообщение от нашего контроллера значений.");
           
            _logger.LogDebug("¬от отладочное сообщение от нашего контроллера значений.");
           
            _logger.LogWarn("¬от сообщение предупреждени€ от нашего контроллера значений.");
           
            _logger.LogError("¬от сообщение об ошибке от нашего контроллера значений.");
            return new string[] { "value1", "value2" }
    }
}