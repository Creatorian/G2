# Multi-stage build for smaller production image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gnome.Api/Gnome.Api.csproj", "Gnome.Api/"]
COPY ["Gnome.Application/Gnome.Application.csproj", "Gnome.Application/"]
COPY ["Gnome.Domain/Gnome.Domain.csproj", "Gnome.Domain/"]
COPY ["Gnome.Infrastructure/Gnome.Infrastructure.csproj", "Gnome.Infrastructure/"]
RUN dotnet restore "Gnome.Api/Gnome.Api.csproj"
COPY . .
WORKDIR "/src/Gnome.Api"
RUN dotnet build "Gnome.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gnome.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gnome.Api.dll"]