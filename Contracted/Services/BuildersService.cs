using System;
using System.Collections.Generic;
using Contracted.Interfaces;
using Contracted.Repositories;
using Contracted.Models;

namespace Contracted.Services
{
  public class BuildersService : IService<Builder>
  {
    private readonly BuildersRepository _buildersRepo;

    public BuildersService(BuildersRepository buildersRepo)
    {
      _buildersRepo = buildersRepo;
    }

    public List<Builder> GetAll()
    {
      return _buildersRepo.GetAll();
    }

    public Builder GetById(int id)
    {
      Builder found = _buildersRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Builder Id");
      }
      return found;
    }

    public List<BuilderViewModel> GetContractorsBuilders(int builderId)
    {
      return _buildersRepo.GetContractorsBuilders(builderId);
    }
    public Builder Create(string userId, Builder data)
    {
      data.CreatorId = userId;
      return _buildersRepo.Create(data);

    }
    public Builder Edit(string userId, Builder data)
    {
      Builder original = GetById(data.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You cannot modify this Builder");
      }
      original.Name = data.Name ?? original.Name;
      original.Location = data.Location ?? original.Location;
      original.Owner = data.Owner ?? original.Owner;
      _buildersRepo.Edit(original);
      return GetById(original.Id);
    }
    public void Delete(string userId, int id)
    {
      Builder found = GetById(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("You cannot delete this Builder");
      }
      _buildersRepo.Delete(id);
    }
  }
}