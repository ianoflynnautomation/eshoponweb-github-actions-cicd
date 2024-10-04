namespace TestContainersSystemTests.Fixtures;

/// <summary>
/// Represents the database fixture.
/// </summary>
/// <typeparam name="TDockerContainer"></typeparam>
public abstract class DataBaseFixture<TDockerContainer>
{
    /// <summary>
    /// Gets or sets the Testcontainers.
    /// </summary>
    public TDockerContainer Container { get; protected set; } = default!;

    /// <summary>
    /// Initializes the Testcontainers.
    /// </summary>
    public abstract Task InitializeAsync();

    /// <summary>
    /// Disposes the Testcontainers.
    /// </summary>
    public abstract Task DisposeAsync();

    /// <summary>
    /// Stops the Testcontainers.
    /// </summary>
    public abstract Task StopContainer();

}
