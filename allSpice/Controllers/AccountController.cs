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
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("favorites")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<FavoriteViewModel>>> GetFavoritesAsync(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<FavoriteViewModel> favorites = _accountService.GetMyFavorites(userInfo.Id);
        return Ok(favorites);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("favorites")]
    [Authorize]
    public async Task<ActionResult<FavoriteViewModel>> Create([FromBody] FavoriteViewModel newFavorite)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newFavorite.CreatorId = userInfo.Id;
        FavoriteViewModel favorite = _accountService.Create(newFavorite);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("favorites/{id}")]
    [Authorize]
    public async Task<ActionResult<FavoriteViewModel>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _accountService.Remove(id, userInfo.Id);
        return Ok("Deleted successfully");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



  }
}