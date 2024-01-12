#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/MealService/API/Meal.API/Meal.API.csproj", "src/Services/MealService/API/Meal.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
COPY ["src/Services/MealService/Application/Meal.Application/Meal.Application.csproj", "src/Services/MealService/Application/Meal.Application/"]
COPY ["src/BuildingBlocks/Common.Caching/Common.Caching/Common.Caching.csproj", "src/BuildingBlocks/Common.Caching/Common.Caching/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/Common.Messaging.RabbitMQ.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/"]
COPY ["src/Services/MealService/AutoMapper/Meal.Mapper/Meal.Mapper.csproj", "src/Services/MealService/AutoMapper/Meal.Mapper/"]
COPY ["src/Services/MealService/Infrastructure/Meal.Infrastructure/Meal.Infrastructure.csproj", "src/Services/MealService/Infrastructure/Meal.Infrastructure/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/Common.Messaging.MassTransit.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/"]
COPY ["src/Services/MealService/Domain/Meal.Domain/Meal.Domain.csproj", "src/Services/MealService/Domain/Meal.Domain/"]
RUN dotnet restore "./src/Services/MealService/API/Meal.API/./Meal.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MealService/API/Meal.API"
RUN dotnet build "./Meal.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Meal.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meal.API.dll"]