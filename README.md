# **Employee Manager**

### **Key Technologies**
- **Back-end:** ASP.NET Core Web API
- **Data Access:** Entity Framework Core (EF Core)
- **Database:** Microsoft SQL Server (MSSQL)
- **Authentication & Security:** JWT-based authentication and authorization
- **Architectural Pattern:** Vertical Slicing with CQRS (Command Query Responsibility Segregation)
- **Logging:** Centralized logging using Serilog and Seq
- **API Versioning:** Support for multiple API versions
- **Containerization:** Docker support for easy deployment and scalability

## **Getting Started**

### **Prerequisites**
Before getting started, ensure you have the following installed:
- [Docker](https://www.docker.com/get-started)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server)

### **Installation**
1. **Clone the repository**  
   First, clone the repository to your local machine:
   ```bash
   git clone https://github.com/iftifar-taz/EmployeeTracker.git
   cd EmployeeManager
   ```

2. **Build and run with Docker Compose**
   Ensure Docker is running on your machine, then build and run the application using Docker Compose:
   ```bash
   docker-compose up
   ```

3. **Database Setup**
   The project will automatically apply database migrations on startup to set up the required schema.
   
5. **Access the API**
   Once the container is running, the API will be accessible on the following URLs:
   - **Identity**: http://localhost:9090
   - **EmployeeManager**: http://localhost:9080
  
6. **API Documentation**
   The API is documented using Swagger. You can access the interactive API documentation at /swagger on the running application. This will allow you to explore and test the API endpoints.
