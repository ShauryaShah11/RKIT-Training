# Logging (NLog)

# Index

1. [Introduction to NLog](#introduction-to-nlog)
2. [NLog Configuration (`nlog.config`)](#nlog-configuration-nlogconfig)
3. [Log Levels and Order](#log-levels-and-order)
4. [Formatting Log Messages](#formatting-log-messages)
5. [Modifying `nlog.config` from Controller](#modifying-nlogconfig-from-controller)
6. [Dynamic Log Level Updates](#dynamic-log-level-updates)
7. [Reloading `nlog.config` at Runtime](#reloading-nlogconfig-at-runtime)
8. [Serilog vs NLog](#serilog-vs-nlog)

---

## Introduction to NLog

NLog is a flexible and powerful logging framework for .NET applications. It allows developers to log messages to various targets such as files, databases, and consoles.

### Features:

- Easy-to-configure logging framework
- Supports multiple logging targets
- High-performance asynchronous logging
- Supports structured logging with JSON
- Rich formatting options for log messages

---

## NLog Configuration (`nlog.config`)

The `nlog.config` file defines how logs are processed, including log levels, output formats, and targets.

### Example `nlog.config`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xsi:schemaLocation="http://www.nlog-project.org/schemas/NLog.xsd NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

  <targets>
    <target name="fileLog" xsi:type="File" fileName="logs/logfile.txt" layout="${longdate} | ${level} | ${message}" />
  </targets>

  <rules>
    <logger name="*" minlevel="Info" writeTo="fileLog" />
  </rules>
</nlog>

```

---

## Log Levels and Order

NLog supports multiple log levels, arranged in the following order (from lowest to highest severity):

1. **Trace**
2. **Debug**
3. **Info** (Default minimum level)
4. **Warn**
5. **Error**
6. **Fatal**

Example usage in code:

```csharp
var logger = LogManager.GetCurrentClassLogger();
logger.Info("This is an informational message.");
logger.Error("An error has occurred!");

```

---

## Formatting Log Messages

NLog provides various placeholders for formatting log messages:

| Placeholder | Description |
| --- | --- |
| `${longdate}` | Full timestamp of log entry |
| `${shortdate}` | Short date (YYYY-MM-DD) |
| `${time}` | Current time (HH:mm:ss) |
| `${level}` | Log level (Info, Warn, etc.) |
| `${message}` | Log message content |
| `${logger}` | Name of the logger |
| `${callsite}` | Fully qualified method name where log was written |
| `${exception}` | Exception details (if available) |
| `${threadid}` | ID of the thread that logged the message |
| `${newline}` | Inserts a newline character |
| `${uppercase:${level}}` | Converts log level to uppercase |

### Example Formatted Log Output:

```
2025-02-26 12:34:56 | INFO | Application started successfully.
2025-02-26 12:35:00 | ERROR | An exception occurred in MyApp.Program.Main()

```

---

## Modifying `nlog.config` from Controller

You can dynamically modify `nlog.config` settings from an ASP.NET Core controller.

### Change Log File Path

```csharp
[HttpPost("change-log-file")]
public IActionResult ChangeLogFile([FromQuery] string newFilePath)
{
    var config = LogManager.Configuration;
    var fileTarget = config.FindTargetByName<FileTarget>("fileLog");
    if (fileTarget != null)
    {
        fileTarget.FileName = newFilePath;
        LogManager.ReconfigExistingLoggers();
        return Ok($"Log file path updated to {newFilePath}");
    }
    return BadRequest("File target not found.");
}

```

---

## Dynamic Log Level Updates

To change the log level at runtime:

```csharp
[HttpPost("change-log-level")]
public IActionResult ChangeLogLevel([FromQuery] string newLevel)
{
    var config = LogManager.Configuration;
    if (LogLevel.FromString(newLevel) is LogLevel level)
    {
        foreach (var rule in config.LoggingRules)
        {
            rule.SetLoggingLevels(level, LogLevel.Fatal);
        }
        LogManager.ReconfigExistingLoggers();
        return Ok($"Log level updated to {newLevel}");
    }
    return BadRequest("Invalid log level.");
}

```

---

## Reloading `nlog.config` at Runtime

If you modify `nlog.config` externally and want to reload it without restarting the application:

```csharp
LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
LogManager.ReconfigExistingLoggers();

```

---

## Serilog vs NLog

### NLog:

- More traditional logging framework
- Configured via `nlog.config`
- Supports file, database, and console logging
- Works well for structured and unstructured logging

### Serilog:

- Designed for structured logging
- Uses JSON-based configuration
- Built-in support for application insights and cloud logging
- Integrates seamlessly with ASP.NET Core

### Choosing Between Serilog and NLog

- Use **NLog** if you need simple file-based logging with traditional config-based setup.
- Use **Serilog** if you require structured logging with JSON support and cloud integrations.

---

This documentation provides a structured reference for **NLog usage, configuration, formatting, and dynamic updates** in ASP.NET Core applications.