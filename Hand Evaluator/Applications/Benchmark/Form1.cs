using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ZedGraph;

namespace Benchmark
{
    public partial class Form1 : Form
    {
        List<ITest> tests = new List<ITest>();
        MasterPane master = null;
        Thread t = null;

        public Form1()
        {
            InitializeComponent();
            evaluateToolStripMenuItem.Checked = true;
            lowestToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = true;
            highestToolStripMenuItem.Checked = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (t != null)
            {
                t.Priority = ThreadPriority.Lowest;
                t.Abort();
            }
            base.OnClosing(e);
        }

        public void StopThread()
        {
            if (t != null)
            {
                t.Priority = ThreadPriority.Lowest;
                t.Abort();
                t = null;
            }
        }

        private void ThreadProc()
        {
            while (true)
            {
                try
                {
                    foreach (ITest test in tests)
                    {
                        test.RunBenchmark();
                        UpdateTest(test);
                        Thread.Sleep(50);
                    }
                }
                catch { }
            }
        }

        private delegate void UpdateTestDelegate(ITest test);

        private void UpdateTest(ITest test)
        {
            if (this.InvokeRequired)
            {
                ThreadPriority save = t.Priority;
               // t.Priority = ThreadPriority.Lowest;
                this.BeginInvoke(new UpdateTestDelegate(UpdateTest), test);
                //t.Priority = save;
                return;
            }

            test.UpdatePane();
            zg1.AxisChange();
            zg1.Invalidate();
        }

        private void InitMasterPane()
        {
            //System.Diagnostics.Debug.Assert(t != null);

            master.PaneList.Clear();

            // Display the MasterPane Title, and set the outer margin to 10 points
            master.Title.IsVisible = true;
            master.Title.Text = "Benchmarks";
            master.Margin.All = 10;

            foreach (ITest test in tests)
            {
                master.Add(test.Pane);
            }

            using (Graphics g = this.CreateGraphics())
            {
                master.SetLayout(g, PaneLayout.SquareRowPreferred);
            }

            if (t == null)
            {
                t = new Thread(new ThreadStart(ThreadProc));
                t.IsBackground = true;
                if (lowestToolStripMenuItem.Checked)
                    t.Priority = ThreadPriority.Lowest;
                //lowestToolStripMenuItem.Checked = false;
                if (belowNormalToolStripMenuItem.Checked)
                    t.Priority = ThreadPriority.BelowNormal;
                //belowNormalToolStripMenuItem.Checked = false;
                if (normalToolStripMenuItem.Checked)
                    t.Priority = ThreadPriority.Normal;
                //normalToolStripMenuItem.Checked = false;
                if (aboveNormalToolStripMenuItem.Checked)
                    t.Priority = ThreadPriority.AboveNormal;
                //aboveNormalToolStripMenuItem.Checked = true;
                //highestToolStripMenuItem.Checked = false;
                if (highestToolStripMenuItem.Checked)
                    t.Priority = ThreadPriority.AboveNormal;
                t.Start();
            }
        }

        private void UpdateMasterPane()
        {
            StopThread();
            master.PaneList.Clear();

            tests.Clear();

            // Load Tests
            if (evaluateToolStripMenuItem.Checked)
                tests.Add(new TestEvaluate(this));

            if (evaluateTypeToolStripMenuItem.Checked)
                tests.Add(new TestEvaluateType(this));

            if (inlineIterationToolStripMenuItem.Checked)
                tests.Add(new TestIterator(this));

            if (evaluateIterateToolStripMenuItem.Checked)
                tests.Add(new TestLoopEvaluate(this));

            if (inlineIterationToolStripMenuItem.Checked)
                tests.Add(new TestInlineEvaluate(this));

            if (threadPoolEvaluateIterateToolStripMenuItem.Checked)
                tests.Add(new TestThread(this));

            InitMasterPane();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            // First, clear out any old GraphPane's from the MasterPane collection
            master = zg1.MasterPane;
            UpdateMasterPane();
           
        }

        private void lowestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowestToolStripMenuItem.Checked = true;
            belowNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            highestToolStripMenuItem.Checked = false;
            t.Priority = ThreadPriority.Lowest;
        }

        private void belowNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowestToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = true;
            normalToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            highestToolStripMenuItem.Checked = false;
            t.Priority = ThreadPriority.BelowNormal;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowestToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = true;
            aboveNormalToolStripMenuItem.Checked = false;
            highestToolStripMenuItem.Checked = false;
            t.Priority = ThreadPriority.Normal;
        }

        private void aboveNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowestToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = true;
            highestToolStripMenuItem.Checked = false;
            t.Priority = ThreadPriority.AboveNormal;
        }

        private void highestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowestToolStripMenuItem.Checked = false;
            belowNormalToolStripMenuItem.Checked = false;
            normalToolStripMenuItem.Checked = false;
            aboveNormalToolStripMenuItem.Checked = false;
            highestToolStripMenuItem.Checked = true;
            t.Priority = ThreadPriority.Highest;
        }

        private void evaluateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            evaluateToolStripMenuItem.Checked = !evaluateToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void evaluateTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            evaluateTypeToolStripMenuItem.Checked = !evaluateTypeToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void inlineIterationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inlineIterationToolStripMenuItem.Checked = !inlineIterationToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void cIterationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cIterationToolStripMenuItem.Checked = !cIterationToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void evaluateIterateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            evaluateIterateToolStripMenuItem.Checked = !evaluateIterateToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void threadPoolEvaluateIterateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            threadPoolEvaluateIterateToolStripMenuItem.Checked = !threadPoolEvaluateIterateToolStripMenuItem.Checked;
            UpdateMasterPane();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            evaluateToolStripMenuItem.Checked = true;
            evaluateTypeToolStripMenuItem.Checked = true;
            inlineIterationToolStripMenuItem.Checked = true;
            cIterationToolStripMenuItem.Checked = true;
            evaluateIterateToolStripMenuItem.Checked = true;
            threadPoolEvaluateIterateToolStripMenuItem.Checked = true;
            UpdateMasterPane();
        }
    }
}