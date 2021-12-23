using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

namespace allSpice.Services
{
  public class AccountService
  {
    private readonly AccountsRepository _repo;
    public AccountService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }

    internal FavoriteViewModel GetFavoriteById(int id)
    {
      FavoriteViewModel favorite = _repo.GetFavoriteById(id);
      if (favorite == null)
      {
        throw new System.Exception("Invalid Favorite Id");
      }
      return favorite;
    }

    internal List<FavoriteViewModel> GetMyFavorites(string id)
    {
      return _repo.GetMyFavorites(id);
    }

    internal FavoriteViewModel Create(FavoriteViewModel newFavorite)
    {
      return _repo.Create(newFavorite);
    }


    internal void Remove(int id, string userId)
    {
      FavoriteViewModel favorite = GetFavoriteById(id);
      if (favorite.CreatorId != userId)
      {
        throw new System.Exception("access denied");
      }
      _repo.Remove(id);
    }



  }
}