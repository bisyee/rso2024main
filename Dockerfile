# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY ./backend/ParkingService/*.csproj ./backend/ParkingService/
COPY ./SharedModels/SharedModels.csproj ./SharedModels/

COPY ./rso-project2024.sln ./rso-project2024.sln

RUN dotnet restore ./rso-project2024.sln

COPY ./backend/ParkingService/. ./backend/ParkingService/
COPY ./SharedModels/. ./SharedModels/

RUN dotnet publish ./backend/ParkingService/ParkingService.csproj -c Release -o /out


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base


WORKDIR /app

EXPOSE 8080

COPY --from=build /out .


ENTRYPOINT ["dotnet", "ParkingService.dll"]