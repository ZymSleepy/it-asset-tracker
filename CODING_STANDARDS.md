# C# / .NET Coding Standards

> Personal engineering standards I follow on every C#/.NET project. Kept here so my code is consistent, reviewable, and professional-grade — and so anyone looking at my repos can see how I work.

---

## 1. Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Class, Interface, Enum, Struct | PascalCase | `OrderProcessor`, `IPaymentGateway` |
| Interface prefix | `I` + PascalCase | `IRepository<T>` |
| Method, Property | PascalCase | `CalculateTotal()`, `IsActive` |
| Local variable, parameter | camelCase | `orderTotal`, `customerId` |
| Private field | `_camelCase` | `_logger`, `_httpClient` |
| Constant | PascalCase | `MaxRetryCount` |
| Async method | PascalCase + `Async` suffix | `GetCustomerAsync()` |
| Boolean variable/property | Question form | `isValid`, `hasPermission`, `canRetry` |
| Generic type parameter | `T` or `TDescriptive` | `T`, `TEntity`, `TKey` |

**No abbreviations** unless universally understood (`Id`, `Url`, `Http` are fine; `Mgr`, `Cfg`, `Proc` are not).

---

## 2. File & Project Structure

- One public type per file; file name matches the type name.
- Namespace mirrors folder structure.
- Group by feature/domain, not by technical layer, for anything beyond a small project:
  ```
  /Orders
    OrderService.cs
    OrderRepository.cs
    OrderController.cs
  /Payments
    PaymentService.cs
  ```
- Solution structure for anything resume-worthy: separate `.csproj` per concern — `MyApp.Api`, `MyApp.Core`, `MyApp.Infrastructure`, `MyApp.Tests`. This alone signals you understand separation of concerns to a reviewer.

---

## 3. Language & Style Rules

- **`var`** only when the type is obvious from the right-hand side (`var list = new List<Order>();`). Otherwise use the explicit type.
- **Nullable reference types enabled** (`<Nullable>enable</Nullable>` in every `.csproj`). Treat every warning as something to fix, not suppress.
- **Expression-bodied members** for one-liners only:
  ```csharp
  public int Age => DateTime.Now.Year - BirthYear;
  ```
- **Pattern matching** over type-checking + casting:
  ```csharp
  // Good
  if (shape is Circle { Radius: > 0 } circle) { ... }

  // Avoid
  if (shape is Circle) { var circle = (Circle)shape; if (circle.Radius > 0) ... }
  ```
- **String interpolation** over concatenation: `$"Order {id} total: {total:C}"`.
- **`using` declarations** (C# 8+) over nested `using` blocks where possible.
- Braces always used, even for single-line `if` statements. No exceptions — this is a common quiet source of bugs.

---

## 4. Async/Await

- Never `async void` except top-level event handlers.
- Always propagate `CancellationToken` through async call chains in service/API code.
- Use `ConfigureAwait(false)` in library code (not needed in ASP.NET Core app code).
- Never block on async code with `.Result` or `.Wait()` — this is one of the fastest ways to signal inexperience in a code review.
- Suffix every async method with `Async`.

---

## 5. Error Handling & Logging

- Exceptions are for *exceptional* cases, not control flow. Don't use exceptions to validate normal user input — return a `Result<T>` or validation object instead.
- Catch specific exception types. Never `catch (Exception)` and swallow it silently.
- Always log with context (structured logging via `ILogger<T>`, not `Console.WriteLine`):
  ```csharp
  _logger.LogWarning("Order {OrderId} failed validation: {Reason}", order.Id, reason);
  ```
- Custom exceptions only when they add meaning (`InsufficientStockException`), not as a wrapper for everything.

---

## 6. SOLID & Design

- **Single Responsibility** — if a class name needs "And" or "Manager" to describe it, it probably does too much.
- **Dependency Injection** by constructor, always. No `new ServiceX()` inside a class that has business logic — this is the #1 thing that makes code testable or not.
- Depend on interfaces/abstractions for anything crossing a boundary (data access, external APIs, file system).
- Favor composition over inheritance unless there's a genuine "is-a" relationship.

---

## 7. Testing

- One test project per production project: `MyApp.Core.Tests`.
- Framework: xUnit (industry default; NUnit/MSTest acceptable if a job posting specifies it).
- Naming: `MethodName_Scenario_ExpectedResult`
  ```csharp
  CalculateTotal_WithDiscountCode_AppliesPercentageOff()
  ```
- **Arrange / Act / Assert** structure, with the sections visibly separated (blank line or comment).
- Mock external dependencies (Moq or NSubstitute) — never hit a real database or API in a unit test.
- Aim for tests around business logic first; don't chase 100% coverage on trivial getters/setters.

---

## 8. Documentation & Comments

- XML doc comments (`/// <summary>`) on all public classes/methods in libraries or anything meant to be consumed by others.
- Comments explain **why**, not **what** — the code should already say what it does.
- Every repo has a `README.md` with: what it does, how to run it, tech stack, and (for portfolio projects) a screenshot or short GIF if it has a UI.

---

## 9. Git & Version Control

- Conventional commit messages:
  ```
  feat: add discount code validation to checkout
  fix: correct off-by-one error in pagination
  refactor: extract email logic into NotificationService
  test: add coverage for OrderService edge cases
  ```
- Small, focused commits over one giant commit at the end.
- No commented-out code or `TODO` left in commits without a linked issue.
- `.gitignore` covers `bin/`, `obj/`, `.vs/`, `*.user` — never commit build artifacts.

---

## 10. Tooling (sets you apart in a portfolio)

- `.editorconfig` checked into every repo to enforce formatting automatically.
- Enable built-in Roslyn analyzers (`<EnableNETAnalyzers>true</EnableNETAnalyzers>`) and treat key warnings as errors in CI.
- If you have a GitHub repo, add a simple GitHub Actions workflow that runs `dotnet build` and `dotnet test` on push — this is a small effort that immediately reads as "this person understands professional workflows."

---

## Quick Pre-Commit Checklist

- [ ] Builds with zero warnings
- [ ] All tests pass
- [ ] No `Console.WriteLine` debug leftovers
- [ ] No commented-out code
- [ ] Nullable warnings resolved, not suppressed
- [ ] Public methods have XML doc comments
- [ ] Commit message follows convention