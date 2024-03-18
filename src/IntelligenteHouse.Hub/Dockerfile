FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5081

ENV ASPNETCORE_URLS=http://+:5081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["IntelligenteHouse.Hub.csproj", "./"]
RUN dotnet restore "IntelligenteHouse.Hub.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IntelligenteHouse.Hub.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "IntelligenteHouse.Hub.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntelligenteHouse.Hub.dll"]
