using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.WinForms.Report.FinIpb
{
    public partial class ucFinIpbByExecDeptConfirm : ucCrossReportBase
    {
        public ucFinIpbByExecDeptConfirm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ȡsql��ID����
        /// </summary>
        protected override void GetSql()
        {
            this.htSQL.Add(1, "JNHIS.LOCAL.Recalled.finIpbExecDeptQuery.Detail.1");
            this.htSQL.Add(2, "JNHIS.LOCAL.Recalled.finIpbExecDeptQuery.Detail.2");
            this.htSQL.Add(3, "JNHIS.LOCAL.Recalled.finIpbExecDeptQuery.Detail.3");
        }

        /// <summary>
        /// ���ӵ���������Ա���
        /// </summary>
        protected override void InitComboBox()
        {
            FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alPact = interMgr.GetConstantList("PACTUNIT");
            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
            c.ID = "ALL";
            c.Name = "ȫ��";
            c.IsValid = true;
            c.SortID = 0;
            alPact.Insert(0, c);
            base.cmbDept.AddItems(alPact);
            base.cmbDept.SelectedIndex = 0;
        }
    }

}
