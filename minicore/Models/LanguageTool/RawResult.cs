using System;
namespace minicore.Models.LanguageTool
{
    public class RawResult
    {
        public Software Software { get; set; }
        public IDictionary<string, dynamic> Warnings {get;set;}
        public Language Language { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<IEnumerable<int>> SentenceRanges { get; set; }

    }
}

