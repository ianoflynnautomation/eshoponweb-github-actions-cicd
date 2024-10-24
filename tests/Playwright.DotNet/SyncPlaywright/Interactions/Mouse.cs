using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.SyncPlaywright.Interactions;

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

    /// <summary>
    /// Click the mouse.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="options"></param>
    public void Click(float x, float y, MouseClickOptions options = null)
    {
        WrappedMouse.ClickAsync(x, y, options).SyncResult();
    }

    /// <summary>
    /// Double click the mouse.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="options"></param>
    public void DblClick(float x, float y, MouseDblClickOptions options = null)
    {
        WrappedMouse.DblClickAsync(x, y, options).SyncResult();
    }

    /// <summary>
    /// Down the mouse.
    /// </summary>
    /// <param name="options"></param>
    public void Down(MouseDownOptions options = null)
    {
        WrappedMouse.DownAsync(options).SyncResult();
    }

    /// <summary>
    /// Move the mouse.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="options"></param>
    public void Move(float x, float y, MouseMoveOptions options = null)
    {
        WrappedMouse.MoveAsync(x, y, options).SyncResult();
    }

    /// <summary>
    /// Up the mouse.
    /// </summary>
    /// <param name="options"></param>
    public void Up(MouseUpOptions options = null)
    {
        WrappedMouse.UpAsync(options).SyncResult();
    }

    /// <summary>
    /// Wheel the mouse.
    /// </summary>
    /// <param name="deltaX"></param>
    /// <param name="deltaY"></param>
    public void Wheel(float deltaX, float deltaY)
    {
        WrappedMouse.WheelAsync(deltaX, deltaY).SyncResult();
    }
}
