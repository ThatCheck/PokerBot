using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.DrawingHand
{
    public class DrawingHand : BaseDrawingHand
    {
        public DrawingHand(Entity.Table.Board board, Entity.Hand.Hand hand)
            : base(board.getBoardFromFlop(), hand)
        {

        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            DrawingHandEnumType[] enumArray = new DrawingHandEnumType[]
            {
                DrawingHandEnumType.DrawHit,
                DrawingHandEnumType.FlushDraw,
                DrawingHandEnumType.NoDraw,
                DrawingHandEnumType.StraightD_FlushD,
                DrawingHandEnumType.StraightDraw
            };
            foreach (DrawingHandEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandType.HandTypeTurn).Name, typeof(DrawingHandTurn).Name };
        }
    }
}
