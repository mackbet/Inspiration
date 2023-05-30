using System;

public static class RandomHelper
{
    private static Random random = new Random();

    public static void SetSeed(int seed)
    {
        random = new Random(seed);
    }
    public static int GetRandomInt(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    public static float GetRandomFloat(float minValue, float maxValue)
    {
        return (float)random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
