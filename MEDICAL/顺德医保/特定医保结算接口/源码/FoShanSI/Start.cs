using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoShanSI
{
    static class Start
    {
        /// </summary>
        [STAThread]
        static void Main(params string[] param)
        {
            Application.EnableVisualStyles();
            Form f = new Form();
            f.Controls.Add(new Components.ucConnectServer());
            Application.Run(f);
        }
    }
}
