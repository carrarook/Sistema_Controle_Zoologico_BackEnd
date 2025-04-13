# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copia APENAS os arquivos do projeto (exclui Dockerfile)
COPY ["Sistema_Controle_Zoologico.csproj", "./"]
COPY ["Controllers/", "Controllers/"]
COPY ["Models/", "Models/"]
COPY ["Data/", "Data/"]
COPY ["Properties/", "Properties/"]
RUN dotnet restore "./Sistema_Controle_Zoologico.csproj"

# Copia o resto (exceto Dockerfile)
COPY . .
RUN dotnet publish "Sistema_Controle_Zoologico.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sistema_Controle_Zoologico.dll"]