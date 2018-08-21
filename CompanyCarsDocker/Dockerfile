FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 55796
EXPOSE 44374

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CompanyCarsDocker/CompanyCarsDocker.csproj", "CompanyCarsDocker/"]
RUN dotnet restore "CompanyCarsDocker/CompanyCarsDocker.csproj"
COPY . .
WORKDIR "/src/CompanyCarsDocker"
RUN dotnet build "CompanyCarsDocker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CompanyCarsDocker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CompanyCarsDocker.dll"]