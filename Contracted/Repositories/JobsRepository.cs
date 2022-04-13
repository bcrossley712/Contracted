using System;
using System.Collections.Generic;
using System.Data;
using Contracted.Interfaces;
using Contracted.Models;
using Dapper;

namespace Contracted.Repositories
{
  public class JobsRepository : IRepository<Job, int>
  {
    private readonly IDbConnection _db;

    public JobsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Job Create(Job data)
    {
      string sql = @"
      INSERT INTO jobs
      (contractorId, builderId)
      VALUES
      (@ContractorId, @BuilderId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    public void Edit(Job data)
    {
      throw new NotImplementedException();
    }

    public List<Job> GetAll()
    {
      throw new NotImplementedException();
    }

    public Job GetById(int id)
    {
      string sql = @"
      SELECT * 
      FROM jobs j
      WHERE j.id = @id;";
      return _db.QueryFirstOrDefault<Job>(sql, new { id });
    }
  }
}