using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucUserSnomedTextControl : ucUserTextControl
    {
        public ucUserSnomedTextControl()
        {
            InitializeComponent();
            ImageList imageList = new ImageList();
            imageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��));
            imageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��));
            this.treeView1.ImageList = imageList;
        }

        DataSet ds;
        DataTable dataTable;

        #region ��ʼ��DataTable
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
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["ID"] };

            System.Collections.ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSNOMED();

            foreach (FS.HISFC.Models.EPR.SNOMED s in al)
            {
                dataTable.Rows.Add(
                    new object[]{
                    s.ID,
                    s.Name,
                    s.SNOPCode,
                    s.EnglishName,
                    s.DiagnoseCode,
                    s.ParentCode,
                    s.Memo,
                    s.SpellCode,
                    s.WBCode,
                    s.UserCode,
                    s.SortID});

            }

        }
        #endregion

        #region snomed����
        ArrayList allData = new ArrayList();
        private void CreateTree(System.Windows.Forms.TreeView tv)
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
            allData = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSNOMED();
            foreach (FS.HISFC.Models.EPR.SNOMED obj in allData)
            {
                if (obj.User01 == "1")
                {

                    al.Add(obj.Clone());
                }
            }

            foreach (FS.HISFC.Models.EPR.SNOMED obj in al)
            {
                if (obj.SNOPCode.IndexOf('\\') > 0)
                {
                    obj.User01 = obj.SNOPCode;
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
                    myGetNodeFromFullPath(parpath, nodeParent, ref node);
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


        private void myGetNodeFromFullPath(string tag, TreeNode tv, ref TreeNode rtnNode)
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
                        myGetNodeFromFullPath(tag, childNode, ref rtnNode);
                    }
                }
            }
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
                if (string.Compare(dr["PARENTCODE"].ToString().ToLower(), "root") == 0)
                {
                    foreach (TreeNode treeNode in tn.Nodes)
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
                            if (string.Compare(dt["PARENTCODE"].ToString().ToLower(), treeModual.Text.ToLower()) == 0)
                            {
                                foreach (TreeNode treeNodeType in treeModual.Nodes)
                                {
                                    if (treeNodeType.Text.ToLower() == dr["ID"].ToString().ToLower())
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

        #endregion

        private void ucUserSnomedTextControl_Load(object sender, EventArgs e)
        {
            //ds = new DataSet();
            //initialTable();
            //ds.Tables.Add(dataTable);
            //ds.Tables["cnp_com_snopmed"].AcceptChanges();//���Ĺ���
            //initialTree(this.treeView1, ds);
            this.treeView1.Nodes.Add("Ԫ��");
            this.treeView1.Nodes[0].Nodes.Add("...");
            this.treeView1.CollapseAll();
            this.treeView1.ItemDrag += new ItemDragEventHandler(treeView1_ItemDrag);
            this.treeView1.DragEnter += new DragEventHandler(treeView1_DragEnter);
            this.treeView1.BeforeCollapse += new TreeViewCancelEventHandler(treeView1_BeforeCollapse);
            Container c = new Container();
            c.Add(this.richTextBox1 );
            this.SetControl(c);
            
        }

        void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text == "Ԫ��" && e.Node.Nodes.Count == 1)
            {
                e.Cancel = true;
                this.CreateTree(this.treeView1);
            }
        }
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ucUserText_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                //string obj1 = e.Data.GetData(DataFormats.StringFormat, true).ToString();
                //string fullPath = "ID='" + obj1 + "'";
                //DataRow[] dr = ds.Tables["cnp_com_snopmed"].Select(fullPath);
                //foreach (DataRow d in dr)
                //{
                //    obj1 = "^@" + d["NAME"] + "#" + d["ID"] + "@^";
                //}
                //if (obj1 == "") return;
                //try
                //{
                //    if (sender.GetType().ToString().IndexOf("RichTextBox") > 0)
                //    {
                //        ((RichTextBox)sender).SelectedText = obj1;
                //    }
                //    else if (sender.GetType().ToString().IndexOf("MultiLine") > 0)
                //    {
                //        ((RichTextBox)sender).SelectedText = obj1;
                //    }
                //    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
                //    {
                //        ((TextBox)sender).SelectedText = obj1;
                //    }
                //    else if (sender.GetType().ToString().IndexOf("ComboBox") > 0)
                //    {
                //        ((ComboBox)sender).SelectedText = obj1;
                //    }
                //    else
                //    {
                //        try
                //        {
                //            ((TextBox)sender).SelectedText = obj1;
                //        }
                //        catch
                //        {
                //            ((Control)sender).Text = obj1;
                //        }
                //    }
                //}
                //catch { }
                //e.Data.SetData("");
                string fullPath = e.Data.GetData(DataFormats.StringFormat, true).ToString();
                string obj1 = "";

                foreach (FS.HISFC.Models.EPR.SNOMED snomed in allData)
                {
                    if ("Ԫ��\\" + snomed.SNOPCode == fullPath)
                    {
                        obj1 = "^@" + snomed.Name + "#" + snomed.ID + "@^";
                        break;
                    }
                }

               
                if (obj1 == "") return;
                try
                {
                    if (sender.GetType().ToString().IndexOf("RichTextBox") > 0)
                    {
                        ((RichTextBox)sender).SelectedText = obj1;
                    }
                    else if (sender.GetType().ToString().IndexOf("MultiLine") > 0)
                    {
                        ((RichTextBox)sender).SelectedText = obj1;
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox))
                    {
                        ((TextBox)sender).SelectedText = obj1;
                    }
                    else if (sender.GetType().ToString().IndexOf("ComboBox") > 0)
                    {
                        ((ComboBox)sender).SelectedText = obj1;
                    }
                    else
                    {
                        try
                        {
                            ((TextBox)sender).SelectedText = obj1;
                        }
                        catch
                        {
                            ((Control)sender).Text = obj1;
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
            TreeNode obj = e.Item as TreeNode;
            if (obj == null) return;
            DoDragDrop(obj.FullPath.ToString(), DragDropEffects.Copy);

        }

        private void ucUserText_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// ѭ�����ÿؼ���
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
    }
}
