using System.Text.Json;

namespace Playwright.DotNet.Services.Contracts;

public interface IJavaScriptService
{
    JsonElement? Execute(string script, params object[] args);
}