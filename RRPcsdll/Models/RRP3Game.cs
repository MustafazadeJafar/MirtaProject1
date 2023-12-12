using RRPcsdll.Exceptions;

namespace RRPcsdll.Models;

internal class RRP3Game
{
    int _playerCount, _playerTurn, _gameEvents;
    int[] _cardParams;
    RRP3Player[] _players;
    public enum Menu
    {
        NotReady,
        MainMenu,
        VictimMenu,
        CardMenu
    }
    public enum GameEvent
    {
        isBackwords
    }


    public RRP3Game(int playerCount = 2)
    {
        //this._cardParams = new int[Enum.GetValues<Card>().Length];
        //this._cardParams[(int)Card.Blank] = 0;
        //this._cardParams[(int)Card.P2Bullets] = 1;
        //this._cardParams[(int)Card.CurseNoAim] = 1;
        //this._cardParams[(int)Card.GainHelmet] = 1;
        //this._cardParams[(int)Card.Reverse] = 1;

        this.PlayerCount = playerCount;
    }

    public int PlayerCount 
    {
        get => this._playerCount;
        init
        {
            if (value < 2) value = 2;
            this._playerCount = value;
            this._players = new RRP3Player[value];

            for (int i = 0; i < value; i++)
            {
                this._players[i] = new RRP3Player();
                
            }
        } 
    }
    public int PlayerTurn
    {
        get => this._playerTurn;
        set
        {
            int step = 1; int index = 0;
            if (this.isGameEvent(GameEvent.isBackwords)) step = -step;
            for (int i = 0; i < this.PlayerCount; i++)
            {
                this._playerTurn += step;
                if (this._playerTurn >= this.PlayerCount) this._playerTurn = 0;
                else if (this._playerTurn < 0) this._playerTurn = this.PlayerCount - 1;

                

            }
            throw new RRP3GameOverException((index - 1).ToString());
        }
    }
    // Getter
    public bool isGameEvent(GameEvent gameEvent) => (this._gameEvents & (1 << (int)gameEvent)) > 0;
    // Setter
    public void isGameEvent(GameEvent gameEvent, bool change) 
    {
        if (change) this._gameEvents |= (1 << (int)gameEvent);
        else this._gameEvents &= ~(1 << (int)gameEvent);
    }
}
