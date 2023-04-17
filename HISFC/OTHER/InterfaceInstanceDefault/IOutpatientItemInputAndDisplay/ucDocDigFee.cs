using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace InterfaceInstanceDefault.IOutpatientItemInputAndDisplay
{
    /// <summary>
    /// 医生职级与挂号级别、诊查费对照
    /// </summary>
    public partial class ucDocDigFee : UserControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucDocDigFee()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 医生职级
        /// </summary>
        private string constType_DoctLvl = "LEVEL";

        /// <summary>
        /// 医生职级帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_DoctLvl = null;

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// 人员帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper empHelper = null;

        /// <summary>
        /// 挂号级别帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_RegLvl = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 非药品项目帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_DiagFee = null;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 挂号综合管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        #region SQL

        /// <summary>
        /// 查询语句
        /// </summary>
        string sql_Select = @"select dept_code,--科室
                                   levl_code,--职级
                                   reglevl_code,--挂号级别
                                   item_code,--诊查费（挂号费）项目
                                   fee_type,--类别：0 挂号费、1 诊查费
                                   valid_flag,
                                   oper_code,
                                   oper_date
                            from fin_com_regfeeset
                            where (dept_code='{0}' or '{0}'='ALL')
                            and (levl_code='{1}' or '{1}'='ALL')
                            and (reglevl_code='{2}' or '{2}'='ALL')
                            and (fee_type='{3}' or '{3}'='ALL')";

        /// <summary>
        /// 插入语句
        /// </summary>
        string sql_Insert = @"insert into fin_com_regfeeset
                                       (dept_code,
                                       levl_code,
                                       reglevl_code,
                                       item_code,
                                       fee_type,
                                       valid_flag,
                                       oper_code,
                                       oper_date)
                                values 
                                       ('{0}',
                                       '{1}',
                                       '{2}',
                                       '{3}',
                                       '{4}',
                                       '{5}',
                                       '{6}',
                                       sysdate
                                       )
                                ";

        /// <summary>
        /// 全部删除语句
        /// </summary>
        string sql_Delete = @"delete from fin_com_regfeeset";

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 初始化列表
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

            #region 初始化列表

            FS.FrameWork.Models.NeuObject allObj = new NeuObject("ALL", "全部", "全部");

            //科室列表
            ArrayList alDept = interManager.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(interManager.Err);
                return -1;
            }
            alDept.Insert(0, allObj);
            deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            ArrayList alEmpl = interManager.QueryEmployeeAll();

            alEmpl.Insert(0, allObj);
            empHelper = new FS.FrameWork.Public.ObjectHelper(alEmpl);

            //医生职级
            ArrayList alConst_DoctLvl = consManager.GetList(this.constType_DoctLvl);
            if (alConst_DoctLvl == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("加载常数[" + constType_DoctLvl + "]列表发生错误！"));
                return -1;
            }
            alConst_DoctLvl.Insert(0, allObj);
            this.constHelper_DoctLvl = new FS.FrameWork.Public.ObjectHelper(alConst_DoctLvl);

            //挂号级别
            ArrayList alConst_RegLvl = this.regIntegrate.QueryRegLevel();

            if (alConst_RegLvl == null)
            {
                MessageBox.Show("查询挂号级别出错：" + regIntegrate.Err);
                return -1;
            }
            alConst_RegLvl.Insert(0, allObj);
            this.constHelper_RegLvl.ArrayObject = alConst_RegLvl;

            //非药品项目
            ArrayList alConst_DiagFee = this.feeIntegrate.QueryValidItems();
            if (alConst_DiagFee == null)
            {
                MessageBox.Show("查询收费项目出错：" + this.feeIntegrate.Err);
                return -1;
            }
            constHelper_DiagFee = new FS.FrameWork.Public.ObjectHelper(alConst_DiagFee);

            FarPoint.Win.Spread.CellType.ComboBoxCellType comCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            //费用类别 0 挂号费 1 诊查费
            string[] arrayTemp = new string[2] { "挂号费", "诊查费" };
            comCellType1.Items = arrayTemp;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.S收费类别].CellType = comCellType1;

            #endregion

            this.neuSpread1_Sheet1.Columns[(int)ColmSet.K科室编码].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.Z职级编码].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.G挂号级别编码].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.F费用编码].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.C操作员编码].Visible = false;

            FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.K科室编码].CellType = textType;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.Z职级编码].CellType = textType;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.G挂号级别编码].CellType = textType;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.F费用编码].CellType = textType;
            this.neuSpread1_Sheet1.Columns[(int)ColmSet.C操作员编码].CellType = textType;


            return this.Query();
        }

        /// <summary>
        /// 双击弹出选择项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            if (e.Column == (int)ColmSet.K科室名称)
            {
                this.PopItem(deptHelper.ArrayObject, (int)ColmSet.K科室名称);
            }
            if (e.Column == (int)ColmSet.Z职级名称)
            {
                this.PopItem(this.constHelper_DoctLvl.ArrayObject, (int)ColmSet.Z职级名称);
            }
            else if (e.Column == (int)ColmSet.G挂号级别名称)
            {
                this.PopItem(this.constHelper_RegLvl.ArrayObject, (int)ColmSet.G挂号级别名称);
            }
            else if (e.Column == (int)ColmSet.S收费项目)
            {
                this.PopItem(this.constHelper_DiagFee.ArrayObject, (int)ColmSet.S收费项目);
            }
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al,int index)
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                if (index == (int)ColmSet.K科室名称)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.K科室编码].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.K科室名称].Value = info.Name;
                }
                //医生职级
                else if (index == (int)ColmSet.Z职级名称)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.Z职级编码].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.Z职级名称].Value = info.Name;
                }
                //挂号级别
                else if (index == (int)ColmSet.G挂号级别名称)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.G挂号级别编码].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.G挂号级别名称].Value = info.Name;
                }
                //收费项目
                else if (index == (int)ColmSet.S收费项目)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.F费用编码].Value = info.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.S收费项目].Value = info.Name;

                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColmSet.K科室名称)
                    {
                        this.PopItem(this.deptHelper.ArrayObject, (int)ColmSet.K科室名称);
                    }
                    else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColmSet.Z职级名称)
                    {
                        this.PopItem(this.constHelper_DoctLvl.ArrayObject, (int)ColmSet.Z职级名称);
                    }
                    else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColmSet.G挂号级别名称)
                    {
                        this.PopItem(this.constHelper_RegLvl.ArrayObject, (int)ColmSet.G挂号级别名称);
                    }
                    else if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColmSet.S收费项目)
                    {
                        this.PopItem(this.constHelper_DiagFee.ArrayObject, (int)ColmSet.S收费项目);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            DataSet ds = new DataSet();
            if (this.consManager.ExecQuery(string.Format(sql_Select, "ALL", "ALL", "ALL", "ALL"), ref ds) == -1)
            {
                MessageBox.Show(consManager.Err);
                return -1;
            }

            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                //科室编码
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.K科室编码].Text = dRow[0].ToString();
                //科室名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.K科室名称].Text = this.deptHelper.GetName(dRow[0].ToString());
                //职级编码
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.Z职级编码].Text = dRow[1].ToString();
                //职级名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.Z职级名称].Text = this.constHelper_DoctLvl.GetName(dRow[1].ToString());
                //挂号级别编码
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.G挂号级别编码].Text = dRow[2].ToString();
                //挂号级别名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.G挂号级别名称].Text = this.constHelper_RegLvl.GetName(dRow[2].ToString());
                //诊查费编码
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.F费用编码].Text = dRow[3].ToString();
                //诊查费名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.S收费项目].Text = constHelper_DiagFee.GetName(dRow[3].ToString());

                //费用类别
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.S收费类别].Text = (this.neuSpread1_Sheet1.Columns[8].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[FS.FrameWork.Function.NConvert.ToInt32(dRow[4])];

                //是否有效
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.Y有效].Value = dRow[5].ToString() == "1" ? "True" : "False";

                //操作员
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.C操作员编码].Text = dRow[6].ToString();
                //操作员
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.C操作员].Text = empHelper.GetName(dRow[6].ToString());
                //操作日期
                this.neuSpread1_Sheet1.Cells[0, (int)ColmSet.C操作日期].Text = FS.FrameWork.Function.NConvert.ToDateTime(dRow[7]).ToString();
            }

            return 1;
        }

        /// <summary>
        /// 获取现在的ID
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSelectData(int row, int column)
        {
            for (int j = 0; j < (this.neuSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items.Length; j++)
            {
                string item = (this.neuSpread1_Sheet1.Columns[column].CellType as FarPoint.Win.Spread.CellType.ComboBoxCellType).Items[j];

                if (item == this.neuSpread1_Sheet1.Cells[row, column].Text)
                {
                    return j.ToString();
                }
            }
            return "0";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            Hashtable hsCheck = new Hashtable();

            string execSQL = "";


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (consManager.ExecNoQuery(sql_Delete) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(consManager.Err);
                return -1;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                string dept = neuSpread1_Sheet1.Cells[i, (int)ColmSet.K科室编码].Value.ToString();
                string levlCode = neuSpread1_Sheet1.Cells[i, (int)ColmSet.Z职级编码].Value.ToString();
                string regLevlCode = neuSpread1_Sheet1.Cells[i, (int)ColmSet.G挂号级别编码].Value.ToString();
                string itemCode = neuSpread1_Sheet1.Cells[i, (int)ColmSet.F费用编码].Value.ToString();
                string typeCode = this.GetSelectData(i, (int)ColmSet.S收费类别);
                string valideFlag = FS.FrameWork.Function.NConvert.ToBoolean(neuSpread1_Sheet1.Cells[i, (int)ColmSet.Y有效].Value) ? "1" : "0";
                string operCode = consManager.Operator.ID;

                if (!hsCheck.Contains(dept + levlCode + regLevlCode + itemCode + typeCode))
                {
                    hsCheck.Add(dept + levlCode + regLevlCode + itemCode + typeCode, null);
                }
                else
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行数据重复！");
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }

                execSQL = string.Format(sql_Insert, dept, levlCode, regLevlCode, itemCode, typeCode, valideFlag, operCode);

                if (consManager.ExecNoQuery(execSQL) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(consManager.Err);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));
            return 0;
        }

        #endregion

        #region IMaintenanceControlable 成员 无用

        public int Add()
        {
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.Rows.Count, 1);
            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColmSet.K科室名称;

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.Y有效].Value = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.C操作员编码].Value = consManager.Operator.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.C操作员].Value = consManager.Operator.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColmSet.C操作日期].Value = this.consManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            return 0;
        }

        public int Copy()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int Cut()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int Delete()
        {
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);
            return 1;
            throw new NotImplementedException();
        }

        public int Export()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int Import()
        {
            return 1;
            throw new NotImplementedException();
        }

        public bool IsDirty
        {
            get
            {
                return true;
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Modify()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int NextRow()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int Paste()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int PreRow()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int Print()
        {
            return 1;
            throw new NotImplementedException();
        }

        public int PrintConfig()
        {
            throw new NotImplementedException();
        }

        public int PrintPreview()
        {
            return 1;
            throw new NotImplementedException();
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return null;
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        enum ColmSet
        {
            K科室编码,
            K科室名称,
            Z职级编码,
            Z职级名称,
            G挂号级别编码,
            G挂号级别名称,
            F费用编码,
            S收费项目,
            S收费类别,
            J急诊,
            Y有效,
            C操作员编码,
            C操作员,
            C操作日期
        }
    }
}
