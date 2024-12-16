FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /app

COPY *.sln .
COPY ./src ./src
COPY ./tests ./tests
RUN dotnet restore

FROM base AS test
ENTRYPOINT ["dotnet", "test", "--no-restore"]
