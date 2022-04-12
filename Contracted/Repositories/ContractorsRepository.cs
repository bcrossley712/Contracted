using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Contracted.Interfaces;
using Contracted.Models;
using Dapper;

namespace Contracted.Repositories
{
  public class ContractorsRepository : IRepository<Contractor, int>
  {
    private readonly IDbConnection _db;

    public ContractorsRepository(IDbConnection db)
    {
      _db = db;
    }


    public List<Contractor> GetAll()
    {
      string sql = @"
      SELECT * FROM contractors
      ";
      return _db.Query<Contractor>(sql).ToList();
    }

    internal List<ContractorViewModel> GetBuildersContractors(int builderId)
    {
      throw new NotImplementedException();
    }

    public Contractor GetById(int id)
    {
      throw new NotImplementedException();
    }
    public Contractor Create(Contractor data)
    {
      string sql = @"
      INSERT INTO contractors
      (name, pricePerHour, skill, creatorId)
      VALUES
      (@name, @pricePerHour, @skill, @creatorId);
      SELECT LAST_INSERT_ID;";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Edit(Contractor data)
    {
      throw new NotImplementedException();
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}