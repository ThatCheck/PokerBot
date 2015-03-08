using PokerBot.Entity.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.HandType
{
    public class HandTypeRiver: BaseHandType
    {
        public HandTypeRiver(Entity.Table.Board board, Entity.Hand.Hand hand) : base(board.getBoardFromRiver(),hand)
        {

        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            HandTypeEnumType[] enumArray = new HandTypeEnumType[]
            {
                HandTypeEnumType.AcePairStrong,
                HandTypeEnumType.AcePairWeak,
                HandTypeEnumType.Busted,
                HandTypeEnumType.Flush,
                HandTypeEnumType.FourOfAKind,
                HandTypeEnumType.FullHouse,
                HandTypeEnumType.JackPair,
                HandTypeEnumType.KingPairStrong,
                HandTypeEnumType.KingPairWeak,
                HandTypeEnumType.LowPair,
                HandTypeEnumType.MidPair,
                HandTypeEnumType.QueenPair,
                HandTypeEnumType.Straight,
                HandTypeEnumType.StraightFlush,
                HandTypeEnumType.TenPair,
                HandTypeEnumType.ThreeOfAKind,
                HandTypeEnumType.TwoPair
            };
            foreach (HandTypeEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] {};
        }
    }
}
