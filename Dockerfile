# Imagen base para la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG COLOMBIA_API=https://api-colombia.com
WORKDIR /src

# Copiar los archivos de proyecto
COPY Client/Client.csproj Client/
COPY Data/Data.csproj Data/

# Restaurar las dependencias de ambos proyectos
RUN dotnet restore "Client/Client.csproj"
RUN dotnet restore "Data/Data.csproj"

# Copiar el resto del código y construir
COPY . .
WORKDIR "/src/Client"
RUN dotnet build "Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar la aplicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.dll"]