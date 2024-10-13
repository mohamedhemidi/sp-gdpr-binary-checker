FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["backend-binary-checker.sln", "./"]
COPY ["src/", "./src/"]

RUN dotnet restore "src/App/App.csproj"

RUN dotnet build "src/App/App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/App/App.csproj" -c Release -o /app/publish


FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=5018
EXPOSE 5018
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]
