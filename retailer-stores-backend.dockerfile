FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RetailerStores.WebApi/RetailerStores.WebApi.csproj", "./RetailerStores.WebApi/"]
COPY ["RetailerStores.Application/RetailerStores.Application.csproj", "./RetailerStores.Application/"]
COPY ["RetailerStores.Domain/RetailerStores.Domain.csproj", "./RetailerStores.Domain/"]
RUN dotnet restore "./RetailerStores.WebApi/RetailerStores.WebApi.csproj"
COPY . .
RUN ["mv", "./RetailerStores.WebApi/appsettings.docker.json", "./RetailerStores.WebApi/appsettings.json"]
WORKDIR "/src/RetailerStores.WebApi/."
RUN dotnet build "RetailerStores.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RetailerStores.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["RetailerStores.WebApi/Site Data.xlsx", "../"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetailerStores.WebApi.dll"]