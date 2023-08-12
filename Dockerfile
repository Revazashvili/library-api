FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.18 AS base
EXPOSE 80
EXPOSE 443
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.18 AS build
WORKDIR /src
COPY ./src .
RUN dotnet publish "./API/API.csproj" -c Release -o /publish

FROM base AS runtime
COPY --from=build /publish .
ENTRYPOINT ["dotnet","API.dll"]