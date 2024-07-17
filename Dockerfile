# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CreditService.csproj", "."]
RUN dotnet restore "./CreditService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CreditService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CreditService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY wait-for-it.sh .

# Altere permissões como root
USER root
RUN chmod +x wait-for-it.sh

# Volte para o usuário app
USER app

ENTRYPOINT ["./wait-for-it.sh", "rabbitmq", "5672", "dotnet CreditService.dll"]
