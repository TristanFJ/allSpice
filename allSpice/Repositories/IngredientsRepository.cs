using System.Collections.Generic;
using System.Data;
using System.Linq;
using allSpice.Models;
using Dapper;

namespace allSpice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Ingredient> Get()
    {
      string sql = "SELECT * FROM ingredients";
      return _db.Query<Ingredient>(sql).ToList();
    }

    internal Ingredient Get(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE id = @id;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      VALUES
      (@Name, @Quantity, @RecipeId);
      SELECT LAST_INSERT_ID()
      ";
      int id = _db.ExecuteScalar<int>(sql, newIngredient);
      newIngredient.Id = id;
      return newIngredient;
    }


  }
}