Users Management Application
Overview
A full-stack CRUD application to manage users, built with ASP.NET Core Web API for the backend and React for the frontend.

Technologies
Backend: ASP.NET Core Web API, Sql Server
Frontend: React, TypeScript

Backend Setup
Clone the Repository:

git clone https://github.com/albysam/UsersManagementAPI.git
cd UsersManagementAPI

Restore NuGet Packages
dotnet restore

Check Configuration Settings
appsettings.json: Review appsettings.json or appsettings.Development.json for any configuration that might need adjustments, like database connection strings.

Build the Project:
dotnet build
dotnet run

Create a new SQL Server database named UserManagementDB.
# SQL Server Configuration
SQLSERVER_CONNECTION_STRING=Server=localhost;Database=UserManagementDB;User Id=your_username;Password=your_password;

Apply Migrations

Start the Backend Server:

The backend server will be running at http://localhost:5000.
npm install
npm run start
The server will be running at http://localhost:3000.

Frontend Setup
Clone the Repository:

git clone https://github.com/albysam/usersmanagementFrontend.git
cd usersmanagementFrontend
Install Dependencies

Ensure the API URL in your api.ts matches your backend server URL:

Copy code
// src/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3000', // Backend API URL
});

export default api;
Start the Frontend:

The frontend will be running at http://localhost:3000.

API Endpoints
Create User: POST /users
Get All Users: GET /users
Get User by ID: GET /users/:id
Update User: PUT /users/:id
Delete User: DELETE /users/:id

Contact For questions, contact me at albypsam001@gmail.com.
