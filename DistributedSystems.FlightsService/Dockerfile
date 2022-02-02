#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

RUN apt update && apt install -y libc-dev

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DistributedSystems.FlightsService/DistributedSystems.FlightsService.csproj", "DistributedSystems.FlightsService/"]
RUN dotnet restore "DistributedSystems.FlightsService/DistributedSystems.FlightsService.csproj"
COPY . .
WORKDIR "/src/DistributedSystems.FlightsService"
RUN dotnet build "DistributedSystems.FlightsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DistributedSystems.FlightsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DistributedSystems.FlightsService.dll"]