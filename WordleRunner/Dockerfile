FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WordleRunner/WordleRunner.csproj", "WordleRunner/"]
RUN dotnet restore "WordleRunner/WordleRunner.csproj"
COPY . .
WORKDIR "/src/WordleRunner"
RUN dotnet build "WordleRunner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordleRunner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordleRunner.dll"]
