using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReckonStringMatching.Contract
{
    public interface IStringSearchAlgorithm
    {
        IEnumerable<ISearchMatch> Search(string toFind, string toSearch);
    }
}
