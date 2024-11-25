# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082
EXPOSE 8083


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Api/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Api.csproj", "Src/Api/"]
COPY ["Src/Core/Ioc/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Ioc.csproj", "Src/Core/Ioc/"]
COPY ["Src/Core/Application/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.csproj", "Src/Core/Application/"]
COPY ["Src/Core/Domain/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.csproj", "Src/Core/Domain/"]
COPY ["Src/External/Infra/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.csproj", "Src/External/Infra/"]
RUN dotnet restore "./Src/Api/FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Api.csproj"
COPY . .
WORKDIR "/src/Src/Api"
RUN dotnet build "./FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Api.dll"]