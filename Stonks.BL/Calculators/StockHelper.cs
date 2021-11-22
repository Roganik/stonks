using System;

namespace Stonks.BL.Calculators
{
    public class StockHelper
    {
        public static string CalcDiffString(float priceFrom, float priceTo)
        {
            var diff = Math.Round((priceTo / priceFrom - 1.0) * 100, 2);
            var diffStr = $"{diff} %";
            if (diff > 0)
            {
                diffStr = "+" + diffStr;
            }

            return diffStr;
        }

        public static object CalcFinanceUrls(string symbol)
        {
            // todo: generate links to Vanin and Yahoo finance
            throw new NotImplementedException();
        }
    }
}