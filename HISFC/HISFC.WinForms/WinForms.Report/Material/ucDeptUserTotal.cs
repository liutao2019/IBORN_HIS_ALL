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
    public partial class ucDeptUserTotal : Report.Common.ucQueryBaseForDataWindow
    {
        public ucDeptUserTotal()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if(base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.beginTime,this.endTime,this.employee.Dept.ID);
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        protected override int OnPrintPreview(object sender,object neuObject)
        {
            return base.OnPrintPreview(sender, neuObject);
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        protected override int OnPrint(object sender,object neuObject)
        {
            this.dwMain.Print(true, true);
            return 1;
        }
    }
}

