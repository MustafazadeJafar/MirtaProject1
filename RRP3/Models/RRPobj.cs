using RRP3.Exceptions;

namespace RRP3.Models;

public class RRPobj
{
    public enum Action
    {
        NotReady,
        MainMenu,
        VictimMenu,
        CardMenu
    }

    public RRPobj(int playerCount = 2)
    {
        if (playerCount < 2) this.PlayerCount = 2;
        else this.PlayerCount = playerCount;

        this.Players = new RRPobj.Player[this.PlayerCount];
        this.PlayerTurn = 0;
        this.BulletTurn = 0;
        this.Actions = 0;
        this.HasAction(Action.MainMenu, true);

        for (int i = 0; i < this.PlayerCount; i++)
        {
            this.Players[i] = new();
        }


    }
    public class Player
    {
        public Player()
        {

        }
        public class Card
        {
            public enum Names
            {
                None,
                P3Bullets,
                NoAim,
                GainHelmet
            }

            public static implicit operator Int32(Card card) => card.Item;

            public int Item { get; private set; }
            public void Update()
            {
                int rdmValue = RRPobj.Randi();
            }
        }

        public int CardMax { get; init; } = 3;
        int[] _datas = new int[3];
        Card[] _cards;

        public enum Effect
        {
            HaveHelmet = 1,
            CantAim
        }

        public int Life
        {
            get => this._datas[0];
            internal set => this._datas[0] = value;
        }
        public int Coins
        {
            get => this._datas[1];
            internal set => this._datas[1] = value;
        }
        public int Effects
        {
            get => this._datas[2];
            internal set => this._datas[2] = value;
        }
        public Card[] Cards
        {
            get => this._cards;
            internal set => this._cards = value;
        }

        public bool HasEffect(Effect index) => ((this.Effects >> (int)index) & 1) == 1;
        internal void HasEffect(Effect index, bool changes)
        {
            if (changes) this.Effects |= (1 << (int)index);
            else this.Effects ^= (1 << (int)index);
        }
        public bool HasCard(Card index)
        {

        }
        internal void HasCard(Card index, bool changes)
        {
            if (changes) this.Cards |= (1 << (int)index);
            else this.Cards ^= (1 << (int)index);
        }
    }

    RRPobj.Player[] _playerDatas;
    int[] _datas = new int[6];



    public int BulletMax { get; init; } = 6;
    public RRPobj.Player[] Players 
    { 
        get => this._playerDatas; 
        set => this._playerDatas = value;
    }
    public int PlayerCount
    {
        get => this._datas[0];
        private set => this._datas[0] = value;
    }
    public int PlayerTurn
    {
        get => this._datas[1];
        private set => this._datas[1] = value;
    }
    public int BulletTurn
    {
        get => this._datas[2];
        private set
        {
            if (value >= this.BulletMax) this._datas[2] = 0;
            else if (value < 0) this._datas[2] = this.BulletMax - 1;
            else this._datas[2] = value;
        }
    }
    public int Bullets 
    { 
        get => this._datas[3];
        private set => this._datas[3] = value;
    }
    public int NextStep
    {
        get => this._datas[4];
        private set
        {
            if (value < 0) this._datas[4] = -1;
            else this._datas[4] = 1;
        }
    }
    public int Actions
    {
        get => this._datas[5];
        private set => this._datas[5] = value;
    }



    public static int Randi(int limit)
    {
        Random rdm = new();
        return rdm.Next(limit);
    }
    public static T Randi<T>(List<(T item, int freq)> probs)
    {
        Random rdm = new();
        int size = 0;

        probs.ForEach(p => size += p.freq);
        int result = rdm.Next(size);

        size = 0;
        for (int i = 0; i < probs.Count; i++)
        {
            size += probs[i].freq;
            if (result < size) return probs[i].item;
        }
        return default;
    }
    public bool HasAction(Action index) => (this.Actions >> (int)index & 1) == 1;
    void HasAction(Action index, bool changes)
    {
        if (changes) this.Actions |= 1 << (int)index;
        else this.Actions ^= (1 << (int)index);
    }
    public bool Baraban(int index) => (this.Bullets >> index & 1) == 1;
    void Baraban(int index, bool changes)
    {
        if (changes) this.Bullets |= 1 << index;
        else this.Bullets &= ~(1 << index);
    }
    bool AddBullet(bool JustOne = false)
    {
        int isEmpty = 0;
        for (int i = 0; i < this.BulletMax; i++)
        {
            if (!this.Baraban(i)) isEmpty++;
        }

        if (isEmpty == 0) return false;
        if (JustOne && isEmpty != this.BulletMax) return false;

        int rdmValue = RRPobj.Randi(isEmpty);
        isEmpty = 0;

        for (int i = 0; i < this.BulletMax; i++)
        {
            if (!this.Baraban(i))
            {
                if (rdmValue == isEmpty)
                {
                    this.Baraban(i, true);
                    return true;
                }
                else isEmpty++;
            }
        }
        return true;
    }
    bool ShootAt(int index)
    {
        bool ifShoot = false;

        if (this.Baraban(this.BulletTurn))
        {
            if (this.Players[this.PlayerTurn].HasEffect(Player.Effect.HaveHelmet))
            {
                this.Players[this.PlayerTurn].HasEffect(Player.Effect.HaveHelmet, false);
            }
            else this.Players[this.PlayerTurn].Life -= 1;

            this.AddBullet(true);
            ifShoot = true;
        }
        this.BulletTurn++;
        return ifShoot;
    }
    public int GameIO(int input)
    {

       

        return 0;
    }
    public void ConsoleRun()
    {
        try
        {

        }
        catch (RRP3Exceptions ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}