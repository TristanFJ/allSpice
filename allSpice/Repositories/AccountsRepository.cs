using System.Collections.Generic;
using System.Data;
using System.Linq;
using allSpice.Models;
using Dapper;

namespace allSpice.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
      INSERT INTO accounts
      (name, picture, email, id)
      VALUES
      (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
      UPDATE accounts
      SET 
        name = @Name,
        picture = @Picture
      WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }

    internal FavoriteViewModel GetFavoriteById(int id)
    {
      string sql = "SELECT * FROM favorites WHERE id = @Id";
      return _db.QueryFirstOrDefault<FavoriteViewModel>(sql, new { id });
    }

    internal List<FavoriteViewModel> GetMyFavorites(string creatorId)
    {
      string sql = @"
      SELECT
        recipe.*,
        favorite.favoriteId AS favoriteId
      FROM favorites favorite
      JOIN recipes recipe ON recipe.id = favorite.id
      WHERE favorite.creatorId = @creatorId;
";
      return _db.Query<FavoriteViewModel>(sql, new { creatorId }).ToList();
    }



    // NOTE the response from this create has null values for the recipe, but when I get it it populates. Not sure if that will create an issue. 
    internal FavoriteViewModel Create(FavoriteViewModel newFavorite)
    {
      string sql = @"
      INSERT INTO favorites
      (creatorId, id)
      VALUES
      (@CreatorId, @Id);
      SELECT LAST_INSERT_ID()
  ";
      int id = _db.ExecuteScalar<int>(sql, newFavorite);
      newFavorite.FavoriteId = id;
      return newFavorite;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM favorites WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}
