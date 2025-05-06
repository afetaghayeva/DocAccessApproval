# DocAccessApproval

A document access approval system built with .NET 8, following Domain-Driven Design (DDD), Clean Architecture principles, and using SQLite as the database.

---

## Project Structure & Design Decisions

This project uses a layered architecture with Domain-Driven Design (DDD) principles:

- **Domain Layer**  
  Contains the core domain models and aggregates ( `Document`, `User`), domain events, and interfaces (like `IDomainEvent`).

- **Application Layer**  
  Includes business logic like commands, queries, handlers, and application services. Uses MediatR for decoupled event handling and CQRS.

- **Infrastructure Layer**   
  Contains database implementations, context, entity configurations (SQLite and EF Core).

- **API Layer** 
  Hosts the RESTful endpoints for interacting with the system.
  - Integrated with **Swagger UI** for API exploration and testing.
  - Includes **custom exception middleware** returning structured `ProblemDetails` responses.
  - Each aggregate root has tailored error responses for improved client understanding.

- **Cross-Cutting Features**  
  - Event simulation via domain events (e.g., `MadeDecisionEvent`)
  - MediatR for domain event dispatching and handler execution
  - Bitwise operations for access control

---

##  Features & Assumptions

###  Implemented Features

1. **Document Request Expiration**  
   Each access request has an expiration date.

2. **Bitwise Access Control**  
   - Access types include `Read`, `Edit`, `Delete`.
   - User access type is stored as an integer using bitwise OR.
   - Access validation uses bitwise AND to check permissions:
     ```csharp
     (userAccess.AccessType & (int)accessType) == (int)accessType
     ```

3. **Filtering & Pagination**  
   - All list endpoints support pagination.
   - Access requests can be filtered by status.

4. **Seed Data**  
   - Default roles: `admin`, `approver`, `user`
   - Default admin user:
     - Username: `admin`
     - Password: `admin`

---

## Unit Test Scenarios

The project includes unit tests focused on verifying application-level logic in isolation using mocks.

### CreateAccessRequestCommandHandlerTests

#### Scenario: Successfully Add Access Request

- Simulates a user requesting access (e.g., Read + Edit) to a document.
- Mocks repository and mapper using Moq.
- Verifies:
  - The document is retrieved from the repository.
  - The `AccessRequest` is added to the aggregate root.
  - The request is persisted via `UnitOfWork`.
  - A valid `AccessRequestDto` is returned by the handler.

### MakeDecisionCommandHandlerTests

#### Scenario: Approver Makes a Decision

- Simulates an approver making an **approve or reject** decision on a pending request.
- Verifies:
  - The relevant document and request are fetched.
  - The decision is applied correctly (status and comments).
  - Changes are saved to the database.
  - A `DecisionDto` is returned.
  - A domain event (`MadeDecisionEvent`) is triggered.

---

##  How to Run the Project

1. **Clone the repo**  
   ```bash
   git clone https://github.com/your-repo/doc-access-approval.git
   cd doc-access-approval
   
2. **Run the project**
   ```bash
   dotnet run

3. **Database Initialization**
   No manual migration is needed.
   The app automatically applies EF Core migrations and creates a SQLite database on first run.

4. **Run the tests**
   ```bash
   dotnet test

## Future Improvements (If More Time Available)
  1. Add Refresh Token support for authentication.
  2. Add Localization for all exception and validation messages.
  3. Integrate FluentValidation for request validation in Commands and Queries.

## Database Design
![image](https://github.com/user-attachments/assets/a0fb5e8f-9c8b-49ff-acad-fafdf942d509)
![image](https://github.com/user-attachments/assets/9f255549-43a8-4f82-8385-8e20b1d1dc39)

## How to Test the Project

You can manually test the system using **Swagger UI** after running the app:

### 1. Register Two Users

Use the **register** endpoint in Swagger to create:
- **Bob** – regular user  
- **Alice** – future approver

### 2. Promote Alice to Approver

1. Login as the **default admin** using:
   - Username: `admin`  
   - Password: `admin`
2. Copy the `accessToken` from the login response.
3. Paste it into Swagger’s "Authorize" dialog as: Bearer <accessToken>
4. Use the **Add Role** endpoint to assign the `Approver` role to **Alice**.

### 3. Test Document Access Flow

- **Alice (Approver)** logs in and creates a **document**.
- **Bob (User)** logs in and requests **access** to the document (can choose Read, Edit, Delete, or any combination).
- **Alice** logs in and views the **pending access requests**.
- She uses the **Post Decision** endpoint to **approve or decline** the request.

### 4. Observe Access Behavior

- If approved, Bob can now access the document based on his requested permissions:
- Read
- Edit
- Delete
- Any valid combination (using bitwise logic)

### 5. Event Confirmation

- After Alice makes a decision, a `MadeDecisionEvent` is triggered internally.
- A **message is printed to the console** confirming the event was handled.





