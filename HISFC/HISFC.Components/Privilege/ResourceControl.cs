using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Privilege;
using FS.HISFC.BizLogic.Privilege.Service;
using FS.HISFC.BizLogic.Privilege.Model;

//using FS.Framework;


namespace FS.HISFC.Components.Privilege
{
    /// <summary>
    /// [��������: ��Դ����]<br></br>
    /// [������:   �ſ���]<br></br>
    /// [����ʱ��: 2008.6.23]<br></br>
    /// <˵��>
    ///     ��Դ��ʾ�ؼ�
    /// </˵��>
    /// </summary>
    public partial class ResourceControl : UserControl
    {

        #region ˽�б���
        private List<FS.HISFC.BizLogic.Privilege.Model.Resource> treeRes = null;
        List<FS.HISFC.BizLogic.Privilege.Model.Resource> currentResourcesLists = null;

        #region �Զ���Tabel
        DataTable resTabel = new DataTable("ResTable");
        DataColumn[] resColumns =
        {
        new DataColumn("ID"),
        new DataColumn("Name"),
        new DataColumn("ParentID"),
        new DataColumn("Layer"),
        new DataColumn("DllName"),
        new DataColumn("WinName"),
        new DataColumn("ControlType"),
        new DataColumn("ShowType"),
        new DataColumn("Shortcut"),
        new DataColumn("Icon"),
        new DataColumn("Tooltip"),
        new DataColumn("Param"),
        new DataColumn("Enabled"),
        new DataColumn("UserID"),
        new DataColumn("OperDate"),
        new DataColumn("Order"),
        new DataColumn("TreeDllName"),
        new DataColumn("TreeName")
        };
        #endregion
        #endregion

        #region ��������
        public ResourceControl(List<FS.HISFC.BizLogic.Privilege.Model.Resource> _menus)
        {
            resTabel.Columns.AddRange(resColumns);
            treeRes = _menus;
            InitializeComponent();
            this.BackColor = FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.Blue);
            FS.FrameWork.WinForms.Classes.Function.SetFarPointStyle(fpSpread1);
            InitTree();
            //��ʼ��Web��Դ����ʾ�С�
            if (_menus.Count != 0)
            {
                if (_menus[0].ControlType == "WebRes")
                {
                    fpSpread1_Sheet1.Columns[4].Visible = false;
                    fpSpread1_Sheet1.Columns[7].Visible = false;
                    fpSpread1_Sheet1.Columns[8].Visible = false;
                    fpSpread1_Sheet1.Columns[11].Visible = false;
                    fpSpread1_Sheet1.Columns[16].Visible = false;
                    fpSpread1_Sheet1.Columns[17].Visible = false;
                    fpSpread1_Sheet1.ColumnHeader.Columns[5].Label = "URL";
                }
            }
        }

        public void InitTree()
        {
            nTreeView1.Nodes.Clear();
            //���ɸ�����
            foreach (FS.HISFC.BizLogic.Privilege.Model.Resource _menu in treeRes)
            {
                if (_menu.ParentId == "ROOT")//��һ��Ϊ����
                {
                    TreeNode _node = new TreeNode(_menu.Name);
                    _node.Tag = _menu;
                    _node.ImageIndex = 0;
                    _node.SelectedImageIndex = 0;
                    this.nTreeView1.Nodes.Add(_node);
                    foreach (FS.HISFC.BizLogic.Privilege.Model.Resource _menuLevel2 in treeRes)
                    {
                        if (_menuLevel2.ParentId.StartsWith("#") && _menuLevel2.ParentId.Contains(_menu.Id))//�ڶ���Ϊ����
                        {
                            TreeNode node = new TreeNode(_menuLevel2.Name);
                            node.Tag = _menuLevel2;
                            node.ImageIndex = 0;
                            node.SelectedImageIndex = 0;
                            _node.Nodes.Add(node);
                        }
                    }
                }
            }

        }


        public void AddChildType(String typeRes)
        {
            if (this.nTreeView1.SelectedNode == null || this.nTreeView1.SelectedNode.Parent != null)
            {
                return;
            }
            FS.HISFC.BizLogic.Privilege.Model.Resource parent = this.nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource;

            FS.HISFC.BizLogic.Privilege.Model.Resource currentRes = new FS.HISFC.BizLogic.Privilege.Model.Resource();
            currentRes.ParentId = "#" + parent.Id + "";
            currentRes.Type = "Menu";
            currentRes.Enabled = true;
            currentRes.Layer = "1";
            currentRes.UserId = FS.FrameWork.Management.Connection.Operator.ID;
            currentRes.OperDate = FrameWork.Function.NConvert.ToDateTime(new FrameWork.Management.DataBaseManger().GetSysDateTime());
            currentRes.Name = "�·���";
            currentRes.ControlType = typeRes;
            currentRes.Hospital = FS.FrameWork.Management.Connection.Hospital;
            //���������Ϣ
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                FrameWork.Management.PublicTrans.BeginTransaction();
                using (_proxy as IDisposable)
                {
                    currentRes = _proxy.SaveResourcesItem(currentRes);
                }
                FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message);
                return;
            }


            TreeNode _node = new TreeNode(currentRes.Name);
            _node.ImageIndex = 0;
            _node.SelectedImageIndex = 0;
            _node.Tag = currentRes;
            this.nTreeView1.SelectedNode.Nodes.Add(_node);
            this.nTreeView1.SelectedNode = _node;
            AddMenuToList(currentRes);

            _node.BeginEdit();
        }

        public void AddType(String typeRes)
        {

            FS.HISFC.BizLogic.Privilege.Model.Resource currentRes = new FS.HISFC.BizLogic.Privilege.Model.Resource();
            currentRes.ParentId = "ROOT";
            currentRes.Type = "Menu";
            currentRes.Enabled = true;
            currentRes.Layer = "1";
            currentRes.UserId = FS.FrameWork.Management.Connection.Operator.ID;
            currentRes.OperDate =FrameWork.Function.NConvert.ToDateTime( new FrameWork.Management.DataBaseManger().GetSysDateTime());
            currentRes.Name = "�·���";
            currentRes.ControlType = typeRes;
            currentRes.Hospital = FS.FrameWork.Management.Connection.Hospital;
            //���������Ϣ
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                FrameWork.Management.PublicTrans.BeginTransaction();
                using (_proxy as IDisposable)
                {
                    currentRes = _proxy.SaveResourcesItem(currentRes);
                }
                FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message);
                return;
            }


            TreeNode _node = new TreeNode(currentRes.Name);
            _node.ImageIndex = 0;
            _node.SelectedImageIndex = 0;
            _node.Tag = currentRes;
            this.nTreeView1.Nodes.Add(_node);
            this.nTreeView1.SelectedNode = _node;
            AddMenuToList(currentRes);
            _node.BeginEdit();
        }

        public void RemoveType()
        {
            EndTreeEdit();
            TreeNode _node = nTreeView1.SelectedNode;
            if (_node == null)
            {
                MessageBox.Show("��ѡ��Ҫɾ�����࣡");
                return;
            }

            if (MessageBox.Show("�Ƿ�Ҫɾ���÷���?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            if (currentResourcesLists.Count > 0)
            {
                MessageBox.Show("�ķ����������ݣ�����ɾ������");
                return;
            }

            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                FrameWork.Management.PublicTrans.BeginTransaction();
                using (_proxy as IDisposable)
                {
                    _proxy.RemoveResourcesItem((_node.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).Id);
                }
                FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            RemoveResFromList((_node.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).Id);
            nTreeView1.Nodes.Remove(_node);
        }

        public void AddRes()
        {
            if (nTreeView1.SelectedNode == null)
            {
                MessageBox.Show("��ѡ��Ҫѡȡ�ķ��࣡");
                return;
            }

            AddResourceForm _frmAdd = new AddResourceForm(nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource, (nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).ControlType);
            _frmAdd.ShowDialog();
            if (_frmAdd.DialogResult == DialogResult.OK)
            {
                FS.HISFC.BizLogic.Privilege.Model.Resource currentRes = _frmAdd.currentRes;
                if (currentRes != null)
                {
                    AddMenuToList(currentRes);
                }

                nTreeView1_AfterSelect(null, null);
            }

        }

        public void RemoveRes()
        {
            if (fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("�Ƿ�Ҫɾ������Դ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                FrameWork.Management.PublicTrans.BeginTransaction();
                using (_proxy as IDisposable)
                {
                    FS.HISFC.BizLogic.Privilege.Model.Resource res = new FS.HISFC.BizLogic.Privilege.Model.Resource();
                    res.Id = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim();
                    res.Type = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 6].Text.Trim();
                    _proxy.RemoveResourcesItem(res);
                }
                FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }

            RemoveResFromList(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim());
            fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
        }
        #endregion

        #region ˽�з���
        private void AddMenuToList(FS.HISFC.BizLogic.Privilege.Model.Resource currentRes)
        {
            //ɾ����
            int i = RemoveResFromList(currentRes.Id);

            if (i >= 0)
            {
                treeRes.Insert(i, currentRes);
            }
            else
            {
                treeRes.Add(currentRes);
            }
        }

        private int RemoveResFromList(string resourcesId)
        {
            for (int i = 0; i < treeRes.Count; i++)
            {
                FS.HISFC.BizLogic.Privilege.Model.Resource currentRes = treeRes[i];
                if (currentRes.Id == resourcesId)
                {
                    treeRes.Remove(currentRes);
                    return i;
                }
            }

            return -1;
        }

        private void TranslateTabel(List<FS.HISFC.BizLogic.Privilege.Model.Resource> currentResourcesLists)
        {
            foreach (FS.HISFC.BizLogic.Privilege.Model.Resource resourcesItem in currentResourcesLists)
            {
                DataRow newRow = resTabel.NewRow();
                newRow["ID"] = resourcesItem.Id;
                newRow["Name"] = resourcesItem.Name;
                newRow["ParentID"] = resourcesItem.ParentId;
                newRow["Layer"] = resourcesItem.Layer;
                newRow["DllName"] = resourcesItem.DllName;
                newRow["WinName"] = resourcesItem.WinName;
                newRow["ControlType"] = resourcesItem.ControlType;
                newRow["ShowType"] = resourcesItem.ShowType;
                newRow["Shortcut"] = resourcesItem.Shortcut;
                newRow["Icon"] = resourcesItem.Icon;
                newRow["Tooltip"] = resourcesItem.Tooltip;
                newRow["Param"] = resourcesItem.Param;
                newRow["Enabled"] = resourcesItem.Enabled;
                newRow["UserID"] = resourcesItem.UserId;
                newRow["OperDate"] = resourcesItem.OperDate;
                newRow["Order"] = resourcesItem.Order;
                newRow["TreeDllName"] = resourcesItem.TreeDllName;
                newRow["TreeName"] = resourcesItem.TreeName;
                resTabel.Rows.Add(newRow);
            }
        }

        private void TranslateEntity(FS.HISFC.BizLogic.Privilege.Model.Resource resourcesItem)
        {
            resourcesItem.Id = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 0].Text.Trim();
            resourcesItem.Name = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 1].Text.Trim();
            resourcesItem.ParentId = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 2].Text.Trim();
            resourcesItem.Layer = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 3].Text.Trim();
            resourcesItem.DllName = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 4].Text.Trim();
            resourcesItem.WinName = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 5].Text.Trim();
            resourcesItem.ControlType = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 6].Text.Trim();
            resourcesItem.ShowType = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 7].Text.Trim();
            resourcesItem.Shortcut = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 8].Text.Trim();
            resourcesItem.Icon = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 9].Text.Trim();
            resourcesItem.Tooltip = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 10].Text.Trim();
            resourcesItem.Param = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 11].Text.Trim();
            resourcesItem.TreeDllName = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 16].Text.Trim();
            resourcesItem.TreeName = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 17].Text.Trim();
            resourcesItem.UserId = FS.FrameWork.Management.Connection.Operator.ID;
            resourcesItem.OperDate = FrameWork.Function.NConvert.ToDateTime(new FrameWork.Management.DataBaseManger().GetSysDateTime());
            resourcesItem.Enabled = FrameWork.Function.NConvert.ToBoolean(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Text.Trim());
        }

        private void Reload()
        {
            resTabel.Clear();

            if (currentResourcesLists != null)
            {
                TranslateTabel(currentResourcesLists);
            }
            fpSpread1_Sheet1.DataSource = resTabel;
        }

        private void EndTreeEdit()
        {
            foreach (TreeNode currentNode in nTreeView1.Nodes)
            {
                currentNode.EndEdit(true);
            }
        }
        #endregion

        #region �¼�
        private void nTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            currentResourcesLists = new List<FS.HISFC.BizLogic.Privilege.Model.Resource>();
            foreach (FS.HISFC.BizLogic.Privilege.Model.Resource currentRes in treeRes)
            {
                if ((nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).Id == currentRes.ParentId)
                {
                    currentResourcesLists.Add(currentRes);
                }
            }
            Reload();
        }

        private void nTreeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeNode _node = nTreeView1.SelectedNode;
            if (_node == null) return;

            //���Ƿ���,����༭
            if (_node.Level > 1) e.CancelEdit = true;

        }

        private void nTreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {

            //����༭��Ϣ            
            FS.HISFC.BizLogic.Privilege.Model.Resource currentRes = (FS.HISFC.BizLogic.Privilege.Model.Resource)e.Node.Tag;

            if (e.Label == null || e.Label.Trim() == "")
            {
                e.CancelEdit = true;
                return;
            }

            if (!FrameWork.Public.String.ValidMaxLengh(e.Label, 60))
            {
                e.CancelEdit = true;
                MessageBox.Show("��������Ʋ��ܳ���30������!", "��ʾ");
                e.Node.BeginEdit();
                return;
            }

            currentRes.Name = e.Label;
            //���������Ϣ
            try
            {
                PrivilegeService _proxy = Common.Util.CreateProxy();
                using (_proxy as IDisposable)
                {
                    currentRes = _proxy.SaveResourcesItem(currentRes);
                }

                if (currentRes == null) return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            AddMenuToList(currentRes);

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.BizLogic.Privilege.Model.Resource oldResItem = new FS.HISFC.BizLogic.Privilege.Model.Resource();
            TranslateEntity(oldResItem);
            AddResourceForm _frmAdd = new AddResourceForm(oldResItem, (nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).ControlType, "updataRes");
            _frmAdd.ShowDialog();

            if (_frmAdd.DialogResult == DialogResult.OK)
            {
                FS.HISFC.BizLogic.Privilege.Model.Resource currentResources = _frmAdd.currentRes;

                if (currentResources != null)
                {
                    AddMenuToList(currentResources);
                }
                nTreeView1_AfterSelect(null, null);
            }

        }

        private void PrivilegeResourceControl_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void AddTypeItem_Click(object sender, EventArgs e)
        {
           // AddType((nTreeView1.SelectedNode.Tag as FS.HISFC.BizLogic.Privilege.Model.Resource).ControlType.Trim());
           // AddType();
        }

        private void RemoveTypeItem_Click(object sender, EventArgs e)
        {
            RemoveType();
        }

        private void AddResItem_Click(object sender, EventArgs e)
        {
            AddRes();
        }

        private void ModifyResItem_Click(object sender, EventArgs e)
        {
            fpSpread1_CellDoubleClick(null, null);
        }

        private void RemoveResItem_Click(object sender, EventArgs e)
        {
            RemoveRes();
        }

        private void mnuTest_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRowIndex < 0) return;

            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            
            FS.HISFC.Models.Admin.SysMenu obj = new FS.HISFC.Models.Admin.SysMenu();
            obj.ModelFuntion.DllName = this.fpSpread1_Sheet1.Cells[row, 4].Text;
            obj.ModelFuntion.WinName = this.fpSpread1_Sheet1.Cells[row, 5].Text;
            obj.MenuParm = this.fpSpread1_Sheet1.Cells[row, 11].Text;
            obj.MenuName = this.fpSpread1_Sheet1.Cells[row, 1].Text;
            #region {CCC3E877-ADB8-43e5-80B5-53FDEE94C47E}
            obj.ModelFuntion.FormShowType = this.fpSpread1_Sheet1.Cells[row, 7].Text; 
            #endregion
            obj.ModelFuntion.TreeControl.WinName = this.fpSpread1_Sheet1.Cells[row, 17].Text;
            obj.ModelFuntion.TreeControl.DllName = this.fpSpread1_Sheet1.Cells[row, 16].Text;
            //obj.ModelFuntion.TreeControl.Param = this.fpSpread1_Sheet1.Cells[row, 5].Text;
            obj.MenuWin = this.fpSpread1_Sheet1.Cells[row, 0].Text;
            Function.ShowForm(obj);

        }

        #endregion

      
    }
}
