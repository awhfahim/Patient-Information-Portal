# Patient-Information-Portal

This project follows the Clean Architecture principles, utilizing a Web API for the backend and ASP.NET Core MVC for the frontend. 
It also heavily employs the Unit of Work and Repository patterns to manage data access and ensure separation of concerns.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET SDK 8 

- You need to create a [.env] file in the root of PatientPortal.Api project 

## Setup Instructions

### Step 1: Clone the Repository

Clone this repository to your local machine using the following command:

```bash
git clone https://github.com/awhfahim/Patient-Information-Portal
```

### Step 2: Create and Configure the .env File

Create a .env file at the root of PatientPortal.Api Project with the following content:

To configure the Serilog Email Sink, set the following environment variables:

```ini
SERILOGEMAILSINKOPTIONS__EMAILFROM="f@gmail.com"
SERILOGEMAILSINKOPTIONS__EMAILTO="c@gmail.com"
SERILOGEMAILSINKOPTIONS__EMAILSUBJECT="Patient Portal Error"
SERILOGEMAILSINKOPTIONS__SMTPHOST="sandbox.smtp.mailtrap.io"
SERILOGEMAILSINKOPTIONS__SMTPPORT=""
SERILOGEMAILSINKOPTIONS__SMTPUSERNAME=""
SERILOGEMAILSINKOPTIONS__SMTPPASSWORD=""
SERILOGEMAILSINKOPTIONS__MINIMUMLOGLEVEL="Error"
```
To configure the database connection string complete this connection string with actual values
```int
# Connection Strings
CONNECTIONSTRINGS__PATIENTPORTALDB="Server=.\\SQLEXPRESS;Database=;User Id=;Password=;Trust Server Certificate=True;"
```

Step 3: Run the migration to create the necessary database schema:

1. Set the correct database connection string in the `.env` file.

    - For `.env` file:

        ```
        ConnectionStrings__DefaultConnection=YourDatabaseConnectionString
        ```

    - For `appsettings.json`:

        ```json
        {
          "ConnectionStrings": {
            "PatientPortalDb": "YourDatabaseConnectionString"
          }
        }
        ```

2. Run this migration command to create the necessary database schema:

    ```bash
    dotnet ef database update --project PatientPortal.Api
    ```

### Step 3: Running the Projects

- First Start the Web API Project
- Then Start the MVC Project

### Step 4: Accessing the Application

- Open your browser and navigate to the MVC application, typically running on `https://localhost:7229/`.
- Ensure the Web API project is running, usually accessible Swagger page at `https://localhost:7236/swagger/index.html`.

## Project Structure

The project is structured according to the Clean Architecture principles:

- `PatientPortal.Domain`: Contains the domain entities and interfaces.
- `PatientPortal.Application`: Contains the business logic and application services.
- `PatientPortal.Infrastructure`: Contains data access implementations and other infrastructure concerns.
- `PatientPortal.API`: The Web API project.
- `PatientPortal.MVC`: The ASP.NET Core MVC frontend project.
