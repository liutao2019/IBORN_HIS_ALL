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
        #region ����


        private System.Data.IDbTransaction trans;
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo;
        private string bloodMinFeeCode;//��Ѫ��������С���ñ���


        #endregion

        #region IBalanceListPrint ��Ա

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
        /// Ϊ������Ϣ��label�ؼ���ֵ
        /// </summary>
        /// <returns>�ɹ�����:1��ʧ�ܷ���:-1</returns>
        private int SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo curPatientInfo)
        {
            //����������
            Neusoft.HISFC.BizLogic.Manager.Constant conMger = new Neusoft.HISFC.BizLogic.Manager.Constant();
            Neusoft.FrameWork.Models.NeuObject neuObj = new Neusoft.FrameWork.Models.NeuObject();
            Neusoft.FrameWork.Models.NeuObject empStatusObj = new Neusoft.FrameWork.Models.NeuObject();//��Ա״̬

            //������뵥ҵ���
          //  Neusoft.HISFC.BizLogic.Order.CheckSlip checkSlip = new Neusoft.HISFC.Management.Order.CheckSlip();

            try
            {
                if (patientInfo.Name != null)
                {
                    this.lblName.Visible = true;
                    this.lblName.Text = patientInfo.Name;
                }//��������
                PatientInfo = curPatientInfo;
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
                    this.lblIllnessType.Visible = true;
                    this.lblIllnessType.Text = this.patientInfo.SIMainInfo.OutDiagnose.Name;//ҽ�����������
                }
                #endregion

                if (patientInfo.PID.ID != null)
                {
                    this.lblIpbNo.Visible = true;
                    this.lblIpbNo.Text = patientInfo.PID.ID;
                }//סԺ��

                if (patientInfo.PVisit.InTime != null && patientInfo.PVisit.OutTime != null)
                {
                    this.lblDate.Visible = true;
                    this.lblDate.Text = patientInfo.PVisit.InTime.ToString() + "��";
                    //Edit By ZhangD 2010-9-26
                    //��Ժʱ��Ϊ��Ĭ��Ϊ��ǰʱ���Ϊ��
                    //{D645E761-AAD7-4c24-B19D-348437D7C4A8}
                    if (patientInfo.PVisit.OutTime < patientInfo.PVisit.InTime)
                    {
                        this.lblDate.Text += DateTime.Now.ToString();
                    }
                    else
                    {
                        this.lblDate.Text += patientInfo.PVisit.OutTime.ToString();
                    }
                }//��Ժʱ��



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
                }//��Ժ����


                if (patientInfo.PVisit.PatientLocation.Dept.Name != null)
                {
                    this.lblDept.Visible = true;
                    this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
                }//�������

                if (patientInfo.PVisit.PatientLocation.Bed != null)
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

                if (patientInfo.Age != null)
                {
                    lblAge.Visible = true;
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
                        neuObj = conMger.GetConstant("MedicalType", patientInfo.SIMainInfo.MedicalType.ID);
                        this.lblPayKind.Text = neuObj.Name;
                    }
                }
                else
                {
                    this.lblPayKind.Visible = true;
                    this.lblPayKind.Text = "�Էѻ���";
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
        /// ��ʾ���û�����Ϣ
        /// </summary>
        /// <returns>�ɹ�����:1��ʧ�ܷ���:-1</returns>
        private int SetBalanceInfo(System.Collections.ArrayList alBalanceList1)
        {
            ArrayList alBalanceList = new ArrayList();
            alBalanceList.Clear();
            alBalanceList = alBalanceList1;
            if (alBalanceList.Count == 0)
                return -1;
            //���������
            fpFee_Sheet1.RowCount = 0;
            int m = 0;//��¼��Ϣ�����λ��
            string fee_Code;//���ñ���

            decimal totCost = 0; //�ϼ��ܷ���
            decimal ownCost = 0; //�ϼ��Է���

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
            ArrayList balanceListsort = new ArrayList();  //����ѱ�
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
                 
                fpFee_Sheet1.Cells[m, 0].Text = bl.FeeCodeStat.StatCate.Name.ToString();//��������

                fpFee_Sheet1.Cells[m, 1].Text = totCost1.ToString();// bl.BalanceBase.FT.TotCost.ToString();//�ܽ��
                totCost = totCost + totCost1;
                fpFee_Sheet1.Cells[m, 4].Text = ownCost1.ToString();// bl.BalanceBase.FT.OwnCost.ToString();//�Էѽ��
                fpFee_Sheet1.Cells[m, 5].Text = ownCost1.ToString();// bl.BalanceBase.FT.OwnCost.ToString();//�Էѽ��+�Ը����
                ownCost = ownCost + bl.BalanceBase.FT.OwnCost;
                //���ʽ��
                fpFee_Sheet1.Cells[m, 2].Text = ""; ((decimal)(bl.BalanceBase.FT.TotCost - bl.BalanceBase.FT.OwnCost)).ToString();
                //fpFee_Sheet1.Cells[m, 7].Text = "0";//�����ҽ��������ʾΪ0��{203861DF-5000-4bf5-A645-52A45812F413}

                fee_Code = bl.FeeCodeStat.MinFee.ID;
                m++;
            }
            fpFee_Sheet1.AddRows(m, 1);
            fpFee_Sheet1.Cells[m, 0].Text = "�ϼ�";
            fpFee_Sheet1.Cells[m, 1].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 4].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 5].Text = totCost.ToString();
            fpFee_Sheet1.Cells[m, 2].Text = "";//((decimal)(totCost - ownCost)).ToString();
            //fpFee_Sheet1.AddRows(m + 1, 1);
            //fpFee_Sheet1.Cells[m + 1, 0].Text = "Ԥ����ϼ�";
            //fpFee_Sheet1.Models.Span.Add(m + 1, 1, 1, 5);
            //fpFee_Sheet1.Cells[m + 1, 1].Text = this.PatientInfo.FT.PrepayCost.ToString("F2");
            return 1;
        }

        #region IBillPrint ��Ա

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
