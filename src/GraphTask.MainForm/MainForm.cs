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
                CleanOptionsAndPlaybackPanels();
            }

            try
            {
                _currentAlgorithm = Program.Algorithms[algorithmPicker.Text];
                algorithmInfoLabel.Text = _currentAlgorithm.Description;

                if (_currentAlgorithm.Options.Controls.Count != 0)
                {
                    algorithmOptionsGroupBox.Controls.Add(_currentAlgorithm.Options);
                    noOptionsLabel.Parent = null;
                }
                else
                {
                    noOptionsLabel.Text = Resources.noOptionsAvailableText;
                    algorithmOptionsGroupBox.Controls.Add(noOptionsLabel);
                }

                playbackPanel.Controls.Add(_currentAlgorithm.Output);
                UpdatePlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load plugin:\n{ex.Message}");
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            PlaybackControlClickHandler(() => _currentAlgorithm.Run(editorField.Text));
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            PlaybackControlClickHandler(() => _currentAlgorithm.NextStep());
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            PlaybackControlClickHandler(() => _currentAlgorithm.PreviousStep());
        }

        private void PlaybackControlClickHandler(Action algorithmAction)
        {
            if (_currentAlgorithm == null) return;
            try
            {
                algorithmAction();
                UpdatePlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{_currentAlgorithm.Name}: {ex.Message}");
            }
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

        private void CleanOptionsAndPlaybackPanels()
        {
            _currentAlgorithm.Output.Parent = null;
            _currentAlgorithm.Options.Parent = null;
            noOptionsLabel.Parent = null;

            playbackPanel.Controls.Clear();
            algorithmOptionsGroupBox.Controls.Clear();
        }
    }
}