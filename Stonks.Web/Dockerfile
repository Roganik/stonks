FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["Stonks.Web/Stonks.Web.csproj", "Stonks.Web/"]
RUN dotnet restore "Stonks.Web/Stonks.Web.csproj"
COPY . .
WORKDIR "/src/Stonks.Web"
RUN dotnet build "Stonks.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stonks.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stonks.Web.dll"]
