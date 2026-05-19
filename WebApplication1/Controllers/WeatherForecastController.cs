using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private ILogger<WeatherForecastController> _logger;
        private static WeatherForecast[] ListWeatherForecast;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this._logger = logger;

            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        /// <summary>
        /// Return all weather forecasts.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Retrieving all weather forecasts.");
            return ListWeatherForecast;
        }

        /// <summary>
        /// return a weather forecast by id.
        /// </summary>
        /// <param name="id">Position</param>
        /// <returns>the element by position</returns>
        [HttpGet("/{id}")]
        public ActionResult<WeatherForecast> GetByPosition(int id)
        {
            if (id > 5 || id < 0)
                return BadRequest();

            return Ok(ListWeatherForecast[id]);
        }
    }
}
