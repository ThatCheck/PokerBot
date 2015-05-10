using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class FlushChangeRiver: BaseStraightFlush
    {
        private FlushChangeTurn _flushTurn;
        public FlushChangeRiver(Entity.Table.Board board)
            : base(board.getBoardFromRiver())
        {
            this._flushTurn = new FlushChangeTurn(board);

            var riverBoard = this._board.getEvaluator().GroupBy(c => c.Suit);
            this.Value = this._flushTurn.Value;
            if (this._flushTurn.Value == StateFlushStraightEnumType.Draw_Draw)
            {
                if (riverBoard.Where(g => g.Count() == 3).Any())
                {
                    this.Value = StateFlushStraightEnumType.Draw_Possible;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Draw_Draw;
                }
            }
            else if (this._flushTurn.Value == StateFlushStraightEnumType.Draw_Possible)
            {
                if (riverBoard.Where(g => g.Count() >= 4).Any())
                {
                    this.Value = StateFlushStraightEnumType.Possible_Likely;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Draw_Possible;
                }
            }
            else if (this._flushTurn.Value == StateFlushStraightEnumType.None_Draw)
            {
                if (riverBoard.Where(g => g.Count() == 3).Any())
                {
                    this.Value = StateFlushStraightEnumType.Draw_Possible;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Draw_Draw;
                }
            }
            else if (this._flushTurn.Value == StateFlushStraightEnumType.None_None)
            {
                    this.Value = StateFlushStraightEnumType.None_Draw;
            }
            else if (this._flushTurn.Value == StateFlushStraightEnumType.Possible_Likely)
            {
                if (riverBoard.Where(g => g.Count() == 5).Any())
                {
                    this.Value = StateFlushStraightEnumType.Likely_Flush;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Likely_Likely;
                }
            }
            else if (this._flushTurn.Value == StateFlushStraightEnumType.Possible_Possible)
            {
                if (riverBoard.Where(g => g.Count() == 4).Any())
                {
                    this.Value = StateFlushStraightEnumType.Possible_Likely;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Possible_Possible;
                }
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
                StateFlushStraightEnumType.None_Draw,
                StateFlushStraightEnumType.Draw_Possible,
                StateFlushStraightEnumType.Likely_Flush,
                StateFlushStraightEnumType.Likely_Likely,
                StateFlushStraightEnumType.Possible_Likely,
                StateFlushStraightEnumType.Possible_Possible
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
