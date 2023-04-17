using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.InpatientFee.Interface;

namespace FS.SOC.Local.InpatientFee.ShenZhen.IInpatientChargePrint
{

    /// <summary>
    /// ucSZBlanceList<br></br>
    /// [功能描述: 住院划价记账单打印]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2012-11-23]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucInpatientChargePrint : UserControl,FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint
    {
        public ucInpatientChargePrint()
        {
            InitializeComponent();
        }

        #region 变量


         
        private FS.HISFC.Models.RADT.PatientInfo patientInfo; 

        private FS.HISFC.BizLogic.Manager.Department deptMgr = new  FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Manager  deptManager = new  FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new  FS.HISFC.BizLogic.Fee.FeeCodeStat();
        private FS.HISFC.BizLogic.Fee.Interface SiInterface = new FS.HISFC.BizLogic.Fee.Interface();

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
                     
                   

                    if (patientInfo.ExtendFlag2 == "3" && patientInfo.Pact.ID == "2")
                    { 
                     
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
                        this.lblDate.Text += DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        this.lblDate.Text += patientInfo.PVisit.OutTime.ToShortDateString();
                    }
                }//在院时间
                else
                {
                    this.lblDate.Visible = true;
                    this.lblDate.Text = patientInfo.PVisit.InTime.ToShortDateString();
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
             
                return 1;
            }
            catch
            {
                return -1;
            }
        }


        /// <summary>
        /// 汇总费用明细
        /// </summary>
        /// <param name="alFeeItemList">患者费用明细列表</param>
        /// <returns>返回汇总后的费用明细泛型</returns>
        private List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> GetFeeItemGroup(ref List<FS.HISFC .Models .Fee .Inpatient .FeeItemList > allFeeItemList)
        {

                List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemGroup = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
                feeItemGroup.Clear();
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in allFeeItemList)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList temp = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    temp = feeItem.Clone();
                    int j = 0;
                    if (feeItem != null)
                    {
                        for (int i = 0; i < feeItemGroup.Count; i++)
                        {
                            if (temp.Item.Name == feeItemGroup[i].Item.Name && temp.Item.ID == feeItemGroup[i].Item.ID && temp.Item.PriceUnit == feeItemGroup[i].Item.PriceUnit)
                            {
                                decimal a;
                                decimal b;
                                a = feeItemGroup[i].Item.Qty;
                                b = temp.Item.Qty;
                                feeItemGroup[i].Item.Qty = a + b;
                                a = feeItemGroup[i].FT.TotCost;
                                b = temp.FT.TotCost;
                                feeItemGroup[i].FT.TotCost = a + b;
                                a = feeItemGroup[i].FT.OwnCost;
                                b = temp.FT.OwnCost;
                                feeItemGroup[i].FT.OwnCost = a + b;
                                j++;
                            }
                        }
                        if (j == 0)//容器中不存在该项收费明细
                        {
                            feeItemGroup.Add(temp);
                        }
                    }
                }
                return feeItemGroup;
        }

        private List<FS.HISFC .Models .Fee .Inpatient .FeeItemList > minfeeGroutp(ref List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> alfeeCollections)
        {
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeMinFeeGroup = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
            feeMinFeeGroup.Clear();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemMinfee in alfeeCollections)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList tempb = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                tempb = feeItemMinfee.Clone();
                int j = 0;
                if (feeItemMinfee != null)
                    {
                        for (int i = 0; i < feeMinFeeGroup.Count; i++)
                        {
                            if (tempb.Item.MinFee.ID == feeMinFeeGroup[i].Item.MinFee.ID)
                            {
                                decimal a;
                                decimal b;
                                a = tempb.FT.TotCost;
                                b = feeMinFeeGroup[i].FT.TotCost;
                                feeMinFeeGroup[i].FT.TotCost = b + a;
                                a = tempb.FT.OwnCost;
                                b = feeMinFeeGroup[i].FT.OwnCost;
                                feeMinFeeGroup[i].FT.OwnCost = b + a;
                                j++;
                            }
                        }
                        if (j == 0)//容器中不存在该项收费明细
                        {
                            feeMinFeeGroup.Add(tempb);
                        }
                    }
            }
            return feeMinFeeGroup;
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

        #region IInpatientChargePrint 成员

        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {

                return null ;
            }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo(this.patientInfo);
            }
        }

        public int SetData(List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemColl)
        {

            FS.HISFC.BizLogic.Fee.Interface feeInterface = new FS.HISFC.BizLogic.Fee.Interface();
            ArrayList alFeeState = new ArrayList();
            FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
            ArrayList alDept = deptManager.QueryDeptmentsInHos(true);
            alDept.AddRange(this.deptMgr.GetDeptmentAll());
            FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
            deptHelper.ArrayObject = alDept;

            //MinFeeSort minsort = new MinFeeSort();
            //feeItemColl.Sort(minsort);

            alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode("ZY01");
            FS.HISFC.Models.Base.Employee operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            int m = 0;//记录信息插入的位置
            int n = 1;
            string fee_Code;//费用编码
            int i = 0;
            decimal sumcost = 0m;

            ArrayList almin = new ArrayList();

            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemListGroup = this.GetFeeItemGroup(ref feeItemColl);//汇总明细
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> minFeeList = this.minfeeGroutp(ref feeItemColl);
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList subFeeItem in minFeeList)//汇总大类
            {

                objFeeStat = this.GetFeeStatByFeeCode(subFeeItem.Item.MinFee.ID, alFeeState);
                int count = 0;
                fpBlance_Sheet1.Cells[m, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                fpBlance_Sheet1.Cells[m, 0].Text = "";//取消费别
                fpBlance_Sheet1.Cells[m, 1].Text = objFeeStat.Name;//feeItem.Invoice.Type.Name;//大类名称
                fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(subFeeItem.FT.TotCost)).ToString();
                fpBlance_Sheet1.Columns[7].Visible = true;
                fpBlance_Sheet1.Cells[m, 8].Text = " ";
                fpBlance_Sheet1.Cells[m, 7].Text = "";

                fee_Code = subFeeItem.Item.MinFee.ID;
                sumcost += subFeeItem.FT.TotCost; //费用总合计
                count = m;
                fpBlance_Sheet1.Cells[count, 6].Text = subFeeItem.FT.TotCost.ToString(); //大类金额
                m++;

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in feeItemListGroup)
                {
                    if (i == 0)
                    {
                        this.lblTitle.Text = new FS.HISFC.BizLogic.Manager.Constant().GetHospitalName() + operObj.Dept.Name + this.lblTitle.Text;
                        this.lblChargeDateInfo.Text = this.lblChargeDateInfo.Text + feeItem.FeeOper.OperTime.ToShortDateString();
                        i = 1;
                    }
                    if (feeItem.Item.MinFee.ID != fee_Code)//不属于该大类的跳过
                    {
                        continue;
                    }
                    FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                    FS.HISFC.Models.SIInterface.Compare compareObj = new FS.HISFC.Models.SIInterface.Compare();
                    int SIRate = 0;
                    fpBlance_Sheet1.Cells[m, 0].Text = n.ToString();//序号
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        SIRate = myInterface.GetCompareSingleItem("2", feeItem.Item.ID, ref compareObj);
                        fpBlance_Sheet1.Cells[m, 1].Text = compareObj.CenterItem.ID;//统一编码
                    }
                    else
                    {
                        fpBlance_Sheet1.Cells[m, 1].Text = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItem.Item.ID).GBCode;//统一编码
                    }
                    //收费项目规格
                    if (feeItem.UndrugComb.ID != "")
                        fpBlance_Sheet1.Cells[m, 2].Text = "【" + feeItem.UndrugComb.Name + "】" + feeItem.Item.Name;// + "/" + feeItemList.Item.Specs;
                    else
                        // fpBlance_Sheet1.Cells[m, 2].Text = feeItemList.Item.Name +"/" + feeItemList.Item.Specs;
                        if (string.IsNullOrEmpty(feeItem.Item.Specs))
                        {
                            fpBlance_Sheet1.Cells[m, 2].Text = feeItem.Item.Name;
                        }
                        else
                        {
                            fpBlance_Sheet1.Cells[m, 2].Text = feeItem.Item.Name + "/" + feeItem.Item.Specs;

                        }

                    fpBlance_Sheet1.Cells[m, 3].Text = feeItem.Item.PriceUnit;//单位
                    fpBlance_Sheet1.Cells[m, 4].Text = feeItem.Item.Qty.ToString("F2");//数量

                    if (feeItem.Item.PackQty > 0)
                    {
                        fpBlance_Sheet1.Cells[m, 5].Text = (feeItem.Item.Price / feeItem.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');//单价
                    }
                    else
                    {
                        fpBlance_Sheet1.Cells[m, 5].Text = feeItem.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');//单价
                    }
                    fpBlance_Sheet1.Cells[m, 6].Text = feeItem.FT.TotCost.ToString();//金额
                    fpBlance_Sheet1.Cells[m, 7].Text = deptHelper.GetName(feeItem.ExecOper.Dept.ID);//费用科室
                    fpBlance_Sheet1.Cells[m, 8].Text = "";
                    n++;
                    m++;
                }
            }
            fpBlance_Sheet1.Cells[m, 0].Text = "合计";
            fpBlance_Sheet1.Cells[m, 6].Text = sumcost.ToString();
            fpBlance_Sheet1.Cells[m, 8].Text = "";//合计处的医保比例显示为零
            this.lblfee.Text = sumcost.ToString();
           
            return 1;
        }
         
        public int Preview()
        {            
            FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("Letter", 800, 1098);

            prn.PageLabel = label1;
            prn.SetPageSize(ps);
            prn.PrintPreview(20, 0, this);
            return 0;
        }
         

        public int Print()
        {
            //FS.FrameWork.WinForms.Classes.Print prn = new FS.FrameWork.WinForms.Classes.Print();
            //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize("ZYJZD", 700, 1000);
            //prn.SetPageSize(ps);
            //prn.PageLabel = label1;
            ////prn.PrintPage(0, 0, this);
            //prn.PrintPreview(20, 0, this);
            //return 0;

            //this.pl2.Location = new Point(this.pl3.Location.X, this.pl3.Location.Y + this.pl3.Height + 30);
            FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pgSize = pageSizeManager.GetPageSize("ZYJZD");
            if (pgSize == null)
            {
                pgSize = new FS.HISFC.Models.Base.PageSize("ZYJZD", 690, 980);//700,1000
            }
            FS.FrameWork.WinForms.Classes.Print printC = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            printC.SetPageSize(pgSize);
            //print.PageLabel = label1;
            //print.PrintPage(pgSize.Left, pgSize.Top, ucPricedList);
            printC.PrintPreview(20, 0, this);
            return 0;
        }
        public class MinFeeSort : System.Collections.Generic.IComparer<FS.HISFC .Models .Fee .Inpatient .FeeItemList >
        {
            public MinFeeSort() { } 

            #region IComparer<FeeItemList> 成员

            public int Compare(FS.HISFC.Models.Fee.Inpatient.FeeItemList x, FS.HISFC.Models.Fee.Inpatient.FeeItemList y)
            {
                string oX = x.Item.MinFee.ID;
                string oY = y.Item.MinFee.ID;
                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }
        #endregion
    }
}
