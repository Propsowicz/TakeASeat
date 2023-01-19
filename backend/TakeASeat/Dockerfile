
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TakeASeat/TakeASeat.csproj", "TakeASeat/"]
RUN dotnet restore "TakeASeat/TakeASeat.csproj"
COPY . .
WORKDIR "/src/TakeASeat"
RUN dotnet build "TakeASeat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TakeASeat.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TakeASeat.dll"]