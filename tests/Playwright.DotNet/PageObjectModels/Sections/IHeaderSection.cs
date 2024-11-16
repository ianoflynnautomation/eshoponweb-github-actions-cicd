
namespace Playwright.DotNet.PageObjectModels.Sections;

public interface IHeaderSection
{    
    Task<IHeaderSection> OpenBasket();

    Task<IHeaderSection> OpenLogin();

    Task<IHeaderSection> UserEmailShouldBe(string email);
}