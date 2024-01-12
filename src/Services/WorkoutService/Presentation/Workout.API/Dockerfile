#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/WorkoutService/Presentation/Workout.API/Workout.API.csproj", "src/Services/WorkoutService/Presentation/Workout.API/"]
COPY ["src/Services/WorkoutService/Infrastructure/Workout.Infrastructure/Workout.Infrastructure.csproj", "src/Services/WorkoutService/Infrastructure/Workout.Infrastructure/"]
COPY ["src/Services/WorkoutService/Core/Workout.Application/Workout.Application.csproj", "src/Services/WorkoutService/Core/Workout.Application/"]
COPY ["src/Services/WorkoutService/Core/Workout.Domain/Workout.Domain.csproj", "src/Services/WorkoutService/Core/Workout.Domain/"]
COPY ["src/Services/WorkoutService/Infrastructure/Workout.Persistance/Workout.Persistance.csproj", "src/Services/WorkoutService/Infrastructure/Workout.Persistance/"]
COPY ["src/BuildingBlocks/Common.Caching/Common.Caching/Common.Caching.csproj", "src/BuildingBlocks/Common.Caching/Common.Caching/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/Common.Messaging.MassTransit.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.MassTransit/"]
COPY ["src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/Common.Messaging.RabbitMQ.csproj", "src/BuildingBlocks/Common.Messaging/Common.Messaging.RabbitMQ/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
RUN dotnet restore "./src/Services/WorkoutService/Presentation/Workout.API/./Workout.API.csproj"
COPY . .
WORKDIR "/src/src/Services/WorkoutService/Presentation/Workout.API"
RUN dotnet build "./Workout.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Workout.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workout.API.dll"]