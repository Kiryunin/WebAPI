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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("��� �������������� ��������� �� ������ ����������� ��������.");

            _logger.LogDebug("��� ���������� ��������� �� ������ ����������� ��������.");

            _logger.LogWarning("��� ��������� �������������� �� ������ ����������� ��������.");

            _logger.LogError("��� ��������� �� ������ �� ������ ����������� ��������.");
            return new string[] { "value1", "value2" };
        }
    }
}