# Northwind Orders API
1. **Clone the repository**:
    ```bash
    git clone https://github.com/kallinnas/NorthwindOrdersAPI.git
    ```
2. **Navigate to the project directory**:
    ```bash
    cd NorthwindOrdersAPI
    ```
3. **Set up the Dockerized MSSQL database**:
    ```bash
    docker pull mcr.microsoft.com/mssql/server:2022-latest
    docker run -d -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Qwerty1!" -p 1433:1433 -d --name mssql_server mcr.microsoft.com/mssql/server:2022-latest
    ```
4. **Configure the connection string** in `appsettings.json` with the appropriate database connection details.

5. **Run the project**:
    - Open the solution in Visual Studio
    - Restore NuGet packages
    - Build and run the project

## Assumptions, Notes, and Design Decisions
- Used MSSQL in Docker for database management.
- Stored procedures with ADO.NET for data queries.
- Proper error handling and logging implemented.


Frontend (Angular): https://github.com/kallinnas/NorthwindOrdersAPI
Backend (C#): https://github.com/kallinnas/northwind-orders-ui

Completed Features:

    Order List Page
    Create New Order Page
    Edit Order Page
    View Order Details Page
    Sorting and Filtring Fields
    Dark Mode Theme
    Custom Autocomplete
    Custom Paginator
    Error Handler

Useful links:

    https://stackoverflow.com/questions/54057030/how-to-change-itemsperpagelabel-in-mat-paginator-in-angular-6
    https://stackoverflow.com/questions/46433953/how-to-cancel-current-request-in-interceptor-angular-4
    https://stackoverflow.com/questions/56393880/how-do-i-detect-dark-mode-using-javascript
    https://stackoverflow.com/questions/68512168/tableheader-of-material-ui-does-not-make-the-content-bold
    https://stackoverflow.com/questions/17615260/the-certificate-chain-was-issued-by-an-authority-that-is-not-trusted-when-conn/70850834#70850834
    https://stackoverflow.com/questions/37172928/how-to-proxy-api-requests-to-another-server/71764796#71764796
    
