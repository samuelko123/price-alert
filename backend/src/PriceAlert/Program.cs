using System;
using PriceAlert.Infrastructure.Kmart;

namespace PriceAlert;

internal class Program
{
    private static void Main()
    {
        var text = KmartScraper.Scrape().Result;
        Console.WriteLine(text);
    }
}
