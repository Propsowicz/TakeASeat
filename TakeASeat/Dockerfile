FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TakeASeat/TakeASeat.csproj" --disable-parallel
RUN dotnet publish "TakeASeat.csproj" -c Release -o /app --no-restore 


FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "TakeASeat.dll"]