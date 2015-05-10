using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HoldemHand;

namespace MultiOddsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Pocket_TextChanged(object sender, EventArgs e)
        {
            if (Pocket.Text.Length > 0 && !Hand.ValidateHand(Pocket.Text))
            {
                Pocket.ForeColor = Color.Red;
            }
            else
            {
                Pocket.ForeColor = Color.Black;
            }

            userControl11.Pocket = Pocket.Text;
        }

        private void Board_TextChanged(object sender, EventArgs e)
        {
            if (Board.Text.Length > 0 && !Hand.ValidateHand(Board.Text))
            {
                Board.ForeColor = Color.Red;
            }
            else
            {
                Board.ForeColor = Color.Black;
            }

            userControl11.Board = Board.Text;
        }

        private void Opponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            int opp = int.Parse(Opponents.Text);
            userControl11.Opponents = opp;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (Pocket.Text.Length > 0 && !Hand.ValidateHand(Pocket.Text))
            {
                Pocket.ForeColor = Color.Red;
            }
            else
            {
                Pocket.ForeColor = Color.Black;
            }

            userControl11.Pocket = Pocket.Text;

            if (Board.Text.Length > 0 && !Hand.ValidateHand(Board.Text))
            {
                Board.ForeColor = Color.Red;
            }
            else
            {
                Board.ForeColor = Color.Black;
            }

            userControl11.Board = Board.Text;

            int opp = int.Parse(Opponents.Text);
            userControl11.Opponents = opp;
        }
    }
}