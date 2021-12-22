using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using allSpice.Models;
using allSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StepsController : ControllerBase
  {
    private readonly StepsService _sts;

    public StepsController(StepsService sts)
    {
      _sts = sts;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Step>> Get()
    {
      try
      {
        var steps = _sts.Get();
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Step>> GetByRecipe(int id)
    {
      try
      {
        var steps = _sts.GetByRecipe(id);
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Step>> Create([FromBody] Step newStep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newStep.CreatorId = userInfo.Id;
        Step step = _sts.Create(newStep);
        return Ok(step);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Step> Create([FromBody] Step updatedStep, int id)
    {
      try
      {
        updatedStep.Id = id;
        Step step = _sts.Update(updatedStep);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Step>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _sts.Remove(id, userInfo.Id);
        return Ok("Step deleted successfully.");

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}