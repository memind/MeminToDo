#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/MealService/API/Meal.API/Meal.API.csproj", "src/Services/MealService/API/Meal.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
COPY ["src/Services/MealService/Application/Meal.Application/Meal.Application.csproj", "src/Services/MealService/Application/Meal.Application/"]
COPY ["src/Services/MealService/AutoMapper/Meal.Mapper/Meal.Mapper.csproj", "src/Services/MealService/AutoMapper/Meal.Mapper/"]
COPY ["src/Services/MealService/Infrastructure/Meal.Infrastructure/Meal.Infrastructure.csproj", "src/Services/MealService/Infrastructure/Meal.Infrastructure/"]
COPY ["src/Services/MealService/Domain/Meal.Domain/Meal.Domain.csproj", "src/Services/MealService/Domain/Meal.Domain/"]
RUN dotnet restore "src/Services/MealService/API/Meal.API/Meal.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MealService/API/Meal.API"
RUN dotnet build "Meal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meal.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meal.API.dll"]