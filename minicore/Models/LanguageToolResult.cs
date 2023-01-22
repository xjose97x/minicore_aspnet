using System;
using minicore.Models.LanguageTool;

namespace minicore.Models
{
	public class LanguageToolResult
	{
		public IEnumerable<Match> Matches { get; set; }
		public string? Original { get; set; }
		public string? Autofix { get; set; }
	}
}

