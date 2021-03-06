﻿using HandHistories.Objects.Actions;
using HandHistories.Objects.Cards;
using HandHistories.Objects.Hand;
using PokerBot.BayesianNetwork.V1.HandType;
using PokerBot.Comparer;
using PokerBot.Entity.Card;
using PokerBot.Entity.Enum;
using PokerBot.Hand;
using PokerBot.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public static class HandUtility
    {
        public static Dictionary<string, int> pfIndex = new Dictionary<string, int>(169, StringComparer.OrdinalIgnoreCase) { { "AA", 0 }, { "KK", 1 }, { "QQ", 2 }, { "AKs", 3 }, { "AKo", 4 }, { "JJ", 5 }, { "TT", 6 }, { "AQs", 7 }, { "AQo", 8 }, { "99", 9 }, { "AJs", 10 }, { "88", 11 }, { "ATs", 12 }, { "KQs", 13 }, { "AJo", 14 }, { "77", 15 }, { "A9s", 16 }, { "ATo", 17 }, { "KJs", 18 }, { "KQo", 19 }, { "A8s", 20 }, { "KTs", 21 }, { "66", 22 }, { "QJs", 23 }, { "QTs", 24 }, { "JTs", 25 }, { "A7s", 26 }, { "A9o", 27 }, { "55", 28 }, { "K9s", 29 }, { "T9s", 30 }, { "J9s", 31 }, { "98s", 32 }, { "44", 33 }, { "33", 34 }, { "A8o", 35 }, { "A6s", 36 }, { "A5s", 37 }, { "A7o", 38 }, { "A4s", 39 }, { "KJo", 40 }, { "K8s", 41 }, { "QJo", 42 }, { "Q9s", 43 }, { "K7s", 44 }, { "22", 45 }, { "A6o", 46 }, { "A3s", 47 }, { "A5o", 48 }, { "A2s", 49 }, { "KTo", 50 }, { "K9o", 51 }, { "QTo", 52 }, { "K6s", 53 }, { "K5s", 54 }, { "T8s", 55 }, { "87s", 56 }, { "J8s", 57 }, { "76s", 58 }, { "97s", 59 }, { "A4o", 60 }, { "A3o", 61 }, { "Q9o", 62 }, { "K4s", 63 }, { "Q8s", 64 }, { "JTo", 65 }, { "K3s", 66 }, { "Q7s", 67 }, { "T9o", 68 }, { "J9o", 69 }, { "Q6s", 70 }, { "98o", 71 }, { "K2s", 72 }, { "65s", 73 }, { "T8o", 74 }, { "J7s", 75 }, { "86s", 76 }, { "75s", 77 }, { "T7s", 78 }, { "Q5s", 79 }, { "A2o", 80 }, { "K8o", 81 }, { "K7o", 82 }, { "K6o", 83 }, { "Q4s", 84 }, { "K5o", 85 }, { "Q8o", 86 }, { "K4o", 87 }, { "Q3s", 88 }, { "Q7o", 89 }, { "Q6o", 90 }, { "K3o", 91 }, { "K2o", 92 }, { "J6s", 93 }, { "J8o", 94 }, { "96s", 95 }, { "Q5o", 96 }, { "Q2s", 97 }, { "J7o", 98 }, { "J5s", 99 }, { "J4s", 100 }, { "T6s", 101 }, { "J6o", 102 }, { "T5s", 103 }, { "T7o", 104 }, { "97o", 105 }, { "85s", 106 }, { "87o", 107 }, { "76o", 108 }, { "86o", 109 }, { "75o", 110 }, { "54s", 111 }, { "64s", 112 }, { "43s", 113 }, { "Q4o", 114 }, { "53s", 115 }, { "65o", 116 }, { "Q3o", 117 }, { "J5o", 118 }, { "54o", 119 }, { "J4o", 120 }, { "95s", 121 }, { "Q2o", 122 }, { "64o", 123 }, { "T6o", 124 }, { "96o", 125 }, { "T5o", 126 }, { "J3s", 127 }, { "T4s", 128 }, { "J2s", 129 }, { "63s", 130 }, { "T3s", 131 }, { "32s", 132 }, { "42s", 133 }, { "52s", 134 }, { "74s", 135 }, { "T2s", 136 }, { "J3o", 137 }, { "84s", 138 }, { "85o", 139 }, { "94s", 140 }, { "93s", 141 }, { "J2o", 142 }, { "92s", 143 }, { "83s", 144 }, { "73s", 145 }, { "82s", 146 }, { "72s", 147 }, { "62s", 148 }, { "95o", 149 }, { "T4o", 150 }, { "T3o", 151 }, { "74o", 152 }, { "84o", 153 }, { "T2o", 154 }, { "94o", 155 }, { "53o", 156 }, { "93o", 157 }, { "63o", 158 }, { "92o", 159 }, { "73o", 160 }, { "83o", 161 }, { "43o", 162 }, { "42o", 163 }, { "82o", 164 }, { "72o", 165 }, { "62o", 166 }, { "52o", 167 }, { "32o", 168 } };

        public static string generateHandBetPattern(IEnumerable<HandAction> handHistory)
        {
            string pattern = "";
            decimal totalCurrentPot = 0;
            Street temporaryStreetTampon = Street.Preflop;
            try
            {
                foreach (HandAction action in handHistory)
                {
                    if (isActionWithAmount(action) && !isPostAction(action))
                    {
                        string hashStep = "";
                        if (temporaryStreetTampon != action.Street && (action.Street == Street.Flop || action.Street == Street.Turn || action.Street == Street.River))
                        {
                            temporaryStreetTampon = action.Street;
                            hashStep += "|";
                        }
                        hashStep += ActionEnumConverter.FromActionEnum(getAction(handHistory, action));
                        pattern += hashStep;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pattern;
        }

        public static ActionEnum getAction(IEnumerable<HandAction> handHistory, HandAction action)
        {
            if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.ALL_IN)
            {
                return ActionEnum.AllIn;
            }
            else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.CALL)
            {
                return ActionEnum.Call;
            }
            else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.FOLD)
            {
                return ActionEnum.Fold;
            }
            else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.CHECK)
            {
                return ActionEnum.Check;
            }
            else if (action.HandActionType == HandHistories.Objects.Actions.HandActionType.BET
                || action.HandActionType == HandHistories.Objects.Actions.HandActionType.RAISE)
            {
                Decimal amountValue = Math.Abs(action.Amount);
                Decimal percentPot = amountValue / handHistory.Sum(p => Math.Abs(p.Amount));
                ActionEnum selectedAction = ActionEnum.Overbet;
                if (percentPot < 0.40M)
                {
                    return ActionEnum.BetQuarter;
                }
                else if (percentPot >= 0.40M && percentPot < 0.60M)
                {
                    return ActionEnum.BetHalf;
                }
                else if (percentPot >= 0.60M && percentPot < 0.85M)
                {
                    return ActionEnum.BetThreeQuarter;
                }
                else if (percentPot >= 0.85M && percentPot <= 1M)
                {
                    return ActionEnum.BetPot;
                }
                return selectedAction;
            }
            throw new ArgumentException("Unable to find a action for action : " + action.HandActionType);
        }

        public static bool isActionWithAmount(HandAction action)
        {
            HandActionType type = action.HandActionType;
            return type == HandActionType.ALL_IN
                || type == HandActionType.ANTE
                || type == HandActionType.BET
                || type == HandActionType.BIG_BLIND
                || type == HandActionType.CALL
                || type == HandActionType.CHECK
                || type == HandActionType.POSTS
                || type == HandActionType.RAISE
                || type == HandActionType.SMALL_BLIND;
        }

        public static bool isPostAction(HandAction action)
        {
            HandActionType type = action.HandActionType;
            return type == HandActionType.BIG_BLIND
                || type == HandActionType.POSTS
                || type == HandActionType.SMALL_BLIND;
        }

        public static bool isFoldAction(HandAction action)
        {
            HandActionType type = action.HandActionType;
            return type == HandActionType.FOLD
                || type == HandActionType.SITTING_OUT
                || type == HandActionType.DISCONNECTED;
        }

        public static int preFlopRank(HoleCards hand)
        {
            Card cardOne = hand[0];
            Card cardTwo = hand[1];
            if (cardOne.Rank == cardTwo.Rank)
            {
                if(pfIndex.ContainsKey(cardOne.Rank + cardTwo.Rank))
                {
                    return pfIndex[cardOne.Rank + cardTwo.Rank];
                }
                else
                {
                    return pfIndex[cardTwo.Rank + cardOne.Rank];
                }
            }
            string suitOne = cardOne.Suit.ToLower();
            string suitTwo = cardTwo.Suit.ToLower();
            if ((suitOne == "d" && suitTwo == "h") || (suitOne == "h" && suitTwo == "d") || (suitOne == "s" && suitTwo == "c") || (suitOne == "c" && suitTwo == "s"))
            {
                if(pfIndex.ContainsKey(cardOne.Rank + cardTwo.Rank + "s"))
                {
                    return pfIndex[cardOne.Rank + cardTwo.Rank + "s"];
                }
                else
                {
                    return pfIndex[cardTwo.Rank + cardOne.Rank + "s"];
                }
            }

            if (pfIndex.ContainsKey(cardOne.Rank + cardTwo.Rank + "o"))
            {
                return pfIndex[cardOne.Rank + cardTwo.Rank + "o"];
            }
            else
            {
                return pfIndex[cardTwo.Rank + cardOne.Rank + "o"];
            }
        }

        public static double handStrenght(Tuple<PlayingCard, PlayingCard> cards, IEnumerable<PlayingCard> boards, List<List<Tuple<PlayingCard, PlayingCard>>> oppsRange, double maxDuration = 1)
        {
            int ahead = 0, tied = 0, behind = 0;
            string ourHand = cards.Item1.getStringCard() + " " + cards.Item2.getStringCard();
            string boardHand = "";
            foreach (PlayingCard hand in boards)
            {
                boardHand += hand.getStringCard() + " ";
            }
            ulong pocket = HoldemHand.Hand.ParseHand(ourHand);
            ulong board = HoldemHand.Hand.ParseHand(boardHand);

            long win = 0, lose = 0, tie = 0;

            // Keep track of time
            double start = HoldemHand.Hand.CurrentTime;
            double duration = maxDuration;

            //Generate the range by range combinations
            //var rangeCombinations = Combinator.AllCombinationsOf(oppsRange.ToArray());
            // Loop for specified time duration
            while ((HoldemHand.Hand.CurrentTime - start) < duration)
            {
                // Player and board info
                ulong rangeMask = HoldemHand.Hand.ParseHand("");
                List<Entity.Hand.Hand> hands = new List<Entity.Hand.Hand>();
                foreach (var rangeOpponent in oppsRange)
                {
                    Entity.Hand.Hand handOpp = null;
                    do
                    {
                        handOpp = new Entity.Hand.Hand(rangeOpponent.PickRandom());
                    } while (hands.Contains(handOpp));
                    hands.Add(handOpp);
                }
                uint playerHandVal = HoldemHand.Hand.Evaluate(pocket | board);
                List<uint> ComponentHandValue = new List<uint>();
                foreach (var hand in hands)
                {
                    ComponentHandValue.Add(HoldemHand.Hand.Evaluate(hand.getMask() | board));
                }
                if(ComponentHandValue.Max() < playerHandVal)
                    win += 1;
                else if(ComponentHandValue.Max() == playerHandVal)
                    tie += 1;
                else
                    lose += 1;
            }

            return (double)(win + (double)tie / 2) / (double)(win + tied + lose);
        }

        public static Tuple<double, double> handPotential(Tuple<PlayingCard, PlayingCard> cards, IEnumerable<PlayingCard> boards, List<List<Tuple<PlayingCard, PlayingCard>>> oppsRange, double maxDuration = 1)
        {
            int[][] hp = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
            int[] hpTotal = new int[] { 0, 0, 0 };
            const int ahead = 2;
            const int tied = 1;
            const int behind = 0;

            string ourHand = cards.Item1.getStringCard() + " " + cards.Item2.getStringCard();
            string boardHand = "";
            foreach (PlayingCard hand in boards)
            {
                boardHand += hand.getStringCard() + " ";
            }
            ulong pocket = HoldemHand.Hand.ParseHand(ourHand);
            ulong board = HoldemHand.Hand.ParseHand(boardHand);


            // Keep track of time
            double start = HoldemHand.Hand.CurrentTime;
            double duration = maxDuration;
            int ncards = HoldemHand.Hand.BitCount(pocket | board);
            //Generate the range by range combinations
            //var rangeCombinations = Combinator.AllCombinationsOf(oppsRange.ToArray());
            // Loop for specified time duration
            while ((HoldemHand.Hand.CurrentTime - start) < duration)
            {
                // Player and board info
                ulong rangeMask = HoldemHand.Hand.ParseHand("");
                List<Entity.Hand.Hand> hands = new List<Entity.Hand.Hand>();
                foreach (var rangeOpponent in oppsRange)
                {
                    Entity.Hand.Hand handOpp = null;
                    do
                    {
                        handOpp = new Entity.Hand.Hand(rangeOpponent.PickRandom());
                    } while (hands.Contains(handOpp));
                    hands.Add(handOpp);
                    rangeMask |= handOpp.getMask();
                }
                uint playerHandVal = HoldemHand.Hand.Evaluate(pocket | board);
                List<uint> ComponentHandValue = new List<uint>();
                foreach (var hand in hands)
                {
                    ComponentHandValue.Add(HoldemHand.Hand.Evaluate(hand.getMask() | board));
                }
                int index;
                if (ComponentHandValue.Max() < playerHandVal)
                    index = ahead;
                else if (ComponentHandValue.Max() == playerHandVal)
                    index = tied;
                else
                    index = behind;

                ulong boardmask = HoldemHand.Hand.RandomHand(board, pocket | rangeMask, 5);
                playerHandVal = HoldemHand.Hand.Evaluate(pocket | boardmask);

                ComponentHandValue = new List<uint>();
                foreach (var hand in hands)
                {
                    ComponentHandValue.Add(HoldemHand.Hand.Evaluate(hand.getMask() | boardmask));
                }

                if (ComponentHandValue.Max() < playerHandVal)
                    hp[index][ahead] += 1;
                else if (ComponentHandValue.Max() == playerHandVal)
                    hp[index][tied] += 1;
                else
                    hp[index][behind] += 1;

                hpTotal[index] += 1;
            }

            double ppot = (double)(hp[behind][ahead] + (double)hp[behind][tied] / 2 + (double)hp[tied][ahead] / 2) / (double)(hpTotal[behind] + (double)hpTotal[tied] / 2);
            double npot = (double)(hp[ahead][behind] + (double)hp[tied][behind] / 2 + (double)hp[ahead][tied] / 2) / (double)(hpTotal[ahead] + (double)hpTotal[behind] / 2);

            return Tuple.Create<double, double>(ppot, npot);
        }

        //TO-DO : Implement multithread ! 
        public static double getWinOdds(Tuple<PlayingCard, PlayingCard> cards, List<PlayingCard> boards, List<List<Tuple<PlayingCard, PlayingCard>>> oppsRange, double maxDuration)
        {
            string ourHand = cards.Item1.getStringCard() + " " + cards.Item2.getStringCard();
            string boardHand = "";
            foreach (PlayingCard hand in boards)
            {
                boardHand += hand.getStringCard() + " ";
            }
            ulong pocket = HoldemHand.Hand.ParseHand(ourHand);
            ulong board = HoldemHand.Hand.ParseHand(boardHand);

            long win = 0, lose = 0, tie = 0;

            // Keep track of time
            double start = HoldemHand.Hand.CurrentTime;
            double duration = maxDuration;

            //Generate the range by range combinations
            //var rangeCombinations = Combinator.AllCombinationsOf(oppsRange.ToArray());
            // Loop for specified time duration
            while ((HoldemHand.Hand.CurrentTime - start) < duration)
            {
                // Player and board info
                ulong rangeMask = HoldemHand.Hand.ParseHand("");
                List<Entity.Hand.Hand> hands = new List<Entity.Hand.Hand>();
                foreach (var rangeOpponent in oppsRange)
                {
                    Entity.Hand.Hand handOpp = null;
                    do
                    {
                        handOpp = new Entity.Hand.Hand(rangeOpponent.PickRandom());
                    } while (hands.Contains(handOpp));
                    hands.Add(handOpp);
                    rangeMask |= handOpp.getMask();
                }
                ulong boardmask = HoldemHand.Hand.RandomHand(board, pocket | rangeMask, 5);
                uint playerHandVal = HoldemHand.Hand.Evaluate(pocket | boardmask);
                // Ensure that dead, board, and pocket cards are not
                // available to opponent hands.
                ulong deadmask = boardmask | pocket;

                //Here we need to pick up some card of player
                List<uint> ComponentHandValue = new List<uint>();
                foreach (var hand in hands)
                {
                    ComponentHandValue.Add(HoldemHand.Hand.Evaluate(hand.getMask() | boardmask));
                }
                if(ComponentHandValue.Max() < playerHandVal)
                    win += 1;
                else if(ComponentHandValue.Max() == playerHandVal)
                    tie += 1;
                else
                    lose += 1;
            }

            // Return stats
            return ((double)(win + tie / 2.0)) / ((double)(win + tie + lose));
        }

        public static List<int> getHandIntValue(IEnumerable<PlayingCard> cards)
        {
            List<int> cardsList = new List<int>();
            foreach (PlayingCard card in cards)
            {
                cardsList.Add(TwoPlusTwoHandEvaluator.transformIntoInt(card));
            }
            return cardsList;
        }

        public static HandTypeEnumType GetHandTypeEnum(IEnumerable<PlayingCard> cards, PokerHandEvaluationResult result)
        {
            HandTypeEnumType hand = HandTypeEnumType.Busted;
            if (result.Result == Entity.Hand.PokerHand.Flush)
            {
                hand = HandTypeEnumType.Flush;
            }
            else if (result.Result == Entity.Hand.PokerHand.FourOfKind)
            {
                hand = HandTypeEnumType.FourOfAKind;
            }
            else if (result.Result == Entity.Hand.PokerHand.FullHouse)
            {
                hand = HandTypeEnumType.FullHouse;
            }
            else if (result.Result == Entity.Hand.PokerHand.RoyalFlush)
            {
                hand = HandTypeEnumType.StraightFlush;
            }
            else if (result.Result == Entity.Hand.PokerHand.Straight)
            {
                hand = HandTypeEnumType.Straight;
            }
            else if (result.Result == Entity.Hand.PokerHand.StraightFlush)
            {
                hand = HandTypeEnumType.StraightFlush;
            }
            else if (result.Result == Entity.Hand.PokerHand.ThreeOfKind)
            {
                hand = HandTypeEnumType.ThreeOfAKind;
            }
            else if (result.Result == Entity.Hand.PokerHand.TwoPair)
            {
                hand = HandTypeEnumType.TwoPair;
            }
            else if (result.Result == Entity.Hand.PokerHand.Pair)
            {
                var kicker = cards.Distinct(new PlayingCardNominalValueComparer()).OrderByDescending(p => p.NominalValue).ElementAt(1);
                if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ace))
                {
                    if (kicker.NominalValue == PlayingCardNominalValue.King ||
                        kicker.NominalValue == PlayingCardNominalValue.Queen ||
                        kicker.NominalValue == PlayingCardNominalValue.Jack ||
                        kicker.NominalValue == PlayingCardNominalValue.Ten)
                    {
                        hand = HandTypeEnumType.AcePairStrong;
                    }
                    else
                    {
                        hand = HandTypeEnumType.AcePairWeak;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.King))
                {
                    if (kicker.NominalValue == PlayingCardNominalValue.Queen ||
                        kicker.NominalValue == PlayingCardNominalValue.Jack ||
                        kicker.NominalValue == PlayingCardNominalValue.Ten)
                    {
                        hand = HandTypeEnumType.KingPairStrong;
                    }
                    else
                    {
                        hand = HandTypeEnumType.KingPairWeak;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Queen))
                {
                    hand = HandTypeEnumType.QueenPair;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Jack))
                {
                    hand = HandTypeEnumType.JackPair;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ten))
                {
                    hand = HandTypeEnumType.TenPair;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Nine || p.NominalValue == PlayingCardNominalValue.Eight))
                {
                    hand = HandTypeEnumType.MidPair;
                }
                else
                {
                    hand = HandTypeEnumType.LowPair;
                }
            }
            else
            {
                hand = HandTypeEnumType.Busted;
            }
            return hand;
        }

        public static PlayingCard getPlayingCardFromTwoPlusTwo(int value)
        {
            return new PlayingCard(TwoPlusTwoHandEvaluator.HandEquivalentList.ToDictionary(kp => kp.Value, kp => kp.Key)[value]);
        }
    }
}
