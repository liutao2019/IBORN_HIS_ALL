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


        #region IBalanceInvoicePrint ��Ա

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
        /// Ϊ������Ϣ��label�ؼ���ֵ
        /// </summary>
        /// <returns></returns>
        private int SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            //����������
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject empStatusObj = new FS.FrameWork.Models.NeuObject();//��Ա״̬

            //������뵥ҵ���
           // FS.HISFC.BizLogic.Order.CheckSlip checkSlip = new FS.HISFC.BizLogic.Order.CheckSlip();

            try
            {
                //���ô�ӡ����
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
                }//��������

                if (patientInfo.Age != null)
                {
                    this.lblage.Visible = true;
                    this.lblage.Text = patientInfo.Age;
                }//����

                if (patientInfo.Sex != null)
                {
                    this.lblsex.Visible = true;
                    this.lblsex.Text = patientInfo.Sex.ToString();
                }//�Ա�

                if (patientInfo.SIMainInfo.InDiagnose != null)
                {
                    this.lblInDiagNose.Visible = true;
                    this.lblInDiagNose.Text = patientInfo.SIMainInfo.InDiagnose.ID;
                }//��Ժ���

                if (patientInfo.SIMainInfo.OutDiagnose != null)
                {
                    this.lbloutDiagNose.Visible = true;
                    this.lbloutDiagNose.Text = patientInfo.SIMainInfo.OutDiagnose.ID;
                }//��Ժ���

                if (patientInfo.IDCard != null)
                {
                    this.lblIDCard.Visible = true;
                    this.lblIDCard.Text = patientInfo.IDCard;
                }//���֤��

                if (patientInfo.PhoneHome != null)
                {
                    this.lbllxdh.Visible = true;
                    this.lbllxdh.Text = patientInfo.PhoneHome;
                }//�绰
                    if (patientInfo.Patient.Pact.PayKind.ID != "01")//���Էѻ���
                //if (patientInfo.Pact.PayKind.ID != "01")//���Էѻ��� {2F3ACEBA-EFD5-4587-BCE2-603127FD0461}
                {
                    this.lblMINo.Visible = true;
                    this.lblMINo.Text = patientInfo.SSN;
                }//ҽ�����߸��˱�ţ� addbyluoff20090915

                if (patientInfo.CompanyName != null)
                {
                    this.lblDNN.Visible = true;
                    this.lblDNN.Text = "";
                }//���Ժ�
                
                if (patientInfo.PID.ID != null)
                {
                    this.lblInpatientNo.Visible = true;
                    this.lblInpatientNo.Text = patientInfo.PID.ID;
                }//סԺ��

                if (patientInfo.PVisit.InTime != null)
                {
                    this.lblInpatientTime.Visible = true;
                    this.lblInpatientTime.Text = patientInfo.PVisit.InTime.ToLongDateString();
                }//��Ժʱ��

                if (patientInfo.PVisit.OutTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime && patientInfo.PVisit.InTime != null)
                {
                    this.lblInpatientDay.Visible = true;
                    //DateTime dt = new DateTime();
                    TimeSpan ts = new TimeSpan();
                    //dt= (DateTime)(patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime);
                    ts = patientInfo.PVisit.OutTime - patientInfo.PVisit.InTime;
                    //this.lblInpatientDay.Text = dt.Day.ToString();
                    this.lblInpatientDay.Text = ts.Days.ToString();
                }//��Ժ����

                if (patientInfo.PVisit.OutTime != null && patientInfo.PVisit.OutTime > patientInfo.PVisit.InTime )
                {
                    this.lblOutTime.Visible = true;
                    this.lblOutTime.Text = patientInfo.PVisit.OutTime.ToLongDateString();
                }//��Ժʱ��

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
                }//��Ժ����

                if (patientInfo.PVisit.PatientLocation.NurseCell != null)
                {
                    this.lblOutpatientDept.Visible = true;
                    this.lblOutpatientDept.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
                }//��Ժ����

                if(patientInfo.ID != null)
                {
                    this.lblInpatientNumber.Visible = true;
                    this.lblInpatientNumber.Text = patientInfo.ID;
                }//סԺ��ˮ��

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// Ϊ������Ϣlabel�ؼ���ֵ
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceInfo"></param>
        /// <param name="alBalanceList"></param>
        /// <param name="alPayList"></param>
        /// <returns></returns>
        private int SetBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo, System.Collections.ArrayList alBalanceList, System.Collections.ArrayList alPayList)
        {
            #region ΪfpBlance�ؼ���ֵ
            int balanceList = alBalanceList.Count;

            for (int i = 0; i < balanceList; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.BalanceList bl = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[i];

                fpBlance_Sheet1.Cells[i, 0].Text = bl.FeeCodeStat.StatCate.Name.ToString(); //������Ŀ

                fpBlance_Sheet1.Cells[i, 1].Text = bl.BalanceBase.FT.TotCost.ToString();    //�ܽ��

                fpBlance_Sheet1.Cells[i, 2].Text = bl.BalanceBase.FT.PubCost.ToString();    //ͳ����

                fpBlance_Sheet1.Cells[i, 3].Text = bl.BalanceBase.FT.OwnCost.ToString();    //�Էѽ��

            }

            //�ܷ��ã��ܣ�
            fpBlance_Sheet1.Cells[17, 1].Text = patientInfo.FT.TotCost.ToString();
            //����ҽ�ƣ��ܣ�
            fpBlance_Sheet1.Cells[17, 2].Text = patientInfo.FT.PubCost.ToString();
            //�Էѷ��ã��ܣ�
            fpBlance_Sheet1.Cells[17, 3].Text = patientInfo.FT.OwnCost.ToString();

            #endregion

            #region ΪfpOwnCost�ؼ���ֵ

            string[,] itemDetail = GetSIItemDetail(patientInfo.ID);

            for (int i = 0; i < itemDetail.Length / itemDetail.Rank; i++)
            {

                //���� �Ը�����
                if (itemDetail[i, 0] == "0105")
                {
                    fpOwnCost_Sheet1.Cells[2, 2].Text = itemDetail[i, 1];
                }

                //������ 5000���²��� ���˷���
                if (itemDetail[i, 0] == "0212")
                {
                    fpOwnCost_Sheet1.Cells[3, 1].Text = itemDetail[i, 1];
                }

                //������ 5000���²��� �Ը�����
                if (itemDetail[i, 0] == "0108")
                {
                    fpOwnCost_Sheet1.Cells[3, 2].Text = itemDetail[i, 1];
                }

                //5000Ԫ����-10000���֣�֧��85%�� ���˷���
                if (itemDetail[i, 0] == "0213")
                {
                    fpOwnCost_Sheet1.Cells[4, 1].Text = itemDetail[i, 1];
                }

                //5000Ԫ����-10000���֣�֧��85%�� �Ը�����
                if (itemDetail[i, 0] == "0109")
                {
                    fpOwnCost_Sheet1.Cells[4, 2].Text = itemDetail[i, 1];
                }

                //10000Ԫ���ϲ��֣�֧��90%�� ���˷���
                if (itemDetail[i, 0] == "0214")
                {
                    fpOwnCost_Sheet1.Cells[5, 1].Text = itemDetail[i, 1];
                }

                //10000Ԫ���ϲ��֣�֧��90%�� �Ը�����
                if (itemDetail[i, 0] == "0110")
                {
                    fpOwnCost_Sheet1.Cells[5, 2].Text = itemDetail[i, 1];
                }

                //���ⶥ���Ը����ַ��� ���˷���
                fpOwnCost_Sheet1.Cells[6, 1].Text = "";

                //���ⶥ���Ը����ַ��� �Ը�����
                if (itemDetail[i, 0] == "0104")
                {
                    fpOwnCost_Sheet1.Cells[6, 2].Text = itemDetail[i, 1];
                }

                //�ϼ� ���˷���
                fpOwnCost_Sheet1.Cells[7, 1].Text = patientInfo.SIMainInfo.PubCost.ToString();

                //�ϼ� �Ը�����
                fpOwnCost_Sheet1.Cells[7, 2].Text = patientInfo.SIMainInfo.PubOwnCost.ToString();
            }

            #endregion

            #region ΪfpTotal��ֵ

            //סԺ�ܷ���
            fpTotal_Sheet1.Cells[0, 1].Text = patientInfo.FT.TotCost.ToString();
            //�����ֽ�֧��
            fpTotal_Sheet1.Cells[1, 1].Text = Convert.ToString(patientInfo.FT.OwnCost + patientInfo.FT.PayCost);
            //ҽ�Ʊ���֧�����
            fpTotal_Sheet1.Cells[2, 1].Text = patientInfo.FT.PubCost.ToString();
            fpTotal_Sheet1.Cells[2, 2].Text = FS.FrameWork.Function.NConvert.ToCapital(patientInfo.SIMainInfo.PubCost);
            
            #endregion

            #region ΪfpSign��ֵ

            //����Ա
            fpSign_Sheet1.Cells[7, 0].Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name;
            //����ʱ��
            fpSign_Sheet1.Cells[7,1].Text = System.DateTime.Now.ToLongDateString();

            #endregion

            return 1;
        }
        /// <summary>
        /// ȡ�ֽ�֧��������Ϣ
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
        /// ��ȡסԺ������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <returns>סԺ��ϸ��Ϣ</returns>
        private string[,] GetSIItemDetail(string InpatientNO)
        {
            string strSql = " select t.zfxm, t.je from si_yb_zyzf t where t.zylsh = '" + InpatientNO +"'";
            //" where t.zylsh = '' --סԺ��ˮ�� ";

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
        /// ��ȡסԺҽ��������Ŀ��Ϣ
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
        /// ��ȡסԺҽ��֧����Ŀ��Ϣ
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
        /// �ѽ��ת���ɴ�д
        /// </summary>
        /// <param name="Cash">���</param>
        /// <returns>ת����Ĵ�д���</returns>
        private string GetUpperCashbyNumber(decimal Cash)
        {
            //��д����
            string[] upperNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            //Сд����
            string[] lowerNumber = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //λ������
            string[,] unit = { { "11", "��" }, { "10", "ǧ��" }, { "9", "����" }, { "8", "ʮ��" }, { "7", "��" }, { "6", "ǧ" }, { "5", "��" }, { "4", "ʰ" }, { "3", "Ԫ" }, { "2", "��" }, { "1", "��" } };

            string sReturn = string.Empty;

            string tmpReturn = Cash.ToString().Replace(".", "");

            int tmp = 0;

            string tmpNum = string.Empty;

            for (int i = 0; i < tmpReturn.Length; i++)
            {
                //�����д����
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
                    //���쵥λ
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
        /// �Էѻ���Label��ֵ
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
        /// ���label�ؼ�
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
