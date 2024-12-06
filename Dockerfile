FROM mcr.microsoft.com/playwright/dotnet:v1.48.0-noble AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
# ARG TEST_USER="playwright"
# RUN adduser --system --uid 1001 playwright
COPY ["tests/DockerSystemTests/DockerSystemTests.csproj", "./tests/DockerSystemTests/"]
COPY . .
WORKDIR "/app/tests/DockerSystemTests" 
RUN dotnet restore
RUN dotnet build "./DockerSystemTests.csproj" -c Release -o /app/build

# ENV PLAYWRIGHT_TEST_BASE_URL=http://host.docker.internal:5106/

FROM build AS publish
RUN dotnet publish "./DockerSystemTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# USER ${TEST_USER}
# ENV PLAYWRIGHT_TEST_BASE_URL=http://host.docker.internal:5106/

CMD ["dotnet", "test", "*DockerSystemTests.dll", "--configuration", "Release", "--logger:trx"]

