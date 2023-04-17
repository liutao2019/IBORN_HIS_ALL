using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.Pharmacy
{
    public partial class ucPhabMedFee : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhabMedFee()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
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

         
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);

        }

    }
}
