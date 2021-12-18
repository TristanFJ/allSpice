using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Services;
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



  }
}