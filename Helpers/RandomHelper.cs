namespace Helpers;

public static class RandomHelper
{
    public static T Sample<T>(Random random, List<T> list) => list[random.Next(list.Count)];
}