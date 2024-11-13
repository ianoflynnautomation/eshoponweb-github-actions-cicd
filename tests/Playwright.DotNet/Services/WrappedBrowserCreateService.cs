using Playwright.DotNet.Enums;
using Playwright.DotNet.Playwright.Core;
using Microsoft.Playwright;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using Playwright.DotNet.Configuration;
using Playwright.DotNet.Configuration.Options;

namespace Playwright.DotNet.Services;

public static class WrappedBrowserCreateService
{
    public static BrowserConfiguration? BrowserConfiguration { get; set; }
    public static int Port { get; set; }
    public static int DebuggerPort { get; set; }


    internal static WrappedBrowser Create(BrowserConfiguration executionConfiguration)
    {
        BrowserConfiguration = executionConfiguration;
        var wrappedBrowser = new WrappedBrowser
        {
            Playwright = new PlaywrightCore(Microsoft.Playwright.Playwright.CreateAsync().Result)
        };

        if (executionConfiguration.ExecutionType == ExecutionType.Regular)
        {
            InitializeWrappedBrowserRegularMode(wrappedBrowser);
        }
        else
        {
            //InitializeWrappedBrowserGridMode(executionConfiguration, wrappedBrowser);
        }

        var pageLoadTimeout = WebSettings.TimeoutSettings?.PageLoadTimeout ?? 30000;

        wrappedBrowser.CurrentPage.WrappedPage.SetDefaultNavigationTimeout(pageLoadTimeout);

        ChangeWindowSize(executionConfiguration.Size, wrappedBrowser);



        return wrappedBrowser;
    }

    private static void ChangeWindowSize(Size windowSize, WrappedBrowser wrappedBrowser)
    {
        if (windowSize != default)
        {
            wrappedBrowser.CurrentPage.ViewportSize.Width = windowSize.Width;
            wrappedBrowser.CurrentPage.ViewportSize.Height = windowSize.Height;
        }
        // There is no maximize option in playwright
        //else
        //{
        //    wrappedWebDriver.Manage().Window.Maximize();
        //}
    }


    private static void InitializeWrappedBrowserRegularMode(WrappedBrowser wrappedBrowser)
    {
        BrowserTypeLaunchOptions launchOptions = new();
        var args = new List<string>();

        Port = GetFreeTcpPort();
        DebuggerPort = GetFreeTcpPort();

        switch (BrowserConfiguration.BrowserType)
        {
            case BrowserTypes.Chromium:
                launchOptions.Headless = false;

                args.Add("--log-level=3");


                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.ChromiumHeadless:
                launchOptions.Headless = true;

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);

                break;
            case BrowserTypes.Chrome:
                launchOptions.Headless = false;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");

                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.ChromeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.Edge:
                launchOptions.Headless = false;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.EdgeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.Firefox:
                launchOptions.Headless = false;
                args.Add("--acceptInsecureCerts=true");

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.Launch(launchOptions);
                break;
            case BrowserTypes.FirefoxHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.Launch(launchOptions);
                break;
            case BrowserTypes.Webkit:
                launchOptions.Headless = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.Launch(launchOptions);
                break;
            case BrowserTypes.WebkitHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.Launch(launchOptions);
                break;
        }

        InitializeBrowserContextAndPage(wrappedBrowser);
    }

    internal static async void InitializeBrowserContextAndPage(WrappedBrowser wrappedBrowser)
    {
        BrowserNewContextOptions options = new();

        if (wrappedBrowser.CurrentContext is not null)
        {
            await wrappedBrowser.CurrentContext.WrappedBrowserContext.DisposeAsync();
        }

        if (WebSettings.PlaywrightSettings is not null && WebSettings.PlaywrightSettings.ContextOptions is not null)
        {
            options = WebSettings.PlaywrightSettings.ContextOptions;
        }

        wrappedBrowser.CurrentContext = wrappedBrowser.Browser.NewContext(options);
        wrappedBrowser.CurrentPage = wrappedBrowser.CurrentContext.NewPage();

    }


    private static int GetFreeTcpPort()
    {
        Thread.Sleep(100);
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
    }

    private static WebSettingsOptions WebSettings => ConfigurationRootInstance.GetSection<WebSettingsOptions>(WebSettingsOptions.SectionName);
}
