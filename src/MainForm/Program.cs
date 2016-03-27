using Common;
using Mono.Addins;
using System;
using System.Windows.Forms;

[assembly: AddinRoot("GraphTasks", "1.0")]
namespace MainForm
{
    internal static class Program
    {
        public static IAlgorithm[] Algorithms { get; private set; }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AddinManager.Initialize();
            AddinManager.Registry.Update();
            Algorithms = AddinManager.GetExtensionObjects<IAlgorithm>();

            var form = new Form1();
            foreach (var algorithm in Algorithms)
            {
                form.algorithmsList.Items.Add(algorithm.Name);
            }

            Application.Run(form);
        }
    }
}
