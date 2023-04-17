using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucPublicBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPublicBill()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.RADT.InPatient myRadt = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
        //Report.Management.PubReport myReport = new Local.Report.Management.PubReport();
        SOC.Local.PubReport.BizLogic.PubReport myReport = new SOC.Local.PubReport.BizLogic.PubReport();

        #endregion

        #region 函数

        public int SaveBill(string flag)
        {
            bool isSelect = false;
            int iReturn = 0;
            if (this.txtGJF.Text.Trim() != "" || this.txtZLF.Text.Trim() != "")
            {
                try
                {
                    decimal gjf = FS.FrameWork.Function.NConvert.ToDecimal(this.txtGJF.Text.Trim());
                    decimal zlf = FS.FrameWork.Function.NConvert.ToDecimal(this.txtZLF.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("请正确输入高检费或者肿瘤审批费！");
                    return -1;
                }

            }
            //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(myRadt.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);//-add
            FS.HISFC.Models.RADT.PatientInfo p = null;
            DateTime dtBegin = this.dtpBegin.Value;
            DateTime dtEnd = this.dtpEnd.Value;
            try
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    isSelect = (bool)this.fpSpread1_Sheet1.Cells[i, 0].Value;
                    if (!isSelect)
                    {
                        continue;
                    }
                    p = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    p.Patient.Sex.User01 = this.txtGJF.Text.Trim();
                    if (p.Patient.Sex.User01 == "")
                    {
                        p.Patient.Sex.User01 = "0";
                    }
                    p.Patient.Sex.User02 = this.txtZLF.Text.Trim();
                    if (p.Patient.Sex.User02 == "")
                    {
                        p.Patient.Sex.User02 = "0";
                    }

                    iReturn = myReport.InsertStatic(p, "0", dtBegin, dtEnd);

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("成功!");
            return 0;
        }
        /// <summary>
        /// 生成单据
        /// </summary>
        /// <returns></returns>
        public void MakeBill()
        {
            FS.HISFC.Models.RADT.PatientInfo p = null;
            this.panel1.Controls.Clear();
            string temp;
            bool isSelect = false; ;
            ArrayList alPatient = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                isSelect = (bool)this.fpSpread1_Sheet1.Cells[i, 0].Value;
                if (!isSelect)
                {
                    continue;
                }
                temp = this.fpSpread1_Sheet1.Cells[i, 11].Text;
                if (temp != "是")
                {
                    continue;
                }
                p = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo;
                p.User01 = this.dtpBegin.Value.ToString();
                p.User02 = this.dtpEnd.Value.ToString();

                alPatient.Add(p);
            }
           // FS.Common.Controls.Function.AddControlToPanelIfNeed(alPatient, new ucTrusteeBill(), this.panel1, new System.Drawing.Size(850, 1164));
           FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alPatient, new ucTrusteeBill(), this.panel1, new System.Drawing.Size(850, 1164),0);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            p.PrintPage(0, 0, this.panel1);

            return 0;
        }
        public int SetStaticPatients(string flag, string inState, string inpatientNo)
        {

            ArrayList alPatient = new ArrayList();
            DateTime dtBegin = this.dtpBegin.Value;
            DateTime dtEnd = this.dtpEnd.Value;
            if (inpatientNo != null && inpatientNo != "")
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                p = myRadt.QueryPatientInfoByInpatientNO(inpatientNo);
                if (p == null)
                {
                    MessageBox.Show("获得患者基本信息出错!");
                    return -1;
                }
                alPatient.Add(p);
            }
            else
            {
                if (inState == "I")
                {
                    //查询截至统计时间前所有公费在院患者
                    //					alPatient = myRadt.PatientQuery(DateTime.MinValue.ToString(), dtEnd.ToString(), inState, "03");
                    alPatient = myRadt.PatientQuery(DateTime.MinValue.ToString(), dtEnd.ToString(), inState, "03", "");
                }
                else
                {
                    //alPatient = myRadt.PatientQuery(dtBegin, dtEnd, "03");
                    alPatient = myRadt.QueryPatientInfoByTimeInState(dtBegin, dtEnd, "03");
                }
            }
            if (alPatient == null)
            {
                MessageBox.Show("查询患者出错!" + "\n" + myRadt.Err);
                return -1;
            }
            SOC.Local.PubReport.Models.PubReport report = null; //托收单实体
            bool isExist = false;//查询患者是否已经存在
            int iReturn = 0;//返回值


            this.fpSpread1_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo p in alPatient)
            {
                //生育保险患者不出托收单，不处理
                if (p.Patient.Pact.ID == "4")
                {
                    continue;
                }
                //验证患者是否已经存在托收信息
                iReturn = this.IsExist(p.ID, dtBegin, dtEnd, ref isExist);
                if (iReturn == -1)
                {
                    return -1;
                }
                if (isExist)//患者在此期间已经存在托收信息
                {
                    //查询患者在此期间有统计的托收信息，给前台显示
                    //report = myReport.GetPubReport(p.ID, dtBegin.ToString(), dtEnd.ToString());

                    report = myReport.GetPubReport(p.ID, dtEnd.Date.ToString());

                    if (report == null)
                    {
                        MessageBox.Show("获得患者托收单信息出错!" + "\n" + myReport.Err);
                        return -1;
                    }
                }
                //将要统计托收的患者显示
                iReturn = SetPatientInfo(p, report, isExist, dtBegin, dtEnd);
                if (iReturn == -1)
                {
                    return -1;
                }
            }


            return 0;
        }
        /// <summary>
        /// 更改患者信息
        /// </summary>
        /// <returns></returns>
        public int Modify()
        {
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.HISFC.Models.RADT.PatientInfo p = this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.RADT.PatientInfo;
            string temp = this.fpSpread1_Sheet1.Cells[row, 11].Text;
            if (temp != "是")
            {
                return -1;
            }
            DateTime dtBegin = this.dtpBegin.Value;
            DateTime dtEnd = this.dtpEnd.Value;
            SOC.Local.PubReport.Models.PubReport report = null;

            if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Tag != null)
            {
                report = myReport.GetPubReport(p.ID, ((SOC.Local.PubReport.Models.PubReport)this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Tag).Static_Month.Date.ToString());
            }
            else
            {
                report = myReport.GetPubReport(p.ID, dtEnd.Date.ToString());
            }
            if (report == null)
            {
                MessageBox.Show("获得患者托收单信息出错!" + myReport.Err);
                return -1;
            }
            SOC.Local.PubReport.Components.ucModifyPublicBill uc = new SOC.Local.PubReport.Components.ucModifyPublicBill();
            uc.PatientInfo = p;
            uc.Report = report;
            uc.SetInfo();
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "修改患者托收: " + p.Name;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            return 0;
        }
        /// <summary>
        /// 删除单
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.HISFC.Models.RADT.PatientInfo p = this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.RADT.PatientInfo;

            if (p == null)
            {
                return -1;
            }
            string temp = this.fpSpread1_Sheet1.Cells[row, 11].Text;
            if (temp != "是")
            {
                MessageBox.Show("该患者还没有生成托收信息!");
                return -1;
            }
            string statid = this.fpSpread1_Sheet1.Cells[row, 12].Text;
            if (statid == "HOHO")
            {
                MessageBox.Show("该患者还没有生成托收信息!");
                return -1;
            }
            //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(myRadt.Connection);
            //Report.Management.PubReport myReport = new Local.Report.Management.PubReport();
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            SOC.Local.PubReport.BizLogic.PubReport myReport = new FS.SOC.Local.PubReport.BizLogic.PubReport();

            myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //			SOC.Local.PubReport.Models.PubReport report = myReport.GetPubReport(p.ID, this.dtpEnd.Value.Date.ToString());
            //			if(report == null)
            //			{
            //				t.RollBack();
            //				MessageBox.Show("删除失败，请注意查询时间正确!");
            //				return -1;
            //			}
            DialogResult r = MessageBox.Show("你确认删除 " + p.Name + " 的本期托收单信息么?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                SOC.Local.PubReport.Models.PubReport reportTemp = (SOC.Local.PubReport.Models.PubReport)(this.fpSpread1_Sheet1.Cells[row, 0].Tag);
                if (reportTemp == null) return -1;
                //EXT_FLAG存的是发票号
                if (reportTemp.InvoiceNo != null && reportTemp.InvoiceNo != "")
                {
                    MessageBox.Show("该托收单已出发票，请进行结算召回处理！");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }

                int iReturn = myReport.DeletePubReport(statid);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败!" + myReport.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }
        /// <summary>
        /// 初始化显示年月份
        /// </summary>
        public void Init()
        {
            try
            {
                //				DateTime nowDate = myReport.GetDateTimeFromSysDateTime();
                //				if(nowDate.Year == 2006 && nowDate.Month == 1)//2006年的一月份统计12月21号0点开始 1月20号的23:59:59秒结束
                //				{
                //					this.dtpBegin.Value = new DateTime(2005, 12, 21, 0, 0, 0);
                //					this.dtpEnd.Value = new DateTime(2006, 1, 20, 23, 59, 59);
                //				}
                //				else if(nowDate.Year == 2006 && nowDate.Month == 2)//2006的二月统计1 21的0点到 25号23:59:59
                //				{
                //					this.dtpBegin.Value = new DateTime(2006, 1, 21, 0, 0, 0);
                //					this.dtpEnd.Value = new DateTime(2006, 2, 25, 23, 59, 59);
                //				}
                //				else
                //				{
                //					if(nowDate.Month == 1)//跨年
                //					{
                //						this.dtpBegin.Value = new DateTime(nowDate.Year - 1, 12, 26, 0, 0, 0);
                //						this.dtpEnd.Value = new DateTime(nowDate.Year, nowDate.Month, 25, 23, 59, 59);
                //					}
                //					else
                //					{
                //						this.dtpBegin.Value = new DateTime(nowDate.Year, nowDate.Month - 1, 26, 0, 0, 0);
                //						this.dtpEnd.Value = new DateTime(nowDate.Year, nowDate.Month, 25, 23, 59, 59);
                //					}
                //				}
                this.fpSpread1_Sheet1.SetColumnAllowAutoSort(-1, true);

                SOC.Local.PubReport.BizLogic.PubReport report = new SOC.Local.PubReport.BizLogic.PubReport();

                FS.FrameWork.Models.NeuObject obj = report.GetStaticTime();

                if (obj == null)
                {
                    return;
                }

                DateTime dtBegin;
                DateTime dtEnd;

                dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(obj.User01);
                dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(obj.User02);

                this.dtpBegin.Value = dtBegin;
                this.dtpEnd.Value = dtEnd;

            }
            catch { }

        }
        /// <summary>
        /// 验证患者在此时间段内是否存在托收单信息.
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isExist"></param>
        /// <returns></returns>
        private int IsExist(string inpatientNo, DateTime dtBegin, DateTime dtEnd, ref bool isExist)
        {
            string tmpValue = "";
            tmpValue = myReport.IsExist(inpatientNo, dtBegin, dtEnd);
            if (tmpValue == null)
            {
                MessageBox.Show("查找患者是否存在托收信息出错!" + "\n" + myReport.Err);
                return -1;
            }
            if (FS.FrameWork.Function.NConvert.ToInt32(tmpValue) > 1)
            {
                MessageBox.Show("该患者在统计时间段内存在多条托收信息，请合并后再统计!");
                return -1;
            }
            isExist = FS.FrameWork.Function.NConvert.ToBoolean(tmpValue);

            return 0;
        }
        private int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo p, SOC.Local.PubReport.Models.PubReport report, bool isSelect,
            DateTime dtBegin, DateTime dtEnd)
        {
            //增加一行
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
            int row = this.fpSpread1_Sheet1.RowCount - 1;//当前行
            string temp = "";
            DateTime pBeginDate = DateTime.MinValue;
            DateTime pEndDate = DateTime.MinValue;
            DateTime lastStatic;

            //查找患者的最后一次统计时间，如果没有，那么说明患者一致没有打印
            //托说单，开始查询的时间设置为患者的入院日期.
            temp = myReport.GetLastStaticTime(p.ID, dtEnd.Date.ToString());
            if (temp == null)
            {
                MessageBox.Show("查找患者" + p.ID + "的最后统计时间出错!" + myReport.Err);
                return -1;
            }
            lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);

            if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
            {
                if (p.PVisit.InTime < dtBegin)
                {

                    pBeginDate = new DateTime(2005, 12, 21, 0, 0, 0);//本来应该入院时间，由于这次已经统计过了，那么取开始时间就可以了.
                }
                else
                {
                    pBeginDate = p.PVisit.InTime;
                }
            }
            else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间 + 1秒
            {
                pBeginDate = lastStatic.AddSeconds(1);//new DateTime(lastStatic.Year , lastStatic.Month, lastStatic.Day, lastStatic.Hour,lastStatic.Minute, lastStatic.Second + 1);
            }
            else //患者的统计时间为统一的开始时间
            {
                pBeginDate = dtBegin;
            }
            if (p.PVisit.InState.ID.ToString() == "O")//结算患者
            {
                //ArrayList alBalance = myFee.GetBalanceHeadInfoByInpatientNo(p.ID);
                ArrayList alBalance = myFee.QueryBalancesByInpatientNO(p.ID);
                if (alBalance == null)
                {
                    MessageBox.Show("获得结算患者信息出错!");
                    return -1;
                }
                DateTime dtTemp = DateTime.MinValue;

                foreach (FS.HISFC.Models.Fee.Inpatient.Balance b in alBalance)
                {
                    if (b.BalanceType.ID.ToString() == "O")
                    {
                        if (b.BalanceOper.OperTime > dtTemp)
                        {
                            dtTemp = b.BalanceOper.OperTime;
                        }
                    }
                }
                if (dtTemp != DateTime.MinValue)//有结算时间
                {
                    if (dtTemp < dtEnd)//本期结算患者
                    {
                        pEndDate = p.PVisit.OutTime; ;
                    }
                    else
                    {
                        pEndDate = dtEnd;
                    }
                }
            }
            else
            {
                pEndDate = dtEnd; //如果是月统计，患者的结束统计时间为统一的结束时间.
            }

            this.fpSpread1_Sheet1.Cells[row, 0].Value = !isSelect;//是否默认选择
            this.fpSpread1_Sheet1.Cells[row, 1].Text = p.PID.PatientNO;//住院号
            this.fpSpread1_Sheet1.Cells[row, 2].Text = p.Name;//姓名
            this.fpSpread1_Sheet1.Cells[row, 3].Text = p.PVisit.PatientLocation.Dept.Name;//患者住院科室
            this.fpSpread1_Sheet1.Cells[row, 4].Text = p.SSN;//社会保险号
            this.fpSpread1_Sheet1.Cells[row, 5].Text = p.Pact.Name;//合同单位
            this.fpSpread1_Sheet1.Cells[row, 6].Text = p.PVisit.InState.ID.ToString();//在院状态
            this.fpSpread1_Sheet1.Cells[row, 7].Text = p.PVisit.InTime.ToString();//入院登记时间
            //this.fpSpread1_Sheet1.Cells[row,8].Text = p.Patient.出院结算时间
            if (isSelect)
            {
                this.fpSpread1_Sheet1.Cells[row, 9].Text = report.Begin.ToString();
                this.fpSpread1_Sheet1.Cells[row, 10].Text = report.End.ToString();
                this.fpSpread1_Sheet1.Cells[row, 11].Text = "是";
                this.fpSpread1_Sheet1.Cells[row, 12].Text = report.ID;
            }
            else
            {
                this.fpSpread1_Sheet1.Cells[row, 9].Text = pBeginDate.ToString();
                this.fpSpread1_Sheet1.Cells[row, 10].Text = pEndDate.ToString();
                this.fpSpread1_Sheet1.Cells[row, 11].Text = "否";
                this.fpSpread1_Sheet1.Cells[row, 12].Text = "HOHO";
            }
            this.fpSpread1_Sheet1.Rows[row].Tag = p;
            this.fpSpread1_Sheet1.Cells[row, 0].Tag = report;
            return 0;
        }

        public void SelectAll(string flag, bool isSelect)
        {
            switch (flag)
            {
                case "1": //全选所有
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        this.fpSpread1_Sheet1.Cells[i, 0].Value = isSelect;
                    }
                    break;
                case "2"://全选已经统计
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if (this.fpSpread1_Sheet1.Cells[i, 11].Text == "是")
                        {
                            this.fpSpread1_Sheet1.Cells[i, 0].Value = isSelect;
                        }
                    }
                    break;
            }
        }

        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("没有该住院号患者");
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
            p = myRadt.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (p == null)
            {
                MessageBox.Show("查找患者信息出错!");
                this.ucQueryInpatientNo1.Focus();
                return;
            }
            SetStaticPatients("0", p.PVisit.InState.ID.ToString() == "O" ? "O" : "I", p.ID);
        }

        private void tabControl1_SelectionChanged(object sender, System.EventArgs e)
        {

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Modify();
        }

        private void tbQuery_Click(object sender, System.EventArgs e)
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == "")
            {
                MessageBox.Show("请输入患者的住院号");
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
            p = myRadt.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (p == null)
            {
                MessageBox.Show("查找患者信息出错!");
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            ArrayList alReport = this.myReport.GetPubReportArray(this.ucQueryInpatientNo1.InpatientNo, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());

            if (alReport == null || alReport.Count <= 0)
            {
                MessageBox.Show("没有查找到该时间段内的托收单!");
                return;
            }

            this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);

            foreach (SOC.Local.PubReport.Models.PubReport rep in alReport)
            {
                this.SetPatientInfo(p, rep, true, this.dtpBegin.Value, this.dtpEnd.Value);
            }
        }
    }
}
