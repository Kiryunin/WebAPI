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
            _logger.LogInfo("��� �������������� ��������� �� ������ ����������� ��������.");
           
            _logger.LogDebug("��� ���������� ��������� �� ������ ����������� ��������.");
           
            _logger.LogWarn("��� ��������� �������������� �� ������ ����������� ��������.");
           
            _logger.LogError("��� ��������� �� ������ �� ������ ����������� ��������.");
            return new string[] { "value1", "value2" }
    }
}