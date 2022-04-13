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

    internal List<BuilderViewModel> GetBuildersContractors(int builderId)
    {
      string sql = @"
      SELECT
        j.*,
        b.*,
        c.*
      FROM jobs j
      JOIN builders b ON b.id = j.builderId
      JOIN contractors c ON c.id = j.contractorId
      WHERE j.builderId = @builderId;
      ";
      return _db.Query<Job, BuilderViewModel, Contractor, BuilderViewModel>(sql, (j, b, c) =>
      {
        b.JobId = j.Id;
        b.ContractorName = c.Name;
        b.PricePerHour = c.PricePerHour;
        b.Skill = c.Skill;
        return b;
      }, new { builderId }).ToList();
    }

    public Builder GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM builders
      WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Builder>(sql, new { id });
    }
    public Builder Create(Builder data)
    {
      string sql = @"
      INSERT INTO builders
      (name, owner, location, creatorId)
      VALUES
      (@name, @owner, @location, @creatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Edit(Builder data)
    {
      string sql = @"
      UPDATE builders
      SET
      name = @Name, 
      owner = @Owner, 
      location = @Location
      WHERE id = @Id;";
      _db.Execute(sql, data);
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM builders WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}