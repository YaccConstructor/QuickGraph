using System;
using System.IO;
using System.Windows.Forms;
using MainForm.Properties;

namespace MainForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void RestoreAlgorithmOptions()
        {
            foreach (Control control in algorithmOptionsGroupBox.Controls)
            {
                Program.CurrentAlgorithm.Options.Controls.Add(control);
            }
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
            if (Program.CurrentAlgorithm != null)
            {
                if (Program.CurrentAlgorithm.Name == algorithmPicker.Text) return;
                RestoreAlgorithmOptions();
            }

            Program.CurrentAlgorithm = Program.Algorithms[algorithmPicker.Text];
            var algorithm = Program.CurrentAlgorithm;
            algorithmInfoLabel.Text = algorithm.Description;

            if (algorithm.Options == null) return;
            foreach (Control control in algorithm.Options.Controls)
            {
                algorithmOptionsGroupBox.Controls.Add(control);
            }

            noOptionsLabel.Text = Resources.noOptionsAvailableText;
            noOptionsLabel.Visible = algorithmOptionsGroupBox.Controls.Count == 0;
            UpdatePlayer();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (Program.CurrentAlgorithm == null) return;
            Program.CurrentAlgorithm.Run(editorField.Text);
            UpdatePlayer();
        }

        private void nextStepButton_Click(object sender, EventArgs e)
        {
            if (Program.CurrentAlgorithm == null) return;
            Program.CurrentAlgorithm.NextStep();
            UpdatePlayer();
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            if (Program.CurrentAlgorithm == null) return;
            Program.CurrentAlgorithm.PreviousStep();
            UpdatePlayer();
        }

        private void UpdatePlayer()
        {
            var algorithm = Program.CurrentAlgorithm;
            var canGoFurther = algorithm != null && algorithm.CanGoFurther;
            var canGoBack = algorithm != null && algorithm.CanGoBack;

            startButton.Enabled = algorithm != null;
            nextStepButton.Enabled = canGoFurther;
            previousStepButton.Enabled = canGoBack;
            algorithmFinishedLabel.Visible = canGoBack && !canGoFurther;
        }
    }
}