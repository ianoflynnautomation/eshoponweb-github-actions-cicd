namespace Playwright.DotNet.Configuration.Options;

public class TimeoutOptions
{
    public int PageLoadTimeout { get; set; } = 3000;
    public int ScriptTimeout { get; set; } = 3000;
    public int WaitUntilReadyTimeout { get; set; } = 3000;
    public int WaitForJavaScriptAnimationsTimeout { get; set; } = 3000;
    public int WaitForAngularTimeout { get; set; } = 3000;
    public int WaitForPartialUrl { get; set; } = 3000;
    public int ValidationsTimeout { get; set; } = 3000;
    public int WaitForAjaxTimeout { get; set; } = 3000;
    public int SleepInterval { get; set; } = 1;
    public int ElementToBeVisibleTimeout { get; set; } = 3000;
    public int ElementToBeAttachedTimeout { get; set; } = 3000;
    public int ElementToBeCheckedTimeout { get; set; } = 3000;
    public int ElementToBeDisabledTimeout { get; set; } = 3000;
    public int ElementToBeEditableTimeout { get; set; } = 3000;
    public int ElementToBeEmptyTimeout { get; set; } = 3000;
    public int ElementToBeEnabledTimeout { get; set; } = 3000;
    public int ElementToBeFocusedTimeout { get; set; } = 3000;
    public int ElementToBeHiddenTimeout { get; set; } = 3000;
    public int ElementToBeInViewportTimeout { get; set; } = 3000;
    public int ElementToVisibleTimeout { get; set; } = 3000;
    public int ElementToContainTextTimeout { get; set; } = 3000;
    public int ElementToHaveAttributeTimeout { get; set; } = 3000;
    public int ElementToHaveHaveClassTimeout { get; set; } = 3000;
    public int ElementToHaveCountTimeout { get; set; } = 3000;
    public int ElementToHaveCSSTimeout { get; set; } = 3000;
    public int ElementToHaveIdTimeout { get; set; } = 3000;
    public int ElementToHaveJSPropertyTimeout { get; set; } = 3000;
    public int ElementToHaveTextTimeout { get; set; } = 3000;
    public int ElementToHaveValueTimeout { get; set; } = 3000;
    public int ElementToHaveValuesTimeout { get; set; } = 3000;
    public int PageToHaveTitleTimeout { get; set; } = 3000;
    public int PageToHaveURLTimeout { get; set; } = 3000;
    public int ResponseToBeOKTimeout { get; set; } = 3000;
    public int ElementToExistTimeout { get; set; } = 3000;
    public int ElementToHaveContentTimeout { get; set; } = 3000;
    public int ActionTimeoutWhenHandlingDialogs { get; set; } = 1000;


}