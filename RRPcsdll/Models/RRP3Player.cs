using RRPcsdll.Services;

namespace RRPcsdll.Models;

internal class RRP3Player
{
    int _hitPoints, _coins, _status;
    //RRP3Game.Card[] _deck;

    //public RRP3Player(int cardCount = 3)
    //{
    //    this._deck = new RRP3Game.Card[cardCount];
    //}

    public enum Status
    {
        HaveHelmet,
        CursedNoAim
    }
    public int HitPoints 
    {
        get => this._hitPoints;
        set
        {
            this._hitPoints = value;
        }
    }
    public int Coins
    {
        get => this._coins;
        set
        {
            this._coins = value;
        }
    }
    //// Getter
    //public Card Deck(int index)
    //{
    //    if (index < 0 || index >= this._deck.Length) return Card.Blank; //
    //    else return this._deck[index];
    //}
    public bool isStatus(Status status) => (this._status & (1 << (int)status)) > 0;
    //// Setter
    //public void Deck(int index, Card card)
    //{
    //    if (index < 0 || index >= this._deck.Length) return; //
    //    else this._deck[index] = card;
    //}
    public void isStatus(Status status, bool change)
    {
        if (change) this._status |= (1 << (int)status);
        else this._status &= ~(1 << (int)status);
    }

}
