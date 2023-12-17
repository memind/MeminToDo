#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/BudgetService/Presentation/Budget.API/Budget.API.csproj", "src/Services/BudgetService/Presentation/Budget.API/"]
COPY ["src/Services/BudgetService/Infrastructure/Budget.Infrastructure/Budget.Infrastructure.csproj", "src/Services/BudgetService/Infrastructure/Budget.Infrastructure/"]
COPY ["src/Services/BudgetService/Core/Budget.Application/Budget.Application.csproj", "src/Services/BudgetService/Core/Budget.Application/"]
COPY ["src/Services/BudgetService/Core/Budget.Domain/Budget.Domain.csproj", "src/Services/BudgetService/Core/Budget.Domain/"]
COPY ["src/Services/BudgetService/Infrastructure/Budget.Persistance/Budget.Persistance.csproj", "src/Services/BudgetService/Infrastructure/Budget.Persistance/"]
COPY ["src/BuildingBlocks/Common.Caching/Common.Caching/Common.Caching.csproj", "src/BuildingBlocks/Common.Caching/Common.Caching/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
RUN dotnet restore "src/Services/BudgetService/Presentation/Budget.API/Budget.API.csproj"
COPY . .
WORKDIR "/src/src/Services/BudgetService/Presentation/Budget.API"
RUN dotnet build "Budget.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Budget.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Budget.API.dll"]