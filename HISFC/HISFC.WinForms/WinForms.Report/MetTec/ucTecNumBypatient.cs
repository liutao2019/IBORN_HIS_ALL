using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.MetTec
{
    public partial class ucTecNumBypatient : Common.ucQueryBaseForDataWindow 
    {
        public ucTecNumBypatient()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;
            //����ʱִ�в�ѯ
            //OnRetrieve(null);
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
            return base.OnRetrieve(confirmDept, this.dtpBeginTime.Value, this.dtpEndTime.Value);
        }
    }
}
