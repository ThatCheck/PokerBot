using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class Straight : BaseStraightFlush
    {
        public Straight(Entity.Table.Board board)
            : base(board.getBoardFromFlop())
        {
            int[] boardIntValue = this._board.getTwoPlusTwoHandEvaluatorEquivalent();
            var allEvaluator = Range.getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
            this.Value = StateFlushStraightEnumType.None;

            bool possible = false;
            Parallel.ForEach(allEvaluator, (evaluated,state) => 
            {
                if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                {
                    possible = true;
                    state.Break();
                }
            });

            if (possible)
            {
                this.Value = StateFlushStraightEnumType.Possible;
                return;
            }

            bool draw = false;
            var allEvaluatorThree = Range.getAllCombinaisonThreeCardsWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
            Parallel.ForEach(allEvaluatorThree, (evaluated,state) =>
            {
                if (TwoPlusTwoHandEvaluator.Instance.LookupHand6(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2, evaluated.Item3 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                {
                    draw = true;
                    state.Break();
                }
            });

            if (draw)
            {
                this.Value = StateFlushStraightEnumType.Draw;
                return;
            }
        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            StateFlushStraightEnumType[] enumArray = new StateFlushStraightEnumType[]
            {
                StateFlushStraightEnumType.None,
                StateFlushStraightEnumType.Possible,
                StateFlushStraightEnumType.Draw
            };
            foreach (StateFlushStraightEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(DrawingHand).Name, typeof(StraightChangeTurn).Name };
        }
    }
}
