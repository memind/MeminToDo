#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/WorkoutService/Presentation/Workout.API/Workout.API.csproj", "src/Services/WorkoutService/Presentation/Workout.API/"]
COPY ["src/Services/WorkoutService/Infrastructure/Workout.Infrastructure/Workout.Infrastructure.csproj", "src/Services/WorkoutService/Infrastructure/Workout.Infrastructure/"]
COPY ["src/Services/WorkoutService/Core/Workout.Application/Workout.Application.csproj", "src/Services/WorkoutService/Core/Workout.Application/"]
COPY ["src/Services/WorkoutService/Core/Workout.Domain/Workout.Domain.csproj", "src/Services/WorkoutService/Core/Workout.Domain/"]
COPY ["src/Services/WorkoutService/Infrastructure/Workout.Persistance/Workout.Persistance.csproj", "src/Services/WorkoutService/Infrastructure/Workout.Persistance/"]
RUN dotnet restore "src/Services/WorkoutService/Presentation/Workout.API/Workout.API.csproj"
COPY . .
WORKDIR "/src/src/Services/WorkoutService/Presentation/Workout.API"
RUN dotnet build "Workout.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workout.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workout.API.dll"]