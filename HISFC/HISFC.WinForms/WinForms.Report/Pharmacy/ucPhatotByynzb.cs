using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// ҩƷ�����б�ɹ������
    /// <br>��ѯҩ������ҩƷ���б�ҩ�Ͳɹ�ҩ�����������</br>
    /// </summary>
    public partial class ucPhatotByynzb :FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhatotByynzb()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.beginTime, this.endTime);
        }
    }
}
