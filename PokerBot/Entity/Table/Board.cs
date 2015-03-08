using HandHistories.Objects.Cards;
using PokerBot.Entity.Card;
using PokerBot.Entity.Enum;
using PokerBot.Entity.Window;
using PokerBot.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Entity.Table
{
    public class Board
    {
        public PlayingCard FirstCard{ get;set; }
        public PlayingCard SecondCard { get; set; }
        public PlayingCard ThirdCard { get; set; }
        public PlayingCard TurnCard { get; set; }
        public PlayingCard RiverCard { get; set; }

        public void reset() 
        {
            this.FirstCard = null;
            this.SecondCard = null;
            this.ThirdCard = null;
            this.TurnCard = null;
            this.RiverCard = null;
        }

        public void setFlop(PlayingCard one, PlayingCard two, PlayingCard three)
        {
            this.FirstCard = one;
            this.SecondCard = two;
            this.ThirdCard = three;
        }

        public void setTurn(PlayingCard turn)
        {
            this.TurnCard = turn;
        }

        public void setRiver(PlayingCard river)
        {
            this.RiverCard = river;
        }

        public Board getBoardFromRiver()
        {
            return new Board(this.FirstCard, this.SecondCard, this.ThirdCard,this.TurnCard,this.RiverCard);
        }


        public Board getBoardFromTurn()
        {
            return new Board(this.FirstCard, this.SecondCard, this.ThirdCard, this.TurnCard);
        }

        public Board getBoardFromFlop()
        {
            return new Board(this.FirstCard, this.SecondCard, this.ThirdCard);
        }

        public Board(PlayingCard one, PlayingCard two, PlayingCard three, PlayingCard four, PlayingCard five)
        {
            this.setFlop(one, two, three);
            this.setTurn(four);
            this.setRiver(five);
        }

        public Board(PlayingCard one, PlayingCard two, PlayingCard three, PlayingCard four)
        {
            this.setFlop(one, two, three);
            this.setTurn(four);
        }

        public Board(PlayingCard one, PlayingCard two, PlayingCard three)
        {
            this.setFlop(one, two, three);
        }

        public Board()
        {
            
        }

        public IEnumerable<PlayingCard> getEvaluator()
        {
            HashSet<PlayingCard> cards = new HashSet<PlayingCard>();
            if (this.FirstCard != null) {
                cards.Add(this.FirstCard);
            } 
            if (this.SecondCard != null)
            {
                cards.Add(this.SecondCard);
            } 
            if (this.ThirdCard != null)
            {
                cards.Add(this.ThirdCard);
            } 
            if (this.TurnCard != null)
            {
                cards.Add(this.TurnCard);
            } 
            if (this.RiverCard != null)
            {
                cards.Add(this.RiverCard);
            }
            return cards;
        }

        public static Board createFromHandHistory(BoardCards board)
        {
            Board returnBoard = new Board();
            if (board.Street == Street.Preflop)
            {
                //Do nothing;
            }
            else if (board.Street == Street.Flop)
            {
                returnBoard.setFlop(CardConverter.fromIntToPlayingCard(board[0]), CardConverter.fromIntToPlayingCard(board[1]), CardConverter.fromIntToPlayingCard(board[2]));
            }
            else if (board.Street == Street.Turn)
            {
                returnBoard.setFlop(CardConverter.fromIntToPlayingCard(board[0]),CardConverter.fromIntToPlayingCard(board[1]),CardConverter.fromIntToPlayingCard(board[2]));
                returnBoard.setTurn(CardConverter.fromIntToPlayingCard(board[3]));
            }
            else if (board.Street == Street.River)
            {
                returnBoard.setFlop(CardConverter.fromIntToPlayingCard(board[0]),CardConverter.fromIntToPlayingCard(board[1]),CardConverter.fromIntToPlayingCard(board[2]));
                returnBoard.setTurn(CardConverter.fromIntToPlayingCard(board[3]));
                returnBoard.setRiver(CardConverter.fromIntToPlayingCard(board[4]));
            }

            return returnBoard;
        }

        public int[] getTwoPlusTwoHandEvaluatorEquivalent()
        {
            List<int> intReturn = new List<int>();
            foreach (var hand in this.getEvaluator())
            {
                intReturn.Add(TwoPlusTwoHandEvaluator.transformIntoInt(hand));
            }
            return intReturn.ToArray();
        }

    }
}
