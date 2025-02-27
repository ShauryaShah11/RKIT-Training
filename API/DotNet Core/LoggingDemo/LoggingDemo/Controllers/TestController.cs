using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Linq;

namespace LoggingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger<TestController> _logger;

        public TestController(Microsoft.Extensions.Logging.ILogger<TestController> logger)
        {
            _logger = logger;
        }

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

        [HttpGet("specific-target")]
        public IActionResult SpecificTarget([FromQuery] string target = "file")
        {
            var logger = LogManager.GetLogger(target);
            if (logger != null)
            {
                logger.Debug("This is a Debug log.");
                logger.Info("This is an Information log.");
                logger.Warn("This is a Warning log.");
                logger.Error("This is an Error log.");
                logger.Fatal("This is a Critical log.");
            }

            return Ok("Check the specific target or log files for logged messages.");
        }

        /// <summary>
        /// Adds a new dynamic file target to NLog at runtime.
        /// </summary>
        [HttpPost("add-dynamic-target")]
        public IActionResult AddDynamicTarget(
            [FromQuery] string targetName,
            [FromQuery] string fileName,
            [FromQuery] string loggerName = null,
            [FromQuery] string minLevel = "Debug")
        {
            if (string.IsNullOrEmpty(targetName) || string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Target name and file name are required");
            }

            var config = LogManager.Configuration;

            // Check if target with this name already exists
            if (config.FindTargetByName(targetName) != null)
            {
                return BadRequest($"Target '{targetName}' already exists.");
            }

            try
            {
                // Create the file target
                var fileTarget = new FileTarget(targetName)
                {
                    FileName = fileName,
                    Layout = "${longdate} | ${level} | ${logger} | ${message} ${exception:format=tostring}"
                };

                // Add the target to NLog config
                config.AddTarget(fileTarget);

                // Create logging rule
                NLog.LogLevel level = NLog.LogLevel.FromString(minLevel);

                // If loggerName is provided, use it, otherwise use * for all loggers
                string loggerNamePattern = loggerName ?? "*";
                LoggingRule? rule = new LoggingRule(loggerNamePattern, level, fileTarget);

                config.LoggingRules.Add(rule);
                LogManager.ReconfigExistingLoggers();

                return Ok($"New file target '{targetName}' added for logs to: {fileName}. Use logger name: '{loggerNamePattern}'");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding target: {ex.Message}");
            }
        }

        /// <summary>
        /// Modifies an existing File target at runtime.
        /// </summary>
        [HttpPost("modify-target")]
        public IActionResult ModifyTarget([FromQuery] string newFileName = "logs/modified_log.txt")
        {
            var config = LogManager.Configuration;

            if (config.FindTargetByName<FileTarget>("dynamicFile") is { } fileTarget)
            {
                fileTarget.FileName = newFileName;
                LogManager.ReconfigExistingLoggers();
                return Ok($"File target updated to: {newFileName}");
            }
            return NotFound("Target not found.");
        }

        /// <summary>
        /// Changes the logging level dynamically at runtime.
        /// </summary>
        [HttpPost("change-log-level")]
        public IActionResult ChangeLogLevel([FromQuery] string newLevel)
        {
            var config = LogManager.Configuration;
            if (config != null && NLog.LogLevel.FromString(newLevel) is { } level)
            {
                foreach (LoggingRule rule in config.LoggingRules)
                {
                    rule.SetLoggingLevels(level, NLog.LogLevel.Fatal);
                }
                LogManager.ReconfigExistingLoggers();
                return Ok($"Log level updated to {newLevel}");
            }
            return BadRequest("Invalid log level.");
        }

        /// <summary>
        /// Adds a new logging rule dynamically.
        /// </summary>
        [HttpPost("add-rule")]
        public IActionResult AddLoggingRule([FromQuery] string fileName = "logs/error_logs.txt", [FromQuery] string minLevel = "Error")
        {
            var config = LogManager.Configuration;
            var level = NLog.LogLevel.FromString(minLevel);

            if (config.FindTargetByName("errorFile") == null)
            {
                var errorTarget = new FileTarget("errorFile")
                {
                    FileName = fileName,
                    Layout = "${longdate} | ${level} | ${message}"
                };

                config.AddTarget(errorTarget);
                var rule = new LoggingRule("*", level, NLog.LogLevel.Fatal, errorTarget);
                config.LoggingRules.Add(rule);
                LogManager.ReconfigExistingLoggers();

                return Ok($"New Logging Rule added: {minLevel}+ logs will be saved to {fileName}");
            }
            return BadRequest("Rule already exists.");
        }

        /// <summary>
        /// Modifies an existing logging rule dynamically.
        /// </summary>
        [HttpPost("modify-rule")]
        public IActionResult ModifyLoggingRule([FromQuery] string newMinLevel = "Warning")
        {
            var config = LogManager.Configuration;
            var level = NLog.LogLevel.FromString(newMinLevel);

            var rule = config.LoggingRules.FirstOrDefault(r => r.Targets.Any(t => t.Name == "errorFile"));
            if (rule != null)
            {
                rule.SetLoggingLevels(level, NLog.LogLevel.Fatal);
                LogManager.ReconfigExistingLoggers();
                return Ok($"Logging Rule modified: {newMinLevel}+ logs will be saved.");
            }
            return NotFound("Rule not found.");
        }

        /// <summary>
        /// Removes a logging rule dynamically.
        /// </summary>
        [HttpDelete("remove-rule")]
        public IActionResult RemoveLoggingRule()
        {
            var config = LogManager.Configuration;

            var ruleToRemove = config.LoggingRules.FirstOrDefault(r => r.Targets.Any(t => t.Name == "errorFile"));
            if (ruleToRemove != null)
            {
                config.LoggingRules.Remove(ruleToRemove);
                config.RemoveTarget("errorFile");
                LogManager.ReconfigExistingLoggers();
                return Ok("Logging Rule removed.");
            }
            return NotFound("Rule not found.");
        }
    }
}
