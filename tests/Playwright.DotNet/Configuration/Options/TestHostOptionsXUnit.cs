
using Playwright.DotNet.Fixtures.XUnit.HostXUnit;

namespace Playwright.DotNet.Configuration.Options;

public class TestHostOptionsXUnit
{
	public const string TestHostSection = "TestHost";

	public HostTypeXUnit HostType { get; set; } = HostTypeXUnit.Internal;

	public string HostUrl { get; set; } = string.Empty;

	public string PublicApiUrl { get; set; } = string.Empty;

}


