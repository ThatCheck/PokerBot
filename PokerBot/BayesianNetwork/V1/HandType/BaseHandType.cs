using PokerBot.Comparer;
using PokerBot.Entity.Card;
using PokerBot.Entity.Table;
using PokerBot.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandType
{
    public abstract class BaseHandType : BaseSmileDefinition
    {
        private Entity.Table.Board _board;
        private Entity.Hand.Hand _hand;
        private HandTypeEnumType _handTypeEnum;

        public HandTypeEnumType HandTypeEnum
        {
            get { return _handTypeEnum; }
            set { _handTypeEnum = value; }
        }
        public Entity.Hand.Hand Hand
        {
            get { return _hand; }
            set { _hand = value; }
        }

        public Entity.Table.Board Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public BaseHandType(Entity.Table.Board board, Entity.Hand.Hand hand)
        {
            this._board = board;
            this._hand = hand;

            HashSet<PlayingCard> cards = new HashSet<PlayingCard>(this._hand.getEvaluator());
            cards.UnionWith(this._board.getEvaluator());
            PokerEvaluator pokerEvaluator = new PokerEvaluator(new PokerHandEvaluator());
            PokerHandEvaluationResult result = pokerEvaluator.EvaluateHand(cards);
            if (result.Result == Entity.Hand.PokerHand.Flush)
            {
                this.HandTypeEnum = HandTypeEnumType.Flush;
            }
            else if (result.Result == Entity.Hand.PokerHand.FourOfKind)
            {
                this.HandTypeEnum = HandTypeEnumType.FourOfAKind;
            }
            else if (result.Result == Entity.Hand.PokerHand.FullHouse)
            {
                this.HandTypeEnum = HandTypeEnumType.FullHouse;
            }
            else if (result.Result == Entity.Hand.PokerHand.RoyalFlush)
            {
                this.HandTypeEnum = HandTypeEnumType.StraightFlush;
            }
            else if (result.Result == Entity.Hand.PokerHand.Straight)
            {
                this.HandTypeEnum = HandTypeEnumType.Straight;
            }
            else if (result.Result == Entity.Hand.PokerHand.StraightFlush)
            {
                this.HandTypeEnum = HandTypeEnumType.StraightFlush;
            }
            else if (result.Result == Entity.Hand.PokerHand.ThreeOfKind)
            {
                this.HandTypeEnum = HandTypeEnumType.ThreeOfAKind;
            }
            else if (result.Result == Entity.Hand.PokerHand.TwoPair)
            {
                this.HandTypeEnum = HandTypeEnumType.TwoPair;
            }
            else if (result.Result == Entity.Hand.PokerHand.Pair)
            {
                var kicker = cards.Distinct(new PlayingCardNominalValueComparer()).OrderByDescending(p => p.NominalValue).ElementAt(1);
                if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ace))
                {
                    if (kicker.NominalValue == PlayingCardNominalValue.King ||
                        kicker.NominalValue == PlayingCardNominalValue.Queen ||
                        kicker.NominalValue == PlayingCardNominalValue.Jack ||
                        kicker.NominalValue == PlayingCardNominalValue.Ten)
                    {
                        this.HandTypeEnum = HandTypeEnumType.AcePairStrong;
                    }
                    else
                    {
                        this.HandTypeEnum = HandTypeEnumType.AcePairWeak;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.King))
                {
                    if (kicker.NominalValue == PlayingCardNominalValue.Queen ||
                        kicker.NominalValue == PlayingCardNominalValue.Jack ||
                        kicker.NominalValue == PlayingCardNominalValue.Ten)
                    {
                        this.HandTypeEnum = HandTypeEnumType.KingPairStrong;
                    }
                    else
                    {
                        this.HandTypeEnum = HandTypeEnumType.KingPairWeak;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Queen))
                {
                    this.HandTypeEnum = HandTypeEnumType.QueenPair;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Jack))
                {
                    this.HandTypeEnum = HandTypeEnumType.Flush;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ten || p.NominalValue == PlayingCardNominalValue.Nine || p.NominalValue == PlayingCardNominalValue.Eight))
                {
                    this.HandTypeEnum = HandTypeEnumType.MidPair;
                }
                else
                {
                    this.HandTypeEnum = HandTypeEnumType.LowPair;
                }
            }
            else 
            {
                this.HandTypeEnum = HandTypeEnumType.Busted;
            }
        }

        public override String ToString()
        {
            return this.HandTypeEnum.ToBayesianNetwork();
        }
    }
}
