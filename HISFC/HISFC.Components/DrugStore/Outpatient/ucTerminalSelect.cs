using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �ն�ѡ��ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// </summary>
    public partial class ucTerminalSelect : UserControl
    {
        public ucTerminalSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �������
        /// </summary>
        DialogResult result = DialogResult.Cancel;

        /// <summary>
        /// �������
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        /// <summary>
        /// �б��ʼ��
        /// </summary>
        public void InitList()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            ArrayList alTermianalList = drugStoreManager.QueryDrugTerminalByTerminalType("0");
            if (alTermianalList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���������ն��б�������") + drugStoreManager.Err);
                return;
            }

            this.tvList.ImageList = this.tvList.groupImageList;

            string privDeptCode = "";
            TreeNode deptNode = null;
            foreach (FS.HISFC.Models.Pharmacy.DrugTerminal info in alTermianalList)
            {
                if (privDeptCode != info.Dept.ID)
                {
                    FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(info.Dept.ID);
                    if (dept == null)
                    {
                        continue;
                    }
                    info.Dept = dept;

                    deptNode = new TreeNode(dept.Name);                    
                    deptNode.ImageIndex = 0;
                    deptNode.SelectedImageIndex = 0;
                    deptNode.Tag = null;

                    privDeptCode = info.Dept.ID;

                    this.tvList.Nodes.Add(deptNode);
                }

                TreeNode terminalNode = new TreeNode(info.Name);
                terminalNode.ImageIndex = 1;
                terminalNode.SelectedImageIndex = 1;
                terminalNode.Tag = info;

                deptNode.Nodes.Add(terminalNode);
            }
        }

        public List<FS.HISFC.Models.Pharmacy.DrugTerminal> GetTerminalList()
        {
            List<FS.HISFC.Models.Pharmacy.DrugTerminal> terminalList = new List<FS.HISFC.Models.Pharmacy.DrugTerminal>();
            foreach (TreeNode deptNode in this.tvList.Nodes)
            {
                if (deptNode.Nodes.Count > 0)
                {
                    foreach (TreeNode terminalNode in deptNode.Nodes)
                    {
                        if (terminalNode.Checked)
                        {
                            terminalList.Add(terminalNode.Tag as FS.HISFC.Models.Pharmacy.DrugTerminal);
                        }
                    }
                }
            }

            return terminalList;
        }

        public virtual void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.GetTerminalList();

            this.result = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.Cancel;

            this.Close();
        }

        private void ucTerminalSelect_Load(object sender, EventArgs e)
        {
            this.InitList();

            this.tvList.AfterCheck += new TreeViewEventHandler(tvList_AfterCheck);
        }

        void tvList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }
    }
}
