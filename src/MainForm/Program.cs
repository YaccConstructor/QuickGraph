using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common;
using Mono.Addins;

[assembly: AddinRoot("GraphTasks", "1.0")]

namespace MainForm
{
    internal static class Program
    {
        internal static Dictionary<string, IAlgorithm> Algorithms { get; private set; }
        internal static IAlgorithm CurrentAlgorithm { get; set; }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AddinManager.Initialize();
            AddinManager.Registry.Update();
            Algorithms = AddinManager.GetExtensionObjects<IAlgorithm>().ToDictionary(algorithm => algorithm.Name);

            var form = new MainForm();
            foreach (var algorithm in Algorithms.Values)
            {
                form.algorithmPicker.Items.Add(algorithm.Name);
            }
            Application.Run(form);
        }
    }
}