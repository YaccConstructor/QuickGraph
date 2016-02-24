using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mono.Addins;
using Common;


[assembly: AddinRoot("GraphTasks", "1.0")]
namespace MainForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            AddinManager.Initialize();
            AddinManager.Registry.Update(null);
            IAlgorithm[] addinInterface = (AddinManager.GetExtensionObjects(typeof(IAlgorithm))).Cast<IAlgorithm>().ToArray<IAlgorithm>();
            foreach (IAlgorithm i in addinInterface)
            {
                form.listBox1.Items.Add(i.Name);
            }
            form.panel2.Controls.Add(addinInterface[0].Input);
            Application.Run(form);
            
        }
    }
}
