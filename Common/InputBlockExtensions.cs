namespace AdventOfCode.Common;

public static class InputBlockExtensions
{
    public static string Text(this InputBlock self)
        => self.Text.Trim();
}
