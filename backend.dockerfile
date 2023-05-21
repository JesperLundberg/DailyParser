FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source

COPY src/Api/ Api/
COPY src/DataAccess/ DataAccess/
COPY src/Model/ Model/
COPY src/Parser/ Parser/
RUN dotnet restore Api/Api.csproj

RUN dotnet publish Api/Api.csproj -c release -o /app

# Final container

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Api.dll"]
