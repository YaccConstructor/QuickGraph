namespace QuickGraph.Algorithms.AssigmentProblem
{
    public class HungarianIteration
    {
        public int[,] Matrix { get; set; }
        public byte[,] Mask { get; set; }

        public bool[] RowsCovered { get; set; }

        public bool[] ColsCovered { get; set; }


        public HungarianAlgorithm.Steps Step { get; set; }
    }
}
