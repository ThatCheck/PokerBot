using HandHistories.Objects.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class Flush : BaseStraightFlush
    {
        public Flush(Entity.Table.Board board) : base(board.getBoardFromFlop())
        {
            int flopBoard = this._board.getEvaluator().GroupBy(c => c.Suit).Count();
            if (flopBoard == 1)
            {
                this.Value = StateFlushStraightEnumType.Possible;
            }
            else if (flopBoard == 2)
            {
                this.Value = StateFlushStraightEnumType.Draw;
            }
            else
            {
                this.Value = StateFlushStraightEnumType.None;
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
            return new String[] { typeof(DrawingHand).Name, typeof(FlushChangeTurn).Name };
        }
    }
}
