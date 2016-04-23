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

        private void algorithmPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.CurrentAlgorithm = Program.Algorithms[algorithmPicker.Text];
            var algorithm = Program.CurrentAlgorithm;

            algorithmInfoLabel.Text = algorithm.Description;
            algorithmPlaybackGroupBox.Enabled = true;

            algorithmOptionsGroupBox.Controls.Clear();
            noOptionsLabel.Text = Resources.noOptionsAvailableText;
            noOptionsLabel.Visible = algorithm.Options == null || algorithm.Options.Controls.Count == 0;

            if (algorithm.Options == null) return;
            foreach (Control control in algorithm.Options.Controls)
            {
                algorithmOptionsGroupBox.Controls.Add(control);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Program.CurrentAlgorithm.Run(editorField.Text);
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
    }
}