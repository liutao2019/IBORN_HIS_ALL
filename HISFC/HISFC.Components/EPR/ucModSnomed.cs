using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
    public partial class ucModSnomed : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ucModSnomed<br></br>
        /// [��������: snomed�༭]<br></br>
        /// [�� �� ��: ���]<br></br>
        /// [����ʱ��: 2007-09-18]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucModSnomed()
        {
            InitializeComponent();
        }

        #region ������������
        private FS.HISFC.Models.EPR.SNOMED snomed = new FS.HISFC.Models.EPR.SNOMED();
        //FS.HISFC.Management.Manager.Spell mySpell = new FS.HISFC.Management.Manager.Spell();
        DataSet ds;
        DataTable dataTable;

        #endregion

        /// <summary>
        /// toolBarService
        /// </summary>

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            return null;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Search(this.neuTreeView, this.txtSearch.Text);
        }

        #region ����
        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���....");
            Application.DoEvents();
            FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

            bool save = true;
            bool insert = false;
            bool del = false;
            bool update = false;
            string strinsert = "";
            string strupdate = "";
            //����RowStateΪAdded��������
            foreach (DataRow dr in ds.Tables["cnp_com_snopmed"].Select("", "", DataViewRowState.Added))
            {
                FS.HISFC.Models.EPR.SNOMED snomedObject = new FS.HISFC.Models.EPR.SNOMED();

                snomedObject.ID = dr["ID"].ToString();
                snomedObject.Name = dr["NAME"].ToString();
                snomedObject.SNOPCode = dr["SNOPCODE"].ToString();
                snomedObject.EnglishName = dr["ENGLISHNAME"].ToString();
                snomedObject.DiagnoseCode = dr["DIAGNOSECODE"].ToString();
                snomedObject.ParentCode = dr["PARENTCODE"].ToString();
                snomedObject.Memo = dr["MEMO"].ToString();
                snomedObject.SpellCode = dr["SPELLCODE"].ToString();
                snomedObject.WBCode = dr["WBCODE"].ToString();
                snomedObject.UserCode = dr["USERCODE"].ToString();
                snomedObject.SortID = FS.FrameWork.Function.NConvert.ToInt32(dr["SORTID"].ToString());

                //����ҵ�����ӷ���
                if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertSNOMED(snomedObject) == -1)
                {
                    FS.HISFC.BizProcess.Factory.Function.RollBack();
                    strinsert = dr["id"].ToString();
                    strinsert = strinsert + "\n";
                    insert = true;
                    save = false;
                }

            }
            if (insert)
            {
                MessageBox.Show("���ʧ�ܣ�\n" + strinsert);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            //����RowStateΪDeleted��������
            foreach (DataRow dr in ds.Tables["cnp_com_snopmed"].Select("", "", DataViewRowState.Deleted))
            {
                string id = dr[0, DataRowVersion.Original].ToString();

                //����ҵ���ɾ������
                if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DelSNOPMEDByCode(id) == -1)
                {
                    FS.HISFC.BizProcess.Factory.Function.RollBack();
                    del = true;
                    save = false;
                }

            }
            if (del)
            {
                MessageBox.Show("ɾ��ʧ�ܣ�");

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }

            //����RowStateΪModified��������
            foreach (DataRow dr in ds.Tables["cnp_com_snopmed"].Select("", "", DataViewRowState.ModifiedCurrent))
            {
                FS.HISFC.Models.EPR.SNOMED snomedObject = new FS.HISFC.Models.EPR.SNOMED();

                snomedObject.ID = dr["ID"].ToString();
                snomedObject.Name = dr["NAME"].ToString();
                snomedObject.SNOPCode = dr["SNOPCODE"].ToString();
                snomedObject.EnglishName = dr["ENGLISHNAME"].ToString();
                snomedObject.DiagnoseCode = dr["DIAGNOSECODE"].ToString();
                snomedObject.ParentCode = dr["PARENTCODE"].ToString();
                snomedObject.Memo = dr["MEMO"].ToString();
                snomedObject.SpellCode = dr["SPELLCODE"].ToString();
                snomedObject.WBCode = dr["WBCODE"].ToString();
                snomedObject.UserCode = dr["USERCODE"].ToString();
                snomedObject.SortID = FS.FrameWork.Function.NConvert.ToInt32(dr["SORTID"].ToString());

                //����ҵ����޸ķ���
                if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateSNOMED(snomedObject) == -1)
                {

                    FS.HISFC.BizProcess.Factory.Function.RollBack();
                    strupdate = dr["id"].ToString();
                    strupdate = strupdate + "\n";
                    update = true;
                    save = false;
                }
            }
            if (update)
            {
                MessageBox.Show("�޸�ʧ��\n" + strupdate);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if (save)
            {
                ds.Tables["cnp_com_snopmed"].AcceptChanges();
                FS.HISFC.BizProcess.Factory.Function.Commit();
                MessageBox.Show("����ɹ���");
                return 0;
            }
            else
                return -1;
        }
        #endregion


        #region �˳�
        public override int Exit(object sender, object neuObject)
        {
            if (ds.HasChanges())
            {
                if (MessageBox.Show("��¼�Ѹ��ģ���Ҫ����ô��", "������ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        return OnSave(sender, neuObject);
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
            }
            return 0;
        }
        #endregion


        #region ��ʼ��DataTable
        System.Collections.ArrayList allData = new System.Collections.ArrayList();
        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        public void initialTable()
        {
            dataTable = new DataTable("cnp_com_snopmed");

            DataColumn dc1 = new DataColumn("ID");//����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn("NAME");//����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn("SNOPCODE");//SNOMED����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc3);

            DataColumn dc4 = new DataColumn("ENGLISHNAME");//Ӣ������
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc4);

            DataColumn dc5 = new DataColumn("DIAGNOSECODE");//��ϱ���
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc5);

            DataColumn dc6 = new DataColumn("PARENTCODE");//��������
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc6);

            DataColumn dc7 = new DataColumn("MEMO");//��ע
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc7);

            DataColumn dc8 = new DataColumn("SPELLCODE");//ƴ����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc8);

            DataColumn dc9 = new DataColumn("WBCODE");//�����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc9);

            DataColumn dc10 = new DataColumn("USERCODE");//�Զ�����
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc10);

            DataColumn dc11 = new DataColumn("SORTID");//����˳��
            dc1.DataType = typeof(System.String);
            dataTable.Columns.Add(dc11);

            //����DataTable����
            //dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["ID"] };

            allData  = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSNOMED();

            //foreach (FS.HISFC.Models.EPR.SNOMED s in allData)
            //{
            //    dataTable.Rows.Add(
            //        new object[]{
            //        s.ID,
            //        s.Name,
            //        s.SNOPCode,
            //        s.EnglishName,
            //        s.DiagnoseCode,
            //        s.ParentCode,
            //        s.Memo,
            //        s.SpellCode,
            //        s.WBCode,
            //        s.UserCode,
            //        s.SortID});

            //}

        }
        #endregion

        #region ��ʼ��TreeView
        public void initialTree(System.Windows.Forms.TreeView tv, System.Data.DataSet ds)
        {
            tv.BeginUpdate();
            tv.Nodes.Clear();
            System.Windows.Forms.TreeNode tn = null;
            if (tv.Nodes.Count == 0)
            {
                tn = new System.Windows.Forms.TreeNode("SNOMED", 4, 4);
                tv.Nodes.Add(tn);
            }
            else
            {
                tn = tv.Nodes[0];
            }

            //����ӽڵ�
            foreach (DataRow dr in ds.Tables["cnp_com_snopmed"].Rows)
            {

                TreeNode treeModual = null;
                if (string.Compare(dr["PARENTCODE"].ToString(), "root") == 0)
                {
                    foreach (TreeNode treeNode in tn.Nodes) //���׽��
                    {
                        break;

                    }
                    if (treeModual == null)
                    {
                        treeModual = new TreeNode(dr["ID"].ToString(), 0, 1);
                        treeModual.Tag = dr["ID"].ToString();
                        tn.Nodes.Add(treeModual);
                        //������ӽڵ�
                        foreach (DataRow dt in ds.Tables["cnp_com_snopmed"].Rows)
                        {

                            TreeNode treeType = null;
                            if (string.Compare(dt["PARENTCODE"].ToString(), treeModual.Text) == 0)
                            {
                                foreach (TreeNode treeNodeType in treeModual.Nodes)
                                {
                                    if (treeNodeType.Text == dr["ID"].ToString())
                                    {
                                        treeType = treeNodeType;
                                        break;
                                    }
                                }
                                if (treeType == null)
                                {
                                    treeType = new TreeNode(dr["ID"].ToString(), 0, 1);
                                    treeType.Tag = dt["ID"].ToString();
                                    treeType.Text = dt["NAME"].ToString();
                                    treeModual.Nodes.Add(treeType);
                                }
                            }
                        }
                        treeModual.Text = dr["NAME"].ToString();

                    }
                }

            }

            tv.EndUpdate();
        }
        private void CreateTree(System.Windows.Forms.TreeView tv, System.Data.DataSet ds)
        {
            tv.BeginUpdate();
            tv.Nodes.Clear();
            TreeNode root = new TreeNode("Ԫ��");
            root.Tag = "ROOT";
            tv.Nodes.Add(root);
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            System.Collections.ArrayList alRoot = new System.Collections.ArrayList();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڶ�ȡ...");
            int i = 0;
            //foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i++, ds.Tables[0].Rows.Count);
            //    Application.DoEvents();
              

            //    if (dr["ISLEAF"].ToString()=="1";
            //    {
            //        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            //        obj.ID = dr["ID"].ToString();
            //        obj.Name = dr["NAME"].ToString();
            //        obj.Memo = dr["PARENTCODE"].ToString();
            //        obj.User03 = obj.Memo;
            //        al.Add(obj);
            //    }
               
            //}
            foreach(FS.HISFC.Models.EPR.SNOMED obj in allData)
            {
                if (obj.User01 == "1")
                    al.Add(obj.Clone());
            }

            i = 0;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("��������...");
           
            foreach (FS.HISFC.Models.EPR.SNOMED obj in al)
            {
                if (obj.SNOPCode.IndexOf('\\') > 0 )
                {
                    obj.User01 = obj.SNOPCode;
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i++, al.Count);
                    Application.DoEvents();
                    obj.User01 = obj.Name;
                    obj.User03 = obj.ParentCode;
                    this.setFullPath(obj);
                    obj.SNOPCode = obj.User01;
                    FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UpdateSNOMED(obj);
                }
            }
            i = 0;
            //�������Ҷ�ӽ�㣬ѭ����ӵ�����
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("�������...");
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i++, al.Count);
                Application.DoEvents();
                string[] fullpath = obj.User01.ToString().Split('\\');
                string parpath = "Ԫ��";
                TreeNode nodeParent = tv.Nodes[0];
                foreach (string path in fullpath)
                {
                    parpath += "\\" + path;
                    TreeNode node = null;
                    GetNodeFromFullPath(parpath, nodeParent, ref node);
                    if (node == null)
                    {
                        node = nodeParent.Nodes.Add(path);
                    }
                    nodeParent = node;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            tv.EndUpdate();

        }
        /// <summary>
        /// ��Ҷ�ӵ�·����ӽ���
        /// </summary>
        /// <param name="obj"></param>
        private void setFullPath(FS.FrameWork.Models.NeuObject obj)
        {

            foreach (FS.HISFC.Models.EPR.SNOMED parentDr in allData)
             {
                 if (obj.User03 == parentDr.ID && parentDr.User01 =="0") //֦�ſ���
                 {
                     obj.User01 = parentDr.Name + "\\" + obj.User01;

                     if (parentDr.ID != "ROOT" &&
                         parentDr.ID != parentDr.ParentCode)
                     {
                         obj.User03 = parentDr.ParentCode;
                         setFullPath(obj);
                     }
                 }
             }
        }

        private void GetNodeFromFullPath(string tag, TreeNode tv, ref TreeNode rtnNode)
        {
            if (tv.FullPath == tag)
            {
                rtnNode = tv;
                return;
            }

            foreach (TreeNode node in tv.Nodes)
            {
                if (node.FullPath == tag)
                {
                    rtnNode = node;
                    return;
                }
                if (tag.IndexOf(node.FullPath) > 0)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        GetNodeFromFullPath(tag, childNode, ref rtnNode);
                    }
                }
            }
        }
      
        #endregion

        #region TreeView MouseDown�¼�
        /// <summary>
        /// TreeView MouseDown�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                System.Drawing.Point point = new Point(e.X, e.Y);
                TreeNode tn = this.neuTreeView.GetNodeAt(point);
                if (tn != null)
                    this.neuTreeView.SelectedNode = tn;

            }
        }
        #endregion

        #region Load
        private void Form1_Load(object sender, EventArgs e)
        {
            ds = new DataSet();
            initialTable();
            ds.Tables.Add(dataTable);
            ds.Tables["cnp_com_snopmed"].AcceptChanges();//���Ĺ���
            //initialTree(this.neuTreeView, ds);
            CreateTree(this.neuTreeView, ds);
            this.neuTreeView.ContextMenu = this.neuContexMenu1;

        }
        #endregion

        #region ContextMenu�˵���ʾ
        /// <summary>
        /// ContextMenu�˵���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuContexMenu1_Popup(object sender, EventArgs e)
        {
            int selectNodeLength = this.neuTreeView.SelectedNode.FullPath.Length - this.neuTreeView.SelectedNode.FullPath.Replace("\\", "").Length;
            switch (selectNodeLength)
            {
                case 0:
                    this.AddChildmenuItem.Enabled = true;
                    this.DelmenuItem.Enabled = false;
                    this.MovemenuItem.Enabled = false;
                    this.ExtendmenuItem.Enabled = true;
                    break;
                case 1:
                    this.AddChildmenuItem.Enabled = true;
                    this.DelmenuItem.Enabled = true;
                    this.MovemenuItem.Enabled = false;
                    this.ExtendmenuItem.Enabled = true;
                    break;
                case 2:
                    this.AddChildmenuItem.Enabled = false;
                    this.DelmenuItem.Enabled = true;
                    this.MovemenuItem.Enabled = true;
                    this.ExtendmenuItem.Enabled = false;
                    break;
                default:
                    this.AddChildmenuItem.Enabled = false;
                    this.DelmenuItem.Enabled = true;
                    this.MovemenuItem.Enabled = true;
                    this.ExtendmenuItem.Enabled = false;
                    break;

            }
        }
        #endregion

        #region ContextMenu ȫ��չ��/�۵��¼�
        /// <summary>
        /// ȫ��չ��/�۵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtendmenuItem_Click(object sender, EventArgs e)
        {
            if (this.neuTreeView.SelectedNode.IsExpanded)
            {
                this.neuTreeView.SelectedNode.Collapse();
            }
            else
            {
                this.neuTreeView.SelectedNode.Expand();
            }
        }
        #endregion

        #region ContextMenu����ӽڵ�
        // <summary>
        // ContextMenu����ӽڵ�
        // </summary>
        // <param name="sender"></param>
        // <param name="e"></param>
        int ModualCount = 1;//�½��ӽڵ����
        int TypeCount = 1;//�½����ӽڵ����
        private void AddChildmenuItem_Click(object sender, EventArgs e)
        {
            string[] strPath = this.neuTreeView.SelectedNode.FullPath.Split('\\');
            int PathLength = strPath.Length;

            switch (PathLength)
            {
                case 1:
                    //�õ��ӽڵ�
                    string code1 = ModualCount.ToString();
                    code1 = "U" + code1.PadLeft(5, '0');
                    while (RearchIDName(code1) == -1)
                    {
                        ModualCount += 1;
                        code1 = ModualCount.ToString();
                        code1 = "U" + code1.PadLeft(5, '0');
                    }
                    TreeNode treeNodeModual = new TreeNode("�ӽڵ�" + ModualCount.ToString(), 0, 1);
                    treeNodeModual.Tag = code1;
                    this.neuTreeView.SelectedNode.Nodes.Add(treeNodeModual);
                    DataRow drNew1 = ds.Tables["cnp_com_snopmed"].NewRow();
                    drNew1["ID"] = code1;
                    drNew1["PARENTCODE"] = "ROOT";
                    drNew1["NAME"] = "�ӽڵ�" + ModualCount.ToString();
                    this.ds.Tables["cnp_com_snopmed"].Rows.Add(drNew1);
                    this.neuTreeView.SelectedNode = treeNodeModual;
                    ModualCount += 1;
                    break;
                case 2:
                    //�õ����ӽڵ�
                    string code2 = TypeCount.ToString();
                    TreeNode tr = this.neuTreeView.SelectedNode;
                    code2 = tr.Tag.ToString() + "U" + code2.PadLeft(5, '0');
                    while (RearchIDName(code2) == -1)
                    {
                        TypeCount += 1;
                        code2 = TypeCount.ToString();
                        code2 = tr.Tag.ToString() + "U" + code2.PadLeft(5, '0');
                    }
                    TreeNode treeNodeId = new TreeNode("���ӽڵ�" + TypeCount.ToString(), 2, 3);
                    treeNodeId.Tag = code2;
                    this.neuTreeView.SelectedNode.Nodes.Add(treeNodeId);
                    DataRow drNew2 = ds.Tables["cnp_com_snopmed"].NewRow();
                    drNew2["ID"] = code2;
                    drNew2["PARENTCODE"] = tr.Tag.ToString();
                    drNew2["NAME"] = "���ӽڵ�" + TypeCount.ToString();
                    this.ds.Tables["cnp_com_snopmed"].Rows.Add(drNew2);
                    this.neuTreeView.SelectedNode = treeNodeId;
                    TypeCount += 1;
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region ContextMenu ɾ���ڵ�
        /// <summary>
        /// ContextMenuɾ���ڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelmenuItem_Click(object sender, EventArgs e)
        {
            string[] strPath = this.neuTreeView.SelectedNode.FullPath.Split('\\');
            int pathLength = strPath.Length;
            string fullPath = "";
            //ɾ��DataRow������
            switch (pathLength)
            {
                case 1: break;
                case 2:
                    fullPath = "PARENTCODE='" + this.neuTreeView.SelectedNode.Tag.ToString() + "'or ID='" + this.neuTreeView.SelectedNode.Tag.ToString() + "'";
                    break;
                case 3:
                    fullPath = "ID='" + this.neuTreeView.SelectedNode.Tag.ToString() + "'";
                    break;
                default:
                    break;

            }
            DataRow[] dr = ds.Tables["cnp_com_snopmed"].Select(fullPath);
            //ɾ����
            foreach (DataRow dataRow in dr)
            {
                dataRow.Delete();
            }
            //ɾ�����
            this.neuTreeView.SelectedNode.Remove();

        }
        #endregion

        #region ContextMenu�ƶ��ڵ��Ӳ˵�
        /// <summary>
        /// ContextMenu�ƶ��ڵ��Ӳ˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovemenuItem_Popup(object sender, EventArgs e)
        {
            this.MovemenuItem.MenuItems.Clear();
            string[] strPath = this.neuTreeView.SelectedNode.FullPath.Split('\\');
            int pathLength = strPath.Length;
            switch (pathLength)
            {
                case 3:
                    foreach (TreeNode modualNode in this.neuTreeView.Nodes[0].Nodes)
                    {
                        if (modualNode != this.neuTreeView.SelectedNode.Parent)
                        {
                            MenuItem menuItem = this.MovemenuItem.MenuItems.Add(modualNode.Text);
                            menuItem.Tag = modualNode.Tag;
                            menuItem.Click += new EventHandler(mItem_Click);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region �ƶ����ӽڵ��¼�
        /// <summary>
        /// �ƶ����ӽڵ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string[] strPath = this.neuTreeView.SelectedNode.FullPath.Split('\\');
            int PathCount = strPath.Length;
            //�޸�DataTable��MODUAl
            string fullPath = "ID='" + this.neuTreeView.SelectedNode.Tag.ToString() + "'";
            DataRow[] dr = ds.Tables["cnp_com_snopmed"].Select(fullPath);
            foreach (DataRow d in dr)
            {
                d["PARENTCODE"] = menuItem.Tag.ToString();
            }
            //�ƶ��ڵ�
            foreach (TreeNode treeNode in this.neuTreeView.Nodes[0].Nodes)
            {
                if (treeNode.Text == menuItem.Text)
                {
                    TreeNode cloneTreeNode = (TreeNode)this.neuTreeView.SelectedNode.Clone();
                    this.neuTreeView.SelectedNode.Remove();
                    treeNode.Nodes.Add(cloneTreeNode);
                    this.neuTreeView.SelectedNode = treeNode;
                    break;
                }
            }

        }
        #endregion

        private void MovemenuItem_Click(object sender, EventArgs e)
        {
            MovemenuItem_Popup(sender, e);
        }

        #region ����ڵ�����¼�
        private void neuTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int pathCount = this.neuTreeView.SelectedNode.FullPath.Length - this.neuTreeView.SelectedNode.FullPath.Replace("\\", "").Length;
            if (pathCount != 0)
            {
                foreach (FS.HISFC.Models.EPR.SNOMED obj in allData)
                {
                    if((obj.SNOPCode !="" && obj.SNOPCode == e.Node.FullPath) || obj.Name == e.Node.Text)
                    {
                        this.txtCode.Text = obj.ID;
                        this.txtName.Text = obj.Name;
                        this.txtScode.Text = obj.SNOPCode;
                        this.txtEname.Text = obj.EnglishName;
                        this.comboBox.Text = obj.DiagnoseCode;
                        this.txtPcode.Text = obj.ParentCode;
                        this.txtMemo.Text = obj.Memo;
                        this.txtSpell.Text = obj.SpellCode;
                        this.txtWB.Text = obj.WBCode;
                        this.txtUcode.Text = obj.UserCode;
                        this.neuNumericTextBox.Text = obj.SortID.ToString();
                        break;
                    }
                }
            }
            else
            {
                this.txtCode.Text = "";
                this.txtEname.Text = "";
                this.txtMemo.Text = "";
                this.txtName.Text = "";
                this.txtPcode.Text = "";
                this.txtScode.Text = "";
                this.txtSpell.Text = "";
                this.txtUcode.Text = "";
                this.txtWB.Text = "";
                this.comboBox.Text = "";
                this.neuNumericTextBox.Text = "";
            }

            this.txtMemo.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtScode.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtEname.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtName.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtSpell.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtUcode.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.txtWB.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.comboBox.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);
            this.neuNumericTextBox.TextChanged += new EventHandler(this.txtneuTInput_TextChanged);

        }
        #endregion

        #region ����ÿ��textBox����DataTable
        private void txtneuTInput_TextChanged(object sender, EventArgs e)
        {
            ModifiedDataTable();
        }
        #region ��������ʱ������DataTable
        /// <summary>
        /// ��������ʱ������DataTable
        /// </summary>
        void ModifiedDataTable()
        {
            DataRow[] dr = ds.Tables["cnp_com_snopmed"].Select("ID='" + this.txtCode.Text + "'");
            if (dr.Length != 0)
            {
                foreach (DataRow d in dr)
                {
                    d["ID"] = this.txtCode.Text;
                    d["ENGLISHNAME"] = this.txtEname.Text;
                    d["MEMO"] = this.txtMemo.Text;
                    d["NAME"] = this.txtName.Text;
                    this.neuTreeView.SelectedNode.Text = this.txtName.Text;
                    d["PARENTCODE"] = this.txtPcode.Text;
                    d["SNOPCODE"] = this.txtScode.Text;
                    if (this.txtSpell.Text == "" || this.txtWB.Text == "")
                    {
                        //������������ƴ����������
                        FS.HISFC.Models.Base.Spell spell = new FS.HISFC.Models.Base.Spell();

                        spell = (FS.HISFC.Models.Base.Spell)FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetSpell(this.txtName.Text.Trim());
                        this.txtSpell.Text = spell.SpellCode;
                        this.txtWB.Text = spell.WBCode;
                    }
                    d["SPELLCODE"] = this.txtSpell.Text;
                    d["USERCODE"] = this.txtUcode.Text;
                    d["WBCODE"] = this.txtWB.Text;
                    d["DIAGNOSECODE"] = this.comboBox.Text;
                    d["SORTID"] = this.neuNumericTextBox.Text;
                }

            }
        }
        #endregion


        #endregion

        #region TreeView_BeforeSelect��������ʱ����
        private void neuTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            this.txtMemo.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtScode.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtEname.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtName.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtSpell.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtUcode.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.txtWB.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.comboBox.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);
            this.neuNumericTextBox.TextChanged -= new EventHandler(this.txtneuTInput_TextChanged);


        }
        #endregion

        #region ���ҵ�ǰ�����IDֵ�Ƿ�������
        /// <summary>
        /// ���ҵ�ǰ�����IDֵ�Ƿ�������
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="compareString"></param>
        /// <returns></returns>
        private int RearchIDName(string compareString)
        {
            foreach (DataRow dr in ds.Tables["cnp_com_snopmed"].Select("", "", DataViewRowState.Unchanged))
            {
                if (dr["ID"].ToString().ToLower() == compareString.ToLower())
                    return -1;
            }
            return 0;
        }
        #endregion

        #region �����ı��س��¼�
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtSearch.Text != "")
                    Search(this.neuTreeView, this.txtSearch.Text);
            }
        }
        #endregion

        #region ������ť�¼�

        private void neuBSearch_Click(object sender, EventArgs e)
        {
            if (this.txtSearch.Text != "")
                Search(this.neuTreeView, this.txtSearch.Text);
        }
        #endregion

        #region ��������
        /// <summary>
        /// �������� 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="RearchText"></param>
        /// <returns></returns>
        public int Search(TreeView treeView, string RearchText)
        {
            if (treeView.Nodes.Count == 0) return -1;
            TreeNode currentNode = null;
            currentNode = treeView.Nodes[0];
            currentNode = currentNode.Nodes[0];
            TreeNode nodeModual = currentNode;
            while (true)
            {
                if (nodeModual == null) break;
                if (nodeModual.Text.ToLower().IndexOf(RearchText.ToLower()) != -1)
                {
                    treeView.SelectedNode = nodeModual;
                    return nodeModual.Index;
                }
                else
                {
                    foreach (TreeNode nodeType in nodeModual.Nodes)
                    {
                        if (nodeType.Text.ToLower().IndexOf(RearchText.ToLower()) != -1)
                        {
                            treeView.SelectedNode = nodeType;
                            return nodeType.Index;
                        }
                    }
                    nodeModual = nodeModual.NextNode;
                }

            }
            MessageBox.Show("û���ҵ�ƥ����");
            return -1;
        }
        #endregion


    }
}
