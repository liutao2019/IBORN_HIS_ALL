using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucFinIpbPatientFeeByStatZH : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFinIpbPatientFeeByStatZH()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 业务层
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();


        /// <summary>
        /// 患者HIS信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        #endregion


        private void Init()
        {
            //this.OnDrawTree();
            this.neuSpread1_Sheet1.RowHeader.Visible = false;
            this.neuSpread1_Sheet1.ColumnHeader.Visible = false;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowCount = 1;
            //this.neuSpread1_Sheet1.ColumnCount = 1;

            //this.neuDateTime.Value = managerIntegrate.GetDateTimeFromSysDateTime().Date.AddDays(-1);
        }


        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public void Query()
        {
            DataSet ds;

            string inpatientNo = this.p.ID;

            ds = new DataSet();
            int intReturn = this.GetInpatientDayFeeByStat(inpatientNo, DateTime.Now.ToString(), DateTime.Now.ToString(), "ALL", ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查询错误");
                return;
            }
            int intRowCount = ds.Tables[0].Rows.Count;
            Hashtable objHash = new Hashtable();
            List<DataRow> objListRow = null;
            DataRow objDr = null;
            for (int intI = 0; intI < intRowCount; intI++)
            {
                objDr = ds.Tables[0].Rows[intI];
                if (!objHash.ContainsKey(objDr[1].ToString()))
                {
                    objListRow = new List<DataRow>();
                    objListRow.Add(objDr);
                    objHash.Add(objDr[1].ToString(), objListRow);
                }
                else
                {
                    objListRow = objHash[objDr[1].ToString()] as List<DataRow>;
                    objListRow.Add(objDr);
                }
            }
            FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowText, 2, false, false, false, true);
            if (ds != null && ds.Tables.Count > 0)
            {
                Font font = new Font(this.neuSpread1.Font.FontFamily, 14F, System.Drawing.FontStyle.Bold);
                #region 循环每个患者往farpoint赋值
                foreach (DictionaryEntry item in objHash)
                {
                    objListRow = (List<DataRow>)item.Value;
                    objDr = objListRow[0];
                    //增加标题。
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = objDr[0].ToString() + "在院患者发生费用汇总清单(按发票分类)";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Font = font;//加粗,加大标题
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;//居中
                    //增加患者基本信息
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "病床号:" + objDr[11].ToString().Substring(4);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "病区:" + objDr[6].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "住院号:" + objDr[1].ToString();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = "姓名:" + objDr[2].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "合同单位:" + objDr[7].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "日期:" + DateTime.Now.ToShortDateString();
                    //屏蔽日期
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = "";

                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                    //显示患者费用列头
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 0, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 1, "费用", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 2, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 3, "费用", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 4, "类别", false);
                    this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.RowCount - 1, 5, "费用", false);
                    int intCount = objListRow.Count;
                    int intM = 0;
                    int intJ = 0;
                    DataRow objdr1 = null;
                    decimal dcmTot_Cost = 0;
                    for (intM = 0; intM < intCount; intM++)
                    {
                        objdr1 = objListRow[intM];
                        intJ = intM % 3;
                        if (intJ == 0)
                        {
                            //显示患者具体统计大类费用
                            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        }
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, intJ * 2].Text = objdr1[18].ToString();
                        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, intJ * 2 + 1].Text = objdr1[19].ToString();
                        dcmTot_Cost += objDr[19] == null ? 0 : decimal.Parse(objdr1[19].ToString());

                        if (intM == intCount - 1)
                        {
                            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                        }
                    }
                    //增加患者费用信息
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 2);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 0].Text = "医疗总费用：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 2].Text = "自付比例：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 4].Text = "未结总费用：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 1].Text = dcmTot_Cost.ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 3].Text = objDr[22].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 5].Text = objDr[3].ToString();


                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "已结金额：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "预交款：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "结余金额：";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = objDr[20].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = objDr[12].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = objDr[14].ToString();

                    FS.HISFC.Models.RADT.PatientInfo obj = this.managerIntegrate.QueryPatientInfoByInpatientNO(inpatientNo);
                    if (obj.Pact.PayKind.ID == "02")
                    {
                        FS.HISFC.Models.Base.FT ft = this.feeMgr.QueryPatientSumFee(inpatientNo, obj.PVisit.InTime.ToString(), DateTime.Now.ToString());
                        if (ft != null)
                        {
                            obj.PVisit.MedicalType.ID = this.GetSiEmplType(inpatientNo);
                            FS.HISFC.BizProcess.Integrate.Fee feeProcess = new FS.HISFC.BizProcess.Integrate.Fee();
                            feeProcess.ComputePatientSumFee(obj, ref ft);
                            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "起付线金额：";
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = "按比例自付金额：";
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = "自费金额：";
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = ft.DefTotCost.ToString();
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = ft.PayCost.ToString();
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = ft.OwnCost.ToString();

                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 3, 3].Text = (ft.FTRate.PayRate * 100).ToString() + "%";
                            ft.PrepayCost = FS.FrameWork.Function.NConvert.ToDecimal(objDr[12].ToString());
                            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 5].Text = (ft.PrepayCost - ft.OwnCost - ft.PayCost).ToString();
                            decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(objDr[3].ToString());
                            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 2, 5].Text = (totCost > ft.DefTotCost) ? (ft.PrepayCost - ft.OwnCost - ft.PayCost - ft.DefTotCost).ToString() : (ft.PrepayCost - ft.OwnCost - ft.PayCost).ToString();
                        }
                    }


                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;

                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 6);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "备注：对医保患者此清单仅供参考，最终以医保结算为准。打印时间：" + DateTime.Now.ToString();


                    this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder1;
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                }
                #endregion
            }
        }



        public int GetInpatientDayFeeByStat(string inpatientNo, string beginDate, string endDate, string dept, ref DataSet ds)
        {
            string sql = "";

            if (this.managerIntegrate.Sql.GetSql("Fee.InpatientFee.GetInpatientFeeByStatZH", ref sql) == -1)
            {
                this.managerIntegrate.Err = "获取患者一日费用出错!";
                return -1;
            }
            sql = string.Format(sql, inpatientNo, beginDate, endDate, dept);
            this.managerIntegrate.ExecQuery(sql, ref ds);
            return 1;
        }

        private string GetSiEmplType(string inpatientNo)
        {
            string sql = "select si.empl_type from fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.valid_flag='1'";
            string str = "1";
            try
            {
                sql = string.Format(sql, inpatientNo);
                str = this.managerIntegrate.ExecSqlReturnOne(sql, "1");
                return str;
            }
            catch (Exception e)
            {

                return "1";
            }
        }

        public void Print()
        {
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize ps = pageSizeMgr.GetPageSize("dayFee");
            if (ps == null)
            {
                MessageBox.Show("请在信息科维护处，维护名叫“dayFee”的纸张，一般设置为高400，宽450");
                return;
            }
            FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
            System.Drawing.Printing.PaperSize ps1 = new System.Drawing.Printing.PaperSize();

            ps1.PaperName = "dayFee";
            ps1.Width = ps.Width;
            ps1.Height = ps.Height;
            //  ps1.Height = 1100;
            ps.Top = 0;
            pi.PaperSize = ps1;
            pi.ShowRowHeaders = false;
            pi.ShowColumnHeaders = false;
            pi.Preview = false;


            this.neuSpread1_Sheet1.PrintInfo = pi;
            this.neuSpread1_Sheet1.PrintInfo.Margin.Top = 0;
            this.neuSpread1_Sheet1.PrintInfo.Margin.Bottom = 30;
            this.neuSpread1_Sheet1.PrintInfo.ShowBorder = false;
            this.neuSpread1.PrintSheet(0);
            //p.PrintPage(0, 0, this.neuPlFp);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucQueryPatientInfo_myEvent();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        private void ucQueryPatientInfo_myEvent()
        {
            this.Init();

            //TODO:1、查询患者HIS信息
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "N")
            {
                MessageBox.Show("该患者已经本地无费退院!");
                //this.Clear();
                this.ucQueryPatientInfo.Focus();
                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "O")
            {
                MessageBox.Show("该患者已经出院!");
                //this.Clear();
                this.ucQueryPatientInfo.Focus();
                return;
            }

            this.p = patientTemp.Clone();

            this.Query();
        }

    }


}
