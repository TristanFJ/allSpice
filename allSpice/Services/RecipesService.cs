

using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

namespace allSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal List<Recipe> Get()
    {
      return _repo.GetAll();
    }

    internal Recipe Get(int id)
    {
      Recipe found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Recipe Create(Recipe newRecipe)
    {
      return _repo.Create(newRecipe);
    }

    internal void Remove(int id, string userId)
    {
      Recipe recipe = Get(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("access denied");
      }
      _repo.Remove(id);
    }
  }
}