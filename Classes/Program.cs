using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace A17_Ex01_Maor_308345354_Nir_032620890
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
            Application.Run(new FormFaceBook());
        }
    }
}
