using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.EPR
{
    /// <summary>
    /// OnlineController <br></br>
    /// [��������: ������д�淶]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <�޸ļ�¼
    /// 
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaseWriteRule : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCaseWriteRule()
        {
            InitializeComponent();
            init();          
        }

        #region ����
        /// <summary>
        /// ����������
        /// </summary>
        //private FS.HISFC.Management.EPR.CaseWriteRule cwrManager = new FS.HISFC.Management.EPR.CaseWriteRule();
        
        /// <summary>
        /// ��ȡ��¼����Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.Employee person = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        /// <summary>
        /// �Ƿ���Ŀ¼���ڹ淶����Ϊ�ղ�������Ϊ�յ������ΪĿ¼��������Ϊ�ǹ淶
        /// </summary>
        //private bool isCatalog = false;
        /// <summary>
        /// ���Ұ����� 
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper;
        /// <summary>
        /// �˵�
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private bool isView = false;
        private string deptCode = string.Empty;
        /// <summary>
        /// �ڵ��б�
        /// </summary>
        private ArrayList treeNodeList = new ArrayList();
        /// <summary>
        /// ��ǰ���ҵ�����
        /// </summary>
        private int currentIndex = 0;
        /// <summary>
        /// �Ƿ��в�ѯ���Ľڵ�
        /// </summary>
        private bool hasNode = false;
        #endregion

        #region ����
        /// <summary>
        /// ���ÿؼ��Ƿ���Ա༭
        /// </summary>
        [Description("���ÿؼ��Ƿ���Ա༭"), Category("����"), DefaultValue(false)] 
        public bool IsView
        {
            get
            {
                return this.isView;
            }
            set
            {
                this.isView = value;
                this.ResetSurface();
                if(value){
                    this.cwc.CatalogTree.AllowDrop = false;
                }
                
            }

        }
        /// <summary>
        /// ����Ҫ�����Ĳ��ű���
        /// </summary>
        [Description("����Ҫ�����Ĳ��ű���"), Category("����"), DefaultValue("")] 
        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
                this.initTree();
            }
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ���˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("ɾ��", "ɾ��", 0, true, false, null);
            toolBar.AddToolButton("���", "���", 1, true, false, null);
            return toolBar;// base.OnInit(sender, neuObject, param);
        }
        /// <summary>
        /// ��ʼ���˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case  "ɾ��":
                    this.DeleteRule();
                    break;
                case "���":
                    this.AddRule_Click(null, null);
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void init()
        {

            ArrayList alDept = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.QueryBaseDepartment();
            this.ncbDept.IsListOnly = true;
            if (alDept != null)
            {
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
                this.ncbDept.AddItems(alDept);
                this.ncbDept.Text = person.Dept.Name;
            }
            if (person.IsManager)
            {
                this.ncbDept.Enabled = true;
                //this.cwc.IsManager = true;
            }
            else
            {
                this.ncbDept.Enabled = false;
                //this.cwc.IsManager = false;
            }

            this.initTree();
            
            this.cwc.CatalogTree.MouseDown += new MouseEventHandler(Catalog_MouseDown);
            this.cwc.CatalogTree.BeforeSelect += new TreeViewCancelEventHandler(CatalogTree_BeforeSelect);
            this.cwc.CatalogTree.AfterSelect += new TreeViewEventHandler(CatalogTree_AfterSelect);
        }
        private void initTree()
        {
            this.cwc.CatalogTree.Nodes.Clear();
            if (string.IsNullOrEmpty(this.deptCode))
            {
                this.cwc.DeptCode = person.Dept.ID;
            }
            else
            {
                this.cwc.DeptCode = this.deptCode;
            }
            this.cwc.init();
        }
        /// <summary>
        /// ������ڵ��б�
        /// </summary>
        /// <param name="node"></param>
        private void FillNodeList(TreeNode node)
        {
            foreach (TreeNode tn in node.Nodes)
            {
                this.treeNodeList.Add(tn);
                if (tn.Nodes.Count > 0)
                {
                    FillNodeList(tn);
                }
            }
        }
        #endregion

        #region ���ݲ��� ����ɾ���ġ���
        /// <summary>
        /// �����д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        private int InsertRule()
        {
            //�Ƿ������
            if (this.isView)
            {
                return 0;
            }
            TreeNode node = this.cwc.CatalogTree.SelectedNode.Parent;
            object cwrParent = (node!=null?node.Tag:null);
            FS.HISFC.Models.EPR.CaseWriteRule cwr = new FS.HISFC.Models.EPR.CaseWriteRule();

            cwr.RuleCode = this.cwc.CatalogTree.SelectedNode.Name;//.Substring(0, this.cwc.CatalogTree.SelectedNode.Name.IndexOf("_"));// this.MakeRuleCode();

            cwr.RuleName = this.ntbName.Text;
            cwr.DeptName = this.ncbDept.Text;
            cwr.DeptCode = this.deptHelper.GetID(this.ncbDept.Text); 
            cwr.Descript = this.ntbDescript.Text;
            cwr.Sort = this.ntbSort.Text;
            cwr.Memo = this.ntbMemo.Text;
            cwr.RuleLink = this.ntbLink.Text;
            cwr.ParentCode = (cwrParent == null ? "0" : ((FS.HISFC.Models.EPR.CaseWriteRule)(cwrParent)).RuleCode);
            cwr.RuleData = this.ntbData.Text;

            int ret = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertRule(cwr);
            if (ret == 1)
            {
                this.cwc.CatalogTree.SelectedNode.Tag = cwr;
                this.cwc.CatalogTree.SelectedNode.Text = cwr.RuleName;
                this.treeNodeList.Clear();
                this.FillNodeList(this.cwc.CatalogTree.Nodes[0]);
            }
            return ret;
        }
        /// <summary>
        /// �޸���д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��</returns>
        private int ModifyRule()
        {
            //�Ƿ������
            if (this.isView)
            {
                return 0;
            }
            TreeNode node = this.cwc.CatalogTree.SelectedNode.Parent;
            object cwrParent = (node != null ? node.Tag : null);
            FS.HISFC.Models.EPR.CaseWriteRule cwr = new FS.HISFC.Models.EPR.CaseWriteRule();

            cwr.RuleCode = this.cwc.CatalogTree.SelectedNode.Name;//.Substring(0, this.cwc.CatalogTree.SelectedNode.Name.IndexOf("_")); //this.MakeRuleCode();

            cwr.RuleName = this.ntbName.Text;
            cwr.DeptName = this.ncbDept.Text;
            cwr.DeptCode = this.deptHelper.GetID(this.ncbDept.Text);
            cwr.Descript = this.ntbDescript.Text;
            cwr.Sort = this.ntbSort.Text;
            cwr.Memo = this.ntbMemo.Text;
            cwr.RuleLink = this.ntbLink.Text;
            cwr.ParentCode = (cwrParent == null ? "0" : ((FS.HISFC.Models.EPR.CaseWriteRule)(cwrParent)).RuleCode);
            cwr.RuleData = this.ntbData.Text;
            int ret = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ModifyRule(cwr);
            if (ret == 1)
            {
                this.cwc.CatalogTree.SelectedNode.Tag = cwr;
                this.cwc.CatalogTree.SelectedNode.Text = cwr.RuleName;
                this.treeNodeList.Clear();
                this.FillNodeList(this.cwc.CatalogTree.Nodes[0]);
            }
            return ret;
        }
        /// <summary>
        /// ɾ����д�淶
        /// </summary>
        /// <returns>1 �ɹ���-1 ʧ��,0 ȡ������</returns>
        private int DeleteRule()
        {
            //�Ƿ������
            if (this.isView)
            {
                return 0;
            }
            TreeNode node = this.cwc.CatalogTree.SelectedNode;
            //if(node == null || node.Name == "0000"){ 
            //    return 0;
            //}

            bool hasGrandchildren = false;
            if (node != null && node.Nodes.Count > 0)
            {
                foreach (TreeNode temp in node.Nodes)
                {
                    if (temp.Nodes.Count > 0)
                    {
                        hasGrandchildren = true;
                    }

                }
            }
            if (hasGrandchildren)
            {
                MessageBox.Show("����ɾ�����ڶ���2���ӽڵ�Ľڵ㣬����ɾ����ײ�ڵ㣡");
                return -1;
            }
            DialogResult dr = MessageBox.Show("�Ƿ�Ҫɾ���ò�����д�淶,\r\n�ò�����ɾ����ӵ�е��ӽڵ�", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                FS.HISFC.Models.EPR.CaseWriteRule cwr = new FS.HISFC.Models.EPR.CaseWriteRule();
                cwr.RuleCode = this.cwc.CatalogTree.SelectedNode.Name;//.Substring(0,this.cwc.CatalogTree.SelectedNode.Name.IndexOf("_")); //((FS.HISFC.Models.EPR.CaseWriteRule)this.cwc.CatalogTree.SelectedNode.Tag).RuleCode;
                cwr.DeptCode = this.deptHelper.GetID(this.ncbDept.Text);
                TreeNode parent = this.cwc.CatalogTree.SelectedNode.Parent;
                this.RemoveChildren(this.cwc.CatalogTree.SelectedNode);
                if (this.cwc.CatalogTree.SelectedNode != null)
                {
                    this.cwc.CatalogTree.SelectedNode.Remove();
                }
                if (parent != null)
                {
                    this.cwc.CatalogTree.SelectedNode = parent;
                }
                if (this.cwc.CatalogTree.Nodes.Count > 0)
                {
                    this.treeNodeList.Clear();
                    this.FillNodeList(this.cwc.CatalogTree.Nodes[0]);
                }
                return FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeleteRule(cwr, true);
                
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// �ݹ�ɾ���ӽڵ�
        /// </summary>
        /// <param name="parentNode"></param>
        private void RemoveChildren(TreeNode parentNode)
        {
            if (parentNode !=null && parentNode.Nodes.Count > 0)
            {
                foreach (TreeNode node in parentNode.Nodes)
                {
                    if (node.Nodes.Count > 0)
                    {
                        RemoveChildren(node);
                    }
                    else
                    {
                        node.Remove();
                    }
                }
            }
        }
        
        /// <summary>
        /// ��ȡ�����淶����
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.EPR.CaseWriteRule GetRuleByID(string id)
        {
            return FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetRule(id);
            
        }

        #endregion

        #region ��Ӧ�¼�
        /// <summary>
        /// �س�ת������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ntbData.Focused)
                {
                    return false;
                }
                if (this.cwc.SearchBox.Focused)
                {
                    if (!string.IsNullOrEmpty(this.cwc.SearchBox.Text))
                    {
                        this.currentIndex = 0;
                        this.hasNode = false;
                        if(this.cwc.CatalogTree.Nodes.Count>0){
                            this.treeNodeList.Clear();
                            this.FillNodeList(this.cwc.CatalogTree.Nodes[0]);
                            //this.treeNodeList = this.cwrManager.QueryCatalogByName(this.cwc.SearchBox.Text);
                            this.LocateRule();
                        }
                        return false;
                    }else{
                        MessageBox.Show("��ѯֵ����Ϊ�գ�");
                        this.cwc.SearchBox.Focus();
                        return false;
                    }
                }
                SendKeys.Send("{TAB}");
                return true;
            }
            if (keyData == Keys.F1)
            {
                this.cwc.SearchBox.Focus();
                return true;
            }
            if (keyData == Keys.F3)
            {
                if (string.IsNullOrEmpty(this.cwc.SearchBox.Text))
                {
                    MessageBox.Show("��ѯֵ����Ϊ�գ�");
                    this.cwc.SearchBox.Focus();
                    return false;
                }
                if (this.currentIndex+1 >= this.treeNodeList.Count)
                {
                    this.currentIndex = 0;
                }
                this.currentIndex++;
                this.LocateRule();
            }
            return base.ProcessDialogKey(keyData);

        }

        /// <summary>
        /// ����Ҽ�����contextMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Catalog_MouseDown(object sender, MouseEventArgs e)
        {
            //�Ƿ������
            if (this.isView)
            {
                return ;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (((TreeView)sender).GetNodeAt(e.Location) == null)
                {
                    ((TreeView)sender).SelectedNode = null;
                }
                this.contextMenuStrip1.ShowCheckMargin = false;
                this.contextMenuStrip1.Show((TreeView)sender, e.Location);
                
            }
        }
        /// <summary>
        /// �ж��Ƿ񱣴�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatalogTree_BeforeSelect(object sender,TreeViewCancelEventArgs e)
        {
            if (this.isChanged())
            {
                DialogResult dr = MessageBox.Show("�Ƿ񱣴��޸ģ�", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.OK)
                {
                    this.Save();
                }
                else
                {
                    TreeNode parentNode = ((TreeView)sender).SelectedNode.Parent;
                    if (((TreeView)sender).SelectedNode != null && ((TreeView)sender).SelectedNode.Tag == null)
                    {
                        ((TreeView)sender).SelectedNode.Remove();
                        if (parentNode != null)
                        {
                            ((TreeView)sender).SelectedNode = parentNode;
                        }
                    }
                    
                }
            }
        }
        
        /// <summary>
        /// ��ҳ�渳ֵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatalogTree_AfterSelect(object sender,TreeViewEventArgs e)
        {
            if (((TreeView)sender).SelectedNode.Tag != null)
            {
                FS.HISFC.Models.EPR.CaseWriteRule cwr = ((TreeView)sender).SelectedNode.Tag as FS.HISFC.Models.EPR.CaseWriteRule;
                this.ntbName.Text = cwr.RuleName;
                this.ncbDept.Text = cwr.DeptName; 
                this.ncbDept.Enabled = false;
                this.ntbSort.Text = cwr.Sort;
                this.ntbDescript.Text = cwr.Descript;
                this.ntbLink.Text = cwr.RuleLink;
                this.ntbData.Text = cwr.RuleData;
                this.ntbMemo.Text = cwr.Memo;
            }
        }

        /// <summary>
        /// ��ӹ淶�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRule_Click(object sender, EventArgs e)
        {
            //this.isCatalog = false;
            //�Ƿ������
            if (this.isView)
            {
                return ;
            }
            TreeNode parentNode = this.cwc.CatalogTree.SelectedNode;
            if (parentNode != null && parentNode.Tag == null)
            {
                MessageBox.Show("���ȱ��浱ǰ��ӵĹ淶��");
                return;
            }
            TreeNode node = new TreeNode();
            node.Name = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetRuleSequence();// +"_" + this.deptHelper.GetID(this.ncbDept.Text); ;
            node.Tag = null;
            node.Text = "�����淶";

            if (parentNode == null)
            {
                this.cwc.CatalogTree.Nodes.Add(node);
            }
            else
            {
                parentNode.Nodes.Add(node);
            }

            this.cwc.CatalogTree.SelectedNode = node;
            if (parentNode != null)
            {
                string deptCode = (parentNode.Tag as FS.HISFC.Models.EPR.CaseWriteRule).DeptCode;
                if (string.IsNullOrEmpty(deptCode))
                {
                    this.ncbDept.Enabled = true;
                }
                else
                {
                    this.ncbDept.Text = this.deptHelper.GetName(deptCode);
                    this.ncbDept.Enabled = false;
                }
            }
            else
            {
                this.ncbDept.Enabled = true;
            }
            ResetSurface();
            this.ntbName.Focus();
        }
        /// <summary>
        /// �������Ʋ��ҹ淶
        /// </summary>
        /// <param name="ruleName"></param>
        private void LocateRule()
        {
            if (this.treeNodeList != null && this.treeNodeList.Count>0)
            {
                for(;this.currentIndex<this.treeNodeList.Count;this.currentIndex++)
                {
                    if (((TreeNode)treeNodeList[this.currentIndex]).Text.IndexOf(this.cwc.SearchBox.Text) != -1)
                    {
                        this.cwc.CatalogTree.SelectedNode = (TreeNode)treeNodeList[this.currentIndex];
                        this.hasNode = true;
                        break;
                    }
                    else
                    {
                        if (this.hasNode && this.currentIndex + 1 >= this.treeNodeList.Count)
                        {
                            this.currentIndex = 0;
                            continue;
                        }
                    }
                }
                if (!this.hasNode)
                {
                    MessageBox.Show("û���ҵ�Ҫ�����Ľڵ㣡");
                }
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nllb_Click(object sender, EventArgs e)
        {
            string link = this.ntbLink.Text;
            if (!string.IsNullOrEmpty(link))
            {
                if (link.ToLower().StartsWith("http://"))
                {
                    System.Diagnostics.Process.Start(link);    
                }
                else
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(link);
                    if (file.Exists)
                    {
                        System.Diagnostics.Process.Start(link);
                    }
                }

            }
        }
        #endregion

        #region ҳ�����
        /// <summary>
        /// ����ҳ��
        /// </summary>
        /// <param name="isCatalog"></param>
        private void ResetSurface()
        {
            //�Ƿ������
            if (this.isView)
            {
                this.ntbName.Enabled = false;
                this.ntbSort.Enabled = false;
                this.ntbDescript.Enabled = false;
                this.ntbMemo.Enabled = false;
                this.ntbLink.Enabled = false;
                this.ntbData.Enabled = false;
                return;
            }
            else
            {
                this.ntbName.Enabled = true;
                this.ntbSort.Enabled = true;
                this.ntbDescript.Enabled = true;
                this.ntbMemo.Enabled = true;
                this.ntbLink.Enabled = true;
                this.ntbData.Enabled = true;
            }
            this.ntbName.Text = string.Empty;
            this.ntbSort.Text = string.Empty;
            this.ntbDescript.Text = string.Empty;
            this.ntbMemo.Text = string.Empty;
            this.ntbLink.Text = string.Empty;
            this.ntbData.Text = string.Empty;
            
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            //�Ƿ������
            if (this.isView)
            {
                return 0;
            }
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        private void Save()
        {
            if (this.isValid())
            {
                if (this.cwc.CatalogTree.SelectedNode.Tag == null)//����
                {
                    if (this.InsertRule() == -1)
                    {
                        MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err + "\r\n" );
                        return;
                    }
                }
                else//�޸�
                {
                    if (this.isChanged())
                    {
                        if (this.ModifyRule() == -1)
                        {
                            MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err + "\r\n" );
                            return;
                        }
                    }
                }
                MessageBox.Show("����ɹ���");
            }
        }
        #endregion

        #region �ж�
        /// <summary>
        /// �ж������Ƿ���Ч
        /// </summary>
        /// <param name="isCatalog"></param>
        /// <returns></returns>
        private bool isValid()
        {
            if (string.IsNullOrEmpty(this.ntbName.Text))
            {
                MessageBox.Show("���Ʋ���Ϊ�գ�");
                return false;
            }
            return true;
        }
        /// <summary>
        /// �ж��Ƿ������ݸ���
        /// </summary>
        /// <returns></returns>
        private bool isChanged()
        {
            //���ڵ�
            TreeNode node = this.cwc.CatalogTree.SelectedNode;
            if (node == null)
            {
                return false;
            }
            //����
            if (this.cwc.CatalogTree.SelectedNode != null && this.cwc.CatalogTree.SelectedNode.Tag == null)
            {
                return true;
            }
            object tag = this.cwc.CatalogTree.SelectedNode.Tag;
            //if (tag == null && this.cwc.CatalogTree.SelectedNode.Name.Substring(0, this.cwc.CatalogTree.SelectedNode.Name.IndexOf("_")) != "0000")
            //{
            //    return true;
            //}
            FS.HISFC.Models.EPR.CaseWriteRule cwr = tag as FS.HISFC.Models.EPR.CaseWriteRule;
            if (this.ntbName.Text != cwr.RuleName
               || (deptHelper.GetID(this.ncbDept.Text) != cwr.DeptCode && !string.IsNullOrEmpty(cwr.DeptCode))//
               || this.ntbSort.Text != cwr.Sort
               || this.ntbDescript.Text != cwr.Descript
               || this.ntbLink.Text != cwr.RuleLink
               || this.ntbData.Text != cwr.RuleData
               || this.ntbMemo.Text != cwr.Memo)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
