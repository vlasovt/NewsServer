FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["NewsServer/NewsServer.csproj", "NewsServer/"]
RUN dotnet restore "NewsServer/NewsServer.csproj"
COPY . .
WORKDIR "/src/NewsServer"
RUN dotnet build "NewsServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NewsServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NewsServer.dll"]