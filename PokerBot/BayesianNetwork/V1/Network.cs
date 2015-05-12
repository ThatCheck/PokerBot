using PokerBot.Entity.Table;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1
{
    public class Network : IDisposable
    {
        #region Action
        private Action.Action _pfAction;
        private Action.ActionFlop _flopAction;
        private Action.ActionTurn _turnAction;
        private Action.ActionRiver _riverAction;

        public Action.ActionRiver RiverAction
        {
            get { return _riverAction; }
            set { _riverAction = value; }
        }

        public Action.ActionTurn TurnAction
        {
            get { return _turnAction; }
            set { _turnAction = value; }
        }

        public Action.ActionFlop FlopAction
        {
            get { return _flopAction; }
            set { _flopAction = value; }
        }

        public Action.Action PfAction
        {
            get { return _pfAction; }
            set { _pfAction = value; }
        }
        #endregion
        #region Board
        private Board.BoardFlop _flopBoard;
        private Board.BoardChangeTurn _turnBoard;
        private Board.BoardChangeRiver _riverBoard;

        public Board.BoardChangeRiver RiverBoard
        {
            get { return _riverBoard; }
            set { _riverBoard = value; }
        }

        public Board.BoardChangeTurn TurnBoard
        {
            get { return _turnBoard; }
            set { _turnBoard = value; }
        }

        public Board.BoardFlop FlopBoard
        {
            get { return _flopBoard; }
            set { _flopBoard = value; }
        }
        #endregion
        #region DrawingHand
        private DrawingHand.DrawingHand _flopDrawingHand;
        private DrawingHand.DrawingHandTurn _turnDrawingHand;

        public DrawingHand.DrawingHandTurn TurnDrawingHand
        {
            get { return _turnDrawingHand; }
            set { _turnDrawingHand = value; }
        }

        public DrawingHand.DrawingHand FlopDrawingHand
        {
            get { return _flopDrawingHand; }
            set { _flopDrawingHand = value; }
        }
        #endregion
        #region Flush
        private DrawingHand.Flush _flushFlopHand;
        private DrawingHand.FlushChangeTurn _flushTurnHand;
        private DrawingHand.FlushChangeRiver _flushRiverHand;

        public DrawingHand.FlushChangeRiver FlushRiverHand
        {
            get { return _flushRiverHand; }
            set { _flushRiverHand = value; }
        }

        public DrawingHand.FlushChangeTurn FlushTurnHand
        {
            get { return _flushTurnHand; }
            set { _flushTurnHand = value; }
        }
        public DrawingHand.Flush FlushFlopHand
        {
            get { return _flushFlopHand; }
            set { _flushFlopHand = value; }
        }
        #endregion
        #region Straight
        private DrawingHand.Straight _straightFlopHand;
        private DrawingHand.StraightChangeTurn _straightTurnHand;
        private DrawingHand.StraightChangeRiver _straightRiverHand;

        public DrawingHand.StraightChangeRiver StraightRiverHand
        {
            get { return _straightRiverHand; }
            set { _straightRiverHand = value; }
        }

        public DrawingHand.StraightChangeTurn StraightTurnHand
        {
            get { return _straightTurnHand; }
            set { _straightTurnHand = value; }
        }

        public DrawingHand.Straight StraightFlopHand
        {
            get { return _straightFlopHand; }
            set { _straightFlopHand = value; }
        }
        #endregion
        #region HandGroup
        private HandGroup.HandGroup _handGroup;

        public HandGroup.HandGroup HandGroup
        {
            get { return _handGroup; }
            set { _handGroup = value; }
        }
        #endregion
        #region HandType
        private HandType.HandType _flopHandType;
        private HandType.HandTypeTurn _turnHandType;
        private HandType.HandTypeRiver _riverHandType;

        public HandType.HandTypeRiver RiverHandType
        {
            get { return _riverHandType; }
            set { _riverHandType = value; }
        }

        public HandType.HandTypeTurn TurnHandType
        {
            get { return _turnHandType; }
            set { _turnHandType = value; }
        }

        public HandType.HandType FlopHandType
        {
            get { return _flopHandType; }
            set { _flopHandType = value; }
        }
        #endregion
        #region NumberPlayerSitIn
        private NumberPlayerSitIn.NumberPlayerSitIn _pfNumberPlayer;
        private NumberPlayerSitIn.FlopNumberPlayerSitIn _flopNumberPlayer;
        private NumberPlayerSitIn.TurnNumberPlayerSitIn _turnNumberPlayer;
        private NumberPlayerSitIn.RiverNumberPlayerSitIn _riverNumberPlayer;

        public NumberPlayerSitIn.RiverNumberPlayerSitIn RiverNumberPlayer
        {
            get { return _riverNumberPlayer; }
            set { _riverNumberPlayer = value; }
        }

        public NumberPlayerSitIn.TurnNumberPlayerSitIn TurnNumberPlayer
        {
            get { return _turnNumberPlayer; }
            set { _turnNumberPlayer = value; }
        }

        public NumberPlayerSitIn.FlopNumberPlayerSitIn FlopNumberPlayer
        {
            get { return _flopNumberPlayer; }
            set { _flopNumberPlayer = value; }
        }

        public NumberPlayerSitIn.NumberPlayerSitIn PfNumberPlayer
        {
            get { return _pfNumberPlayer; }
            set { _pfNumberPlayer = value; }
        }
        #endregion
        #region Position
        private Position.Position _pfPosition;
        private Position.PositionFlop _flopPosition;
        private Position.PositionTurn _turnPosition;
        private Position.PositionRiver _riverPosition;

        public Position.PositionRiver RiverPosition
        {
            get { return _riverPosition; }
            set { _riverPosition = value; }
        }

        public Position.PositionTurn TurnPosition
        {
            get { return _turnPosition; }
            set { _turnPosition = value; }
        }

        public Position.PositionFlop FlopPosition
        {
            get { return _flopPosition; }
            set { _flopPosition = value; }
        }

        public Position.Position PfPosition
        {
            get { return _pfPosition; }
            set { _pfPosition = value; }
        }
        #endregion
        #region PotOdds
        private PotOdds.PotOdds _pfPotOdds;
        private PotOdds.PotOddsFlop _flopPotOdds;
        private PotOdds.PotOddsTurn _turnPotOdds;
        private PotOdds.PotOddsRiver _riverPotOdds;

        public PotOdds.PotOddsRiver RiverPotOdds
        {
            get { return _riverPotOdds; }
            set { _riverPotOdds = value; }
        }

        public PotOdds.PotOddsTurn TurnPotOdds
        {
            get { return _turnPotOdds; }
            set { _turnPotOdds = value; }
        }

        public PotOdds.PotOddsFlop FlopPotOdds
        {
            get { return _flopPotOdds; }
            set { _flopPotOdds = value; }
        }

        public PotOdds.PotOdds PfPotOdds
        {
            get { return _pfPotOdds; }
            set { _pfPotOdds = value; }
        }
        #endregion
        #region Stats
        private Stats.FoldToThreeBet _foldToThreeBet;
        private Stats.PFR _pfr;
        private Stats.ThreeBet _threeBet;
        private Stats.VPIP _vpip;

        public Stats.VPIP Vpip
        {
            get { return _vpip; }
            set { _vpip = value; }
        }

        public Stats.ThreeBet ThreeBet
        {
            get { return _threeBet; }
            set { _threeBet = value; }
        }

        public Stats.PFR Pfr
        {
            get { return _pfr; }
            set { _pfr = value; }
        }

        public Stats.FoldToThreeBet FoldToThreeBet
        {
            get { return _foldToThreeBet; }
            set { _foldToThreeBet = value; }
        }
        #endregion
        #region Network
        Smile.Network _network;

        public Smile.Network SmileNetwork
        {
            get { return _network; }
        }
        #endregion

        public Network()
        {
            //Init network to null ! 
            EmptyModelInitializer.EmptyModel<Network>(this);
        }

        public Network(String networkFile) : base()
        {
            try
            {
                this._network = new Smile.Network();
                this._network.ReadFile(networkFile);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Network(Smile.Network networkFile)
            : base()
        {
            try
            {
                this._network = networkFile;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private void clearAllEvidence()
        {
            this._network.ClearAllEvidence();
        }

        public IOrderedEnumerable<KeyValuePair<String,double>> getValueForHandType(Table table, Player player, HandHistories.Objects.Cards.Street forceStreet = HandHistories.Objects.Cards.Street.Null )
        {
            List<HandHistories.Objects.Actions.HandAction> handAction = table.HandHistory.HandActions;
            HandHistories.Objects.Cards.Street maxStreet = HandHistories.Objects.Cards.Street.Null;
            try
            {
                this.clearAllEvidence();
                if (forceStreet == HandHistories.Objects.Cards.Street.Null)
                {
                    if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Showdown))
                    {
                        maxStreet = HandHistories.Objects.Cards.Street.Showdown;
                    }
                    else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.River))
                    {
                        maxStreet = HandHistories.Objects.Cards.Street.River;
                    }
                    else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Turn))
                    {
                        maxStreet = HandHistories.Objects.Cards.Street.Turn;
                    }
                    else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Flop))
                    {
                        maxStreet = HandHistories.Objects.Cards.Street.Flop;
                    }
                    else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Preflop))
                    {
                        maxStreet = HandHistories.Objects.Cards.Street.Preflop;
                    }
                }
                else
                {
                    maxStreet = forceStreet;
                }
                //INIT STAT PLAYER
                String name = player.Name;
                Entity.Player.Stats stats = player.Stats;
                this._pfr = new Stats.PFR(stats.Pfr);
                this._vpip = new Stats.VPIP(stats.Vpip);
                this._foldToThreeBet = new Stats.FoldToThreeBet(stats.FoldToThreeBet);
                this._threeBet = new Stats.ThreeBet(stats.ThreeBetPF);

                this._network.SetEvidence(typeof(Stats.PFR).Name, this._pfr.ToString());
                this._network.SetEvidence(typeof(Stats.VPIP).Name, this._vpip.ToString());
                this._network.SetEvidence(typeof(Stats.FoldToThreeBet).Name, this._foldToThreeBet.ToString());
                this._network.SetEvidence(typeof(Stats.ThreeBet).Name, this._threeBet.ToString());

                //Init PREFLOP ACTION
                if (maxStreet >= HandHistories.Objects.Cards.Street.Preflop)
                {
                    this._pfAction = new Action.Action(handAction, name);
                    this._pfNumberPlayer = new NumberPlayerSitIn.NumberPlayerSitIn(handAction);
                    this._pfPosition = new Position.Position(handAction, name);
                    this._pfPotOdds = new PotOdds.PotOdds(handAction, name);
                    //this._handGroup = new HandGroup.HandGroup(player.Hand);

                    this._network.SetEvidence(typeof(Action.Action).Name, this._pfAction.ToString());
                    this._network.SetEvidence(typeof(NumberPlayerSitIn.NumberPlayerSitIn).Name, this._pfNumberPlayer.ToString());
                    this._network.SetEvidence(typeof(Position.Position).Name, this._pfPosition.ToString());
                    this._network.SetEvidence(typeof(PotOdds.PotOdds).Name, this._pfPotOdds.ToString());
                }
                if (maxStreet >= HandHistories.Objects.Cards.Street.Flop)
                {
                    if (handAction.Any(p => p.PlayerName == name && p.Street == maxStreet))
                    {
                        this._flopAction = new Action.ActionFlop(handAction, name);
                        this._network.SetEvidence(typeof(Action.ActionFlop).Name, this._flopAction.ToString());
                        this._flopPotOdds = new PotOdds.PotOddsFlop(handAction, name);
                        this._network.SetEvidence(typeof(PotOdds.PotOddsFlop).Name, this._flopPotOdds.ToString());
                        this._flopPosition = new Position.PositionFlop(handAction, name);
                        this._network.SetEvidence(typeof(Position.PositionFlop).Name, this._flopPosition.ToString());
                    }

                    this._flopBoard = new Board.BoardFlop(table.Board);
                    //this._flopDrawingHand = new DrawingHand.DrawingHand(table.Board, player.Hand);
                    //this._flopHandType = new HandType.HandType(table.Board, player.Hand);
                    this._flopNumberPlayer = new NumberPlayerSitIn.FlopNumberPlayerSitIn(handAction);
                    this._flushFlopHand = new DrawingHand.Flush(table.Board);
                    this._straightFlopHand = new DrawingHand.Straight(table.Board);

                    this._network.SetEvidence(typeof(Board.BoardFlop).Name, this._flopBoard.ToString());
                    this._network.SetEvidence(typeof(NumberPlayerSitIn.FlopNumberPlayerSitIn).Name, this._flopNumberPlayer.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.Flush).Name, this._flushFlopHand.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.Straight).Name, this._straightFlopHand.ToString());
                }
                if (maxStreet >= HandHistories.Objects.Cards.Street.Turn)
                {
                    if (handAction.Any(p => p.PlayerName == name && p.Street == maxStreet))
                    {
                        this._turnAction = new Action.ActionTurn(handAction, name);
                        this._turnPosition = new Position.PositionTurn(handAction, name);
                        this._turnPotOdds = new PotOdds.PotOddsTurn(handAction, name);

                        this._network.SetEvidence(typeof(Position.PositionTurn).Name, this._turnPosition.ToString());
                        this._network.SetEvidence(typeof(PotOdds.PotOddsTurn).Name, this._turnPotOdds.ToString());
                        this._network.SetEvidence(typeof(Action.ActionTurn).Name, this._turnAction.ToString());

                    }
                    this._turnBoard = new Board.BoardChangeTurn(table.Board);
                    //this._turnDrawingHand = new DrawingHand.DrawingHandTurn(table.Board, player.Hand);
                    //this._turnHandType = new HandType.HandTypeTurn(table.Board, player.Hand);
                    this._turnNumberPlayer = new NumberPlayerSitIn.TurnNumberPlayerSitIn(handAction);
                    this._flushTurnHand = new DrawingHand.FlushChangeTurn(table.Board);
                    this._straightTurnHand = new DrawingHand.StraightChangeTurn(table.Board);

                    this._network.SetEvidence(typeof(Board.BoardChangeTurn).Name, this._turnBoard.ToString());
                    this._network.SetEvidence(typeof(NumberPlayerSitIn.TurnNumberPlayerSitIn).Name, this._turnNumberPlayer.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.FlushChangeTurn).Name, this._flushTurnHand.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.StraightChangeTurn).Name, this._straightTurnHand.ToString());
                }
                if (maxStreet >= HandHistories.Objects.Cards.Street.River)
                {

                    if (handAction.Any(p => p.PlayerName == name && p.Street == maxStreet))
                    {
                        this._riverAction = new Action.ActionRiver(handAction, name);
                        this._riverPosition = new Position.PositionRiver(handAction, name);
                        this._riverPotOdds = new PotOdds.PotOddsRiver(handAction, name);

                        this._network.SetEvidence(typeof(Action.ActionRiver).Name, this._riverAction.ToString());
                        this._network.SetEvidence(typeof(Position.PositionRiver).Name, this._riverPosition.ToString());
                        this._network.SetEvidence(typeof(PotOdds.PotOddsRiver).Name, this._riverPotOdds.ToString());
                    }
                    this._riverBoard = new Board.BoardChangeRiver(table.Board);
                    //this._riverHandType = new HandType.HandTypeRiver(table.Board, player.Hand);
                    this._riverNumberPlayer = new NumberPlayerSitIn.RiverNumberPlayerSitIn(handAction);
                    this._flushRiverHand = new DrawingHand.FlushChangeRiver(table.Board);
                    this._straightRiverHand = new DrawingHand.StraightChangeRiver(table.Board);

                    this._network.SetEvidence(typeof(Board.BoardChangeRiver).Name, this._riverBoard.ToString());
                    this._network.SetEvidence(typeof(NumberPlayerSitIn.RiverNumberPlayerSitIn).Name, this._riverNumberPlayer.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.FlushChangeRiver).Name, this._flushRiverHand.ToString());
                    this._network.SetEvidence(typeof(DrawingHand.StraightChangeRiver).Name, this._straightRiverHand.ToString());
                }

                this._network.UpdateBeliefs();
                double[] result = null;
                Dictionary<String, double> returnValue = new Dictionary<string, double>();
                String toAnalyze = null;
                if (maxStreet == HandHistories.Objects.Cards.Street.Preflop)
                {
                    toAnalyze = typeof(HandGroup.HandGroup).Name;
                } 
                else if (maxStreet == HandHistories.Objects.Cards.Street.Flop)
                {
                    toAnalyze = typeof(HandType.HandType).Name;
                } 
                else if (maxStreet == HandHistories.Objects.Cards.Street.Turn)
                {
                    toAnalyze = typeof(HandType.HandTypeTurn).Name;
                } 
                else if (maxStreet >= HandHistories.Objects.Cards.Street.River)
                {
                    toAnalyze = typeof(HandType.HandTypeRiver).Name;
                }

                result = this._network.GetNodeValue(toAnalyze);
                String[] aSuccessOutcomeIds = this._network.GetOutcomeIds(toAnalyze);
                for (int outcomeIndex = 0; outcomeIndex < aSuccessOutcomeIds.Length; outcomeIndex++)
                {
                    returnValue.Add(aSuccessOutcomeIds[outcomeIndex], result[outcomeIndex]);
                }

                return returnValue.OrderByDescending(p => p.Value);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void setTableForTraining(Table table,Player player)
        {
            List<HandHistories.Objects.Actions.HandAction> handAction = table.HandHistory.HandActions;
            HandHistories.Objects.Cards.Street maxStreet = HandHistories.Objects.Cards.Street.Null;

            if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Showdown))
            {
                maxStreet = HandHistories.Objects.Cards.Street.Showdown;
            }
            else if(handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.River))
            {
                maxStreet = HandHistories.Objects.Cards.Street.River;
            }
            else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Turn))
            {
                maxStreet = HandHistories.Objects.Cards.Street.Turn;
            }
            else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Flop))
            {
                maxStreet = HandHistories.Objects.Cards.Street.Flop;
            }
            else if (handAction.Any(p => p.Street == HandHistories.Objects.Cards.Street.Preflop))
            {
                maxStreet = HandHistories.Objects.Cards.Street.Preflop;
            }
            //INIT STAT PLAYER
            String name = player.Name;
            Entity.Player.Stats stats = player.Stats;
            this._pfr = new Stats.PFR(stats.Pfr);
            this._vpip = new Stats.VPIP(stats.Vpip);
            this._foldToThreeBet = new Stats.FoldToThreeBet(stats.FoldToThreeBet);
            this._threeBet = new Stats.ThreeBet(stats.ThreeBetPF);

            //Init PREFLOP ACTION
            if (maxStreet >= HandHistories.Objects.Cards.Street.Preflop)
            {
                this._pfAction = new Action.Action(handAction, name);
                this._pfNumberPlayer = new NumberPlayerSitIn.NumberPlayerSitIn(handAction);
                this._pfPosition = new Position.Position(handAction, name);
                this._pfPotOdds = new PotOdds.PotOdds(handAction, name);
                this._handGroup = new HandGroup.HandGroup(player.Hand);
            }
            if (maxStreet >= HandHistories.Objects.Cards.Street.Flop)
            {
                this._flopAction = new Action.ActionFlop(handAction, name);
                this._flopBoard = new Board.BoardFlop(table.Board);
                this._flopDrawingHand = new DrawingHand.DrawingHand(table.Board, player.Hand);
                this._flopHandType = new HandType.HandType(table.Board, player.Hand);
                this._flopNumberPlayer = new NumberPlayerSitIn.FlopNumberPlayerSitIn(handAction);
                this._flopPosition = new Position.PositionFlop(handAction, name);
                this._flopPotOdds = new PotOdds.PotOddsFlop(handAction, name);
                this._flushFlopHand = new DrawingHand.Flush(table.Board);
                this._straightFlopHand = new DrawingHand.Straight(table.Board);
            }
            if (maxStreet >= HandHistories.Objects.Cards.Street.Turn)
            {
                this._turnAction = new Action.ActionTurn(handAction, name);
                this._turnBoard = new Board.BoardChangeTurn(table.Board);
                this._turnDrawingHand = new DrawingHand.DrawingHandTurn(table.Board, player.Hand);
                this._turnHandType = new HandType.HandTypeTurn(table.Board, player.Hand);
                this._turnNumberPlayer = new NumberPlayerSitIn.TurnNumberPlayerSitIn(handAction);
                this._turnPosition = new Position.PositionTurn(handAction, name);
                this._turnPotOdds = new PotOdds.PotOddsTurn(handAction, name);
                this._flushTurnHand = new DrawingHand.FlushChangeTurn(table.Board);
                this._straightTurnHand = new DrawingHand.StraightChangeTurn(table.Board);
            } 
            if (maxStreet >= HandHistories.Objects.Cards.Street.River)
            {
                this._riverAction = new Action.ActionRiver(handAction, name);
                this._riverBoard = new Board.BoardChangeRiver(table.Board);
                this._riverHandType = new HandType.HandTypeRiver(table.Board, player.Hand);
                this._riverNumberPlayer = new NumberPlayerSitIn.RiverNumberPlayerSitIn(handAction);
                this._riverPosition = new Position.PositionRiver(handAction, name);
                this._riverPotOdds = new PotOdds.PotOddsRiver(handAction, name);
                this._flushRiverHand = new DrawingHand.FlushChangeRiver(table.Board);
                this._straightRiverHand = new DrawingHand.StraightChangeRiver(table.Board);
            }
        }

        public List<String> getValue()
        {
            List<String> list = new List<string>();
            list.Add(this._pfAction.ToString());
            list.Add(this._flopAction.ToString());
            list.Add(this._turnAction.ToString());
            list.Add(this._riverAction.ToString());
            list.Add(this._flopBoard.ToString());
            list.Add(this._turnBoard.ToString());
            list.Add(this._riverBoard.ToString());
            list.Add(this._flopDrawingHand.ToString());
            list.Add(this._turnDrawingHand.ToString());
            list.Add(this._flushFlopHand.ToString());
            list.Add(this._flushTurnHand.ToString());
            list.Add(this._flushRiverHand.ToString());
            list.Add(this._straightFlopHand.ToString());
            list.Add(this._straightTurnHand.ToString());
            list.Add(this._straightRiverHand.ToString());
            list.Add(this._handGroup.ToString());
            list.Add(this._flopHandType.ToString());
            list.Add(this._turnHandType.ToString());
            list.Add(this._riverHandType.ToString());
            list.Add(this._pfNumberPlayer.ToString());
            list.Add(this._flopNumberPlayer.ToString());
            list.Add(this._turnNumberPlayer.ToString());
            list.Add(this._riverNumberPlayer.ToString());
            list.Add(this._pfPosition.ToString());
            list.Add(this._flopPosition.ToString());
            list.Add(this._turnPosition.ToString());
            list.Add(this._riverPosition.ToString());
            list.Add(this._pfPotOdds.ToString());
            list.Add(this._flopPotOdds.ToString());
            list.Add(this._turnPotOdds.ToString());
            list.Add(this._riverPotOdds.ToString());
            list.Add(this._vpip.ToString());
            list.Add(this._pfr.ToString());
            list.Add(this._threeBet.ToString());
            list.Add(this._foldToThreeBet.ToString());
            return list;
        }

        public static List<String> getHeader()
        {
            List<String> list = new List<string>();
            foreach (Type addingClass in getAllClassForNetworkCreation())
            {
                list.Add(addingClass.Name);
            }
            return list;
        }

        public static List<Type> getAllClassForNetworkCreation()
        {
            return new List<Type>()
            {
                typeof(Action.Action),
                typeof(Action.ActionFlop),
                typeof(Action.ActionRiver),
                typeof(Action.ActionTurn),
                typeof(Board.BoardFlop),
                typeof(Board.BoardChangeTurn),
                typeof(Board.BoardChangeRiver),
                typeof(DrawingHand.DrawingHand),
                typeof(DrawingHand.DrawingHandTurn),
                typeof(DrawingHand.Flush),
                typeof(DrawingHand.FlushChangeTurn),
                typeof(DrawingHand.FlushChangeRiver),
                typeof(DrawingHand.Straight),
                typeof(DrawingHand.StraightChangeTurn),
                typeof(DrawingHand.StraightChangeRiver),
                typeof(HandGroup.HandGroup),
                typeof(HandType.HandType),
                typeof(HandType.HandTypeTurn),
                typeof(HandType.HandTypeRiver),
                typeof(NumberPlayerSitIn.NumberPlayerSitIn),
                typeof(NumberPlayerSitIn.FlopNumberPlayerSitIn),
                typeof(NumberPlayerSitIn.TurnNumberPlayerSitIn),
                typeof(NumberPlayerSitIn.RiverNumberPlayerSitIn),
                typeof(Position.Position),
                typeof(Position.PositionFlop),
                typeof(Position.PositionTurn),
                typeof(Position.PositionRiver),
                typeof(PotOdds.PotOdds),
                typeof(PotOdds.PotOddsFlop),
                typeof(PotOdds.PotOddsTurn),
                typeof(PotOdds.PotOddsRiver),
                typeof(Stats.VPIP),
                typeof(Stats.PFR),
                typeof(Stats.ThreeBet),
                typeof(Stats.FoldToThreeBet),
            };
        }

        public void Dispose()
        {
            if (this._network != null)
            {
                this._network.Dispose();
            }
        }
    }
}
