using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    public partial class frmOutPatientQuery : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public frmOutPatientQuery()
        {
            InitializeComponent();
        }

        #region IPreArrange 成员

        /// <summary>
        /// 登陆前的各种判断
        /// </summary>
        /// <returns></returns>
        public int PreArrange()
        {
            Classes.LogManager.Write("【开始门诊医生from界面初始化前 动作】");

            return 1;
        }

        #endregion

    }
}
