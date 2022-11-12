#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["project206pizza/project206pizza.csproj", "project206pizza/"]
RUN dotnet restore "project206pizza/project206pizza.csproj"
COPY . .
WORKDIR "/src/project206pizza"
RUN dotnet build "project206pizza.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "project206pizza.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "project206pizza.dll"]