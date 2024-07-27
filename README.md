# Northwind Orders API

## Repositories
    - **Frontend (Angular)**: [Northwind Orders UI](https://github.com/kallinnas/northwind-orders-ui)
    - **Backend (C#)**: [Northwind Orders API](https://github.com/kallinnas/NorthwindOrdersAPI)

## Completed Features
    - Order List Page
    - Create New Order Page
    - Edit Order Page
    - View Order Details Page
    - Sorting and Filtering Fields
    - Dark Mode Theme
    - Custom Autocomplete
    - Custom Paginator
    - Error Handler

## Project Setup Instructions

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

  ## Useful Links

- [How to change itemsPerPageLabel in mat-paginator in Angular](https://stackoverflow.com/questions/54057030/how-to-change-itemsperpagelabel-in-mat-paginator-in-angular-6)
- [How to cancel current request in interceptor Angular](https://stackoverflow.com/questions/46433953/how-to-cancel-current-request-in-interceptor-angular-4)
- [How do I detect dark mode using JavaScript](https://stackoverflow.com/questions/56393880/how-do-i-detect-dark-mode-using-javascript)
- [Table header of Material-UI does not make the content bold](https://stackoverflow.com/questions/68512168/tableheader-of-material-ui-does-not-make-the-content-bold)
- [The certificate chain was issued by an authority that is not trusted](https://stackoverflow.com/questions/17615260/the-certificate-chain-was-issued-by-an-authority-that-is-not-trusted-when-conn/70850834#70850834)
- [How to proxy API requests to another server](https://stackoverflow.com/questions/37172928/how-to-proxy-api-requests-to-another-server/71764796#71764796)

