using System;
namespace minicore.Models.LanguageTool
{
	public class Match
	{
        public Rule? Rule { get; set; }

        public long Length { get; set; }

        public long Offset { get; set; }

        public string? Message { get; set; }

        public IEnumerable<Replacement> Replacements { get; set; }

        public string? ShortMessage { get; set; }

        public Context Context { get; set; }

        public string Sentence { get; set; }

        public bool IgnoreForIncompleteSentence { get; set; }

        public long ContextForSureMatch { get; set; }
    }
}

