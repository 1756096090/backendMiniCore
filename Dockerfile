FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backendMiniCore/backendMiniCore.csproj", "backendMiniCore/"]
RUN dotnet restore "backendMiniCore/backendMiniCore.csproj"
COPY . .
WORKDIR "/src/backendMiniCore"
RUN dotnet build "backendMiniCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backendMiniCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backendMiniCore.dll"]
