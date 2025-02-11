FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 7221

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Back
COPY ["./Back.csproj", "./"]
RUN dotnet restore "./Back.csproj"
COPY . .
RUN dotnet build "./Back.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Back.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final (Execução)
FROM base AS final
WORKDIR /app
# Ajuste de permissões antes de definir o usuário
RUN chown -R app:app /app
USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Back.dll"]
