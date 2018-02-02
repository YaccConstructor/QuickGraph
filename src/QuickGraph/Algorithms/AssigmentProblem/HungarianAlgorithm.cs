using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.AssigmentProblem
{
    public class HungarianAlgorithm
    {
        public enum Steps { Init, Step1, Step2, Step3, Step4, End }
        public int[] AgentsTasks;

        private Steps _step;
        private int[,] _costs;
        private byte[,] _masks;
        private bool[] _rowsCovered;
        private bool[] _colsCovered;
        private int _width;
        private int _height;
        private Location _pathStart;
        private Location[] _path;

        public HungarianAlgorithm(int[,] costs)
        {
            this._costs = costs;
            this._step = Steps.Init;
        }

        /// <summary>
        /// Returns assigments without visualisation
        /// </summary>
        /// <returns>Array of assigments</returns>
        public int[] Run()
        {
            while (DoStep() != Steps.End){}
            return AgentsTasks;
        }

        /// <summary>
        /// Returns iterations that can be used to visualise the algorithm
        /// </summary>
        /// <returns>List of iterations of algorithm</returns>
        public List<HungarianIteration> GetIterations()
        {
            var list = new List<HungarianIteration>();
            var step = Steps.Init;

            while (step != Steps.End)
            {
                step = DoStep();
                list.Add(new HungarianIteration
                {
                    Matrix = (int[,])_costs.Clone(),
                    Mask = (byte[,])_masks.Clone(),
                    RowsCovered = (bool[])_rowsCovered.Clone(),
                    ColsCovered = (bool[])_colsCovered.Clone(),
                    Step = step
                });
            }
            return list;
        }



        private Steps DoStep()
        {

            if (_step == Steps.Init)
            {
                _height = _costs.GetLength(0);
                _width = _costs.GetLength(1);

                //Reduse by rows
                for (var i = 0; i < _height; i++)
                {
                    var min = int.MaxValue;
                    for (var j = 0; j < _width; j++)
                        min = Math.Min(min, _costs[i, j]);
                    for (var j = 0; j < _width; j++)
                        _costs[i, j] -= min;
                }
                //Set 1 where job assigned
                _masks = new byte[_height, _width];
                _rowsCovered = new bool[_height];
                _colsCovered = new bool[_width];

                for (var i = 0; i < _height; i++)
                {
                    for (var j = 0; j < _width; j++)
                    {
                        if (_costs[i, j] == 0 && !_rowsCovered[i] && !_colsCovered[j])
                        {
                            _masks[i, j] = 1;
                            _rowsCovered[i] = true;
                            _colsCovered[j] = true;
                        }
                    }
                }

                ClearCovers(_rowsCovered, _colsCovered, _width, _height);

                _path = new Location[_width * _height];
                _pathStart = default(Location);
                _step = Steps.Step1;
                return Steps.Init;
            }
            if (_step != Steps.End)
            {
                switch (_step)
                {
                    case Steps.Step1:
                        {
                            Steps currentStep = _step;
                            _step = RunStep1(_masks, _colsCovered, _width, _height);
                            return currentStep;
                        }
                    case Steps.Step2:
                        {
                            Steps currentStep = _step;
                            _step = RunStep2(_costs, _masks, _rowsCovered, _colsCovered, _width, _height, ref _pathStart);
                            return currentStep;
                        }
                    case Steps.Step3:
                        {
                            Steps currentStep = _step;
                            _step = RunStep3(_masks, _rowsCovered, _colsCovered, _width, _height, _path, _pathStart);
                            return currentStep;
                        }
                    case Steps.Step4:
                        {
                            Steps currentStep = _step;
                            _step = RunStep4(_costs, _rowsCovered, _colsCovered, _width, _height);
                            return currentStep;
                        }
                }
                return Steps.End;
            }
            AgentsTasks = new int[_height];
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    if (_masks[i, j] == 1)
                    {
                        AgentsTasks[i] = j;
                        break;
                    }
                }
            }
            return Steps.End;
        }

        private Steps RunStep1(byte[,] masks, bool[] colsCovered, int w, int h)
        {
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (masks[i, j] == 1)
                        colsCovered[j] = true;
                }
            }
            var colsCoveredCount = 0;
            for (var j = 0; j < w; j++)
            {
                if (colsCovered[j])
                    colsCoveredCount++;
            }
            return colsCoveredCount == h ? Steps.End : Steps.Step2;
        }


        private Steps RunStep2(int[,] costs, byte[,] masks, bool[] rowsCovered, bool[] colsCovered, int w, int h,
            ref Location pathStart)
        {
            // Search for another assigment
            var loc = FindZero(costs, rowsCovered, colsCovered, w, h);
            // If there is not another options we should change matrix
            if (loc.Row == -1)
            {
                return Steps.Step4;
            }
            masks[loc.Row, loc.Column] = 2;
            var starCol = FindStarInRow(masks, w, loc.Row);
            if (starCol != -1)
            {
                rowsCovered[loc.Row] = true;
                colsCovered[starCol] = false;
            }
            else
            {
                pathStart = loc;
                return Steps.Step3;
            }
            return Steps.Step2;
        }


        private Steps RunStep3(byte[,] masks, bool[] rowsCovered, bool[] colsCovered, int w, int h,
            Location[] path, Location pathStart)
        {
            var pathIndex = 0;
            path[0] = pathStart;
            var row = FindStarInColumn(masks, h, path[pathIndex].Column);
            while (row != -1)
            {
                pathIndex++;
                path[pathIndex] = new Location(row, path[pathIndex - 1].Column);
                var col = FindPrimeInRow(masks, w, path[pathIndex].Row);
                pathIndex++;
                path[pathIndex] = new Location(path[pathIndex - 1].Row, col);
                row = FindStarInColumn(masks, h, path[pathIndex].Column);
            }
            ConvertPath(masks, path, pathIndex + 1);
            ClearCovers(rowsCovered, colsCovered, w, h);
            ClearPrimes(masks, w, h);
            return Steps.Step1;
        }

        private Steps RunStep4(int[,] costs, bool[] rowsCovered, bool[] colsCovered, int w, int h)
        {
            var minValue = FindMinimum(costs, rowsCovered, colsCovered, w, h);
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (rowsCovered[i])
                        costs[i, j] += minValue;
                    if (!colsCovered[j])
                        costs[i, j] -= minValue;
                }
            }
            return Steps.Step2;
        }

        private void ConvertPath(byte[,] masks, Location[] path, int pathLength)
        {
            for (var i = 0; i < pathLength; i++)
            {
                switch (masks[path[i].Row, path[i].Column])
                {
                    case 1:
                        masks[path[i].Row, path[i].Column] = 0;
                        break;
                    case 2:
                        masks[path[i].Row, path[i].Column] = 1;
                        break;
                }
            }
        }

        private Location FindZero(int[,] costs, bool[] rowsCovered, bool[] colsCovered,
            int w, int h)
        {
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (costs[i, j] == 0 && !rowsCovered[i] && !colsCovered[j])
                        return new Location(i, j);
                }
            }
            return new Location(-1, -1);
        }

        private int FindMinimum(int[,] costs, bool[] rowsCovered, bool[] colsCovered, int w, int h)
        {
            var minValue = int.MaxValue;
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (!rowsCovered[i] && !colsCovered[j])
                        minValue = Math.Min(minValue, costs[i, j]);
                }
            }
            return minValue;
        }

        private int FindStarInRow(byte[,] masks, int w, int row)
        {
            for (var j = 0; j < w; j++)
            {
                if (masks[row, j] == 1)
                    return j;
            }
            return -1;
        }

        private int FindStarInColumn(byte[,] masks, int h, int col)
        {
            for (var i = 0; i < h; i++)
            {
                if (masks[i, col] == 1)
                    return i;
            }
            return -1;
        }

        private int FindPrimeInRow(byte[,] masks, int w, int row)
        {
            for (var j = 0; j < w; j++)
            {
                if (masks[row, j] == 2)
                    return j;
            }
            return -1;
        }


        private void ClearCovers(bool[] rowsCovered, bool[] colsCovered, int w, int h)
        {
            for (var i = 0; i < h; i++)
                rowsCovered[i] = false;
            for (var j = 0; j < w; j++)
                colsCovered[j] = false;
        }

        private void ClearPrimes(byte[,] masks, int w, int h)
        {
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (masks[i, j] == 2)
                        masks[i, j] = 0;
                }
            }
        }

        ///<summary>
        ///Represents coordinates: raw and column number
        /// </summary>
        private struct Location
        {
            public int Row;
            public int Column;

            public Location(int row, int col)
            {
                this.Row = row;
                this.Column = col;
            }
        }
    }
}
