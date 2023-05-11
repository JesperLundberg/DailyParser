FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source

COPY src/Api/ Api/
COPY src/DataAccess/ DataAccess/
COPY src/Model/ Model/
COPY src/Parser/ Parser/
RUN dotnet restore Api/Api.csproj

RUN dotnet publish Api/Api.csproj -c release -o /app

# Todo: When time is right, a real certificate is needed.
#       This cert generation is only for development. In
#       production the real cert is to be mounted instead.
# RUN dotnet dev-certs https -ep /source/api/cert.pfx -p password1234

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
# WORKDIR /https
# COPY --from=build /source/api/cert.pfx .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Api.dll", "-r linux-musl-x64"]
