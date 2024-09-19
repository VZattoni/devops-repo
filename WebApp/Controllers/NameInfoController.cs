using devops_project.Interfaces;
using devops_project.Helpers;
using devops_project.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace devops_project.Controllers
{
    [ApiController]
    [Route("api")]
    public class NameInfoController : ControllerBase
    {

        private readonly ILogger<NameInfoController> _logger;
        private readonly IIbgeService _ibgeService;

        public NameInfoController(ILogger<NameInfoController> logger, IIbgeService ibgeService)
        {
            _logger = logger;
            _ibgeService = ibgeService;
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetNameAsync(string name)
        {
            _logger.LogInformation($"Starting request: name: {name}");
            var response = await _ibgeService.GetNameFrequencyAsync(name);
            if(response == null)
            {
                return StatusCode(500);
            }
            return Ok(response);
        }

        [HttpGet("names")]
        public IActionResult GetNames()
        {
            string[] response = NamesList.Names;
            if (response == null)
            {
                return StatusCode(500);
            }
            return Ok(response);
        }
    }
}
