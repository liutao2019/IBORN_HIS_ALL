using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.DrugStore
{
    public partial class ucSalesState : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucSalesState()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 常数管理类－取常数列表


        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(base.beginTime, base.endTime, base.employee.Dept.ID);
        }
        
    }
}  

