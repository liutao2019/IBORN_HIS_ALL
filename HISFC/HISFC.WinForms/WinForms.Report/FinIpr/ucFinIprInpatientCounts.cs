using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinIpr
{
    public partial class ucFinIprInpatientCounts : Report.Common.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// 全院住院患者统计
        /// </summary>
        public ucFinIprInpatientCounts ()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请稍候......");
                this.dwMain.Retrieve(new object[] { });
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }
    }
}

