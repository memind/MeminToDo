#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/LogService/Log.API/Log.API.csproj", "src/Services/LogService/Log.API/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
RUN dotnet restore "src/Services/LogService/Log.API/Log.API.csproj"
COPY . .
WORKDIR "/src/src/Services/LogService/Log.API"
RUN dotnet build "Log.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Log.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Log.API.dll"]