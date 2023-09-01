#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/LogService/Presentation/Log.API/Log.API.csproj", "src/Services/LogService/Presentation/Log.API/"]
COPY ["src/Services/LogService/Infrastructure/Log.Infrastructure/Log.Infrastructure.csproj", "src/Services/LogService/Infrastructure/Log.Infrastructure/"]
COPY ["src/Services/LogService/Core/Log.Application/Log.Application.csproj", "src/Services/LogService/Core/Log.Application/"]
COPY ["src/Services/LogService/Core/Log.Domain/Log.Domain.csproj", "src/Services/LogService/Core/Log.Domain/"]
COPY ["src/Services/LogService/Infrastructure/Log.Persistance/Log.Persistance.csproj", "src/Services/LogService/Infrastructure/Log.Persistance/"]
RUN dotnet restore "src/Services/LogService/Presentation/Log.API/Log.API.csproj"
COPY . .
WORKDIR "/src/src/Services/LogService/Presentation/Log.API"
RUN dotnet build "Log.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Log.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Log.API.dll"]