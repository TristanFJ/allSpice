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


    // ANCHOR Stopping here for now. Getting error on create 
    // Cannot add or update a child row: a foreign key constraint fails (`TristanDevDB`.`recipes`, CONSTRAINT `recipes_ibfk_1` FOREIGN KEY (`creatorId`) REFERENCES `accounts` (`id`) ON DELETE CASCADE)

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
  }
}