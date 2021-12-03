using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTables;
using Stonks.BL.Calculators;
using Stonks.BL.Commands.InitialLoad;
using Stonks.BL.Queries.Stock;
using Stonks.BL.Queries.Youtube;
using Stonks.Db;

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
            var fantasy = new DesignTimeDbContextFactoryFantasyDbContext();
            var context = fantasy.CreateDbContext(new []{""});

            var query = new YoutubeHistoryQuery(context);
            var results = await query.Get("UCnOJQq22gDaX5pjGCHKRf8Q");
        }

        static async Task Test4()
        {
            var fantasyContext = new DesignTimeDbContextFactoryFantasyDbContext().CreateDbContext(new []{""});
            var stocksContext = new DesignTimeDbContextFactoryStocksDbContext().CreateDbContext(new []{""});

            var cmd = new InvestReviewYoutubeChannelSourceLoaderCommand(stocksContext, fantasyContext);
            var channels = new List<InvestReviewYoutubeChannelSourceLoaderCommand.InModel>
            {
                new InvestReviewYoutubeChannelSourceLoaderCommand.InModel()
                {
                    // Simple Capitalism
                    ChannelId = "UCnOJQq22gDaX5pjGCHKRf8Q",
                    Url = "https://www.youtube.com/channel/UCnOJQq22gDaX5pjGCHKRf8Q",
                },
                new InvestReviewYoutubeChannelSourceLoaderCommand.InModel()
                {
                    // Nazar
                    ChannelId = "UCbhXz_OPX3B0eTimt24PGVQ",
                    Url = "https://www.youtube.com/channel/UCbhXz_OPX3B0eTimt24PGVQ",
                },
                new InvestReviewYoutubeChannelSourceLoaderCommand.InModel()
                {
                    // Vanin
                    ChannelId = "UCRcGq7mDsvVRjaxC4f6Zjxw",
                    Url = "https://www.youtube.com/channel/UCRcGq7mDsvVRjaxC4f6Zjxw",
                },
            };

            foreach (var inModel in channels)
            {
                var result = await cmd.Exe(inModel, CancellationToken.None);
                if (!result.IsSuccess)
                {
                    throw new Exception(result.Message);
                }
            }
        }
    }
}