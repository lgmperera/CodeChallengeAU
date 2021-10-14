FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY CodeChallenge.sln .
COPY ["CodeChallengeTest/CodeChallengeTest.csproj", "CodeChallengeTest/"]
COPY ["CodeChallenge/CodeChallenge.csproj", "CodeChallenge/"]
RUN dotnet restore CodeChallenge.sln

COPY . .
RUN dotnet build
RUN dotnet test --logger:trx 

WORKDIR "/src/CodeChallengeTest"
RUN dotnet build "CodeChallengeTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeChallengeTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeChallengeTest.dll"]

