using PokerBot.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public abstract class BaseDrawingHand : BaseSmileDefinition
    {
        private DrawingHandEnumType _drawingHand;

        public  DrawingHandEnumType DrawingHand
        {
            get { return _drawingHand; }
            set { _drawingHand = value; }
        }

        public BaseDrawingHand(Entity.Table.Board board, Entity.Hand.Hand hand)
        {
            PokerHandEvaluator pokerHandEvaluator = new PokerHandEvaluator();
            var evaluator = new PokerEvaluator(pokerHandEvaluator);
            var enumarable = board.getEvaluator().Union(hand.getEvaluator());
            PokerHandEvaluationResult result = evaluator.EvaluateHand(enumarable);
            if (result.Result == Entity.Hand.PokerHand.Straight || result.Result == Entity.Hand.PokerHand.Flush)
            {
                this._drawingHand = DrawingHandEnumType.DrawHit;
            }
            else if (result.Result == Entity.Hand.PokerHand.RoyalFlush || result.Result == Entity.Hand.PokerHand.StraightFlush)
            {
                this._drawingHand = DrawingHandEnumType.StraightD_FlushD;
            }
            else if (pokerHandEvaluator.PossibilityOfBilateralStraigh(enumarable) || pokerHandEvaluator.PossibilityOfInsideStraigh(enumarable))
            {
                this._drawingHand = DrawingHandEnumType.StraightDraw;
            }
            else if (pokerHandEvaluator.PossibilityFlush(enumarable))
            {
                this._drawingHand = DrawingHandEnumType.FlushDraw;
            }
            else
            {
                this._drawingHand = DrawingHandEnumType.NoDraw;
            }
        }

        public override String ToString()
        {
            return this._drawingHand.ToBayesianNetwork();
        }
    }
}
