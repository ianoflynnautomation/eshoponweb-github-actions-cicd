using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.SyncPlaywright.Interactions;

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

    /// <summary>
    /// Down the key.
    /// </summary>
    /// <param name="key"></param>
    public void Down(string key)
    {
        WrappedKeyboard.DownAsync(key).SyncResult();
    }

    /// <summary>
    /// Insert text.
    /// </summary>
    /// <param name="text"></param>
    public void InsertText(string text)
    {
        WrappedKeyboard.InsertTextAsync(text).SyncResult();
    }

    /// <summary>
    /// Press the key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    public void Press(string key, KeyboardPressOptions? options = null)
    {
        WrappedKeyboard.PressAsync(key, options).SyncResult();
    }

    /// <summary>
    /// Type the text.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="options"></param>
    public void Type(string text, KeyboardTypeOptions? options = null)
    {
        WrappedKeyboard.TypeAsync(text, options).SyncResult();
    }

    /// <summary>
    /// Up the key.
    /// </summary>
    /// <param name="key"></param>
    public void Up(string key)
    {
        WrappedKeyboard.UpAsync(key).SyncResult();
    }
}
