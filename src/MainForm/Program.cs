using Common;
using Mono.Addins;
using System;
using System.Linq;
using System.Windows.Forms;

[assembly: AddinRoot("GraphTasks", "1.0")]
namespace MainForm
{
    static class Program
    {
        private static IAlgorithm[] algs;

        public static IAlgorithm[] Algorithms
        {
            get
            {
                return algs;
            }
        }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AddinManager.Initialize();
            AddinManager.Registry.Update();
            algs = AddinManager.GetExtensionObjects<IAlgorithm>();

            Form1 form = new Form1();
            form.algorithmsList.Items.AddRange(algs.Select(x => x.Name).ToArray());

            Application.Run(form);

        }
    }
}
