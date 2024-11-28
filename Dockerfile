FROM mcr.microsoft.com/playwright/dotnet:v1.48.0-noble AS base
WORKDIR /app

COPY . .

WORKDIR "/app/tests/DockerSystemTests" 

RUN dotnet restore

RUN dotnet build --configuration Release --no-restore

CMD ["dotnet", "test", "--configuration", "Release", "--logger:trx"]
