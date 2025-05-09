# MyApp Starter Kit

A clean, scalable ASP.NET Core 8.0 Web API starter kit built with **Clean Architecture**, **MediatR**, **JWT Authentication**, **FluentValidation**, and **Entity Framework Core**.

---

## 🧱 Project Structure

```
src/
🔹️ MyApp.WebAPI              # API entry point
🔹️ MyApp.Application         # Use cases (MediatR commands, queries, validators)
🔹️ MyApp.Infrastructure      # EF Core, repository implementations
🔹️ MyApp.Core                # Domain models, interfaces
```

---

## 🚀 Features

✅ Clean Architecture (CQRS + layered separation)
✅ Secure JWT-based Authentication
✅ Role-based Authorization
✅ FluentValidation + Global Validation Errors
✅ MediatR for decoupled command/query handling
✅ EF Core + Code-first Migrations
✅ Swagger with JWT support
✅ Seeded Admin & User accounts
✅ Easily extensible for any domain

---

## 🔐 Seeded Users

| Role  | Email                                         | Password |
| ----- | --------------------------------------------- | -------- |
| Admin | [admin@example.com](mailto:admin@example.com) | admin123 |
| User  | [user@example.com](mailto:user@example.com)   | user123  |

> Passwords are **BCrypt hashed** in the database.

---

## 💠 How to Run

1. **Clone** the repo:

   ```bash
   git clone https://github.com/yourusername/MyApp.StarterKit.git
   cd MyApp.StarterKit
   ```

2. **Apply migrations**:

   ```bash
   dotnet ef database update --project src/MyApp.Infrastructure --startup-project src/MyApp.WebAPI
   ```

3. **Run the API**:

   ```bash
   dotnet run --project src/MyApp.WebAPI
   ```

4. Open Swagger UI at:

   ```
   https://localhost:5001/swagger
   ```

---

## 🔐 JWT Authentication

1. Call `/api/users/login` with a POST body:

```json
{
  "email": "admin@example.com",
  "password": "admin123"
}
```

2. Copy the returned JWT token

3. Click “Authorize” in Swagger and paste:

```
Bearer {your-token}
```

4. Now you can access `[Authorize]` endpoints

---

## 🥪 Available Endpoints

| Method | Route                 | Description                       | Auth Required |
| ------ | --------------------- | --------------------------------- | ------------- |
| POST   | `/api/users/register` | Register a new user               | ❌             |
| POST   | `/api/users/login`    | Login and receive JWT             | ❌             |
| GET    | `/api/users`          | Admin: view all users; User: self | ✅             |
| GET    | `/api/users/me`       | Get current user's email & role   | ✅             |

---

## 📦 Tech Stack

* ASP.NET Core 8.0
* Entity Framework Core
* MediatR
* FluentValidation
* JWT (System.IdentityModel.Tokens)
* Swagger / Swashbuckle
* Clean Architecture principles

---

## 🧹 Next Steps

* ✅ Add refresh tokens or logout flow
* 🔐 Implement password reset or email verification
* 🧪 Add unit + integration tests
* 📁 Package as a NuGet/Template repo

---

## 💡 License

MIT — feel free to use, modify, and build on it.

---

## 🙌 Acknowledgments

Inspired by industry patterns: [Clean Architecture by Robert C. Martin](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html), ASP.NET community best practices.
