FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN mkdir /app
RUN chown app:app /app
WORKDIR /app

COPY ./zscaler.cer /usr/local/share/ca-certificates/zscaler.crt
RUN apt-get install --assume-yes ca-certificates
RUN update-ca-certificates

USER app

COPY --chown=app:app *.sln .
COPY --chown=app:app ./src ./src
COPY --chown=app:app ./tests ./tests

RUN mkdir -p /app/src/PriceAlert/bin
RUN chown app:app /app/src/PriceAlert/bin

RUN mkdir -p /app/src/PriceAlert/obj
RUN chown app:app /app/src/PriceAlert/obj

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

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS production
WORKDIR /app/bin

COPY ./zscaler.cer /usr/local/share/ca-certificates/zscaler.crt
RUN apt-get install --assume-yes ca-certificates
RUN update-ca-certificates

USER app

COPY --from=publish /app/bin /app/bin
ENTRYPOINT ["dotnet", "PriceAlert.dll"]
