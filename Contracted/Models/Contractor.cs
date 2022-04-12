namespace Contracted.Models
{
  public class Contractor : Virtual<int>
  {
    public string Name { get; set; }
    public int PricePerHour { get; set; }
    public string Skill { get; set; }
    public string CreatorId { get; set; }

  }
  public class ContractorViewModel : Contractor
  {
    public int JobId { get; set; }
    public string Location { get; set; }
    public string BuilderName { get; set; }
  }
}