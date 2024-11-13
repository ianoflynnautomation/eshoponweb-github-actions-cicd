using System.Text.Json;

namespace Playwright.DotNet.Services.Contracts;

public interface IJavaScriptService
{
    Task<JsonElement?> ExecuteAsync(string script, params object[] args);
}