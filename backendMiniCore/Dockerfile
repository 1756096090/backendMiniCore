# Etapa de construcción: Usamos el SDK de .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiar el archivo de proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos del proyecto
COPY . ./

# Construir el proyecto en modo Release
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish -c Release -o /app/publish

# Etapa de producción: Usamos la imagen de ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build /app/publish .

# Configurar el comando de inicio del contenedor
ENTRYPOINT ["dotnet", "backendMiniCore.dll"]
