# # Use the official .NET SDK image to build and publish the app
# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app

# # Copy the published output from the build stage
# COPY . .

# # Expose the port your app runs on
# EXPOSE 5000
# EXPOSE 5001

# # Set the entry point to run the app
# CMD ["dotnet", "MeditationApp.dll"]

# Use .NET 9 SDK to build the app
# FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# WORKDIR /app
# COPY . .
# RUN dotnet publish -c Release -o out

# # Use .NET 9 ASP.NET runtime to run the app
# FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
# WORKDIR /app
# COPY --from=build /app/out .
# CMD ["dotnet", "MeditationApp.dll"]

# Use the full .NET SDK for development
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore

# Expose ports
EXPOSE 5000
EXPOSE 5001

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Development

# Run the application in watch mode
CMD ["dotnet", "watch", "run"]

