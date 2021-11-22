using System;
using System.Threading.Tasks;
using Yahoo.Finance;

namespace Stonks.BL.Queries.Stock
{
    public class StockHistoryQuery
    {
        public async Task<(HistoricalDataDownloadResult DownloadResult, HistoricalDataRecord[] HistoricalData)>
            Get(string symbol, DateTime periodStart, DateTime periodEnd)
        {
            var hdp = new HistoricalDataProvider();
            await hdp.DownloadHistoricalDataAsync(symbol, periodStart, periodEnd);

            return (hdp.DownloadResult, hdp.HistoricalData);
        }
    }
}