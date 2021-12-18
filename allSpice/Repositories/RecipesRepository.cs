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
      string sql = @"
      SELECT
        recipe.*,
        account.*
      FROM recipes recipe
      JOIN accounts account on recipe.creatorId = account.id
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }, splitOn: "id").ToList();
    }
  }
}