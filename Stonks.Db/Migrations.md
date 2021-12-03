# Useful commands:

### Preconditions:

_I use bash and assume this envinronment for commands below._

__1.__ Set [connection strings](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings) by using environment variables.

in ~/.bash_profile
```
# Stonks databases
export STONKS_ConnectionStrings__StocksDb="Data Source=$HOME/Projects/stonks/db/stocks.sqlite"
export STONKS_ConnectionStrings__FantasyDb="Data Source=$HOME/Projects/stonks/db/fantasy.sqlite"
```

__2.__ Create databases

```
mkdir ~/Projects/stonks/db/
touch ~/Projects/stonks/db/stocks.sqlite
touch ~/Projects/stonks/db/fantasy.sqlite
```

__3.__ All commands below are excecuted inside __Stonks.Db__ folder

``` 
cd Stonks.Db/ 
```

### To see available contexts:
```
dotnet ef dbcontext list
```

### Adding new migrations:
```
dotnet ef migrations add --help

dotnet ef migrations add <NAME> --context Stonks.Db.FantasyDbContext --output-dir Migrations-Fantasy
dotnet ef migrations add <NAME> --context Stonks.Db.StocksDbContext --output-dir Migrations-Stocks
```


Commands:
```
dotnet ef database update --help

dotnet ef database update --context Stonks.Db.FantasyDbContext
dotnet ef database update --context Stonks.Db.StocksDbContext
```

# Docs:

* https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
* https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
