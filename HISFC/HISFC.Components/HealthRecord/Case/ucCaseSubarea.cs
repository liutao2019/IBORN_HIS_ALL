using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    /// <summary>
    /// ucCabinet<br></br>
    /// [��������: ��������վά��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-09-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaseSubarea : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.HealthRecord.Case.CaseSubareaManager cbManager = new FS.HISFC.BizLogic.HealthRecord.Case.CaseSubareaManager();

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("���", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "���":
                    this.Add();
                    break;
                default:
                    break;
            }
        }

        private void Add()
        {
            FS.HISFC.Components.HealthRecord.Case.Controls.ucSubareaHandler uc = new FS.HISFC.Components.HealthRecord.Case.Controls.ucSubareaHandler();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        public ucCaseSubarea()
        {
            InitializeComponent();
        }

        public void FillTreeview()
        {
            FS.HISFC.BizProcess.Integrate.Manager constant = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList alConstant = constant.GetConstantList("CASE13");

            TreeNode root = new TreeNode("���з���");
            this.neuTreeView1.Nodes.Add(root);

            if (alConstant.Count == 0 || alConstant == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ����ʧ��"));
                return;
            }
            for (int i = 0; i < alConstant.Count; i++)
            {
                FS.HISFC.Models.Base.Const cons = alConstant[i] as FS.HISFC.Models.Base.Const;
                if (cons == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ʧ��"));
                    return;
                }
                TreeNode tn = new TreeNode(cons.Name);
                tn.Tag = cons;
                root.Nodes.Add(tn);

                
            }
        }

        private void neuTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Clear();

            if (e.Node.Parent == null)
            {
                return;
            }
            FS.HISFC.Models.Base.Const subarea = e.Node.Tag as FS.HISFC.Models.Base.Const;
            if( subarea == null )
            {
                return;
            }

            List<FS.HISFC.Models.HealthRecord.Case.CaseSubarea> listSubarea = this.cbManager.QueryBySubareaID(subarea.ID);
            if (listSubarea == null || listSubarea.Count == 0)
            {
                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.Case.CaseSubarea area in listSubarea)
            {
                this.FillFP(area);
            }


        }

        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
        }

        private void FillFP(FS.HISFC.Models.HealthRecord.Case.CaseSubarea subarea)
        {
            int rowIndex = this.neuSpread1_Sheet1.Rows.Count;            
            
            this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);

            this.neuSpread1_Sheet1.SetText(rowIndex, 0, subarea.SubArea.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 1, subarea.SubArea.Name);
            this.neuSpread1_Sheet1.SetText(rowIndex, 2, subarea.NurseStation.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 3, subarea.NurseStation.Name);

            this.neuSpread1_Sheet1.Rows[rowIndex].Tag = subarea;
        }

        private void ucCaseSubarea_Load(object sender, EventArgs e)
        {
            this.FillTreeview();
        }
    }
}