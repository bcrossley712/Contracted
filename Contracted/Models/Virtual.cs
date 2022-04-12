using System;
using Contracted.Interfaces;

namespace Contracted.Models
{
  public class Virtual<T> : IRepoItem<T>
  {
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}