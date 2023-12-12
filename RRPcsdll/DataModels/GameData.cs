namespace RRPcsdll.DataModels;

public class GameData
{
    public int Id { get; set; }

    // Sturucture //
    public int PlayerCount { get; set; }
    public int PlayerTurn { get; set; }
    public int Baraban { get; set; }
    public int BulletTurn { get; set; }
    public int GameEvents { get; set; }
    public int CurrentMenu { get; set; }
    public string SessionCsv { get; set; }

    // Params //
    public int MaxCards { get; set; }
    public int MaxBullets { get; set; }
    public int UsedCards { get; set; }
    public int UsedBullets { get; set; }

    // Releation //
    public IEnumerable<PlayerData>? PlayerDatas { get; set; }
    public IEnumerable<CardData>? CardDatas { get; set; }
}
