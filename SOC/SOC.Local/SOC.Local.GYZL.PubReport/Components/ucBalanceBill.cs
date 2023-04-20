using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.GYZL.PubReport.Components
{
    public partial class ucBalanceBill : UserControl
    {  

        public ucBalanceBill()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 住院流水号
        /// </summary>
        string inPatientNo;
        public string InPatientNo
        {
            get { return this.inPatientNo; }
            set { this.inPatientNo = value; }
        }

        /// <summary>
        /// 入院时间
        /// </summary>
        DateTime dtBegin;
        /// <summary>
        /// 结算时间
        /// </summary>
        DateTime dtEnd;
        bool IsPrintTr = false;

        bool isBalance = false;
        public bool IsBalance
        {
            set
            {
                this.isBalance = false;
            }
            get
            {
                return this.isBalance;
            }
        }
        /// <summary>
        /// 住院患者信息实体
        /// </summary>
        public  FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizLogic.RADT.InPatient patientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizLogic.Fee.FeeReport feeReport = new FS.HISFC.BizLogic.Fee.FeeReport();
        SOC.Local.GYZL.PubReport.BizLogic.PubReport myReport = new SOC.Local.GYZL.PubReport.BizLogic.PubReport();
        private ucTrusteeBill ucRep;
       
        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.BackColor = Color.White;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134))); ;
        }
        /// <summary>
        /// 查询患者费用
        /// </summary>
        /// <param name="info">患者基本信息</param>
        private int QueryFee(FS.HISFC.Models.RADT.PatientInfo info)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
        
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            con.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (feeMgr.GetStopAccount(info.ID) == "1")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", "提示");
                return -1;
            }


            //设置入院时间和结算时间
            this.dtBegin = info.PVisit.InTime;
            this.dtEnd = this.feeMgr.GetDateTimeFromSysDateTime();

            int PubDay = 0;
            //公费患者，应该减去一天日限额			
            if (info.Pact.PayKind.ID == "03")
            {
              
                TimeSpan DaysSpan = info.PVisit.OutTime.Date - info.PVisit.InTime.Date;
                 PubDay = DaysSpan.Days;
                if (PubDay == 0)
                {
                    PubDay = 1;
                }

                DialogResult dr;

                if(PubDay==1)
                {
                    dr = MessageBox.Show("是否需要减去当天日限额？", "提示"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes)
                    {
                        info.FT.OvertopCost = info.FT.OvertopCost + info.FT.DayLimitCost;

                    }
                }
              

            }


            //查询患者公费日限额是否超标
            if (info.Pact.PayKind.ID == "03")
            {

                //删除公费超标调整金额fin_ipb_medicinelist
                if (this.feeMgr.DeleteOverLimitMedInfo(info.ID,"0") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除公费超标调整金额fin_ipb_medicinelist出错!" + this.feeMgr.Err);
                    return -1;
                }
                //删除公费超标调整金额fin_ipb_feeinfo
                if (this.feeMgr.DeleteOverLimitFeeInfo(info.ID, "0") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除公费超标调整金额fin_ipb_feeinfo出错!" + this.feeMgr.Err);
                    return -1;
                }
                //更新住院主表的费用信息
                FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
                int result=0;
                result = this.feeMgr.UpdateInmainInfoFT(info.ID, "0", ref ft);
                if (result == -1 ||result >1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新住院主表的费用信息出错!" + this.feeMgr.Err);
                    return -1;
                }

                //获取所有的记账药费
                decimal decTotPubPay = 0m;
                if (this.feeMgr.GetTotPubPayCost(info.ID,"0",ref decTotPubPay )==-1)
                {
                      FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("获取所有的记账药费出错!" + this.feeMgr.Err);
                    return -1;
                }

                //计算要调整的药费超标
                string flag = this.feeMgr.GetUpdateLimitOverTopAndTotFlag(info.ID);
                if (string.IsNullOrEmpty(flag) || flag != "M")
                {

                    info.FT.DayLimitTotCost = info.FT.DayLimitCost * PubDay;
                    info.FT.OvertopCost = decTotPubPay - info.FT.DayLimitTotCost;
                }
                else 
                {


                    info.FT.OvertopCost = decTotPubPay - info.FT.DayLimitTotCost;
                }

                if (info.FT.OvertopCost > 0)
                {
                    //调整日限额超标部分
                    if (this.feeMgr.AdjustOverLimitFee(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调整日限额超标部分出错!" + this.feeMgr.Err);
                        return -1;
                    }
                }


            }

            //查询患者费用结构
            //自费总金额
            decimal decOwnTotCost = 0m;
            ArrayList al = new ArrayList();
            //-----------------------------有调整四舍五入的费用，放后面查询
            //////al = this.feeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(info.ID, "0");
            //////if (al == null)
            //////{
            //////    FS.FrameWork.Management.PublicTrans.RollBack();

            //////    return -1;
            //////}
            //-----------------------------有调整四舍五入的费用，放后面查询
            //显示最小费用
            //如果是公费患者设置托收单信息
            FS.FrameWork.Public.ObjectHelper minfee = new FS.FrameWork.Public.ObjectHelper();
            minfee.ArrayObject = con.GetList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (this.pInfo.Pact.PayKind.ID == "03")//生育保险除外
            {
                #region 公费
                //托收单打印
                this.ucRep = new ucTrusteeBill();
                int iReturn = 0;

                FS.FrameWork.Models.NeuObject obj = myReport.GetStaticTime();
                if (obj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("托收单统计日期没有维护，请维护");
                    return -1;
                }

                DateTime now = myReport.GetDateTimeFromSysDateTime();
                DateTime statBegin;
                FS.FrameWork.Models.NeuObject tempRecordStateMonth = new FS.FrameWork.Models.NeuObject();
                //当前时间大于上期月结时间，算作下期日结.
                if (now > FS.FrameWork.Function.NConvert.ToDateTime(obj.User02))
                {
                    int year = 0;
                    int month = 0;

                    year = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);
                    month = FS.FrameWork.Function.NConvert.ToInt32(obj.Memo);

                    if (month == 12)
                    {
                        month = 1;
                        year = year + 1;
                    }
                    else
                    {
                        month = month + 1;
                    }
                    FS.FrameWork.Models.NeuObject objNext = myReport.GetStaticTime(year.ToString(), month.ToString());
                    if (objNext == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("托收单统计日期没有维护，请维护");
                        return -1;
                    }
                    //本次统计开始时间为下月统计的开始时间
                    statBegin = FS.FrameWork.Function.NConvert.ToDateTime(objNext.User01);

                    tempRecordStateMonth = objNext;
                }
                //本期范围内
                else
                {
                    statBegin = FS.FrameWork.Function.NConvert.ToDateTime(obj.User01);

                    tempRecordStateMonth = obj;
                }
                iReturn = myReport.InsertStatic(this.pInfo, "1", statBegin, FS.FrameWork.Function.NConvert.ToDateTime(tempRecordStateMonth.User02));
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("生成患者托收信息出错!" + myReport.Err);
                    return -1;
                }
               // Local.Report.Object.PubReport pReport = this.myReport.GetPubReport(pInfo.ID, FS.FrameWork.Function.NConvert.ToDateTime(tempRecordStateMonth.User02).Date.ToString());
                SOC.Local.GYZL.PubReport.Models.PubReport pReport = this.myReport.GetPubReport(pInfo.ID, FS.FrameWork.Function.NConvert.ToDateTime(tempRecordStateMonth.User02).Date.ToString());
                if (pReport == null)
                {
                    IsPrintTr = false;
                }
                else
                {
                    IsPrintTr = true;
                    this.ucRep.SetData(pInfo, pReport);
                }
                try
                {
                    this.ucRep.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel4.Controls.Add(this.ucRep);
                }
                catch { }

                al = this.feeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(info.ID, "0");
                if (al == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    return -1;
                }

                //总记账内的自负部分
                decimal payCost = 0m;
                ArrayList alOwn = new ArrayList();
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo infoPay;
                    infoPay = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                    payCost += infoPay.FT.PayCost;
                    if (infoPay.FT.OwnCost > 0)
                    {
                        alOwn.Add(al[i]);
                    }
                }
                //如果自付部分不为零，转入最小费用==>自付
                if (payCost > 0)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo pay = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    pay.Item.MinFee.ID = "ABC";
                    pay.FT.OwnCost = payCost;
                    pay.FT.TotCost = payCost;
                    alOwn.Add(pay);
                }

                //设置结算清单
                for (int i = 0; i < alOwn.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfo;
                    FeeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alOwn[i];
                    //获取最小费用名称
             
                    FeeInfo.Item.MinFee.Name = minfee.GetName(FeeInfo.Item.MinFee.ID);
                    if (FeeInfo.Item.MinFee.Name == "")
                    {
                        FeeInfo.Item.MinFee.Name = "自负";
                    }

                    decOwnTotCost += FeeInfo.FT.OwnCost;
                    //输入到Fps
                    int j = i / 5;
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j), FeeInfo.Item.MinFee.Name);
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j) + 1, FeeInfo.FT.OwnCost);

                }

                IsPrintTr = true;
                //显示公费费用信息
                decimal Jizhang = 0;
                Jizhang = this.pInfo.FT.TotCost - this.pInfo.FT.OwnCost;
                this.lblJiZhang.Text = Jizhang.ToString();
                this.lblBenQi.Text = this.ucRep.PubTotCost;


                #endregion
            }
            else
            {
                #region 非公费
                al = this.feeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(info.ID, "0");
                if (al == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    return -1;
                }

                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfo;
                    FeeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                    //获取最小费用名称
                    FeeInfo.Item.MinFee.Name = minfee.GetName(FeeInfo.Item.MinFee.ID);


                    //输入到Fps
                    int j = i / 5;
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j), FeeInfo.Item.MinFee.Name);
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j) + 1, FeeInfo.FT.TotCost);
                    decOwnTotCost += FeeInfo.FT.OwnCost + FeeInfo.FT.PayCost;
                }

                this.lblJiZhang.Text = "0";
                this.lblBenQi.Text = "0";
                #endregion
            }
            //提交
            FS.FrameWork.Management.PublicTrans.Commit();
            this.pInfo = this.patientMgr.QueryPatientInfoByInpatientNO(this.inPatientNo);
            this.pInfo.Memo = this.patientMgr.Operator.ID;
            this.DisplayPatient();

            //add 2011-4-21 ch
            //未清总计
            decimal WeiQing = decOwnTotCost ;
            this.lblWeiQing.Text = WeiQing.ToString();
            decimal Bujiao = 0m;
            Bujiao = WeiQing - this.pInfo.FT.PrepayCost;
            if (Bujiao >= 0)
            {
                this.lblNeedCh.Text = " 补 交：";
                this.lblBuJiao.Text = Bujiao.ToString();
            }
            else
            {
                this.lblNeedCh.Text = " 返 还：";
                Bujiao = -Bujiao;
                this.lblBuJiao.Text = Bujiao.ToString();
            }
            //复核
            this.lblFuHe.Text = this.pInfo.Memo;
            return 1;

        }
    
        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        private void DisplayPatient()
        {
            //在控件中显示患者的基本信息
            this.lblPatientNo.Text = this.pInfo.PID.PatientNO; ;
            this.lblName.Text = this.pInfo.Name;
            this.lblDept.Text = this.pInfo.PVisit.PatientLocation.Dept.Name;
            this.lblDateIn.Text = this.pInfo.PVisit.InTime.ToShortDateString();

            //出院日期和住院天数
            TimeSpan count;
            if (this.pInfo.PVisit.OutTime.Date.ToShortDateString() != "0001-01-01")
            {
                this.lblDateoOut.Text = this.pInfo.PVisit.OutTime.ToShortDateString();
                count = this.pInfo.PVisit.OutTime.Date - this.pInfo.PVisit.InTime.Date;
                int day = count.Days;
                if (day == 0)
                {
                    day = 1;
                }

                this.lblDate.Text = day.ToString();
            }
            else
            {
                DateTime now = DateTime.Now;
                this.lblDateoOut.Text = now.ToShortDateString();
                count = now.Date - this.pInfo.PVisit.InTime.Date;
                this.lblDate.Text = count.Days.ToString();
            }
            this.lblDatePrint.Text = DateTime.Now.ToShortDateString();
            this.lblType.Text = "出院结算";
            //显示患者费用信息

            this.lblPrePay.Text = this.pInfo.FT.PrepayCost.ToString();
            this.lblQingJiao.Text = this.pInfo.FT.BalancedPrepayCost.ToString();
            //this.pInfo.Patient
            //总费用和已清费用
            if (this.pInfo.FT.BalancedCost != 0)
            {
                decimal tot = this.pInfo.FT.BalancedCost + this.pInfo.FT.TotCost;
                this.lblZongJi.Text = tot.ToString();
                this.lblYiQing.Text = this.pInfo.FT.BalancedCost.ToString();
            }
            else
            {
                this.lblZongJi.Text = this.pInfo.FT.TotCost.ToString();
                this.lblYiQing.Text = "0";
            }

        }
        /// <summary>
        /// 根据住院号查询患者信息
        /// 此方法中没有判断住院号是否为空，请再调用前判断
        /// </summary>
        /// <param name="code">住院流水号</param>
        private int QueryPatient(string code)
        {
            //this.pInfo = this.patientMgr.PatientQuery(code);
            string temGJ = this.pInfo.Sex.User01;
            string temZLSP = this.pInfo.Sex.User02;

            this.pInfo = this.patientMgr.QueryPatientInfoByInpatientNO(code);
            this.pInfo.Sex.User01 = temGJ;
            this.pInfo.Sex.User02 = temZLSP;

            return this.QueryFee(this.pInfo);
        }
        private int InsertShiftData()
        {
            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.BizLogic.RADT.InPatient iptMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            oldObj.ID = "BTC";
            oldObj.Name = "封账";
            return (iptMgr.SetShiftData(this.inPatientNo,FS.HISFC.Models.Base.EnumShiftType.BP, "结算清单", oldObj, oldObj,this.pInfo.IsBaby));

        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 清空标签控件
        /// </summary>
        public void Clear()
        {
            try
            {
                this.lblBenQi.Text = "";
                this.lblBuJiao.Text = "";
                this.lblDate.Text = "";
                this.lblDateBalance.Text = "";
                this.lblDateIn.Text = "";
                this.lblDateoOut.Text = "";
                this.lblDatePrint.Text = "";
                this.lblDept.Text = "";
                this.lblFuHe.Text = "";
                this.lblJiZhang.Text = "";
                this.lblName.Text = "";
                this.lblPatientNo.Text = "";
                this.lblPrePay.Text = "";
                this.lblQingJiao.Text = "";
                this.lblType.Text = "";
                this.lblWeiQing.Text = "";
                this.lblYiQing.Text = "";
                this.lblZiJiao.Text = "";
                this.lblZongJi.Text = "";
                this.lblUpText.Text = "未清明细:";
                this.lblUpCost.Visible = false;
                this.fpSpread1_Sheet1.Cells[0, 0, 2, 9].Text = "";
                this.panel4.Controls.Clear();
            }
            catch { }
        }
        /// <summary>
        /// 根据住院流水号，对inPatientNo赋值,查询患者信息
        /// </summary>
        /// <param name="code">住院流水号</param>
        public int SetPatientNo(string code)
        {
            try
            {
                this.Clear();//清空显示信息
                if (code == null || code.Trim() == "")
                    return -1;
                this.inPatientNo = code;
                return this.QueryPatient(code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

        }
        public void Print()
        {
            if (this.pInfo == null || this.pInfo.ID == "")
            {
                MessageBox.Show("请输入患者住院号,并按回车键确认。", "提示");
                return;
            }
            if (this.InsertShiftData() == -1)
            {
                MessageBox.Show("插入信息变更表失败！");
                return;
            }

            try
            {
                //FS.neuFC.Interface.Classes.Print p = new FS.neuFC.Interface.Classes.Print();
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                //p.ControlBorder = FS.neuFC.Interface.Classes.enuControlBorder.None;
                //p.IsDataAutoExtend = false;
                //FS.Common.Class.Function.GetPageSize("newbill", ref p);
           
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                p.IsDataAutoExtend = false;
                //FS.Common.Class.Function.GetPageSize("newbill", ref p);
                FS.HISFC.Models.Base.PageSize ps = (new FS.HISFC.BizLogic.Manager.PageSize()).GetPageSize("newbill");
                p.SetPageSize(ps);
                //p.PrintPreview(this.panel1);
                p.PrintPage(ps.Left, ps.Top, this.panel1);

                //如果是公费患者打印托收单
                if (this.pInfo.Pact.PayKind.ID == "03" && this.IsPrintTr)
                    this.ucRep.Print();

            }
            catch { }
        }
        public int SetPatientFee(FS.HISFC.Models.RADT.PatientInfo info)
        {
            this.pInfo = info;
            //设置入院时间和结算时间
            this.dtBegin = info.PVisit.InTime;
            this.dtEnd = this.feeMgr.GetDateTimeFromSysDateTime();

            //公费患者，应该减去一天日限额			
            if (info.Pact.PayKind.ID == "03")
            {
                string strRep = "";
                DateTime RepDate;
                strRep = this.feeMgr.GetPubReportDate("1");
                if (strRep == "-1")
                {
                    MessageBox.Show("查询月结时间出错" + this.feeMgr.Err);
                    return -1;
                }
                RepDate = FS.FrameWork.Function.NConvert.ToDateTime(strRep);
                /*如果患者入院时间大于上次报表时间
                 * 取患者上次报表时间,
                 * 否则取入院时间为开始时间*/
                if (RepDate < info.PVisit.InTime)
                {
                    RepDate = info.PVisit.InTime;
                }
                TimeSpan DaysSpan = info.PVisit.OutTime.Date - RepDate.Date;
                int PubDay = DaysSpan.Days;
                if (PubDay == 0)
                {
                    PubDay = 1;
                }
            }

            //查询患者费用结构
            ArrayList al = new ArrayList();
            al = this.feeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(info.ID, "1");
            if (al == null)
            {
                return -1;
            }
            //医保患者
            if (this.pInfo.Pact.PayKind.ID == "02")
            {
                //				decimal UploadCost = 0m;
                //				if(this.feeMgr.GetUploadTotCost(pInfo.ID,ref UploadCost)==-1)
                //				{					
                //					MessageBox.Show("获取上传总费用出错！"+this.feeMgr.Err,"提示");
                //					return -1;
                //				}
                //				this.lblUpText.Text ="医保上传:";
                //				this.lblUpCost.Visible = true;
                //				this.lblUpCost.Text = UploadCost.ToString();

            }
            //显示最小费用
            //如果是公费患者设置托收单信息
            FS.FrameWork.Public.ObjectHelper minfee = new FS.FrameWork.Public.ObjectHelper();
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            minfee.ArrayObject = con.GetList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (this.pInfo.Pact.PayKind.ID == "03")//生育保险除外
            {
                #region 公费

                //总记账内的自负部分
                decimal payCost = 0m;
                ArrayList alOwn = new ArrayList();
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo infoPay;
                    infoPay = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                    payCost += infoPay.FT.PayCost;
                    if (infoPay.FT.OwnCost > 0)
                    {
                        alOwn.Add(al[i]);
                    }
                }
                //如果自付部分不为零，转入最小费用==>自付
                if (payCost > 0)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo pay = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    pay.Item.MinFee.ID = "ABC";
                    pay.FT.OwnCost = payCost;
                    pay.FT.TotCost = payCost;
                    alOwn.Add(pay);
                }
                //设置结算清单
                for (int i = 0; i < alOwn.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfo;
                    FeeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alOwn[i];
                    //获取最小费用名称
                    FeeInfo.Item.MinFee.Name = minfee.GetName(FeeInfo.Item.MinFee.ID);
                    if (FeeInfo.Item.MinFee.Name == "")
                    {
                        FeeInfo.Item.MinFee.Name = "公费自付";
                    }


                    //输入到Fps
                    int j = i / 5;
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j), FeeInfo.Item.MinFee.Name);
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j) + 1, FeeInfo.FT.OwnCost);

                }

                //显示公费费用信息
                decimal Jizhang = 0;
                Jizhang = this.pInfo.FT.TotCost - this.pInfo.FT.OwnCost;
                this.lblJiZhang.Text = Jizhang.ToString();
                this.lblBenQi.Text = this.ucRep.PubTotCost;


                #endregion
            }
            else
            {
                #region 非公费
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfo;
                    FeeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                    //获取最小费用名称
                    FeeInfo.Item.MinFee.Name = minfee.GetName(FeeInfo.Item.MinFee.ID);
                    //输入到Fps
                    int j = i / 5;
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j), FeeInfo.Item.MinFee.Name);
                    this.fpSpread1_Sheet1.SetValue(j, 2 * (i - 5 * j) + 1, FeeInfo.FT.TotCost);

                }

                this.lblJiZhang.Text = "0";
                this.lblBenQi.Text = "0";
                #endregion
            }
            this.pInfo = this.patientMgr.QueryPatientInfoByInpatientNO(info.ID);
            this.pInfo.Memo = this.patientMgr.Operator.ID;
            this.DisplayPatient();
            //未清总计
            decimal WeiQing = 0m;
            WeiQing = this.pInfo.FT.OwnCost + this.pInfo.FT.PayCost;
            this.lblWeiQing.Text = WeiQing.ToString();
            //补交或返还
            decimal Bujiao = 0m;
            Bujiao = WeiQing - this.pInfo.FT.PrepayCost;
            if (Bujiao >= 0)
            {
                this.lblNeedCh.Text = " 补 交：";
                this.lblBuJiao.Text = Bujiao.ToString();
            }
            else
            {
                this.lblNeedCh.Text = " 返 还：";
                Bujiao = -Bujiao;
                this.lblBuJiao.Text = Bujiao.ToString();
            }
            //复核
            this.lblFuHe.Text = this.pInfo.Memo;
            return 1;
        }
        public void Reprint()
        {
            //FS.neuFC.Interface.Classes.Print p = new FS.neuFC.Interface.Classes.Print();
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.ControlBorder = FS.neuFC.Interface.Classes.enuControlBorder.None;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsDataAutoExtend = false;
            //FS.Common.Class.Function.GetPageSize("newbill", ref p);
            FS.HISFC.Models.Base.PageSize ps = (new FS.HISFC.BizLogic.Manager.PageSize()).GetPageSize("newbill");
            p.SetPageSize(ps);
            p.PrintPage(ps.Left, ps.Top, this.panel1);

            //如果是公费患者打印托收单
            if (this.pInfo.Pact.PayKind.ID == "03" && this.IsPrintTr)
                this.ucRep.Print();
        }
        #endregion
        private void ucBalanceBill_Load(object sender, System.EventArgs e)
        {
            //初始化控件
            this.Init();
            //清空文本框
            this.Clear();
        }

        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo p)
        {
            this.pInfo = p;
            this.DisplayPatient();
            return;
        }
    }
}
