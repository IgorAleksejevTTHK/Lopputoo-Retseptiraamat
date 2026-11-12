#!/usr/bin/env bash
# Creates labels, milestones, backlog issues and a GitHub Project (v2) board.
# Requirements: gh CLI logged in with scopes repo + project:
#   gh auth refresh -s project
# Usage: ./scripts/create-backlog.sh
set -euo pipefail

REPO="IgorAleksejevTTHK/L-pput--RetseptiRaamat"
OWNER="IgorAleksejevTTHK"
PROJECT_TITLE="Retseptid ja toiduplaan"

label() { gh label create "$1" --repo "$REPO" --color "$2" --force >/dev/null; }
label backend 1d76db
label frontend 5319e7
label database 0e8a16
label security b60205
label tests fbca04
label docs c5def5

milestone() {
  gh api "repos/$REPO/milestones" -f title="$1" -f description="$2" -f state=open >/dev/null 2>&1 || true
}
milestone "Week 1" "Data model, repositories, recipe CRUD"
milestone "Week 2" "Servings, meal plan, ProblemDetails, tests"
milestone "Week 3" "JWT, roles, CORS"
milestone "Week 4" "React client"
milestone "Week 5" "README and documentation"

PROJECT_NUMBER=$(gh project create --owner "$OWNER" --title "$PROJECT_TITLE" --format json --jq .number)
gh project link "$PROJECT_NUMBER" --owner "$OWNER" --repo "$REPO" >/dev/null || true

issue() { # title, milestone, labels, body
  local url
  url=$(gh issue create --repo "$REPO" --title "$1" --milestone "$2" --label "$3" --body "$4")
  gh project item-add "$PROJECT_NUMBER" --owner "$OWNER" --url "$url" >/dev/null
  echo "created: $1"
}

issue "Data model and EF Core migrations" "Week 1" "database,backend" \
"- [x] Users, Categories, Ingredients, Recipes, RecipeIngredients, RecipeSteps
- [x] Initial migration
- [x] Seed data (HasData)"
issue "Repository pattern, services and DI" "Week 1" "backend" \
"- [x] IRecipeRepository, IIngredientRepository, ICategoryRepository
- [x] RecipeService, CatalogService registered in DI"
issue "Recipe CRUD with search, filter, sort, pagination" "Week 1" "backend" \
"- [x] GET /api/recipes?search=&ingredient=&categoryId=&sortBy=&page=
- [x] GET/POST/PUT/DELETE with correct status codes (200, 201, 204, 400, 404)
- [x] DTOs only, Swagger XML comments, .http file"
issue "Servings recalculation" "Week 2" "backend" \
"- [ ] GET /api/recipes/{id}?servings=6 scales ingredient quantities
- [ ] Validation for servings range"
issue "Weekly meal plan" "Week 2" "database,backend" \
"- [ ] MealPlan + MealPlanEntry tables, migration
- [ ] CRUD /api/meal-plans/{week} (ISO week, e.g. 2026-W47)"
issue "Global error handling (ProblemDetails) and logging" "Week 2" "backend" \
"- [ ] Custom exceptions, IExceptionHandler -> ProblemDetails
- [ ] Remove try/catch from controllers
- [ ] Request and service logging"
issue "Unit tests" "Week 2" "tests" \
"- [ ] At least 8 unit tests
- [ ] At least 2 using Moq"
issue "JWT authentication and roles" "Week 3" "security,backend" \
"- [ ] Register / login, password hashing
- [ ] Roles User, Admin, Service
- [ ] [Authorize], ownership checks
- [ ] Swagger Authorize button"
issue "CORS for React client" "Week 3" "security,backend" \
"- [ ] Allow the Vite dev origin"
issue "React client: recipes" "Week 4" "frontend" \
"- [ ] Login
- [ ] List with search, filter, sort, paging
- [ ] Details with servings switch
- [ ] Create/edit form with client-side validation"
issue "React client: weekly meal plan" "Week 4" "frontend" \
"- [ ] Week view, add/remove recipes per day"
issue "README in English" "Week 5" "docs" \
"- [ ] Setup, architecture, endpoints, test accounts"
issue "Documentation in Estonian (PDF)" "Week 5" "docs" \
"- [ ] Written by the school template, exported to PDF for Moodle"

echo "Project #$PROJECT_NUMBER created: https://github.com/users/$OWNER/projects/$PROJECT_NUMBER"
