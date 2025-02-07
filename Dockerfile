# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy the solution and project files
COPY ./backend/ParkingService/ParkingSpots/*.csproj ./backend/ParkingService/ParkingSpots/
COPY ./SharedModels/SharedModels.csproj ./SharedModels/
COPY ./backend/ParkingService/ParkingService.csproj ./backend/ParkingService/
COPY ./rso-project2024.sln ./rso-project2024.sln

# Restore dependencies
RUN dotnet restore ./rso-project2024.sln

# Copy the source code
COPY ./backend/ParkingService/ParkingSpots/ ./backend/ParkingService/ParkingSpots/
COPY ./SharedModels/ ./SharedModels/

# Build and publish the app
RUN dotnet publish ./backend/ParkingService/ParkingSpots/ParkingSpots.csproj -c Release -o /out

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

# Expose the port
EXPOSE 8083

# Copy the published app from the build stage
COPY --from=build /out .

# Set the entry point
ENTRYPOINT ["dotnet", "ParkingSpots.dll"]
