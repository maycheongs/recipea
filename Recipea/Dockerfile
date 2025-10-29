# Adjust DOTNET_OS_VERSION as desired
ARG DOTNET_OS_VERSION="-alpine"
ARG DOTNET_SDK_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["Recipea.csproj", "./"]
RUN dotnet restore "Recipea.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "Recipea.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recipea.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recipea.dll"]
