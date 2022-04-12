using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Contracted.Interfaces;
using Contracted.Models;
using Dapper;

namespace Contracted.Repositories
{
  public class BuildersRepository : IRepository<Builder, int>
  {
    private readonly IDbConnection _db;

    public BuildersRepository(IDbConnection db)
    {
      _db = db;
    }


    public List<Builder> GetAll()
    {
      string sql = @"
      SELECT * FROM builders
      ";
      return _db.Query<Builder>(sql).ToList();
    }

    internal List<BuilderViewModel> GetContractorsBuilders(int builderId)
    {
      throw new NotImplementedException();
    }

    public Builder GetById(int id)
    {
      throw new NotImplementedException();
    }
    public Builder Create(Builder data)
    {
      string sql = @"
      INSERT INTO builders
      (name, owner, location, creatorId)
      VALUES
      (@name, @owner, @location, @creatorId);
      SELECT LAST_INSERT_ID;";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Edit(Builder data)
    {
      throw new NotImplementedException();
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM builders WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}