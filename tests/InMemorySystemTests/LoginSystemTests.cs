
using Playwright.DotNet.PageObjectModels.LoginPage;
using Playwright.DotNet.PageObjectModels.Sections;

namespace EShopOnWeb.InMemorySystemTests;

[Parallelizable(ParallelScope.Self)]

[TestFixture]
public class LoginOrderSystemTests : BaseTest
{
    // [Test]
    // public async Task Login_Success_UserJourney()
    // {        
    //     var headerSection = new HeaderSection(Page);
    //     var loginPage = new LoginPage(Page);

    //     await headerSection.OpenLogin();
    //     await loginPage.Login("admin@microsoft.com", "Pass@word1", false);
   
    // }

}