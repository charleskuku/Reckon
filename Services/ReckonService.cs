using ReckonStringMatching.Contract;
using ReckonStringMatching.Models;
using ReckonStringMatching.Services.Integration;
using ReckonStringMatching.Services.SearchAlgorithm;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ReckonStringMatching.Services
{
    public class ReckonService : IService
    {
        private readonly IntegrationService _integration;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        public ReckonService(IntegrationService integration)
        {
            _integration = integration;
        }

        public async Task<SearchResultModel> RunSearchTask()
        {
            var resultTTS = await _integration.GetTextToSearch(_cancellationTokenSource.Token);

            
            if (resultTTS is not null)
            {
                var resultST = await _integration.GetSubTexts(_cancellationTokenSource.Token);

                if(resultST is not null)
                {
                    var algoResult =  ProcessStringAlgorithm(resultST, resultTTS);
                    
                    if(algoResult is not null)
                    {
                       var postResult = await _integration.PostResults(algoResult, _cancellationTokenSource.Token);
                        postResult.StringSearchResult = algoResult;
                        return postResult;
                       
                    }
                }
            }
            return null;
        }

        private StringSearchModel ProcessStringAlgorithm(SubTextModel textToFind, TextToSearchModel textToSearch)
        {
            LinearStringSearchService linear = new();
            StringSearchModel modelSearch = new();
            modelSearch.Results = new();

            modelSearch.Text = textToSearch.Text;

            foreach(var subText in textToFind.Subtexts)
            {
                SubResultModel subResultModel = new();
                subResultModel.SubText = subText;

                List<string> output = new List<string>();
                foreach (var item in linear.Search(subText, textToSearch.Text))
                {
                    output.Add(item.Start.ToString());
                    subResultModel.Result = string.Join(",", output);
                }

                if(subResultModel.Result is null)
                {
                    subResultModel.Result = "<No Output>";
                    
                }

                modelSearch.Results.Add(new SubResultModel 
                { 
                    SubText = subResultModel.SubText, 
                    Result = subResultModel.Result
                });
                
            }

            return modelSearch;
        }
    }
}
