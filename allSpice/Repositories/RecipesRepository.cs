using System.Collections.Generic;
using System.Data;
using Dapper;
using allSpice.Models;
using System.Linq;

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


    // Cannot add or update a child row: a foreign key constraint fails (`TristanDevDB`.`recipes`, CONSTRAINT `recipes_ibfk_1` FOREIGN KEY (`creatorId`) REFERENCES `accounts` (`id`) ON DELETE CASCADE)
    // NOTE fixed! I was using the authorization token for an account that wasn't in my accounts database. Do we need to create our own "Create account" routes?
    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO recipes
      (title, subtitle, category, creatorId)
      VALUES
      (@Title, @Subtitle, @Category, @CreatorId);
      SELECT LAST_INSERT_ID()
      ";
      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      newRecipe.Id = id;
      return newRecipe;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }


  }
}