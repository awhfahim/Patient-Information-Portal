# Patient-Information-Portal

This project follows the Clean Architecture principles, utilizing a Web API for the backend and ASP.NET Core MVC for the frontend. 
It also heavily employs the Unit of Work and Repository patterns to manage data access and ensure separation of concerns.

## Prerequisites

Before you begin, ensure you have met the following requirements:

.NET SDK 8 

You need to create a [.env] file in the root of PatientPortal.Api project 

## Setup Instructions

### Step 1: Clone the Repository

Clone this repository to your local machine using the following command:

```bash
git clone https://github.com/awhfahim/Patient-Information-Portal
```

### Step 2: Create and Configure the .env File

Create a .env file with the following content:

To configure the Serilog Email Sink, set the following environment variables:

```ini
SERILOGEMAILSINKOPTIONS__EMAILFROM="f@gmail.com"
SERILOGEMAILSINKOPTIONS__EMAILTO="c@gmail.com"
SERILOGEMAILSINKOPTIONS__EMAILSUBJECT="Patient Portal Error"
SERILOGEMAILSINKOPTIONS__SMTPHOST="sandbox.smtp.mailtrap.io"
SERILOGEMAILSINKOPTIONS__SMTPPORT="587"
SERILOGEMAILSINKOPTIONS__SMTPUSERNAME="2e41d5385f620c"
SERILOGEMAILSINKOPTIONS__SMTPPASSWORD="2b6d27adf1826a"
SERILOGEMAILSINKOPTIONS__MINIMUMLOGLEVEL="Error"
```
To configure the database connection string
```int
# Connection Strings
CONNECTIONSTRINGS__PATIENTPORTALDB="Server=.\\SQLEXPRESS;Database=PatientPortal;User Id=fahim;Password=123456;Trust Server Certificate=True;"
```

Step 3: Database Migration
Run the migration to create the necessary database schema:

1. Set the correct database connection string in the `.env` file.

    - For `.env` file:

        ```
        ConnectionStrings__DefaultConnection=YourDatabaseConnectionString
        ```

    - For `appsettings.json`:

        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "YourDatabaseConnectionString"
          }
        }
        ```

2. Run the migration to create the necessary database schema:

    ```bash
    dotnet ef database update --project PatientPortal.Api
    ```

### Step 3: Running the Projects

#### 1. Start the Web API Project

1. Navigate to the API project directory:

    ```bash
    cd path/to/your-repository/YourApiProject
    ```

2. Run the Web API project:

    ```bash
    dotnet run
    ```

#### 2. Start the MVC Project

1. Open a new terminal window and navigate to the MVC project directory:

    ```bash
    cd path/to/your-repository/YourMvcProject
    ```

2. Run the MVC project:

    ```bash
    dotnet run
    ```

### Step 4: Accessing the Application

- Open your browser and navigate to the MVC application, typically running on `http://localhost:5000`.
- Ensure the Web API project is running, usually accessible at `http://localhost:5001`.

## Project Structure

The project is structured according to the Clean Architecture principles:

- `Core`: Contains the domain entities and interfaces.
- `Application`: Contains the business logic and application services.
- `Infrastructure`: Contains data access implementations and other infrastructure concerns.
- `API`: The Web API project.
- `MVC`: The ASP.NET Core MVC frontend project.

## Additional Information

- For more details on the Clean Architecture, refer to [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
- If you encounter any issues, please [create an issue](https://github.com/your-username/your-repository/issues) in the repository.

## License

This project is licensed under the [MIT License](LICENSE).

---

Feel free to customize the placeholders (`your-username`, `your-repository`, `YourApiProject`, `YourMvcProject`, etc.) with your actual project details. This README provides a clear, step-by-step guide for setting up and running the project, ensuring users can get started easily.
