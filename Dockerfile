# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia a solution
COPY "AlianceDDD.sln" ./

# Copia os arquivos de cada projeto
COPY Aliance.Domain/Aliance.Domain.csproj Aliance.Domain/
COPY Aliance.Application/Aliance.Application.csproj Aliance.Application/
COPY Aliance.Crosscutting/Aliance.Crosscutting.csproj Aliance.Crosscutting/
COPY Aliance.Infrastructure/Aliance.Infrastructure.csproj Aliance.Infrastructure/
COPY Aliance.API/Aliance.API.csproj Aliance.API/


# Restaura apenas a API (e tudo que ela referencia)
RUN dotnet restore Aliance.API/Aliance.API.csproj

# Copia todo o restante do código
COPY . .

# Publica a API
RUN dotnet publish Aliance.API/Aliance.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Aliance.API.dll"]
