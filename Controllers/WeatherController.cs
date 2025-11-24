using Microsoft.AspNetCore.Mvc;
using WebApplication10_Nov19.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication10_Nov19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public const string API_KEY_TEXT = "API_KEY";

        // GET: api/<WeatherController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            bool isLoggedIn = User.Identity.IsAuthenticated;
            string key = Request.Query.ContainsKey(API_KEY_TEXT) ? Request.Query[API_KEY_TEXT][0] : string.Empty;
            
            if (isLoggedIn || key.Equals("12345")) {


                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }.ToString())
                .ToArray();

            }
            else {

                return new string[] { "Invalid Request" };


            }



        }

       

    }
}
