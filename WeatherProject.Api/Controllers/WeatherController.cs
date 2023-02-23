using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WeatherProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        HttpClient client = new HttpClient();
        string path = "https://api.weatherapi.com/v1/forecast.json?key=39f8ecaf506c4f76b3f55139222906&q=";

        [HttpGet("{city}")]
        public async Task<string> GetByCity(string city)
        {
            path += city;
            var data = await client.GetAsync(path).Result.Content.ReadAsStringAsync();
            JObject jsonData = JObject.Parse(data);
            var current = (JObject)jsonData["current"];
            var condition = (JObject)current["condition"];
            var text = (string)condition["text"];
            var temp_c = (int)current["temp_c"];

            return $"the weather in {city} is: {temp_c} temp, condition {text}";
        }
        [HttpGet("{city},{index}")]
        public async Task<string> GetByCity(string city, int index)
        {
            path += city;
            path += "&days=3&aqi=yes&alerts=yes";
            var data = await client.GetAsync(path).Result.Content.ReadAsStringAsync();
             
            return data;
        }
    }
}

