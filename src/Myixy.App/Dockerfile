FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Myixy.App/Myixy.App.csproj", "Myixy.App/"]
RUN dotnet restore "Myixy.App/Myixy.App.csproj"
COPY . .
WORKDIR "/src/Myixy.App"
RUN dotnet build "Myixy.App.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Myixy.App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Myixy.App.dll"]