# Roadmap

Target grade: **4** (Programming + Distributed Applications).
Stack: ASP.NET Core 8 Web API, EF Core 8 + SQLite, xUnit + Moq, .NET MAUI mobile client (MVVM, CommunityToolkit.Mvvm).
This is a mobile topic: the main client is the MAUI app.

Deadline: 30.11.2026. One step per working day.

| Date | Scope | Status |
|------|-------|--------|
| 12.11 | Solution, data model (Users, Categories, Ingredients, Recipes, RecipeIngredients, RecipeSteps), migrations + seed, repositories, services, DI, recipe CRUD with search / ingredient filter / sorting / pagination, DTOs, Swagger XML docs, `.http` file, backlog script | done |
| 13.11 | Servings recalculation `GET /api/recipes/{id}?servings=6` (logic in service) | todo |
| 16.11 | Meal plan data model: MealPlan + MealPlanEntry tables, migration, seed | todo |
| 17.11 | `CRUD /api/meal-plans/{week}` (ISO week, e.g. 2026-W47): repository, service, controller, DTOs, `.http` | todo |
| 18.11 | Global error handling with ProblemDetails (custom exceptions, `IExceptionHandler`, no try/catch in controllers), logging | todo |
| 19.11 | Unit tests: at least 8, at least 2 with Moq | todo |
| 20.11 | JWT: register/login, password hashing, PasswordHash migration | todo |
| 23.11 | Roles User/Admin/Service, `[Authorize]`, ownership checks, Swagger Authorize button, CORS (for future web clients) | todo |
| 24.11 | MAUI: src/RecipePlanner.Mobile.Core (net8.0: ViewModels with CommunityToolkit.Mvvm, API client with JWT) + src/RecipePlanner.Mobile (MAUI Shell, DI, pages), login and recipe list with search/filter/sort/paging; GitHub Actions workflow that builds the MAUI app (Android) on windows-latest | todo |
| 25.11 | MAUI: recipe details with servings switch, create/edit form with client-side validation (ViewModel validation) | todo |
| 26.11 | MAUI: weekly meal plan page (add/remove recipes per day) | todo |
| 27.11 | English README, Estonian documentation (school template) as PDF | todo |

Out of scope (grade 5): shopping list, sharing, Worker Service, WebSocket, second client (web admin).
