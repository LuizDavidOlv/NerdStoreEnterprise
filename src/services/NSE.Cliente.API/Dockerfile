#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/NSE.Cliente.API/NSE.Cliente.API.csproj", "services/NSE.Cliente.API/"]
COPY ["building-blocks/NSE.Core/NSE.Core.csproj", "building-blocks/NSE.Core/"]
COPY ["building-blocks/NSE.WebApi.Core/NSE.WebApi.Core.csproj", "building-blocks/NSE.WebApi.Core/"]
COPY ["building-blocks/NSE.MessageBus/NSE.MessageBus.csproj", "building-blocks/NSE.MessageBus/"]

RUN dotnet restore "services/NSE.Cliente.API/NSE.Cliente.API.csproj"
COPY services/NSE.Cliente.API/ services/NSE.Cliente.API/
COPY building-blocks/NSE.Core/ building-blocks/NSE.Core/
COPY building-blocks/NSE.WebApi.Core/ building-blocks/NSE.WebApi.Core/
COPY building-blocks/NSE.MessageBus/ building-blocks/NSE.MessageBus/

WORKDIR "services/NSE.Cliente.API"
RUN dotnet build "NSE.Cliente.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Cliente.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Cliente.API.dll"]