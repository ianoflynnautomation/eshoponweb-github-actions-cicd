using Microsoft.Playwright;

namespace Playwright.DotNet.Playwright.Interactions;

/// <summary>
/// Options for keyboard press.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Keyboard"/> class.
/// </remarks>
/// <param name="keyboard"></param>
public class Keyboard(IKeyboard keyboard)
{

    /// <summary>
    /// Gets the wrapped keyboard.
    /// </summary>
    public IKeyboard WrappedKeyboard { get; internal set; } = keyboard;

}
