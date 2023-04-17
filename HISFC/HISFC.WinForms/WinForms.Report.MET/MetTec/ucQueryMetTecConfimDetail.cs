using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetTec
{
    public partial class ucQueryMetTecConfimDetail :FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucQueryMetTecConfimDetail()
        {
            InitializeComponent();
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

            string confirmDept;
            confirmDept = base.employee.Dept.ID;
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, confirmDept);
        }
  
    }
}
