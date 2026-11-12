using Microsoft.EntityFrameworkCore;
using RecipePlanner.Api.Models;

namespace RecipePlanner.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            e.Property(x => x.Email).HasMaxLength(100).IsRequired();
            e.Property(x => x.Role).HasMaxLength(20).IsRequired();
            e.HasIndex(x => x.UserName).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

        b.Entity<Category>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(50).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        b.Entity<Ingredient>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(80).IsRequired();
            e.Property(x => x.DefaultUnit).HasMaxLength(20).IsRequired();
            e.HasIndex(x => x.Name).IsUnique();
        });

        b.Entity<Recipe>(e =>
        {
            e.Property(x => x.Title).HasMaxLength(120).IsRequired();
            e.Property(x => x.Description).HasMaxLength(1000);
            e.HasOne(x => x.Category).WithMany(c => c.Recipes)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Owner).WithMany(u => u.Recipes)
                .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<RecipeIngredient>(e =>
        {
            e.Property(x => x.Quantity).HasPrecision(10, 2);
            e.Property(x => x.Unit).HasMaxLength(20).IsRequired();
            e.HasOne(x => x.Recipe).WithMany(r => r.Ingredients)
                .HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Ingredient).WithMany(i => i.RecipeIngredients)
                .HasForeignKey(x => x.IngredientId).OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<RecipeStep>(e =>
        {
            e.Property(x => x.Instruction).HasMaxLength(1000).IsRequired();
            e.HasOne(x => x.Recipe).WithMany(r => r.Steps)
                .HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);
        });

        SeedData.Apply(b);
    }
}
