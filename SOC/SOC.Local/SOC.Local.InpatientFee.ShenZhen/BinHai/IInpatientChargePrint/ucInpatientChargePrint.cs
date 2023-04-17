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
    /// [��������: סԺ���ۼ��˵���ӡ]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2012-11-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucInpatientChargePrint : UserControl,FS.HISFC.BizProcess.Interface.FeeInterface.IInpatientChargePrint
    {
        public ucInpatientChargePrint()
        {
            InitializeComponent();
        }

        #region ����


         
        private FS.HISFC.Models.RADT.PatientInfo patientInfo; 

        private FS.HISFC.BizLogic.Manager.Department deptMgr = new  FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Manager  deptManager = new  FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new  FS.HISFC.BizLogic.Fee.FeeCodeStat();
        private FS.HISFC.BizLogic.Fee.Interface SiInterface = new FS.HISFC.BizLogic.Fee.Interface();

        #endregion
     
        /// <summary>
        /// Ϊ������Ϣ��label�ؼ���ֵ
        /// </summary>
        /// <returns>�ɹ�����:1��ʧ�ܷ���:-1</returns>
        private int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            
            //����������
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject empStatusObj = new FS.FrameWork.Models.NeuObject();//��Ա״̬ 
            
            try
            {
                if (patientInfo.Name != null)
                {
                    this.lblName.Visible = true;
                    this.lblName.Text = patientInfo.Name;
                }//��������

                #region ���߲��֣�ȡ����סԺ�����
                if (patientInfo.Pact.PayKind.ID.Equals("01"))//�Էѻ��������
                {
                    this.lblIllnessType.Visible = false; //sel �Էѻ��߲������ ���Ҫ���ټӻ���
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
                }//סԺ��

                if (patientInfo.PVisit.InTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime)
                {
                    this.lblDate.Visible = true;
                    this.lblDate.Text = patientInfo.PVisit.InTime.ToString() + "��";
                    //Edit By ZhangD 2010-9-26
                    //��Ժʱ��Ϊ��Ĭ��Ϊ��ǰʱ���Ϊ��
                    //{D645E761-AAD7-4c24-B19D-348437D7C4A8}
                    if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                    {
                        this.lblDate.Text += DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        this.lblDate.Text += patientInfo.PVisit.OutTime.ToShortDateString();
                    }
                }//��Ժʱ��
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
                }//��Ժ����


                if (!string.IsNullOrEmpty(patientInfo.PVisit.PatientLocation.Dept.Name))
                {
                    this.lblDept.Visible = true;
                    this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                }//�������

                if (patientInfo.PVisit.PatientLocation.Bed !=null)
                {
                    this.lblBedNo.Visible = true;
                    this.lblBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ToString();
                }//����

                if (patientInfo.Sex != null)
                {
                    lblSex.Visible = true;
                    lblSex.Text = patientInfo.Sex.ToString();
                }
                //�Ա�

                if (!string.IsNullOrEmpty(patientInfo.Age))
                {
                  //  lblAge.Visible = true;
                    lblAge.Text = patientInfo.Age;
                }//����

                #region ��Ա��� 
                if (!patientInfo.Pact.PayKind.ID.Equals("01"))//���Էѻ���,
                {
                    this.lblPayKind.Visible = true;

                    if (patientInfo.SIMainInfo.MedicalType.ID == "11")
                    {
                        neuObj = conMger.GetConstant("PersonType", patientInfo.SIMainInfo.PersonType.ID);//��Ա���
                        empStatusObj = conMger.GetConstant("EmplType", patientInfo.SIMainInfo.EmplType);//��Ա״̬
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
                    this.lblPayKind.Text = "�Է�";
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
        /// ���ܷ�����ϸ
        /// </summary>
        /// <param name="alFeeItemList">���߷�����ϸ�б�</param>
        /// <returns>���ػ��ܺ�ķ�����ϸ����</returns>
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
                        if (j == 0)//�����в����ڸ����շ���ϸ
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
                        if (j == 0)//�����в����ڸ����շ���ϸ
                        {
                            feeMinFeeGroup.Add(tempb);
                        }
                    }
            }
            return feeMinFeeGroup;
        }
        /// <summary>
        /// ͨ����С���û�ȡͳ�ƴ���memo���ӡ˳��
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

        #region IInpatientChargePrint ��Ա

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
            int m = 0;//��¼��Ϣ�����λ��
            int n = 1;
            string fee_Code;//���ñ���
            int i = 0;
            decimal sumcost = 0m;

            ArrayList almin = new ArrayList();

            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemListGroup = this.GetFeeItemGroup(ref feeItemColl);//������ϸ
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> minFeeList = this.minfeeGroutp(ref feeItemColl);
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList subFeeItem in minFeeList)//���ܴ���
            {

                objFeeStat = this.GetFeeStatByFeeCode(subFeeItem.Item.MinFee.ID, alFeeState);
                int count = 0;
                fpBlance_Sheet1.Cells[m, 1].Font = new Font("����", 9, FontStyle.Bold);
                fpBlance_Sheet1.Cells[m, 0].Text = "";//ȡ���ѱ�
                fpBlance_Sheet1.Cells[m, 1].Text = objFeeStat.Name;//feeItem.Invoice.Type.Name;//��������
                fpBlance_Sheet1.Cells[m, 7].Text = ((decimal)(subFeeItem.FT.TotCost)).ToString();
                fpBlance_Sheet1.Columns[7].Visible = true;
                fpBlance_Sheet1.Cells[m, 8].Text = " ";
                fpBlance_Sheet1.Cells[m, 7].Text = "";

                fee_Code = subFeeItem.Item.MinFee.ID;
                sumcost += subFeeItem.FT.TotCost; //�����ܺϼ�
                count = m;
                fpBlance_Sheet1.Cells[count, 6].Text = subFeeItem.FT.TotCost.ToString(); //������
                m++;

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in feeItemListGroup)
                {
                    if (i == 0)
                    {
                        this.lblTitle.Text = new FS.HISFC.BizLogic.Manager.Constant().GetHospitalName() + operObj.Dept.Name + this.lblTitle.Text;
                        this.lblChargeDateInfo.Text = this.lblChargeDateInfo.Text + feeItem.FeeOper.OperTime.ToShortDateString();
                        i = 1;
                    }
                    if (feeItem.Item.MinFee.ID != fee_Code)//�����ڸô��������
                    {
                        continue;
                    }
                    FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                    FS.HISFC.Models.SIInterface.Compare compareObj = new FS.HISFC.Models.SIInterface.Compare();
                    int SIRate = 0;
                    fpBlance_Sheet1.Cells[m, 0].Text = n.ToString();//���
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        SIRate = myInterface.GetCompareSingleItem("2", feeItem.Item.ID, ref compareObj);
                        fpBlance_Sheet1.Cells[m, 1].Text = compareObj.CenterItem.ID;//ͳһ����
                    }
                    else
                    {
                        fpBlance_Sheet1.Cells[m, 1].Text = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItem.Item.ID).GBCode;//ͳһ����
                    }
                    //�շ���Ŀ���
                    if (feeItem.UndrugComb.ID != "")
                        fpBlance_Sheet1.Cells[m, 2].Text = "��" + feeItem.UndrugComb.Name + "��" + feeItem.Item.Name;// + "/" + feeItemList.Item.Specs;
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

                    fpBlance_Sheet1.Cells[m, 3].Text = feeItem.Item.PriceUnit;//��λ
                    fpBlance_Sheet1.Cells[m, 4].Text = feeItem.Item.Qty.ToString("F2");//����

                    if (feeItem.Item.PackQty > 0)
                    {
                        fpBlance_Sheet1.Cells[m, 5].Text = (feeItem.Item.Price / feeItem.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');//����
                    }
                    else
                    {
                        fpBlance_Sheet1.Cells[m, 5].Text = feeItem.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');//����
                    }
                    fpBlance_Sheet1.Cells[m, 6].Text = feeItem.FT.TotCost.ToString();//���
                    fpBlance_Sheet1.Cells[m, 7].Text = deptHelper.GetName(feeItem.ExecOper.Dept.ID);//���ÿ���
                    fpBlance_Sheet1.Cells[m, 8].Text = "";
                    n++;
                    m++;
                }
            }
            fpBlance_Sheet1.Cells[m, 0].Text = "�ϼ�";
            fpBlance_Sheet1.Cells[m, 6].Text = sumcost.ToString();
            fpBlance_Sheet1.Cells[m, 8].Text = "";//�ϼƴ���ҽ��������ʾΪ��
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

            #region IComparer<FeeItemList> ��Ա

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
