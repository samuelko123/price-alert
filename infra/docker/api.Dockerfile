FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN mkdir /app
RUN chown app:app /app
WORKDIR /app
USER app

COPY --chown=app:app *.sln .
COPY --chown=app:app ./src ./src
COPY --chown=app:app ./tests ./tests

################################################

FROM base AS development
ENTRYPOINT ["dotnet", "watch", "run", "--project", "/app/src/API"]

################################################

FROM base AS build
RUN dotnet restore
RUN dotnet clean /app/src/API --output ./bin
RUN dotnet build --configuration Release --no-restore

################################################

FROM build AS test
ENTRYPOINT ["dotnet", "test", "--configuration", "Release", "--no-build"]

################################################

FROM build AS publish
RUN dotnet publish --configuration Release /app/src/API --output ./bin --no-build

################################################

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS production
WORKDIR /app/bin
USER app

COPY --from=publish /app/bin /app/bin
ENTRYPOINT ["dotnet", "PriceAlert.dll"]
