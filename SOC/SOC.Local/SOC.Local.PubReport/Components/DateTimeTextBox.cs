using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class DateTimeTextBox : FS.FrameWork.WinForms.Controls.NeuTextBox
    {
        public DateTimeTextBox()
        {
            InitializeComponent();
        }

        public DateTimeTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        string err = "";
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                this.err = value;
            }
        }
        public DateTime GetStaticMonth()
        {
            string strMonth = this.Text;
            if (strMonth.Length < 6)
            {
                this.Err =  "统计月份输入不正确,输入格式为'yyyymm'如'200901'";
                return DateTime.MinValue;
            }
            try
            {
                int year = FS.FrameWork.Function.NConvert.ToInt32(strMonth.Substring(0, 4));
                int month = FS.FrameWork.Function.NConvert.ToInt32(strMonth.Substring(4, 2));
                return new DateTime(year, month, 01);
            }
            catch (Exception ex)
            {
                this.Err = "统计月份输入不正确,输入格式为'yyyymm'如'200901' " + ex.Message;
                return DateTime.MinValue;
            }
        }

        public void SetMonth(DateTime dt)
        {
            try
            {
                this.Text = dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
            }
        }
    }

   
}
