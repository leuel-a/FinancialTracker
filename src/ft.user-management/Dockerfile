FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ft.user-management.WebApi/ft.user-management.WebApi.csproj", "ft.user-management.WebApi/"]
COPY ["ft.user-management.Application/ft.user-management.Application.csproj", "ft.user-management.Application/"]
COPY ["ft.user-management.Domain/ft.user-management.Domain.csproj", "ft.user-management.Domain/"]
COPY ["ft.user-management.Infrastructure/ft.user-management.Infrastructure.csproj", "ft.user-management.Infrastructure/"]
COPY ["ft.user-management.Persistence/ft.user-management.Persistence.csproj", "ft.user-management.Persistence/"]
RUN dotnet restore "ft.user-management.WebApi/ft.user-management.WebApi.csproj"
COPY . .
WORKDIR "/src/ft.user-management.WebApi"
RUN dotnet build "ft.user-management.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ft.user-management.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ft.user-management.WebApi.dll"]
