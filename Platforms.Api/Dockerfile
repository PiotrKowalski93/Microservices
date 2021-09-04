#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
#WORKDIR /src
#COPY ["Platform.Api/Platform.Api.csproj", "Platform.Api/"]
#RUN dotnet restore "Platform.Api/Platform.Api.csproj"
#COPY . .
#WORKDIR "/src/Platform.Api"
#RUN dotnet build "Platform.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Platform.Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Platform.Api.dll"]


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build-env
WORKDIR /app

COPY "Platform.Domain/Platforms.Domain.csproj" ./
COPY "Platforms.Api.csproj" ./
RUN dotnet restore

#COPY . .
RUN dotnet publish "Platform.Api.csproj" -c Release -o /app/out
RUN dotnet publish "Platforms.Domain.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Platform.Api.dll"]