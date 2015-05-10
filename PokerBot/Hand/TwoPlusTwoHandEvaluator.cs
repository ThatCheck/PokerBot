using PokerBot.Entity.Card;
using PokerBot.Entity.Hand;
using PokerBot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Hand
{
    public struct TwoPlusTwoHandEvaluatorResult : IComparable 
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

        public int CompareTo(object obj)
        {
            TwoPlusTwoHandEvaluatorResult hand = (TwoPlusTwoHandEvaluatorResult)obj;

            if (hand.handType == this.handType)
            {
                if (this.handRank < hand.handRank)
                {
                    return -1;
                }
                else if (this.handRank > hand.handRank)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (this.handType < hand.handType)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static bool operator <(TwoPlusTwoHandEvaluatorResult e1, TwoPlusTwoHandEvaluatorResult e2)
        {
            return e1.CompareTo(e2) < 0;
        }

        public static bool operator >(TwoPlusTwoHandEvaluatorResult e1, TwoPlusTwoHandEvaluatorResult e2)
        {
            return e1.CompareTo(e2) > 0;
        }

        public static bool operator ==(TwoPlusTwoHandEvaluatorResult e1, TwoPlusTwoHandEvaluatorResult e2)
        {
            return e1.CompareTo(e2) == 0;
        }

        public static bool operator !=(TwoPlusTwoHandEvaluatorResult e1, TwoPlusTwoHandEvaluatorResult e2)
        {
            return e1.CompareTo(e2) != 0;
        }

        public override bool Equals(Object obj)
        {
            return this.CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
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
            if (!File.Exists("./OnTheFly/HandRanks.dat"))
            {
                Stream data = new MemoryStream(PokerBot.Properties.Resources.HandRanks);
                Directory.CreateDirectory("./OnTheFly");
                ZipArchive archive = new ZipArchive(data);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile("./OnTheFly/HandRanks.dat"); // .Open will return a stream
                }
            }
            BinaryReader reader = new BinaryReader(File.OpenRead("./OnTheFly/HandRanks.dat"));
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

        public void init()
        {

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

        public TwoPlusTwoHandEvaluatorResult Lookup(int[] cards) // to get a hand strength
        {
            if (cards.Length == 7)
                return LookupHand7(cards);
            else if (cards.Length == 6)
                return LookupHand6(cards);
            else if (cards.Length == 5)
                return LookupHand5(cards);

            throw new ArgumentException("Unable to choose a lookup table with : " +cards.Length + " cards");
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
