using System;
namespace minicore.Models.LanguageTool
{
	public class Language
	{
        public string Name { get; set; }

        public string Code { get; set; }

        public DetectedLanguage DetectedLanguage { get; set; }
    }
}
