using System;
using System.Threading.Tasks;
using ConsoleTables;
using Stonks.BL.Calculators;
using Stonks.BL.Queries.Stock;
using Stonks.BL.Queries.Youtube;

namespace stonks.cmd
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return 0;
        }

        static async Task Test1()
        {
            var historyQuery = new StockHistoryQuery();
            var history = await historyQuery.Get("AGRO.ME", DateTime.Now.AddMonths(-1), DateTime.Now);

            var table = new ConsoleTable();
            table.AddColumn(new[] { "Date", "Open", "Close", "Diff" });
            foreach (var hd in history.HistoricalData)
            {
                var diffStr = StockHelper.CalcDiffString(hd.Open, hd.Close);
                table.AddRow(hd.Date.ToShortDateString(), hd.Open, hd.Close, diffStr);
            }
            table.Write();
        }

        static async Task Test2()
        {
            // https://www.youtube.com/watch?v=kFqemi7dq60
            // Cigna Corp - Aug 10, 2021 - CI

            var query = new StockFantasyTeamPerformanceQuery();
            var result = await query.Get("CI", new DateTime(2021, 08, 10));
        }

        static async Task Test3()
        {
            var query = new YoutubeHistoryQuery();
            var results = await query.Get("UCnOJQq22gDaX5pjGCHKRf8Q");
        }
    }
}