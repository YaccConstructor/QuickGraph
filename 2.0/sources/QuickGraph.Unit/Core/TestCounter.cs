using System;

namespace QuickGraph.Unit.Core
{
    [Serializable]
    public sealed class TestCounter
    {
        private int totalCount=0;
        private int successCount;
        private int failureCount;
        private int ignoreCount;

        public TestCounter(int testCount)
        {
            this.totalCount = testCount;
        }

        public int TotalCount
        {
            get { return this.totalCount; }
        }

        public int RunCount
        {
            get { return this.successCount + this.failureCount + this.ignoreCount; }
        }

        public int SuccessCount
        {
            get { return this.successCount; }
            set { this.successCount = value; }
        }
        public int FailureCount
        {
            get { return this.failureCount; }
            set { this.failureCount = value; }
        }
        public int IgnoreCount
        {
            get { return this.ignoreCount; }
            set { this.ignoreCount = value; }
        }
        public bool HasFailures
        {
            get { return this.FailureCount>0; }
        }
        public bool Succeeded
        {
            get { return this.SuccessCount == this.TotalCount; }
        }

        public void Success()
        {
            this.successCount++;
        }
        public void Failure()
        {
            this.failureCount++;
        }
        public void Ignore()
        {
            this.ignoreCount++;
        }
        public double PercentRun
        {
            get
            {
                if (this.TotalCount == 0)
                    return 0;
                return (double)this.RunCount / this.TotalCount * 100;
            }
        }

        public double PercentSuccess
        {
            get
            {
                if (this.TotalCount == 0)
                    return 0;
                return (double)this.SuccessCount / this.TotalCount * 100;
            }
        }


        public void RollbackResults(TestCounter counter)
        {
            this.failureCount -= counter.FailureCount;
            this.successCount -= counter.SuccessCount;
            this.ignoreCount -= counter.IgnoreCount;
        }

        public static TestCounter Add(TestCounter a, TestCounter b)
        {
            TestCounter c = new TestCounter(a.totalCount + b.totalCount);
            c.successCount = a.successCount + b.successCount;
            c.failureCount = a.failureCount + b.failureCount;
            c.ignoreCount = a.ignoreCount + b.ignoreCount;
            return c;
        }

        public static TestCounter operator +(TestCounter a, TestCounter b)
        {
            return Add(a, b);
        }

        public override string ToString()
        {
            return String.Format("{0:0.00}% ({1},+{2},-{3},~{4})",
                this.PercentRun,
                this.RunCount,
                this.SuccessCount,
                this.FailureCount, 
                this.IgnoreCount);
        }
    }
}
