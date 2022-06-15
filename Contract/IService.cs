using ReckonStringMatching.Models;
using System.Threading.Tasks;

namespace ReckonStringMatching.Contract
{
    public interface IService
    {
        Task<SearchResultModel> RunSearchTask();
    }
}
