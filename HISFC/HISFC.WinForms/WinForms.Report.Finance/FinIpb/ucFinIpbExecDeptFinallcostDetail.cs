﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// 执行科室收入明细
    /// </summary>
    public partial class ucFinExecDeptFinallcostDetail:FSDataWindow.Controls.ucQueryBaseForDataWindow//FrameWork.WinForms.Controls.ucQueryBaseForDataWindow
    {
        public ucFinExecDeptFinallcostDetail ()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        /// 

        protected override int OnRetrieve(params object[] objects)
        {
       
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            //try
            //{
                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索数据请稍候......");

                //Application.DoEvents();

                //this.dwMain.Modify("time.text='统计时间：" + this.beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + this.endTime.ToString("yyyy-MM-dd HH:mm:ss") + " 执行科室: " + this.employee.Dept.Name + "'");
                return this.dwMain.Retrieve(this.employee.Dept.ID,this.beginTime, this.endTime,this.employee.Dept.Name);
            //}
            //catch (Exception ex)
            //{
            //    return -1;
            //}
            //finally
            //{
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //}
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
            }
            catch (Exception ex)
            {
                return 1;
            }

            return 1;
        }

        private void neuLabelTextBox1_TextChanged(object sender, EventArgs e)
        {
            string filterStr = this.neuTextBox1.Text.Trim();
            string feeStr = this.txtfeetype.Text.Trim();
            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }
            if (filterStr.Equals("") && feeStr.Equals(""))
            {
                //this.dwMain.SetFilter("");
                //this.dwMain.Filter();
                dv.RowFilter = "";
                return;
            }

            filterStr = filterStr.Replace(@"\", "").Replace(@"'", "").ToUpper();
            feeStr = feeStr.Replace(@"\", "").Replace(@"'", "").ToUpper();
            filterStr = string.Format(this.FilterStr, filterStr, feeStr);

            //this.dwMain.SetFilter(filterStr);
            //this.dwMain.Filter();

            try
            {
                dv.RowFilter = filterStr;
            }
            catch
            {
                MessageBox.Show("请输入正确信息，不许输入特殊字符");
                return;
            }
            
        }

        /// <summary>
        /// 过滤字符串

        /// </summary>
        string FilterStr = "((item_name like '{0}%') or (spell_code like '{0}%') or (wb_code like '{0}%')) and ((fee_name like '{1}%') or (fee_spell_code like '{1}%') or (fee_wb_code like '{1}%'))";
        //string FilterStr = "((item_name like '{0}%') or (spell_code like '{0}%') or (wb_code like '{0}%')) and ((fee_name like '{1}%') or (fee_name_spell like '{1}%'))";

    }
}
