using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YourCookBook.Models.Entities;

public partial class CookBookContext : DbContext
{
    public CookBookContext(DbContextOptions<CookBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC07647EAA62");

            entity.Property(e => e.Amount).HasMaxLength(50);
            entity.Property(e => e.IngredientName).HasMaxLength(50);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_RecipeId");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recipe__3214EC07B144C0F0");

            entity.ToTable("Recipe");

            entity.Property(e => e.ImageUrl).HasMaxLength(50);
            entity.Property(e => e.RecipeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
