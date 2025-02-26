using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Config;

namespace LoggingDemo.Controllers
{
    /// <summary>
    /// Controller to test different logging levels with ILogger and Serilog.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger<TestController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance injected for logging messages in this controller.</param>
        public TestController(Microsoft.Extensions.Logging.ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Endpoint to log messages at different levels (Debug, Information, Warning, Error, Critical).
        /// </summary>
        /// <returns>Returns a response indicating that the log messages have been generated.</returns>
        [HttpGet("log-test")]
        public IActionResult LogTest()
        {
            _logger.LogDebug("This is a Debug log.");
            _logger.LogInformation("This is an Information log.");
            _logger.LogWarning("This is a Warning log.");
            _logger.LogError("This is an Error log.");
            _logger.LogCritical("This is a Critical log.");

            return Ok("Check the console or log files for logged messages.");
        }

        /// <summary>
        /// Changes the logging level dynamically at runtime.
        /// </summary>
        /// <param name="newLevel">New log level (e.g., Debug, Info, Error).</param>
        /// <returns>Returns success message if log level is updated.</returns>
        [HttpPost("change-log-level")]
        public IActionResult ChangeLogLevel([FromQuery] string newLevel)
        {
            LoggingConfiguration config = LogManager.Configuration;
            if (config != null && NLog.LogLevel.FromString(newLevel) is { } level)
            {
                foreach (LoggingRule? rule in config.LoggingRules)
                {
                    rule.SetLoggingLevels(level, NLog.LogLevel.Fatal);
                }
                LogManager.ReconfigExistingLoggers();
                return Ok($"Log level updated to {newLevel}");
            }
            return BadRequest("Invalid log level.");
        }
    }
}
