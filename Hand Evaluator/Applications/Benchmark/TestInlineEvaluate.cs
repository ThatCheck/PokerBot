using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using HoldemHand;
using ZedGraph;

namespace Benchmark
{
    class TestInlineEvaluate : ITest
    {
        #region Fields
        private bool bFirst = true;
        GraphPane graphpane = null;
        BarItem myBar1 = null;
        BarItem myBar2 = null;
        LineItem average7curve = null;
        LineItem average5curve = null;
        double loop1, loop2, loop3, loop4, loop5, loop6, loop7;
        double avgloop1, avgloop2, avgloop3, avgloop4, avgloop5, avgloop6, avgloop7;
        double loopwdead1, loopwdead2, loopwdead3, loopwdead4, loopwdead5, loopwdead6, loopwdead7;
        double avgloopwdead1, avgloopwdead2, avgloopwdead3, avgloopwdead4, avgloopwdead5, avgloopwdead6, avgloopwdead7;
        #endregion

        #region Constructor
        public TestInlineEvaluate(System.Windows.Forms.Form form)
        {
            graphpane = new GraphPane();
            graphpane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);

            graphpane.XAxis.MajorGrid.IsVisible = true;
            graphpane.YAxis.MajorGrid.IsVisible = true;

            // Set the Titles
            graphpane.Title.Text = "Inline/Evaluate";
            graphpane.XAxis.Title.Text = "Hand Size";
            graphpane.YAxis.Title.Text = "Hands/Sec";
        }
        #endregion

        #region Test
        #region LoopTest
        double Loop7Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e, f, g;
            ulong _card1, _n2, _n3, _n4, _n5, _n6;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 6; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    for (b = a + 1; b < Hand.CardMasksTableSize - 5; b++)
                    {
                        _n2 = _card1 | Hand.CardMasksTable[b];
                        for (c = b + 1; c < Hand.CardMasksTableSize - 4; c++)
                        {
                            _n3 = _n2 | Hand.CardMasksTable[c];
                            for (d = c + 1; d < Hand.CardMasksTableSize - 3; d++)
                            {
                                _n4 = _n3 | Hand.CardMasksTable[d];
                                for (e = d + 1; e < Hand.CardMasksTableSize - 2; e++)
                                {
                                    _n5 = _n4 | Hand.CardMasksTable[e];
                                    for (f = e + 1; f < Hand.CardMasksTableSize - 1; f++)
                                    {
                                        _n6 = _n5 | Hand.CardMasksTable[f];
                                        for (g = f + 1; g < Hand.CardMasksTableSize; g++)
                                        {
                                            Hand.Evaluate(_n6 | Hand.CardMasksTable[g], 7);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop6Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e, f;
            ulong _card1, _n2, _n3, _n4, _n5;

            do
            {
                 for (a = 0; a < Hand.CardMasksTableSize - 5; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    for (b = a + 1; b < Hand.CardMasksTableSize - 4; b++)
                    {
                        _n2 = _card1 | Hand.CardMasksTable[b];
                        for (c = b + 1; c < Hand.CardMasksTableSize - 3; c++)
                        {
                            _n3 = _n2 | Hand.CardMasksTable[c];
                            for (d = c + 1; d < Hand.CardMasksTableSize - 2; d++)
                            {
                                _n4 = _n3 | Hand.CardMasksTable[d];
                                for (e = d + 1; e < Hand.CardMasksTableSize - 1; e++)
                                {
                                    _n5 = _n4 | Hand.CardMasksTable[e];
                                    for (f = e + 1; f < Hand.CardMasksTableSize; f++)
                                    {
                                        Hand.Evaluate(_n5 | Hand.CardMasksTable[f], 6);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }

        double Loop5Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e;
            ulong _card1, _n2, _n3, _n4;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 4; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    for (b = a + 1; b < Hand.CardMasksTableSize - 3; b++)
                    {
                        _n2 = _card1 | Hand.CardMasksTable[b];
                        for (c = b + 1; c < Hand.CardMasksTableSize - 2; c++)
                        {
                            _n3 = _n2 | Hand.CardMasksTable[c];
                            for (d = c + 1; d < Hand.CardMasksTableSize - 1; d++)
                            {
                                _n4 = _n3 | Hand.CardMasksTable[d];
                                for (e = d + 1; e < Hand.CardMasksTableSize; e++)
                                {
                                    Hand.Evaluate(_n4 | Hand.CardMasksTable[e], 5);
                                    count++;
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop4Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d;
            ulong _card1, _n2, _n3;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 3; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    for (b = a + 1; b < Hand.CardMasksTableSize - 2; b++)
                    {
                        _n2 = _card1 | Hand.CardMasksTable[b];
                        for (c = b + 1; c < Hand.CardMasksTableSize - 1; c++)
                        {
                            _n3 = _n2 | Hand.CardMasksTable[c];
                            for (d = c + 1; d < Hand.CardMasksTableSize; d++)
                            {
                                Hand.Evaluate(_n3 | Hand.CardMasksTable[d], 4);
                                count++;
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop3Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c;
            ulong _card1, _n2;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 2; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    for (b = a + 1; b < Hand.CardMasksTableSize - 1; b++)
                    {
                        _n2 = _card1 | Hand.CardMasksTable[b];
                        for (c = b + 1; c < Hand.CardMasksTableSize; c++)
                        {
                            Hand.Evaluate(_n2 | Hand.CardMasksTable[c], 3);
                            count++;
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop2Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a;

            do
            {
                for (a = 0; a < Hand.TwoCardMaskTableSize; a++)
                {
                    Hand.Evaluate(Hand.TwoCardMaskTable[a], 2);
                    count++;
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }

        double Loop1Test()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize; a++)
                {
                    Hand.Evaluate(Hand.CardMasksTable[a], 1);
                    count++;
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        #endregion
        #region Loop W/Dead Test
        double Loop7WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e, f, g;
            ulong _card1, _card2, _card3, _card4, _card5, _card6, _card7;
            ulong _n2, _n3, _n4, _n5, _n6;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 6; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize - 5; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        _n2 = _card1 | _card2;
                        for (c = b + 1; c < Hand.CardMasksTableSize - 4; c++)
                        {
                            _card3 = Hand.CardMasksTable[c];
                            if ((dead & _card3) != 0) continue;
                            _n3 = _n2 | _card3;
                            for (d = c + 1; d < Hand.CardMasksTableSize - 3; d++)
                            {
                                _card4 = Hand.CardMasksTable[d];
                                if ((dead & _card4) != 0) continue;
                                _n4 = _n3 | _card4;
                                for (e = d + 1; e < Hand.CardMasksTableSize - 2; e++)
                                {
                                    _card5 = Hand.CardMasksTable[e];
                                    if ((dead & _card5) != 0) continue;
                                    _n5 = _n4 | _card5;
                                    for (f = e + 1; f < Hand.CardMasksTableSize - 1; f++)
                                    {
                                        _card6 = Hand.CardMasksTable[f];
                                        if ((dead & _card6) != 0) continue;
                                        _n6 = _n5 | _card6;
                                        for (g = f + 1; g < Hand.CardMasksTableSize; g++)
                                        {
                                            _card7 = Hand.CardMasksTable[g];
                                            if ((dead & _card7) != 0) continue;
                                            Hand.Evaluate(_n6 | _card7 | shared, 7);
                                            count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop6WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e, f;
            ulong _card1, _card2, _card3, _card4, _card5, _card6;
            ulong _n2, _n3, _n4, _n5;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 5; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize - 4; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        _n2 = _card1 | _card2;
                        for (c = b + 1; c < Hand.CardMasksTableSize - 3; c++)
                        {
                            _card3 = Hand.CardMasksTable[c];
                            if ((dead & _card3) != 0) continue;
                            _n3 = _n2 | _card3;
                            for (d = c + 1; d < Hand.CardMasksTableSize - 2; d++)
                            {
                                _card4 = Hand.CardMasksTable[d];
                                if ((dead & _card4) != 0) continue;
                                _n4 = _n3 | _card4;
                                for (e = d + 1; e < Hand.CardMasksTableSize - 1; e++)
                                {
                                    _card5 = Hand.CardMasksTable[e];
                                    if ((dead & _card5) != 0) continue;
                                    _n5 = _n4 | _card5;
                                    for (f = e + 1; f < Hand.CardMasksTableSize; f++)
                                    {
                                        _card6 = Hand.CardMasksTable[f];
                                        if ((dead & _card6) != 0) continue;
                                        Hand.Evaluate(_n5 | _card6 | shared, 6);
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop5WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d, e;
            ulong _card1, _card2, _card3, _card4, _card5;
            ulong _n2, _n3, _n4;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 4; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize - 3; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        _n2 = _card1 | _card2;
                        for (c = b + 1; c < Hand.CardMasksTableSize - 2; c++)
                        {
                            _card3 = Hand.CardMasksTable[c];
                            if ((dead & _card3) != 0) continue;
                            _n3 = _n2 | _card3;
                            for (d = c + 1; d < Hand.CardMasksTableSize - 1; d++)
                            {
                                _card4 = Hand.CardMasksTable[d];
                                if ((dead & _card4) != 0) continue;
                                _n4 = _n3 | _card4;
                                for (e = d + 1; e < Hand.CardMasksTableSize; e++)
                                {
                                    _card5 = Hand.CardMasksTable[e];
                                    if ((dead & _card5) != 0) continue;
                                    Hand.Evaluate(_n4 | _card5 | shared, 5);
                                    count++;
                                }
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop4WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c, d;
            ulong _card1, _card2, _card3, _card4;
            ulong _n2, _n3;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 3; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize - 2; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        _n2 = _card1 | _card2;
                        for (c = b + 1; c < Hand.CardMasksTableSize - 1; c++)
                        {
                            _card3 = Hand.CardMasksTable[c];
                            if ((dead & _card3) != 0) continue;
                            _n3 = _n2 | _card3;
                            for (d = c + 1; d < Hand.CardMasksTableSize; d++)
                            {
                                _card4 = Hand.CardMasksTable[d];
                                if ((dead & _card4) != 0) continue;
                                Hand.Evaluate(_n3 | _card4 | shared, 4);
                                count++;
                            }
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop3WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b, c;
            ulong _card1, _card2, _card3;
            ulong _n2;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 2; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize - 1; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        _n2 = _card1 | _card2;
                        for (c = b + 1; c < Hand.CardMasksTableSize; c++)
                        {
                            _card3 = Hand.CardMasksTable[c];
                            if ((dead & _card3) != 0) continue;
                            Hand.Evaluate(_n2 | _card3 | shared, 3);
                            count++;
                        }
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        double Loop2WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a, b;
            ulong _card1, _card2;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize - 1; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    for (b = a + 1; b < Hand.CardMasksTableSize; b++)
                    {
                        _card2 = Hand.CardMasksTable[b];
                        if ((dead & _card2) != 0) continue;
                        Hand.Evaluate(_card1 | _card2 | shared, 2);
                        count++;
                    }
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }

        double Loop1WDeadTest()
        {
            double start = Hand.CurrentTime;
            long count = 0;
            int a;
            ulong _card1;
            ulong dead = 0UL;
            ulong shared = 0UL;

            do
            {
                for (a = 0; a < Hand.CardMasksTableSize; a++)
                {
                    _card1 = Hand.CardMasksTable[a];
                    if ((dead & _card1) != 0) continue;
                    Hand.Evaluate(_card1 | shared, 1);
                    count++;
                }
            } while (count < 1000000);

            return count / (Hand.CurrentTime - start);
        }
        #endregion
        #endregion

        #region ITest

        /// <summary>
        /// 
        /// </summary>
        public void RunBenchmark()
        {
            loop1 = Loop1Test();
            avgloop1 = (bFirst ? loop1 : avgloop1 * 0.9 + loop1 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop2 = Loop2Test();
            avgloop2 = (bFirst ? loop2 : avgloop2 * 0.9 + loop2 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop3 = Loop3Test();
            avgloop3 = (bFirst ? loop3 : avgloop3 * 0.9 + loop3 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop4 = Loop4Test();
            avgloop4 = (bFirst ? loop4 : avgloop4 * 0.9 + loop4 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop5 = Loop5Test();
            avgloop5 = (bFirst ? loop5 : avgloop5 * 0.9 + loop5 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop6 = Loop6Test();
            avgloop6 = (bFirst ? loop6 : avgloop6 * 0.9 + loop6 * 0.1);
            System.Threading.Thread.Sleep(0);
            loop7 = Loop7Test();
            avgloop7 = (bFirst ? loop7 : avgloop7 * 0.9 + loop7 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead1 = Loop1WDeadTest();
            avgloopwdead1 = (bFirst ? loopwdead1 : avgloopwdead1 * 0.9 + loopwdead1 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead2 = Loop2WDeadTest();
            avgloopwdead2 = (bFirst ? loopwdead2 : avgloopwdead2 * 0.9 + loopwdead2 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead3 = Loop3WDeadTest();
            avgloopwdead3 = (bFirst ? loopwdead3 : avgloopwdead3 * 0.9 + loopwdead3 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead4 = Loop4WDeadTest();
            avgloopwdead4 = (bFirst ? loopwdead4 : avgloopwdead4 * 0.9 + loopwdead4 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead5 = Loop5WDeadTest();
            avgloopwdead5 = (bFirst ? loopwdead5 : avgloopwdead5 * 0.9 + loopwdead5 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead6 = Loop6WDeadTest();
            avgloopwdead6 = (bFirst ? loopwdead6 : avgloopwdead6 * 0.9 + loopwdead6 * 0.1);
            System.Threading.Thread.Sleep(0);
            loopwdead7 = Loop7WDeadTest();
            avgloopwdead7 = (bFirst ? loopwdead7 : avgloopwdead7 * 0.9 + loopwdead7 * 0.1);
            System.Threading.Thread.Sleep(0);
            bFirst = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public GraphPane Pane
        {
            get
            {
                return graphpane;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdatePane()
        {
            // Make up some random data points
            string[] labels = { "1", "2", "3", "4", "5", "6", "7" };
            double[] y = { loop1, loop2, loop3, loop4, loop5, loop6, loop7 };
            double[] z = { loopwdead1, loopwdead2, loopwdead3, loopwdead4, loopwdead5, loopwdead6, loopwdead7 };
            double[] avgy = { avgloop1, avgloop2, avgloop3, avgloop4, avgloop5, avgloop6, avgloop7 };
            double[] avgz = { avgloopwdead1, avgloopwdead2, avgloopwdead3, avgloopwdead4, avgloopwdead5, avgloopwdead6, avgloopwdead7 };
            try
            {
                // Generate a red bar with "Curve 1" in the legend
                if (myBar1 == null)
                {
                    /// Generate a black line with "Curve 4" in the legend
                    average7curve = graphpane.AddCurve("Avg Hands()", null, avgy, Color.Aqua, SymbolType.Circle);
                    //average7curve.Line.Fill = new Fill(Color.White, Color.LightSkyBlue, -45F);
                    //// Fix up the curve attributes a little
                    average7curve.Symbol.Size = 8.0F;
                    average7curve.Symbol.Fill = new Fill(Color.White);
                    average7curve.Line.Width = 2.0F;

                    /// Generate a black line with "Curve 4" in the legend
                    average5curve = graphpane.AddCurve("Avg Hands() w/dead", null, avgz, Color.BlueViolet, SymbolType.TriangleDown);
                    //average7curve.Line.Fill = new Fill(Color.White, Color.LightSkyBlue, -45F);
                    //// Fix up the curve attributes a little
                    average5curve.Symbol.Size = 8.0F;
                    average5curve.Symbol.Fill = new Fill(Color.White);
                    average5curve.Line.Width = 2.0F;


                    myBar1 = graphpane.AddBar("Inline", null, y, Color.Violet);
                    myBar2 = graphpane.AddBar("Inline W/Dead", null, z, Color.Blue);


                    myBar1.Bar.Fill = new Fill(Color.Aqua, Color.White, Color.Aqua);
                    myBar2.Bar.Fill = new Fill(Color.CadetBlue, Color.White, Color.CadetBlue);
                }
                else
                {
                    int size = myBar1.NPts;
                    for (int i = 0; i < size; i++)
                        myBar1.RemovePoint(0);

                    foreach (double v in y)
                        myBar1.AddPoint(v, v);

                    size = myBar2.NPts;
                    for (int i = 0; i < size; i++)
                        myBar2.RemovePoint(0);

                    foreach (double v in z)
                        myBar2.AddPoint(v, v);

                    size = average7curve.NPts;
                    for (int i = 0; i < size; i++)
                        average7curve.RemovePoint(0);

                    foreach (double v in avgy)
                        average7curve.AddPoint(v, v);

                    size = average5curve.NPts;
                    for (int i = 0; i < size; i++)
                        average5curve.RemovePoint(0);

                    foreach (double v in avgz)
                        average5curve.AddPoint(v, v);
                }

                // Draw the X tics between the labels instead of 
                // at the labels
                //graph.GraphPane.XAxis.M.IsBetweenLabels = true;

                // Set the XAxis labels
                graphpane.XAxis.Scale.TextLabels = labels;
                // Set the XAxis to Text type
                graphpane.XAxis.Type = AxisType.Text;
            }
            catch
            {
            }
        }
        #endregion
    }
}

