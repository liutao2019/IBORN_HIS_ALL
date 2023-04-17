using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbFeeListQuery : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbFeeListQuery()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }

            FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();

            inState.ID = FS.HISFC.Models.Base.EnumInState.I.ToString();

            ArrayList emplList = managerIntegrate.QueryPatientBasic(base.employee.Dept.ID, inState);

            TreeNode parentTreeNode = new TreeNode("±¾¿Æ»¼Õß");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
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

            if (selectNode == null) 
            {
                return -1;
            }

            if (selectNode.Level == 0)
            {
                return -1;
            }
            string emplCode = selectNode.Tag.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, emplCode);
        }
    }
}

