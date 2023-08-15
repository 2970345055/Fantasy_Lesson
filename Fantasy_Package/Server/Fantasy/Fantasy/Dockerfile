FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Fantasy/Fantasy.csproj", "Fantasy/"]
RUN dotnet restore "Fantasy/Fantasy.csproj"
COPY . .
WORKDIR "/src/Fantasy"
RUN dotnet build "Fantasy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fantasy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fantasy.dll"]
