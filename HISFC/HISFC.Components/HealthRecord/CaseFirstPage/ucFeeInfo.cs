using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucFeeInfo<br></br>
    /// [功能描述: 病案费用信息录入]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-04-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucFeeInfo : UserControl
    {
        public ucFeeInfo()
        {
            InitializeComponent();
        }
        #region  变量
        private DataTable dtfeeInfo = new DataTable("诊断");
        public ArrayList feeInfoList = null;
        //金额列是否锁定的标志位 
        private bool boolType = false;
        //保存病人信息
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        #endregion
        /// <summary>
        /// 病人信息
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }
        #region 金额列是否可修改
        /// <summary>
        /// 设定金额是否可修改 
        /// </summary>
        public bool BoolType
        {
            get
            {
                return boolType;
            }
            set
            {
                boolType = value;
            }
        }
        #endregion

        #region  函数
        /// <summary>
        /// 限定格的宽度很可见性 
        /// </summary>
        private void SetFpEnter()
        {
            this.fpSpread1_Sheet1.Columns[0].Visible = false; //统计编码
            this.fpSpread1_Sheet1.Columns[1].Width = 129;//费用名称
            this.fpSpread1_Sheet1.Columns[1].Locked = true;
            this.fpSpread1_Sheet1.Columns[2].Width = 80;//费用金额
            this.fpSpread1_Sheet1.Columns[2].Locked = !boolType;
        }
        /// <summary>
        /// 清空原有的数据
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtfeeInfo != null)
            {
                this.dtfeeInfo.Clear();
                SetFpEnter();
            }
            else
            {
                MessageBox.Show("费用表为null");
            }
            return 1;
        }
        /// <summary>
        /// 校验数据的合法性。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.RADT.Patient obj in list)
            {
                if (obj.ID == null || obj.ID == "")
                {
                    MessageBox.Show("费用信息 住院流水号不能为空");
                    return -1;
                }
                if (obj.ID.Length > 14)
                {
                    MessageBox.Show("费用信息 住院流水号过长");
                    return -1;
                }
                if (obj.DIST == null || obj.DIST == "")
                {
                    MessageBox.Show("费用信息 统计代码不能为空");
                    return -1;
                }
                if (obj.DIST.Length > 3)
                {
                    MessageBox.Show("费用信息 统计代码过长");
                    return -1;
                }
                if (obj.AreaCode == null || obj.AreaCode == "")
                {
                    MessageBox.Show("费用信息 统计名称不能为空");
                    return -1;
                }
                if (obj.AreaCode.Length > 16)
                {
                    MessageBox.Show("费用信息 统计名称过长");
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToDecimal(obj.IDCard) > (decimal)99999999.99)
                {
                    MessageBox.Show("费用信息 金额过大");
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// 获取费用信息
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFeeInfoList()
        {
            feeInfoList = null;
            if (dtfeeInfo != null)
            {
                foreach (DataRow dr in this.dtfeeInfo.Rows)
                {
                    dr.EndEdit();
                }

                dtfeeInfo.AcceptChanges();//
                feeInfoList = new ArrayList();
                FS.HISFC.Models.RADT.Patient info = null;
                foreach (DataRow row in dtfeeInfo.Rows)
                {
                    info = new FS.HISFC.Models.RADT.Patient();
                    info.ID = patientInfo.ID;
                    info.DIST = row["统计编码"].ToString();//统计大类编码
                    if (info.DIST == "" || info.DIST == null)
                    {
                        continue;
                    }
                    info.AreaCode = row["费用名称"].ToString(); //统计名称 
                    if (row["费用金额"] != DBNull.Value)
                    {
                        info.IDCard = row["费用金额"].ToString();//统计费用 
                    }
                    feeInfoList.Add(info);
                }
            }
            return feeInfoList;
        }
        /// <summary>
        /// 查询并显示数据
        /// </summary>
        /// <returns>出错返回 －1 正常 0 不允许有病案1  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (this.dtfeeInfo != null)
            {
                this.dtfeeInfo.Clear();
                this.dtfeeInfo.AcceptChanges();
            }
            if (patient == null)
            {
                return -1;
            }
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //不允许有病案
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Fee ba = new FS.HISFC.BizLogic.HealthRecord.Fee();
            //查询符合条件的数据
            if (patientInfo.CaseState == "1")
            {
                feeInfoList = ba.QueryFeeInfoState(patientInfo.ID);
            }
            else
            {
                feeInfoList = ba.QueryFeeInfoState(patientInfo.ID);
            }
            if (feeInfoList == null)
            {
                return -1;
            }
            //循环插入数据
            foreach (FS.HISFC.Models.RADT.Patient info in feeInfoList)
            {
                DataRow row = dtfeeInfo.NewRow();
                SetRow(row, info);
                dtfeeInfo.Rows.Add(row);
            }
            decimal tempDec = 0;
            foreach (FS.HISFC.Models.RADT.Patient info in feeInfoList)
            {
                tempDec = tempDec + FS.FrameWork.Function.NConvert.ToDecimal(info.IDCard);
            }
            FS.HISFC.Models.RADT.Patient obj = new FS.HISFC.Models.RADT.Patient();
            obj.AreaCode = "合计：";
            obj.IDCard = tempDec.ToString();
            DataRow rows = dtfeeInfo.NewRow();
            SetRow(rows, obj);
            dtfeeInfo.Rows.Add(rows);

            //更改标志
            dtfeeInfo.AcceptChanges();
            SetFpEnter();
            return 0;
        }
        /// <summary>
        /// 查询并显示数据
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int LoadInfoDrgs(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //仅用于显示不保存
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //不允许有病案
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Fee ba = new FS.HISFC.BizLogic.HealthRecord.Fee();
            DataSet ds = new DataSet();
            ba.QueryFeeForDrgsByInpatientNO(patient.ID, ref ds);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0].Rows.Count < 0)
            {
                return -1;
            }
            this.SetFarpointForDrgs(ds);
            return 0;
        }
        /// <summary>
        /// 将实体中的值赋值到row中
        /// </summary>
        /// <param name="row">传入的row</param>
        /// <param name="info">传入的实体</param>
        private void SetRow(DataRow row, FS.HISFC.Models.RADT.Patient info)
        {
            row["统计编码"] = info.DIST;//统计大类编码
            row["费用名称"] = info.AreaCode; //统计名称 
            row["费用金额"] = FS.FrameWork.Function.NConvert.ToDecimal(info.IDCard);//统计费用 
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <returns></returns>
        public int InitInfo()
        {
            this.InitDateTable();
            fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            SetFpEnter();
            return 0;
        }
        /// <summary>
        /// 初始化表
        /// </summary>
        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type DecimalType = typeof(System.Decimal);

                dtfeeInfo.Columns.AddRange(new DataColumn[]{
															new DataColumn("统计编码", strType),	//0
															new DataColumn("费用名称", strType),	 //1
															new DataColumn("费用金额", DecimalType)});//9
                //绑定数据源
                this.fpSpread1_Sheet1.DataSource = dtfeeInfo;
                //				//设置fpSpread1 的属性
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 离开CELL时发生,用于  计算合计金额 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            if (fpSpread1_Sheet1.ActiveColumnIndex == 2)
            {
                decimal tempDecimal = 0;
                for (int i = 0; i < fpSpread1_Sheet1.Rows.Count - 1; i++)
                {
                    //累计金额
                    tempDecimal += FS.FrameWork.Function.NConvert.ToDecimal(fpSpread1_Sheet1.Cells[i, 2].Text);
                }
                //更改合计金额
                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.Rows.Count - 1, 2].Text = tempDecimal.ToString();
            }
        }

        /// <summary>
        /// 显示drgs费用
        /// </summary>
        /// <param name="ds"></param>
        private void SetFarpointForDrgs(DataSet ds)
        {
            this.fpSpread1_Sheet1.RowCount = 40;
            this.fpSpread1_Sheet1.Columns[0].Width = 0;
            this.fpSpread1_Sheet1.Columns[1].Width = 180;

            this.fpSpread1_Sheet1.Cells[0, 1].Text = "总费用";
            this.fpSpread1_Sheet1.Cells[0, 2].Text = ds.Tables[0].Rows[0][0].ToString();
            this.fpSpread1_Sheet1.Cells[1, 1].Text = "自付金额";
            this.fpSpread1_Sheet1.Cells[1, 2].Text = ds.Tables[0].Rows[0][1].ToString();

            this.fpSpread1_Sheet1.Cells[2, 1].Text = "1、综合医疗服务类";
            this.fpSpread1_Sheet1.Cells[2, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[3, 1].Text = "一般医疗服务费";
            this.fpSpread1_Sheet1.Cells[3, 2].Text = ds.Tables[0].Rows[0][2].ToString();
            this.fpSpread1_Sheet1.Cells[4, 1].Text = "一般治疗操作费";
            this.fpSpread1_Sheet1.Cells[4, 2].Text = ds.Tables[0].Rows[0][3].ToString();
            this.fpSpread1_Sheet1.Cells[5, 1].Text = "护理费";
            this.fpSpread1_Sheet1.Cells[5, 2].Text = ds.Tables[0].Rows[0][4].ToString();
            this.fpSpread1_Sheet1.Cells[6, 1].Text = "其他费用";
            this.fpSpread1_Sheet1.Cells[6, 2].Text = ds.Tables[0].Rows[0][5].ToString();

            this.fpSpread1_Sheet1.Cells[7, 1].Text = "2、诊断类";
            this.fpSpread1_Sheet1.Cells[7, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[8, 1].Text = "病理诊断费";
            this.fpSpread1_Sheet1.Cells[8, 2].Text = ds.Tables[0].Rows[0][6].ToString();
            this.fpSpread1_Sheet1.Cells[9, 1].Text = "实验室诊断费";
            this.fpSpread1_Sheet1.Cells[9, 2].Text = ds.Tables[0].Rows[0][7].ToString();
            this.fpSpread1_Sheet1.Cells[10, 1].Text = "影像诊断费";
            this.fpSpread1_Sheet1.Cells[10, 2].Text = ds.Tables[0].Rows[0][8].ToString();
            this.fpSpread1_Sheet1.Cells[11, 1].Text = "临床诊断费";
            this.fpSpread1_Sheet1.Cells[11, 2].Text = ds.Tables[0].Rows[0][9].ToString();

            this.fpSpread1_Sheet1.Cells[12, 1].Text = "3、治疗类";
            this.fpSpread1_Sheet1.Cells[12, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[13, 1].Text = "非手术治疗项目费";
            this.fpSpread1_Sheet1.Cells[13, 2].Text = ds.Tables[0].Rows[0][10].ToString();
            this.fpSpread1_Sheet1.Cells[14, 1].Text = "临床物理治疗费";
            this.fpSpread1_Sheet1.Cells[14, 2].Text = ds.Tables[0].Rows[0][11].ToString();
            this.fpSpread1_Sheet1.Cells[15, 1].Text = "手术治疗费";
            this.fpSpread1_Sheet1.Cells[15, 2].Text = ds.Tables[0].Rows[0][12].ToString();
            this.fpSpread1_Sheet1.Cells[16, 1].Text = "麻醉费";
            this.fpSpread1_Sheet1.Cells[16, 2].Text = ds.Tables[0].Rows[0][13].ToString();
            this.fpSpread1_Sheet1.Cells[17, 1].Text = "手术费";
            this.fpSpread1_Sheet1.Cells[17, 2].Text = ds.Tables[0].Rows[0][14].ToString();

            this.fpSpread1_Sheet1.Cells[18, 1].Text = "4、康复类";
            this.fpSpread1_Sheet1.Cells[18, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[19, 1].Text = "康复费";
            this.fpSpread1_Sheet1.Cells[19, 2].Text = ds.Tables[0].Rows[0][15].ToString();

            this.fpSpread1_Sheet1.Cells[20, 1].Text = "5、中医类（中医和民族医医疗服务）";
            this.fpSpread1_Sheet1.Cells[20, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[21, 1].Text = "中医治疗";//中医类的需要再处理
            this.fpSpread1_Sheet1.Cells[21, 2].Text = ds.Tables[0].Rows[0][16].ToString();

            this.fpSpread1_Sheet1.Cells[22, 1].Text = "6、西药类";
            this.fpSpread1_Sheet1.Cells[22, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[23, 1].Text = "西药费";
            this.fpSpread1_Sheet1.Cells[23, 2].Text = ds.Tables[0].Rows[0][17].ToString();
            this.fpSpread1_Sheet1.Cells[24, 1].Text = "抗菌药物费用";
            this.fpSpread1_Sheet1.Cells[24, 2].Text = ds.Tables[0].Rows[0][18].ToString();

            this.fpSpread1_Sheet1.Cells[25, 1].Text = "7、中药类";
            this.fpSpread1_Sheet1.Cells[25, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[26, 1].Text = "中成药费";
            this.fpSpread1_Sheet1.Cells[26, 2].Text = ds.Tables[0].Rows[0][19].ToString();
            this.fpSpread1_Sheet1.Cells[27, 1].Text = "中草药费";
            this.fpSpread1_Sheet1.Cells[27, 2].Text = ds.Tables[0].Rows[0][20].ToString();

            this.fpSpread1_Sheet1.Cells[28, 1].Text = "8、血液和血液制品类";
            this.fpSpread1_Sheet1.Cells[28, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[29, 1].Text = "血费";
            this.fpSpread1_Sheet1.Cells[29, 2].Text = ds.Tables[0].Rows[0][21].ToString();
            this.fpSpread1_Sheet1.Cells[30, 1].Text = "白蛋白类制品费";
            this.fpSpread1_Sheet1.Cells[30, 2].Text = ds.Tables[0].Rows[0][22].ToString();
            this.fpSpread1_Sheet1.Cells[31, 1].Text = "球蛋白类制品费";
            this.fpSpread1_Sheet1.Cells[31, 2].Text = ds.Tables[0].Rows[0][23].ToString();
            this.fpSpread1_Sheet1.Cells[32, 1].Text = "凝血因子类制品费";
            this.fpSpread1_Sheet1.Cells[32, 2].Text = ds.Tables[0].Rows[0][24].ToString();
            this.fpSpread1_Sheet1.Cells[33, 1].Text = "细胞因子类制品费";
            this.fpSpread1_Sheet1.Cells[33, 2].Text = ds.Tables[0].Rows[0][25].ToString();

            this.fpSpread1_Sheet1.Cells[34, 1].Text = "9、耗材类";
            this.fpSpread1_Sheet1.Cells[34, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[35, 1].Text = "检查用一次性医用材料费";
            this.fpSpread1_Sheet1.Cells[35, 2].Text = ds.Tables[0].Rows[0][26].ToString();
            this.fpSpread1_Sheet1.Cells[36, 1].Text = "治疗用一次性医用材料费";
            this.fpSpread1_Sheet1.Cells[36, 2].Text = ds.Tables[0].Rows[0][27].ToString();
            this.fpSpread1_Sheet1.Cells[37, 1].Text = "手术用一次性医用材料费";
            this.fpSpread1_Sheet1.Cells[37, 2].Text = ds.Tables[0].Rows[0][28].ToString();

            this.fpSpread1_Sheet1.Cells[38, 1].Text = "10、其他类";
            this.fpSpread1_Sheet1.Cells[38, 1].ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells[39, 1].Text = "其他费";
            this.fpSpread1_Sheet1.Cells[39, 2].Text = ds.Tables[0].Rows[0][29].ToString();
            this.fpSpread1_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(this.fpSpread1_Sheet1_CellChanged);
        }
        #endregion 
    }
}
