using System;
using ArduinoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArduinoAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ArduinoController : ControllerBase
    {
        private readonly ILogger<ArduinoController> _logger;

        private readonly ICommService _serialService;

        public ArduinoController(ILogger<ArduinoController> logger, ICommService serialService)
        {
            _logger = logger;

            _serialService = serialService;
        }

        [HttpPost]
        public ActionResult SetAngle(int angle)
        {
            if (angle < 0 || angle > 180)
            {
                _logger.LogError("Invalid angle");
                return BadRequest("You must input a valid angle between 0 and 180");
            }

            _logger.LogInformation("Writing to port");
            bool result = _serialService.Write(angle);

            return result
                ? Ok("Successfully written to port")
                : BadRequest("There was an error writing to the port");
        }
    }
}
