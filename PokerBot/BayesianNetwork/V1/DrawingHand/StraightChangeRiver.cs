using PokerBot.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class StraightChangeRiver :BaseStraightFlush
    {
        public StraightChangeRiver(Entity.Table.Board board)
            : base(board.getBoardFromRiver())
        {
            StraightChangeTurn boardTurn = new StraightChangeTurn(board);

            int[] boardIntValue = this._board.getTwoPlusTwoHandEvaluatorEquivalent();

            if (boardTurn.Value == StateFlushStraightEnumType.Draw_Draw
                || boardTurn.Value == StateFlushStraightEnumType.None_Draw)
            {
                bool possible = false;
                if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue).hand == Entity.Hand.PokerHand.Straight)
                {

                    this.Value = StateFlushStraightEnumType.Draw_Straight;
                    return;
                }

                var allEvaluator = Range.getAllCombinaisonOneCardWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                possible = false;
                Parallel.ForEach(allEvaluator, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand6(boardIntValue.Union(new int[] { evaluated }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
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
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand7(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
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
                
            }
            else if (boardTurn.Value == StateFlushStraightEnumType.Possible_Likely 
                || boardTurn.Value == StateFlushStraightEnumType.Draw_Likely)
            {
                if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue).hand == Entity.Hand.PokerHand.Straight)
                {

                    this.Value = StateFlushStraightEnumType.Likely_Straight;
                    return;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Likely_Likely;
                }
            }
            else if (boardTurn.Value == StateFlushStraightEnumType.None_None)
            {
                var allEvaluatorTwo = Range.getAllCombinaisonWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                bool possible = false;
                Parallel.ForEach(allEvaluatorTwo, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand7(boardIntValue.Union(new int[] { evaluated.Item1, evaluated.Item2 }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
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
                this.Value = StateFlushStraightEnumType.None_None;
                return;
            }
            else if (boardTurn.Value == StateFlushStraightEnumType.Draw_Possible
               || boardTurn.Value == StateFlushStraightEnumType.None_Possible
               || boardTurn.Value == StateFlushStraightEnumType.Possible_Possible)
            {
                bool possible = false;
                if (TwoPlusTwoHandEvaluator.Instance.LookupHand5(boardIntValue).hand == Entity.Hand.PokerHand.Straight)
                {

                    this.Value = StateFlushStraightEnumType.Possible_Straight;
                    return;
                }

                var allEvaluator = Range.getAllCombinaisonOneCardWithoutCardsSelectedForTwoPlusTwo(boardIntValue);
                possible = false;
                Parallel.ForEach(allEvaluator, (evaluated, state) =>
                {
                    if (TwoPlusTwoHandEvaluator.Instance.LookupHand6(boardIntValue.Union(new int[] { evaluated }).ToArray()).hand == Entity.Hand.PokerHand.Straight)
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
                this.Value = boardTurn.Value;
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
                StateFlushStraightEnumType.Draw_Straight,
                StateFlushStraightEnumType.Likely_Likely,
                StateFlushStraightEnumType.Likely_Straight,
                StateFlushStraightEnumType.None_None,
                StateFlushStraightEnumType.None_Draw,
                StateFlushStraightEnumType.None_Possible,
                StateFlushStraightEnumType.Possible_Possible,
                StateFlushStraightEnumType.Possible_Likely,
                StateFlushStraightEnumType.Possible_Straight
            };
            foreach (StateFlushStraightEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandType.HandTypeRiver).Name };
        }
    }
}
