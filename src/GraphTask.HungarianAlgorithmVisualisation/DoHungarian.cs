using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common;
using Mono.Addins;
using QuickGraph.Algorithms.AssigmentProblem;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]


namespace GraphTask.HungarianAlgorithmVisualisation
{
    [Extension]
    public class HungarianPlugin : IAlgorithm
    {
        private readonly TextBox _dimensionTextBox;
        private readonly CheckBox _showMessages;
        private readonly TextBox _resultTextBox;
        private readonly TextBox _colsTextBox;
        private readonly TextBox _rowsTextBox;

        private HungarianAlgorithm _algorithm;
        private int _iteration;
        private int _dimension;
        private Dictionary<int, List<HungarianState>> _states;
        private bool _canGoFurther;
        private List<HungarianIteration> _iterations;

        public HungarianPlugin()
        {
            _dimensionTextBox = new TextBox { Text = "3", Location = new Point(12, 30) };
            _showMessages = new CheckBox { Text = "Show messages", Location = new Point(12, 6), Checked = true };
            _resultTextBox = new TextBox { Text = "", Location = new Point(12, 54), Enabled = false };
            _colsTextBox = new TextBox { Text = "", Location = new Point(230, 22), Enabled = false };
            _rowsTextBox = new TextBox { Text = "", Location = new Point(230, 64), Enabled = false };
            var resLabel = new Label { Location = new Point(120, 54), Text = "result" };
            var dimensionLabel = new Label { Location = new Point(120, 30), Text = "dimension" };
            var colsLabel = new Label { Location = new Point(230, 2), Text = "colsCovered:" };
            var rowsabel = new Label { Location = new Point(230, 44), Text = "rowsCovered:" };
            Options.Controls.AddRange(new Control[] { _showMessages, _dimensionTextBox, dimensionLabel,
                _resultTextBox, resLabel, _colsTextBox, _rowsTextBox, colsLabel, rowsabel });
        }

        public string Name =>
          "Hungarian Algorithm (matrix)";
        public string Author =>
          "Egor Gumin";

        public string Description =>
          "The Hungarian method is a combinatorial optimization algorithm\n" +
          "solves the assignment problem in polynomial time and which\n" +
          "anticipated later primal-dual methods.";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            _iteration = 0;

            Output.Controls.Clear();

            if (int.TryParse(_dimensionTextBox.Text, out _dimension))
            {
                if (_dimension < 2)
                {
                    MessageBox.Show("Dimension must be at least 2");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error when try to parse dimension.");
                return;
            }

            for (var i = 0; i < _dimension; i++)
            {
                for (var j = 0; j < _dimension; j++)
                {
                    var cell = new TextBox { Text = "0", Location = new Point(j * 60, i * 30), Size = new Size { Height = 20, Width = 50 } };
                    Output.Controls.Add(cell);
                }
            }
            _states = new Dictionary<int, List<HungarianState>>();
            _canGoFurther = true;
        }

        private void SaveState(int key)
        {
            if (!_states.ContainsKey(key))
            {
                _states[key] = new List<HungarianState>();
                foreach (var control in Output.Controls)
                {
                    var cell = (TextBox)control;
                    _states[key].Add(new HungarianState
                    {
                        Color = cell.BackColor,
                        Value = cell.Text
                    });
                }
            }
        }

        private void SetMatrix()
        {
            var maxtrix = _iterations[_iteration].Matrix;
            for (var i = 0; i < _dimension; i++)
            {
                for (var j = 0; j < _dimension; j++)
                {
                    var cell = (TextBox)Output.Controls[i * _dimension + j];
                    cell.Text = maxtrix[i, j] + "";
                }
            }
        }

        private void SetCovereds(int iteration)
        {
            var cols = _iterations[iteration].ColsCovered;
            var rows = _iterations[iteration].RowsCovered;
            var strCols = "";
            var strRows = "";
            for (int i = 0; i < _dimension; i++)
            {
                if (cols[i])
                {
                    strCols += i + " ";
                }
                if (rows[i])
                {
                    strRows += i + " ";
                }
            }
            _colsTextBox.Text = strCols;
            _rowsTextBox.Text = strRows;
        }

        public void NextStep()
        {
            if (_iteration == 0)
            {
                var matrix = new int[_dimension, _dimension];
                var index = 0;
                foreach (var cell in Output.Controls)
                {
                    var txtBox = (TextBox)cell;
                    int num;
                    if (!int.TryParse(txtBox.Text, out num))
                    {
                        MessageBox.Show("Error when trying to parse matrix. Integers expected.");
                        return;
                    }
                    txtBox.Enabled = false;
                    matrix[index / _dimension, index % _dimension] = num;
                    index++;
                }
                _algorithm = new HungarianAlgorithm(matrix);
                _iterations = _algorithm.GetIterations();
            }
            SaveState(_iteration);
            var step = _iterations[_iteration].Step;

            if (step != HungarianAlgorithm.Steps.End)
            {
                SetMatrix();
                SetCovereds(_iteration);
                switch (step)
                {
                    case HungarianAlgorithm.Steps.Init:
                        HighlightDiffs();
                        if (_showMessages.Checked)
                        {
                            MessageBox.Show("Reduce the matrix by rows.");
                        }
                        break;
                    case HungarianAlgorithm.Steps.Step1:
                        HighlightMask();
                        if (_showMessages.Checked)
                        {
                            MessageBox.Show("Choose appropriate zeros. Mark covered columns. Covered column is a column, where a zero was chosen (job assigned).");
                        }
                        break;
                    case HungarianAlgorithm.Steps.Step2:
                        HighlightMask();
                        if (_showMessages.Checked)
                        {
                            MessageBox.Show("Looking for another assigments (zeros) in uncovered columns and rows. If there are not other assigments, transform the matrix. " +
                                            Environment.NewLine + "If there is one then check up whether we have chosen other assigment in this row. " +
                                            Environment.NewLine + "If yes, then abort it (uncover last assigment's column and cover its row, search again). " +
                                            Environment.NewLine + "if no, choose appropriate zeros.");
                        }
                        break;
                    case HungarianAlgorithm.Steps.Step3:
                        HighlightMask();
                        if (_showMessages.Checked)
                        {
                            MessageBox.Show("The new row assigment was chosen. Choose appropriate zeros (assigments). Clear covers.");
                        }
                        break;
                    case HungarianAlgorithm.Steps.Step4:
                        HighlightDiffs();
                        if (_showMessages.Checked)
                        {
                            MessageBox.Show("There are no other ways to reassign (choose uncovered zeros). Find the minimum in the uncovered cells. " +
                                            Environment.NewLine + "Then transform the matrix: coveredRows + min, uncoveredCols - min.");
                        }
                        break;
                }
            }
            else
            {
                if (_showMessages.Checked)
                {
                    MessageBox.Show("We have found an optimal result. Look at the result textbox for the assigment array.");
                }
                SetResult();
                _canGoFurther = false;
            }
            _iteration++;
        }

        private void HighlightDiffs()
        {
            var i = 0;
            var oldStates = _states[_iteration];
            foreach (var control in Output.Controls)
            {
                var cell = (TextBox)control;
                cell.BackColor = cell.Text != oldStates[i].Value ? Color.PowderBlue : Color.White;
                i++;
            }
        }

        private void HighlightMask()
        {
            for (var i = 0; i < _dimension; i++)
            {
                for (var j = 0; j < _dimension; j++)
                {
                    var cell = (TextBox)Output.Controls[i * _dimension + j];
                    switch (_iterations[_iteration].Mask[i, j])
                    {
                        case 0:
                            cell.BackColor = Color.White;
                            break;
                        case 1:
                            cell.BackColor = Color.LightGoldenrodYellow;
                            break;
                        case 2:
                            cell.BackColor = Color.PaleGreen;
                            if (_iterations[_iteration - 1].Mask[i, j] == 0)
                            {
                                cell.BackColor = Color.Violet;
                            }
                            break;
                    }
                }
            }
        }

        private void SetResult()
        {
            var res = _algorithm.AgentsTasks;
            var str = "";
            foreach (var x in res)
            {
                str += x + " ";
            }
            _resultTextBox.Text = str;
        }


        private void ApplyState(int key)
        {
            var states = _states[key];
            var i = 0;
            foreach (var state in states)
            {
                var cell = (TextBox)Output.Controls[i++];
                cell.Text = state.Value;
                cell.BackColor = state.Color;
            }
            if (key == 0)
            {
                _rowsTextBox.Text = "";
                _colsTextBox.Text = "";
            }
            else
            {
                SetCovereds(key - 1);
            }
        }

        public void PreviousStep()
        {
            ApplyState(--_iteration);
            _canGoFurther = true;
            _resultTextBox.Text = "";
        }

        public bool CanGoBack =>
          _iteration > 0;

        public bool CanGoFurther =>
          _canGoFurther;
    }
}