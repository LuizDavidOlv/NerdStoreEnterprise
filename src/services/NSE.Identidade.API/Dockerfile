#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/NSE.Identidade.API/NSE.Identidade.API.csproj", "services/NSE.Identidade.API/"]
COPY ["building-blocks/NSE.Core/NSE.Core.csproj", "building-blocks/NSE.Core/"]
COPY ["building-blocks/NSE.WebApi.Core/NSE.WebApi.Core.csproj", "building-blocks/NSE.WebApi.Core/"]
COPY ["building-blocks/NSE.MessageBus/NSE.MessageBus.csproj", "building-blocks/NSE.MessageBus/"]

RUN dotnet restore "services/NSE.Identidade.API/NSE.Identidade.API.csproj"
COPY services/NSE.Identidade.API/ services/NSE.Identidade.API/
COPY building-blocks/NSE.Core/ building-blocks/NSE.Core/
COPY building-blocks/NSE.WebApi.Core/ building-blocks/NSE.WebApi.Core/
COPY building-blocks/NSE.MessageBus/ building-blocks/NSE.MessageBus/

WORKDIR "services/NSE.Identidade.API"
RUN dotnet build "NSE.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Identidade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Identidade.API.dll"]