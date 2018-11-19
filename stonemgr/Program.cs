using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace stonemgr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Login login = new Login();
                login.ShowDialog();
                if (login.DialogResult == DialogResult.OK)
                {
                    Application.Run(new MainForm());//rum mainform
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message) ;
            }
        }
    }
}
