using System;
namespace minicore.Models.LanguageTool
{
	public class Match
	{
        public Rule? Rule { get; set; }

        public long Length { get; set; }

        public long Offset { get; set; }

        public string? Message { get; set; }

        public string[]? Replacements { get; set; }

        public string? ShortMessage { get; set; }
    }
}

