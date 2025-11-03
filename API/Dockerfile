# ====== build ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./API/*.csproj ./API/
RUN dotnet restore ./API/API.csproj

COPY ./API ./API
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish

# ====== runtime ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080 DOTNET_EnableDiagnostics=0
COPY --from=build /app/publish ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]
