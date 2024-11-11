
using Playwright.DotNet.Fixtures.Host;

namespace Playwright.DotNet.Configuration.Options;

public class TestHostOptions
{
	public const string TestHostSection = "TestHost";

	public HostType HostType { get; set; } = HostType.Internal;

	public string HostUrl { get; set; } = string.Empty;

	public string PublicApiUrl { get; set; } = string.Empty;

}


