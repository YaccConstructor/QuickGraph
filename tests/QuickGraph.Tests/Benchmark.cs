using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace QuickGraph.Tests
{
    public static class PreciseTimer
    {
        [SuppressUnmanagedCodeSecurity]
        sealed class Win32
        {
            [DllImport("Kernel32.dll"), SuppressUnmanagedCodeSecurity]
            public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

            [DllImport("Kernel32.dll"), SuppressUnmanagedCodeSecurity]
            public static extern bool QueryPerformanceFrequency(out long lpFrequency);
        }

        private readonly static long frequency;
        static PreciseTimer()
        {
            if (!Win32.QueryPerformanceFrequency(out frequency))
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }
        }

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public static long Frequency
        {
            get { return frequency; }
        }

        /// <summary>
        /// Gets the current ticks value.
        /// </summary>
        /// <value>The now.</value>
        public static long Now
        {
            get
            {
                long startTime;
                if (!Win32.QueryPerformanceCounter(out startTime))
                    throw new Win32Exception("QueryPerformanceCounter failed");
                return startTime;
            }
        }

        /// <summary>
        /// Returns the duration of the timer (in seconds)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [Pure]
        public static double ToSeconds(long start, long end)
        {
            Contract.Requires(start >= 0 && end >= 0 && start <= end);

            return (end - start) / (double)frequency;
        }

        /// <summary>
        ///Returns the duration in seconds
        /// </summary>
        /// <param name="ticks">The ticks.</param>
        /// <returns></returns>
        [Pure]
        public static double ToSeconds(long ticks)
        {
            Contract.Requires(ticks >= 0);
            return ticks / (double)frequency;
        }

        /// <summary>
        ///Returns the duration in seconds from <paramref name="start"/>
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        public static double ToSecondsFromNow(long start)
        {
            return ToSeconds(start, PreciseTimer.Now);
        }
    }

    public sealed class Benchmark
    {
        readonly string name;
        long duration = 0;
        long samples = 0;

        public Benchmark(string name)
        {
            Contract.Requires(!String.IsNullOrEmpty(name));
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }

        public double Seconds
        {
            get { return PreciseTimer.ToSeconds(this.duration); }
        }

        public long Samples
        {
            get { return this.samples; }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}s, {2} samples", this.Name, this.Seconds, this.samples);
        }

        public void Run(Action action)
        {
            long start = PreciseTimer.Now;
            try
            {
                action();
            }
            finally
            {
                long finish = PreciseTimer.Now;
                this.duration += finish - start;
                this.samples++;
            }
        }
    }
}
