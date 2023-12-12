namespace RRPcsdll.DataModels;

public class CardData
{
    public int Id { get; set; }
    public int GameDataId { get; set; }

    // Structure //
    public int IndexNumber { get; set; }
    public int Frequency { get; set; }

    // Reletaion //
    public GameData? GameData { get; set; }
    public IEnumerable<PlayerCardData>? PlayerCardDatas { get; set; }
}
