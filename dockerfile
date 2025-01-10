# Use the .NET 8 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and all project files to restore dependencies
COPY *.sln ./
COPY src/EmployeeTracker.Web/*.csproj ./src/EmployeeTracker.Web/
COPY src/EmployeeTracker.Infrastructure/*.csproj ./src/EmployeeTracker.Infrastructure/
COPY src/EmployeeTracker.Core/*.csproj ./src/EmployeeTracker.Core/
COPY src/EmployeeTracker.Application/*.csproj ./src/EmployeeTracker.Application/
RUN dotnet restore ./src/EmployeeTracker.Web/EmployeeTracker.Web.csproj

# Copy the entire source code and build the application
COPY . ./
WORKDIR /app/src/EmployeeTracker.Web
RUN dotnet publish -c Release -o /out

# Use a lightweight runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory inside the container
WORKDIR /app

# Set the environment variable
ENV DOTNET_ENVIRONMENT=Production

# Copy the published output from the build stage
COPY --from=build /out .

# Expose the application's port
EXPOSE 8080
EXPOSE 8081

# Define the entry point for the container
ENTRYPOINT ["dotnet", "EmployeeTracker.Web.dll"]
