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
        /// 是否允许排班时间交叉
        /// </summary>
        [Category("设置"), Description("是否允许排班时间交叉")]
        public bool IsAllowTimeCross
        {
            get { return isAllowTimeCross; }
            set { isAllowTimeCross = value; }
        }

        private bool isCheckChangceAndSave = true;
        /// <summary>
        /// 是否检查表格变更并提示保存
        /// </summary>
        [Category("设置"), Description("是否检查表格变更并提示保存"), DefaultValue(true)]
        public bool IsCheckChangceAndSave
        {
            get { return isCheckChangceAndSave; }
            set { isCheckChangceAndSave = value; }
        }
        /// <summary>
        /// 停诊是否通知预约平台
        /// </summary>
        [Category("设置"), Description("停诊是否通知预约平台")]
        public bool IsNotifyAppointPlatForm
        {
            get { return isNotifyAppointPlatForm; }
            set { isNotifyAppointPlatForm = value; }
        }
        private bool isNotifyAppointPlatForm = false;
        /// <summary>
        /// 排班控件数组,方便操作
        /// </summary>
        protected Registration.ucSchema[] controls;

        /// <summary>
        /// 排班模板类别
        /// </summary>
        protected FS.HISFC.Models.Base.EnumSchemaType SchemaType;

        private void frmDoctShemaTemplet_Load(object sender, EventArgs e)
        {
            //if (this.Tag == null || this.Tag.ToString() == "" || this.Tag.ToString().ToUpper() == "DEPT")
            //{
            //    this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
            //    this.Text = "专科排班";
            //}
            //else
            //{
                this.SchemaType = FS.HISFC.Models.Base.EnumSchemaType.Doct;
                this.Text = FS.FrameWork.Management.Language.Msg( "专家排班" );
            //}

            this.InitTab();

            this.InitArray();

            this.InitDept();

            this.InitControls();

            //查询本周所有数据
            this.QueryAll();

            this.QueryTodaySchema();

            this.treeView1.SelectedNode = this.treeView1.Nodes[0];

            this.treeView1_AfterSelect(null, null);
            //
            //			this.treeView1_AfterSelect(new object(),new System.Windows.Forms.TreeViewEventArgs(new TreeNode(),System.Windows.Forms.TreeViewAction.Unknown));
        }
        /// <summary>
        /// 生成排班日期
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
            string[] Week = new string[] { "一", "二", "三", "四", "五", "六", "日" };

            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.TabPages[i].Tag = Monday.AddDays(i);
                this.tabControl1.TabPages[i].Text = Monday.AddDays(i).ToString("yyyy-MM-dd") + "  " + Week[i];
            }
        }
        /// <summary>
        /// 获取当前日期所在星期的星期一
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private DateTime GetMonday(DateTime current)
        {
            DayOfWeek today = current.DayOfWeek;

            int interval = 1 - (int)today;

            if (interval == 1)//星期日
            {
                interval = -6;//将星期日归到上星期
            }

            return current.AddDays(interval);
        }

        /// <summary>
        /// 初始化数组
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
            //赋属性
            foreach (ucSchema us in controls)
            {
                us.IsAllowTimeCross = this.isAllowTimeCross;
            }
        }

        /// <summary>
        /// 初始化模板控间
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
        /// 生成门诊科室树
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
                } //{0FBEA522-F50E-4fd2-9108-9A8FA8712890} 添加B超排班
                else
                {
                    al = new ArrayList();
                    FS.HISFC.Models.Base.Department dept = deptMgr.GetDepartment(item.Memo);
                    al.Add(dept);
                }
                if (al == null)
                {
                    MessageBox.Show("获取科室列表时出错!" + deptMgr.Err, "提示");
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
        /// 查询当周排班
        /// </summary>
        private void QueryAll()
        {
            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.SelectedIndex = i;
            }
        }


        /// <summary>
        /// 默认显示当日排班
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
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("数据已经改变,是否保存"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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

            if (node.Parent == null)//父节点
            {
                deptID = "ALL";
            }
            else
            {
                dept = (FS.HISFC.Models.Base.Department)node.Tag;
                deptID = dept.ID;
               
            }
            //{0FBEA522-F50E-4fd2-9108-9A8FA8712890} 添加B超排班
            controls[this.tabControl1.SelectedIndex].Init(DateTime.Parse(tabControl1.TabPages[tabControl1.SelectedIndex].Tag.ToString()), controls[this.tabControl1.SelectedIndex].SchemaType);
           // controls[this.tabControl1.SelectedIndex].initDoct();
            controls[this.tabControl1.SelectedIndex].Dept = dept;
            controls[this.tabControl1.SelectedIndex].Query(deptID);
        }
        
        /// <summary>
        /// 增加
        /// </summary>
        private void Add()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].Add();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].Del();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (controls[Index].Save() == -1)
            {
                controls[Index].Focus();
                return;
            }

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"), "提示");

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
        /// 删除
        /// </summary>
        private void DelAll()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].DelAll();
        }
        /// <summary>
        /// 载入模板
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
                    //if (this.treeView1.SelectedNode.Index > 0)什么东西，应该以父节点来判断吧
                    if (this.treeView1.SelectedNode.Parent != null)
                    {
                        deptCode = (this.treeView1.SelectedNode.Tag as FS.HISFC.Models.Base.Department).ID;
                    }
                }
                catch (Exception)
                {                    
                }

                //获取全部模板信息
                ArrayList al = templetMgr.Query(this.SchemaType, f.SelectedWeek, deptCode);
                if (al == null)
                {
                    MessageBox.Show("查询模板信息时出错!" + templetMgr.Err, "提示");
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
        // 选择星期切换提示保存数据
        private void tabControl1_SelectionChanging(object sender, EventArgs e)
        {
            if (!this.isCheckChangceAndSave)
            {
                return;
            }
            int Index = this.tabControl1.SelectedIndex;

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("数据已经修改,是否保存变动"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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
            toolBarService.AddToolButton("增加", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("复制模板", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolBarService.AddToolButton("查找", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);
            toolBarService.AddToolButton("下周", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);
            toolBarService.AddToolButton("上周", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S上一个, true, false, null);
            toolBarService.AddToolButton("全部删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全退, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "增加":
                    this.Add();

                    break;
                case "删除":
                    this.Del();

                    break;
                case "复制模板":
                    this.LoadTemplet();
                    break;
                case "查找":
                    this.Find();
                    break;
                case "上周":
                    Prior();

                    break;
                case "下周":
                    Next();

                    break;
                case "全部删除":
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
        /// 导出排班XLS
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
        /// 数据导出
        /// </summary>
        /// <returns></returns>
        private int ExportData()
        {
            FS.FrameWork.WinForms.Controls.NeuSpread fpSpreads = new FS.FrameWork.WinForms.Controls.NeuSpread();
            string[] Week = new string[] { "一", "二", "三", "四", "五", "六", "日" };
            Registration.ucSchema sc;

            this.tabControl1.SelectedIndex = 0; //不知为何第一天无法加载，选择后再回选
            this.QueryTodaySchema();
            
            for (int i = 0; i < 7; i++)
            {
                sc = controls[i];
                FarPoint.Win.Spread.SheetView sv = sc.GetFpSheet();
                sv.SheetName = "星期" + Week[i];
                fpSpreads.Sheets.Add(sv);
            }
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    fpSpreads.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("导出成功", "提示");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
                return -1;
            }
            return 1;
        }        




    }
}
