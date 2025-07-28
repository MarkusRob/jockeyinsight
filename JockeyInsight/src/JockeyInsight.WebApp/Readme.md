
# Racehorse Jockey Insight App

## Overview

This project is a simple web application to help racehorse owners review recent race results and identify the best jockey for their horse. Users can add notes to race results to aid decision-making.

The backend is built with .NET 6 and MySQL, and the frontend uses Angular 16.

## How to Run

### Backend

1. Ensure you have .NET 6 SDK installed.
2. Configure your MySQL connection string in `appsettings.json`.
3. Run database migrations to create/update the database schema:
   ```bash
   dotnet ef database update
   ```
4. Start the backend API:
   ```bash
   dotnet run
   ```

### Frontend

1. Navigate to the frontend folder:
   ```bash
   cd frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the Angular development server:
   ```bash
   npm start
   ```
4. Open your browser and go to `http://localhost:4200`

## Features

- Upload race results CSV (admin use).
- Paginated list of race results with search filters.
- Add/edit notes for each race result.
- Dashboard insights on jockey performance per horse.

---

Feel free to reach out if you have questions or need help running the project.
