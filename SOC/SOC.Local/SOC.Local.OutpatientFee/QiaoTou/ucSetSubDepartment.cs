using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.QiaoTou
{
    /// <summary>
    /// 设置科室子分类 
    /// </summary>
    public partial class ucSetSubDepartment : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSetSubDepartment()
        {
            InitializeComponent();
        }
        #region 变量
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.FrameWork.Management.Database tempdatabase = new FS.FrameWork.Management.ExtendParam();


        public TreeNode selectedNode = new TreeNode();
        public FS.HISFC.Models.Base.Department selectedDept = new FS.HISFC.Models.Base.Department();

        public bool isNewRow = false;

        public ArrayList deleList = new ArrayList();

        #endregion


        public void ucSetSubDepartment_Load(object sender, System.EventArgs e)
        {
            this.Init();
        }
        #region 菜单
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("添加", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("清空", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == -1)
            {
                return -1;
            }
            return base.OnSave(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "添加":
                    this.Add();
                    break;
                case "删除":
                    this.Delete();
                    break;
                case "清空":
                    this.Clear();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        public void Init()
        {
            this.InitTree();
            this.InitFpFormat();
            this.selectedDept = (this.selectedNode.Tag) as FS.HISFC.Models.Base.Department;

        }
        public void InitTree()
        {
            this.tvDept.Nodes.Clear();
            TreeNode root = new TreeNode("大分类科室");

            root.ImageIndex = 40;
            this.tvDept.Nodes.Add(root);
            //获得用法列表
            ArrayList deptlist = new ArrayList();
            deptlist = managerIntegrate.GetDepartment();
            if (deptlist != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in deptlist)
                {
                    TreeNode node = new TreeNode(obj.Name);
                    node.Tag = obj;
                    node.ImageIndex = 41;
                    root.Nodes.Add(node);
                }
                root.Expand();
                this.cmbDept.AddItems(deptlist);
            }
        }
        public void InitFpFormat()
        {
            this.neuSpread1_Sheet1.Columns.Count = 11;
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "科室编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 1].Text = "科室名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 2].Text = "属性编码";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 3].Text = "属性名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 4].Text = "字符属性";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 5].Text = "数值属性";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 6].Text = "日期属性";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 7].Text = "备注";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 8].Text = "操作人员";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 9].Text = "操作时间";
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 10].Text = "科室类型";

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].CellType = txtType;
            }
            this.setFpWidth();
        }
        public void setFpWidth()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                float preWidth = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
                this.neuSpread1_Sheet1.Columns[i].Width = preWidth;
            }
        }

        public int Save()
        {
            if (deleList.Count != 0)
            {
                foreach (FS.HISFC.Models.Base.DepartmentExt tempdelete in deleList)
                {
                    if (this.deleteSubdept(tempdelete ) == -1)
                    {
                        MessageBox.Show("删除记录失败！");
                        return -1;
                    }
                    
                }
            }
            else 
            {
                ArrayList fpData = this.AddFpToData();
                try
                {
                    foreach (FS.HISFC.Models.Base.DepartmentExt fplist in fpData)
                    {
                        if (this.deleteSubdept(fplist) == -1)
                        {
                            MessageBox.Show("删除记录失败！");
                            return -1;
                        }
                    }

                    foreach (FS.HISFC.Models.Base.DepartmentExt tempfplist in fpData)
                    {
                        if (this.insertSubDept(tempfplist) == -1)
                        {
                            MessageBox.Show("保存失败！");
                            return -1;
                        }
                    }
                }
                catch
                {
                    return -1;
                }
            }
            MessageBox.Show("保存成功");
            return 1;
        }
        public void Delete()
        {
            FS.HISFC.Models.Base.DepartmentExt deleSubDept = new FS.HISFC.Models.Base.DepartmentExt();
            deleSubDept.Dept.ID = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            deleSubDept.Dept.ID = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            deleSubDept.Dept.Name = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text;
            deleSubDept.PropertyCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text;
            deleSubDept.PropertyName = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Text;
            deleSubDept.StringProperty = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Text;
            deleSubDept.NumberProperty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 5].Text);
            deleSubDept.DateProperty = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 6].Text);
            deleSubDept.Memo = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Text;
            deleSubDept.OperEnvironment.ID = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 8].Text;
            deleSubDept.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 9].Text);
            deleSubDept.User01 = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 10].Text;
            this.deleList.Add(deleSubDept);

            this.neuSpread1_Sheet1.RemoveRows(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
        }
        public void Add()
        {
            this.isNewRow = false;

            this.neuSpread1_Sheet1.RowCount += 1;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = (this.selectedNode.Tag as FS.HISFC.Models.Base.Department).ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = (this.selectedNode.Tag as FS.HISFC.Models.Base.Department).Name;

            this.isNewRow = true;
        }
        public void Clear()
        {

        }
        private ArrayList AddFpToData()
        {
            ArrayList al = new ArrayList();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Base.DepartmentExt departmentExtend = new FS.HISFC.Models.Base.DepartmentExt();
                    departmentExtend.Dept.ID = this.neuSpread1_Sheet1.Cells[i, 0].Text;
                    departmentExtend.Dept.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                    departmentExtend.PropertyCode = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                    departmentExtend.PropertyName = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    departmentExtend.StringProperty = this.neuSpread1_Sheet1.Cells[i, 4].Text;
                    departmentExtend.NumberProperty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 5].Text);
                    departmentExtend.DateProperty = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 6].Text);
                    departmentExtend.Memo = this.neuSpread1_Sheet1.Cells[i, 7].Text;
                    departmentExtend.OperEnvironment.ID = this.neuSpread1_Sheet1.Cells[i, 8].Text;
                    departmentExtend.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, 9].Text);
                    departmentExtend.User01 = this.neuSpread1_Sheet1.Cells[i, 10].Text;
                    //0  科室编码departmentExt.Dept.ID
                    //1 科室名称   departmentExt.Dept.Name 
                    //2 属性编码departmentExt.PropertyCode
                    //3 属性名称departmentExt.PropertyName
                    //4 字符属性 departmentExt.StringProperty  
                    //5 数值属性departmentExt.NumberProperty 
                    //6 日期属性departmentExt.DateProperty
                    //7 备注departmentExt.Memo
                    //8 操作日期departmentExt.OperEnvironment.ID
                    //9 操作时间departmentExt.OperEnvironment.OperTime
                    //科室类型departmentExt.User01 
                    al.Add(departmentExtend);
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            return al;

        }

        public void cmbDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode  == Keys.Enter)
            {
                if (!this.isNewRow)
                {
                    this.Add();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = this.cmbDept.SelectedItem.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = this.cmbDept.SelectedItem.Name;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "0";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "0";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = System.DateTime.Now.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = this.tempdatabase.Operator.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = System.DateTime.Now.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = ((FS.HISFC.Models.Base.Department)(this.cmbDept.SelectedItem)).DeptType.ID.ToString();

                    this.setFpWidth();
                    this.isNewRow = false;
                }
                else
                {

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = this.cmbDept.SelectedItem.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = this.cmbDept.SelectedItem.Name;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "0";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "0";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = System.DateTime.Now.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = this.tempdatabase.Operator.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = System.DateTime.Now.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = ((FS.HISFC.Models.Base.Department)(this.cmbDept.SelectedItem)).DeptType.ID.ToString();

                    this.setFpWidth();
                    this.isNewRow = false;
                }
            }

        }


        public void tvDept_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            this.selectedNode = this.tvDept.SelectedNode;
            this.neuSpread1_Sheet1.RowCount = 0;
            ArrayList list = this.GetDepartmentSubdept((this.selectedNode.Tag as FS.HISFC.Models.Base.Department).ID);
            foreach (FS.HISFC.Models.Base.DepartmentExt templist in list)
            {
                this.neuSpread1_Sheet1.RowCount += 1;
                int rowcount = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.Cells[rowcount, 0].Text = templist.Dept.ID.ToString();
                this.neuSpread1_Sheet1.Cells[rowcount, 1].Text = templist.Dept.Name;
                this.neuSpread1_Sheet1.Cells[rowcount, 2].Text = templist.PropertyCode;
                this.neuSpread1_Sheet1.Cells[rowcount, 3].Text = templist.PropertyName;
                this.neuSpread1_Sheet1.Cells[rowcount, 4].Text = templist.StringProperty;
                this.neuSpread1_Sheet1.Cells[rowcount, 5].Text = templist.NumberProperty.ToString();
                this.neuSpread1_Sheet1.Cells[rowcount, 6].Text = templist.DateProperty.ToString();
                this.neuSpread1_Sheet1.Cells[rowcount, 7].Text = templist.Memo;
                this.neuSpread1_Sheet1.Cells[rowcount, 8].Text = templist.OperEnvironment.ID;
                this.neuSpread1_Sheet1.Cells[rowcount, 9].Text = templist.OperEnvironment.OperTime.ToString();
                this.neuSpread1_Sheet1.Cells[rowcount, 10].Text = templist.User01.ToString();
                //0  科室编码departmentExt.Dept.ID
                //1 科室名称       departmentExt.Dept.Name 
                //2 属性编码departmentExt.PropertyCode
                //3 属性名称departmentExt.PropertyName
                //4 字符属性 departmentExt.StringProperty  
                //5 数值属性departmentExt.NumberProperty 
                //6 日期属性departmentExt.DateProperty
                //7 备注departmentExt.Memo
                //8 操作日期departmentExt.OperEnvironment.ID
                //9 操作时间departmentExt.OperEnvironment.OperTime
                //科室类型departmentExt.User01

            }
        }
        public int deleteSubdept(FS.HISFC.Models.Base.DepartmentExt tempdept)
        {
            string strSQL = "";
            if (this.tempdatabase.Sql.GetSql("Manager.DepartmentExt.DeleteDepartmentSubdept", ref strSQL) == -1)
            {
                return -1;
            }
            string[] param = this.myGetParmDepartmentExt(tempdept);
            try
            {
                strSQL = string.Format(strSQL, param);
            }
            catch
            {
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.tempdatabase.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (this.tempdatabase.ExecNoQuery(strSQL) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;

            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                return 1;
            }
        }
        public int insertSubDept(FS.HISFC.Models.Base.DepartmentExt tempSubDept)
        {
            string strSQL = "";
            if (this.tempdatabase.Sql.GetSql("Manager.DepartmentExt.InsertDepartmentSubdept", ref strSQL) == -1)
            {
                return -1;
            }
            string[] param = this.myGetParmDepartmentExt(tempSubDept);
            try
            {
                strSQL = string.Format(strSQL, param);
            }
            catch
            {
                return -1;
            }
            if (this.tempdatabase.ExecNoQuery(strSQL) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
        }

        private ArrayList GetDepartmentSubdept(string deptcode)
        {
            string strSQL = "";
            if (this.tempdatabase.Sql.GetSql("Manager.DepartmentExt.GetDepartmentSubdept", ref strSQL) == -1)
            {
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptcode);
            }
            catch
            {
                return null;
            }
            return this.myGetDepartmentExt(strSQL);
        }
        public ArrayList GetDepartmentExtList(string propertyCode)
        {
            string strSQL = "";
            //string strWhere = "";
            //取SELECT语句
            if (this.tempdatabase.Sql.GetSql("Manager.DepartmentExt.GetDepartmentExtList", ref strSQL) == -1)
            {
                this.tempdatabase.Err = "没有找到Manager.DepartmentExt.GetDepartmentExtList字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, propertyCode);
            }
            catch (Exception ex)
            {
                this.tempdatabase.Err = "格式化SQL语句时出错Manager.DepartmentExt.GetDepartmentExtList:" + ex.Message;
                return null;
            }

            //取科室属性数据
            return this.myGetDepartmentExt(strSQL);
        }

        private ArrayList myGetDepartmentExt(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.DepartmentExt departmentExt; //科室属性信息实体


            //执行查询语句
            if (this.tempdatabase.ExecQuery(SQLString) == -1)
            {
                this.tempdatabase.Err = "获得科室属性信息时，执行SQL语句出错！" + this.tempdatabase.Err;
                this.tempdatabase.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.tempdatabase.Reader.Read())
                {
                    //取查询结果中的记录
                    departmentExt = new FS.HISFC.Models.Base.DepartmentExt();
                    departmentExt.Dept.ID = this.tempdatabase.Reader[0].ToString();          //0 科室编码
                    departmentExt.Dept.Name = this.tempdatabase.Reader[1].ToString();          //1 科室名称
                    departmentExt.PropertyCode = this.tempdatabase.Reader[2].ToString();     //2 属性编码
                    departmentExt.PropertyName = this.tempdatabase.Reader[3].ToString();     //3 属性名称
                    departmentExt.StringProperty = this.tempdatabase.Reader[4].ToString();     //4 字符属性 
                    departmentExt.NumberProperty = FS.FrameWork.Function.NConvert.ToDecimal(this.tempdatabase.Reader[5].ToString()); //5 数值属性
                    departmentExt.DateProperty = FS.FrameWork.Function.NConvert.ToDateTime(this.tempdatabase.Reader[6].ToString());//6 日期属性
                    departmentExt.Memo = this.tempdatabase.Reader[7].ToString();          //7 备注
                    departmentExt.OperEnvironment.ID = this.tempdatabase.Reader[8].ToString();          //8 操作日期
                    departmentExt.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.tempdatabase.Reader[9].ToString());     //9 操作时间
                    departmentExt.User01 = this.tempdatabase.Reader[10].ToString();         //科室类型

                    al.Add(departmentExt);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.tempdatabase.Err = "获得科室属性信息时出错！" + ex.Message;
                this.tempdatabase.ErrCode = "-1";
                return null;
            }
            this.tempdatabase.Reader.Close();

            return al;
        }

        /// <summary>
        /// 获得update或者insert科室属性表的传入参数数组
        /// </summary>
        /// <param name="departmentExt">科室属性类</param>
        /// <returns>字符串数组</returns>
        private string[] myGetParmDepartmentExt(FS.HISFC.Models.Base.DepartmentExt departmentExt)
        {

            string[] strParm ={   
								 departmentExt.Dept.ID,                  //0 科室编码
                                 departmentExt .Dept .Name ,             //1 科室名称
								 departmentExt.PropertyCode.ToString(),  //2 属性编码
								 departmentExt.PropertyName.ToString(),  //3 属性名称
								 departmentExt.StringProperty.ToString(),//4 字符属性 
								 departmentExt.NumberProperty.ToString(),//5 数值属性
								 departmentExt.DateProperty.ToString(),  //6 日期属性
								 departmentExt.Memo.ToString(),          //7 备注
								 //this.tempdatabase .Operator .ID ,
                                 departmentExt .OperEnvironment.ID.ToString () ,        //7 操作员编码
                                 departmentExt .OperEnvironment .OperTime.ToString () ,//9操作时间
                                 departmentExt .User01                      //10 科室类型
                                  
			};
            return strParm;
        }
    }
}
