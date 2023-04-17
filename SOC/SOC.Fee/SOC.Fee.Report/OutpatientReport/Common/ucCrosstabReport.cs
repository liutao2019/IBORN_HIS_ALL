using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.Report.OutpatientReport.Common
{
    /// <summary>
    /// ucCrosstabReport<br></br>
    /// <Font color='#FF1111'>[功能描述: 通过DataSet实现交叉报表]</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2009-4-13]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public partial class ucCrosstabReport : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucCrosstabReport()
        {
            InitializeComponent();
        }
        #endregion 构造函数

        #region 变量

        #region 私有变量

        /// <summary>
        /// 是否使用科室查询条件
        /// </summary>
        private bool isHaveDept = false;

        /// <summary>
        /// 科室类型
        /// </summary>
        //private Neusoft.HISFC.Object.Base.EnumDepartmentType deptType;

        /// <summary>
        /// 三级权限代码，通过权限加载科室，为空时加载所有科室
        /// </summary>
        private string privClass3Code = "";

        /// <summary>
        /// 标题
        /// </summary>
        private string title = "";

        /// <summary>
        /// sql语句id
        /// </summary>
        private string sqlId = "";

        /// <summary>
        /// sql语句id2
        /// 用于按科室查询
        /// </summary>
        private string sqlId2 = "";

        /// <summary>
        /// 是否包含合计行
        /// </summary>
        private bool haveSum = true;

        /// <summary>
        /// 是否包含合计列
        /// </summary>
        private bool haveRowSum = true;

        /// <summary>
        /// 是否将空值转为零
        /// </summary>
        private bool replaceNullToZero = true;
        /// <summary>
        /// 设置查询科室
        /// </summary>
        private string queryDeptcode = "";

        /// <summary>
        /// 时间格式
        /// </summary>
        private string dataTiemFromat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 默认查询的时间范围：天
        /// </summary>
        private int queryDays = 1;
        /// <summary>
        /// 冻结列数
        /// </summary>
        private int frozen = 0;

        #endregion 私有变量

        #region 保护变量
        #endregion 保护变量

        #region 公开变量

        #endregion 公开变量

        #endregion 变量

        #region 属性

        /// <summary>
        /// 是否使用科室查询条件
        /// </summary>
        [Category("查询设置"), Description("是否添加科室查询条件,true添加,false不添加"), DefaultValue("false")]
        public bool IsHaveDept
        {
            get
            {
                return isHaveDept;
            }
            set
            {
                isHaveDept = value;
                this.lbDept.Visible = value;
                this.cmbDept.Visible = value;
            }
        }

        /// <summary>
        /// 科室类型
        /// </summary>
        //[Category("查询设置"), Description("科室类型")]
        //public Neusoft.HISFC.Object.Base.EnumDepartmentType DeptType
        //{
        //    get
        //    {
        //        return deptType;
        //    }
        //    set
        //    {
        //        deptType = value;
        //    }
        //}

        /// <summary>
        /// 三级权限代码，通过权限加载科室，为空时加载所有科室
        /// </summary>
        [Category("查询设置"), Description("三级权限代码，通过权限加载科室，为空时加载所有科室")]
        public string PrivClass3Code
        {
            get
            {
                return privClass3Code;
            }
            set
            {
                privClass3Code = value;
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        [Category("查询设置"), Description("标题")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// sql语句id
        /// </summary>
        [Category("查询设置"), Description("sql语句id")]
        public string SqlId
        {
            get
            {
                return sqlId;
            }
            set
            {
                sqlId = value;
            }
        }

        /// <summary>
        /// sql语句id
        /// </summary>
        [Category("报表冻结设置"), Description("报表是否冻结")]
        public int Frozen
        {
            get
            {
                return frozen;
            }
            set
            {
                frozen = value;
            }
        }
        /// <summary>
        /// 是否添加合计
        /// </summary>
        [Category("查询设置"), Description("是否添加合计,true添加,false不添加"), DefaultValue("true")]
        public bool HaveSum
        {
            get
            {
                return haveSum;
            }
            set
            {
                haveSum = value;
            }
        }

        /// <summary>
        /// 是否包含合计列
        /// </summary>
        [Category("查询设置"), Description("是否添加行合计,true添加,false不添加"), DefaultValue("true")]
        public bool HaveRowSum
        {
            get
            {
                return haveRowSum;
            }
            set
            {
                haveRowSum = value;
            }
        }

        /// <summary>
        /// 是否将空值转为零
        /// </summary>
        [Category("查询设置"), Description("是否将空值转为零,true转换,false不转换"), DefaultValue("true")]
        public bool ReplaceNullToZero
        {
            get
            {
                return replaceNullToZero;
            }
            set
            {
                replaceNullToZero = value;
            }
        }

        /// <summary>
        /// 时间格式
        /// </summary>
        [Category("查询设置"), Description("时间格式，默认：yyyy-MM-dd HH:mm:ss"), DefaultValue("yyyy-MM-dd HH:mm:ss")]
        public string DataTiemFromat
        {
            get
            {
                return dataTiemFromat;
            }
            set
            {
                dataTiemFromat = value;
            }
        }

        /// <summary>
        /// 默认查询的时间范围：天
        /// </summary>
        [Category("查询设置"), Description("默认查询的时间范围，单位：天，默认1天"), DefaultValue("1")]
        public int QueryDays
        {
            get
            {
                return queryDays;
            }
            set
            {
                queryDays = value;
            }
        }

        private string dataFromat = "";

        [Category("查询设置"), Description("数据显示格式"), DefaultValue("")]
        public string DataFromat
        {
            get
            {
                return dataFromat;
            }
            set
            {
                dataFromat = value;
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        [Category("设置查询科室"), Description("设置查询科室")]
        public string QueryDeptcode
        {
            get
            {
                return queryDeptcode; ;
            }
            set
            {
                queryDeptcode = value;
            }
        }
        #endregion 属性

        #region 方法

        #region 资源释放
        #endregion 资源释放

        #region 克隆
        #endregion 克隆

        #region 私有方法

        private void Init()
        {
            //科室查询条件
            if (this.isHaveDept)
            {
                if (string.IsNullOrEmpty(this.privClass3Code.Trim()))
                {
                   Neusoft.HISFC.BizLogic.Manager.Department interMgr = new Neusoft.HISFC.BizLogic.Manager.Department();
                    ArrayList alDept = interMgr.GetDeptmentAll();
                    this.cmbDept.AddItems(alDept);
                }
                else
                {
                    Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager managerIntegrate = new Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager();
                    List<   Neusoft.FrameWork.Models.NeuObject> alPrivDept = managerIntegrate.QueryUserPriv(Neusoft.FrameWork.Management.Connection.Operator.ID, this.privClass3Code.Trim());
                    if (alPrivDept != null)
                    {
                        this.cmbDept.AddItems(new ArrayList(alPrivDept.ToArray()));
                    }
                }
            }
            this.lbDept.Visible = this.isHaveDept;
            this.cmbDept.Visible = this.isHaveDept;
            this.cmbDept.Tag = this.queryDeptcode;
            //表头
            this.lbTitle.Text = this.title;
            //hn 排序
            this.neuSpread1_Sheet1.SetColumnAllowAutoSort(1, true);
            //时间格式
            this.dtpFromDate.CustomFormat = this.dataTiemFromat;
            this.dtpEndDate.CustomFormat = this.dataTiemFromat;
            this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, this.dtpFromDate.Value.Month, this.dtpFromDate.Value.Day, 0, 0, 0);
            this.dtpEndDate.Value = new DateTime(this.dtpEndDate.Value.Year, this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59);
            this.dtpFromDate.Value = this.dtpEndDate.Value.AddDays(-this.queryDays).AddSeconds(1);
            //添加初始时间
            if (this.queryDays < 1)
            {
                this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, dtpFromDate.Value.Month, 01);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            try
            {
                   Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请稍等");
                Application.DoEvents();
                //执行sql语句，获取DataTable
                DataTable dt = this.GetDataTableBySql();
                if (dt == null)
                {
                       Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //根据sql查询结果的DataTable生成交叉表的DataTable
                DataTable dtCross = this.GetCrossDataTable(dt);
                if (dtCross == null)
                {
                       Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                if (dt.Columns.Count != 4)
                {
                    //添加合计
                    this.ComputeSum(dtCross);
                }
                else
                {
                    // 右医按照医生统计查询
                    this.ComputeSum2(dtCross);
                }
                //将空值转为零
                this.ConverNullToZero(dtCross);
                this.ConverDataFromat(dtCross);
                //FarPoint赋值，设置格式
                this.SetFp(dtCross);
                   Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                   Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询数据发生错误：" + ex.Message);
            }
        }

        private void ConverDataFromat(DataTable dtCross)
        {
            if (this.dataFromat.Trim() == "")
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                        {
                        }
                        else
                        {
                            dtCross.Rows[i][j] = Neusoft.FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()).ToString(this.dataFromat);//System.Math.Round(Neusoft.FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()), 3);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 执行sql语句，获取DataTable
        /// </summary>
        private DataTable GetDataTableBySql()
        {
            //执行sql语句
            if (string.IsNullOrEmpty(this.sqlId.Trim()))
            {
                return null;
            }
            if (this.dtpFromDate.Value > this.dtpEndDate.Value)
            {
                MessageBox.Show("开始时间不能大于截止时间，请重新输入");
                return null;
            }
            Neusoft.HISFC.BizLogic.Manager.Report reportMgr = new Neusoft.HISFC.BizLogic.Manager.Report();

            string[] parm = null;
            if (this.isHaveDept)
            {
                parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.cmbDept.Tag.ToString() };
            }
            else
            {
                parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") };
            }

            DataSet ds = new DataSet();
            if (reportMgr.ExecQuery(this.sqlId, ref ds, parm) < 0)
            {
                MessageBox.Show("查询数据出错：" + reportMgr.Err);
                return null;
            }
            return ds.Tables[0];
        }

        int intColumns = 0;

        /// <summary>
        /// 根据sql查询结果的DataTable生成交叉表表的DataTable
        /// </summary>
        /// <param name="dt">原DataTable</param>
        /// <returns>CrossDataTable</returns>
        private DataTable GetCrossDataTable(DataTable dt)
        {
            this.intColumns = dt.Columns.Count;
            if (dt.Columns.Count < 3)
            {
                MessageBox.Show("sql语句错误：交叉报表数据列不能少于3列");
                return null;
            }
            else if (dt.Columns.Count == 4)
            {
                DataTable dtCross = new DataTable();
                //添加列
                //第一列、名字为空
                dtCross.Columns.Add(new DataColumn("医生"));
                //第二列、科室名称
                dtCross.Columns.Add(new DataColumn("科室"));
                //第三列、添加合计
                dtCross.Columns.Add(new DataColumn("合计"));
                //第三列、添加合计
                //dtCross.Columns.Add(new DataColumn("药品比"));
                foreach (DataRow drCol in dt.Rows)
                {
                    string colName = drCol[2].ToString();
                    if (dtCross.Columns.Contains(colName))
                    {
                        continue;
                    }
                    dtCross.Columns.Add(colName);
                }

                //添加合计行
                DataRow drSum = dtCross.NewRow();
                dtCross.Rows.Add(drSum);
                drSum[1] = "合计:";


                //添加数据
                Hashtable htRow = new Hashtable();

                foreach (DataRow drRow in dt.Rows)
                {
                    string rowName = drRow[0].ToString();
                    string deptName = drRow[1].ToString();
                    string key = rowName + "|" + deptName;
                    if (htRow.ContainsKey(key))
                    {
                        DataRow drAdded = htRow[key] as DataRow;
                        drAdded[drRow[2].ToString()] = (Neusoft.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[2].ToString()].ToString())
                                                                                + Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[3].ToString())).ToString("#.####");
                    }
                    else
                    {
                        DataRow drNew = dtCross.NewRow();
                        htRow.Add(key, drNew);
                        dtCross.Rows.Add(drNew);
                        drNew[0] = rowName;
                        drNew[1] = drRow[1].ToString();
                        drNew[drRow[2].ToString()] = Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[3].ToString()).ToString("#.####");
                    }
                }
                return dtCross;
            }
            else
            {
                DataTable dtCross = new DataTable();
                //添加列
                //第一列、名字为空
                dtCross.Columns.Add(new DataColumn(" "));
                //第二列、添加合计
                dtCross.Columns.Add(new DataColumn("合计"));
                //第三列、添加比
                //dtCross.Columns.Add(new DataColumn("药品比"));
                foreach (DataRow drCol in dt.Rows)
                {
                    string colName = drCol[1].ToString();
                    if (dtCross.Columns.Contains(colName))
                    {
                        continue;
                    }
                    dtCross.Columns.Add(colName);
                }
                //添加数据
                Hashtable htRow = new Hashtable();

                foreach (DataRow drRow in dt.Rows)
                {
                    string rowName = drRow[0].ToString();

                    if (htRow.ContainsKey(rowName))
                    {
                        DataRow drAdded = htRow[rowName] as DataRow;
                        drAdded[drRow[1].ToString()] = (Neusoft.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[1].ToString()].ToString())
                                                                                + Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString())).ToString("#.####");
                    }
                    else
                    {
                        DataRow drNew = dtCross.NewRow();
                        htRow.Add(rowName, drNew);
                        dtCross.Rows.Add(drNew);
                        drNew[0] = rowName;
                        drNew[drRow[1].ToString()] = Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString()).ToString("#.####");
                    }
                }
                return dtCross;
            }
        }

        /// <summary>
        /// 添加合计
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void ComputeSum(DataTable dtCross)
        {
            //列合计
            if (this.haveSum)
            {
                DataRow drSum = dtCross.NewRow();
                dtCross.Rows.Add(drSum);
                drSum[0] = "合计:";
                for (int i = 1; i < dtCross.Columns.Count; i++)
                {
                    DataColumn dc = dtCross.Columns[i];
                    decimal sum = 0;
                    foreach (DataRow dr in dtCross.Rows)
                    {
                        sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                    }
                    drSum[dc.ColumnName] = Math.Round(sum, 2); ;//sum.ToString("#.####");
                }
            }
            //行合计
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("合计");
                //dtCross.Columns.Add(dcSum);
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = 0;
                    for (int j = 1; j < dtCross.Columns.Count; j++)
                    {
                        rowSum += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                    }
                    drR["合计"] = Math.Round(rowSum,2);//rowSum.ToString("#.####");
                }

            }
            //行比率
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("合计");
                //dtCross.Columns.Add(dcSum);
                decimal Rat = 0m;
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = Neusoft.FrameWork.Function.NConvert.ToDecimal(drR["合计"]);
                    decimal rowSum1 = 0;
                    for (int j = 1; j < dtCross.Columns.Count; j++)
                    {
                        if (dtCross.Columns[j].ColumnName.ToString() == "西药" || dtCross.Columns[j].ColumnName.ToString() == "中草药" ||
                            dtCross.Columns[j].ColumnName.ToString() == "中成药")
                        {
                            rowSum1 += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                        }
                        else { continue; }

                    }
                    if (rowSum > 0)
                    {
                        Rat = Math.Round(rowSum1 / rowSum, 4) * 100;
                    }
                    //drR["药品比"] = Rat.ToString("#.####");//rowSum.ToString("#.####");
                }

            }
        }

        /// <summary>
        /// 右医按照医生统计添加行合计
        /// </summary>
        /// <param name="dtCross"></param>
        private void ComputeSum2(DataTable dtCross)
        {
            //列合计
            if (this.haveSum)
            {
                DataRow drSum = dtCross.Rows[0];
                for (int i = 2; i < dtCross.Columns.Count; i++)
                {
                    DataColumn dc = dtCross.Columns[i];
                    decimal sum = 0;
                    foreach (DataRow dr in dtCross.Rows)
                    {
                        sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                    }
                    drSum[dc.ColumnName] = sum.ToString("#.####");
                }
            }
            //行合计
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("合计");
                //dtCross.Columns.Add(dcSum);
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = 0;
                    for (int j = 2; j < dtCross.Columns.Count; j++)
                    {
                        rowSum += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                    }
                    drR["合计"] = rowSum.ToString("#.####");
                }

            }
        }

        /// <summary>
        /// FarPoint赋值，设置格式
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void SetFp(DataTable dtCross)
        {
            Neusoft.HISFC.BizLogic.Manager.Constant Constanst=new Neusoft.HISFC.BizLogic.Manager.Constant();
            string OperDept = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.Name;
            string OperId = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).ID;
            //查询信息赋值
            this.lbQueryInfo.Text = "时间范围：" + this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + " 至 " + this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (isHaveDept)
            {
                this.lbQueryInfo.Text += "  查询科室：" + this.cmbDept.Text;
            }
            //数据源
            this.neuSpread1_Sheet1.DataSource = dtCross;
            //bool Operpriv = Constanst.ReturnPriv(OperId);
            //hn新加的。除管理员去掉其它科室信息
            //for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            //{
            //    if (this.neuSpread1_Sheet1.Cells[i, 0].Text != OperDept && (Operpriv == false) && (IsHaveDept == false))
            //    {

            //        this.neuSpread1_Sheet1.Rows[i].Visible = false;
            //    }
            //}
            //宽度
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                
                this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth() + 8;
            }
            //表头位置
            //int windowX = this.Width;
            decimal spreadWith = 0;
            foreach (FarPoint.Win.Spread.Column fpCol in this.neuSpread1_Sheet1.Columns)
            {
                spreadWith += (decimal)fpCol.Width;
            }
            if (spreadWith > this.plPrint.Width)
            {
                spreadWith = this.plPrint.Width;
            }

            int titleWidth = this.lbTitle.Size.Width;
            int titleX = Neusoft.FrameWork.Function.NConvert.ToInt32((spreadWith - titleWidth) / 2);
            if (titleX <= 0)
            {
                titleX = 1;
            }
            this.lbTitle.Location = new Point(titleX, this.lbTitle.Location.Y);
            //Fp冻结列数
            if (this.Frozen > 0)
            {
                this.neuSpread1_Sheet1.FrozenColumnCount = frozen;
            }

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                if (i < this.intColumns - 2)
                {
                    continue;
                }
                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
                {
                    this.neuSpread1_Sheet1.Cells[j, i].CellType = numberCellType1;
                }
            }
        }

        /// <summary>
        /// 将空值转为零
        /// </summary>
        /// <param name="dtCross"></param>
        private void ConverNullToZero(DataTable dtCross)
        {
            if (!this.replaceNullToZero)
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                    {
                        dtCross.Rows[i][j] = "0";
                    }
                }
            }
        }

        #endregion 私有方法

        #region 保护方法
        #endregion 保护方法

        #region 公开方法
        #endregion 公开方法

        #endregion 方法

        #region 事件

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCrosstabReport_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 1;
            }
               Neusoft.FrameWork.WinForms.Classes.Print print = new    Neusoft.FrameWork.WinForms.Classes.Print();
            try
            {
                print.IsLandScape = this.isLandScape;
            }
            catch
            {
            }

            // print.ShowPageSetup();
            //print.SetPageSize(new Neusoft.HISFC.Object.Base.PageSize("asdf", 1145, 800));
            //System.Drawing.Printing.PaperKind paperKind = System.Drawing.Printing.PaperKind.
            //print.SetPageSize(System.Drawing.Printing.PaperKind.Custom);
            //print.PrintPreview(0, 0, this.plPrint);
           
            print.PrintPage(0, 0, this.plPrint);

            return 1;
        }

        bool isLandScape = false;

        public bool IsLandScape
        {
            get
            {
                return isLandScape;
            }
            set
            {
                isLandScape = value;

            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show("导出成功");
            }

            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
               Neusoft.FrameWork.WinForms.Classes.Print print = new    Neusoft.FrameWork.WinForms.Classes.Print();
            try
            {
                print.IsLandScape = this.isLandScape;
            }
            catch
            {
            }
            print.PrintPreview(0, 0, this.plPrint);

            return 1;
        }

        #endregion 事件

        #region 接口实现
        #endregion 接口实现

    }
}