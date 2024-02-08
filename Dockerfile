FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       clang zlib1g-dev

USER $APP_UID
WORKDIR /app
EXPOSE 8080
WORKDIR /source

COPY . .
RUN dotnet publish -o /app ManualShippingProvider.csproj

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["/app/ManualShippingProvider"]
