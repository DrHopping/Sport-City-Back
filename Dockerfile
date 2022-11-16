FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SportCity.Web/SportCity.Web.csproj", "SportCity.Web/"]
COPY ["src/SportCity.Infrastructure/SportCity.Infrastructure.csproj", "SportCity.Infrastructure/"]
COPY ["src/SportCity.Core/SportCity.Core.csproj", "SportCity.Core/"]
COPY ["src/SportCity.SharedKernel/SportCity.SharedKernel.csproj", "SportCity.SharedKernel/"]
RUN dotnet restore "src/SportCity.Web/SportCity.Web.csproj"
COPY . .
WORKDIR "/src/SportCity.Web"
RUN dotnet build "SportCity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportCity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportCity.Web.dll"]
