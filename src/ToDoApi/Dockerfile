# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the csproj file and restore the dependencies
COPY ToDoApi.csproj ./
RUN dotnet restore

# Copy the rest of the application files
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Set the working directory for the runtime container
WORKDIR /app

# Expose port 80 for the app to listen to
EXPOSE 80

# Copy the published files from the build image
COPY --from=build /app/out .

# Define the entry point to run the app
ENTRYPOINT ["dotnet", "ToDoApi.dll"]


# docker build --platform linux/amd64 -t mrstevebang/todoapp-api:1.0.10 .
# docker push mrstevebang/todoapp-api:latest