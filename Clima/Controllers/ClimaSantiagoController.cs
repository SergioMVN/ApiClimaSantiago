using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ClimaSantiagoController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClimaSantiago>>> Get()
    {
        try
        {
            var weatherData = await Programa.GetWeatherData();
            if (weatherData == null)
            {
                return StatusCode(500, "An error occurred while fetching the weather data.");
            }
            return new List<ClimaSantiago>
            {
                new ClimaSantiago
                {
                    Date = DateTime.Now,
                    TemperatureC = weatherData.Main.TemperatureC,
                    Summary = Programa.TraducirMain(weatherData.Weather[0].Main),
                    Main = weatherData.Weather[0].Main,
                    Description = weatherData.Weather[0].Description
                }
            };
        }
        catch (Exception ex)
        {
            // Log the exception here
            return StatusCode(500, "An error occurred while fetching the weather data.");
        }
    }
}