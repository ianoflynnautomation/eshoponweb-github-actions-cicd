namespace Playwright.DotNet.PageObjectModels.LoginPage;

public interface ILoginPage
{
    Task SetEmail(string email);

    Task SetPassword(string password);

    Task CheckRememberMe();

    Task Login(string email, string password, bool rememberMe = false);

}