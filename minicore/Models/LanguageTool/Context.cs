using System;
namespace minicore.Models.LanguageTool
{
	public class Context
	{
        public string Text { get; set; }

        public long Offset { get; set; }

        public long Length { get; set; }
    }
}

