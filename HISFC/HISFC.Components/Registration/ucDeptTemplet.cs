using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration
{
    public partial class ucDeptTemplet : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptTemplet()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucTempletForm_Load);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            this.treeView1.AfterSelect  += new TreeViewEventHandler(treeView1_AfterSelect);

            this.tabControl1.Deselecting += new TabControlCancelEventHandler(tabControl1_SelectionChanging);
            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_SelectionChanged);
            
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.KeyDown += new KeyEventHandler(cmbDept_KeyDown);
        }

        #region ����
        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected Registration.ucSchemaTemplet[] controls;
        /// <summary>
        /// �Ű�ģ�����
        /// </summary>
        protected FS.HISFC.Models.Base.EnumSchemaType SchemaType;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region �¼�
        private void ucTempletForm_Load(object sender, EventArgs e)
        {
            if (this.Tag == null || this.Tag.ToString() == "" || this.Tag.ToString().ToUpper() == "DEPT")
            {
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
                this.Text = FS.FrameWork.Management.Language.Msg( "ר���Ű�ģ��ά��" );
                //this.tbFind.Visible = false;
            }
            else
            {
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Doct;
                this.Text = FS.FrameWork.Management.Language.Msg( "ר���Ű�ģ��ά��" );
            }

            this.InitArray();

            this.InitControls();

            this.InitDept();

            this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            int Index = this.tabControl1.SelectedIndex;
            Index++;

            if (Index == 7) Index = 0;

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ѿ��ı�,�Ƿ񱣴�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (controls[Index].Save() == -1)
                    {
                        e.Cancel = true;
                        controls[Index].Focus();
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;

            if (node == null) return;

            string deptID;
            FS.HISFC.Models.Base.Department dept = null;

            if (node.Parent == null)//���ڵ�
            {
                deptID = "ALL";
            }
            else
            {
                dept = (FS.HISFC.Models.Base.Department)node.Tag;
                deptID = dept.ID;
            }

            int Index = this.tabControl1.SelectedIndex;

            Index++;

            if (Index == 7) Index = 0;

            controls[Index].Dept = dept;
            controls[Index].Query(deptID);

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            //{
            //    this.Del();
            //    return true;
            //}
            //else if (keyData == Keys.Add || keyData == Keys.Oemplus)
            //{
            //    this.Add();
            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            //{
            //    this.Save();
            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();
            //}
            if (keyData == Keys.F1)
            {
                this.cmbDept.Focus();
                return true;
            }
            //else if (keyData == Keys.F3)
            //{
            //    this.Find();
            //    return true;
            //}

            return base.ProcessDialogKey(keyData);
        }

        private void tabControl1_SelectionChanging(object sender, EventArgs e)
        {
            int Index = this.tabControl1.SelectedIndex;
            int TabIndex = Index;

            Index++;

            if (Index == 7) Index = 0;

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�����Ѿ��ı�,�Ƿ񱣴�" ), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (controls[Index].Save() == -1)
                    {
                        //this.tabControl1.SelectedIndex = TabIndex ;
                        return;
                    }
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = this.tabControl1.TabPages[this.tabControl1.SelectedIndex].Tag;

            if (obj == null || obj.ToString() == "")
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));
                this.tabControl1.TabPages[this.tabControl1.SelectedIndex].Tag = "Has Retrieve";
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if ((node.Tag as FS.HISFC.Models.Base.Department).ID == this.cmbDept.Tag.ToString())
                {
                    this.treeView1.SelectedNode = node;
                    break;
                }
            }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int Index = this.tabControl1.SelectedIndex;

                if (Index == 6)
                {
                    Index = 0;
                }
                else
                {
                    Index = Index + 1;
                }

                controls[Index].Focus();
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "",(int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton("ȫ��ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Add();
                    break;
                case "ɾ��":
                    this.Del();
                    break;
                case "ȫ��ɾ��":
                    this.DelAll();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 0;
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitArray()
        {
            controls = new Registration.ucSchemaTemplet[7];
            controls[0] = this.ucSchemaTemplet7;
            controls[1] = this.ucSchemaTemplet1;
            controls[2] = this.ucSchemaTemplet2;
            controls[3] = this.ucSchemaTemplet3;
            controls[4] = this.ucSchemaTemplet4;
            controls[5] = this.ucSchemaTemplet5;
            controls[6] = this.ucSchemaTemplet6;
        }

        /// <summary>
        /// ��ʼ��ģ��ؼ�
        /// </summary>
        private void InitControls()
        {
            Registration.ucSchemaTemplet obj;

            for (int i = 0; i < 7; i++)
            {
                obj = controls[i];
                obj.Init((DayOfWeek)i, this.SchemaType);
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        private void InitDept()
        {
            this.treeView1.Nodes.Clear();
            TreeNode parent = new TreeNode( FS.FrameWork.Management.Language.Msg( "�������" ) );
            this.treeView1.ImageList = this.treeView1.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.treeView1.Nodes.Add(parent);

            FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList al = Mgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("��ȡ�����б�ʱ����!" + Mgr.Err, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Base.Department dept in al)
            {
                TreeNode node = new TreeNode();
                node.Text = dept.Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = dept;

                parent.Nodes.Add(node);
            }

            this.cmbDept.AddItems(al);
            parent.ExpandAll();
        }



        //private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        //{
        //    if (e.Button == this.tbAdd)
        //    {
        //        this.Add();
        //    }
        //    else if (e.Button == this.tbDel)
        //    {
        //        this.Del();
        //    }
        //    else if (e.Button == this.tbSave)
        //    {
        //        this.Save();
        //    }
        //    else if (e.Button == this.tbExit)
        //    {
        //        this.Close();
        //    }
        //    else if (e.Button == this.tbDelAll)
        //    {
        //        this.DelAll();
        //    }
        //    else if (e.Button == this.tbFind)
        //    {
        //        this.Find();
        //    }
        //}
        /// <summary>
        /// ����
        /// </summary>
        private void Add()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            controls[Index].Add();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            controls[Index].Del();
        }

        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        private void DelAll()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            controls[Index].DelAll();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            if (controls[Index].Save() == -1)
            {
                controls[Index].Focus();
                return;
            }

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "����ɹ�" ), "��ʾ" );

            controls[Index].Focus();
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        private void Find()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            this.cmbDept.Focus();

            //frmFindEmployee f = new frmFindEmployee();
            //f.SelectedEmployee = controls[Index].SearchEmployee;

            //if (f.ShowDialog() == DialogResult.Yes)
            //{
            //    controls[Index].Focus();
            //    controls[Index].SearchEmployee = f.SelectedEmployee;
            //    f.Dispose();
            //}
        }
        #endregion

    }
}
