using System;
using minicore.Models;

namespace minicore.Interfaces
{
	public interface ILanguageService
    {
        float FleschKinkaidScore(string text);
        Task<LanguageToolResult> LanguageTool(string text);
    }
}

