#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/EntertainmentService/Presentation/Entertainment.API/Entertainment.API.csproj", "src/Services/EntertainmentService/Presentation/Entertainment.API/"]
COPY ["src/Services/EntertainmentService/Infrastructure/Entertainment.Infrastructure/Entertainment.Infrastructure.csproj", "src/Services/EntertainmentService/Infrastructure/Entertainment.Infrastructure/"]
COPY ["src/Services/EntertainmentService/Core/Entertainment.Application/Entertainment.Application.csproj", "src/Services/EntertainmentService/Core/Entertainment.Application/"]
COPY ["src/Services/EntertainmentService/Core/Entertainment.Domain/Entertainment.Domain.csproj", "src/Services/EntertainmentService/Core/Entertainment.Domain/"]
COPY ["src/Services/EntertainmentService/Infrastructure/Entertainment.Persistance/Entertainment.Persistance.csproj", "src/Services/EntertainmentService/Infrastructure/Entertainment.Persistance/"]
COPY ["src/BuildingBlocks/Common.Caching/Common.Caching/Common.Caching.csproj", "src/BuildingBlocks/Common.Caching/Common.Caching/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/Common.Messaging.MassTransit.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/Common.Messaging.RabbitMQ.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/"]
RUN dotnet restore "./src/Services/EntertainmentService/Presentation/Entertainment.API/./Entertainment.API.csproj"
COPY . .
WORKDIR "/src/src/Services/EntertainmentService/Presentation/Entertainment.API"
RUN dotnet build "./Entertainment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Entertainment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Entertainment.API.dll"]