using System.Threading.Tasks;

namespace PriceAlert.Infrastructure.Kmart;

public interface IKmartScraper
{
  public Task<string> Scrape();
}
