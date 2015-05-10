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
    public class BoardChangeTurn : BaseBoard
    {
        public BoardChangeTurn(Entity.Table.Board board) : base(board.getBoardFromTurn())
        {

            IEnumerable<PlayingCard> flopBoard = this._board.getBoardFromFlop().getEvaluator();
            IEnumerable<PlayingCard> turnBoard = this._board.getBoardFromTurn().getEvaluator();

            HashSet<PlayingCard> cards = new HashSet<PlayingCard>(turnBoard);
            PokerEvaluator pokerEvaluator = new PokerEvaluator(new PokerHandEvaluator());
            PokerHandEvaluationResult result = pokerEvaluator.EvaluateHand(cards);

            if (!flopBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Ace) && turnBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Ace))
            {
                this.BoardEnumType = V1.Board.BoardEnumType.FirstA;
            }
            else if (!flopBoard.Any(p => p.NominalValue == PlayingCardNominalValue.King) && turnBoard.Any(p => p.NominalValue == PlayingCardNominalValue.King))
            {
                this.BoardEnumType = V1.Board.BoardEnumType.FirstK;
            }
            else if (!flopBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Queen) && turnBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Queen))
            {
                this.BoardEnumType = V1.Board.BoardEnumType.FirstQ;
            }
            else if (!flopBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Jack) && turnBoard.Any(p => p.NominalValue == PlayingCardNominalValue.Jack))
            {
                this.BoardEnumType = V1.Board.BoardEnumType.FirstJ;
            }
            else if (result.Result == Entity.Hand.PokerHand.Pair)
            {
                var kicker = cards.Distinct(new PlayingCardNominalValueComparer()).OrderByDescending(p => p.NominalValue).ElementAt(1);

                if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ace || p.NominalValue == PlayingCardNominalValue.King || p.NominalValue == PlayingCardNominalValue.Queen || p.NominalValue == PlayingCardNominalValue.Jack))
                {
                    this.BoardEnumType = V1.Board.BoardEnumType.HighPair;
                }
                else if (result.ResultCards.Any(p => p.NominalValue == PlayingCardNominalValue.Ten || p.NominalValue == PlayingCardNominalValue.Nine || p.NominalValue == PlayingCardNominalValue.Eight))
                {
                    this.BoardEnumType = V1.Board.BoardEnumType.MidPair;
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
            else if (result.Result == Entity.Hand.PokerHand.FourOfKind)
            {
                this.BoardEnumType = V1.Board.BoardEnumType.FourOfAKind;
            }
            else if (result.Result == Entity.Hand.PokerHand.TwoPair)
            {
                this.BoardEnumType = V1.Board.BoardEnumType.TwoPair;
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
                BoardEnumType.FirstA,
                BoardEnumType.FirstK,
                BoardEnumType.FirstQ,
                BoardEnumType.FirstJ,
                BoardEnumType.FourOfAKind,
                BoardEnumType.MidPair,
                BoardEnumType.TwoPair,
                BoardEnumType.LowPair,
                BoardEnumType.HighPair,
                BoardEnumType.ThreeOfAKind,
                BoardEnumType.Low
            };
            foreach (BoardEnumType enumValue in enumArray)
            {
                data.Add(enumValue.ToBayesianNetwork());
            }
            return data.ToArray();
        }

        public new static string[] getArcForValue()
        {
            return new String[] { typeof(HandType.HandTypeTurn).Name, typeof(BoardChangeRiver).Name };
        }
    }
}
