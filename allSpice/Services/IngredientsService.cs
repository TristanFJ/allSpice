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


    internal Ingredient Update(Ingredient updatedIngredient)
    {
      Ingredient oldIngredient = Get(updatedIngredient.Id);
      updatedIngredient.Name = updatedIngredient.Name != null ? updatedIngredient.Name : oldIngredient.Name;
      updatedIngredient.Quantity = updatedIngredient.Quantity != null ? updatedIngredient.Quantity : oldIngredient.Quantity;
      updatedIngredient.RecipeId = updatedIngredient.RecipeId != 0 ? updatedIngredient.RecipeId : oldIngredient.RecipeId;
      return _repo.Update(updatedIngredient);
    }


  }
}