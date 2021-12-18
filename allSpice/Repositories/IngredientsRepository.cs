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
  }
}