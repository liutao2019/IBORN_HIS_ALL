using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucFinIpbMedDept : Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbMedDept()
        {
            InitializeComponent();
            this.neuComboBoxType.SelectedIndex = 0;
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string deptType = "";
            //��������Ϊȫ��
            if (this.neuComboBoxType.SelectedIndex == 0)
            {
                deptType = "A";
            }
            else
            {
                //��������Ϊ����
                if (this.neuComboBoxType.SelectedIndex == 1)
                {
                    deptType = "M";
                }
                //��������Ϊ��Ժ
                else
                {
                    deptType = "Z";
                }
            }

            return base.OnRetrieve(beginTime, endTime, deptType);
        }
    }
}
