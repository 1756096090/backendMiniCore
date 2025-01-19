# Usar la imagen del SDK de .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos del proyecto
COPY . /app/

# Restaurar dependencias
RUN dotnet restore

# Construir el proyecto
RUN dotnet build -c Release

# Publicar el proyecto
RUN dotnet publish -c Release -o /app/publish

# Usar la imagen de ASP.NET para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "backendMiniCore.dll"]
