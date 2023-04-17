using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.FinSim
{
    public partial class ucFinSimIpbCostLn : Report.Common.ucQueryBaseForDataWindow 
    {
        public ucFinSimIpbCostLn()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            dwDetail.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);

        }

    }
}
