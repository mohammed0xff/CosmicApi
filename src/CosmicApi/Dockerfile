FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80

# Creates /app in container if it does not already exist
# Ports code into /app
# see : https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CosmicApi/CosmicApi.csproj", "src/CosmicApi/"]
COPY ["src/CosmicApi.Application/CosmicApi.Application.csproj", "src/CosmicApi.Application/"]
COPY ["src/CosmicApi.Domain/CosmicApi.Domain.csproj", "src/CosmicApi.Domain/"]
COPY ["src/CosmicApi.Infrastructure/CosmicApi.Infrastructure.csproj", "src/CosmicApi.Infrastructure/"]
RUN dotnet restore "src/CosmicApi/CosmicApi.csproj"
COPY . .
WORKDIR "/src/src/CosmicApi"
RUN dotnet build "CosmicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CosmicApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CosmicApi.dll"]