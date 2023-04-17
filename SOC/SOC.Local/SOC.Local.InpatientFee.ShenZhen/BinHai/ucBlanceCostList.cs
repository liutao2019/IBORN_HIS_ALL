using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.SOC.HISFC.InpatientFee.Interface;

namespace Neusoft.SOC.Local.InpatientFee.ShenZhen
{
    public partial class ucBlanceCostList : UserControl, IBillPrint
    {
        public ucBlanceCostList()
        {
            InitializeComponent();
            this.lblTitle.Text = new Neusoft.HISFC.BizLogic.Manager.Constant().GetHospitalName() + lblTitle.Text;
        }
        #region 变量


        private System.Data.IDbTransaction trans;
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo;
        private string bloodMinFeeCode;//用血互助金最小费用编码


        #endregion

        #region IBalanceListPrint 成员

        public Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                patientInfo = value;
            }
            get
            {
                return this.patientInfo;
            }
        }
        public string InvoiceType
        {
            get
            {
                return "ZY01";
            }
        }

        public string BloodMinFeeCode
        {
            set
            {
                bloodMinFeeCode = value;
            }
        }

        public int Clear()
        {
            return 0;
        }

        public int Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print prn = new Neusoft.FrameWork.WinForms.Classes.Print();
            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 680, 800);
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 780, 800);//{203861DF-5000-4bf5-A645-52A45812F413}
            prn.SetPageSize(ps);
            prn.PrintPage(0, 0, this);
            return 0;
        }

        public int PrintPreview()
        {
            Neusoft.FrameWork.WinForms.Classes.Print prn = new Neusoft.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 680, 800);
            prn.SetPageSize(ps);
            prn.PrintPreview(0, 0, this);
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
        }

        public int SetValueForPreview(Neusoft.HISFC.Models.RADT.PatientInfo curPatientInfo, Neusoft.HISFC.Models.Fee.Inpatient.Balance balanceInfo,
            System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alFeeItemList, System.Collections.ArrayList alPayList)
        {
            SetPatientInfo(curPatientInfo);
            SetBalanceInfo(alBalanceList);
            return 1;
        }

        public int SetValueForPrint(Neusoft.HISFC.Models.RADT.PatientInfo curPatientInfo, Neusoft.HISFC.Models.Fee.Inpatient.Balance balanceInfo,
            System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alFeeItemList, System.Collections.ArrayList alPayList)
        {
            return SetValueForPreview(curPatientInfo, balanceInfo, alBalanceList, alFeeItemList, alPayList);
        }

        public int SetValueForPrint(Neusoft.HISFC.Models.RADT.PatientInfo curPatientInfo, Neusoft.HISFC.Models.Fee.Inpatient.Balance balanceInfo,
            System.Collections.ArrayList alBalanceList)
        {
            return 1;
        }

        public IDbTransaction Trans
        {
            set
            {
                this.trans = value;
            }
            get
            {
                return this.trans;
            }
        }

        #endregion

        /// <summary>
        /// 为患者信息的label控件赋值
        /// </summary>
        /// <returns>成功返回:1；失败返回:-1</returns>
        private int SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo curPatientInfo)
        {
            //常数管理类
            Neusoft.HISFC.BizLogic.Manager.Constant conMger = new Neusoft.HISFC.BizLogic.Manager.Constant();
            Neusoft.FrameWork.Models.NeuObject neuObj = new Neusoft.FrameWork.Models.NeuObject();
            Neusoft.FrameWork.Models.NeuObject empStatusObj = new Neusoft.FrameWork.Models.NeuObject();//人员状态

            //检查申请单业务层
          //  Neusoft.HISFC.BizLogic.Order.CheckSlip checkSlip = new Neusoft.HISFC.Management.Order.CheckSlip();

            try
            {
                if (patientInfo.Name != null)
                {
                    this.lblName.Visible = true;
                    this.lblName.Text = patientInfo.Name;
                }//患者姓名
                PatientInfo = curPatientInfo;
                #region 患者病种，取得是住院主诊断
                if (patientInfo.Pact.PayKind.ID.Equals("01"))//自费患者主诊断
                {
                    this.lblIllnessType.Visible = false; //sel 自费患者不打诊断 如果要打，再加回来
                    //string diagnose = string.Empty;
                    //diagnose = checkSlip.QueryOutpatinetDiagName(this.patientInfo.PID.CardNO)[0].ToString();
                    //if (diagnose != null && diagnose != string.Empty)
                    //{
                    //    this.lblIllnessType.Visible = true;
                    //    this.lblIllnessType.Text = diagnose;//patientInfo.SIMainInfo.OutDiagnose.Name
                    //}
                }
                else
                {
                    this.lblIllnessType.Visible = true;
                    this.lblIllnessType.Text = this.patientInfo.SIMainInfo.OutDiagnose.Name;//医保患者主诊断
                }
                #endregion

                if (patientInfo.PID.ID != null)
                {
                    this.lblIpbNo.Visible = true;
                    this.lblIpbNo.Text = patientInfo.PID.ID;
                }//住院号

                if (patientInfo.PVisit.InTime != null && patientInfo.PVisit.OutTime != null)
                {
                    this.lblDate.Visible = true;
                    this.lblDate.Text = patientInfo.PVisit.InTime.ToString() + "至";
                    //Edit By ZhangD 2010-9-26
                    //出院时间为空默认为当前时间或为空
                    //{D645E761-AAD7-4c24-B19D-348437D7C4A8}
                    if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                    {
                        this.lblDate.Text += DateTime.Now.ToString();
                    }
                    else
                    {
                        this.lblDate.Text += patientInfo.PVisit.OutTime.ToString();
                    }
                }//在院时间



                if (patientInfo.PVisit.OutTime != null || patientInfo.PVisit.InTime != null)
                {
                    this.lblIpbDays.Visible = true;
                    //DateTime dt = new DateTime();
                    TimeSpan ts = new TimeSpan();
                    //dt= (DateTime)(patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime);
                    //Edit By ZhangD 2010-9-26 
                    //{861F4976-187E-4cd4-94C3-356A63E13F33}
                    if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                    {
                        ts = DateTime.Now - patientInfo.PVisit.InTime;
                    }
                    else
                    {
                        ts = patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime;
                    }
                    //this.lblInpatientDay.Text = dt.Day.ToString();
                    this.lblIpbDays.Text = ts.Days.ToString();
                }//在院天数


                if (patientInfo.PVisit.PatientLocation.Dept.Name != null)
                {
                    this.lblDept.Visible = true;
                    this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                }//科室类别

                if (patientInfo.PVisit.PatientLocation.Bed != null)
                {
                    this.lblBedNo.Visible = true;
                    this.lblBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ToString();
                }//床号

                if (patientInfo.Sex != null)
                {
                    lblSex.Visible = true;
                    lblSex.Text = patientInfo.Sex.ToString();
                }
                //性别

                if (patientInfo.Age != null)
                {
                    lblAge.Visible = true;
                    lblAge.Text = patientInfo.Age;
                }//年龄

                #region 人员类别
                if (!patientInfo.Pact.PayKind.ID.Equals("01"))//非自费患者,
                {
                    this.lblPayKind.Visible = true;

                    if (patientInfo.SIMainInfo.MedicalType.ID == "11")
                    {
                        neuObj = conMger.GetConstant("PersonType", patientInfo.SIMainInfo.PersonType.ID);//人员类别
                        empStatusObj = conMger.GetConstant("EmplType", patientInfo.SIMainInfo.EmplType);//人员状态
                        this.lblPayKind.Text = neuObj.Name + "(" + empStatusObj.Name + ")";
                    }
                    else
                    {
                        neuObj = conMger.GetConstant("MedicalType", patientInfo.SIMainInfo.MedicalType.ID);
                        this.lblPayKind.Text = neuObj.Name;
                    }
                }
                else
                {
                    this.lblPayKind.Visible = true;
                    this.lblPayKind.Text = "自费患者";
                }

                #endregion

                this.lblPrintTime.Text = conMger.GetDateTimeFromSysDateTime().ToString();
                this.lblOprName.Text = conMger.Operator.Name;

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 显示费用汇总信息
        /// </summary>
        /// <returns>成功返回:1；失败返回:-1</returns>
        private int SetBalanceInfo(System.Collections.ArrayList alBalanceList1)
        {
            ArrayList alBalanceList = new ArrayList();
            alBalanceList.Clear();
            alBalanceList = alBalanceList1;
            if (alBalanceList.Count == 0)
                return -1;
            //清空所有行
            fpFee_Sheet1.RowCount = 0;
            int m = 0;//记录信息插入的位置
            string fee_Code;//费用编码

            decimal totCost = 0; //合计总费用
            decimal ownCost = 0; //合计自费用

            int balanceList = alBalanceList.Count;
            Hashtable ht = new Hashtable();
            ht.Clear();

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.BalanceList al in alBalanceList)
            {
                if (ht.Contains(al.FeeCodeStat.SortID))
                {
                   
                    continue;
                }
                else
                {

                    ht.Add(al.FeeCodeStat.SortID, al);
                }
            }
            ArrayList alhast = new ArrayList(ht.Keys);
            ArrayList balanceListsort = new ArrayList();  //排序费别
            alhast.Sort();
            balanceListsort.Clear();
            for (int i = 0; i < alhast.Count; i++)
            {
                foreach (Neusoft.HISFC.Models.Fee.Inpatient.BalanceList dl in alBalanceList)
                {
                    if ((int)alhast[i] == dl.FeeCodeStat.SortID)
                    {
                        balanceListsort.Add(dl);

                        break;
                    }
             
                }

            }


            for (int i = 0; i < balanceListsort.Count; i++)
            {
                fpFee_Sheet1.AddRows(i, 1);
                Neusoft.HISFC.Models.Fee.Inpatient.BalanceList bl = (Neusoft.HISFC.Models.Fee.Inpatient.BalanceList)balanceListsort[i];
        
                   decimal pubCost1 = 0m;
                   decimal ownCost1 = 0m;
                   decimal payCost1 = 0m;
                   decimal totCost1 = 0m;

                    int mm =1;
                    foreach (Neusoft.HISFC.Models.Fee.Inpatient.BalanceList c1 in alBalanceList)
                    {
                        if (bl.FeeCodeStat.SortID == c1.FeeCodeStat.SortID)
                        {
                            if (mm == 1)
                            {
                                pubCost1 = bl.BalanceBase.FT.PubCost;
                                ownCost1 = bl.BalanceBase.FT.OwnCost;
                                payCost1 = bl.BalanceBase.FT.PayCost;
                                totCost1 = bl.BalanceBase.FT.TotCost;
                                mm++;
                            }
                            else
                            {
                                pubCost1 = pubCost1 + c1.BalanceBase.FT.PubCost;
                                ownCost1 = ownCost1 + c1.BalanceBase.FT.OwnCost;
                                payCost1 = payCost1 + c1.BalanceBase.FT.PayCost;
                                totCost1 = totCost1 + c1.BalanceBase.FT.TotCost;
                            }
                          

                        }
                    }

                        //bl.BalanceBase.FT.PubCost = pubCost1;
                        //bl.BalanceBase.FT.OwnCost = ownCost1;
                        //bl.BalanceBase.FT.PayCost = payCost1;
                        //bl.BalanceBase.FT.TotCost = totCost1;
                 
                fpFee_Sheet1.Cells[m, 0].Text = bl.FeeCodeStat.StatCate.Name.ToString();//大类名称

                fpFee_Sheet1.Cells[m, 1].Text = totCost1.ToString();// bl.BalanceBase.FT.TotCost.ToString();//总金额
                totCost = totCost + totCost1;
                fpFee_Sheet1.Cells[m, 4].Text = ownCost1.ToString();// bl.BalanceBase.FT.OwnCost.ToString();//自费金额
                fpFee_Sheet1.Cells[m, 5].Text = ownCost1.ToString();// bl.BalanceBase.FT.OwnCost.ToString();//自费金额+自付金额
                ownCost = ownCost + bl.BalanceBase.FT.OwnCost;
                //记帐金额
                fpFee_Sheet1.Cells[m, 2].Text = ""; ((decimal)(bl.BalanceBase.FT.TotCost - bl.BalanceBase.FT.OwnCost)).ToString();
                //fpFee_Sheet1.Cells[m, 7].Text = "0";//大类的医保比例显示为0；{203861DF-5000-4bf5-A645-52A45812F413}

                fee_Code = bl.FeeCodeStat.MinFee.ID;
                m++;
            }
            fpFee_Sheet1.AddRows(m, 1);
            fpFee_Sheet1.Cells[m, 0].Text = "合计";
            fpFee_Sheet1.Cells[m, 1].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 4].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 5].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 2].Text = "";//((decimal)(totCost - ownCost)).ToString();
            //fpFee_Sheet1.AddRows(m + 1, 1);
            //fpFee_Sheet1.Cells[m + 1, 0].Text = "预交金合计";
            //fpFee_Sheet1.Models.Span.Add(m + 1, 1, 1, 5);
            //fpFee_Sheet1.Cells[m + 1, 1].Text = this.PatientInfo.FT.PrepayCost.ToString("F2");
            return 1;
        }

        #region IBillPrint 成员

        void IBillPrint.Print()
        {
            throw new NotImplementedException();
        }

        public int SetData(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, EnumPrintType printType, object t, ref string errInfo, params object[] appendParams)
        {
            throw new NotImplementedException();
        }

        public int SetData(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, object t, ref string errInfo, params object[] appendParams)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
