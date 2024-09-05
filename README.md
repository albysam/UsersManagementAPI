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


Create a new SQL Server database named UserManagementDB.

Create a .env File:

In the root directory of the backend project, create a file named .env and add the following content, replacing placeholder values with your actual SQL Server details:

env
Copy code
# SQL Server Configuration
SQLSERVER_CONNECTION_STRING=Server=localhost;Database=UserManagementDB;User Id=your_username;Password=your_password;
Apply Migrations:

Run the following command to apply the database migrations and set up the schema:

bash
Copy code
dotnet ef database update
Start the Backend Server:

Start the ASP.NET Core Web API server using:

bash
Copy code
dotnet run
The backend server will be running at http://localhost:5000.


npm run start
The server will be running at http://localhost:3000.

Frontend Setup
Clone the Repository:

bash
Copy code
git clone https://github.com/albysam/usersmanagementFrontend.git
cd usersmanagementFrontend
Install Dependencies:

bash
Copy code
npm install
Configure API URL:

Ensure the API URL in your api.ts matches your backend server URL:

typescript
Copy code
// src/api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3000', // Backend API URL
});

export default api;
Start the Frontend:

bash
Copy code
npm start
The frontend will be running at http://localhost:3000.

API Endpoints
Create User: POST /users
Get All Users: GET /users
Get User by ID: GET /users/:id
Update User: PUT /users/:id
Delete User: DELETE /users/:id
Error Handling
Duplicate email errors during user creation.
User not found for fetch, update, or delete operations.
Validation errors if required fields are missing.
Contact
For questions, contact me at albypsam001@gmail.com.
