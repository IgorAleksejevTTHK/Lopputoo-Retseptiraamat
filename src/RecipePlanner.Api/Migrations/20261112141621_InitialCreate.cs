using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipePlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    DefaultUnit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Servings = table.Column<int>(type: "INTEGER", nullable: false),
                    PrepTimeMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Instruction = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Breakfast" },
                    { 2, "Soup" },
                    { 3, "Main course" },
                    { 4, "Dessert" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "DefaultUnit", "Name" },
                values: new object[,]
                {
                    { 1, "g", "Oat flakes" },
                    { 2, "ml", "Milk" },
                    { 3, "g", "Potato" },
                    { 4, "pcs", "Onion" },
                    { 5, "pcs", "Carrot" },
                    { 6, "g", "Chicken fillet" },
                    { 7, "tsp", "Salt" },
                    { 8, "pcs", "Egg" },
                    { 9, "g", "Wheat flour" },
                    { 10, "g", "Sugar" },
                    { 11, "g", "Butter" },
                    { 12, "g", "Pasta" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@recipes.local", "Admin", "admin" },
                    { 2, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "mari@recipes.local", "User", "mari" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "OwnerId", "PrepTimeMinutes", "Servings", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Classic Estonian breakfast porridge.", 2, 10, 2, "Oatmeal porridge" },
                    { 2, 2, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Light soup with vegetables.", 2, 50, 4, "Chicken soup" },
                    { 3, 4, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thin pancakes.", 1, 30, 4, "Pancakes" },
                    { 4, 3, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Quick weekday dinner.", 1, 25, 3, "Chicken pasta" }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, 1, 100m, 1, "g" },
                    { 2, 2, 400m, 1, "ml" },
                    { 3, 7, 0.5m, 1, "tsp" },
                    { 4, 6, 400m, 2, "g" },
                    { 5, 3, 500m, 2, "g" },
                    { 6, 4, 1m, 2, "pcs" },
                    { 7, 5, 2m, 2, "pcs" },
                    { 8, 7, 1m, 2, "tsp" },
                    { 9, 9, 200m, 3, "g" },
                    { 10, 2, 500m, 3, "ml" },
                    { 11, 8, 2m, 3, "pcs" },
                    { 12, 10, 30m, 3, "g" },
                    { 13, 11, 20m, 3, "g" },
                    { 14, 12, 300m, 4, "g" },
                    { 15, 6, 300m, 4, "g" },
                    { 16, 4, 1m, 4, "pcs" }
                });

            migrationBuilder.InsertData(
                table: "RecipeSteps",
                columns: new[] { "Id", "Instruction", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { 1, "Bring the milk to a boil.", 1, 1 },
                    { 2, "Add oat flakes and salt, cook for 5 minutes while stirring.", 1, 2 },
                    { 3, "Boil the chicken in 2 litres of water for 20 minutes.", 2, 1 },
                    { 4, "Add diced potatoes, carrots and onion.", 2, 2 },
                    { 5, "Cook until vegetables are soft, season with salt.", 2, 3 },
                    { 6, "Whisk eggs with sugar, add milk and flour.", 3, 1 },
                    { 7, "Fry thin pancakes on buttered pan.", 3, 2 },
                    { 8, "Cook pasta according to the package.", 4, 1 },
                    { 9, "Fry chicken and onion, mix with pasta.", 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeId",
                table: "RecipeSteps",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
