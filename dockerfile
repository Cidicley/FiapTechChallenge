FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copiar tudo
COPY . ./
# Restore
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Usar a imagem do .NET Runtime 8 para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
# Copiar a aplicação publicada do estágio anterior
COPY --from=build-env /App/out .

# Definir o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "FiapTechChallengeApi.dll"]