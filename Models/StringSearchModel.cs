using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReckonStringMatching.Models
{
    public class StringSearchModel
    {
        public string Candidate { get; set; } = "Charles Kuku Mendoza";

        public string Text { get; set; }

        public List<SubResultModel> Results { get; set; }
    }

    public class SubResultModel
    {
        public string SubText { get; set; }
        public string Result { get; set; }
    }
}
