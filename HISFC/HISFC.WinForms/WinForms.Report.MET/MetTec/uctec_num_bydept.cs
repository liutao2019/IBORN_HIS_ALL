using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;


namespace FS.Report.MET.MetTec
{
    public partial class uctec_num_bydept :FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public uctec_num_bydept()
        {
            InitializeComponent();
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
        //protected override void OnLoad()
        //{
        //    this.Init();
        //    base.OnLoad();
        //}

        protected override int OnRetrieve(params object[] objects)
        {
            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            if (beginTime > endTime)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                return -1;
            }
            string name = this.employee.Name;
            string deptCode = this.employee.Dept.ID;
            return dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, name);
            //return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, name);
        }
    }
}
