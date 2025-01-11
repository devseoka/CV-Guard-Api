# First stage: build the application using the .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire application source code into the container
COPY . ./

# Build the app in Release mode and publish it to the 'out' folder
RUN dotnet publish -c Release -o out

# Second stage: create the runtime image using the .NET 8 ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory inside the container
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 80

# Define the entrypoint to run the application
ENTRYPOINT ["dotnet", "Cv.Guard.Api.dll"]
