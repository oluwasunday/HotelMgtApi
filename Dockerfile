#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HotelMgt.API/HotelMgt.API.csproj", "HotelMgt.API/"]
COPY ["HotelMgt.Utilities/HotelMgt.Utilities.csproj", "HotelMgt.Utilities/"]
COPY ["HotelMgt.Dtos/HotelMgt.Dtos.csproj", "HotelMgt.Dtos/"]
COPY ["HotelMgt.Models/HotelMgt.Models.csproj", "HotelMgt.Models/"]
COPY ["HotelMgt.Core/HotelMgt.Core.csproj", "HotelMgt.Core/"]
COPY ["HotelMgt.Data/HotelMgt.Data.csproj", "HotelMgt.Data/"]
RUN dotnet restore "HotelMgt.API/HotelMgt.API.csproj"
COPY . .
WORKDIR "/src/HotelMgt.API"
RUN dotnet build "HotelMgt.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelMgt.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "HotelMgt.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HotelMgt.API.dll