

namespace Playwright.DotNet.Utilities;

public sealed class Wait
{
    public static void Until(
        Func<bool> condition,
        int? timeoutInSeconds = 10,
        string exceptionMessage = "Timeout exceeded.",
        int? retryRateDelay = 50)
    {
        var start = DateTime.Now;
        while (!condition())
        {
            var now = DateTime.Now;
            var totalSeconds = (now - start).TotalSeconds;
            if (totalSeconds >= timeoutInSeconds)
            {
                throw new TimeoutException(exceptionMessage + $" Elapsed time: {totalSeconds} s.");
            }

            Thread.Sleep(retryRateDelay ?? 50);
        }
    }

    public static void ForConditionUntilTimeout(
        Func<bool> condition,
        int totalRunTimeoutMilliseconds = 5000,
        Action? onTimeout = null,
        int sleepTimeMilliseconds = 2000)
    {
        var startTime = DateTime.UtcNow;
        var timeout = startTime + TimeSpan.FromMilliseconds(totalRunTimeoutMilliseconds);

        while (true)
        {
            var conditionFinished = condition();
            if (conditionFinished)
            {
                break;
            }

            if (DateTime.UtcNow >= timeout)
            {
                onTimeout?.Invoke();

                break;
            }

            Thread.Sleep(sleepTimeMilliseconds);
        }
    }
}