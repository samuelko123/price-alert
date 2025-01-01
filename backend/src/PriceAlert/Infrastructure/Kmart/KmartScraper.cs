using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PriceAlert.Infrastructure.Kmart;

public class KmartScraper() : IKmartScraper
{
  public async Task<string> Scrape()
  {
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync();
    var page = await browser.NewPageAsync();
    await page.GotoAsync("https://google.com");
    var text = await page.InnerHTMLAsync("body");
    return text;
  }
}
