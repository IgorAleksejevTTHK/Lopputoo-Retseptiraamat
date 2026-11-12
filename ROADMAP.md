# Roadmap

Target grade: **4** (Programming + Distributed Applications).
Stack: ASP.NET Core 8 Web API, EF Core 8 + SQLite, xUnit + Moq, React (Vite).

| Week | Scope | Status |
|------|-------|--------|
| 1 | Solution, data model (Users, Categories, Ingredients, Recipes, RecipeIngredients, RecipeSteps), migrations + seed, repositories, services, DI, recipe CRUD with search / ingredient filter / sorting / pagination, DTOs, Swagger XML docs, `.http` file | done |
| 2 | Servings recalculation `GET /api/recipes/{id}?servings=6`; weekly meal plan (MealPlan + MealPlanEntry tables, migration) `CRUD /api/meal-plans/{week}`; global error handling with ProblemDetails (custom exceptions instead of try/catch in controllers); request logging; unit tests (>= 8, >= 2 with Moq) | todo |
| 3 | JWT authentication (register/login, password hashing, PasswordHash migration), roles User/Admin/Service, `[Authorize]` + ownership checks, Swagger Authorize button, CORS for the React client, Postman-style `.http` updates | todo |
| 4 | React client (Vite): login, recipe list with search/filter/sort/paging, recipe details with servings switch, recipe create/edit form with client validation, weekly meal plan view; works only through the API | todo |
| 5 | English README (setup, architecture, endpoints), Estonian documentation (school template) exported to PDF, final polishing | todo |

Out of scope (grade 5): shopping list, sharing, Worker Service, WebSocket, second client.
