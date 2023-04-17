using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpr
{
    public partial class ucFinIprChangePact :  FSDataWindow.Controls.ucQueryBaseForDataWindow //Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIprChangePact()
        {
            InitializeComponent();
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            return this.dwMain.Retrieve(this.beginTime,this.endTime);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
            }
            catch (Exception ex)
            { }
            
            return 1;
        }
    }
}

