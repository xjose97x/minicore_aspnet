using System;
namespace minicore.Models.LanguageTool
{
	public class DetectedLanguage
	{
        public string Name { get; set; }

        public string Code { get; set; }

        public double Confidence { get; set; }

        public string Source { get; set; }
    }
}

