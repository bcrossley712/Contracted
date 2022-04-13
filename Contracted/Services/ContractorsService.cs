using System;
using System.Collections.Generic;
using Contracted.Interfaces;
using Contracted.Models;
using Contracted.Repositories;

namespace Contracted.Services
{
  public class ContractorsService : IService<Contractor>
  {
    private readonly ContractorsRepository _contractorsRepo;

    public ContractorsService(ContractorsRepository contractorsRepo)
    {
      _contractorsRepo = contractorsRepo;
    }

    public List<Contractor> GetAll()
    {
      return _contractorsRepo.GetAll();
    }

    public Contractor GetById(int id)
    {
      Contractor found = _contractorsRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Contractor Id");
      }
      return found;
    }

    public List<ContractorViewModel> GetContractorsBuilders(int contractorId)
    {
      return _contractorsRepo.GetContractorsBuilders(contractorId);
    }
    public Contractor Create(string userId, Contractor data)
    {
      data.CreatorId = userId;
      return _contractorsRepo.Create(data);

    }
    public Contractor Edit(string userId, Contractor data)
    {
      Contractor original = GetById(data.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You cannot modify this contractor");
      }
      original.Name = data.Name ?? original.Name;
      original.Skill = data.Skill ?? original.Skill;
      original.PricePerHour = data.PricePerHour >= 0 ? data.PricePerHour : original.PricePerHour;
      _contractorsRepo.Edit(original);
      return GetById(original.Id);
    }
    public void Delete(string userId, int id)
    {
      Contractor found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You cannot delete this contractor");
      }
      _contractorsRepo.Delete(id);
    }


  }
}