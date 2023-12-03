using RRP3.Exceptions;

namespace RRP3.Models;

internal class RRPgame
{
    public class RRPaction
    {
        public int input { get; init; }
        public Menu menu { get; init; }
    }

    int[] _datas = new int[10];
    RRPplayer[] _players;
    List<RRPaction> _actions = new List<RRPaction>();

    public enum Menu
    {
        NotReady,
        MainMenu,
        VictimMenu,
        CardMenu
    }
    public enum MainMenu
    {
        ShootSelf,
        ShootSomeone,
        UseCard,
        UpdateCard
    }
    public enum GameData
    {
        PlayerInput,
        PlayerCount,
        PlayerTurn,
        MaxCards,
        Baraban,
        BulletTurn,
        MaxBullets,
        BulletsAlways,
        GameEvents,
        CurrentMenu
    }
    public enum GameEvent
    {
        Backwords
    }
    public enum GameResult
    {
        GetShot = 1
    }
    public RRPgame(int playerCount = 2, int maxBullets = 6, int bulletsAlways = 1, int maxCards = 3)
    {
        this.PlayerTurn = 0;
        this.BulletTurn = 0;
        this.PlayerCount = playerCount;
        this.PlayerInput = this.PlayerCount;
        this.MaxBullets = maxBullets;
        this.BulletsAlways = bulletsAlways;
        this.MaxCards = maxCards;
        this.CurrentMenu = (int)Menu.MainMenu;
    }


    public int MaxCards
    {
        get => this._datas[(int)GameData.MaxCards];
        private init
        {
            if (value < 0) value = 0;
            this._datas[(int)GameData.MaxCards] = value;

            for (int i = 0; i < this.PlayerCount; i++)
            {
                this._players[i] = new RRPplayer(value);
            }
        }
    }
    public int PlayerCount 
    { 
        get => this._datas[(int)GameData.PlayerCount];
        private init
        {
            if (value < 2) value = 2;
            this._datas[(int)GameData.PlayerCount] = value;

            this._players = new RRPplayer[this.PlayerCount];
        }
    }
    public int PlayerTurn
    {
        get => this._datas[(int)GameData.PlayerTurn];
        private set
        {
            if (value < 0) value = this.PlayerCount - 1;
            else if (value >= this.PlayerCount) value = 0;
            this._datas[(int)GameData.PlayerTurn] = value;
        }
    }
    public int MaxBullets
    {
        get => this._datas[(int)GameData.MaxBullets];
        private init
        {
            if (value < 2) value = 2;
            this._datas[(int)GameData.MaxBullets] = value;
        }
    }
    public int BulletTurn
    {
        get => this._datas[(int)GameData.BulletTurn];
        private set
        {
            if (value < 0) this._datas[(int)GameData.BulletTurn] = this.MaxBullets - 1;
            else if (value >= this.MaxBullets) this._datas[(int)GameData.BulletTurn] = 0;
            else this._datas[(int)GameData.BulletTurn] = value;
        }
    }
    public int BulletsAlways
    {
        get => this._datas[(int)GameData.BulletsAlways];
        private init
        {
            if (value < 1) this._datas[(int)GameData.BulletsAlways] = 1;
            else if (value >= this.MaxBullets) this._datas[(int)GameData.BulletsAlways] = this.MaxBullets - 1;
            else this._datas[(int)GameData.BulletsAlways] = value;

            for (int i = 0; i < value; i++)
            {
                this.AddBullet();
            }
        }
    }
    public int CurrentMenu
    {
        get => this._datas[(int)GameData.CurrentMenu];
        private set
        {
            if(!Enum.IsDefined(typeof(Menu), value)) throw new RRP3InvalidEnumException();

            this._actions.Add(new RRPaction() 
            { 
                input = this.PlayerInput,
                menu = (Menu)this.CurrentMenu 
            });
            this._datas[(int)GameData.CurrentMenu] = value;
        }
    }
    public int PlayerInput 
    {
        get => this._datas[(int)GameData.PlayerInput];
        private set
        { 
            this._datas[(int)GameData.PlayerInput] = value;
        }
    }


    public RRPaction ActionsList(int index)
    {
        if(index < 0) return this._actions[^(-index)];
        return this._actions[index];
    }
    public bool isGameEvent(GameEvent gameEvent)
    {
        if (!Enum.IsDefined<GameEvent>(gameEvent)) throw new RRP3InvalidEnumException();
        if ((this._datas[(int)GameData.GameEvents] & (1 << (int)gameEvent)) == 0) return false;
        return true;
    }
    public void isGameEvent(GameEvent gameEvent, bool change)
    {
        if (!Enum.IsDefined<GameEvent>(gameEvent)) throw new RRP3InvalidEnumException();
        if (change) this._datas[(int)GameData.GameEvents] |= (1 << (int)gameEvent);
        else this._datas[(int)GameData.GameEvents] &= ~(1 << (int)gameEvent);
    }
    public bool Baraban(int index)
    {
        if ((this._datas[(int)GameData.Baraban] & (1 << index)) == 0) return false;
        return true;
    }
    public void Baraban(int index, bool change)
    {
        if (change) this._datas[(int)GameData.Baraban] |= (1 << index);
        else this._datas[(int)GameData.Baraban] &= ~(1 << index);
    }
    public RRPplayer Player(int index) => this._players[index];
    public bool AddBullet(bool JustOne = false)
    {
        int isEmpty = 0;
        for (int i = 0; i < this.MaxBullets; i++)
        {
            if (!this.Baraban(i)) isEmpty++;
        }

        if (isEmpty == 0) return false;
        if (JustOne && isEmpty <= this.MaxBullets - this.BulletsAlways) return false;

        int rdmValue = RRP3Tools.Randi(isEmpty);
        isEmpty = 0;

        for (int i = 0; i < this.MaxBullets; i++)
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
    public bool ShootAt(RRPplayer player)
    {
        bool ifShoot = false;

        if (this.Baraban(this.BulletTurn))
        {
            if (player.StatusIs(RRPplayer.Status.HaveHelmet))
            {
                player.StatusIs(RRPplayer.Status.HaveHelmet, false);
            }
            else player.HitPoints = player.HitPoints - 1;

            this.Baraban(this.BulletTurn, false);

            for (int i = 0; i < this.BulletsAlways; i++)
            {
                this.AddBullet(true);
            }
            ifShoot = true;
        }
        this.BulletTurn = this.BulletTurn + 1;
        return ifShoot;
    }
    public int GameIO(int input)
    {
        //this.PlayerInput = input;
        int result = 0;
        bool GoNext = true;

        switch ((Menu)this.CurrentMenu)
        {
            case Menu.MainMenu:
                if (!Enum.IsDefined(typeof(MainMenu), input)) return 0;

                switch ((MainMenu)input)
                {
                    case MainMenu.ShootSelf:
                        if (this.ShootAt(this.Player(this.PlayerTurn))) result = (int)GameResult.GetShot;
                        this.PlayerInput = input;
                        this.CurrentMenu = (int)Menu.MainMenu;

                        break;
                    case MainMenu.ShootSomeone:
                        if (this.Player(this.PlayerTurn).Coins < 2) break;
                        this.PlayerInput = input;
                        this.CurrentMenu = (int)Menu.VictimMenu;
                        GoNext = false;

                        break;
                    case MainMenu.UseCard:
                        this.PlayerInput = input;
                        this.CurrentMenu = (int)Menu.CardMenu; 
                        GoNext = false;

                        break;
                    case MainMenu.UpdateCard:
                        RRPplayer player = this.Player(this.PlayerTurn);
                        if (player.Coins >= 2)
                        {
                            player.UpdateDeck();
                            player.Coins -= 2;
                        }

                        this.PlayerInput = input;
                        this.CurrentMenu = (int)Menu.MainMenu;
                        GoNext = false;

                        break;
                    default:
                        GoNext = false;
                        break;
                }

                break;
            case Menu.VictimMenu:
                if (input >= this.PlayerCount || input < 0)
                {
                    this.PlayerInput = input;
                    this.CurrentMenu = (int)Menu.MainMenu;
                    break;
                }
                RRPaction prevAction = this.ActionsList(-1);
                RRPplayer player1 = this.Player(this.PlayerTurn);
                RRPplayer player2 = this.Player(input);

                if (prevAction.menu == Menu.CardMenu) 
                {
                    switch (player1.Deck(prevAction.input))
                    {
                        case RRPplayer.Card.CurseNoAim:
                            player1.Deck(prevAction.input, RRPplayer.Card.Blank);
                            player2.StatusIs(RRPplayer.Status.CursedNoAim, true);
                            break;
                    }
                }
                else
                {
                    if (player1.StatusIs(RRPplayer.Status.CursedNoAim)) player2 = new();
                    if (this.ShootAt(player2)) result = (int)GameResult.GetShot;
                    player1.Coins -= 2;
                }
                this.PlayerInput = input;
                this.CurrentMenu = (int)Menu.MainMenu;

                break;
            case Menu.CardMenu:
                try
                {
                    RRPplayer player = this.Player(this.PlayerTurn);
                    switch (player.Deck(input))
                    {
                        case RRPplayer.Card.Blank:
                            this.PlayerInput = input;
                            this.CurrentMenu = (int)Menu.MainMenu;
                            GoNext = false;

                            break;
                        case RRPplayer.Card.CurseNoAim:
                            this.PlayerInput = input;
                            this.CurrentMenu = (int)Menu.VictimMenu;
                            GoNext = false;

                            break;
                        case RRPplayer.Card.P2Bullets:
                            player.Deck(input, RRPplayer.Card.Blank);
                            this.AddBullet();
                            this.AddBullet();
                            this.PlayerInput = input;
                            this.CurrentMenu = (int)Menu.MainMenu;

                            break;
                        case RRPplayer.Card.GainHelmet:
                            player.StatusIs(RRPplayer.Status.HaveHelmet, true);
                            player.Deck(input, RRPplayer.Card.Blank);
                            this.PlayerInput = input;
                            this.CurrentMenu = (int)Menu.MainMenu;

                            break;
                        case RRPplayer.Card.Reverse:
                            this.isGameEvent(GameEvent.Backwords, !this.isGameEvent(GameEvent.Backwords));
                            player.Deck(input, RRPplayer.Card.Blank);
                            this.PlayerInput = input;
                            this.CurrentMenu = (int)Menu.MainMenu;

                            break;
                    }

                }
                catch (RRP3IndexOutOfDeckException)
                {
                    this.PlayerInput = input;
                    this.CurrentMenu = (int)Menu.MainMenu;
                    GoNext = false;
                }
                break;
            default:
                return 0;
        }

        int isNotDeath = 0, isAlive = 0;
        for (int i = 0; i < this.PlayerCount; i++)
        {
            if (this.Player(i).HitPoints > 0)
            {
                isAlive++;
                isNotDeath = i;
            }
        }

        if (isAlive == 1) return -(isNotDeath + 1);
        if (GoNext)
        {
            int protector = 0;
            int step = (this.isGameEvent(GameEvent.Backwords)) ? -1 : 1;
            do
            {
                this.PlayerTurn += step;
                if (this.Player(this.PlayerTurn).HitPoints > 0) break;
                protector++;
            } while (protector <= this.PlayerCount);
        }

        return result;
    }
}
