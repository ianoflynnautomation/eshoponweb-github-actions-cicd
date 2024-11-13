using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Interactions;

/// <summary>
/// Options for mouse click.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Mouse"/> class.
/// </remarks>
/// <param name="mouse"></param>
public class Mouse(IMouse mouse)
{

    /// <summary>
    /// Gets the wrapped mouse.
    /// </summary>
    public IMouse WrappedMouse { get; internal set; } = mouse;

}
