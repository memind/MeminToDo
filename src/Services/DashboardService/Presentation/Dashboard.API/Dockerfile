#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/DashboardService/Presentation/Dashboard.API/Dashboard.API.csproj", "src/Services/DashboardService/Presentation/Dashboard.API/"]
COPY ["src/Services/DashboardService/Infrastructure/Dashboard.Infrastructure/Dashboard.Infrastructure.csproj", "src/Services/DashboardService/Infrastructure/Dashboard.Infrastructure/"]
COPY ["src/Services/DashboardService/Core/Dashboard.Application/Dashboard.Application.csproj", "src/Services/DashboardService/Core/Dashboard.Application/"]
COPY ["src/Services/DashboardService/Core/Dashboard.Domain/Dashboard.Domain.csproj", "src/Services/DashboardService/Core/Dashboard.Domain/"]
COPY ["src/Services/DashboardService/Infrastructure/Dashboard.Persistance/Dashboard.Persistance.csproj", "src/Services/DashboardService/Infrastructure/Dashboard.Persistance/"]
RUN dotnet restore "src/Services/DashboardService/Presentation/Dashboard.API/Dashboard.API.csproj"
COPY . .
WORKDIR "/src/src/Services/DashboardService/Presentation/Dashboard.API"
RUN dotnet build "Dashboard.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dashboard.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dashboard.API.dll"]