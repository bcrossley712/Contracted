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

    public JobsService(JobsRepository jobsRepo)
    {
      _jobsRepo = jobsRepo;
    }

    public Job Create(string userId, Job data)
    {
      throw new NotImplementedException();
    }

    public void Delete(string userId, int id)
    {
      throw new NotImplementedException();
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
      throw new NotImplementedException();
    }
  }
}