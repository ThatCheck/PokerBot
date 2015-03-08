using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class FlushChangeTurn : BaseStraightFlush
    {
        private Flush _flushFlop;
        public FlushChangeTurn(Entity.Table.Board board)
            : base(board.getBoardFromTurn())
        {
            this._flushFlop = new Flush(board);
            var flopBoard = this._board.getEvaluator().GroupBy(c => c.Suit);
            if (this._flushFlop.Value == StateFlushStraightEnumType.None)
            {
                if (flopBoard.Where(g => g.Count() == 2).Any())
                {
                    this.Value = StateFlushStraightEnumType.None_Draw;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.None_None;
                }
            }
            else if (this._flushFlop.Value == StateFlushStraightEnumType.Draw)
            {
                if (flopBoard.Where(g => g.Count() == 3).Any())
                {
                    this.Value = StateFlushStraightEnumType.Draw_Possible;
                }
                else
                {
                    this.Value = StateFlushStraightEnumType.Draw_Draw;
                }
            }

            else if (this._flushFlop.Value == StateFlushStraightEnumType.Possible)
            {
                if (flopBoard.Where(g => g.Count() == 4).Any())
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
                StateFlushStraightEnumType.None_None,
                StateFlushStraightEnumType.Possible_Likely,
                StateFlushStraightEnumType.Possible_Possible,
                StateFlushStraightEnumType.Likely_Flush,
                StateFlushStraightEnumType.Likely_Likely
            };
            foreach (StateFlushStraightEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(DrawingHandTurn).Name, typeof(FlushChangeRiver).Name };
        }
    }
}
