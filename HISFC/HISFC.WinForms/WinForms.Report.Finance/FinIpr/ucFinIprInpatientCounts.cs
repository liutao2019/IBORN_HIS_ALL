using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpr
{
    public partial class ucFinIprInpatientCounts : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        /// <summary>
        /// ȫԺסԺ����ͳ��
        /// </summary>
        public ucFinIprInpatientCounts ()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�......");
                this.dwMain.Retrieve(new object[] { });
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
    }
}

