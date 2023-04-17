using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.Material
{
    public partial class ucMatInputCollect : Report.Common.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucMatInputCollect()
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
            return base.OnRetrieve(this.beginTime,this.endTime,this.employee.Dept.ID);
        }
    }
}

