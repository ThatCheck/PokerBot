using System;
using System.Collections.Generic;
using System.Text;
using ZedGraph;

namespace Benchmark
{
    interface ITest
    {
        void RunBenchmark();
        GraphPane Pane { get;}
        void UpdatePane();
    }
}
