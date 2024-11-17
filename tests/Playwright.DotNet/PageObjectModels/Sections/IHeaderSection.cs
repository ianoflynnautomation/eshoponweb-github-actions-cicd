
namespace Playwright.DotNet.PageObjectModels.Sections;

public interface IHeaderSection
{    
    Task OpenBasket();

    Task OpenLogin();

    Task UserEmailShouldBe(string email);
}