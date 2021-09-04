FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

COPY "Platforms.Api" "Platforms.Api"
COPY "Platforms.Domain" "Platforms.Domain"

WORKDIR "/src/Platforms.Api"
RUN dotnet restore "Platforms.Api.csproj"

RUN dotnet build "Platforms.Api.csproj" -c Release -o /src/build

RUN dotnet publish "Platforms.Api.csproj" -c Release -o /src/publish

FROM base as final
WORKDIR /app
COPY --from=build-env /src/publish .
ENTRYPOINT ["dotnet", "Platform.Api.dll"]


