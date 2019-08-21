using System.Collections.Generic;
using TestApplication.Models;

namespace TestApplication.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindByInput(string input, int suggestCount);
    }
}
