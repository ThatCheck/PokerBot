// This application is covered by the LGPL Gnu license. See http://www.gnu.org/copyleft/lesser.html 
// for more information on this license.
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OddsGridApp
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
            Application.Run(new Form1());
        }
    }
}