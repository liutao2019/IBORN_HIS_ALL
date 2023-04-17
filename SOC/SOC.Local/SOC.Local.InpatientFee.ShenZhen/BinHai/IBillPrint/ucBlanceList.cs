using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.InpatientFee.Interface;

namespace FS.SOC.Local.InpatientFee.ShenZhen
{

    /// <summary>
    /// ucSZBlanceList<br></br>
    /// [功能描述: 出院费用结算单打印]<br></br>
    /// [创 建 者: 聂爱军]<br></br>
    /// [创建时间: 2010-4-20]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucBlanceList : UserControl//FS.HISFC.Integrate.FeeInterface.IBalanceListPrint
    {
        public ucBlanceList()
        {
            InitializeComponent();
           this.lblTitle.Text =new FS.HISFC.BizLogic.Manager.Constant().GetHospitalName() + lblTitle.Text;
        }

        #region 变量


        private System.Data.IDbTransaction trans;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        private string bloodMinFeeCode;//用血互助金最小费用编码

        private FS.HISFC.BizLogic.Manager.Department deptMgr = new  FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Manager  deptManager = new  FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new  FS.HISFC.BizLogic.Fee.FeeCodeStat();
        private FS.HISFC.BizLogic.Fee.Interface SiInterface = new FS.HISFC.BizLogic.Fee.Interface();


        #endregion
    
        #region IBalanceListPrint 成员

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set { patientInfo = value; }
        }
        public string InvoiceType
        {
            get { return "ZY01"; }
        }

        public string BloodMinFeeCode
        {
            set { bloodMinFeeCode = value; }
        }

        public int Clear()
        {
            return 0;
        }

        public int Print()
        {
            
            FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 680, 800);
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJS", 790, 1098);
            prn.SetPageSize(ps);
            prn.PageLabel = label1;
            prn.PrintPage(0, 0, this);
            return 0;
        }

        public int PrintPreview()
        {
            FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("Letter", 800, 1098);
            
            prn.PageLabel = label1;
            prn.SetPageSize(ps);
            prn.PrintPreview(20, 0, this);
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans; 
        }

        public int SetValueForPreview(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, 
            System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alFeeItemList, System.Collections.ArrayList alPayList)
        {
            SetPatientInfo(patientInfo);
            SetBalanceInfoNew(patientInfo, alBalanceList, alFeeItemList);
            return 1;
        }

        public int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, 
            System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alFeeItemList, System.Collections.ArrayList alPayList)
        {
            return SetValueForPreview(patientInfo, balanceInfo, alBalanceList, alFeeItemList, alPayList);
        }

        private int SetValueForPrint(FS.HISFC.Models.RADT.PatientInfo curPatientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo,
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
        /// <returns>成功返回:1；失败返回:-1</returns>
        private int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            
            //常数管理类
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject empStatusObj = new FS.FrameWork.Models.NeuObject();//人员状态

            //检查申请单业务层
         //   FS.HISFC.BizLogic.Order.CheckSlip checkSlip = new FS.HISFC.BizLogic.Order.CheckSlip();
            
            try
            {
                if (patientInfo.Name != null)
                {
                    this.lblName.Visible = true;
                    this.lblName.Text = patientInfo.Name;
                }//患者姓名

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
                    
                     this.lblIllnessType.Visible = false;
                    
                    //if (!string.IsNullOrEmpty(patientInfo.SIMainInfo.OutDiagnose.Name.Trim()))
                    //{//出院诊断
                    //    try
                    //    {
                    //        this.lblIllnessType.Visible = true;
                    //        this.lblIllnessType.Text = patientInfo.SIMainInfo.OutDiagnose.Name;//医保患者主诊断
                    //    }
                    //    catch
                    //    {
                    //        this.lblIllnessType.Visible = false; 
                    //    }
                   

                    if (patientInfo.ExtendFlag2 == "3" && patientInfo.Pact.ID == "2")
                    {

                       // this.lblIllnessType.Text = SiInterface.GetBalanceOutDiagnose(patientInfo.ID);
                     
                    }
                   
                }
                #endregion

                if (!string.IsNullOrEmpty(patientInfo.PID.ID))
                {
                    this.lblIpbNo.Visible = true;
                    this.lblIpbNo.Text = patientInfo.PID.ID;
                }//住院号

                if (patientInfo.PVisit.InTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime)
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
                else
                {
                    this.lblDate.Visible = true;
                    this.lblDate.Text = patientInfo.PVisit.InTime.ToString();
                }
                if (patientInfo.User01 == "T") //取一时间费用日期
                {
                    label2.Text = patientInfo.User02;
                    label2.Visible = true;
                }
                else
                {
                    label2.Visible = false;
                    neuLabel2.Visible = false;
                }


                if (patientInfo.PVisit.OutTime != null || patientInfo.PVisit.InTime != null)
                {
                    this.lblIpbDays.Visible = true;
                    TimeSpan ts = new TimeSpan();
                 
                    if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                    {
                        ts = DateTime.Now - FS.FrameWork.Function.NConvert.ToDateTime(patientInfo.PVisit.InTime.ToString("D") + " 00:00:00");
                    }
                    else
                    {
                        ts = patientInfo.PVisit.OutTime -FS.FrameWork.Function.NConvert.ToDateTime(patientInfo.PVisit.InTime.ToString("D") +" 00:00:00");
                    }
                    //this.lblInpatientDay.Text = dt.Day.ToString();
                    this.lblIpbDays.Text = ts.Days.ToString();
                }//在院天数


                if (!string.IsNullOrEmpty(patientInfo.PVisit.PatientLocation.Dept.Name))
                {
                    this.lblDept.Visible = true;
                    this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                }//科室类别

                if (patientInfo.PVisit.PatientLocation.Bed !=null)
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

                if (!string.IsNullOrEmpty(patientInfo.Age))
                {
                  //  lblAge.Visible = true;
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
                        neuObj = conMger.GetConstant("SZPACTUNIT", patientInfo.PVisit.MedicalType.ID);
                        this.lblPayKind.Text = neuObj.Name;
                    }
                }
                else
                {
                    this.lblPayKind.Visible = true;
                    this.lblPayKind.Text = "自费";
                }

                #endregion

                this.lblPrintTime.Text = conMger.GetDateTimeFromSysDateTime().ToString();
                this.lblOprName.Text = conMger.Operator.Name;
                //this.lblfee.Text = patientInfo.FT.TotCost.ToString();
             
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        /// <summary>
        /// 显示费用明细信息
        /// </summary>
        /// <returns>成功返回:1；失败返回:-1</returns>
        private int SetBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo,System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alFeeItemList)
        {
            
            FS.HISFC.BizLogic.Fee.Interface feeInterface = new FS.HISFC.BizLogic.Fee.Interface();
            ArrayList alFeeState = new ArrayList();
            FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
            ArrayList alDept = deptManager.QueryDeptmentsInHos(true);
            alDept.AddRange(this.deptMgr.GetDeptmentAll());
            FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
            deptHelper.ArrayObject = alDept;


            alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode("ZY01");//

            int balanceList = alBalanceList.Count;
            Hashtable ht = new Hashtable();
            ht.Clear();
            
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList al in alBalanceList)
            {
                if (ht.Contains(al.FeeCodeStat.SortID))
                {
                    continue;
                }
                else

                ht.Add(al.FeeCodeStat.SortID, al);
            }
             ArrayList alhast = new ArrayList(ht.Keys);
             ArrayList balanceListsort = new ArrayList();  //排序费别
             alhast.Sort();
             balanceListsort.Clear();
             for (int i = 0; i < alhast.Count; i++)
             {
                 foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList dl in alBalanceList)
                 {
                     if ((int)alhast[i] == dl.FeeCodeStat.SortID)
                     {
                         balanceListsort.Add(dl);
                         break;
                     }
                 }
             
             }
       
            int m = 0;//记录信息插入的位置
            int n = 1;
            string fee_Code;//费用编码

            decimal totCost = 0; //合计总费用
            decimal ownCost = 0; //合计自费用

            
            //获取汇总后的明细列表
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemListGroup = GetFeeItemGroup(alFeeItemList);

            for (int i = 0; i < balanceListsort.Count; i++)
            {

                FS.HISFC.Models.Fee.Inpatient.BalanceList bl = (FS.HISFC.Models.Fee.Inpatient.BalanceList)balanceListsort[i];
                decimal sumcost = 0m;
                int count = 0;
                
                fpBlance_Sheet1.Cells[m, 0].Text = "";//取消费别
                fpBlance_Sheet1.Cells[m, 1].Text=bl.FeeCodeStat.StatCate.Name.ToString();//大类名称

                
                //记帐金额
                fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(bl.BalanceBase.FT.TotCost-bl.BalanceBase.FT.OwnCost)).ToString();
                fpBlance_Sheet1.Columns[7].Visible = true;
                fpBlance_Sheet1.Cells[m, 8].Text = " ";
                fpBlance_Sheet1.Cells[m, 7].Text = "";
                fpBlance_Sheet1.Cells[m, 9].Text = "";

                fee_Code = bl.FeeCodeStat.StatCate.ID;
                count = m;
                m++;

                //在明细中查询对应大类的收费明细
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemListGroup)
                {

                    objFeeStat = this.GetFeeStatByFeeCode(feeItemList.Item.MinFee.ID, alFeeState);
                    if (objFeeStat.ID== fee_Code && feeItemList.Item.Qty.ToString()!="0")
                    {
                        FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                        FS.HISFC.Models.SIInterface.Compare compareObj = new  FS.HISFC.Models.SIInterface.Compare();
                        int SIRate = 0;
                     
                        SIRate = myInterface.GetCompareSingleItem("2", feeItemList.Item.ID, ref compareObj);

                        fpBlance_Sheet1.Cells[m, 0].Text = n.ToString();
                        fpBlance_Sheet1.Cells[m, 1].Text = compareObj.CenterItem.ID; //feeItemList.Compare.CenterItem.ID.ToString();
                        if (feeItemList.UndrugComb.ID != "")
                            fpBlance_Sheet1.Cells[m, 2].Text = "［" + feeItemList.UndrugComb.Name + "］" + feeItemList.Item.Name ;// + "/" + feeItemList.Item.Specs;
                        else
                            fpBlance_Sheet1.Cells[m, 2].Text = feeItemList.Item.Name + "/" + feeItemList.Item.Specs;
                        fpBlance_Sheet1.Cells[m, 3].Text = feeItemList.Item.PriceUnit;
                        fpBlance_Sheet1.Cells[m, 4].Text = feeItemList.Item.Qty.ToString("F2");
                        fpBlance_Sheet1.Cells[m, 5].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                        fpBlance_Sheet1.Cells[m, 6].Text = feeItemList.FT.TotCost.ToString();
                        fpBlance_Sheet1.Cells[m, 7].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                        //if (patientInfo.Pact.ID == "2" && (compareObj.CenterItem.ItemGrade != "0" || feeItemList.Compare.CenterItem.ID.Contains("z") ))
                        //    fpBlance_Sheet1.Cells[m, 8].Text = "自费";
                        //else if ((patientInfo.Pact.ID == "2" &&  feeInterface.GetcompareItemSIgrade(patientInfo.ID, feeItemList.Item.ID, feeItemList.Order.ID) != "") || feeItemList.Item.SpecialFlag3 == "1")
                        //    fpBlance_Sheet1.Cells[m, 8].Text = "自费限制";
                        //else
                        //    fpBlance_Sheet1.Cells[m, 8].Text = " ";
                      
                        fpBlance_Sheet1.Cells[m, 9].Text = "";
                      //  fpBlance_Sheet1.Cells[m,6].Text = feeItemList.RecipeOp
                       // fpBlance_Sheet1.Cells[m, 6].Text = feeItemList.FT.OwnCost.ToString();
                       // fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(feeItemList.FT.TotCost - feeItemList.FT.OwnCost)).ToString();

                        #region 

                   

                        sumcost += feeItemList.FT.TotCost;
                        totCost += feeItemList.FT.TotCost;
                        ownCost += feeItemList.FT.TotCost;
                        #endregion
                        n++;
                        m++;
                    }

                  //  fpBlance_Sheet1.Cells[count, 5].Text = sumcost.ToString();//总金额
                    fpBlance_Sheet1.Cells[count, 6].Text = sumcost.ToString();
                   
                   
                }

            }
            fpBlance_Sheet1.Cells[m, 0].Text = "合计" ;
            //fpBlance_Sheet1.Cells[m, 5].Text = totCost.ToString();
            fpBlance_Sheet1.Cells[m, 6].Text = ownCost.ToString();
        //  fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(totCost - ownCost)).ToString();
            fpBlance_Sheet1.Cells[m, 8].Text = "";//合计处的医保比例显示为零
            fpBlance_Sheet1.Cells[m, 9].Text = "";
            this.lblfee.Text = totCost.ToString();
           // this.pl2.Top = pl1.Height+(m+n+2)*20;
            return 1;
        }
        /// <summary>
        /// 显示费用明细信息
        /// </summary>
        /// <returns>成功返回:1；失败返回:-1</returns>
        private int SetBalanceInfoNew(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alBalanceList,System.Collections.ArrayList alFeeItemList)
        {

            FS.HISFC.BizLogic.Fee.Interface feeInterface = new FS.HISFC.BizLogic.Fee.Interface();
            ArrayList alFeeState = new ArrayList();
            FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
            ArrayList alDept = deptManager.QueryDeptmentsInHos(true);
            alDept.AddRange(this.deptMgr.GetDeptmentAll());
            FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
            deptHelper.ArrayObject = alDept;

            int m = 0;//记录信息插入的位置
            int n = 1;
            string fee_Code;//费用编码

            decimal totCost = 0; //合计总费用
            decimal ownCost = 0; //合计自费用

            decimal sumcost = 0m;

            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemListGroup = GetFeeItemGroup(alFeeItemList);

            for (int i = 0; i < alBalanceList.Count; i++)
            {

                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alBalanceList[i];

                int count = 0;
                fpBlance_Sheet1.Cells[m, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                fpBlance_Sheet1.Cells[m, 0].Text = "";//取消费别
                fpBlance_Sheet1.Cells[m, 1].Text = feeinfo.Invoice.Type.Name;//大类名称
                fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(feeinfo.FT.TotCost)).ToString();
                fpBlance_Sheet1.Columns[7].Visible = true;
                fpBlance_Sheet1.Cells[m, 8].Text = " ";
                fpBlance_Sheet1.Cells[m, 7].Text = "";
                //fpBlance_Sheet1.Cells[m, 9].Text = "";

                fee_Code = feeinfo.Invoice.Type.Name;
                sumcost += feeinfo.FT.TotCost; //费用总合计
                count = m;
                fpBlance_Sheet1.Cells[count, 6].Text = feeinfo.FT.TotCost.ToString(); //大类金额
                m++;
                //在明细中查询对应大类的收费明细
               

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemListGroup)
                {
                    if (feeItemList.Invoice.Type.Memo == "组套" && feeItemList.Invoice.Type.Name == fee_Code && feeItemList.Item.Qty.ToString() != "0")
                    {

                        continue;
                    //    FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                    //    FS.HISFC.Models.SIInterface.Compare compareObj = new FS.HISFC.Models.SIInterface.Compare();
                    //    int SIRate = 0;

                    //    SIRate = myInterface.GetCompareSingleItem("2", feeItemList.Item.ID, ref compareObj);

                    //    fpBlance_Sheet1.Cells[m, 0].Text = n.ToString();
                    //   // fpBlance_Sheet1.Cells[m, 1].Text = compareObj.CenterItem.ID; //feeItemList.Compare.CenterItem.ID.ToString();
                    //    if (feeItemList.UndrugComb.ID != "")
                    //        fpBlance_Sheet1.Cells[m, 1].Text = "［" + feeItemList.UndrugComb.Name + "］" + feeItemList.Item.Name;// + "/" + feeItemList.Item.Specs;
                    //    else
                    //        if (string.IsNullOrEmpty(feeItemList.Item.Specs))
                    //        {
                    //            fpBlance_Sheet1.Cells[m, 1].Text = feeItemList.Item.Name;
                              
                    //        }
                    //        else
                    //        {
                    //            fpBlance_Sheet1.Cells[m, 1].Text = feeItemList.Item.Name + "/" + feeItemList.Item.Specs;
                             
                    //        }
                    //    fpBlance_Sheet1.Cells[m, 3].Text = feeItemList.Item.PriceUnit;
                    //    fpBlance_Sheet1.Cells[m, 4].Text = feeItemList.Item.Qty.ToString("F3");
                    //    if (feeItemList.Item.PackQty > 0)
                    //    {
                    //        fpBlance_Sheet1.Cells[m, 6].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F3");
                    //    }
                    //    else
                    //    {
                    //        fpBlance_Sheet1.Cells[m, 6].Text = feeItemList.Item.Price.ToString("F3");
                    //    }
                    //    fpBlance_Sheet1.Cells[m, 7].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                    //    fpBlance_Sheet1.Cells[m, 8].Text = "";
                    //    n++;
                    //    m++;
                    }
                    else 
                    if (feeItemList.Invoice.Type.Name == fee_Code && feeItemList.Item.Qty != 0)
                    {
                        FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                        FS.HISFC.Models.SIInterface.Compare compareObj = new FS.HISFC.Models.SIInterface.Compare();
                        int SIRate = 0;
                        fpBlance_Sheet1.Cells[m, 0].Text = n.ToString();
                        if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            SIRate = myInterface.GetCompareSingleItem("2", feeItemList.Item.ID, ref compareObj);
                            fpBlance_Sheet1.Cells[m, 1].Text = compareObj.CenterItem.ID; //feeItemList.Compare.CenterItem.ID.ToString();
                        }
                        else
                        {
                            fpBlance_Sheet1.Cells[m, 1].Text = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).GBCode;      
                        }
                        if (feeItemList.UndrugComb.ID != "")
                            fpBlance_Sheet1.Cells[m, 2].Text = "【" + feeItemList.UndrugComb.Name + "】" + feeItemList.Item.Name;// + "/" + feeItemList.Item.Specs;
                        else
                           // fpBlance_Sheet1.Cells[m, 2].Text = feeItemList.Item.Name +"/" + feeItemList.Item.Specs;
                        if (string.IsNullOrEmpty(feeItemList.Item.Specs))
                        {
                            fpBlance_Sheet1.Cells[m, 2].Text = feeItemList.Item.Name;
                        }
                        else
                        {
                            fpBlance_Sheet1.Cells[m, 2].Text = feeItemList.Item.Name + "/" + feeItemList.Item.Specs;
                       
                        }
                        fpBlance_Sheet1.Cells[m, 3].Text = feeItemList.Item.PriceUnit;
                        fpBlance_Sheet1.Cells[m, 4].Text = feeItemList.Item.Qty.ToString("F2");
                        if (feeItemList.Item.PackQty > 0)
                        {
                            fpBlance_Sheet1.Cells[m, 5].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                        }
                        else
                        {
                            fpBlance_Sheet1.Cells[m, 5].Text = feeItemList.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                        }
                            fpBlance_Sheet1.Cells[m, 6].Text = feeItemList.FT.TotCost.ToString();
                        fpBlance_Sheet1.Cells[m, 7].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                        fpBlance_Sheet1.Cells[m, 8].Text = "";
                        n++;
                        m++;
                    }

                    //  fpBlance_Sheet1.Cells[count, 5].Text = sumcost.ToString();//总金额
      


                }
             
                //totCost += feeItemList.FT.TotCost;
                //ownCost += feeItemList.FT.TotCost;

            }
            fpBlance_Sheet1.Cells[m, 0].Text = "合计";
            //fpBlance_Sheet1.Cells[m, 5].Text = totCost.ToString();
            fpBlance_Sheet1.Cells[m, 6].Text = sumcost.ToString();
            //  fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(totCost - ownCost)).ToString();
            fpBlance_Sheet1.Cells[m, 8].Text = "";//合计处的医保比例显示为零
            //fpBlance_Sheet1.Cells[m, 9].Text = "";
            this.lblfee.Text = sumcost.ToString();
            // this.pl2.Top = pl1.Height+(m+n+2)*20;
            return 1;
        }


        /// <summary>
        /// 汇总费用明细
        /// </summary>
        /// <param name="alFeeItemList">患者费用明细列表</param>
        /// <returns>返回汇总后的费用明细泛型</returns>
        private List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> GetFeeItemGroup(System.Collections.ArrayList alFeeItemList)
        {
            //记录一条记录是否存在于原列表中
            int j = 0;
            
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemListGroup=new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
            feeItemListGroup.Clear();
            foreach (Object obj in alFeeItemList)
            {
                j = 0;
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = obj as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (feeItemList != null)
                {
                    for (int i=0;i<feeItemListGroup.Count;i++)
                    {
                        if (feeItemList.Item.Name == feeItemListGroup[i].Item.Name && feeItemList.Item.ID == feeItemListGroup[i].Item.ID&& feeItemList.Item.PriceUnit == feeItemListGroup[i].Item.PriceUnit)
                        {

                            feeItemListGroup[i].Item.Qty = feeItemListGroup[i].Item.Qty+feeItemList.Item.Qty;
                            feeItemListGroup[i].FT.TotCost = feeItemListGroup[i].FT.TotCost + feeItemList.FT.TotCost;
                            feeItemListGroup[i].FT.OwnCost = feeItemListGroup[i].FT.OwnCost + feeItemList.FT.OwnCost;
                            j++;                 
                            break;
                        }
                    }
                    if (j==0)//容器中不存在该项收费明细
                    {
                        feeItemListGroup.Add(feeItemList);
                    }
                }
            }
            return feeItemListGroup;
        }


        /// <summary>
        /// 通过最小费用获取统计大类memo存打印顺序
        /// </summary>
        /// <param name="feeCode"></param>
        /// <param name="alInvoice"></param>
        /// <returns></returns>
        protected FS.FrameWork.Models.NeuObject GetFeeStatByFeeCode(string feeCode, ArrayList al)
        {
            
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Fee.FeeCodeStat feeStat;

            for (int i = 0; i < al.Count; i++)
            {
                feeStat = (FS.HISFC.Models.Fee.FeeCodeStat)al[i];
                if (feeStat.MinFee.ID == feeCode)
                {
                    obj.ID = feeStat.StatCate.ID;

                    obj.Name = feeStat.StatCate.Name;
                    obj.Memo = feeStat.SortID.ToString();
                    return obj;
                }
            }
            return null;
        }
    }
}
