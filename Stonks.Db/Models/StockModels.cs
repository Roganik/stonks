using System;

namespace Stonks.Db.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public string StockSymbol { get; set; }
        public string Exchange { get; set; }

        public DateTime Updated { get; set; }

        public DateTime HasDataFrom { get; set; }
        public DateTime HasDataTo { get; set; }
        public string DataJson { get; set; }
    }
}