namespace RRPcsdll.Models;

internal class RRPgame
{
    int _playerCount;


    public int PlayerCount 
    {
        get => this._playerCount;
        set
        {
            if (value < 2) value = 2;
            this._playerCount = value;
        } 
    }
    public int PlayerCount
    {
        get => this._playerCount;
        set
        {
            if (value < 2) value = 2;
            this._playerCount = value;
        }
    }
}
