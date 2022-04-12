using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Contracted.Models;
using Contracted.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contracted.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ContractorsController : ControllerBase
  {
    private readonly ContractorsService _contractorsService;
    private readonly BuildersService _buildersService;

    public ContractorsController(ContractorsService contractorsService, BuildersService buildersService)
    {
      _contractorsService = contractorsService;
      _buildersService = buildersService;
    }

    [HttpGet("{id}/builders")]
    public ActionResult<List<BuilderViewModel>> GetContractorsBuilders(int id)
    {
      try
      {
        List<BuilderViewModel> builders = _buildersService.GetContractorsBuilders(id);
        return Ok(builders);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Contractor>> GetAll()
    {
      try
      {
        List<Contractor> contractors = _contractorsService.GetAll();
        return Ok(contractors);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Contractor> GetById(int id)
    {
      try
      {
        Contractor contractor = _contractorsService.GetById(id);
        return Ok(contractor);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Contractor>> Create([FromBody] Contractor contractorData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Contractor contractor = _contractorsService.Create(userInfo.Id, contractorData);
        return Created($"api/contractors/{contractor.Id}", contractor);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Contractor>> Update([FromBody] Contractor updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        Contractor update = _contractorsService.Edit(userInfo.Id, updateData);
        return Created($"api/contractors/{update.Id}", update);
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
        _contractorsService.Delete(userInfo.Id, id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}