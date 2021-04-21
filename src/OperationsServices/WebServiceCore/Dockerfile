#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebServiceCore/WebServiceCore.csproj", "WebServiceCore/"]
RUN dotnet restore "WebServiceCore/WebServiceCore.csproj"
COPY . .
WORKDIR "/src/WebServiceCore"
RUN dotnet build "WebServiceCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebServiceCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServiceCore.dll"]