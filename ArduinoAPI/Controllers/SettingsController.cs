using System;
using ArduinoAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArduinoAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
	
        private readonly IOptions<PortConfiguration> _options;

        private readonly PortConfiguration _portConfig;

        public SettingsController(ILogger<SettingsController> logger, IOptions<PortConfiguration> options)
        {
            _logger = logger;

            _options = options;

            _portConfig = options.Value;
        }

		[HttpGet]
        public ActionResult<string> GetPortName() {
		    _logger.LogInformation("Retrieving PortName");

            return _portConfig.PortName;
	    }

		[HttpGet]
        public ActionResult<int> GetBaudRate() {
		    _logger.LogInformation("Retrieving BaudRate");

            return _portConfig.BaudRate;
	    }

		[HttpPost]
        public ActionResult SetPortName(string newName) {
		    _logger.LogInformation("Setting Port Name");

            _options.Value.PortName = newName;

		    return Ok();
	    }

		[HttpPost]
        public ActionResult SetBaudRate(int newRate) {
		    _logger.LogInformation("Setting BaudRate");

            _portConfig.BaudRate = newRate;

		    return Ok();
	    }
    }
}
