using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace InterfaceInstanceDefault.IBalanceInvoicePrintmy
{
    /// <summary>
    /// ucZZFYBalanceInvoicePrint<br></br>
    /// [功能描述: 郑州住院发票<br></br>//{5D2A0629-A594-468d-9F6F-42119405A080}
    /// [创 建 者: 董国强]<br></br>
    /// [创建时间: 2010-08-05]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucZZFYBalanceInvoicePrint : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy
    {
        /// <summary>
        /// 字段：是否是现场发票打印，默认false
        /// </summary>
        private bool isZZFYPrint = false;

        /// <summary>
        /// 字段：是否是现场发票打印，默认false
        /// </summary>
        public bool IsZZFYPrint
        {
            get { return isZZFYPrint; }
            set
            {
                isZZFYPrint = value;
                if (isZZFYPrint)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        if (ctrl.GetType().FullName == "System.Windows.Forms.Label")
                        {
                            if (ctrl.Name.StartsWith("neu"))
                            {
                                ctrl.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        

        public ucZZFYBalanceInvoicePrint()
        {
            IsZZFYPrint = false ;
            InitializeComponent();
        }


        #region IBalanceInvoicePrintmy 成员

        /// <summary>
        /// 字段：中途结算标记
        /// </summary>
        protected FS.HISFC.Models.Base.EBlanceType MidBalanceFlag;
        /// <summary>
        /// 属性：中途结算标记
        /// </summary>
        public FS.HISFC.Models.Base.EBlanceType IsMidwayBalance
        {
            get
            {
                return MidBalanceFlag;
            }
            set
            {
                MidBalanceFlag = value;
            }
        }

        #endregion

        #region IBalanceInvoicePrint 成员

        public int Clear()
        {
            return 1;
        }

        private string invoiceType;

        /// <summary>
        /// 发票类别
        /// </summary>
        public string InvoiceType
        {
            get { return invoiceType; }
        }

        private FS.HISFC.Models.RADT.PatientInfo patientInfo;

        /// <summary>
        /// 患者实体
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                patientInfo = value;
                //if (patientInfo.Pact.PayKind.ID == "01")
                //{
                    //自费
                    invoiceType = "ZY01";
                //}
                //else if (patientInfo.Pact.PayKind.ID == "02")
                //{
                //    //市医保
                //    invoiceType = "ZY02";
                //}
            }
        }

        public int Print()
        {
            //设置为非现场打印
            IsZZFYPrint = false;
            //

            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                FS.HISFC.Models.Base.PageSize ps = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);

                    return -1;
                }
                string paperName = string.Empty;
                if (this.InvoiceType == "ZY02")
                {
                    paperName = "ZYYBFP";

                }
                else if (this.InvoiceType == "ZY01")
                {
                    paperName = "ZYXJFP";
                }
                ps = new FS.HISFC.Models.Base.PageSize(paperName, 0, 0);
                ps.Top = 0;
                ps.Left = 0;
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }

            return 1;
        }

        public int PrintPreview()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                FS.HISFC.Models.Base.PageSize ps = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);

                    return -1;
                }
                string paperName = string.Empty;
                if (this.InvoiceType == "ZY02")
                {
                    paperName = "ZYZF";

                }
                else if (this.InvoiceType == "ZY01")
                {
                    paperName = "ZYYB";
                }
                ps = new FS.HISFC.Models.Base.PageSize(paperName, 0, 0);
                ps.Top = 0;
                ps.Left = 0;
                print.SetPageSize(ps);
                print.PrintPreview(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 1;
            }

            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            return;
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetPrintValue(patientInfo, balanceInfo, alBalanceList, true);
        }

        private int SetPrintValue(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceHead, System.Collections.ArrayList alBalanceList, bool IsPreview)
        { 
            #region 画发票

            //System.Drawing.Graphics g = this.CreateGraphics();
            //if (g!=null)
            //{
            //    g.DrawString("河南省非营利性医疗机构住院收据", new System.Drawing.Font("黑体", 14, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Purple, new System.Drawing.PointF(300, 10));
            //    g.DrawString("专用票据", new System.Drawing.Font("黑体", 14, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Purple, new System.Drawing.PointF(150, 40));
            //    g.DrawString("票据代码：410008003", new System.Drawing.Font("黑体", 10, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Purple, new System.Drawing.PointF(600, 40));
            //    g.DrawString("票据批次：RA[2009]", new System.Drawing.Font("黑体", 10, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Purple, new System.Drawing.PointF(600, 60));


            //    //
            //    g.DrawString("姓    名：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(0, 80));
            //    g.DrawString("科    室：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(150, 80));
            //    g.DrawString("医 保 号：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(300, 80));
            //    g.DrawString("      No：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(450, 80));

            //    g.DrawString("住 院 号：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(0, 100));
            //    g.DrawString("住院日期：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(150, 100));
            //    g.DrawString("出院日期：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(300, 100));
            //    g.DrawString("结算形式：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(450, 100));

            //    //边框
            //    g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 2), new System.Drawing.Rectangle(20, 130, 790, 360));

            //    //横线
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 20, 160, 810, 160);

            //    System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1);
            //    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            //    g.DrawLine(p, 20, 190, 810, 190);
            //    g.DrawLine(p, 20, 220, 810, 220);
            //    g.DrawLine(p, 20, 250, 810, 250);
            //    g.DrawLine(p, 20, 280, 810, 280);
            //    g.DrawLine(p, 20, 310, 810, 310);

            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 20, 340, 810, 340);

            //    g.DrawLine(p, 20, 370, 810, 370);

            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 20, 400, 810, 400);
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 20, 430, 810, 430);
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 20, 460, 810, 460);

            //    //竖线
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 200, 130, 200, 340);
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 400, 130, 400, 340);
            //    g.DrawLine(new System.Drawing.Pen(System.Drawing.Brushes.Purple, 1), 600, 130, 600, 340);

            //    g.DrawString("项目", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(50, 140));
            //    g.DrawString("项目", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(250, 140));
            //    g.DrawString("项目", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(450, 140));
            //    g.DrawString("项目", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(650, 140));

            //    g.DrawString("金额", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(100, 140));
            //    g.DrawString("金额", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(300, 140));
            //    g.DrawString("金额", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(500, 140));
            //    g.DrawString("金额", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(700, 140));

            //    g.DrawString("自费费用：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 350));
            //    g.DrawString("个人自付：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(190, 350));
            //    g.DrawString("起付标准：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(350, 350));
            //    g.DrawString("按比例自付：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(510, 350));
            //    g.DrawString("统筹记账：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(670, 350));

            //    g.DrawString("大额记账：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 380));
            //    g.DrawString("超大额记账：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(190, 380));
            //    g.DrawString("公务员记账：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(350, 380));
            //    g.DrawString("个人账户：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(510, 380));
            //    g.DrawString("现金支付：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(670, 380));


            //    g.DrawString("费用合计：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 410));
            //    g.DrawString("预收押金：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 440));
            //    g.DrawString("实收金额(大写)：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 470));
            //    g.DrawString("流水号：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(30, 500));

            //    g.DrawString("制单：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(190, 500));

            //    g.DrawString("医保记账：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(350, 410));
            //    g.DrawString("退押金：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(350, 440));
            //    g.DrawString("结算员：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(350, 500));

            //    g.DrawString("补交款：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(510, 440));
            //    g.DrawString("￥：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(510, 470));
            //    g.DrawString("单位盖章：", new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Purple, new System.Drawing.PointF(510, 500));
            //}
            #endregion

            #region 设置自费发票打印内容
  
            FS.HISFC.BizProcess.Integrate.Manager mgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //打票单位
            this.lblHospitalName.Text = mgr.GetHospitalName();

            //姓名
            this.lblPatientName.Text = patientInfo.Name;

            //住院科室
            this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

            //医保号
            //

            //住院号码
            this.lblInNo.Text = patientInfo.PID.PatientNO;

            //住院日期
            this.lblInTime.Text = balanceHead.BeginTime.ToString("yyyy.MM.dd");

            //出院日期
            if (MidBalanceFlag == FS.HISFC.Models.Base.EBlanceType.Mid)
            {
                this.lblOutTime.Text = balanceHead.EndTime.ToString("yyyy.MM.dd");
                //this.lblInDay.Text = new TimeSpan(balanceHead.EndTime.Ticks - balanceHead.BeginTime.Ticks).Days.ToString();
            }
            else
            {
                this.lblOutTime.Text = patientInfo.PVisit.OutTime.ToString("yyyy.MM.dd");
                //this.lblInDay.Text = new TimeSpan(patientInfo.PVisit.OutTime.Ticks - balanceHead.BeginTime.Ticks).Days.ToString();
            }

            //结算形式
            //this.lblPayKind.Text = patientInfo.PayKind;

            #region 具体的项目
            //Dictionary<string, decimal> FeeItems = new Dictionary<string, decimal>();


            for (int i = 0; i < alBalanceList.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList detail = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                detail = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
                //if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                //{
                    foreach (System.Windows.Forms.Control ctrl in this.Controls.Find("lblItem" + i.ToString(),true))
                    {
                        ctrl.Visible = true;
                        ctrl.Text = detail.FeeCodeStat.StatCate.Name;
                    }

                    foreach (System.Windows.Forms.Control ctrl in this.Controls.Find("lblItemFee" + i.ToString(), true))
                    {
                        ctrl.Visible = true;
                        ctrl.Text = detail.BalanceBase.FT.TotCost.ToString();
                    }
                //}
            }
                        
                        
                        
                        
            //            , detail.BalanceBase.FT.TotCost);
            //        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
            //        if (detail.FeeCodeStat.StatCate.Name.Length > 5)
            //        {
            //            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.StatCate.Name.Substring(0, 5);
            //        }
            //        else
            //        {
            //            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.StatCate.Name;
            //        }
            //    }
            //}
            //int feeInfoNameIdx = 0;
            //int FeeInfoIndex = 0;
            //foreach (decimal d in FeeInfo)
            //{
            //    //测试用
            //    //FeeInfo[FeeInfoIndex] = 999999.99m;
            //    //名称
            //    Label lName = GetFeeNameLable("lblFeeName" + feeInfoNameIdx.ToString(), lblPrint);
            //    Label lValue = GetFeeNameLable("lblFeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
            //    if (lName != null)
            //    {
            //        if (FeeInfo[FeeInfoIndex] > 0)
            //        {
            //            //测试用
            //            //lName.Text = FeeInfoName[feeInfoNameIdx] + "普通人工器官材料费";
            //            //lName.Text = FeeInfoName[FeeInfoIndex] + "";
            //            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');
            //            feeInfoNameIdx++;
            //        }
            //    }
            //    feeInfoNameIdx++;
            //    FeeInfoIndex++;
            //}

            //for (int ridx = 0; ridx < 24; ridx++) 
            //{
            //    for (int cidx = 0; cidx < 6; cidx++)
            //    {

            //    }
            //}

            //for (int i = 0; i < alBalanceList.Count; i++) 
            //{

            //}




            //----01----医疗费总额TotCost
            //----02----个人帐户支付金额PayCost
            //----03----统筹支付金额PubCost
            //----04----个人现金支付OwnCost
            //----05----救助金支出金额OverCost
            //----06----公务员补助支出金额OfficalCost
            //----07----保健对象补贴支出BaseCost
            //----08----离休人员统筹支出PubOwnCost
            //----09----医院付担金额HosCost
            //----10----上次进入统筹医疗费用累计
            //----11----本次进入统筹医疗费用金额
            //----12----上次个人帐户余额
            //----13----个人自费金额
            //----14----乙类药品个人自理||||住院封顶线以上公务员补助支出金额
            //----15----起付标准自付金额
            //----16----分段自理金额
            //----17----超过封顶线个人自付金额
            //----18----住院自付部分公务员补助支出金额
            //----19----住院人次
            //----20----工伤基金支付金额
            //----21----生育基金支付金额

            
            //string[] temp = patientInfo.SIMainInfo.Memo.Split('|');
            //this.lblTemp18.Text = temp[18];
            //this.lblTemp6.Text = temp[6];
            //this.lblTemp7.Text = temp[7];
            //this.lblTemp8.Text = temp[8];
            //this.lblTemp20.Text = temp[20];
            //this.lblTemp21.Text = temp[21];
            //this.lblTemp15.Text = temp[15];
            //this.lblTemp13.Text = temp[13];


            ////票面信息
            //string[] FeeInfoName =
            //    //---------------------1-----------2------------3------------4-------------5-----------------
            //    new string[18] { string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
            //                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
            //                     //string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
            //                     string.Empty,string.Empty,string.Empty,//string.Empty,
            //                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty};

            ////票面信息
            //decimal[] FeeInfo =
            //    //---------------------1-----------2------------3------------4-------------5-----------------
            //    new decimal[18]{decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
            //                    decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
            //                    //decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
            //                    decimal.Zero,decimal.Zero,decimal.Zero,//decimal.Zero,
            //                    decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};


            //for (int i = 0; i < alBalanceList.Count; i++)
            //{
            //    FS.HISFC.Models.Fee.Inpatient.BalanceList detail = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
            //    detail = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];
            //    if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
            //    {
            //        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
            //        if (detail.FeeCodeStat.StatCate.Name.Length > 5)
            //        {
            //            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.StatCate.Name.Substring(0, 5);
            //        }
            //        else
            //        {
            //            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.StatCate.Name;
            //        }
            //    }
            //}
            //int feeInfoNameIdx = 0;
            //int FeeInfoIndex = 0;
            //foreach (decimal d in FeeInfo)
            //{
            //    //测试用
            //    //FeeInfo[FeeInfoIndex] = 999999.99m;
            //    //名称
            //    Label lName = GetFeeNameLable("lblFeeName" + feeInfoNameIdx.ToString(), lblPrint);
            //    Label lValue = GetFeeNameLable("lblFeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
            //    if (lName != null)
            //    {
            //        if (FeeInfo[FeeInfoIndex] > 0)
            //        {
            //            //lName.Text = FeeInfoName[feeInfoNameIdx] + "普通人工器官材料费";
            //            // lName.Text = FeeInfoName[FeeInfoIndex] + "";
            //            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');

            //        }
            //    }
            //    FeeInfoIndex++;
            //    feeInfoNameIdx++;
            //}

            #endregion

            #region 医保相关信息

            //this.lblSSNTitle.Visible = false;
            //this.lblPayCostTitle.Visible = false;
            //this.lblPubCostTitle.Visible = false;
            //this.lblOwnCost.Visible = false;
            //this.lblPubCost.Visible = false;
            //this.lblPayCost.Visible = false;
            //this.lblOverCost.Visible = false;
            //this.lblSSN.Visible = false;
            //this.lblOwnCostTitle.Visible = false;

            //if (balanceHead.Patient.Pact.PayKind.ID == "02")
            //{
            //    this.lblSSNTitle.Visible = true;
            //    this.lblPayCostTitle.Visible = true;
            //    this.lblPubCostTitle.Visible = true;
            //    this.lblOwnCost.Visible = true;
            //    this.lblPubCost.Visible = true;
            //    this.lblPayCost.Visible = true;
            //    this.lblOverCost.Visible = true;
            //    this.lblSSN.Visible = true;
            //    this.lblOwnCostTitle.Visible = true;
            //    this.lblSSN.Text = patientInfo.SSN;
            //    decimal GeRenCost = decimal.Zero;
            //    decimal TongChouCost = decimal.Zero;
            //    decimal XianJinCost = decimal.Zero;
            //    decimal GongWuYuanCost = decimal.Zero;
            //    decimal DaECost = decimal.Zero;

            //    //自费费用
            //    //

            //    //个人自付
            //    //

            //    //起付标准
            //    //

            //    //按比例自付
            //    //

            //    //统筹记账
            //    TongChouCost = patientInfo.SIMainInfo.PubCost;
            //    if (TongChouCost != 0)
            //    {
            //        this.lblPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(TongChouCost, 2);
            //    }

            //    //大额记账
            //    DaECost = patientInfo.SIMainInfo.OverCost;
            //    if (DaECost != 0)
            //    {
            //        this.lblOverCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DaECost, 2);
            //    }

            //    //超大额记账
            //    //

            //    //公务员记账
            //    GongWuYuanCost = patientInfo.SIMainInfo.OfficalCost;
            //    if (GongWuYuanCost > 0)
            //    {
            //        this.lblGongWuYuanCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GongWuYuanCost, 2);
            //    }

            //    //个人账户
            //    GeRenCost = patientInfo.SIMainInfo.PayCost;
            //    if (GeRenCost != 0)
            //    {
            //        this.lblPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GeRenCost, 2);
            //    }

            //    //现金支付
            //    XianJinCost = patientInfo.SIMainInfo.OwnCost;
            //    if (XianJinCost != 0)
            //    {
            //        this.lblOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(XianJinCost, 2);
            //        this.lblUpOwnCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(FS.FrameWork.Public.String.FormatNumber(XianJinCost, 2));
            //    }

            //}


            #endregion           

            //费用合计
            if (balanceHead.FT.TotCost != 0)
            {
                this.lblTotAll.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);
            }

            //医保记账
            //

            //预收押金
            if (balanceHead.FT.PrepayCost - balanceHead.FT.BalancedPrepayCost != 0)
            {
                this.lblPriPrepay.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.PrepayCost - balanceHead.FT.BalancedPrepayCost, 2);
            }
            //退押金
            if (balanceHead.FT.ReturnCost != 0)
            {
                this.lblPriReturn.Text = FS.FrameWork.Public.String.FormatNumberReturnString(0 - balanceHead.FT.ReturnCost, 2);
            }
            //补交款
            if (balanceHead.FT.SupplyCost != 0)
            {
                this.lblPriAdd.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.SupplyCost, 2);
            }
            //实收金额(大写)
            if (balanceHead.FT.TotCost != 0)
            {
                this.lblUpTotCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(FS.FrameWork.Public.String.FormatNumber(balanceHead.FT.TotCost, 2));
            }
            //实收金额
            if (balanceHead.FT.TotCost != 0)
            {
                this.lblTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(balanceHead.FT.TotCost, 2);
            }

            //流水号
            this.lblInvoiceNo.Text = balanceHead.Invoice.ID;

            //制单
            //

            //结算员
            this.lblOperater.Text = new FS.HISFC.BizLogic.Manager.Person().GetPersonByID(balanceHead.BalanceOper.ID).Name;

            //发票号
            //this.lblInvoiceNO.Text = balanceHead.Invoice.ID;
            
            
            #endregion
            

            return 0;
        }

        public System.Data.IDbTransaction Trans
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
