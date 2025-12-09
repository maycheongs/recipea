# Adjust DOTNET_OS_VERSION as desired
ARG DOTNET_OS_VERSION="-alpine"
ARG DOTNET_SDK_VERSION=8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0${DOTNET_OS_VERSION} AS base
WORKDIR /app
EXPOSE 8080

# Install supercronic
RUN apk add --no-cache curl \
    && curl -L https://github.com/aptible/supercronic/releases/download/v0.2.26/supercronic-linux-amd64 -o /usr/local/bin/supercronic \
    && chmod +x /usr/local/bin/supercronic

FROM mcr.microsoft.com/dotnet/sdk:8.0${DOTNET_OS_VERSION} AS build
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

# Copy crontab file
COPY crontab /etc/cron.d/reset-db

# Create startup script
RUN echo '#!/bin/sh' > /start.sh && \
    echo 'supercronic /etc/cron.d/reset-db &' >> /start.sh && \
    echo 'exec dotnet Recipea.dll' >> /start.sh && \
    chmod +x /start.sh

ENTRYPOINT ["/start.sh"]
