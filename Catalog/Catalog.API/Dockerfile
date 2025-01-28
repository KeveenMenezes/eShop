FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Service/Catalog/Catalog.API/Catalog.API.csproj", "Service/Catalog/Catalog.API/"]
RUN dotnet restore "./Service/Catalog/Catalog.API/./Catalog.API.csproj"
COPY . .
WORKDIR "/src/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]