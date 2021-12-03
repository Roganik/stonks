using System;
using System.Linq;
using System.Threading.Tasks;
using Stonks.BL.Calculators;
using Yahoo.Finance;

namespace Stonks.BL.Queries.Stock
{
    public class StockFantasyTeamPerformanceQuery
    {
        public class ResultModel
        {
            public string Symbol { get; set; }
            public string CompanyName { get; set; }
            public DateTime DateMentioned { get; set; }
            public float PriceOnDateMentioned { get; set; }
            public float PriceNow { get; set; }
            public string PriceDiffPercent { get; set; }
        }

        public async Task<ResultModel> Get(string symbol, DateTime dateMentioned)
        {
            var historyQuery = new StockHistoryQuery();
            var summaryQuery = new StockSummaryQuery();

            // todo: move calculation of history to DB coz the dateMentioned is a constant
            var priceOneDateMentioned = await PriceOneDateMentioned(symbol, dateMentioned, historyQuery);
            var summary = await summaryQuery.Get(symbol);

            return new ResultModel
            {
                Symbol = symbol,
                CompanyName = summary.Name,
                DateMentioned = dateMentioned,
                PriceOnDateMentioned = priceOneDateMentioned,
                PriceNow = summary.Price,
                PriceDiffPercent = StockHelper.CalcDiffString(priceOneDateMentioned, summary.Price)
            };
        }

        private static async Task<float> PriceOneDateMentioned(string symbol, DateTime dateMentioned, StockHistoryQuery historyQuery)
        {
            var history = await historyQuery.Get(symbol, dateMentioned, dateMentioned.AddDays(7));
            if (history.DownloadResult != HistoricalDataDownloadResult.Successful)
            {
                throw new NotImplementedException();
            }

            var priceOneDateMentioned = history.HistoricalData.OrderBy(d => d.Date).FirstOrDefault()?.Close;
            if (priceOneDateMentioned == null)
            {
                throw new NotImplementedException();
            }

            return priceOneDateMentioned.Value;
        }
    }
}