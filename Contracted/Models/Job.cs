namespace Contracted.Models
{
  public class Job : Virtual<int>
  {
    public int ContractorID { get; set; }
    public int CompanyId { get; set; }

    
  }
}