using System.Runtime.CompilerServices;

namespace RRPcsdll.Services;

public class RRP3Services
{
    public static int Rando(int limit)
    {
        Random rdm = new();
        return rdm.Next(limit);
    }
    public static int Randi(int[] probs)
    {
        int size = 0;

        foreach (int item in probs) size += item;
        int rdmvalue = Rando(size);

        size = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            size += probs[i];
            if (rdmvalue < size) return i;
        }

        return -1;
    }
    public static T Randi<T>(List<(int value, T key)> probs)
    {
        int result = Randi(probs.Select(p => p.value).ToArray());
        if (result >= 0) return probs[result].key;
        return default;
    }
}
