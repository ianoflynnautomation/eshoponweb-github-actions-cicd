
using Playwright.DotNet.Infra.NUnit;
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]

[TestFixture]
public class LoginOrderSystemTests : InMemorySystemTestsBase
{

}