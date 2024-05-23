using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

public class ClimaSantiago
{
    [JsonProperty("main")]
    public string Main { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    public DateTime Date { get; set; }

    public double TemperatureC { get; set; }

    public string Summary { get; set; }
}

public class Main
{
    [JsonProperty("temp")]
    public double TemperatureC { get; set; }
}

public class WeatherResponse
{
    [JsonProperty("weather")]
    public List<ClimaSantiago> Weather { get; set; }

    [JsonProperty("main")]
    public Main Main { get; set; }
}

public class Programa
{
    private static readonly HttpClient client = new HttpClient();

    public static string TraducirMain(string main)
    {
        switch (main.ToLower())
        {
            case "clear":
                return "claro";
            case "clouds":
                return "nubes";
            case "rain":
                return "lluvia";
            case "drizzle":
                return "llovizna";
            case "thunderstorm":
                return "tormenta";
            case "snow":
                return "nieve";
            case "mist":
                return "niebla";
            default:
                return main;
        }
    }

    public static async Task<WeatherResponse> GetWeatherData()
    {
        string apiKey = "36dcb703de5183cd18c150da70639605"; // Aquí es donde debes proporcionar tu clave de la API.
        string city = "Santiago,cl";
        string language = "es";
        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&lang={language}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);

            return weatherResponse;
        }
        catch (Exception ex)
        {
            // Log the exception here
            Console.WriteLine("Se produjo un error al obtener los datos meteorológicos.");
            return null;
        }
    }
}