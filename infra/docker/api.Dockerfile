FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN mkdir /app
RUN chown app:app /app
WORKDIR /app

# Install dependencies for playwright
RUN apt-get update -q && \
  apt-get install -y -qq --no-install-recommends \
  xvfb \
  libxcomposite1 \
  libxdamage1 \
  libatk1.0-0 \
  libasound2 \
  libdbus-1-3 \
  libnspr4 \
  libgbm1 \
  libatk-bridge2.0-0 \
  libcups2 \
  libxkbcommon0 \
  libatspi2.0-0 \
  libnss3 \
  libpango-1.0-0 \
  libcairo2 \
  libxrandr2

# Install client-side certificate for mutual TLS
COPY ./zscaler.cer /usr/local/share/ca-certificates/zscaler.crt
RUN apt-get install --assume-yes ca-certificates
RUN update-ca-certificates

COPY --chown=app:app *.sln .
COPY --chown=app:app ./src ./src
COPY --chown=app:app ./tests ./tests

RUN mkdir -p /app/src/PriceAlert/bin
RUN chown app:app /app/src/PriceAlert/bin

RUN mkdir -p /app/src/PriceAlert/obj
RUN chown app:app /app/src/PriceAlert/obj

# Install playwright
RUN dotnet build
RUN dotnet tool install --global Microsoft.Playwright.CLI
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN playwright install chromium

################################################

FROM base AS development
ENTRYPOINT ["dotnet", "watch", "run", "--project", "/app/src/PriceAlert"]

################################################

FROM base AS build
RUN dotnet restore
RUN dotnet clean /app/src/PriceAlert --output ./bin
RUN dotnet build --configuration Release --no-restore

################################################

FROM build AS test
ENTRYPOINT ["dotnet", "test", "--configuration", "Release", "--no-build"]

################################################

FROM build AS publish
RUN dotnet publish --configuration Release /app/src/PriceAlert --output ./bin --no-build

################################################

FROM publish AS production
WORKDIR /app/bin
ENTRYPOINT ["dotnet", "PriceAlert.dll"]
