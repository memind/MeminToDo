#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/UserService/Presentation/User.API/User.API.csproj", "src/Services/UserService/Presentation/User.API/"]
COPY ["src/Services/UserService/Infrastructure/User.Infrastructure/User.Infrastructure.csproj", "src/Services/UserService/Infrastructure/User.Infrastructure/"]
COPY ["src/Services/UserService/Core/User.Application/User.Application.csproj", "src/Services/UserService/Core/User.Application/"]
COPY ["src/Services/UserService/Core/User.Domain/User.Domain.csproj", "src/Services/UserService/Core/User.Domain/"]
COPY ["src/Services/UserService/Infrastructure/User.Persistance/User.Persistance.csproj", "src/Services/UserService/Infrastructure/User.Persistance/"]
RUN dotnet restore "src/Services/UserService/Presentation/User.API/User.API.csproj"
COPY . .
WORKDIR "/src/src/Services/UserService/Presentation/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]