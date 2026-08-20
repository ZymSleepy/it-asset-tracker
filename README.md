![Build Status](https://github.com/ZymSleepy/it-asset-tracker/actions/workflows/dotnet.yml/badge.svg)

# IT Asset & Ticket Tracker

# IT Asset & Ticket Tracker

A web application for managing IT assets (laptops, monitors, etc.) and support tickets, built to reflect real IT support workflows — inspired by my experience in IT support and endpoint security monitoring during my internship.

![Dashboard Screenshot](docs/dashboard-screenshot.png)

## Features

- **Asset Management** — add, view, edit, and delete IT assets with status tracking (Available, Assigned, Retired)
- **Ticket Tracking** — log support tickets linked to specific assets, with priority and status dropdowns
- **Dashboard** — live overview of total assets, available assets, and open/total tickets
- **Relational data** — tickets are linked to assets via a proper foreign key relationship, with a dropdown selector

## Tech Stack

- **ASP.NET Core MVC** (.NET 10)
- **Entity Framework Core** with SQLite
- **Bootstrap** (default ASP.NET Core template styling)

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 10.0 or later)
- Visual Studio 2022 Community (recommended) or VS Code

### Run locally

```bash
git clone https://github.com/ZymSleepy/it-asset-tracker
cd ITAssetTracker
dotnet restore
dotnet ef database update
dotnet run
```

Then open the URL shown in the terminal (e.g. `https://localhost:7208`) in your browser.

## Project Structure

```
/Controllers    - MVC controllers (Assets, Tickets, Home)
/Models         - Asset and Ticket entity classes
/Data           - EF Core DbContext
/Views          - Razor views for each feature
/Migrations     - EF Core database migrations
```

## Coding Standards

This project follows the standards documented in [CODING_STANDARDS.md](CODING_STANDARDS.md), covering naming conventions, async/await practices, error handling, and testing approach.

## Roadmap

- [x] Asset CRUD
- [x] Ticket CRUD with asset relationship
- [x] Dashboard with live counts
- [ ] Unit tests for business logic
- [ ] CI pipeline (GitHub Actions)

## Author

Built by Azim — Computer Science (Multimedia Computing) graduate, currently seeking entry-level backend/software developer roles. [Connect on LinkedIn](https://www.linkedin.com/in/azim-haikal-79b474345/).