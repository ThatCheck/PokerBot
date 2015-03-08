using PokerBot.Comparer;
using PokerBot.Entity.Card;
using PokerBot.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.BayesianNetwork.V1.Board
{
    public class BoardFlop : BaseBoard
    {
        public BoardFlop(Entity.Table.Board board) : base(board.getBoardFromFlop())
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>(this.Board.getEvaluator());
            PokerEvaluator pokerEvaluator = new PokerEvaluator(new PokerHandEvaluator());
            PokerHandEvaluationResult result = pokerEvaluator.EvaluateHand(cards);
            if (result.Result == Entity.Hand.PokerHand.HighCard)
            {
                if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ace))
                {
                    if (cards.Any(p => p.NominalValue == PlayingCardNominalValue.King || p.NominalValue == PlayingCardNominalValue.Queen || p.NominalValue == PlayingCardNominalValue.Jack))
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.AHigh;
                    }
                    else
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.ALow;
                    }
                } 
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.King))
                {
                    if (cards.Any(p => p.NominalValue == PlayingCardNominalValue.Queen || p.NominalValue == PlayingCardNominalValue.Jack || p.NominalValue == PlayingCardNominalValue.Ten))
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.KHigh;
                    }
                    else
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.KLow;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Queen))
                {
                    if (cards.Any(p => p.NominalValue == PlayingCardNominalValue.Jack || p.NominalValue == PlayingCardNominalValue.Ten))
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.QHigh;
                    }
                    else
                    {
                        this.BoardEnumType = V1.Board.BoardEnumType.QLow;
                    }
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Jack))
                {
                    this.BoardEnumType = V1.Board.BoardEnumType.JLow;
                }
            }
            else if (result.Result == Entity.Hand.PokerHand.Pair)
            {
                 var kicker = cards.Distinct(new PlayingCardNominalValueComparer()).OrderByDescending(p => p.NominalValue).ElementAt(1);
                if (kicker.NominalValue == PlayingCardNominalValue.King ||
                           kicker.NominalValue == PlayingCardNominalValue.Queen ||
                           kicker.NominalValue == PlayingCardNominalValue.Jack ||
                           kicker.NominalValue == PlayingCardNominalValue.Ten)
                {
                    this.BoardEnumType = V1.Board.BoardEnumType.HighPair;
                }
                else
                {
                    this.BoardEnumType = V1.Board.BoardEnumType.LowPair;
                }
            }
            else if (result.Result == Entity.Hand.PokerHand.ThreeOfKind)
            {
                this.BoardEnumType = V1.Board.BoardEnumType.ThreeOfAKind;
            }
            else
            {
                this.BoardEnumType = V1.Board.BoardEnumType.Low;
            }
        }

        public new static string getCaseName()
        {
            return System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        public new static string[] getValueName()
        {
            List<String> data = new List<string>();
            BoardEnumType[] enumArray = new BoardEnumType[]
            {
                BoardEnumType.AHigh,
                BoardEnumType.ALow,
                BoardEnumType.KHigh,
                BoardEnumType.KLow,
                BoardEnumType.QHigh,
                BoardEnumType.QLow,
                BoardEnumType.JLow,
                BoardEnumType.Low,
                BoardEnumType.LowPair,
                BoardEnumType.HighPair,
                BoardEnumType.ThreeOfAKind
            };
            foreach (BoardEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandType.HandType).Name, typeof(BoardChangeTurn).Name };
        }
    }
}
