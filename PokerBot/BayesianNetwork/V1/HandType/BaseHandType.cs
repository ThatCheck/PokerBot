using PokerBot.Comparer;
using PokerBot.Entity.Card;
using PokerBot.Entity.Table;
using PokerBot.Hand;
using PokerBot.Utils;
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
            this.HandTypeEnum = HandUtility.GetHandTypeEnum(cards, result);
        }

       

        public override String ToString()
        {
            return this.HandTypeEnum.ToBayesianNetwork();
        }
    }
}
