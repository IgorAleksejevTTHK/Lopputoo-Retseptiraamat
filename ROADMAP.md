# Roadmap

Target grade: **4** (Programming + Distributed Applications).
Stack: ASP.NET Core 8 Web API, EF Core 8 + SQLite, xUnit + Moq, React (Vite).

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
| 23.11 | Roles User/Admin/Service, `[Authorize]`, ownership checks, Swagger Authorize button, CORS | todo |
| 24.11 | React (Vite) client: setup, API client, login, recipe list with search/filter/sort/paging | todo |
| 25.11 | React: recipe details with servings switch, create/edit form with client validation | todo |
| 26.11 | React: weekly meal plan view | todo |
| 27.11 | English README, Estonian documentation (school template) as PDF | todo |

Out of scope (grade 5): shopping list, sharing, Worker Service, WebSocket, second client.
