using System.Threading.Tasks;
using Yahoo.Finance;

namespace Stonks.BL.Queries.Stock
{
    public class StockStatisticsQuery
    {
        public async Task<EquityStatisticalData> Get(string symbol)
        {
            var stock = Equity.Create(symbol);
            await stock.DownloadStatisticsAsync();

            return stock.Statistics;
        }
    }
}