namespace Contracted.Models
{
  public class Job : Virtual<int>
  {
    public int ContractorId { get; set; }
    public int BuilderId { get; set; }


  }
}