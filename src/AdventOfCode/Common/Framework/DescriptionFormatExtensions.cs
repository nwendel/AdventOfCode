namespace AdventOfCode.Common.Framework;

public static class DescriptionFormatExtensions
{
    extension(DescriptionFormat format)
    {
        public string ToFileExtension()
            => format switch
            {
                DescriptionFormat.Html => "html",
                DescriptionFormat.Markdown => "md",
            };
    }
}
