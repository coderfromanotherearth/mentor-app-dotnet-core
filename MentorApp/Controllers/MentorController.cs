using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MentorApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MentorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MentorController> _logger;

        public MentorController(ILogger<MentorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Mentor> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Mentor
            {   
                Id=1,
                Name="Test"
            })
            .ToArray();
        }
    }
}
