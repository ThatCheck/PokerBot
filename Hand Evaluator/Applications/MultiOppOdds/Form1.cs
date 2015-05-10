using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HoldemHand;
using ZedGraph;
using System.Threading;

namespace MultiOppOdds
{
    public partial class Form1 : Form
    {
        private Cursor saved;
        private Thread tid1;
        private int speedIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {          
 
            graph.GraphPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);
            //graph.GraphPane.PaneFill = new Fill(Color.FromArgb(250, 250, 255));

            graph.GraphPane.XAxis.MajorGrid.IsVisible = true;
            graph.GraphPane.YAxis.MajorGrid.IsVisible = true;
            //graph.GraphPane.XAxis.GridColor = Color.LightGray;
            //graph.GraphPane.YAxis.GridColor = Color.LightGray;

            CalculateGraph();
        }

        private void pocket_TextChanged(object sender, EventArgs e)
        {

            if (!PocketHands.ValidateQuery(pocket.Text))
            {
                pocket.ForeColor = Color.Red;
                Calculate.Enabled = false;
            }
            else
            {
                pocket.ForeColor = Color.Black;
                Calculate.Enabled = true;
            }
        }

        private void board_TextChanged(object sender, EventArgs e)
        {
            if (!Hand.ValidateHand(board.Text) && board.Text != "")
            {
                board.ForeColor = Color.Red;
                Calculate.Enabled = false;
            }
            else
            {
                ulong boardmask = Hand.ParseHand(board.Text);
                if (Hand.BitCount(boardmask) > 5)
                {
                    board.ForeColor = Color.Red;
                    Calculate.Enabled = false;
                }
                else
                {
                    board.ForeColor = Color.Black;
                    Calculate.Enabled = true;
                }
            }
        }

        private void CalculateThread()
        {
            double[] speedtbl = {
               0.05, 0.075, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0, 1.25
            };
            PointPairList win = new PointPairList();
            PointPairList handstrength = new PointPairList();
            PointPairList ppot = new PointPairList();
            PointPairList npot = new PointPairList();
            string pocketstr = pocket.Text;
            string boardstr = board.Text;
          
            BeginCalcGraph();

            string[] labels = { "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            graph.GraphPane.Title.Text = string.Format("Texas Holdem Odds [{0}{1}{2}]",pocket.Text, board.Text != "" ? " - " : "", board.Text);
            graph.GraphPane.YAxis.Title.Text = "Percentage";
            graph.GraphPane.XAxis.Title.Text = "Opponents";
            graph.GraphPane.XAxis.Scale.TextLabels = labels;
            graph.GraphPane.XAxis.Type = AxisType.Text;

            // Clear the data in the graph
            graph.GraphPane.CurveList.Clear();
            this.RefreshGraph();

            // Validate Data
            if (!PocketHands.ValidateQuery(pocketstr)) return;
            if (board.Text != "" && !Hand.ValidateHand(boardstr)) return;

            //ulong pocketmask = Hand.ParseHand(pocket.Text);
            ulong boardmask = Hand.ParseHand(boardstr);

            bool bCalcPot = (PotentialCheckBox.Checked && (Hand.BitCount(boardmask) == 3 || Hand.BitCount(boardmask) == 4));
            bool bCalcStrength = HandStrengthCheck.Checked;
            bool bCalcWin = WinCheckBox.Checked;

            //if (Hand.BitCount(pocketmask) != 2) return;
            if (Hand.BitCount(boardmask) > 5) return;

            // Calculate Data
            for (int i = 1; i < 10; i++)
            {
                if (bCalcPot) {
                    double npotv, ppotv;
                    Hand.HandPotential(pocketstr, boardstr, out ppotv, out npotv, i, speedtbl[speedIndex]);
                    npot.Add(i, 100.0 * npotv);
                    ppot.Add(i, 100.0 * ppotv);
                }

                if (bCalcStrength)
                {
                    handstrength.Add(i, 100.0 * Hand.HandStrength(pocketstr, boardstr, i, speedtbl[speedIndex]));
                }

                if (WinCheckBox.Checked)
                {
                    win.Add(i, Hand.WinOdds(pocket.Text, board.Text, "", i, speedtbl[speedIndex]) * 100.0);
                }
            }

            // Put Data in the Graph
            if (bCalcWin)
            {
                LineItem myCurve = graph.GraphPane.AddCurve("Win", win, Color.Green, SymbolType.Circle);
                // Make curve thicker
                myCurve.Line.Width = 2.0F;
            }

            if (bCalcStrength)
            {
                LineItem hsCurve = graph.GraphPane.AddCurve("Hand Strength", handstrength, Color.Blue);
                // Make curve thicker
                hsCurve.Line.Width = 2.0F;
            }

            if (bCalcPot)
            {
                LineItem ppotCurve = graph.GraphPane.AddCurve("Postive Potential", ppot, Color.MediumSeaGreen);
                LineItem npotCurve = graph.GraphPane.AddCurve("Negative Potential", npot, Color.MediumVioletRed);
                // Make curves thicker
                ppotCurve.Line.Width = 2.0F;
                npotCurve.Line.Width = 2.0F;
            }

            // Display the changes
            graph.AxisChange();

            EndCalcGraph();
            RefreshGraph();
        }

        private delegate void VoidDelegate();

        private void CalculateGraph()
        {
            if (pocket.BackColor != Color.Red && board.BackColor != Color.Red)
            {
                speedIndex = Speed.Value;
                tid1 = new Thread(new ThreadStart(CalculateThread));
                tid1.IsBackground = true;
                tid1.Start();
            }
        }

        private void BeginCalcGraph()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VoidDelegate(BeginCalcGraph));
                return;
            }
            saved = Cursor;
            this.Cursor = Cursors.WaitCursor;
            this.Calculate.Enabled = false;
            this.pocket.Enabled = false;
            this.board.Enabled = false;
        }

        private void EndCalcGraph()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VoidDelegate(EndCalcGraph));
                return;
            }
            this.Cursor = saved;
            this.Calculate.Enabled = true;
            this.pocket.Enabled = true;
            this.board.Enabled = true;
        }

        private void RefreshGraph()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new VoidDelegate(RefreshGraph));
                return;
            }
            graph.Refresh();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            CalculateGraph();
        }
    }
}