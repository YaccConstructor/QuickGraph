using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Common;
using MainForm.Properties;

namespace MainForm
{
    public partial class MainForm : Form
    {
        private static IAlgorithm _currentAlgorithm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void editorNewButton_Click(object sender, EventArgs e)
        {
            editorField.Clear();
            editorField.Focus();
        }

        private async void editorOpenButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = Resources.dotFileTypes,
                Multiselect = false
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                editorField.Text = await new StreamReader(dialog.FileName).ReadToEndAsync();
                editorField.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Resources.couldNotReadFile}\n{ex.Message}");
            }
        }

        private void algorithmPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentAlgorithm != null)
            {
                if (_currentAlgorithm.Name == algorithmPicker.Text) return;
                RestoreAlgorithmControls();
            }

            _currentAlgorithm = Program.Algorithms[algorithmPicker.Text];
            algorithmInfoLabel.Text = _currentAlgorithm.Description;

            if (_currentAlgorithm.Options == null) return;
            foreach (Control control in _currentAlgorithm.Options.Controls)
            {
                algorithmOptionsGroupBox.Controls.Add(control);
            }
            foreach (Control control in _currentAlgorithm.Output.Controls)
            {
                playbackPanel.Controls.Add(control);
            }

            noOptionsLabel.Text = Resources.noOptionsAvailableText;
            noOptionsLabel.Visible = algorithmOptionsGroupBox.Controls.Count == 0;
            UpdatePlayer();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (_currentAlgorithm == null) return;
            _currentAlgorithm.Run(editorField.Text);
            UpdatePlayer();
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            if (_currentAlgorithm == null) return;
            _currentAlgorithm.NextStep();
            UpdatePlayer();
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            if (_currentAlgorithm == null) return;
            _currentAlgorithm.PreviousStep();
            UpdatePlayer();
        }

        private void UpdatePlayer()
        {
            var canGoFurther = _currentAlgorithm != null && _currentAlgorithm.CanGoFurther;
            var canGoBack = _currentAlgorithm != null && _currentAlgorithm.CanGoBack;

            startButton.Enabled = _currentAlgorithm != null;
            nextStepButton.Enabled = canGoFurther;
            previousStepButton.Enabled = canGoBack;
            algorithmFinishedLabel.Visible = canGoBack && !canGoFurther;
        }

        private void MoveControlsToPanel(IEnumerable collection, Control panel)
        {
            foreach (Control control in collection)
            {
                if (control != noOptionsLabel) panel.Controls.Add(control);
            }
        }

        private void RestoreAlgorithmControls()
        {
            if (_currentAlgorithm == null) return;

            MoveControlsToPanel(algorithmOptionsGroupBox.Controls, _currentAlgorithm.Options);
            MoveControlsToPanel(playbackPanel.Controls, _currentAlgorithm.Output);
        }
    }
}