using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.EPR.Controls
{
    /// <summary>
    /// [��������: �û������ı�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucUserCommonText : UserControl
    {
        public ucUserCommonText()
        {
            InitializeComponent();

            this.treeView1.Nodes.Clear();
            nodeSign = new System.Windows.Forms.TreeNode("������Ϣ");
            nodeRelation = new System.Windows.Forms.TreeNode("�����Ϣ");
            nodeWord = new System.Windows.Forms.TreeNode("�ַ���Ϣ");
            nodeNormal = new System.Windows.Forms.TreeNode("������Ϣ");

            this.treeView1.Nodes.Add(nodeSign);//0
            this.treeView1.Nodes.Add(nodeRelation);//1
            this.treeView1.Nodes.Add(nodeWord);//2
            this.treeView1.Nodes.Add(nodeNormal);//3
        }

        #region ����
        TreeNode nodeSign = null;
        TreeNode nodeWord = null;
        TreeNode nodeRelation = null;
        TreeNode nodeNormal = null;
        #endregion

        private void ucUserCommonText_Load(object sender, EventArgs e)
        {
            try
            {
                this.RefreshList();
                this.treeView1.MouseHover += new EventHandler(treeView1_MouseHover);
                this.treeView1.ItemDrag += new ItemDragEventHandler(treeView1_ItemDrag);
                this.treeView1.DragEnter += new DragEventHandler(treeView1_DragEnter);
                this.treeView1.MouseMove += new MouseEventHandler(treeView1_MouseMove);
                this.toolTip1.InitialDelay = 0;
                this.toolTip1.ReshowDelay = 0;
                this.toolTip1.AutomaticDelay = 0;
            }
            catch { }

        }

        //FS.HISFC.Management.Manager.UserText manager = new FS.HISFC.Management.Manager.UserText();

        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshList()
        {
            this.nodeSign.Nodes.Clear();
            this.nodeRelation.Nodes.Clear();
            this.nodeWord.Nodes.Clear();

            //��õ�ǰ����Ա��
            FS.HISFC.Models.Base.Employee p = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (p == null) return;

            #region ��ó��÷����б�
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetUserTextList("SIGN", 2);//��ó��÷����б�
            if (al == null)
            {
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
                return;
            }
            foreach (FS.HISFC.Models.Base.UserText obj in al)
            {
                TreeNode node = new TreeNode(obj.Name);
                try
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                }
                catch { }
                node.Tag = obj;
                this.nodeSign.Nodes.Add(node);//����
            }
            #endregion

            #region ��ó��õ���
            al = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetUserTextList("WORD", 2);//��ó��õ����б�
            if (al == null)
            {
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
                return;
            }
            foreach (FS.HISFC.Models.Base.UserText obj in al)
            {
                TreeNode node = new TreeNode(obj.Name);
                try
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                }
                catch { }
                node.Tag = obj;
                this.nodeWord.Nodes.Add(node);//����
            }
            #endregion

            #region �����Ϣ

            if (this.myInpatientno == "")
            {
                al = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetUserTextList("RELATION", 2);//��ó��������Ϣ�б�
                if (al == null)
                {
                    MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
                    return;
                }
                foreach (FS.HISFC.Models.Base.UserText obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    try
                    {
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 2;
                    }
                    catch { }
                    node.Tag = obj;
                    this.nodeRelation.Nodes.Add(node);//�����Ϣ
                }
            }
            else
            {
                this.SetPatient(this.myInpatientno, this.myTable, this.iSql);
            }
            #endregion


            this.treeView1.ExpandAll();
        }


        #region ���߸���

        protected string myInpatientno;
        protected string myTable;
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="table"></param>
        public void SetPatient(string inpatientNo, string table, FS.FrameWork.Management.Interface ISql)
        {
           // FS.HISFC.Management.File.DataFile datafileManager = new FS.HISFC.Management.File.DataFile();

            this.nodeRelation.Nodes.Clear();
            this.nodeNormal.Nodes.Clear();

            myInpatientno = inpatientNo;
            myTable = table;

            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.GetUserTextList("RELATION", 2);//��ó��������Ϣ�б�
            if (al == null)
            {
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
                return;
            }
            foreach (FS.HISFC.Models.Base.UserText obj in al)
            {
                TreeNode node = new TreeNode(obj.Name);
                string sName = obj.Name;

                //ȥ��[]
                if (sName.Substring(0, 1) == "[" && sName.Substring(sName.Length - 1) == "]")
                    sName = sName.Substring(1, sName.Length - 2);
                //��ýڵ���ֵ
                string sValue = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetNodeValueFormDataStore(table, inpatientNo, sName);
                if (sValue == "-1") sValue = "";
                obj.Text = sValue;

                try
                {
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                }
                catch { }
                node.Tag = obj;
                this.nodeRelation.Nodes.Add(node);//�����Ϣ
            }

            if (ISql == null) return;
            this.iSql = ISql;
            for (int j = 0; j < ISql.Count; j++)
            {
                FS.FrameWork.Models.NeuInfo obj = ISql.GetInfo(j);
                FS.HISFC.Models.Base.UserText objText = new FS.HISFC.Models.Base.UserText();
                objText.Name = obj.Name;
                objText.Text = obj.value;
                if (obj.showType.Trim() == "1" && obj.type == FS.FrameWork.Models.NeuInfo.infoType.Temp)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    try
                    {
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 2;
                    }
                    catch { }
                    node.Tag = objText;
                    this.nodeNormal.Nodes.Add(node);//������Ϣ
                }
            }
        }

        protected FS.FrameWork.Management.Interface iSql = null;


        #endregion

        #region ���������¼�
        private void treeView1_MouseHover(object sender, EventArgs e)
        {

        }
        protected FS.HISFC.Models.Base.UserText GetSelectedObject(TreeNode node)
        {
            if (node.Tag == null) return null;
            return node.Tag as FS.HISFC.Models.Base.UserText;
        }
        /// <summary>
        /// �½���
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="RichText"></param>
        /// <returns></returns>
        public int Add(string Text, string RichText)
        {
            FS.HISFC.Models.Base.UserText obj = new FS.HISFC.Models.Base.UserText();
            obj.Text = Text;
            obj.RichText = RichText;
            ucUserTextControl u = new ucUserTextControl();
            u.IsShowGeneral = true;
            u.UserText = obj;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
            this.RefreshList();
            return 0;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.Models.Base.UserText obj = this.GetSelectedObject(this.treeView1.SelectedNode);
            if (obj == null) return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.HISFC.Management.Manager.UserText m = new FS.HISFC.Management.Manager.UserText();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(m.Connection);
            //t.BeginTransaction();
            //m.SetTrans(t.Trans);
            int i = 0;
            i = FS.HISFC.BizProcess.Factory.Function.IntegrateManager.DeleteUserText(obj.ID);
            if (i == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateManager.Err);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ɾ���ɹ���");
                this.RefreshList();
            }
        }
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// ���ÿؼ�������ק�¼�
        /// </summary>
        /// <param name="c"></param>
        public void SetControl(IContainer c)
        {
            if (c == null) return;
            foreach (Component p in c.Components)
            {
                try
                {
                    ((Control)p).AllowDrop = true;
                    try
                    {
                        ((Control)p).DragEnter -= new DragEventHandler(ucUserText_DragEnter);
                        ((Control)p).DragDrop -= new DragEventHandler(ucUserText_DragDrop);
                    }
                    catch { }
                    ((Control)p).DragEnter += new DragEventHandler(ucUserText_DragEnter);
                    ((Control)p).DragDrop += new DragEventHandler(ucUserText_DragDrop);
                }
                catch { }

            }
        }
        private void ucUserText_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void ucUserText_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.String))) //FS.HISFC.Models.Base.UserText
            {
                //FS.HISFC.Models.Base.UserText obj = e.Data.GetData("FS.HISFC.Models.Base.UserText") as FS.HISFC.Models.Base.UserText;
                FS.HISFC.Models.Base.UserText obj = new FS.HISFC.Models.Base.UserText();
                string s = e.Data.GetData(DataFormats.StringFormat, true).ToString();
                obj.Text = s;
                if (s == "") return;
                try
                {
                    if (sender.GetType().ToString().IndexOf("RichTextBox") > 0)
                    {
                        ((RichTextBox)sender).SelectedText = obj.Text;
                    }
                    else if (sender.GetType().ToString().IndexOf("MultiLine") > 0)
                    {
                        ((RichTextBox)sender).SelectedText = obj.Text;
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
                    {
                        ((TextBox)sender).SelectedText = obj.Text;
                    }
                    else if (sender.GetType().ToString().IndexOf("ComboBox") > 0)
                    {
                        ((ComboBox)sender).SelectedText = obj.Text;
                    }
                    else
                    {
                        try
                        {
                            ((TextBox)sender).SelectedText = obj.Text;
                        }
                        catch
                        {
                            ((Control)sender).Text = obj.Text;
                        }
                    }
                }
                catch { }
                e.Data.SetData("");
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            FS.HISFC.Models.Base.UserText obj = this.GetSelectedObject(e.Item as TreeNode);
            if (obj == null) return;
            DoDragDrop(obj.Text, DragDropEffects.Copy);
        }



        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = this.treeView1.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            TreeNode node = this.treeView1.GetNodeAt(p);
            if (node == null || node.Tag == null)
            {
                this.toolTip1.SetToolTip(this.treeView1, "");
                this.toolTip1.Active = true;
            }
            else
            {
                this.toolTip1.SetToolTip(this.treeView1, this.GetSelectedObject(node).Text);
                this.toolTip1.Active = true;

            }
        }
        #endregion

        #region �ʵ�
        private int iLoop = 0;
        private void label1_DoubleClick(object sender, System.EventArgs e)
        {
            //if (iLoop > 10)
            //{
                Add("", "");
            //}
            //iLoop++;
        }
        #endregion

    }
}
