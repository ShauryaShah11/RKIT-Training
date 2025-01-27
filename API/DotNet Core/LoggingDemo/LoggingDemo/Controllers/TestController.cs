using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LoggingDemo.Controllers
{
    /// <summary>
    /// Controller to test different logging levels with ILogger and Serilog.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance injected for logging messages in this controller.</param>
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Endpoint to log messages at different levels (Debug, Information, Warning, Error, Critical).
        /// Also logs an Information message using Serilog.
        /// </summary>
        /// <returns>Returns a response indicating that the log messages have been generated and user should check logs.</returns>
        [HttpGet("log-test")]
        public IActionResult LogTest()
        {
            // Log a message with 'Debug' level
            _logger.LogDebug("This is a Debug log.");

            // Log a message with 'Information' level
            _logger.LogInformation("This is an Information log.");

            // Log a message with 'Warning' level
            _logger.LogWarning("This is a Warning log.");

            // Log a message with 'Error' level
            _logger.LogError("This is an Error log.");

            // Log a message with 'Critical' level
            _logger.LogCritical("This is a Critical log.");

            // Serilog-specific log, will also be written to file as per the configuration
            Log.Information("This is an Information log (Serilog)");

            // Return a response indicating that the logs were written, and user should check logs
            return Ok("Check the console or debug output for logged messages.");
        }
    }
}
