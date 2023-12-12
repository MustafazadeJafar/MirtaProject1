namespace RRPcsdll.DataModels;

public class PlayerCardData
{
    public int Id { get; set; }
    public int PlayerDataId { get; set; }
    public int GameCardDataId { get; set; }

    // Reletaion //
    public PlayerData? PlayerData { get; set; }
    public CardData? CardData { get; set; }
}
