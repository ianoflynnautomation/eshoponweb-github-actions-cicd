using Microsoft.Extensions.Configuration;

namespace Playwright.DotNet.Configuration;

public static class ConfigurationRootInstance
{
    private static readonly Lazy<IConfigurationRoot> _configurationValue = new(GetTestConfiguration());

    public static IConfigurationRoot TestConfiguration => _configurationValue.Value;

    public static TSection GetSection<TSection>(string sectionName) where TSection : class
    {
        return TestConfiguration.GetSection(sectionName).Get<TSection>() ?? throw new InvalidOperationException($"Section {sectionName} not found in configuration.");
    }

    private static IConfigurationRoot GetTestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", optional: true, reloadOnChange: true);

        return builder.Build();
    }
}