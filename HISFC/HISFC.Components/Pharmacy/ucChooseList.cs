using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ�б����ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucChooseList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChooseList()
        {
            InitializeComponent();
        }

        public delegate void ChooseDataHandler(FarPoint.Win.Spread.SheetView sv,int activeRowIndex);

        public event System.EventHandler TvNodeAfterSelect;

        public event System.EventHandler TvNodeAfterClick;

        public event System.EventHandler TvNodeDoubleClick;

        public event System.EventHandler TvMouseRight;

        public event ChooseDataHandler ChooseDataEvent;        

        #region ����

        /// <summary>
        /// �Ƿ���ʾ���ڵ�
        /// </summary>
        [Description("�Ƿ���ʾ�����б�"), Category("����"), DefaultValue(true)]
        public bool IsShowTree
        {
            get
            {
                return this.tvList.Visible;
            }
            set
            {
                this.tvList.Visible = value;

                this.titlePanel.Visible = value;
            }
        }

        /// <summary>
        /// �б�����
        /// </summary>
        [Description("�б���������"), Category("����"), DefaultValue("�� ��")]
        public string ListTitle
        {
            get
            {
                return this.lbTitle.Text;
            }
            set
            {
                this.lbTitle.Text = value;
            }
        }

        /// <summary>
        /// ��ǰѡ�нڵ�
        /// </summary>
        public TreeNode SelectedNode
        {
            get
            {
                return this.tvList.SelectedNode;
            }
        }

        /// <summary>
        /// ��ǰ��ʵ��
        /// </summary>
        public FS.HISFC.Components.Common.Controls.baseTreeView TvList
        {
            get
            {
                return this.tvList;
            }
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        public void SetFoucs()
        {
            this.ucDrugList1.Select();
            this.ucDrugList1.SetFocusSelect();
        }

        /// <summary>
        /// ��ʾҩƷ�б�
        /// </summary>
        public void ShowPharmacyList()
        {
            this.ucDrugList1.ShowPharmacyList();                   
        }

        /// <summary>
        /// ��ʾ���ҩƷ�б�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="isBatch">�Ƿ������</param>
        public void ShowDeptStorage(string deptCode, bool isBatch)
        {
            this.ucDrugList1.ShowDeptStorage(deptCode, isBatch, 0);
        }

        /// <summary>
        /// ��ȡ��ʾָ���еĿ��
        /// </summary>
        /// <param name="columnNum"></param>
        /// <param name="iWidth"></param>
        public void GetColumnWidth(int columnNum,ref int iWidth)
        {
            this.ucDrugList1.GetColumnWidth(columnNum, ref iWidth);
        }

        /// <summary>
        /// �����
        /// </summary>
        public void TreeClear()
        {
            this.tvList.Nodes.Clear();
        }

        /// <summary>
        /// �����б�����ӽڵ�
        /// </summary>
        /// <param name="node">����ӵĽڵ�</param>
        /// <param name="isRootNode">�Ƿ���ڵ�</param>
        /// <param name="isBrotherNode">�Ƿ�ǰѡ�нڵ���ֵܽڵ�</param>
        public void AddTreeNode(TreeNode node,bool isRootNode,bool isBrotherNode)
        {
            if (isRootNode)
            {
                this.tvList.Nodes.Add(node);
            }
            else
            {
                if (isBrotherNode)
                {
                    if (this.tvList.SelectedNode.Parent != null)
                        this.tvList.SelectedNode.Parent.Nodes.Add(node);
                }
                else
                {
                    if (this.tvList.SelectedNode != null)
                        this.tvList.SelectedNode.Nodes.Add(node);
                }
            }
                
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && this.TvNodeAfterSelect != null)
            {
                this.TvNodeAfterSelect(sender, null);
            }
        }

        private void tvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.TvNodeDoubleClick != null)
            {
                this.TvNodeDoubleClick(sender, null);
            }
        }

        private void tvList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && this.TvNodeAfterClick != null)
            {
                this.TvNodeAfterClick(sender, null);
            }
        }

        private void tvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = this.tvList.GetNodeAt(e.X, e.Y);
                if (node != null && this.TvMouseRight != null)                
                {
                    this.TvMouseRight(sender, null);
                }
            }
        }

        public virtual void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (this.ChooseDataEvent != null)
            {
                this.ChooseDataEvent(sv, activeRow);
            }
        }
    }
}
