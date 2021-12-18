using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Services;
using Microsoft.AspNetCore.Mvc;

namespace allSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ins;

    // removing RecipeService because I think I only need that "dual service" for the many-to-many

    public IngredientsController(IngredientsService ins)
    {
      _ins = ins;
    }

    // NOTE I don't know if I need to "Get all ingredients", or if I only need to get ingredients by recipeId. I probably want a GetAll for the main page? So that I can just get them all and display them according to each recipe component? 

    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> Get()
    {
      try
      {
        var ingredients = _ins.Get();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}