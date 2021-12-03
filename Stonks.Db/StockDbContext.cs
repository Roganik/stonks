using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stonks.Db.Models;

namespace Stonks.Db
{
    public class StocksDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public readonly string ConnectionName = "StocksDb";

        public string ConnectionString { get; private set; }

        public DbSet<Stock> Stocks { get; set; }

        public StocksDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString(ConnectionName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}