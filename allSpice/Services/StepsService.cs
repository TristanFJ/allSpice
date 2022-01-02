

using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

namespace allSpice.Services
{
  public class StepsService
  {
    private readonly StepsRepository _repo;

    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Step> Get()
    {
      return _repo.Get();
    }

    internal Step Get(int id)
    {
      Step found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }
    internal List<Step> GetByRecipe(int id)
    {
      return _repo.GetByRecipe(id);
    }

    internal Step Create(Step newStep)
    {
      return _repo.Create(newStep);
    }

    internal Step Update(Step updatedStep, string userId)
    {
      Step oldStep = Get(updatedStep.Id);
      if (oldStep.CreatorId != userId)
      {
        throw new Exception("access denied");
      }

      updatedStep.Body = updatedStep.Body != null ? updatedStep.Body : oldStep.Body;
      updatedStep.Sequence = updatedStep.Sequence != 0 ? updatedStep.Sequence : oldStep.Sequence;
      updatedStep.RecipeId = updatedStep.RecipeId != 0 ? updatedStep.RecipeId : oldStep.RecipeId;
      updatedStep.CreatorId = updatedStep.CreatorId != null ? updatedStep.CreatorId : oldStep.CreatorId;
      return _repo.Update(updatedStep);
    }

    internal void Remove(int id, string userId)
    {
      Step step = Get(id);
      if (step.CreatorId != userId)
      {
        throw new Exception("access denied");
      }
      _repo.Remove(id);
    }

  }
}