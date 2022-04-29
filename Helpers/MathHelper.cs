namespace Helpers;

public static class MathHelper
{
    public static bool IsInBounds(int x, int xEnd, int y, int yEnd)
    {
        return new Range(0, xEnd).Contains(x) && new Range(0, yEnd).Contains(y);
    }
}

public record Range(int Start, int End)
{
    public bool Contains(int value)
    {
        return value >= Start && value < End;
    }
};