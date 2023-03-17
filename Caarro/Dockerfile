FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Caarro/Caarro.csproj", "Caarro/"]
RUN dotnet restore "Caarro/Caarro.csproj"
COPY . .
WORKDIR "/src/Caarro"
RUN dotnet build "Caarro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Caarro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Caarro.dll"]
