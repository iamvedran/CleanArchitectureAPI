# Clean Architecture API

This is an ASP.NET Core 7 application built following the principles of SOLID and Clean Architecture (Onion Architecture).
## Features

- **SOLID Principles**: The application adheres to SOLID principles, which promote modular, flexible, and testable code.
- **Clean Architecture**: The project follows the Clean Architecture pattern, emphasizing separation of concerns and a layered architectural approach.
- **CQRS and Mediator Pattern**: The application implements the Command Query Responsibility Segregation (CQRS) pattern with the help of Mediator, allowing a clear separation between read and write operations.
- **Advanced libraries**: The project utilizes advanced libraries such as MediatR for handling commands and queries, AutoMapper for object mapping, FluentValidation for validation of input data, Serilog for structured logging capabilities, Entity Framework for data access and database operations and SendGrid is integrated into the application for sending emails and notifications.
- **Custom Middleware**: Custom middleware is implemented to handle specific application needs, enhancing the request/response pipeline.
- **Swagger API Documentation**: Swagger is integrated into the project for generating API documentation, making it easier to explore and understand the available endpoints.
- **JWT Security**: The application uses JWT (JSON Web Tokens) to secure the API and handle user authentication and authorization.
- **Custom Exceptions and Global Error Handling**: Custom exceptions are implemented to handle specific error scenarios, and global error handling is applied to ensure consistent and informative error responses.
- **Unit Testing with xUnit**: The project includes unit tests written using xUnit framework to ensure the correctness of individual components and functionalities.
- **Integration Testing**: Integration tests are included to verify the proper integration between various components of the application.

## Getting Started

To get started with the ASP.NET Core - SOLID and Clean Architecture project, follow these steps:

1. Clone the repository: `git clone https://github.com/iamvedran/CleanArchitectureAPI`
2. Navigate to the project directory: `cd CleanArchitectureAPI`
3. Restore the NuGet packages: `dotnet restore`
4. Build the solution: `dotnet build`
5. Run the application: `dotnet run`

Make sure you have the necessary dependencies installed and configured.

## Configuration

The application uses various configuration settings that can be modified in the `appsettings.json` file. Update the configuration values according to your needs before running the application.

## Entity Framework Core

The application utilizes Entity Framework Core for data access and database operations. The database connection and configuration can be found in the `appsettings.json` file. Make sure to update the connection string with the appropriate values for your environment.

To apply the database migrations and create the database, run the following command:

```bash
dotnet ef database update
```
## SendGrid Integration

SendGrid is integrated into the application for sending emails and notifications. To use SendGrid, make sure you have a SendGrid account and obtain an API key. Update the SendGrid configuration in the `appsettings.json` file with your API key and other required information.

## Testing

The project includes unit tests and integration tests. You can run the tests using the following command:

```bash
dotnet test
```

Ensure that the application is built before running the tests.

