using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuCasbin.DomainService.Admin;
using Microsoft.AspNetCore.Authorization;
using AuCasbin.Core.Auth;

namespace AuCasbinApi.Controllers
{
   // [ApiController]
   // [Route("[controller]")]
    public class WeatherForecastController : AreaController
    {
        private readonly IUserDomainService _UserDomainService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private IUser _user;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserDomainService UserDomainService,IUser user)
        {
            _logger = logger;
            _UserDomainService = UserDomainService;
            _user = user;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            _UserDomainService.GetUserInfoAsync().Wait();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<WeatherForecast> Get2()
        {
            Console.WriteLine(_user?.Name);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
