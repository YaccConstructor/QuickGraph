using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Common;
using ICSharpCode.AvalonEdit;
using MainForm.Properties;
using FontFamily = System.Windows.Media.FontFamily;

namespace MainForm
{
    public partial class MainForm : Form
    {
        private IAlgorithm _currentAlgorithm;
        private readonly TextEditor _editor;

        public MainForm()
        {
            InitializeComponent();

            _editor = new TextEditor { FontFamily = new FontFamily("Consolas") };
            editorHost.Child = _editor;
        }

        private void editorNewButton_Click(object sender, EventArgs e)
        {
            _editor.Clear();
            _editor.Focus();
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
                _editor.Text = await new StreamReader(dialog.FileName).ReadToEndAsync();
                _editor.Focus();
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
            var dotSource = _editor.Text;
            if (string.IsNullOrEmpty(dotSource))
            {
                MessageBox.Show(Resources.noInputGraph);
                return;
            }
            if (_currentAlgorithm == null) return;
            PlaybackControlClickHandler(() => _currentAlgorithm.Run(dotSource));
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            if (_currentAlgorithm == null) return;
            PlaybackControlClickHandler(() => _currentAlgorithm.NextStep());
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            if (_currentAlgorithm == null) return;
            PlaybackControlClickHandler(() => _currentAlgorithm.PreviousStep());
        }

        private void PlaybackControlClickHandler(Action algorithmAction)
        {
            try
            {
                algorithmAction();
                UpdatePlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{_currentAlgorithm.Name}:\n{ex.Source}: {ex.Message}");
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