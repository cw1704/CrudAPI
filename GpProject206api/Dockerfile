#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GpProject206api/GpProject206api.csproj", "GpProject206api/"]
COPY ["GpProject206/GpProject206.csproj", "GpProject206/"]
RUN dotnet restore "GpProject206api/GpProject206api.csproj"
COPY . .
WORKDIR "/src/GpProject206api
RUN dotnet build "GpProject206api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GpProject206api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GpProject206api.dll"]