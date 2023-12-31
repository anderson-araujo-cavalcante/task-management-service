#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["src/TaskManagement.API/TaskManagement.API.csproj", "src/TaskManagement.API/"]
COPY ["src/TaskManagement.Domain/TaskManagement.Domain.csproj", "src/TaskManagement.Domain/"]
COPY ["src/TaskManagement.Data/TaskManagement.Data.csproj", "src/TaskManagement.Data/"]
RUN dotnet restore "./src/TaskManagement.API/./TaskManagement.API.csproj"

COPY . .
WORKDIR "/src/src/TaskManagement.API"
RUN dotnet build "./TaskManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./TaskManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManagement.API.dll"]