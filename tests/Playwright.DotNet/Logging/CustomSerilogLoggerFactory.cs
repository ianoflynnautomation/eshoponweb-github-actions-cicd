using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

namespace Playwright.DotNet.Logging;

/// <summary>
/// Represents the custom Serilog logger factory.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CustomSerilogLoggerFactory"/> class.
/// </remarks>
internal sealed class CustomSerilogLoggerFactory(IConfiguration configuration) : SerilogLoggerFactory(
    new LoggerConfiguration()
      .WriteTo.Console()
        .ReadFrom
        .Configuration(configuration)
        .CreateLogger(),
    true)
{
}
