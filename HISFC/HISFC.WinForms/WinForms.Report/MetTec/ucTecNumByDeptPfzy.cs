﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace Report.MetTec
{
    public partial class ucTecNumByDeptPfzy : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucTecNumByDeptPfzy()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {
            string deptCode = this.employee.Dept.ID;
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode);
        }
        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {

                this.dwMain.Print();
                return 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错", "提示");
                return -1;
            }

        }
    }
}
