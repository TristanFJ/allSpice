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

    internal List<Ingredient> GetByRecipe(int id)
    {
      return _repo.GetByRecipe(id);
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      return _repo.Create(newIngredient);
    }

    internal Ingredient Update(Ingredient updatedIngredient, string userId)
    {
      Ingredient oldIngredient = Get(updatedIngredient.Id);
      if (oldIngredient.CreatorId != userId)
      {
        throw new Exception("access denied");
      }
      updatedIngredient.Name = updatedIngredient.Name != null ? updatedIngredient.Name : oldIngredient.Name;
      updatedIngredient.Quantity = updatedIngredient.Quantity != null ? updatedIngredient.Quantity : oldIngredient.Quantity;
      updatedIngredient.RecipeId = updatedIngredient.RecipeId != 0 ? updatedIngredient.RecipeId : oldIngredient.RecipeId;
      updatedIngredient.CreatorId = updatedIngredient.CreatorId != null ? updatedIngredient.CreatorId : oldIngredient.CreatorId;
      return _repo.Update(updatedIngredient);
    }

    internal void Remove(int id, string userId)
    {
      Ingredient ingredient = Get(id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("access denied");
      }
      _repo.Remove(id);
    }

  }
}