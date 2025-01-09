FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ./frontend/frontend.csproj ./frontend/
RUN dotnet restore ./frontend/frontend.csproj

COPY . .
WORKDIR /src/frontend
RUN dotnet publish ./frontend.csproj -c Release -o /app/publish


FROM nginx:alpine


COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
