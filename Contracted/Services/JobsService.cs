using System;
using System.Collections.Generic;
using Contracted.Interfaces;
using Contracted.Models;
using Contracted.Repositories;

namespace Contracted.Services
{
  public class JobsService : IService<Job>
  {
    private readonly JobsRepository _jobsRepo;
    private readonly ContractorsService _contractorsService;
    private readonly BuildersService _buildersService;

    public JobsService(JobsRepository jobsRepo, ContractorsService contractorsService, BuildersService buildersService)
    {
      _jobsRepo = jobsRepo;
      _contractorsService = contractorsService;
      _buildersService = buildersService;
    }

    public Job Create(string userId, Job data)
    {
      return _jobsRepo.Create(data);
    }

    public void Delete(string userId, int id)
    {
      Job foundJob = GetById(id);
      Builder foundBuilder = _buildersService.GetById(foundJob.BuilderId);
      Contractor foundContractor = _contractorsService.GetById(foundJob.ContractorId);
      if (userId != foundBuilder.CreatorId && userId != foundContractor.CreatorId)
      {
        throw new Exception("You cannot delete this job");
      }
      _jobsRepo.Delete(id);
    }

    public Job Edit(string userId, Job data)
    {
      throw new NotImplementedException();
    }

    public List<Job> GetAll()
    {
      throw new NotImplementedException();
    }

    public Job GetById(int id)
    {
      Job found = _jobsRepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Job Id");
      }
      return found;
    }
  }
}