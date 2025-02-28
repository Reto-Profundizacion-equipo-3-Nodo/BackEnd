# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
# Copiar archivos del proyecto y restaurar dependencias
COPY . .
RUN dotnet restore
# Compilar y publicar la aplicación
RUN dotnet publish "FundAntivirus.csproj" -c Release -o /out

# Etapa de ejecución en una imagen optimizada
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
# Copiar la aplicación publicada
COPY --from=build /out .

# Configurar la aplicación para usar solo HTTP
# ENV ASPNETCORE_URLS=http://+:8080
# ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# Exponer el puerto para la aplicación
EXPOSE 8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "FundAntivirus.dll"]
