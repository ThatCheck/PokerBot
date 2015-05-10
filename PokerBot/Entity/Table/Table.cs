using HandHistories.Objects.Cards;
using HandHistories.Objects.Hand;
using HandHistories.Objects.Players;
using NLog;
using NLog.Config;
using NLog.Targets;
using PokerBot.Entity.Card;
using PokerBot.Entity.Enum;
using PokerBot.Entity.Event;
using PokerBot.Entity.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerBot.Entity.Table
{
    public class Table
    {

        private String _name;
        private IntPtr _hwnd;
        private const Boolean OBSERVER =true;
        private bool _inDecoding;

        /**
         * Entity
         */
        private HandHistory _handHistory;
        private long _handId;
        private int _numberPlayer;
        private int _numberPlayerPlaying;
        private List<Player> _listOrderedPlayer;
        private String _hash;
        private Board _board;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private List<Player> _listPlayer;
        /** Event */

        public delegate void EndTableAnalyzerHandler(object sender);
        public event EndTableAnalyzerHandler EndTableAnalyzer;




        public List<Player> ListPlayer
        {
            get { return _listPlayer; }
            set { _listPlayer = value; }
        }

        public Board Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public HandHistory HandHistory
        {
            get { return _handHistory; }
            set { _handHistory = value; }
        }

        public String Hash
        {
            get { return _hash; }
            set { _hash = value; }
        }

        public List<Player> ListOrderedPlayer
        {
            get { return _listOrderedPlayer; }
        }


        public int NumberPlayerPlaying
        {
            get { return _numberPlayerPlaying; }
        }

        public int NumberPlayer
        {
            get { return _numberPlayer; }
        }

        public long HandId
        {
            get { return _handId; }
        }

        public String Name
        {
            get
            {
                return this._name;
            }
        }

       
        public Table (IntPtr hwnd)
        {
            this._hwnd = hwnd;
            this._name = Utils.WindowHandle.getWindowText(this._hwnd);
            this._inDecoding = false;
        }

        //This is for training table
        public Table(HandHistory hand)
        {
            this._name = hand.TableName;
            this._handHistory = hand;
            this._inDecoding = false;
            this.analyze(hand);
        }

        public void analyze(HandHistory hand)
        {
            try
            {
                //detect the street
                this._inDecoding = true;

                //Copying principle information
                this._handId = hand.HandId;
                this._numberPlayer = hand.NumPlayersSeated;
                this._numberPlayerPlaying = hand.NumPlayersActive;
                this._listPlayer = new List<Player>();
                Street currentStreet = Street.Preflop;
                foreach (var handAction in hand.HandActions)
                {
                    if (handAction.Street == Street.Flop && currentStreet == Street.Preflop)
                    {
                        currentStreet = Street.Flop;
                    }
                    else if (handAction.Street == Street.Turn && currentStreet == Street.Flop)
                    {
                        currentStreet = Street.Turn;
                    }
                    else if (handAction.Street == Street.River && currentStreet == Street.Turn)
                    {
                        currentStreet = Street.River;
                    }
                    else if (handAction.Street == Street.Showdown && currentStreet == Street.River)
                    {
                        currentStreet = Street.Showdown;
                    }
                }
                //No need to act
                if (currentStreet == Street.Null)
                {
                    return;
                }

                HandHistories.Objects.Players.Player hero = hand.Hero;
                //Create the list of player
                Dictionary<String, Player> noOrderedList = new Dictionary<String, Player>();

                if (hero == null && OBSERVER == false)
                {
                    //we are just observator, abord !
                    return;
                }
                else if (hero != null)
                {
                    //Create the hero configuration
                    HeroPlayer heroPlayer = (HeroPlayer)Player.fromHandHistoriesPlayer(hero);
                    heroPlayer.Hand = new Hand.Hand(CardConverter.fromIntToPlayingCard(hero.HoleCards[0]), CardConverter.fromIntToPlayingCard(hero.HoleCards[1]));
                    noOrderedList.Add(hero.PlayerName, heroPlayer);
                }


                //create the current board
                this._board = Board.createFromHandHistory(hand.ComumnityCards);

                PlayerList playerList = hand.Players;

                //Create the data for player
                foreach (var player in playerList)
                {
                    this._listPlayer.Add(Player.fromHandHistoriesPlayer(player));
                }

                //HandHistories.Objects.Players.Player dealer = hand.Players.Where(p => p.SeatNumber == hand.DealerButtonPosition).First();
                foreach (var player in playerList.Where(p => p.IsSittingOut == false))
                {
                    if (hero == null || !hero.PlayerName.Equals(player.PlayerName))
                    {
                        noOrderedList.Add(player.PlayerName, Player.fromHandHistoriesPlayer(player));
                    }

                    if (hand.DealerButtonPosition == player.SeatNumber)
                    {
                        noOrderedList[player.PlayerName].IsDealer = true;
                    }
                }

                //Construct all the hand data
                decimal totalCurrentPot = 0;
                this._hash = "";
                Street temporaryStreetTampon = Street.Preflop;
                foreach (var action in hand.HandActions)
                {
                    string hashStep = "";
                    if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.ALL_IN)
                    {
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.AllIn, Math.Abs(action.Amount));
                        hashStep = ActionEnumConverter.FromActionEnum(ActionEnum.AllIn);
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.CALL)
                    {
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Call, Math.Abs(action.Amount));
                        hashStep = ActionEnumConverter.FromActionEnum(ActionEnum.Call);
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.FOLD)
                    {
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Fold, 0);
                        noOrderedList[action.PlayerName].IsFold = true;
                        hashStep = ActionEnumConverter.FromActionEnum(ActionEnum.Fold);
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.CHECK)
                    {
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Check, 0);
                        hashStep = ActionEnumConverter.FromActionEnum(ActionEnum.Check);
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.SMALL_BLIND)
                    {
                        noOrderedList[action.PlayerName].IsSmallBlind = true;
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Other, Math.Abs(action.Amount));
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.BIG_BLIND)
                    {
                        noOrderedList[action.PlayerName].IsBigBlind = true;
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Other, Math.Abs(action.Amount));
                    }
                    else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.BET
                        || action.HandActionType == HandHistories.Objects.Actions.HandActionType.RAISE)
                    {
                        Decimal amountValue = Math.Abs(action.Amount);
                        Decimal percentPot = amountValue / totalCurrentPot;
                        ActionEnum selectedAction = ActionEnum.Overbet;
                        if (percentPot < 0.40M)
                        {
                            selectedAction = ActionEnum.BetQuarter;
                        }
                        else if (percentPot >= 0.40M && percentPot < 0.60M)
                        {
                            selectedAction = ActionEnum.BetHalf;
                        }
                        else if (percentPot >= 0.60M && percentPot < 0.85M)
                        {
                            selectedAction = ActionEnum.BetThreeQuarter;
                        }
                        else if (percentPot >= 0.85M && percentPot <= 1M)
                        {
                            selectedAction = ActionEnum.BetPot;
                        }
                        hashStep = ActionEnumConverter.FromActionEnum(selectedAction);
                        noOrderedList[action.PlayerName].addAction(action.Street, selectedAction, amountValue);
                    }
                    else
                    {
                        noOrderedList[action.PlayerName].addAction(action.Street, ActionEnum.Other, Math.Abs(action.Amount));
                    }
                    //If hero who play, go upper ! 
                    if (hero != null && hero.PlayerName.Equals(action.PlayerName))
                    {
                        hashStep = hashStep.ToUpper();
                    }
                    if (temporaryStreetTampon != action.Street && (action.Street == Street.Flop || action.Street == Street.Turn || action.Street == Street.River))
                    {
                        temporaryStreetTampon = action.Street;
                        hashStep += "|";
                    }
                    this._hash += hashStep;
                    totalCurrentPot += Math.Abs(action.Amount);
                }

                List<Player> noOrderedListFromDictionnary = noOrderedList.Values.ToList();
                Player dealer = noOrderedListFromDictionnary.Where(p => p.IsDealer == true).First();
                //Construct the order to play
                List<Player> orderedPlayerList = new List<Player>();
                int currentNumberPlayers = hand.NumPlayersActive;
                if (currentStreet.Equals(Street.Preflop))
                {
                    //Start with utg
                    if (currentNumberPlayers == 2)
                    {
                        var firstPlayer = noOrderedListFromDictionnary.First();
                        if (dealer.Name.Equals(firstPlayer.Name))
                        {
                            orderedPlayerList.Add(noOrderedListFromDictionnary.Last());
                            orderedPlayerList.Add(noOrderedListFromDictionnary.First());
                        }
                        else
                        {
                            orderedPlayerList.Add(noOrderedListFromDictionnary.First());
                            orderedPlayerList.Add(noOrderedListFromDictionnary.Last());
                        }
                    }
                    else
                    {
                        IEnumerable<Player> playerWhoCanPlay = noOrderedListFromDictionnary.Where(p => p.IsDealer == false
                            && p.IsSmallBlind == false
                            && p.IsBigBlind == false);

                        int dealerSitPosition = hand.DealerButtonPosition;
                        Player smallBlindPlayer = noOrderedListFromDictionnary.Where(p => p.IsSmallBlind == true).First();
                        Player bigBlindPlayer = noOrderedListFromDictionnary.Where(p => p.IsBigBlind == true).First();
                        int maxPositionNumber = hand.GameDescription.SeatType.MaxPlayers;
                        Boolean sitDirection = smallBlindPlayer.SeatPosition > bigBlindPlayer.SeatPosition;
                        int cpt = bigBlindPlayer.SeatPosition;
                        IEnumerable<Player> playerOrderByPositionSeat = noOrderedListFromDictionnary.OrderBy(p => p.SeatPosition);
                        for (int i = 0; i < maxPositionNumber; i++)
                        {
                            cpt = (++cpt % (maxPositionNumber + 1)) + 1;
                            Player selected = playerOrderByPositionSeat.Where(p => p.SeatPosition == cpt).FirstOrDefault();
                            if (selected != null)
                            {
                                orderedPlayerList.Add(selected);
                            }
                        }
                        orderedPlayerList.Add(dealer);
                        orderedPlayerList.Add(noOrderedListFromDictionnary.Where(p => p.IsSmallBlind == true).First());
                        orderedPlayerList.Add(noOrderedListFromDictionnary.Where(p => p.IsBigBlind == true).First());
                    }
                }
                else
                {

                    orderedPlayerList.Add(noOrderedListFromDictionnary.Where(p => p.IsSmallBlind == true).First());
                    orderedPlayerList.Add(noOrderedListFromDictionnary.Where(p => p.IsBigBlind == true).First());
                    IEnumerable<Player> playerWhoCanPlay = noOrderedListFromDictionnary.Where(p => p.IsDealer == false
                           && p.IsSmallBlind == false
                           && p.IsBigBlind == false);
                    int dealerSitPosition = hand.DealerButtonPosition;
                    Player smallBlindPlayer = noOrderedListFromDictionnary.Where(p => p.IsSmallBlind == true).First();
                    Player bigBlindPlayer = noOrderedListFromDictionnary.Where(p => p.IsBigBlind == true).First();
                    int maxPositionNumber = hand.GameDescription.SeatType.MaxPlayers;
                    Boolean sitDirection = smallBlindPlayer.SeatPosition > bigBlindPlayer.SeatPosition;
                    int cpt = bigBlindPlayer.SeatPosition;
                    IEnumerable<Player> playerOrderByPositionSeat = noOrderedListFromDictionnary.OrderBy(p => p.SeatPosition);
                    for (int i = 0; i < maxPositionNumber; i++)
                    {
                        cpt = (++cpt % (maxPositionNumber + 1)) + 1;
                        Player selected = playerOrderByPositionSeat.Where(p => p.SeatPosition == cpt).FirstOrDefault();
                        if (selected != null && orderedPlayerList.Where(p => p.SeatPosition != selected.SeatPosition).FirstOrDefault() == null)
                        {
                            orderedPlayerList.Add(selected);
                        }
                    }
                    orderedPlayerList.Add(dealer);
                }
                this._listOrderedPlayer = orderedPlayerList;
                OnEndTableAnalyzer(this);
                this._inDecoding = false;
            }
            catch (Exception ex)
            {
                _logger.Error("Unable to decode table : " + ex.Message + " \n " + ex.StackTrace);
                throw ex;
            }
        }

        public void NewHandEventHandler(object sender, DecodedHandEventArgs e)
        {
            if(e.HandHistory.TableName.Contains(this._name.Split(new string[]{" - "},StringSplitOptions.None).First().Trim()) && this._inDecoding == false)
            {
                this.analyze(e.HandHistory);
            }
        }

        protected void OnEndTableAnalyzer(object sender)
        {
            if (EndTableAnalyzer != null)
            {
                EndTableAnalyzer(this);
            }
        }
    }
}
