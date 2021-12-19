using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

namespace allSpice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _repo;

    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }

    internal List<Ingredient> Get()
    {
      return _repo.Get();
    }


    internal Ingredient Get(int id)
    {
      Ingredient found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      return _repo.Create(newIngredient);
    }


  }
}