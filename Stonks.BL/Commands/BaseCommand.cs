using System.Threading;
using System.Threading.Tasks;
using Stonks.Db;

namespace Stonks.BL.Commands
{
    public abstract class BaseCommand<TIn, TOut>
    {
        protected readonly StocksDbContext Stocks;
        protected readonly FantasyDbContext Fantasy;

        protected BaseCommand(StocksDbContext stocks, FantasyDbContext fantasy)
        {
            Stocks = stocks;
            Fantasy = fantasy;
        }

        public Task<TOut> Exe(TIn model, CancellationToken cancellationToken)
        {
            return Execute(model, cancellationToken);
        }

        protected abstract Task<TOut> Execute(TIn model, CancellationToken cancellationToken);
    }
}