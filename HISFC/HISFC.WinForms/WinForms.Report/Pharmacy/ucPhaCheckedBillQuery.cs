﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucPhaCheckedBillQuery : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhaCheckedBillQuery()
        {
            InitializeComponent();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在检索数据请稍候......");

                //Application.DoEvents();

                //this.dwMain.Modify("time.text='统计时间：" + this.beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + this.endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                return this.dwMain.Retrieve(this.beginTime, this.endTime);
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
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

        private string queryStr = "((dept_name like '{0}%') or (dept_spell_name like '{0}%') or (dept_wb_name like '{0}%')) and ((check_code like '{1}%') )";

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            string dept = this.neuTextBox1.Text.Trim().ToUpper().Replace(@"\", "");
            string check = this.neuTextBox2.Text.Trim().ToUpper().Replace(@"\", "");


            if (dept.Equals("") && check.Equals("") )
            {
                this.dwMain.SetFilter("");
                this.dwMain.Filter();
                return;
            }

            string str = string.Format(this.queryStr, dept, check);
            this.dwMain.SetFilter(str);
            this.dwMain.Filter();
        }

    }
}
