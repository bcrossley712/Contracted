using System;
using System.Collections.Generic;
using Contracted.Services;
using Microsoft.AspNetCore.Mvc;
using Contracted.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace Contracted.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class BuildersController : ControllerBase
  {
    private readonly BuildersService _buildersService;
    private readonly ContractorsService _contractorsService;

    public BuildersController(BuildersService buildersService, ContractorsService contractorsService)
    {
      _buildersService = buildersService;
      _contractorsService = contractorsService;
    }

    [HttpGet("{id}/contractors")]
    public ActionResult<List<BuilderViewModel>> GetBuildersContractors(int id)
    {
      try
      {
        List<BuilderViewModel> contractors = _buildersService.GetBuildersContractors(id);
        return Ok(contractors);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Builder>> GetAll()
    {
      try
      {
        List<Builder> builders = _buildersService.GetAll();
        return Ok(builders);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Builder> GetById(int id)
    {
      try
      {
        Builder builder = _buildersService.GetById(id);
        return Ok(builder);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Builder>> Create([FromBody] Builder builderData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Builder builder = _buildersService.Create(userInfo.Id, builderData);
        return Created($"api/builders/{builder.Id}", builder);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Builder>> Update([FromBody] Builder updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        Builder update = _buildersService.Edit(userInfo.Id, updateData);
        return Created($"api/builders/{update.Id}", update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _buildersService.Delete(userInfo.Id, id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}