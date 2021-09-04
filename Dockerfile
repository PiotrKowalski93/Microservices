#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build-env
WORKDIR /src

COPY "Platforms.Api" "Platforms.Api"
COPY "Platforms.Domain" "Platforms.Domain"

WORKDIR "/src/Platforms.Api"
RUN dotnet restore "Platforms.Api.csproj"

# WORKDIR "/src/Platforms.Api"
# RUN dotnet build "Platforms.Api.csproj" -c Release -o /src/build

RUN dotnet publish "Platforms.Api.csproj" -c Release -o /src/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "Platform.Api.dll"]


