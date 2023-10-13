#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorldTools.API.Queries/WorldTools.API.Queries.csproj", "WorldTools.API.Queries/"]
COPY ["WorldTools.Application.Queries/WorldTools.Application.Queries.csproj", "WorldTools.Application.Queries/"]
COPY ["WorldTools.Domain/WorldTools.Domain.csproj", "WorldTools.Domain/"]
COPY ["WorldTools.Infrastructure/WorldTools.SqlAdapter.csproj", "WorldTools.Infrastructure/"]
COPY ["WorldTools.Application/WorldTools.Application.csproj", "WorldTools.Application/"]
COPY ["WorldTools.Rabbit.SubscribeAdapter/WorldTools.Rabbit.SubscribeAdapter.csproj", "WorldTools.Rabbit.SubscribeAdapter/"]
RUN dotnet restore "WorldTools.API.Queries/WorldTools.API.Queries.csproj"
COPY . .
WORKDIR "/src/WorldTools.API.Queries"
RUN dotnet build "WorldTools.API.Queries.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorldTools.API.Queries.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldTools.API.Queries.dll"]