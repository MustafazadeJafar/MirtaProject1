namespace RRP3.Models;

public static class RRP3Tools
{
    public static int Randi(int limit)
    {
        Random rdm = new();
        return rdm.Next(limit);
    }
    public static T Randi<T>(List<(T item, int freq)> probs)
    {
        Random rdm = new();
        int size = 0;

        probs.ForEach(p => size += p.freq);
        int rdmvalue = rdm.Next(size);

        size = 0;

        for (int i = 0; i < probs.Count; i++)
        {
            size += probs[i].freq;
            if (rdmvalue < size) return probs[i].item;
        }

        return default;
    }
}

