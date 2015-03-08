using PokerBot.Entity.Card;
using PokerBot.Entity.Enum;
using PokerBot.Entity.Player;
using PokerBot.Entity.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Table
{
    public class Player
    {
        private Dictionary<HandHistories.Objects.Cards.Street, List<Tuple<ActionEnum, Decimal, Decimal>>> _action;

        protected Hand.Hand _hand;
        protected String _name;
        protected Decimal _startStack;
        protected Boolean _isDealer;
        protected Boolean _isSmallBlind;
        protected Boolean _isBigBlind;
        protected Decimal _cachedStack;
        protected Boolean _isFold;
        protected int _seatPosition;
        private Boolean _isSittingOut;

        protected Boolean IsSittingOut
        {
            get { return _isSittingOut; }
            set { _isSittingOut = value; }
        }

        public Hand.Hand Hand
        {
            get { return _hand; }
            set { _hand = value; }
        }


        public int SeatPosition
        {
            get { return _seatPosition; }
            set { _seatPosition = value; }
        }

        public HandHistories.Objects.Cards.Street LastStreet
        {
            get
            {
                return this._action.Keys.OrderBy(x => (int)x).Last();
            }
        }

        public Boolean IsFold
        {
            get { return _isFold; }
            set { _isFold = value; }
        }

        public Decimal CurrentStack
        {
            get { return _cachedStack; }
            set { _cachedStack = value; }
        }

        public Boolean IsBigBlind
        {
            get { return _isBigBlind; }
            set { _isBigBlind = value; }
        }

        public Boolean IsSmallBlind
        {
            get { return _isSmallBlind; }
            set { _isSmallBlind = value; }
        }

        public Boolean IsDealer
        {
            get { return _isDealer; }
            set { _isDealer = value; }
        }

        public Decimal StartStack
        {
            get { return _startStack; }
            set { _startStack = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Stats Stats
        {
            get 
            {
                return StatSingleton.Instance.getStatForPlayerByName(this.Name);
            }
        }

        public Dictionary<HandHistories.Objects.Cards.Street, List<Tuple<ActionEnum, Decimal, Decimal>>> ListAction
        {
            get
            {
                return this._action;
            }
            set
            {
                this._action = value;
            }
        }

        public Player()
        {
            this._action = new Dictionary<HandHistories.Objects.Cards.Street, List<Tuple<ActionEnum, Decimal, Decimal>>>();
        }

        public void addAction(HandHistories.Objects.Cards.Street step, ActionEnum actionEnum, Decimal bet)
        {
            if (!this._action.ContainsKey(step)) 
            {
                this._action[step] = new List<Tuple<ActionEnum, Decimal, Decimal>>();
            }
            this._action[step].Add(new Tuple<ActionEnum, Decimal, Decimal>(actionEnum, this._cachedStack, bet));
            
            this._cachedStack -= bet;
        }

        public static Player fromHandHistoriesPlayer(HandHistories.Objects.Players.Player player)
        {
            Player returnPlayer = new Player();
            returnPlayer.Name = player.PlayerName;
            returnPlayer.StartStack = player.StartingStack;
            returnPlayer.ListAction = new Dictionary<HandHistories.Objects.Cards.Street, List<Tuple<ActionEnum, Decimal, Decimal>>>();
            returnPlayer.CurrentStack = returnPlayer.StartStack;
            returnPlayer.SeatPosition = player.SeatNumber;
            returnPlayer.IsSittingOut = player.IsSittingOut;
            if (player.hasHoleCards)
            {
                returnPlayer.Hand = new Hand.Hand(CardConverter.fromIntToPlayingCard(player.HoleCards[0]), CardConverter.fromIntToPlayingCard(player.HoleCards[1]));
            }
            return returnPlayer;
        }
    }
}
