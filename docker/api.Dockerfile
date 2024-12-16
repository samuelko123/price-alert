FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN mkdir /app
RUN chown app:app /app
WORKDIR /app
USER app

COPY --chown=app:app *.sln .
COPY --chown=app:app ./src ./src
COPY --chown=app:app ./tests ./tests
RUN dotnet restore

################################################

FROM base AS test
ENTRYPOINT ["dotnet", "test", "--no-restore"]

################################################

FROM base AS build
RUN dotnet clean ./src/API --output ./bin
RUN dotnet publish ./src/API --configuration Release --output ./bin --no-restore

################################################

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS production
WORKDIR /app/bin
USER app

COPY --from=build /app/bin /app/bin
ENTRYPOINT ["dotnet", "API.dll"]
