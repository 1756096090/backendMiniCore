# Establece la imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Establece el directorio de trabajo
WORKDIR /app

# Copia los archivos del proyecto al contenedor
COPY . .

# Restaura las dependencias
RUN dotnet restore

# Publica el proyecto
RUN dotnet publish -c Release -o /app/publish

# Establece la imagen base para la ejecución (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Establece el directorio de trabajo
WORKDIR /app

# Copia los archivos publicados del contenedor anterior
COPY --from=build /app/publish .

# Expone el puerto que utiliza tu aplicación
EXPOSE 80

# Establece el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "MiAplicacion.dll"]
