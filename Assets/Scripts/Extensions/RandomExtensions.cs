using System;

public static class RandomExtensions
{
    public static double Range(this Random random, double minimum, double maximum)
    {
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}
