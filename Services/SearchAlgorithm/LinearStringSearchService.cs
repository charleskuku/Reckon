using ReckonStringMatching.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReckonStringMatching.Services.SearchAlgorithm
{
    public class LinearStringSearchService : IStringSearchAlgorithm
    {
        public IEnumerable<ISearchMatch> Search(string toFind, string toSearch)
        {
            if (toFind == null)
            {
                throw new ArgumentNullException("toFind");
            }

            if (toSearch == null)
            {
                throw new ArgumentNullException("toSearch");
            }

            if (toFind.Length > 0 && toSearch.Length > 0)
            {
                for (int startIndex = 0; startIndex <= toSearch.Length - toFind.Length; startIndex++)
                {
                    int matchCount = 0;

                    //while (toFind[matchCount] == toSearch[startIndex + matchCount])
                    while (string.Equals(toFind[matchCount].ToString(), toSearch[startIndex + matchCount].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        matchCount++;

                        if (toFind.Length == matchCount)
                        {
                            yield return new StringSearchMatch(++startIndex, matchCount);

                            startIndex += matchCount - 1;
                            break;
                        }
                    }
                }
            }
        }
    }
}
