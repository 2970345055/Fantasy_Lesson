FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Fantasy_Server/Fantasy_Server.csproj", "Fantasy_Server/"]
RUN dotnet restore "Fantasy_Server/Fantasy_Server.csproj"
COPY . .
WORKDIR "/src/Fantasy_Server"
RUN dotnet build "Fantasy_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fantasy_Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fantasy_Server.dll"]
