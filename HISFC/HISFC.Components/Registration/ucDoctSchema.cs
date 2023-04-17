using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Registration
{
    public partial class ucDoctSchema : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDoctSchema()
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

        private bool isAllowTimeCross = false;
        /// <summary>
        /// �Ƿ������Ű�ʱ�佻��
        /// </summary>
        [Category("����"), Description("�Ƿ������Ű�ʱ�佻��")]
        public bool IsAllowTimeCross
        {
            get { return isAllowTimeCross; }
            set { isAllowTimeCross = value; }
        }

        private bool isCheckChangceAndSave = true;
        /// <summary>
        /// �Ƿ�����������ʾ����
        /// </summary>
        [Category("����"), Description("�Ƿ�����������ʾ����"), DefaultValue(true)]
        public bool IsCheckChangceAndSave
        {
            get { return isCheckChangceAndSave; }
            set { isCheckChangceAndSave = value; }
        }
        /// <summary>
        /// ͣ���Ƿ�֪ͨԤԼƽ̨
        /// </summary>
        [Category("����"), Description("ͣ���Ƿ�֪ͨԤԼƽ̨")]
        public bool IsNotifyAppointPlatForm
        {
            get { return isNotifyAppointPlatForm; }
            set { isNotifyAppointPlatForm = value; }
        }
        private bool isNotifyAppointPlatForm = false;
        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected Registration.ucSchema[] controls;

        /// <summary>
        /// �Ű�ģ�����
        /// </summary>
        protected FS.HISFC.Models.Base.EnumSchemaType SchemaType;

        private void frmDoctShemaTemplet_Load(object sender, EventArgs e)
        {
            //if (this.Tag == null || this.Tag.ToString() == "" || this.Tag.ToString().ToUpper() == "DEPT")
            //{
            //    this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
            //    this.Text = "ר���Ű�";
            //}
            //else
            //{
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Doct;
                this.Text = FS.FrameWork.Management.Language.Msg( "ר���Ű�" );
            //}

            this.InitTab();

            this.InitArray();

            this.InitDept();

            this.InitControls();

            //��ѯ������������
            this.QueryAll();

            this.QueryTodaySchema();

            this.treeView1.SelectedNode = this.treeView1.Nodes[0];

            this.treeView1_AfterSelect(null, null);
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

            this.ucSchema1.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema2.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema3.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema4.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema5.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema6.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            this.ucSchema7.IsCheckChangceAndSave = this.isCheckChangceAndSave;
            controls[0] = this.ucSchema1;
            controls[1] = this.ucSchema2;
            controls[2] = this.ucSchema3;
            controls[3] = this.ucSchema4;
            controls[4] = this.ucSchema5;
            controls[5] = this.ucSchema6;
            controls[6] = this.ucSchema7;
            //������
            foreach (ucSchema us in controls)
            {
                us.IsAllowTimeCross = this.isAllowTimeCross;
            }
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
                obj.IsNotifyAppointPlatForm = isNotifyAppointPlatForm;
                obj.Init(DateTime.Parse(tabControl1.TabPages[i].Tag.ToString()), this.SchemaType);
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        private void InitDept()
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            this.treeView1.Nodes.Clear();

            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList DeptType = constantMgr.GetList("ScheduleType");
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            foreach (NeuObject item in DeptType)
            {
                TreeNode parent = new TreeNode(FS.FrameWork.Management.Language.Msg(item.Name));
                this.treeView1.ImageList = this.treeView1.deptImageList;
                parent.ImageIndex = 5;
                parent.SelectedImageIndex = 5;
                parent.Tag = item.ID;
                this.treeView1.Nodes.Add(parent);
                ArrayList al = null;
                if (item.ID == "1")
                {
                    al = deptMgr.QueryRegDepartment();
                } //{0FBEA522-F50E-4fd2-9108-9A8FA8712890} ���B���Ű�
                else
                {
                    al = new ArrayList();
                    FS.HISFC.Models.Base.Department dept = deptMgr.GetDepartment(item.Memo);
                    al.Add(dept);
                }
                if (al == null)
                {
                    MessageBox.Show("��ȡ�����б�ʱ����!" + deptMgr.Err, "��ʾ");
                    return;
                }

                foreach (FS.HISFC.Models.Base.Department dept in al)
                {
                    if (curDepartment.HospitalID == dept.HospitalID)// {D59C1D74-CE65-424a-9CB3-7F9174664504
                    {
                        TreeNode node = new TreeNode();
                        node.Text = dept.Name;
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 1;
                        node.Tag = dept;

                        parent.Nodes.Add(node);
                    }
                }

                parent.ExpandAll();

                this.cmbDept.AddItems(al);
            }
            
        }

        /// <summary>
        /// ��ѯ�����Ű�
        /// </summary>
        private void QueryAll()
        {
            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.SelectedIndex = i;
            }
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
            if (node.Parent == null)
                controls[this.tabControl1.SelectedIndex].SchemaType = (FS.HISFC.Models.Base.EnumSchemaType)Convert.ToInt32(node.Tag);
            else
                controls[this.tabControl1.SelectedIndex].SchemaType = (FS.HISFC.Models.Base.EnumSchemaType)Convert.ToInt32(node.Parent.Tag);
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
            //{0FBEA522-F50E-4fd2-9108-9A8FA8712890} ���B���Ű�
            controls[this.tabControl1.SelectedIndex].Init(DateTime.Parse(tabControl1.TabPages[tabControl1.SelectedIndex].Tag.ToString()), controls[this.tabControl1.SelectedIndex].SchemaType);
           // controls[this.tabControl1.SelectedIndex].initDoct();
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

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"), "��ʾ");

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
        }
        /// <summary>
        /// ����ģ��
        /// </summary>
        private void LoadTemplet()
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;
            
            frmSelectWeek f = new frmSelectWeek();

            DateTime week = DateTime.Parse(this.tabControl1.SelectedTab.Tag.ToString());
            f.SelectedWeek = week.DayOfWeek;
            if (f.ShowDialog() == DialogResult.Yes)
            {
                FS.HISFC.BizLogic.Registration.SchemaTemplet templetMgr = new FS.HISFC.BizLogic.Registration.SchemaTemplet();


                string deptCode = "ALL";
                try
                {
                    //if (this.treeView1.SelectedNode.Index > 0)ʲô������Ӧ���Ը��ڵ����жϰ�
                    if (this.treeView1.SelectedNode.Parent != null)
                    {
                        deptCode = (this.treeView1.SelectedNode.Tag as FS.HISFC.Models.Base.Department).ID;
                    }
                }
                catch (Exception)
                {                    
                }

                //��ȡȫ��ģ����Ϣ
                ArrayList al = templetMgr.Query(this.SchemaType, f.SelectedWeek, deptCode);
                if (al == null)
                {
                    MessageBox.Show("��ѯģ����Ϣʱ����!" + templetMgr.Err, "��ʾ");
                    return;
                }

                ArrayList newAll = new ArrayList();// {63B27717-4D42-46d6-9AE3-CE89853E9B5E
                foreach (FS.HISFC.Models.Registration.SchemaTemplet templet1 in al)
                {
                    FS.HISFC.Models.Base.Department dept = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(templet1.Dept.ID);
                    //schema.Templet.Dept.ID;
                    if (dept.HospitalID == curDepartment.HospitalID)
                    {
                        newAll.Add(templet1);
                    }
                }

                al = new ArrayList();
                al = newAll;

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

            frmFindEmployee f = new frmFindEmployee();
            f.SelectedEmployee = controls[Index].SearchEmployee;

            this.cmbDept.Focus();

            if (f.ShowDialog() == DialogResult.Yes)
            {
                controls[Index].Focus();
                controls[Index].SearchEmployee = f.SelectedEmployee;
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
        // ѡ�������л���ʾ��������
        private void tabControl1_SelectionChanging(object sender, EventArgs e)
        {
            if (!this.isCheckChangceAndSave)
            {
                return;
            }
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
            //object obj = controls[this.tabControl1.SelectedIndex].Tag;

            //if (obj == null || obj.ToString() == "")
            //{
            //    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //    this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));

            //    controls[this.tabControl1.SelectedIndex].Tag = "Has Retrieve";
            //}
            this.treeView1_AfterSelect(null, null);
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

        /// <summary>
        /// �����Ű�XLS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.ExportData();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <returns></returns>
        private int ExportData()
        {
            FS.FrameWork.WinForms.Controls.NeuSpread fpSpreads = new FS.FrameWork.WinForms.Controls.NeuSpread();
            string[] Week = new string[] { "һ", "��", "��", "��", "��", "��", "��" };
            Registration.ucSchema sc;

            this.tabControl1.SelectedIndex = 0; //��֪Ϊ�ε�һ���޷����أ�ѡ����ٻ�ѡ
            this.QueryTodaySchema();
            
            for (int i = 0; i < 7; i++)
            {
                sc = controls[i];
                FarPoint.Win.Spread.SheetView sv = sc.GetFpSheet();
                sv.SheetName = "����" + Week[i];
                fpSpreads.Sheets.Add(sv);
            }
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    fpSpreads.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("�����ɹ�", "��ʾ");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ݷ�������>>" + ex.Message);
                return -1;
            }
            return 1;
        }        




    }
}
