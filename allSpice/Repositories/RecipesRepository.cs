using System.Collections.Generic;
using System.Data;
using Dapper;
using allSpice.Models;
using System.Linq;
using System;

namespace allSpice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> GetAll()
    {
      string sql = "SELECT * FROM recipes";
      return _db.Query<Recipe>(sql).ToList();
    }

    internal Recipe Get(int id)
    {
      string sql = "SELECT * FROM recipes WHERE id = @id;";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }

    internal List<Recipe> GetMyRecipes(string id)
    {
      string sql = "SELECT * FROM recipes WHERE creatorId = @id";
      return _db.Query<Recipe>(sql, new { id }).ToList();
    }

    internal List<Recipe> GetByCategory(string category)
    {
      string sql = "SELECT * FROM recipes WHERE category = @category";
      return _db.Query<Recipe>(sql, new { category }).ToList();
    }

    internal List<Recipe> GetByTitle(string title)
    {
      string sql = "SELECT * FROM recipes WHERE title LIKE @title";
      return _db.Query<Recipe>(sql, new { title }).ToList();
    }

    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO recipes
      (title, subtitle, category, creatorId, imgUrl)
      VALUES
      (@Title, @Subtitle, @Category, @CreatorId, @ImgUrl);
      SELECT LAST_INSERT_ID()
      ";
      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      newRecipe.Id = id;
      return newRecipe;
    }

    internal Recipe Update(Recipe updatedRecipe)
    {
      string sql = @"
      UPDATE recipes
      SET
      title = @Title,
      subtitle = @Subtitle,
      category = @Category, 
      imgUrl = @ImgUrl
      WHERE id = @Id;
      ";
      int rows = _db.Execute(sql, updatedRecipe);
      if (rows <= 0)
      {
        throw new Exception("Update failed.");
      }
      return updatedRecipe;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }


  }
}


