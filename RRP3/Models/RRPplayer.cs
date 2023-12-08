using RRP3.Exceptions;

namespace RRP3.Models;

internal class RRPplayer
{
    public enum Card
    {
        Blank,
        P2Bullets,
        CurseNoAim,
        GainHelmet,
        Reverse
    }
    public enum Status
    {
        HaveHelmet,
        CursedNoAim
    }
    public enum PlayerData
    {
        HitPoints,
        Coins,
        Status
    }

    internal List<(Card card, int freq)> _prob = new List<(Card card, int freq)>()
    {
        (Card.P2Bullets, 1),(Card.CurseNoAim, 1),(Card.GainHelmet, 1),(Card.Reverse, 1)
    };

    int[] _datas = new int[3];
    Card[] _deck;

    public RRPplayer(int cardCount = 3, int initialHitPoints = 1, int initialCoins = 2)
    {
        if (cardCount < 0) cardCount = 0;
        if (initialHitPoints < 1) throw new RRP3PlayerInvalidHitPointsException();

        this.HitPoints = initialHitPoints;
        this.Coins = initialCoins;
        this._deck = new Card[cardCount];
        this.UpdateDeck(true);
    }

    public int HitPoints 
    {
        get => this._datas[(int)PlayerData.HitPoints];
        set
        {
            this._datas[(int)PlayerData.HitPoints] = value;
        }
    }
    public int Coins 
    {
        get => this._datas[(int)PlayerData.Coins];
        set
        {
            this._datas[(int)PlayerData.Coins] = value;
        }
    }


    public void UpdateDeck(bool firstTime = false)
    {
        for (int i = 0; i < this._deck.Length; i++)
        {
            if (firstTime || this._deck[i] != Card.Blank)
            {
                this._deck[i] = RRP3Tools.Randi(this._prob);
            }
        }
    }
    public bool StatusIs(Status status)
    {
        if (!Enum.IsDefined<Status>(status)) throw new RRP3InvalidEnumException();
        if ((this._datas[(int)PlayerData.Status] & (1 << (int)status)) == 0) return false;
        return true;
    }
    public void StatusIs(Status status, bool change)
    {
        if (!Enum.IsDefined<Status>(status)) throw new RRP3InvalidEnumException();
        if (change) this._datas[(int)PlayerData.Status] |= (1 << (int)status);
        else this._datas[(int)PlayerData.Status] &= ~(int)status;
    }
    public Card Deck(int index)
    {
        if (!(index < this._deck.Length && index >= 0)) throw new RRP3IndexOutOfBorderException();
        return this._deck[index];
    }
    public void Deck(int index, Card card)
    {
        if (this.Deck(index) != null) this._deck[index] = card;
    }
}
