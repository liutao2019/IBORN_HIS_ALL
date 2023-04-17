using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IBillPrint
{
    public partial class ucSZBlanceInvoice1 : UserControl
    {
        public ucSZBlanceInvoice1()
        {
            InitializeComponent();
        }

        private System.Data.IDbTransaction trans;
        protected FS.HISFC.Models.Base.EBlanceType MidBalanceFlag;
        private string invoiceType;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;


        FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();

       // private FS.HISFC.Management.Fee.InPatient feeInpatient = new FS.HISFC.Management.Fee.InPatient();

       // private FS.HISFC.Management.Fee.Interface SiInterface = new FS.HISFC.Management.Fee.Interface();


        #region IBalanceInvoicePrintmy 成员

        //public FS.HISFC.Integrate.FeeInterface.EBlanceType IsMidwayBalance
        //{
        //    get
        //    {
        //        return MidBalanceFlag;
        //    }
        //    set
        //    {
        //        MidBalanceFlag = value;
        //    }
        //}

        #endregion

        #region IBalanceInvoicePrint 成员

        public int Clear()
        {
            return 0;
        }

        public string InvoiceType
        {
            get { return "ZY01"; }
        }

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set { patientInfo = value; }
        }

        public int Print()
        {
            //FS.NFC.Interface.Classes.Print prn = new FS.NFC.Interface.Classes.Print();
            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 850, 1100);
            //prn.SetPageSize(ps);
            //prn.PrintPage(0, 0, this);

            //FS.NFC.Interface.Classes.Print p = new FS.NFC.Interface.Classes.Print();
            //if (p == null)
            //{
            //    p = new FS.NFC.Interface.Classes.Print();

            //    FS.UFC.Common.Classes.Function.GetPageSize("ZYFP", ref p);//界面配置，信息科模块
            //    p.ControlBorder = FS.NFC.Interface.Classes.enuControlBorder.None;
            //}
            //System.Windows.Forms.Control c = this;

            //p.PrintPage(5, 1, c);


            return 0;
        }

        public int PrintPreview()
        {
            //FS.NFC.Interface.Classes.Print prn = new FS.NFC.Interface.Classes.Print();
            ////prn.PrintPage(0, 0, this);
            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 850, 1100);
            //prn.SetPageSize(ps);
            //prn.PrintPreview(0, 0, this);

            //FS.NFC.Interface.Classes.Print p = new FS.NFC.Interface.Classes.Print();
            //if (p == null)
            //{
            //    p = new FS.NFC.Interface.Classes.Print();

            //    FS.UFC.Common.Classes.Function.GetPageSize("ZYFP", ref p);//界面配置，信息科模块
            //    p.ControlBorder = FS.NFC.Interface.Classes.enuControlBorder.None;
            //}
            //System.Windows.Forms.Control c = this;

            //p.PrintPreview(5, 1, c);
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetValueForPrint(patientInfo, balanceInfo, alBalanceList, alPayList);
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            this.SetPatientInfo(patientInfo, balanceInfo, alBalanceList, alPayList);
            this.SetBalanceInfo(patientInfo, balanceInfo, alBalanceList, alPayList);
            return 1;
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo curPatientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo,
            System.Collections.ArrayList alBalanceList)
        {
            return 1;
        }

        public IDbTransaction Trans
        {
            set { this.trans = value; }
            get { return this.trans; }
        }

        #endregion

        /// <summary>
        /// 为患者信息的label控件赋值
        /// </summary>
        /// <returns></returns>
        private int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            //常数管理类
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject empStatusObj = new FS.FrameWork.Models.NeuObject();//人员状态

            //检查申请单业务层
           // FS.HISFC.Management.Order.CheckSlip checkSlip = new FS.HISFC.Management.Order.CheckSlip();

            try
            {
                if (patientInfo.SIMainInfo.PersonType.ID == "7")
                {
                    lblTitle.Text = "深圳市(少儿及大学生)住院医药费收据及收费项目清单";
                }
                else if(patientInfo.SIMainInfo.PersonType.ID =="4")
                {
                    lblTitle.Text = "深圳市家属统筹保险住院结账单";
                }
                if (!string.IsNullOrEmpty(patientInfo.Name))
                {
                    this.lblInpatientName.Visible = true;
                    this.lblInpatientName.Text = patientInfo.Name; 
                }//患者姓名
                ////医院名称
                //this.lblWorkPlaceName.Visible = true;
                //this.lblWorkPlaceName.Text = patientInfo.SIMainInfo.PersonType.Name; 
                //性别
                if (patientInfo.Sex !=null)
                {
                    
                    this.lblsex.Visible = true;
                    this.lblsex.Text = patientInfo.Sex.Name;
                }
                //年龄
                if (!string.IsNullOrEmpty(patientInfo.Age))
                {
                    this.lblage.Visible = true;
                    this.lblage.Text = patientInfo.Age;
                }
                //电脑号
                if (patientInfo != null)
                {
                    this.lblDNN.Visible = true;
                    this.lblDNN.Text = "";
                }

                //病区科室
                if (patientInfo.PVisit.PatientLocation != null)
                {
                    this.lblDepartmentType.Visible = true;
                    this.lblDepartmentType.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                }

                //入院诊断
                if (!string.IsNullOrEmpty(patientInfo.ClinicDiagnose))
                {
                    this.lblInDiagNose.Visible = true;
                    this.lblInDiagNose.Text = patientInfo.ClinicDiagnose.ToString();
                }
                //出院诊断
                if (!string.IsNullOrEmpty(patientInfo.SIMainInfo.OutDiagnose.Name))
                {
                    this.lbloutDiagNose.Visible = false; 
                    //try
                    //{
                    //    this.lbloutDiagNose.Visible = true;
                    //    this.lbloutDiagNose.Text = this.patientInfo.SIMainInfo.OutDiagnose.Name;
                    //}
                    //catch
                    //{
                    //    this.lbloutDiagNose.Visible = false;
                    //}
                }

                //单位编码
                if (patientInfo.SIMainInfo.Corporation != null)
                {
                    this.lblUnitCode.Visible = true;
                    this.lblUnitCode.Text = patientInfo.SIMainInfo.Corporation.ToString();
                }
                //医疗卡号
                if (!string.IsNullOrEmpty(patientInfo.SIMainInfo.ICCardCode ))
                {
                    this.lblMINo.Visible = true;
                    this.lblMINo.Text = patientInfo.SIMainInfo.ICCardCode;
                }

                if (patientInfo.PVisit.OutTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime)
                {
                    this.lblOutTime.Visible = true;
                    this.lblOutTime.Text = patientInfo.PVisit.OutTime.ToLongDateString();
                }//出院时间

                if (patientInfo.Pact.PayKind.ID != "01")//非自费患者
                //if (patientInfo.Pact.PayKind.ID != "01")//非自费患者 {2F3ACEBA-EFD5-4587-BCE2-603127FD0461}
                {
                    this.lblMINo.Visible = true;
                    this.lblMINo.Text = patientInfo.SSN;
                }//医保患者个人编号， addbyluoff20090915

                if (patientInfo.Pact.ID == "2")
                {
                    lblpaykind.Visible = true;
                    lblpaykind.Text = patientInfo.SSN;
                }

                if (!string.IsNullOrEmpty(patientInfo.PID.ID))
                {
                    this.lblInpatientNo.Visible = true;
                    this.lblInpatientNo.Text = patientInfo.PID.PatientNO;
                }//住院号

                if (patientInfo.PVisit.InTime!=null)
                {
                    this.lblInpatientTime.Visible = true;
                    this.lblInpatientTime.Text = patientInfo.PVisit.InTime.ToLongDateString();
                }//入院时间

              
                TimeSpan ts = new TimeSpan();
                if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                {
                    ts = DateTime.Now - patientInfo.PVisit.InTime;
                }
                else
                {
                    ts = patientInfo.PVisit.OutTime - FS.FrameWork.Function.NConvert.ToDateTime(patientInfo.PVisit.InTime.ToString("D") + " 00:00:00");
                }
                this.lblInpatientDay.Visible = true;
                this.lblInpatientDay.Text = ts.Days.ToString();

                //住院流水号
                if (patientInfo != null)
                {
                    this.lblInpatientNumber.Visible = true;
                    this.lblInpatientNumber.Text = patientInfo.ID;
                }

                #region 人员类别 addbyluoff20090915

                if (!patientInfo.Pact.PayKind.ID.Equals("01"))//非自费患者,
                {
                    this.lblInpatientType.Visible = true;

                    if (patientInfo.SIMainInfo.MedicalType.ID == "11")
                    {
                        neuObj = conMger.GetConstant("PersonType", patientInfo.SIMainInfo.PersonType.ID);//人员类别
                        empStatusObj = conMger.GetConstant("EmplType", patientInfo.SIMainInfo.EmplType);//人员状态
                        this.lblInpatientType.Text = neuObj.Name + "(" + empStatusObj.Name + ")";
                    }
                    else
                    {
                        neuObj = conMger.GetConstant("SZPERSONTYPE", patientInfo.PVisit.MedicalType.ID);
                        this.lblInpatientType.Text = neuObj.Name;
                    }
                }
                else
                {
                    this.lblInpatientType.Visible = true;
                    //this.lblInpatientType.Text = "自费患者";
                    this.lblInpatientType.Text = patientInfo.Pact.Name;
                }

                #endregion

                //{2F3ACEBA-EFD5-4587-BCE2-603127FD0461}
                //if (balanceInfo.Patient.Pact.PayKind.ID != "01")
                //{
                //    //this.SetMIInfo();
                //}
                //else
                //{
                //    this.SetSILableVis();
                //}

                //收款员
                if (FS.FrameWork.Management.Connection.Operator != null)
                {
                    this.lblPayOper.Visible = true;
                    this.lblPayOper.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
                }

                //收款日期
                //if (patientInfo.BalanceDate != null)
                //{
                this.lblDate.Visible = true;
                this.lblDate.Text = System.DateTime.Now.ToLongDateString();
                //}

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// 为费用信息label控件赋值
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceInfo"></param>
        /// <param name="alBalanceList"></param>
        /// <param name="alPayList"></param>
        /// <returns></returns>
        private int SetBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            #region 为fpBalance赋值

            int balanceList = alBalanceList.Count;
            if (patientInfo.PVisit.InState.ID.Equals("O"))  //已结算过病人 补打医保单
            {
                string invoiceNO = "";
                FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
                ArrayList alAllBill = inpatientFeeManager.QueryBalancesByInpatientNO(patientInfo.ID, "ALL");//出院结算发票。
         


                if (alAllBill == null)
                {
                   
                     return 1;
                }
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance alBalance in alAllBill)
                {
                    if (alBalance.Patient.Pact.ID == "2")
                    {
                        invoiceNO = alBalance.Invoice.ID;
                    }

                }
                    //只结算过一次

                    //FS.HISFC.Object.Fee.Inpatient.Balance balance = new FS.HISFC.Object.Fee.Inpatient.Balance();
                    //balance = (FS.HISFC.Object.Fee.Inpatient.Balance)alAllBill[0];
                    //invoiceNO = balance.Invoice.ID;
                   
            

                string[,] zfitem = this.GetSIZFitem();//获取基本基金支付项目
                string[,] xjitem = this.GetSIXJZFitem();//获取现金支付项目
                string[,] Jsxmitem = this.GetSIJSXMitem(); //获取结算项目 
                string[,] strJSMX = this.GetSIJsxmDetail(invoiceNO);
                string[,] strblZFmx = this.GetSIItemDetail(invoiceNO);
                int zjcount = 2;  //取基本记账行
                int xjcount = 8;  //现金行

                for (int i = 0; i < strJSMX.Length/strJSMX.Rank; i++)
                {
                   // string[] strJSMX = strblJs[i].Split(',');
                    if (i <= 16)
                    {
                        for (int n = 0; n < Jsxmitem.Length / Jsxmitem.Rank; n++)
                        {
                            if (Jsxmitem[n, 0] == strJSMX[i, 0])
                                fpBlance_Sheet1.Cells[i, 0].Text = Jsxmitem[n, 1];

                        }
                       // fpBlance_Sheet1.Cells[i, 0].Text = strJSMX[i,0];
                        fpBlance_Sheet1.Cells[i, 1].Text = strJSMX[i,1];
                    }
                    else
                    {
                        for (int n = 0; n < Jsxmitem.Length / Jsxmitem.Rank; n++)
                        {
                            if (Jsxmitem[n, 0] == strJSMX[i, 0])
                                fpBlance_Sheet1.Cells[i - 16, 2].Text = Jsxmitem[n, 1];    //Modified By Huangd  2011/10/24

                        }
                        //fpBlance_Sheet1.Cells[i - 16, 2].Text = strJSMX[i,0];
                        fpBlance_Sheet1.Cells[i - 16, 3].Text = strJSMX[i,1];
                    }

                }

                for (int m = 0; m < strblZFmx.Length/strblZFmx.Rank; m++)
                {

                   // string[] strblZFmx = strblZF[m].Split(',');
                    if (strblZFmx[m,0] == "02")  //记账支付
                    {
                        fpTotal_Sheet1.Cells[0, 1].Text = strblZFmx[m,1];
                    }

                    for (int zfcount = 0; zfcount < zfitem.Length / zfitem.Rank; zfcount++)
                    {
                        if (zjcount < 5)
                        {
                            if (zfitem[zfcount, 0] == strblZFmx[m,0])
                            {
                                fpTotal_Sheet1.Cells[zjcount, 0].Text = zfitem[zfcount, 1]; //支付项目名称
                                fpTotal_Sheet1.Cells[zjcount, 1].Text = strblZFmx[m,1]; //支付姓名金额
                                zjcount++;
                            }
                        }
                        else
                        {
                            if (zfitem[zfcount, 0] == strblZFmx[m,0])
                            {
                                fpTotal_Sheet1.Cells[zjcount - 6, 2].Text = zfitem[zfcount, 1]; //支付项目名称
                                fpTotal_Sheet1.Cells[zjcount - 6, 3].Text = strblZFmx[m,1]; //支付姓名金额
                                zjcount++;
                            }

                        }

                    }
                    if (strblZFmx[m,0] == "0306") //个人支付余额
                    {
                        if (Convert.ToDecimal(strblZFmx[m,1]) > 0)
                            fpTotal_Sheet1.Cells[3, 3].Text = strblZFmx[m,1];
                    }
                    if (strblZFmx[m,0] == "0304") //基本统筹最高限额
                    {
                        if (Convert.ToDecimal(strblZFmx[m,1]) > 0)
                            fpTotal_Sheet1.Cells[4, 3].Text = strblZFmx[m,1];
                    }
                    if (strblZFmx[m,0] == "0305")  //补充统筹最高限额
                    {
                        if (Convert.ToDecimal(strblZFmx[m,1]) > 0)
                            fpTotal_Sheet1.Cells[5, 3].Text = strblZFmx[m,1];
                    }
                    if (strblZFmx[m,0] == "03") //费用合计
                    {
                        fpBlance_Sheet1.Cells[18, 0].Text = "费用合计（大写）：" + FS.FrameWork.Function.NConvert.ToCapital(Convert.ToDecimal(strblZFmx[m, 1]));    
                        fpBlance_Sheet1.Cells[18, 3].Text = strblZFmx[m, 1];
                    }
                    // 现金
                    if (strblZFmx[m,0] == "01") //现金支付
                    {
                        fpTotal_Sheet1.Cells[6, 1].Text = strblZFmx[m,1];
                    }

                    for (int xfcount = 0; xfcount < xjitem.Length/xjitem.Rank; xfcount++)
                    {
                        if (xjcount <= 11)
                        {
                            if (strblZFmx[m,0] == xjitem[xfcount, 0])
                            {
                                fpTotal_Sheet1.Cells[xjcount, 0].Text = xjitem[xfcount, 1];
                                fpTotal_Sheet1.Cells[xjcount, 1].Text = strblZFmx[m,1];
                                xjcount++;

                            }
                        }
                        else
                        {
                            if (strblZFmx[m,0] == xjitem[xfcount, 0])
                            {
                                fpTotal_Sheet1.Cells[xjcount - 6, 2].Text = xjitem[xfcount, 1];//现金支付项目
                                fpTotal_Sheet1.Cells[xjcount - 6, 3].Text = strblZFmx[m,1];
                                xjcount++;

                            }
                        }

                    }

                 }
            }
            else
            {
                string[,] zfitem = this.GetSIZFitem();//获取基本基金支付项目
                string[,] xjitem = this.GetSIXJZFitem();//获取现金支付项目
                string[,] Jsxmitem = this.GetSIJSXMitem(); //获取结算项目 
                int zjcount = 2;  //取基本记账行
                int xjcount = 8;  //现金行

                if (patientInfo.SIMainInfo.User03 == null)
                {
                    return -1;
                }
                else
                {
                 string[] strblJs = patientInfo.SIMainInfo.User03.Split('|');  //结算项目
               

                for (int i = 0; i < strblJs.Length; i++)
                {
                    string[] strJSMX = strblJs[i].Split(',');
                    if (i <= 16)
                    {
                         for (int n = 0; n < Jsxmitem.Length / Jsxmitem.Rank; n++)
                         {
                             if (Jsxmitem[n, 0] == strJSMX[0])
                                 fpBlance_Sheet1.Cells[i, 0].Text = Jsxmitem[n,1];

                                 
                         }
                       // fpBlance_Sheet1.Cells[i, 0].Text = strJSMX[0];
                        fpBlance_Sheet1.Cells[i, 1].Text = strJSMX[1];
                    }
                    else
                    {
                        for (int n = 0; n < Jsxmitem.Length / Jsxmitem.Rank; n++)
                        {
                            if (Jsxmitem[n, 0] == strJSMX[0])
                                fpBlance_Sheet1.Cells[i - 16, 2].Text = Jsxmitem[n, 1];


                        }
                       // fpBlance_Sheet1.Cells[i - 16, 2].Text = strJSMX[0];
                        fpBlance_Sheet1.Cells[i - 16, 3].Text = strJSMX[1];
                    }

                }

               }
                if(patientInfo.SIMainInfo.User02 ==null)
                {
                  return -1;
                }
                else
                {
                string[] strblZF = patientInfo.SIMainInfo.User02.Split('|');
                for (int m = 0; m < strblZF.Length; m++)
                {

                    string[] strblZFmx = strblZF[m].Split(',');
                    if (strblZFmx[0] == "02")  //记账支付
                    {
                        fpTotal_Sheet1.Cells[0, 1].Text = strblZFmx[1];
                    }

                    for (int zfcount = 0; zfcount < zfitem.Length/zfitem.Rank; zfcount++)
                    {
                        if (zjcount < 5)
                        {
                            if (zfitem[zfcount, 0] == strblZFmx[0])
                            {
                               
                                fpTotal_Sheet1.Cells[zjcount, 0].Text = zfitem[zfcount, 1]; //支付项目名称
                                fpTotal_Sheet1.Cells[zjcount, 1].Text = strblZFmx[1]; //支付姓名金额
                                zjcount++;
                            }
                        }
                        else
                        {
                            if (zfitem[zfcount, 0] == strblZFmx[0])
                            {
                                fpTotal_Sheet1.Cells[zjcount - 6, 2].Text = zfitem[zfcount, 1]; //支付项目名称
                                fpTotal_Sheet1.Cells[zjcount - 6, 3].Text = strblZFmx[1]; //支付姓名金额
                                zjcount++;
                            }

                        }

                    }
                    if (strblZFmx[0] == "0306") //个人支付余额
                    {
                        if (Convert.ToDecimal(strblZFmx[1]) > 0)
                            fpTotal_Sheet1.Cells[3, 3].Text = strblZFmx[1];
                    }
                    if (strblZFmx[0] == "0304") //基本统筹最高限额
                    {
                        if (Convert.ToDecimal(strblZFmx[1]) > 0)
                            fpTotal_Sheet1.Cells[4, 3].Text = strblZFmx[1];
                    }
                    if (strblZFmx[0] == "0305")  //补充统筹最高限额
                    {
                        if (Convert.ToDecimal(strblZFmx[1]) > 0)
                            fpTotal_Sheet1.Cells[5, 3].Text = strblZFmx[1];
                    }

                    if (strblZFmx[0] == "03") //费用合计
                    {
                        fpBlance_Sheet1.Cells[18, 0].Text = "费用合计（大写）：" + FS.FrameWork.Function.NConvert.ToCapital(Convert.ToDecimal(strblZFmx[1]));
                        fpBlance_Sheet1.Cells[18, 3].Text = strblZFmx[1];
                    }
                    // 现金
                    if (strblZFmx[0] == "01") //现金支付
                    {
                        fpTotal_Sheet1.Cells[6, 1].Text = strblZFmx[1];
                    }

                    for (int xfcount = 0; xfcount < xjitem.Length/xjitem.Rank; xfcount++)
                    {
                        if (xjcount <= 11)
                        {
                            if (strblZFmx[0] == xjitem[xfcount, 0])
                            {
                                fpTotal_Sheet1.Cells[xjcount, 0].Text = xjitem[xfcount, 1];
                                fpTotal_Sheet1.Cells[xjcount, 1].Text = strblZFmx[1];
                                xjcount++;

                            }
                        }
                        else
                        {
                            if (strblZFmx[0] == xjitem[xfcount, 0])
                            {
                                fpTotal_Sheet1.Cells[xjcount - 6, 2].Text = xjitem[xfcount, 1];//现金支付项目
                                fpTotal_Sheet1.Cells[xjcount - 6, 3].Text = strblZFmx[1];
                                xjcount++;

                            }
                        }

                    }
                 }

                }


            }
          

            //预收金额
            fpTotal_Sheet1.Cells[9, 3].Text = patientInfo.FT.PrepayCost.ToString();
            //补收金额
            fpTotal_Sheet1.Cells[10, 3].Text = patientInfo.FT.SupplyCost.ToString();
            //找赎金额
            fpTotal_Sheet1.Cells[11, 3].Text = patientInfo.FT.ReturnCost.ToString();

            #endregion

            return 1;
        }
        /// <summary>
        /// 取现金支付部分信息
        /// </summary>
        /// <returns></returns>
        private string[,] GetSIXJZFitem()
        {
            string StrSql = "select a.code,a.name from com_dictionary a where a.type ='SZZFTYPE' and a.valid_state ='1' and a.input_code ='01'";

            DataSet dsItem = new DataSet();

            Manager.ExecQuery(StrSql, ref dsItem);

            string[,] itemDetail = new string[0, 2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;
        
        }


        /// <summary>
        /// 获取住院医保结算项目信息
        /// </summary>
        /// <returns></returns>
        private string[,] GetSIJSXMitem()
        {
            string StrSql = "select a.code,a.name from com_dictionary a where a.type ='CENTERFEECODE' and a.valid_state ='1'";

            DataSet dsItem = new DataSet();

            Manager.ExecQuery(StrSql, ref dsItem);

            string[,] itemDetail = new string[0, 2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;


        }
        /// <summary>
        /// 获取住院医保支付项目信息
        /// </summary>
        /// <returns></returns>
        private string[,] GetSIZFitem()
        {
            string StrSql = "select a.code,a.name from com_dictionary a where a.type ='SZZFTYPE' and a.valid_state ='1' and a.input_code ='02'";

            DataSet dsItem = new DataSet();

            Manager.ExecQuery(StrSql, ref dsItem);

            string[,] itemDetail = new string[0, 2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;

        
        }
        /// <summary>
        /// 获取住院结算项目
        /// </summary>
        /// <param name="InpatientNO"></param>
        /// <returns></returns>
        private string[,] GetSIJsxmDetail(string InpatientNO)
        {
            string strSql = "select t.jsxm ,t.je from si_yb_zyjsxm t where t.fphm = '" + InpatientNO + "' and t.trantype ='1' ";
            DataSet dsItem = new DataSet();

            Manager.ExecQuery(strSql, ref dsItem);
            string[,] itemDetail = new string[0, 2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;

        
        }

        /// <summary>
        /// 获取住院结算信息支付项目
        /// </summary>
        /// <param name="InpatientNO">住院流水号</param>
        /// <returns>住院详细信息</returns>
        private string[,] GetSIItemDetail(string InpatientNO)
        {
            string strSql = "select t.zfxm, t.je from si_yb_zyzf t where t.fphm = '" + InpatientNO + "' and t.trantype ='1'"; 
                //" where t.zylsh = '' --住院流水号 ";

            DataSet dsItem = new DataSet();
            Manager.ExecQuery(strSql, ref dsItem);

            string[,] itemDetail = new string[0,2];

            if (dsItem.Tables[0].Rows.Count > 0)
            {
                itemDetail = new string[dsItem.Tables[0].Rows.Count, 2];

                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    itemDetail[i, 0] = dsItem.Tables[0].Rows[i][0].ToString();
                    itemDetail[i, 1] = dsItem.Tables[0].Rows[i][1].ToString();
                }
            }
            return itemDetail;
        }

        /// <summary>
        /// 自费患者Label赋值
        /// </summary>
        private void SetSILableVis()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Tag.ToString() == "SI")
                {
                    c.Visible = false;
                }
                else
                {
                    
                }
            }
        }

        /// <summary>
        /// 获得label控件
        /// </summary>
        /// <param name="labelName"></param>
        /// <param name="labelIndex"></param>
        /// <returns></returns>
        private Label GetLabel(string labelName,string labelIndex)
        {
            Control l=null;
            foreach (Control c in this.Controls)
            {
                if (c.Name == labelName + labelIndex)
                {
                    l = c;
                    break;
                }
            }
            return (Label)l;
        }
    }
}
