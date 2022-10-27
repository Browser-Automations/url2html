using HtmlAgilityPack;
using PuppeteerSharp;

//Loads url using http request
var content = await UsingHttpRequestAsync("http://example.com");
Console.WriteLine(content);

//Load url using HtmlagilityPack Parser
content = await UsingHtmlParserAsync("http://example.com");
Console.WriteLine(content);

//Load url using puppeteerSharp. Best for cases where you want to render Javascript
content = await UsingBrowserAsync("http://example.com");
Console.WriteLine(content);

Console.ReadKey();

async Task<string> UsingHttpRequestAsync(string url)
{
    using (HttpClient client = new HttpClient())
    {
        return await client.GetStringAsync(url);
    }

}

async Task<string> UsingHtmlParserAsync(string url)
{
    HtmlWeb web = new HtmlWeb();
    var doc = await web.LoadFromWebAsync(url);
    return doc.DocumentNode.OuterHtml;
}

async Task<string> UsingBrowserAsync(string url, bool headless=true)
{
    string content = string.Empty;
    using var browserFetcher = new BrowserFetcher();
    await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
    var browser = await Puppeteer.LaunchAsync(new LaunchOptions
    {
        Headless = headless
    });
    var page = (await browser.PagesAsync())[0];
    await page.GoToAsync(url);
    await page.WaitForNetworkIdleAsync(new WaitForNetworkIdleOptions { Timeout = 30000 });
    content = await page.GetContentAsync();
    await browser.CloseAsync();
    return content;
}
