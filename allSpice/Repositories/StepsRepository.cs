using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allSpice.Models;
using Dapper;

namespace allSpice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Step> Get()
    {
      string sql = "SELECT * FROM steps";
      return _db.Query<Step>(sql).ToList();
    }

    internal Step Get(int id)
    {
      string sql = "SELECT * FROM steps WHERE id = @id;";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
    }

    internal Step Create(Step newStep)
    {
      string sql = @"
      INSERT INTO steps
      (sequence, body, recipeId, creatorId)
      VALUES
      (@Sequence, @Body, @RecipeId, @CreatorId);
      SELECT LAST_INSERT_ID()
      ";
      int id = _db.ExecuteScalar<int>(sql, newStep);
      newStep.Id = id;
      return newStep;
    }


    internal Step Update(Step updatedStep)
    {
      string sql = @"
      UPDATE steps
      SET
      sequence = @Sequence,
      body = @Body,
      recipeId = @RecipeId
      WHERE id = @Id;
      ";
      int rows = _db.Execute(sql, updatedStep);
      if (rows <= 0)
      {
        throw new Exception("Update failed.");
      }
      return updatedStep;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }


  }
}