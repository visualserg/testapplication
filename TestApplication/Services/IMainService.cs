using System.Collections.Generic;
using TestApplication.Classes;
using TestApplication.Models;

namespace TestApplication.Services
{
    public interface IMainService
    {
        IEnumerable<Suggest> Suggest(SuggestRequest input);
    }
}
