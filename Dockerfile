# Usa la imagen base de .NET SDK para la construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Establece el directorio de trabajo
WORKDIR /app

# Copia el archivo de proyecto (ajusta la ruta si es necesario)
COPY ./backendMiniCore/*.csproj ./backendMiniCore/

# Restaura las dependencias
RUN dotnet restore ./backendMiniCore/*.csproj

# Copia el resto del código fuente y construye la aplicación
COPY . ./ 
RUN dotnet publish ./backendMiniCore -c Release -o out

# Usa la imagen base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app/out . 

# Expone el puerto 80
EXPOSE 80

# Ejecuta la aplicación
ENTRYPOINT ["dotnet", "MiAplicacion.dll"]
