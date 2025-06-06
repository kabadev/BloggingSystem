# BloggingSystem

# Author Blog Post API

A clean architecture-based RESTful API built with ASP.NET Core, Entity Framework Core, and SQL Server. This project demonstrates a blogging system where authors can create blogs, and each blog can have multiple posts. The solution follows Onion Architecture and includes a Unit of Work pattern.

---

## 🧱 Project Structure

```
BloggingSystem/
├── BloggingSystem.API/               # Web API layer (Controllers)
├── BloggingSystem.Application/      # Services and DTOs
├── BloggingSystem.Domain/           # Entities and Interfaces
├── BloggingSystem.Infrastructure/   # Repositories and DbContext

```

---

## 🔧 Technologies Used

- ASP.NET Core Web API  (.NET 8.0.0)
- Entity Framework Core  
- SQL Server  
- Onion Architecture (Domain, Application, Infrastructure, API)  
- Repository & Unit of Work Pattern  
- Clean JSON Response Wrapping  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- Visual Studio 2022 or later

### Setup Instructions

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/AuthorBlogPostApi.git
   cd AuthorBlogPostApi
   ```

2. **Update the connection string:**

   Open `appsettings.json` in `BloggingSystem.API` and configure your SQL Server connection string.

3. **Apply migrations & create database:**

   ```bash
   dotnet ef migrations add InitialCreate --project BloggingSystem.Infrastructure --startup-project BloggingSystem.API
   dotnet ef database update --project BloggingSystem.Infrastructure --startup-project BloggingSystem.API
   ```

4. **Run the application:**

   ```bash
   dotnet run --project BloggingSystem.API
   ```

   Visit: `https://localhost:5001/swagger` to explore the API via Swagger UI.

---

## 📦 API Endpoints Overview

### 🔹 Authors

- `GET /api/authors` – Get all authors
- `GET /api/authors/{id}` – Get author by ID
- `POST /api/authors` – Create new author
- `DELETE /api/authors/{id}` – Delete an author
- `GET /api/authors/{authorId}/blogs` – Get blogs by author ID

### 🔹 Blogs

- `GET /api/blogs` – Get all blogs
- `GET /api/blogs/{id}` – Get blog by ID
- `POST /api/blogs` – Create new blog
- `DELETE /api/blogs/{id}` – Delete a blog

### 🔹 Posts

- `GET /api/posts` – Get all posts
- `GET /api/posts/{id}` – Get post by ID
- `POST /api/posts` – Create new post
- `DELETE /api/posts/{id}` – Delete a post
- `GET /api/blogs/{blogId}/posts` – Get posts by blog ID

---

## ✅ Response Format

All responses follow a standardized format:

Success:
```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": { /* result data */ }
}
```

Error:
```json
{
  "success": false,
  "message": "An error occurred",
  "error": "Detailed error message"
}
```

---

## 📌 Future Enhancements
-read and write operations (command and query)
- Add authentication/authorization (JWT)
- Implement pagination
- Add update endpoints (PUT/PATCH)
- Unit and integration tests

---

## 🧑‍💻 Contributing

Pull requests are welcome. Please open an issue first to discuss your proposal.

---

## 📄 License

This project is open-source and available under the MIT License.
