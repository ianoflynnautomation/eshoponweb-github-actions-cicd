namespace Playwright.DotNet.Find;
public sealed class Find
{
    static Find() => By = new FindStrategyFactory();

    public static FindStrategyFactory By { get; }
}