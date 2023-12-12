using System.Numerics;

namespace Front2;

internal class Program
{
    public enum Card
    {
        Blank,
        P2Bullets,
        CurseNoAim,
        GainHelmet,
        Reverse
    }
    static void Main(string[] args)
    {
        List<(int, string)> list =
        [
            (1, "f1"),
            (1, "f2"),
            (1, "f3"),
            (1, "f4")
        ];

        string gg1 = "12,13;14,165;2,4444";
        string[] ggs = gg1.Split(';');
        foreach (string gg in ggs)
        {
            foreach(string g in gg.Split(','))
            {
                Console.Write(g + "_");
            }
            Console.WriteLine();
        }
    }
}
