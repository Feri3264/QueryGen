
# QueryGen

## 1. Introduction
**QueryGen** is an API service that allows you to generate valid SQL queries from natural language prompts and executing them on your database — without directly connecting the LLM to your actual database.  
The LLM only has access to **database metadata**, ensuring your data remains secure.

---

## 2. Features
- **LLM is isolated from the actual DB** → It only receives metadata.
- **Supports SQL Server** → Connect using a custom connection string.
- **Session-based architecture** → Each database is managed in a separate session.
- **JWT Authentication** → Secure endpoints with access and refresh tokens.
- **Clean Architecture + CQRS** → Highly maintainable and testable.
- **Switchable LLM Providers** → Currently using an external API but can switch to local models.
- **Regex-based Query Extraction & Validation** → Ensures query correctness.

---

## 3. Tech Stack
- **Backend**: .NET 9
- **Database**: SQL Server
- **Architecture**: Clean Architecture, CQRS
- **Authentication**: JWT + Refresh Token
- **LLM**: External API (future support for local models like Ollama)

---

## 4. Installation

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- A valid API key from [OpenRouter](https://openrouter.ai/docs/api-reference/authentication)
- An OpenRouter [LLM Model](https://openrouter.ai/models)
- You can also import [PostmanQueryGen](PostmanQueryGen) file in your Postman.

### Steps
```bash
# 1. Clone the repository
git clone https://github.com/Feri3264/QueryGen.git
cd QueryGen

# 2. Update appsettings.json with your DB
# Example:
# "ConnectionStrings": { "QueryGen": "Data Source=...;Initial Catalog=...;User Id=...;Password=..." }

# 3. Apply migrations
dotnet ef database update --project src/QueryGen.Infrastructure --startup-project src/QueryGen.Api


# 4. Run the API
dotnet run --project src/QueryGen.Api
```
---

## 5. Usage

### Step 1: Register & Login
```bash
POST /api/user/register
{
  "username": "test",
  "password": "12345678"
}

POST /api/user/login
{
  "username": "test",
  "password": "12345678"
}
# Response contains accessToken & refreshToken
```

### Step 2: Create a Session
```bash
POST /api/session
Authorization: Bearer {accessToken}

{
  "sessionName": "Test DB",
  "apiToken": "LLM_API_KEY",
  "llmModel": "LLMModel",
  "server": "localhost",
  "dbName": "ShopDB",
  "useWinAuth": false,
  "username": "sa",
  "password": "1234",
  "port": 1433
}
```

### Step 3: Send a Prompt
```bash
POST /api/session/{sessionId}/prompt
Authorization: Bearer {accessToken}

{
  "prompt": "Give me a query to get the most sold product."
}
```

---

## 6. API Reference

### User API
| Method | Endpoint | Description | Body |
|--------|----------|-------------|------|
| **POST** | `/api/user/register` | Register a new user | `{ "username": "string", "password": "string" }` |
| **POST** | `/api/user/login` | Login and get JWT & refresh token | `{ "username": "string", "password": "string" }` |
| **PATCH** | `/api/user` | Change User Password | `{ "newPassword": "string", "oldPassword": "string" }` |
| **POST** | `/api/auth/refreshtoken` | Refresh JWT using refresh token | `{ "refreshToken": "string" }` |

### Session API
| Method | Endpoint | Description | Body |
|--------|----------|-------------|------|
| **GET** | `/api/sessions` | Get all sessions for the current user | - |
| **GET** | `/api/session/{sessionId}` | Get session details | - |
| **POST** | `/api/session` | Create a new session | `{ "sessionName": "...", "apiToken": "...","llmModel": "...", "server": "...", "dbName": "...", "useWinAuth": true/false, "username": "...", "password": "...", "port": number }` |
| **PATCH** | `/api/session/{sessionId}/name` | Change Session Name | `{ "name": "..." }` |
| **PATCH** | `/api/session/{sessionId}/llmmodel` | Change Session LLM Model | `{ "model": "..." }` |
| **DELETE** | `/api/session/{sessionId}` | Delete a session | - |
| **POST** | `/api/session/{sessionId}/prompt` | Send a natural language request to the LLM for a specific session | `{ "prompt": "string" }` |

---

## 7. Future Plans
- Add a Web UI
- Save prompt & response history
- Support multiple LLMs with selectable options per session
- Support more databases (PostgreSQL, MySQL, etc.)

---

## 8. License
This project is licensed under the [MIT License](LICENSE).

