using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    public partial class ucDeptSchema : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptSchema()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmDoctShemaTemplet_Load);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            this.tabControl1.Deselecting += new TabControlCancelEventHandler(tabControl1_SelectionChanging);
            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_SelectionChanged);

            this.cmbDept.IsFlat = true;
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDept.KeyDown += new KeyEventHandler(cmbDept_KeyDown);
            
        }

        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected Registration.ucSchema[] controls;
        /// <summary>
        /// �Ű�ģ�����
        /// </summary>
        protected FS.HISFC.Models.Base.EnumSchemaType SchemaType;

        private string  deptID = "All";

        private void frmDoctShemaTemplet_Load(object sender, EventArgs e)
        {
            if (this.Tag == null || this.Tag.ToString() == "" || this.Tag.ToString().ToUpper() == "DEPT")
            {
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
                this.Text = FS.FrameWork.Management.Language.Msg( "ר���Ű�" );
            }
            else
            {
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Doct;
                this.Text = FS.FrameWork.Management.Language.Msg( "ר���Ű�" );
            }

            this.InitTab();

            this.InitArray();

            this.InitDept();

            this.InitControls();

            this.QueryTodaySchema();
            //
            //			this.treeView1_AfterSelect(new object(),new System.Windows.Forms.TreeViewEventArgs(new TreeNode(),System.Windows.Forms.TreeViewAction.Unknown));
        }
        /// <summary>
        /// �����Ű�����
        /// </summary>
        private void InitTab()
        {
            FS.HISFC.BizLogic.Registration.Schema sMgr = new FS.HISFC.BizLogic.Registration.Schema();
            DateTime Current = sMgr.GetDateTimeFromSysDateTime();

            DateTime Monday = this.GetMonday(Current);

            this.setWeek(Monday);
        }
        private void setWeek(DateTime Monday)
        {
            string[] Week = new string[] { "һ", "��", "��", "��", "��", "��", "��" };

            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.TabPages[i].Tag = Monday.AddDays(i);
                this.tabControl1.TabPages[i].Text = Monday.AddDays(i).ToString("yyyy-MM-dd") + "  " + Week[i];
            }
        }
        /// <summary>
        /// ��ȡ��ǰ�����������ڵ�����һ
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private DateTime GetMonday(DateTime current)
        {
            DayOfWeek today = current.DayOfWeek;

            int interval = 1 - (int)today;

            if (interval == 1)//������
            {
                interval = -6;//�������չ鵽������
            }

            return current.AddDays(interval);
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitArray()
        {
            controls = new Registration.ucSchema[7];
            controls[0] = this.ucSchema1;
            controls[1] = this.ucSchema2;
            controls[2] = this.ucSchema3;
            controls[3] = this.ucSchema4;
            controls[4] = this.ucSchema5;
            controls[5] = this.ucSchema6;
            controls[6] = this.ucSchema7;
        }

        /// <summary>
        /// ��ʼ��ģ��ؼ�
        /// </summary>
        private void InitControls()
        {
            Registration.ucSchema obj;

            for (int i = 0; i < 7; i++)
            {
                obj = controls[i];
                obj.Init(DateTime.Parse(tabControl1.TabPages[i].Tag.ToString()), this.SchemaType);
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

            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList al = deptMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("��ȡ�����б�ʱ����!" + deptMgr.Err, "��ʾ");
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

            parent.ExpandAll();

            this.cmbDept.AddItems(al);
        }

        /// <summary>
        /// Ĭ����ʾ�����Ű�
        /// </summary>
        private void QueryTodaySchema()
        {
            FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();

            DateTime today = SchemaMgr.GetDateTimeFromSysDateTime();

            int Index = (int)today.DayOfWeek;
            Index--;
            if (Index == -1) Index = 6;

            this.tabControl1.SelectedIndex = Index;
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            int Index = this.tabControl1.SelectedIndex;

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

            //string deptID;
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

            controls[this.tabControl1.SelectedIndex].Dept = dept;
            controls[this.tabControl1.SelectedIndex].Query(deptID);
        }
        
        /// <summary>
        /// ����
        /// </summary>
        private void Add()
        {
            int Index = this.tabControl1.SelectedIndex;
         

            controls[Index].Add();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].Del();
            controls[Index].Focus();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (controls[Index].Save() == -1)
            {
                controls[Index].Focus();
                return;
            }

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "����ɹ�" ), "��ʾ" );

            controls[Index].Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Next()
        {
            DateTime Monday;

            
             Monday = this.GetMonday(DateTime.Parse(this.tabPage7.Tag.ToString()).AddDays(2));
                            
            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].SeeDate = DateTime.Parse(this.tabControl1.TabPages[i].Tag.ToString());
            }


            this.controls[this.tabControl1.SelectedIndex].Query("ALL");
        }

        private void Prior()
        {
            DateTime Monday;

            Monday = this.GetMonday(DateTime.Parse(this.tabPage1.Tag.ToString()).AddDays(-3));

            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].SeeDate = DateTime.Parse(this.tabControl1.TabPages[i].Tag.ToString());
            }


            this.controls[this.tabControl1.SelectedIndex].Query("ALL");
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void DelAll()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].DelAll();
            controls[Index].Focus();
        }
        /// <summary>
        /// ����ģ��
        /// </summary>
        private void LoadTemplet()
        {
            frmSelectWeek f = new frmSelectWeek();

            DateTime week = DateTime.Parse(this.tabControl1.SelectedTab.Tag.ToString());
            f.SelectedWeek = week.DayOfWeek;
            if (f.ShowDialog() == DialogResult.Yes)
            {
                FS.HISFC.BizLogic.Registration.SchemaTemplet templetMgr = new FS.HISFC.BizLogic.Registration.SchemaTemplet();
                //��ȡȫ��ģ����Ϣ
                //ArrayList al = templetMgr.Query(this.SchemaType, f.SelectedWeek, "ALL");
                ArrayList al = templetMgr.Query(this.SchemaType, f.SelectedWeek, this.deptID);
                if (al == null)
                {
                    MessageBox.Show("��ѯģ����Ϣʱ����!" + templetMgr.Err, "��ʾ");
                    return;
                }

                foreach (FS.HISFC.Models.Registration.SchemaTemplet templet in al)
                {
                    controls[this.tabControl1.SelectedIndex].Add(templet);
                }

                controls[this.tabControl1.SelectedIndex].Focus();
                f.Dispose();
            }
        }
        private void Find()
        {
            int Index = this.tabControl1.SelectedIndex;

            frmFindDepartment f = new frmFindDepartment();
            f.SelectedDepartment = controls[Index].SearchDepartment;

            this.cmbDept.Focus();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.Yes)
            {
                controls[Index].Focus();
                controls[Index].SearchDepartment = f.SelectedDepartment;
                Application.DoEvents();
                f.Dispose();
            }
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
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();
            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            //{
            //    this.Save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            //{
            //    LoadTemplet();

            //    return true;
            //}
            //else if (keyData == Keys.F2)
            //{
            //    Next();

            //    return true;
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

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ѿ��޸�,�Ƿ񱣴�䶯"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (controls[Index].Save() == -1)
                    {
                        return;
                    }
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = controls[this.tabControl1.SelectedIndex].Tag;

            if (obj == null || obj.ToString() == "")
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));

                controls[this.tabControl1.SelectedIndex].Tag = "Has Retrieve";
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
                else
                {
                    if (node.Parent == null)
                    {
                        this.treeView1.SelectedNode = node;
                    }
                    else
                    {
                        this.treeView1.SelectedNode = node.Parent;
                    }

                }
            }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbDept.Text == "" || this.cmbDept.Text == string.Empty)
                {
                    foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
                    {
                       
                            if (node.Parent == null)
                            {
                                this.treeView1.SelectedNode = node;
                            }
                            else
                            {
                                this.treeView1.SelectedNode = node.Parent;
                            }

            
                    }
                }
                int Index = this.tabControl1.SelectedIndex;

                controls[Index].Focus();
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����ģ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C����, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null);
            toolBarService.AddToolButton("ȫ��ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);

            return toolBarService;
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
                case "����ģ��":
                    this.LoadTemplet();
                    break;
                case "����":
                    this.Find();
                    break;
                case "����":
                    Prior();

                    break;
                case "����":
                    Next();

                    break;
                case "ȫ��ɾ��":
                    DelAll();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }
    }
}
