// This application is covered by the LGPL Gnu license. See http://www.gnu.org/copyleft/lesser.html 
// for more information on this license.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HoldemHand;

namespace OddsGridApp
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PocketCards_TextChanged(object sender, EventArgs e)
        {
            this.oddsGrid1.Clear();
            this.PocketCards.ForeColor = Color.Black;
            try
            {
                ulong [] list = PocketHands.Query(this.PocketCards.Text);
                if (list.Length == 0)
                    this.PocketCards.ForeColor = Color.Red;
            }
            catch
            {
                this.PocketCards.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_TextChanged(object sender, EventArgs e)
        {
            this.oddsGrid1.Clear();
            this.Board.ForeColor = Color.Black;
            try
            {
                ulong hand = Hand.ParseHand(Board.Text);
                int count = Hand.BitCount(hand);

                if (!Hand.ValidateHand(this.Board.Text) || (count != 0 && count != 3 && count != 4 && count != 5))
                    this.Board.ForeColor = Color.Red;
            }
            catch
            {
                this.Board.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpponentCards_TextChanged(object sender, EventArgs e)
        {
            this.oddsGrid1.Clear();
            this.OpponentCards.ForeColor = Color.Black;
            try
            {
                ulong [] list = PocketHands.Query(this.OpponentCards.Text);
                if (list.Length == 0)
                    this.PocketCards.ForeColor = Color.Red;
            }
            catch
            {
                this.OpponentCards.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.oddsGrid1.Clear();
            this.oddsGrid1.Pocket = this.PocketCards.Text;
            this.oddsGrid1.Board = this.Board.Text;
            this.oddsGrid1.Opponent = this.OpponentCards.Text;
            this.oddsGrid1.UpdateContents();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            ulong hand;
            int count;
            this.oddsGrid1.Clear();

            try
            {
                ulong [] list = PocketHands.Query(this.PocketCards.Text);
                if (list.Length == 0) return;
                list = PocketHands.Query(this.OpponentCards.Text);
                if (list.Length == 0) return;
                hand = Hand.ParseHand(Board.Text);
                count = Hand.BitCount(hand);
            }
            catch
            {
                return;
            }

            if ((this.Board.Text == "" || Hand.ValidateHand(this.Board.Text)) && (count == 0 || count == 3 || count == 4 || count == 5))
            {
                this.oddsGrid1.Pocket = this.PocketCards.Text;
                this.oddsGrid1.Board = this.Board.Text;
                this.oddsGrid1.Opponent = this.OpponentCards.Text;
                Cursor saved = Cursor;
                this.Cursor = Cursors.WaitCursor;
                this.oddsGrid1.UpdateContents();
                this.Cursor = saved;
            }
        }
    }
}