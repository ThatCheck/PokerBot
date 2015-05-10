// This control is covered by the LGPL Gnu license. See http://www.gnu.org/copyleft/lesser.html 
// for more information on this license.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HoldemHand;

namespace OddsGrid
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OddsGrid : UserControl
    {
        private string pocket = "";
        private string opponent = "?";
        private string board = "";

        /// <summary>
        /// 
        /// </summary>
        public string Pocket
        {
            get { return pocket; }
            set { pocket = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Board
        {
            get { return board; }
            set { board = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Opponent
        {
            get { return opponent; }
            set { opponent = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string FormatPercent(double v, bool montecarlo)
        {
            if (v != 0.0)
            {
                if (v * 100.0 >= 1.0)
                    return string.Format("{0:##0.0}%", v * 100.0);
                else
                    return "<1%";
            }
            return "n/a";
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateContents()
        {
            if (!this.DesignMode)
            {
                double playerwins = 0.0;
                double opponentwins = 0.0;
                double[] player = new double[9];
                double[] opponent = new double[9];
                ulong[] pockethands = null;
                ulong[] opponenthands = null;

                try
                {
                    pockethands = PocketHands.Query(Pocket);
                    opponenthands = PocketHands.Query(Opponent);

                    if (Board != "" && !Hand.ValidateHand(Board))
                    {
                        Clear();
                        return;
                    }
                }
                catch
                {
                    Clear();
                    return;
                }

                bool montecarlo = Hand.HandWinOdds(pockethands, opponenthands, Hand.ParseHand(Board), out player, out opponent);

                for (int i = 0; i < 9; i++)
                {
                    switch ((Hand.HandTypes)i)
                    {
                        case Hand.HandTypes.HighCard:
                            PlayerHighCard.Text = FormatPercent(player[i], montecarlo);
                            OpponentHighCard.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.Pair:
                            PlayerPair.Text = FormatPercent(player[i], montecarlo);
                            OpponentPair.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.TwoPair:
                            PlayerTwoPair.Text = FormatPercent(player[i], montecarlo);
                            OpponentTwoPair.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.Trips:
                            Player3ofaKind.Text = FormatPercent(player[i], montecarlo);
                            Opponent3ofaKind.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.Straight:
                            PlayerStraight.Text = FormatPercent(player[i], montecarlo);
                            OpponentStraight.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.Flush:
                            PlayerFlush.Text = FormatPercent(player[i], montecarlo);
                            OpponentFlush.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.FullHouse:
                            PlayerFullhouse.Text = FormatPercent(player[i], montecarlo);
                            OpponentFullhouse.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.FourOfAKind:
                            Player4ofaKind.Text = FormatPercent(player[i], montecarlo);
                            Opponent4ofaKind.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                        case Hand.HandTypes.StraightFlush:
                            PlayerStraightFlush.Text = FormatPercent(player[i], montecarlo);
                            OpponentStraightFlush.Text = FormatPercent(opponent[i], montecarlo);
                            break;
                    }
                    playerwins += player[i] * 100.0;
                    opponentwins += opponent[i] * 100.0;
                }

                PlayerWin.Text = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", playerwins);
                OpponentWin.Text = string.Format("{0}{1:##0.0}%", montecarlo ? "~" : "", opponentwins);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            PlayerHighCard.Text = "";
            OpponentHighCard.Text = "";
            PlayerPair.Text = "";
            OpponentPair.Text = "";
            PlayerTwoPair.Text = "";
            OpponentTwoPair.Text = "";
            Player3ofaKind.Text = "";
            Opponent3ofaKind.Text = "";
            PlayerStraight.Text = "";
            OpponentStraight.Text = "";
            PlayerFlush.Text = "";
            OpponentFlush.Text = "";
            PlayerFullhouse.Text = "";
            OpponentFullhouse.Text = "";
            Player4ofaKind.Text = "";
            Opponent4ofaKind.Text = "";
            PlayerStraightFlush.Text = "";
            OpponentStraightFlush.Text = "";
            PlayerWin.Text = "";
            OpponentWin.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        public OddsGrid()
        {
            InitializeComponent();
        }
    }
}