# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

# Use the official ASP.NET Core SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj and restore dependencies
COPY ["Lolgraphics.API/Lolgraphics.API.csproj", "Lolgraphics.API/"]
COPY ["Lolgraphics.BFF/Lolgraphics.BFF.csproj", "Lolgraphics.BFF/"]
COPY ["Lolgraphics.Core/Lolgraphics.Core.csproj", "Lolgraphics.Core/"]
COPY ["Lolgraphics.Infrastructure/Lolgraphics.Infrastructure.csproj", "Lolgraphics.Infrastructure/"]
COPY ["Lolgraphics.Tests/Lolgraphics.Tests.csproj", "Lolgraphics.Tests/"]

RUN dotnet restore "Lolgraphics.API/Lolgraphics.API.csproj"

# Copy the rest of the application code
COPY . .

# Build and publish the API project
WORKDIR "/src/Lolgraphics.API"
RUN dotnet publish -c Release -o /app/publish

# Copy the build output to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lolgraphics.API.dll"]
