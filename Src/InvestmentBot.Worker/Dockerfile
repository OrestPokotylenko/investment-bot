FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the entire solution
COPY . .

# Restore, build, and publish the Worker project
RUN dotnet restore Src/InvestmentBot.Worker/InvestmentBot.Worker.csproj
RUN dotnet build Src/InvestmentBot.Worker/InvestmentBot.Worker.csproj -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish Src/InvestmentBot.Worker/InvestmentBot.Worker.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InvestmentBot.Worker.dll"]