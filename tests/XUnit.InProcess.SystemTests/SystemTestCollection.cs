
using Playwright.DotNet.Fixtures.XUnit;
using Xunit;

namespace EShopOnWeb.XUnit.InProcess.SystemTests;

[CollectionDefinition("SystemTestCollection")]
public class SystemTestCollection : ICollectionFixture<SystemTestFixtureXUnit>
{
    // No code is needed here. This class simply associates the fixture with the collection.
}