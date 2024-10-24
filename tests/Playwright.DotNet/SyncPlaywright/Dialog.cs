using Playwright.DotNet.SyncPlaywright.Core;
using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;

namespace Playwright.DotNet.SyncPlaywright;

/// <summary>
/// Synchronous wrapper for Playwright IDialog
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="Dialog"/> class.
/// </remarks>
/// <param name="dialog"></param>
public class Dialog(IDialog dialog)
{

    /// <summary>
    /// Gets the wrapped dialog.
    /// </summary>
    public IDialog WrappedDialog { get; internal set; } = dialog;

    /// <summary>
    /// Gets the dialog default value.
    /// </summary>
    public string DefaultValue => WrappedDialog.DefaultValue;

    /// <summary>
    /// Gets the dialog message.
    /// </summary>
    public string Message => WrappedDialog.Message;

    /// <summary>
    /// Gets the dialog page.
    /// </summary>
    public BrowserPage Page => new BrowserPage(WrappedDialog.Page);

    /// <summary>
    /// Gets the dialog type.
    /// </summary>
    public string Type => WrappedDialog.Type;

    /// <summary>
    /// Accept the dialog.
    /// </summary>
    /// <param name="promptText"></param>
    public void Accept(string promptText = null)
    {
        WrappedDialog.AcceptAsync(promptText).SyncResult();
    }

    /// <summary>
    /// Dismiss the dialog.
    /// </summary>
    public void Dismiss()
    {
        WrappedDialog.DismissAsync().SyncResult();
    }
}
