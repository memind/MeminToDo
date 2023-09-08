#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/SkillService/Presentation/Skill.API/Skill.API.csproj", "src/Services/SkillService/Presentation/Skill.API/"]
COPY ["src/Services/SkillService/Core/Skill.Application/Skill.Application.csproj", "src/Services/SkillService/Core/Skill.Application/"]
COPY ["src/Services/SkillService/Core/Skill.Domain/Skill.Domain.csproj", "src/Services/SkillService/Core/Skill.Domain/"]
COPY ["src/Services/SkillService/Infrastructure/Skill.Infrastructure/Skill.Infrastructure.csproj", "src/Services/SkillService/Infrastructure/Skill.Infrastructure/"]
COPY ["src/Services/SkillService/Infrastructure/Skill.Persistance/Skill.Persistance.csproj", "src/Services/SkillService/Infrastructure/Skill.Persistance/"]
COPY ["src/BuildingBlocks/Common.Logging/Common.Logging/Common.Logging.csproj", "src/BuildingBlocks/Common.Logging/Common.Logging/"]
RUN dotnet restore "src/Services/SkillService/Presentation/Skill.API/Skill.API.csproj"
COPY . .
WORKDIR "/src/src/Services/SkillService/Presentation/Skill.API"
RUN dotnet build "Skill.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Skill.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Skill.API.dll"]