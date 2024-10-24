
namespace EShopOnWeb.TestContainersSystemTests;

/// <summary>
/// Represents the test host.
/// </summary>
public interface ITestHost
{
    /// <summary>
    /// Initializes the test host.
    /// </summary>
    Task InitializeAsync();
    
    /// <summary>
    /// Disposes the test host.
    /// </summary>
    Task DisposeAsync();

    /// <summary>
    /// Disposes the test host.
    /// </summary>
    void Dispose();
}
