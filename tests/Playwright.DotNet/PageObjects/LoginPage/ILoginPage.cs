namespace Playwright.Dotnet.PageObjects.LoginPage;

/// <summary>
/// Represents the login page of the eShopOnWeb application.
/// </summary>
public interface ILoginPage
{
    /// <summary>
    /// Sets the email on the login page.
    /// </summary>
    /// <param name="email">Users account email address</param>
    ILoginPage SetEmail(string email);

    /// <summary>
    /// Sets the password on the login page.
    /// </summary>
    /// <param name="password">Users account password</param>
    ILoginPage SetPassword(string password);

    /// <summary>
    /// Checks the remember me checkbox. Default is Unchecked.
    /// </summary>
    ILoginPage CheckRememberMe();

    /// <summary>
    /// Attempts to login with the provided credentials.
    /// </summary>
    /// <param name="email">Users account email addres</param>
    /// <param name="password">Users account password</param>
    /// <param name="rememberMe">Checks the remember me checkbox. Default is Unchecked</param>
    ILoginPage Login(string email, string password, bool rememberMe = false);

}