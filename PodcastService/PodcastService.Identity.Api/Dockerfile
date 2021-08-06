#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PodcastService.Api/PodcastService.Identity.Api.csproj", "PodcastService.Api/"]
RUN dotnet restore "PodcastService.Api/PodcastService.Identity.Api.csproj"
COPY . .
WORKDIR "/src/PodcastService.Api"
RUN dotnet build "PodcastService.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PodcastService.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PodcastService.Identity.Api.dll"]