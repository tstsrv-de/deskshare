#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 9977

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeskShareApi.csproj", "."]
RUN dotnet restore "./DeskShareApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DeskShareApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeskShareApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY wait-for-it.sh /app/wait-for-it.sh
RUN chmod +x wait-for-it.sh




