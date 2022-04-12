namespace Contracted.Models
{
  public class Builder : Virtual<int>
  {
    public string Name { get; set; }
    public string Owner { get; set; }
    public string Location { get; set; }
    public string CreatorId { get; set; }
  }

  public class BuilderViewModel : Builder
  {
    public int JobId { get; set; }
    public string ContractorName { get; set; }
    public int PricePerHour { get; set; }
    public string Skill { get; set; }
  }
}