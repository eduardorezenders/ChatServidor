using System;
using System.Windows.Forms;

namespace CRM.CHATSERVER
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmCHATSERVER());
        }
    }
}
