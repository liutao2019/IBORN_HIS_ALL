using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinSim
{
    public partial class ucOpbCostLN : Common.ucQueryBaseForDataWindow 
    {
        public ucOpbCostLN()
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

        private void dwMain_Click(object sender, EventArgs e)
        {

        }

    }
}
