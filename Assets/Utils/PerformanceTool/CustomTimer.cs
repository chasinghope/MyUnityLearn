using System;
using System.Diagnostics;

namespace Chasing.Utils
{
    /// <summary>
    /// 代码耗时检测
    /// 1. 用于性能分析
    /// </summary>
    public class CustomTimer : IDisposable
    {
        private string timerName;
        private int numTests;
        private Stopwatch watch;

        public CustomTimer(string timeName, int numTests = 1)
        {
            this.timerName = timeName;
            this.numTests = numTests;
            if (this.numTests <= 0)
                this.numTests = 1;
            watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            watch.Stop();
            float ms = watch.ElapsedMilliseconds;
            UnityEngine.Debug.Log($"{timerName} fininshed. Total: {ms: 0.00} ms, per-test：{ms/ numTests: 0.000000} ms, TestNum: {numTests}");
        }
    }

}