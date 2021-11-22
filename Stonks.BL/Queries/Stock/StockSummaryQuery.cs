using System;
using System.Threading.Tasks;
using Yahoo.Finance;

namespace Stonks.BL.Queries.Stock
{
    public class StockSummaryQuery
    {
        public async Task<EquitySummaryData> Get(string symbol)
        {
            var stock = Equity.Create(symbol);
            await stock.DownloadSummaryAsync();

            return stock.Summary;
        }
    }
}