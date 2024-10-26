using System;

public class RandomBoolGenerator
{
    private static Random random = new Random();

    public static bool GetRandomBool()
    {
        return random.Next(0, 2) == 0;
    }
}
