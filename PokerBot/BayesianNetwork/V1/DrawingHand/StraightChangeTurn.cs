using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class StraightChangeTurn : BaseStraightFlush
    {
        public StraightChangeTurn(Entity.Table.Board board)
            : base(board.getBoardFromTurn())
        {
            Straight boardFlop = new Straight(board.getBoardFromFlop());
            if (boardFlop.Value == StateFlushStraightEnumType.Draw)
            {
                int[] boardIntValue = this._board.getTwoPlusTwoHandEvaluatorEquivalent();
                var allEvaluator = Range.getAllCombinaisonOneCardWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                bool possible = false;
                Parallel.ForEach(allEvaluator, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue.Union(new int[] {evaluated}).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                    {
                        possible = true;
                        state.Break();
                    }
                });

                if (possible)
                {
                    this.Value = StateFlushStraightEnumType.Draw_Likely;
                    return;
                }

                var allEvaluatorTwo = Range.getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                possible = false;
                Parallel.ForEach(allEvaluatorTwo, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand6(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                    {
                        possible = true;
                        state.Break();
                    }
                });

                if (possible)
                {
                    this.Value = StateFlushStraightEnumType.Draw_Possible;
                    return;
                }

                this.Value = StateFlushStraightEnumType.Draw_Draw;
                return;
            }
            else if (boardFlop.Value == StateFlushStraightEnumType.Possible)
            {
                int[] boardIntValue = this._board.getTwoPlusTwoHandEvaluatorEquivalent();
                var allEvaluator = Range.getAllCombinaisonOneCardWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                bool possible = false;
                Parallel.ForEach(allEvaluator, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue.Union(new int[] { evaluated }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                    {
                        possible = true;
                        state.Break();
                    }
                });

                if (possible)
                {
                    this.Value = StateFlushStraightEnumType.Possible_Likely;
                    return;
                }

                this.Value = StateFlushStraightEnumType.Possible_Possible;
                return;
            }
            else
            {
                int[] boardIntValue = this._board.getTwoPlusTwoHandEvaluatorEquivalent();
                var allEvaluator = Range.getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                this.Value = StateFlushStraightEnumType.None;

                bool possible = false;
                Parallel.ForEach(allEvaluator, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand6(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                    {
                        possible = true;
                        state.Break();
                    }
                });

                if (possible)
                {
                    this.Value = StateFlushStraightEnumType.None_Possible;
                    return;
                }

                bool draw = false;
                var allEvaluatorThree = Range.getAllCombinaisonThreeCardsWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                Parallel.ForEach(allEvaluatorThree, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand7(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2, evaluated.Item3 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
                    {
                        draw = true;
                        state.Break();
                    }
                });

                if (draw)
                {
                    this.Value = StateFlushStraightEnumType.None_Draw;
                    return;
                }
                this.Value = StateFlushStraightEnumType.None_None;
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
                StateFlushStraightEnumType.Draw_Draw,
                StateFlushStraightEnumType.Draw_Likely,
                StateFlushStraightEnumType.Draw_Possible,
                StateFlushStraightEnumType.None_None,
                StateFlushStraightEnumType.None_Draw,
                StateFlushStraightEnumType.None_Possible,
                StateFlushStraightEnumType.Possible_Possible,
                StateFlushStraightEnumType.Possible_Likely
            };
            foreach (StateFlushStraightEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(DrawingHandTurn).Name, typeof(StraightChangeRiver).Name };
        }
    }
}
