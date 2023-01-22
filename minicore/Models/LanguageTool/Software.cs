using System;
namespace minicore.Models.LanguageTool
{
	public class Software
	{
        public string Name { get; set; }

        public string Version { get; set; }

        public string BuildDate { get; set; }

        public long ApiVersion { get; set; }

        public bool Premium { get; set; }

        public string PremiumHint { get; set; }

        public string Status { get; set; }
    }
}

