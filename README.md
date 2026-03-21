Simple Movie Database API (SMDB)

A simple RESTful API built in C# using a custom lightweight HTTP framework.
This project demonstrates core backend concepts including routing, middleware, layered architecture, and in-memory data persistence.

🚀 Features

Custom HTTP server using HttpListener

Middleware pipeline (logging, error handling, parsing, etc.)

Layered architecture:

Repository (data access)

Service (business logic)

Controller (HTTP layer)

Router (routing system)

In-memory database (no external DB required)

JSON-based API responses

Pagination support

Parametrized routing

📦 Resources Implemented

The API supports CRUD+L operations for:

🎥 Movies

🎭 Actors

👤 Users

🔗 Actor-Movies (relationships)

🏗️ Project Structure
SimpleMDB/
│
├── src/
│   ├── Smdb.Api/          # API layer (controllers, routers, app)
│   ├── Smdb.Core/         # Core logic (models, services, repositories)
│
├── tests/                 # Unit tests
│
└── SharedLibrary/         # Custom HTTP framework

⚙️ Setup & Run
1. Build the project
dotnet build
2. Run the API
dotnet run --project src/Smdb.Api
3. Default URL
http://localhost:5000
📡 API Endpoints
Movies
GET    /api/v1/movies
POST   /api/v1/movies
GET    /api/v1/movies/:id
PUT    /api/v1/movies/:id
DELETE /api/v1/movies/:id
Actors
GET    /api/v1/actors
POST   /api/v1/actors
GET    /api/v1/actors/:id
PUT    /api/v1/actors/:id
DELETE /api/v1/actors/:id
Users
GET    /api/v1/users
POST   /api/v1/users
GET    /api/v1/users/:id
PUT    /api/v1/users/:id
DELETE /api/v1/users/:id
Actor-Movies
GET    /api/v1/actors-movies
POST   /api/v1/actors-movies
GET    /api/v1/actors-movies/:id
PUT    /api/v1/actors-movies/:id
DELETE /api/v1/actors-movies/:id
🧪 Example Requests
Get Movies
curl "http://localhost:5000/api/v1/movies?page=1&size=5"
Create Movie
curl -X POST "http://localhost:5000/api/v1/movies" \
-H "Content-Type: application/json" \
-d '{
  "id": -1,
  "title": "Inception",
  "year": 2010,
  "description": "Dream infiltration."
}'

🧠 Architecture Overview

Request flow:

HTTP Request
   ↓
Router
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
Memory Database
   ↓
JSON Response

📁 Configuration

Configuration is handled via:

appsettings.cfg

Example:

HOST=http://localhost
PORT=5000
🛠️ Technologies

C# (.NET 8)

Custom HTTP framework (SharedLibrary)

JSON serialization (System.Text.Json)

📌 Notes

No external database is used — all data is stored in memory.

Designed for educational purposes to demonstrate backend architecture.

Easily extendable to SQL or NoSQL databases.

👨‍💻 Author

Edward Navarreto
