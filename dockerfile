# Etapa 1: Construção e Testes
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copiar o arquivo de projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código
COPY . ./

# Rodar os testes de unidade e integração antes de construir a aplicação
RUN dotnet test ./tests/FiapTechChallenge.Test/FiapTechChallenge.Test.csproj --configuration Release --logger "trx"

# Build e publicação para produção
RUN dotnet publish -c Release -o out

# Etapa 2: Imagem de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copiar a aplicação publicada do estágio anterior
COPY --from=build-env /App/out .

# Definir o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "FiapTechChallengeApi.dll"]
