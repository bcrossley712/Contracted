using System.Collections.Generic;

namespace Contracted.Interfaces
{
  public interface IService<T>
  {
    List<T> GetAll();
    T GetById(int id);
    T Create(string userId, T data);
    T Edit(string userId, T data);
    void Delete(string userId, int id);

  }
}