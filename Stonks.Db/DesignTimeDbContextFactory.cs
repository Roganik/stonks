using System;
using Microsoft.EntityFrameworkCore.Design;
using Stonks.Cfg;

namespace Stonks.Db
{
    // see: https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    public class DesignTimeDbContextFactoryStocksDbContext  : IDesignTimeDbContextFactory<StocksDbContext>
    {
        public StocksDbContext CreateDbContext(string[] args)
        {
            var cfg = ConfigBuilder.GetConfiguration();
            var ctx = new StocksDbContext(cfg);
            Console.WriteLine("StocksDbContext Connection String = " + ctx.ConnectionString);
            return ctx;
        }
    }

    public class DesignTimeDbContextFactoryFantasyDbContext  : IDesignTimeDbContextFactory<FantasyDbContext>
    {
        public FantasyDbContext CreateDbContext(string[] args)
        {
            var cfg = ConfigBuilder.GetConfiguration();
            var ctx = new FantasyDbContext(cfg);
            Console.WriteLine("FantasyDbContext Connection String = " + ctx.ConnectionString);
            return ctx;
        }
    }
}