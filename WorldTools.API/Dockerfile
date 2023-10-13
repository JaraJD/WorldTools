#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorldTools.API/WorldTools.API.csproj", "WorldTools.API/"]
COPY ["WorldTools.Application/WorldTools.Application.csproj", "WorldTools.Application/"]
COPY ["WorldTools.Domain/WorldTools.Domain.csproj", "WorldTools.Domain/"]
COPY ["WorldTools.Infrastructure/WorldTools.SqlAdapter.csproj", "WorldTools.Infrastructure/"]
COPY ["WorldTools.Application.Queries/WorldTools.Application.Queries.csproj", "WorldTools.Application.Queries/"]
COPY ["WorldTools.MongoAdapter/WorldTools.MongoAdapter.csproj", "WorldTools.MongoAdapter/"]
COPY ["WorldTools.Rabbit.PublishAdapter/WorldTools.Rabbit.PublishAdapter.csproj", "WorldTools.Rabbit.PublishAdapter/"]
COPY ["WorldTools.WebSocketAdapter/WorldTools.WebSocketAdapter.csproj", "WorldTools.WebSocketAdapter/"]
RUN dotnet restore "WorldTools.API/WorldTools.API.csproj"
COPY . .
WORKDIR "/src/WorldTools.API"
RUN dotnet build "WorldTools.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorldTools.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldTools.API.dll"]