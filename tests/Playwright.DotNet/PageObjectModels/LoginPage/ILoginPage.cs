namespace Playwright.DotNet.PageObjectModels.LoginPage;

public interface ILoginPage
{
    Task<ILoginPage> SetEmail(string email);
    Task<ILoginPage> SetPassword(string password);
    Task<ILoginPage> CheckRememberMe();
    Task<ILoginPage> Login(string email, string password, bool rememberMe = false);

}