FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN mkdir /app
RUN chown app:app /app
WORKDIR /app
USER app

COPY --chown=app:app *.sln .
COPY --chown=app:app ./src ./src
COPY --chown=app:app ./tests ./tests

RUN dotnet clean ./src/API --output ./bin
RUN dotnet restore
RUN dotnet build --configuration Release --no-restore

################################################

FROM base AS test
ENTRYPOINT ["dotnet", "test", "--configuration", "Release", "--no-build"]

################################################

FROM base AS publish
RUN dotnet publish ./src/API --output ./bin --no-build

################################################

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS production
WORKDIR /app/bin
USER app

COPY --from=publish /app/bin /app/bin
ENTRYPOINT ["dotnet", "API.dll"]
