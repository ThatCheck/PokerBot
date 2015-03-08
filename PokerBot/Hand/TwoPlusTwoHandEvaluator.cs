using PokerBot.Entity.Card;
using PokerBot.Entity.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public struct TwoPlusTwoHandEvaluatorResult
    {
        public PokerHand hand;
        public int handType;
        public int handRank;
        public int value;

        public TwoPlusTwoHandEvaluatorResult(PokerHand hand, int handType, int handRank, int value)
        {
            this.hand = hand;
            this.handType = handType;
            this.handRank = handRank;
            this.value = value;
        }
    }

    public sealed class TwoPlusTwoHandEvaluator
    {
        private static volatile TwoPlusTwoHandEvaluator instance;
        private static object syncRoot = new Object();
        public static int[] _lut;
        private const int maxSize = 32487834;
        private static List<PokerHand> handTypeList = new List<PokerHand>() { PokerHand.Bad, PokerHand.HighCard, PokerHand.Pair, PokerHand.TwoPair, PokerHand.ThreeOfKind, PokerHand.Straight, PokerHand.Flush, PokerHand.FullHouse, PokerHand.FourOfKind, PokerHand.StraightFlush }; 
        private static Dictionary<String,int> _handEquivalentList = new Dictionary<string, int>()
            {
                {"2c", 1},
                {"2d", 2},
                {"2h", 3},
                {"2s", 4},
                {"3c", 5},
                {"3d", 6},
                {"3h", 7},
                {"3s", 8},
                {"4c", 9},
                {"4d", 10},
                {"4h", 11},
                {"4s", 12},
                {"5c", 13},
                {"5d", 14},
                {"5h", 15},
                {"5s", 16},
                {"6c", 17},
                {"6d", 18},
                {"6h", 19},
                {"6s", 20},
                {"7c", 21},
                {"7d", 22},
                {"7h", 23},
                {"7s", 24},
                {"8c", 25},
                {"8d", 26},
                {"8h", 27},
                {"8s", 28},
                {"9c", 29},
                {"9d", 30},
                {"9h", 31},
                {"9s", 32},
                {"tc", 33},
                {"td", 34},
                {"th", 35},
                {"ts", 36},
                {"jc", 37},
                {"jd", 38},
                {"jh", 39},
                {"js", 40},
                {"qc", 41},
                {"qd", 42},
                {"qh", 43},
                {"qs", 44},
                {"kc", 45},
                {"kd", 46},
                {"kh", 47},
                {"ks", 48},
                {"ac", 49},
                {"ad", 50},
                {"ah", 51},
                {"as", 52}
            };

        public static Dictionary<String, int> HandEquivalentList
        {
            get { return TwoPlusTwoHandEvaluator._handEquivalentList; }
        }

        private TwoPlusTwoHandEvaluator() 
        {

            BinaryReader reader = new BinaryReader(new MemoryStream(PokerBot.Properties.Resources.HandRanks));
            try
            {
                _lut = new int[maxSize];
                var tempBuffer = reader.ReadBytes(maxSize * 4);
                Buffer.BlockCopy(tempBuffer, 0, _lut, 0, maxSize * 4);
            }
            finally
            {
                reader.Close();
            }
        }

        public static TwoPlusTwoHandEvaluator Instance
        {
            get 
            {
                if (instance == null) 
                {
                    lock (syncRoot) 
                    {
                        if (instance == null)
                            instance = new TwoPlusTwoHandEvaluator();
                    }
                }
                return instance;
            }
        }

        public TwoPlusTwoHandEvaluatorResult LookupHand7(int[] cards) // to get a hand strength
        {
            int p = _lut[53 + cards[0]];
            p = _lut[p + cards[1]];
            p = _lut[p + cards[2]];
            p = _lut[p + cards[3]];
            p = _lut[p + cards[4]];
            p = _lut[p + cards[5]];
            return this.getResultStruct(_lut[p + cards[6]]);
        }

        public TwoPlusTwoHandEvaluatorResult LookupHand5(int[] cards) // to get a hand strength
        {
            int p = _lut[53 + cards[0]];
            p = _lut[p + cards[1]];
            p = _lut[p + cards[2]];
            p = _lut[p + cards[3]];
            p = _lut[p + cards[4]];
            return this.getResultStruct(_lut[p]);
        }

        public TwoPlusTwoHandEvaluatorResult LookupHand6(int[] cards) // to get a hand strength
        {
            int p = _lut[53 + cards[0]];
            p = _lut[p + cards[1]];
            p = _lut[p + cards[2]];
            p = _lut[p + cards[3]];
            p = _lut[p + cards[4]];
            p = _lut[p + cards[5]];
            return this.getResultStruct(_lut[p]);
        }

        public static int transformIntoInt(PlayingCard card)
        {
            return _handEquivalentList[(NominalValueConverter.NormalizeFormat(card.NominalValue) + CardSuitConverter.NormalizeFormat(card.Suit)).ToLower()];
        }

        private TwoPlusTwoHandEvaluatorResult getResultStruct(int p)
        {
            return new TwoPlusTwoHandEvaluatorResult(handTypeList[p >> 12], p >> 12, p & 0x00000fff, p);
        }
    }
}
