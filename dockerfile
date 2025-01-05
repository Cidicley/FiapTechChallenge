# Usar a imagem base para o SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copiar o arquivo .csproj da aplicação para o diretório /App
COPY FiapTechChallenge/FiapTechChallengeApi.csproj ./FiapTechChallenge/

# Restaurar as dependências
RUN dotnet restore ./FiapTechChallenge/FiapTechChallengeApi.csproj

# Copiar todo o código da aplicação para o container
COPY . ./

# Rodar os testes de unidade e integração
RUN dotnet test ./tests/FiapTechChallenge.Test/FiapTechChallenge.Test.csproj --configuration Release --logger "trx"

# Publicar a aplicação
RUN dotnet publish -c Release -o out ./FiapTechChallenge/FiapTechChallengeApi.csproj

# Usar a imagem do ASP.NET para o container de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copiar a aplicação publicada do estágio de build para o container final
COPY --from=build-env /App/out .

# Definir o comando de entrada do container para rodar a aplicação
ENTRYPOINT ["dotnet", "FiapTechChallengeApi.dll"]
