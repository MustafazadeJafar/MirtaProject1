using RRP3.Models;
using static RRP3.Models.RRPgame;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRP3.Services;

public static class RRP3service
{
    static RRPgame _game;

    public static void Create(int number = 2)
    {
        RRP3service._game = new RRPgame(number);
        
    }

    public static void ConsoleRun(bool devmod = false)
    {
        int IInput;
        int result = 0;

        Console.Write("Give the number of players: ");
        Int32.TryParse(Console.ReadLine(), out IInput);
        RRP3service.Create(IInput);

        do
        {
            Console.WriteLine("\nTurn of Player " + (RRP3service._game.PlayerTurn + 1));
            if (devmod)
            {
                for (int i = 0; i < RRP3service._game.MaxBullets; i++)
                {
                    Console.Write(RRP3service._game.Baraban(i) ? (1) : (0));
                }
                Console.WriteLine("-" + RRP3service._game.BulletTurn);
            }
            switch ((Menu)RRP3service._game.CurrentMenu)
            {
                case Menu.MainMenu:
                    Console.WriteLine("Coins: " + RRP3service._game.Player(RRP3service._game.PlayerTurn).Coins);
                    Console.WriteLine("1. Shot self");
                    Console.WriteLine("2. Shot someone");
                    Console.WriteLine("3. Use card");
                    Console.WriteLine("4. Update card");

                    break;
                case Menu.VictimMenu:
                    for (int i = 0; i < RRP3service._game.PlayerCount; i++)
                    {
                        Console.WriteLine(i + ". Shot player " + (i + 1));
                    }

                    break;
                case Menu.CardMenu:
                    for (int i = 0; i < RRP3service._game.MaxCards; i++)
                    {
                        Console.WriteLine((i + 1) + ". " +
                            RRP3service._game.Player(RRP3service._game.PlayerTurn).Deck(i));
                    }

                    break;
            }
            Console.Write("Your input: ");
            Int32.TryParse(Console.ReadLine(), out IInput);
            result = RRP3service._game.GameIO(IInput - 1);
            if (result != 0) Console.WriteLine("Bang!");
        } while (result >= 0);

        Console.WriteLine("\nAnd the winner is.... Player " + (-result));
    }

    public static bool LoadGame(GameDatas gdatas, PlayerDatas[] pdatas, List<DeckDatas[]> pdecks)
    {
        
        return true;
    }

    public static (GameDatas gdatas, PlayerDatas[] pdatas, List<DeckDatas[]> pdecks) SaveGame()
    {
        return new();
    }
}

public record GameDatas
{
    public int ID { get; set; }


    [Required, StringLength(7), DataType("VARCHAR")]
    public string RoomCode { get; set; }

    [Required, Range(2,97)]
    public int PlayerCount { get; set; }

    [Required]
    public int MyProperty { get; set; }


}
public record PlayerDatas
{
    public int ID { get; set; }


    public IEnumerable<DeckDatas>? DecksDatas { get; set; }
}
public record DeckDatas
{
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    public int PlayerID { get; set; }
    public PlayerDatas? PlayerDatas { get; set; }
}