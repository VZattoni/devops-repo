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
        private readonly IStaticNamesWrapper _staticNamesWrapper;

        public NameInfoController(ILogger<NameInfoController> logger, IIbgeService ibgeService, IStaticNamesWrapper staticNamesWrapper)
        {
            _logger = logger;
            _ibgeService = ibgeService;
            _staticNamesWrapper = staticNamesWrapper;
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
            string[] response = _staticNamesWrapper.GetNamesList();
            if (response == null)
            {
                return StatusCode(500);
            }
            return Ok(response);
        }

        [HttpGet("name/concat/{firstName}/{secondName}")]
        public IActionResult GetConcatNames(string firstName, string secondName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(secondName))
            {
                return StatusCode(500);
            }
            return Ok(string.Concat(firstName, " ", secondName));
        }

        [HttpGet("name")]
        public IActionResult GetName()
        {
            string[] namesList = _staticNamesWrapper.GetNamesList();
            if (namesList == null)
            {
                return StatusCode(500);
            }
            Random random = new Random();
            return Ok(namesList.ElementAt(random.Next(1, namesList.Length)));
        }

    }
}
