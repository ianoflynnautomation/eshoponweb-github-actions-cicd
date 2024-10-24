using Bunit;
using Xunit.Abstractions;

namespace EShopOnWeb.Blazor.UI.UnitTests;

public abstract class TestContextBase : TestContext
{
    public TestContextBase(ITestOutputHelper outputHelper)
    {
        Services.AddXunitLogger(outputHelper);
    }
}