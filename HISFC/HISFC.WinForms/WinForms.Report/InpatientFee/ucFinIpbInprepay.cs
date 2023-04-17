using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucFinIpbInprepay : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbInprepay()
        {
            InitializeComponent();
        }

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList emplList = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

            TreeNode parentTreeNode = new TreeNode("���в���Ա");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Employee empl in emplList)
            {
                TreeNode emplNode = new TreeNode();
                emplNode.Tag = empl.ID;
                emplNode.Text = empl.Name;
                parentTreeNode.Nodes.Add(emplNode);
            }

            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            TreeNode selectNode = tvLeft.SelectedNode;
            string emplCode;
            switch (selectNode.Level)
            {
                case 0:
                    emplCode = "ALL";
                    break;
                default:
                    emplCode = selectNode.Tag.ToString();
                    break;
            }



            return base.OnRetrieve(base.beginTime, base.endTime, emplCode, string.IsNullOrEmpty(this.tbInvoiceNo.Text) ? "ALL" : this.tbInvoiceNo.Text.PadLeft(12, '0'), string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo) ? "ALL" : this.ucQueryInpatientNo1.InpatientNo);
        }
    }
}

