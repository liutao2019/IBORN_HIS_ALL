using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.Pharmacy
{
    /// <summary>
    /// tvCustomQuery<br></br>
    /// [��������: tvCustomQuery�Զ����ѯ]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-9-12]<br></br>
    /// <�޸ļ�¼ {85997F7C-0E19-46e8-B552-2A60009747B4}
    ///		�޸���='����' 
    ///		�޸�ʱ��='2010-05-18' 
    ///		�޸�Ŀ��='��ֲ����5.0'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class tvCustomQuery : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public tvCustomQuery()
        {
            this.Init();
            this.miEdit.Click += new EventHandler(miEdit_Click);
            this.miNewType.Click += new EventHandler(miNewType_Click);
            this.miNewDefine.Click += new EventHandler(miNewDefine_Click);
            this.miDeleteDefine.Click += new EventHandler(miDeleteDefine_Click);
        }

        public delegate void DeleteDefine();
        public event DeleteDefine DeleteDefineHandler;

        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            string type = "All";
            if (this.Tag != null && string.IsNullOrEmpty(this.Tag.ToString()))
            {
                type = this.Tag.ToString();
            }
            string deptCode = ((FS.HISFC.Models.Base.Employee)this.dbMgr.Operator).Dept.ID;
            //��ȡ���п�����ͼ
            List<TreeNode> lsParentNodes = this.getObject("Local.CustomQuery.GetViewInfo", this.dbMgr.Operator.ID, type);

            this.ImageList = this.deptImageList;
            //���Զ����ѯ������ͼ������ʾ
            foreach (TreeNode ParentNode in lsParentNodes)
            {
                ParentNode.ImageIndex = 0;
                ParentNode.SelectedImageIndex = 1;

                this.Nodes.Add(ParentNode);

                FS.FrameWork.Models.NeuObject obj = ParentNode.Tag as FS.FrameWork.Models.NeuObject;

                List<TreeNode> lsNode = this.getObject("Local.CustomQuery.GetDefineInfo", obj.ID, dbMgr.Operator.ID, deptCode);

                TreeNode typeNode = new TreeNode();
                typeNode.Tag = obj.Clone();
                string customType = obj.Name;
                foreach (TreeNode node in lsNode)
                {
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 5;

                    FS.FrameWork.Models.NeuObject o = node.Tag as FS.FrameWork.Models.NeuObject;

                    //��Ԥ�������ͼ����[˵��]��ͬʱ��˵���û������˹���
                    if (o.Name != obj.Name)
                    {

                        //�û���������ж������㰴�����������ֻҪ��������ͬ�ľ�����һ������
                        if (o.Name != customType)
                        {
                            typeNode = new TreeNode();
                            customType = o.Name;

                            FS.FrameWork.Models.NeuObject typeObject = obj.Clone();
                            typeObject.Name = customType;//���͸�ֵ
                            typeNode.Tag = typeObject;

                            typeNode.Text = o.Name;
                            typeNode.ImageIndex = 2;
                            typeNode.SelectedImageIndex = 3;
                            ParentNode.Nodes.Add(typeNode);
                        }
                        typeNode.Nodes.Add(node);
                    }
                    else
                    {
                        ParentNode.Nodes.Add(node);
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="param">����</param>
        /// <returns>���������</returns>
        private List<TreeNode> getObject(string sqlIndex, params string[] param)
        {
            List<TreeNode> ls = new List<TreeNode>();
            string SQL = "";

            if (dbMgr.Sql.GetSql(sqlIndex, ref SQL) == -1)
            {
                this.ShowMessageBox("û���ҵ�SQL��" + sqlIndex, "����");
            }
            SQL = string.Format(SQL, param);
            if (dbMgr.ExecQuery(SQL) == -1)
            {
                this.ShowMessageBox("ִ��SQL��������" + dbMgr.Err, "����");
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    TreeNode node = new TreeNode();
                    obj.ID = dbMgr.Reader[0].ToString();//��ͼ����
                    obj.Name = dbMgr.Reader[1].ToString();//�Զ������� ��ͼ˵��
                    obj.Memo = dbMgr.Reader[2].ToString();//��ע
                    obj.User01 = dbMgr.Reader[3].ToString();//ǿ������ SQL����
                    obj.User02 = dbMgr.Reader[4].ToString();//����Ȩ��
                    //sql ������û�����,��ӽ����ȡ
                    //�����Ԥ����Ϊ��
                    obj.User03 = dbMgr.Reader[5].ToString();

                    node.Tag = obj;
                    node.Text = string.IsNullOrEmpty(obj.Memo) ? obj.Name : obj.Memo;
                    //node.ImageIndex = 3;
                    //node.SelectedImageIndex = 4;

                    ls.Add(node);

                }
                dbMgr.Reader.Close();
            }
            catch (Exception ex)
            {
                this.ShowMessageBox("ִ��SQL��������" + ex.Message, "����");
            }
            return ls;
        }

        /// <summary>
        /// ��ʾMessageBox
        /// </summary>
        /// <param name="text">����</param>
        /// <param name="caption">����</param>
        private void ShowMessageBox(string text, string caption)
        {
            MessageBox.Show(FS.FrameWork.Management.Language.Msg(text), FS.FrameWork.Management.Language.Msg(caption));
        }

        #region ����Ҽ�
        MenuItem miEdit = new MenuItem("�༭");
        MenuItem miNewType = new MenuItem("�½��ļ���");
        MenuItem miNewDefine = new MenuItem("������ѯ");
        MenuItem miDeleteDefine = new MenuItem("ɾ��");

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.ContextMenu == null)
                {
                    this.ContextMenu = new ContextMenu();
                }

                //����µ�����
                if (this.SelectedNode.Parent != null)
                {
                    //�༭
                    if (!this.ContextMenu.MenuItems.Contains(this.miEdit))
                    {
                        this.ContextMenu.MenuItems.Add(this.miEdit);
                    }
                    if (this.ContextMenu.MenuItems.Contains(this.miNewType))
                    {
                        this.ContextMenu.MenuItems.Remove(this.miNewType);
                    }
                }
                else
                {
                    //�༭
                    if (this.ContextMenu.MenuItems.Contains(this.miEdit))
                    {
                        this.ContextMenu.MenuItems.Remove(this.miEdit);
                    }

                    if (!this.ContextMenu.MenuItems.Contains(this.miNewType))
                    {
                        this.ContextMenu.MenuItems.Add(this.miNewType);
                    }
                }

                //����µ��Զ����ѯ
                if (this.SelectedNode.ImageIndex > 3)
                {
                    if (this.ContextMenu.MenuItems.Contains(this.miNewDefine))
                    {
                        this.ContextMenu.MenuItems.Remove(this.miNewDefine);
                    }
                }
                else
                {
                    if (!this.ContextMenu.MenuItems.Contains(this.miNewDefine))
                    {
                        this.ContextMenu.MenuItems.Add(this.miNewDefine);
                    }
                }

                //ɾ��
                if (!this.ContextMenu.MenuItems.Contains(this.miDeleteDefine))
                {
                    this.ContextMenu.MenuItems.Add(this.miDeleteDefine);
                }
            }
            base.OnMouseClick(e);
        }

        void miEdit_Click(object sender, EventArgs e)
        {
            this.LabelEdit = true;
            this.SelectedNode.BeginEdit();
        }

        void miNewType_Click(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = (this.SelectedNode.Tag as FS.FrameWork.Models.NeuObject).Clone();
                TreeNode node = new TreeNode();
                node.Tag = obj;
                node.Text = "�½��ļ���" + (this.SelectedNode.Nodes.Count + 1).ToString();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 3;
                this.SelectedNode.Nodes.Add(node);
                this.SelectedNode = node;

                this.LabelEdit = true;
                node.BeginEdit();
            }
            catch
            { }
        }

        void miNewDefine_Click(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.Models.NeuObject d = (this.SelectedNode.Tag as FS.FrameWork.Models.NeuObject).Clone();

                //����������ͼ
                //d.ID = "";

                //����
                d.Name = this.SelectedNode.Text;

                TreeNode node = new TreeNode();
                node.Tag = d;
                node.Text = "�½�" + (this.SelectedNode.Nodes.Count + 1).ToString();
                node.ImageIndex = 4;
                node.SelectedImageIndex = 5;

                this.SelectedNode.Nodes.Add(node);
                this.SelectedNode = node;

                this.LabelEdit = true;
                node.BeginEdit();
            }
            catch
            { }
        }

        void miDeleteDefine_Click(object sender, EventArgs e)
        {
            //FS.FrameWork.Models.NeuObject obj = (this.SelectedNode.Tag as FS.FrameWork.Models.NeuObject).Clone();
            if (this.DeleteDefineHandler != null)
            {
                this.DeleteDefineHandler();
            }
        }
        #endregion
    }
}
