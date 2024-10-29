using Microsoft.AspNetCore.Mvc.Testing;

namespace Playwright.DotNet.Fixtures;

public class ProgramTest
{
    private static WebApplicationFactory<Microsoft.eShopWeb.PublicApi.Program> _application = new();

    public static HttpClient NewClient
    {
        get
        {
            return _application.CreateClient();
        }
    }
}
