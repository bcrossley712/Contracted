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

    internal List<ContractorViewModel> GetContractorsBuilders(int contractorId)
    {
      string sql = @"
      SELECT
        j.*,
        c.*,
        b.*
      FROM jobs j
      JOIN contractors c ON c.id = j.contractorId
      JOIN builders b ON b.id = j.builderId
      WHERE j.contractorId = @contractorId;
      ";
      return _db.Query<Job, ContractorViewModel, Builder, ContractorViewModel>(sql, (j, c, b) =>
      {
        c.JobId = j.Id;
        c.BuilderName = b.Name;
        c.Location = b.Location;
        return c;
      }, new { contractorId }).ToList();
    }

    public Contractor GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM contractors
      WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Contractor>(sql, new { id });
    }
    public Contractor Create(Contractor data)
    {
      string sql = @"
      INSERT INTO contractors
      (name, pricePerHour, skill, creatorId)
      VALUES
      (@name, @pricePerHour, @skill, @creatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public void Edit(Contractor data)
    {
      string sql = @"
      UPDATE contractors
      SET
      name = @Name, 
      pricePerHour = @PricePerHour, 
      skill = @Skill
      WHERE id = @Id;";
      _db.Execute(sql, data);
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}