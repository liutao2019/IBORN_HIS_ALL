using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;


namespace Report.MetTec
{
    public partial class uctec_num_bydept : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public uctec_num_bydept()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ��ӡ����
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
                MessageBox.Show("��ӡ����", "��ʾ");
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

            string name = this.employee.Name;
            string deptCode = this.employee.Dept.ID;
            return dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, name);
            //return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, name);
        }
    }
}
