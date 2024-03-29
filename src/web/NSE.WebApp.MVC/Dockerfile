#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./web/NSE.WebApp.MVC/NSE.WebApp.MVC.csproj", "web/NSE.WebApp.MVC/"]
COPY ["./building-blocks/NSE.Core/NSE.Core.csproj", "building-blocks/NSE.Core/"]
COPY ["./building-blocks/NSE.WebApi.Core/NSE.WebApi.Core.csproj", "building-blocks/NSE.WebApi.Core/"]

RUN dotnet restore "web/NSE.WebApp.MVC/NSE.WebApp.MVC.csproj"
COPY web/NSE.WebApp.MVC/ web/NSE.WebApp.MVC/
COPY building-blocks/NSE.Core/ building-blocks/NSE.Core/
COPY building-blocks/NSE.WebApi.Core/ building-blocks/NSE.WebApi.Core/

WORKDIR "web/NSE.WebApp.MVC"
RUN dotnet build "NSE.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.WebApp.MVC.dll"]