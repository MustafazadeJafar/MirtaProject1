namespace RRPcsdll.DataModels;

public class PlayerData
{
    public int Id { get; set; }
    public int GameDataId { get; set; }

    // Structure //
    public int HitPoints { get; set; }
    public int Coins { get; set; }
    public int Status { get; set; }

    // Reletaion //
    public GameData? GameData { get; set; }
    public IEnumerable<PlayerCardData>? PlayerCardDatas { get; set; }
}
