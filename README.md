# URL2HTML
Script that will load a URL and returns HTML(Source Page)

## Quickstart

```c#
//Load url using http request
var content = await UsingHttpRequestAsync("http://example.com");
Console.WriteLine(content);

//Load url using HtmlagilityPack Parser
content = await UsingHtmlParserAsync("http://example.com");
Console.WriteLine(content);

//Load url using puppeteerSharp. Best for cases where you want to render Javascript
content = await UsingBrowserAsync("http://example.com");
Console.WriteLine(content);
