# Recipea 🫛

A recipe management application built with ASP.NET Core.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?logo=bootstrap)
![AWS](https://img.shields.io/badge/AWS-S3-FF9900?logo=amazon-aws)
![SQLite](https://img.shields.io/badge/SQLite-Database-003B57?logo=sqlite)

## Screenshots

### Desktop View

![Recipe List - Desktop](public/web-screenshot.png)

### Tablet & Mobile Views

<div style="display: flex; gap: 10px; flex-wrap: wrap;">
  <img src="public/recipes-tablet.png" alt="Recipe List - Tablet" width="400"/>
  <img src="public/create-tablet.png" alt="Create Recipe - Tablet" width="400"/>
  <img src="public/details-mobile.png" alt="Recipe Details - Mobile" width="300"/>
</div>

## Features

### Core Recipe Management

- **Create, Read, Update, Delete** recipes with full CRUD operations
- **Rich recipe data** including ingredients, instructions, description, and source
- **Time tracking** with Active Time and Total Time fields
- **Markdown-style formatting** - Use `[Title]` in ingredients and instructions for section headers

### Image Management

- **Image upload** directly to AWS S3 for reliable cloud storage
- **Image URL support** for using external images
- **Automatic fallback** to default placeholder images
- **Optimized for performance** with automatic image compression

### Search & Filter

- **Full-text search** across recipe titles, descriptions, and ingredients
- **Time-based filtering** using sliders for Active Time and Total Time

### Recipe Import

- **Import from URL** using Spoonacular API integration
- **Automatic parsing** of ingredients, instructions, and metadata
  
### Modern UI/UX

- **Responsive design** optimized for desktop, tablet, and mobile
- **Bootstrap 5** for modern, clean interface
- **Collapsible descriptions** for better content organization


## Tech Stack

### Backend

- **.NET 8.0** 
- **ASP.NET Core Razor Pages** - Server-side rendering with MVVM pattern
- **Entity Framework Core 9.0** - ORM for database operations
- **SQLite** - Lightweight, file-based database

### Frontend

- **Bootstrap 5.3** - Responsive CSS framework
- **Vanilla JavaScript** - ES6+ for interactivity
- **HTML5** - Semantic markup

### Cloud Services

- **AWS S3** - Scalable image storage
- **Spoonacular API** - Recipe import parsing service

### Key Packages

- `AWSSDK.S3` - AWS S3 integration for image uploads
- `Microsoft.EntityFrameworkCore.Sqlite` - SQLite database provider
- `Microsoft.EntityFrameworkCore.Design` - EF Core tooling
