using System;
using System.Windows.Forms;
using Common;

namespace MainForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void algorithmsList_SelectedValueChanged(object sender, EventArgs e)
        {
            codeEditorPanel.Controls.Clear();
            playerPanel.Controls.Clear();

            IAlgorithm algorithm = Program.Algorithms[algorithmsList.SelectedIndex];
            codeEditorPanel.Controls.Add(algorithm.Input);
        }
    }
}
