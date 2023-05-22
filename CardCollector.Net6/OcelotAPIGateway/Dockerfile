#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5184

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CardCollector.Net6/OcelotAPIGateway/OcelotAPIGateway.csproj", "CardCollector.Net6/OcelotAPIGateway/"]
RUN dotnet restore "CardCollector.Net6/OcelotAPIGateway/OcelotAPIGateway.csproj"
COPY . .
WORKDIR "/src/CardCollector.Net6/OcelotAPIGateway"
RUN dotnet build "OcelotAPIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotAPIGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5184
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotAPIGateway.dll"]
