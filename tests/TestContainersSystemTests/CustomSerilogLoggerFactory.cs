using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;


namespace TestContainersSystemTests;

/// <summary>
/// Represents the custom Serilog logger factory.
/// </summary>
internal sealed class CustomSerilogLoggerFactory : SerilogLoggerFactory
{

  /// <summary>
  /// Initializes a new instance of the <see cref="CustomSerilogLoggerFactory"/> class.
  /// </summary>
  public CustomSerilogLoggerFactory(IConfiguration configuration)
    : base(
      new LoggerConfiguration()
        .ReadFrom
        .Configuration(configuration)
        .CreateLogger(),
      true)
  {
  }

}
