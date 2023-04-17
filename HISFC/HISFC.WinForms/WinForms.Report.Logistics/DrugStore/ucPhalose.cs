using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    public partial class ucPhalose : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhalose()
        {
            InitializeComponent();

        }

           /// <summary>
        /// 登录人员信息
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = null;

         
        
        
        protected override int OnRetrieve(params object[] objects)
        {
            this.employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
       
            return base.OnRetrieve(base.beginTime, base.endTime, employee.Dept.ID);
        }
       
    }
}

