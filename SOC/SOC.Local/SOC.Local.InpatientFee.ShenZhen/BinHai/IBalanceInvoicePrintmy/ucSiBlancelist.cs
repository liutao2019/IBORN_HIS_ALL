using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IBillPrint
{
    public partial class ucSiBlancelist : UserControl//, FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy
    {
        public ucSiBlancelist()
        {
            InitializeComponent();
        }

        private System.Data.IDbTransaction trans;
        protected FS.HISFC.Models.Base.EBlanceType MidBalanceFlag;
        private string invoiceType;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;


        #region IBalanceInvoicePrint 成员

        public int Clear()
        {
            return 0;
        }

        public string InvoiceType
        {
            get { return "ZY01"; }
        }

   
        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 850,1100);
            prn.SetPageSize(ps);
            prn.PrintPage(0, 0, this);

 
            return 0;
        }

        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            //prn.PrintPage(0, 0, this);
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 850, 1100);
            prn.SetPageSize(ps);
            prn.PrintPreview(0, 0, this);

 
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans; 
        }
        public int SetValueForPreviewNew(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetValueForPrint(patientInfo, balanceInfo, alBalanceList, alPayList);
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            return this.SetValueForPrint(patientInfo,balanceInfo,alBalanceList,alPayList);
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
           // FS.HISFC.BizLogic.Order.CheckSlip checkSlip = new FS.HISFC.BizLogic.Order.CheckSlip();

            try
            {
                //设置打印标题
                if (patientInfo.SIMainInfo.PersonType != null)
                {
                    this.lblTitle.Text = this.lblTitle.Text.Replace(" ", patientInfo.SIMainInfo.PersonType.Name);
                }
                else
                {
                    this.lblTitle.Text = this.lblTitle.Text.Replace(" ", "");
                }

                if (patientInfo.Name != null)
                {
                    this.lblInpatientName.Visible = true;
                    this.lblInpatientName.Text = patientInfo.Name;
                }//患者姓名

                if (patientInfo.Age != null)
                {
                    this.lblage.Visible = true;
                    this.lblage.Text = patientInfo.Age;
                }//年龄

                if (patientInfo.Sex != null)
                {
                    this.lblsex.Visible = true;
                    this.lblsex.Text = patientInfo.Sex.ToString();
                }//性别

                if (patientInfo.SIMainInfo.InDiagnose != null)
                {
                    this.lblInDiagNose.Visible = true;
                    this.lblInDiagNose.Text = patientInfo.SIMainInfo.InDiagnose.ID;
                }//入院诊断

                if (patientInfo.SIMainInfo.OutDiagnose != null)
                {
                    this.lbloutDiagNose.Visible = true;
                    this.lbloutDiagNose.Text = patientInfo.SIMainInfo.OutDiagnose.ID;
                }//出院诊断

                if (patientInfo.IDCard != null)
                {
                    this.lblIDCard.Visible = true;
                    this.lblIDCard.Text = patientInfo.IDCard;
                }//身份证号

                if (patientInfo.PhoneHome != null)
                {
                    this.lbllxdh.Visible = true;
                    this.lbllxdh.Text = patientInfo.PhoneHome;
                }//电话
                    if (patientInfo.Patient.Pact.PayKind.ID != "01")//非自费患者
                //if (patientInfo.Pact.PayKind.ID != "01")//非自费患者 {2F3ACEBA-EFD5-4587-BCE2-603127FD0461}
                {
                    this.lblMINo.Visible = true;
                    this.lblMINo.Text = patientInfo.SSN;
                }//医保患者个人编号， addbyluoff20090915

                if (patientInfo.CompanyName != null)
                {
                    this.lblDNN.Visible = true;
                    this.lblDNN.Text = "";
                }//电脑号
                
                if (patientInfo.PID.ID != null)
                {
                    this.lblInpatientNo.Visible = true;
                    this.lblInpatientNo.Text = patientInfo.PID.ID;
                }//住院号

                if (patientInfo.PVisit.InTime != null)
                {
                    this.lblInpatientTime.Visible = true;
                    this.lblInpatientTime.Text = patientInfo.PVisit.InTime.ToLongDateString();
                }//入院时间

                if (patientInfo.PVisit.OutTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime && patientInfo.PVisit.InTime != null)
                {
                    this.lblInpatientDay.Visible = true;
                    //DateTime dt = new DateTime();
                    TimeSpan ts = new TimeSpan();
                    //dt= (DateTime)(patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime);
                    ts = patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime;
                    //this.lblInpatientDay.Text = dt.Day.ToString();
                    this.lblInpatientDay.Text = ts.Days.ToString();
                }//在院天数

                if (patientInfo.PVisit.OutTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime )
                {
                    this.lblOutTime.Visible = true;
                    this.lblOutTime.Text = patientInfo.PVisit.OutTime.ToLongDateString();
                }//出院时间

                //if (balanceInfo.Patient.Pact.PayKind.ID != "01")
                ////if (this.patientInfo.Pact.PayKind.ID != "01"){2F3ACEBA-EFD5-4587-BCE2-603127FD0461}
                //{
                //    this.SetMIInfo();
                //}
                //else
                //{
                //    this.SetSILableVis();
                //}

                if (patientInfo.PVisit.PatientLocation.NurseCell != null)
                {
                    this.lblInpatientDept.Visible = true;
                    this.lblInpatientDept.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
                }//入院病区

                if (patientInfo.PVisit.PatientLocation.NurseCell != null)
                {
                    this.lblOutpatientDept.Visible = true;
                    this.lblOutpatientDept.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
                }//出院病区

                if(patientInfo.ID != null)
                {
                    this.lblInpatientNumber.Visible = true;
                    this.lblInpatientNumber.Text = patientInfo.ID;
                }//住院流水号

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
            #region 为fpBlance控件赋值
            int balanceList = alBalanceList.Count;

            for (int i = 0; i < balanceList; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList bl = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];

                fpBlance_Sheet1.Cells[i, 0].Text = bl.FeeCodeStat.StatCate.Name.ToString(); //结算项目

                fpBlance_Sheet1.Cells[i, 1].Text = bl.BalanceBase.FT.TotCost.ToString();    //总金额

                fpBlance_Sheet1.Cells[i, 2].Text = bl.BalanceBase.FT.PubCost.ToString();    //统筹金额

                fpBlance_Sheet1.Cells[i, 3].Text = bl.BalanceBase.FT.OwnCost.ToString();    //自费金额

            }

            //总费用（总）
            fpBlance_Sheet1.Cells[17, 1].Text = patientInfo.FT.TotCost.ToString();
            //公费医疗（总）
            fpBlance_Sheet1.Cells[17, 2].Text = patientInfo.FT.PubCost.ToString();
            //自费费用（总）
            fpBlance_Sheet1.Cells[17, 3].Text = patientInfo.FT.OwnCost.ToString();

            #endregion

            #region 为fpOwnCost控件赋值

            string[,] itemDetail = GetSIItemDetail(patientInfo.ID);

            for (int i = 0; i < itemDetail.Length / itemDetail.Rank; i++)
            {

                //起付线 自付费用
                if (itemDetail[i, 0] == "0105")
                {
                    fpOwnCost_Sheet1.Cells[2, 2].Text = itemDetail[i, 1];
                }

                //超起付线 5000以下部分 记账费用
                if (itemDetail[i, 0] == "0212")
                {
                    fpOwnCost_Sheet1.Cells[3, 1].Text = itemDetail[i, 1];
                }

                //超起付线 5000以下部分 自付费用
                if (itemDetail[i, 0] == "0108")
                {
                    fpOwnCost_Sheet1.Cells[3, 2].Text = itemDetail[i, 1];
                }

                //5000元以上-10000部分（支付85%） 记账费用
                if (itemDetail[i, 0] == "0213")
                {
                    fpOwnCost_Sheet1.Cells[4, 1].Text = itemDetail[i, 1];
                }

                //5000元以上-10000部分（支付85%） 自付费用
                if (itemDetail[i, 0] == "0109")
                {
                    fpOwnCost_Sheet1.Cells[4, 2].Text = itemDetail[i, 1];
                }

                //10000元以上部分（支付90%） 记账费用
                if (itemDetail[i, 0] == "0214")
                {
                    fpOwnCost_Sheet1.Cells[5, 1].Text = itemDetail[i, 1];
                }

                //10000元以上部分（支付90%） 自付费用
                if (itemDetail[i, 0] == "0110")
                {
                    fpOwnCost_Sheet1.Cells[5, 2].Text = itemDetail[i, 1];
                }

                //超封顶线自付部分费用 记账费用
                fpOwnCost_Sheet1.Cells[6, 1].Text = "";

                //超封顶线自付部分费用 自付费用
                if (itemDetail[i, 0] == "0104")
                {
                    fpOwnCost_Sheet1.Cells[6, 2].Text = itemDetail[i, 1];
                }

                //合计 记账费用
                fpOwnCost_Sheet1.Cells[7, 1].Text = patientInfo.SIMainInfo.PubCost.ToString();

                //合计 自付费用
                fpOwnCost_Sheet1.Cells[7, 2].Text = patientInfo.SIMainInfo.PubOwnCost.ToString();
            }

            #endregion

            #region 为fpTotal赋值

            //住院总费用
            fpTotal_Sheet1.Cells[0, 1].Text = patientInfo.FT.TotCost.ToString();
            //个人现金支付
            fpTotal_Sheet1.Cells[1, 1].Text = Convert.ToString(patientInfo.FT.OwnCost + patientInfo.FT.PayCost);
            //医疗保险支付金额
            fpTotal_Sheet1.Cells[2, 1].Text = patientInfo.FT.PubCost.ToString();
            fpTotal_Sheet1.Cells[2, 2].Text = FS.FrameWork.Function.NConvert.ToCapital(patientInfo.SIMainInfo.PubCost);
            
            #endregion

            #region 为fpSign赋值

            //操作员
            fpSign_Sheet1.Cells[7, 0].Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            //操作时间
            fpSign_Sheet1.Cells[7,1].Text = System.DateTime.Now.ToLongDateString();

            #endregion

            return 1;
        }
        /// <summary>
        /// 取现金支付部分信息
        /// </summary>
        /// <returns></returns>
        private string[,] GetSIXJZFitem()
        {
            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
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
        /// 获取住院结算信息
        /// </summary>
        /// <param name="InpatientNO">住院流水号</param>
        /// <returns>住院详细信息</returns>
        private string[,] GetSIItemDetail(string InpatientNO)
        {
            string strSql = " select t.zfxm, t.je from si_yb_zyzf t where t.zylsh = '" + InpatientNO +"'";
            //" where t.zylsh = '' --住院流水号 ";

            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
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
        /// 获取住院医保结算项目信息
        /// </summary>
        /// <returns></returns>
        private string[,] GetSIJSXMitem()
        {

            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
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
            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
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
        /// 把金额转换成大写
        /// </summary>
        /// <param name="Cash">金额</param>
        /// <returns>转换后的大写金额</returns>
        private string GetUpperCashbyNumber(decimal Cash)
        {
            //大写对照
            string[] upperNumber = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            //小写对照
            string[] lowerNumber = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //位数对照
            string[,] unit = { { "11", "亿" }, { "10", "千万" }, { "9", "百万" }, { "8", "十万" }, { "7", "万" }, { "6", "千" }, { "5", "百" }, { "4", "拾" }, { "3", "元" }, { "2", "角" }, { "1", "分" } };

            string sReturn = string.Empty;

            string tmpReturn = Cash.ToString().Replace(".", "");

            int tmp = 0;

            string tmpNum = string.Empty;

            for (int i = 0; i < tmpReturn.Length; i++)
            {
                //构造大写数字
                for (int m = 0; m < lowerNumber.Length; m++)
                {
                    tmpNum = tmpReturn.Substring(i, 1);
                    if (lowerNumber[m] == tmpNum)
                    {
                        sReturn += upperNumber[m];
                    }
                }
                if (Cash > 0)
                {
                    //构造单位
                    for (int j = 0; j < unit.Length / unit.Rank; j++)
                    {
                        tmp = i + 1;
                        if (tmp.ToString() == unit[j, 0])
                        {
                            sReturn += unit[j, 1];
                        }
                    }
                }
            }
                
            return sReturn;
        }

        /// <summary>
        /// 自费患者Label赋值
        /// </summary>
        private void SetSILableVis()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Tag == "SI")
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
