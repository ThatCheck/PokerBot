using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using HoldemHand;
using System.Threading;
using ZedGraph;

namespace Benchmark
{
    class TestThread : ITest
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
        System.Windows.Forms.Form form;
        #endregion

        #region Constructor
        public TestThread(System.Windows.Forms.Form form)
        {
            this.form = form;
            graphpane = new GraphPane();
            graphpane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);

            graphpane.XAxis.MajorGrid.IsVisible = true;
            graphpane.YAxis.MajorGrid.IsVisible = true;

            // Set the Titles
            graphpane.Title.Text = "Thread Pool - Hands()/Evaluate";
            graphpane.XAxis.Title.Text = "Hand Size";
            graphpane.YAxis.Title.Text = "Hands/Sec";
        }
        #endregion

        #region Test
        #region LoopTest

        const int N = 4;

        private delegate long LooperDelegate(int hands);

        static long Looper(int hands)
        {
            long count = 0;
            do
            {
                foreach (ulong mask in Hand.Hands(hands))
                {
                    Hand.Evaluate(mask, hands);
                    count++;
                }
            } while (count < 1000000);
            return count;
        }

        static long LooperWithDead(int hands)
        {
            long count = 0;
            do
            {
                foreach (ulong mask in Hand.Hands(0UL, 0UL, hands))
                {
                    Hand.Evaluate(mask, hands);
                    count++;
                }
            } while (count < 1000000);
            return count;
        }

        double Loop7Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                 results[i] = looper.BeginInvoke(7, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop6Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(6, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop5Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(5, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop4Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(4, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop3Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(3, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop2Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(2, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }

        double Loop1Test()
        {
            LooperDelegate looper = new LooperDelegate(Looper);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(1, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        #endregion
        #region Loop W/Dead Test
        double Loop7WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(7, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop6WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(6, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop5WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(5, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop4WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(4, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop3WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(3, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }
        double Loop2WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(2, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

            return count / (Hand.CurrentTime - start);
        }

        double Loop1WDeadTest()
        {
            LooperDelegate looper = new LooperDelegate(LooperWithDead);
            IAsyncResult[] results = new IAsyncResult[N];

            double start = Hand.CurrentTime;

            long count = 0;

            for (int i = 0; i < N; i++)
            {
                results[i] = looper.BeginInvoke(1, null, null);
            }

            for (int i = 0; i < N; i++)
            {
                count += looper.EndInvoke(results[i]);
            }

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
                    average7curve = graphpane.AddCurve("Avg Loop", null, avgy, Color.Purple, SymbolType.Circle);
                    //average7curve.Line.Fill = new Fill(Color.White, Color.LightSkyBlue, -45F);
                    //// Fix up the curve attributes a little
                    average7curve.Symbol.Size = 8.0F;
                    average7curve.Symbol.Fill = new Fill(Color.White);
                    average7curve.Line.Width = 2.0F;

                    /// Generate a black line with "Curve 4" in the legend
                    average5curve = graphpane.AddCurve("Avg Loop w/dead", null, avgz, Color.DarkBlue, SymbolType.TriangleDown);
                    //average7curve.Line.Fill = new Fill(Color.White, Color.LightSkyBlue, -45F);
                    //// Fix up the curve attributes a little
                    average5curve.Symbol.Size = 8.0F;
                    average5curve.Symbol.Fill = new Fill(Color.White);
                    average5curve.Line.Width = 2.0F;


                    myBar1 = graphpane.AddBar("Loop Test", null, y, Color.Aqua);
                    myBar2 = graphpane.AddBar("Loop W/Dead Test", null, z, Color.Blue);


                    myBar1.Bar.Fill = new Fill(Color.Aqua, Color.White, Color.Aqua);
                    myBar2.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
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

