using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace InterfaceInstanceDefault.IInvoicePrint
{
    /// <summary>
    /// ���﷢Ʊ��ӡ�ӿ�ʵ��
    /// </summary>
    public partial class IInvoicePrintDefault : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint
    {
        public IInvoicePrintDefault()
        {
            InitializeComponent();
        }
        #region ����
        /// <summary>
        /// �ַ�Ʊ���֧����ʽ
        /// </summary>
        private string setPayModeType;
        /// <summary>
        /// �ַ�Ʊ���֧����ʽ
        /// </summary>
        private string splitinvoicepaymode;
        /// <summary>
        /// �����Ƿ�ΪԤ��ģʽ
        /// </summary>
        private bool isPreView = false;
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        private System.Data.IDbTransaction trans;
        #endregion

        #region ����
        #region IInvoicePrint ��Ա

        /// <summary>
        /// �ؼ������������д��
        /// </summary>
        public string Description
        {
            get { return "��Ʊ��ӡʵ��"; }
        }

        /// <summary>
        /// �����Ƿ�ΪԤ��ģʽ
        /// </summary>
        public bool IsPreView
        {
            set { this.isPreView = value; }
        }
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);

                    return -1;
                }
                string paperName=string.Empty ;
                //if (this.InvoiceType == "MZ05")
                //{
                //    paperName = "MZTK";

                //}
                //else if (this.InvoiceType == "MZ01")
                //{
                paperName = "MZFP";
                //}

                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
                ////ֽ�ſ��
                //ps.Width = this.Width;
                ////ֽ�Ÿ߶�
                //ps.Height = this.Height;
                //�ϱ߾�
                ps.Top = 0;
                //��߾�
                ps.Left = 0;               
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
                while (alName.Count>0)
                {
                    SetPrintValueOther();
                    PrintOther(); 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        public int PrintOther()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);

                    return -1;
                }
                string paperName = string.Empty;
                //if (this.InvoiceType == "MZ05")
                //{
                //    paperName = "MZTK";

                //}
                //else if (this.InvoiceType == "MZ01")
                //{
                paperName = "MZFP";
                //}

                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize(paperName, 0, 0);
                ////ֽ�ſ��
                //ps.Width = this.Width;
                ////ֽ�Ÿ߶�
                //ps.Height = this.Height;
                //�ϱ߾�
                ps.Top = 0;
                //��߾�
                ps.Left = 0;
                print.SetPageSize(ps);
                print.PrintPage(0, 0, this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ӡ��������
        /// </summary>
        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
        public int PrintOtherInfomation()
        {
            return 1;
        }
        /// <summary>
        /// �ַ�Ʊ���֧����ʽ
        /// </summary>
        public string SetPayModeType
        {
            set { this.setPayModeType = value; }
        }
        /// <summary>
        /// �����Ƿ�ΪԤ��ģʽ
        /// </summary>
        public void SetPreView(bool isPreView)
        {
            this.isPreView = isPreView;
        }
        /// <summary>
        /// ���ô�ӡ��������
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="Invoices">��������Ʊ��Ϣ</param>
        /// <param name="invoiceDetails">���з�Ʊ��ϸ��Ϣ</param>
        /// <param name="feeDetails">���з�����Ϣ</param>
        /// <returns></returns>
        public int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, System.Collections.ArrayList Invoices, System.Collections.ArrayList invoiceDetails, System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

       

        Control c;
        ArrayList alName = new ArrayList();
        ArrayList alValue = new ArrayList();
        decimal  drugTotCost = 0;
        FS.HISFC.Models.Registration.Register regInfoOther;
        FS.HISFC.Models.Fee.Outpatient.Balance invoiceOther;

        /// <summary>
        /// ���÷�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail,ArrayList alPayMode,
            ArrayList alFeeItemList,
            bool isPreview)
        {
           //this.isPreView = false  ;
            try
            {
                this.regInfoOther = regInfo.Clone();
                invoiceOther = invoice.Clone();
                this.Register = regInfo;
                this.Controls.Clear();
                //���������ϸΪ�գ��򷵻�
                if (alFeeItemList.Count <= 0)
                {
                    return -1;
                }
                #region ��¡һ��������ϸ��Ϣ�б���Ϊ���������Ҫ���б�Ԫ����ɾ��������
                ArrayList alInvoiceDetailClone = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList det in alInvoiceDetail)
                {
                    alInvoiceDetailClone.Add(det.Clone());
                }
                #endregion
                if (this.InvoiceType == "MZ01")
                {
                    c = new ucMZFP();
                    while (c.Controls.Count > 0)
                    {
                        this.Controls.Add(c.Controls[0]);
                    }
                    this.Size = c.Size;
                    this.InitReceipt();
                    SetMZFPPrintValue (regInfo,
                           invoice,
                           alInvoiceDetailClone,
                           alFeeItemList,
                           isPreview);
                }
              
                //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
                if (isPreview)
                {
                    SetToPreviewMode();
                }
                else
                {
                    SetToPrintMode();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ���÷�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        public int SetPrintValueOther()
        {
            //this.isPreView = false  ;
            try
            {
                this.Controls.Clear();
                if (this.InvoiceType == "MZ01")
                {
                    c = new ucMZFP();
                    while (c.Controls.Count > 0)
                    {
                        this.Controls.Add(c.Controls[0]);
                    }
                    this.Size = c.Size;
                    this.InitReceiptOther ();
                  
                    SetMZFPPrintValueOther ();
                }

                ////���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
                //if (isPreview)
                //{
                //    SetToPreviewMode();
                //}
                //else
                //{
                SetToPrintMode();
                //}
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// �����ֽ�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        private int SetMZFPPrintValueOther()
        {
            try
            {
                ucMZFP   ucReceipt = (ucMZFP)c;
                int otherIdx = 0;
                while (alName.Count > 0)
                {
                    if (otherIdx < 3)
                    {
                        Label lName = GetFeeNameLable("lblOtherInvoiceNO" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoiceOther .Invoice.ID;
                        lName = GetFeeNameLable("lblOtherName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfoOther.Name;
                        lName = GetFeeNameLable("lblOtherDate" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoiceOther.PrintTime.ToShortDateString();
                        lName = GetFeeNameLable("lblOtherDoctorInfoTempletDeptName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfoOther.DoctorInfo.Templet.Dept.Name;
                        lName = GetFeeNameLable("lblOtherFeeName" + otherIdx.ToString(), lblPrint);
                        lName.Text = alName[0].ToString();
                        lName = GetFeeNameLable("lblOtherFeeInfo" + otherIdx.ToString(), lblPrint);
                        lName.Text = FS.FrameWork.Public.String.FormatNumberReturnString(System.Convert.ToDecimal(alValue[0]), 2).PadLeft(9, ' ');
                        otherIdx++;
                        alName.RemoveAt(0);
                        alValue.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
                ucReceipt.lblScrap.Visible = true;
                ucReceipt.lblScrap1.Visible = true;
                ucReceipt.lblScrap.Text = "����";
                ucReceipt.lblScrap1.Text = "����";
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// �����ֽ�Ʊ��ӡ����
        /// </summary>
        /// <param name="regInfo">�Һ���Ϣ</param>
        /// <param name="invoice">��Ʊ������Ϣ</param>
        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
        /// <returns></returns>
        private int SetMZFPPrintValue(
            FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
            ArrayList alInvoiceDetail,
            ArrayList alFeeItemList,
            bool isPreview)
        {
            try
            {
                #region ���÷�Ʊ��ӡ����
                ucMZFP ucReceipt = (ucMZFP)c;

                #region ҽ�ƻ���
                //ucReceipt.lblYiLiaoJiGou.Text = "�������������ڶ�����ҽԺ";
                #endregion

                #region �����
                ucReceipt.lblCardNO.Text = regInfo.PID.CardNO;
                ucReceipt.lblCardNO1.Text = regInfo.PID.CardNO;
                #endregion

                #region ��ӡʱ��
                ucReceipt.lblDate.Text = invoice.PrintTime.ToString("yyyy  MM  dd");
                ucReceipt.lblDateTime.Text = invoice.PrintTime.ToShortTimeString();
                ucReceipt.lblDate1.Text = invoice.PrintTime.ToString("yyyy  MM  dd");
                //ucReceipt.lblDateTime1.Text = invoice.PrintTime.ToShortTimeString();
                #endregion

                #region ��Ʊ��
                ucReceipt.lblInvoiceNO.Text = invoice.Invoice.ID;
                ucReceipt.lblInvoiceNO1.Text = invoice.Invoice.ID;
                #endregion

                #region �շ�Ա
                FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
                string operUserCode = string.Empty;
                operUserCode = person.GetPersonByID(invoice.BalanceOper.ID).Name;
                ucReceipt.lblOperName.Text = operUserCode;
                ucReceipt.lblOperName1.Text = operUserCode;
                #endregion

                #region ��ͬ��λ
                //if (invoice.Patient.Pact.PayKind.ID == "01" || invoice.Patient.Pact.PayKind.ID == "02")
                //{
                ucReceipt.lblPactName.Text = regInfo.Pact.Name;
                ucReceipt.lblPactName1.Text = regInfo.Pact.Name;
                //} 
                #endregion


                #region ����
                ucReceipt.lblName.Text = regInfo.Name;
                ucReceipt.lblName1.Text = regInfo.Name;
                #endregion

                #region �Ʊ�
                //FS.HISFC.Models.Fee.Outpatient.FeeItemList detFeeItemList = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                //ucReceipt.lblExeDept.Text =  detFeeItemList.ExecOper.Dept.Name;
                #endregion
                #region ����ҽʦ
                //ucReceipt.lblRecipeOperName.Text = regInfo.DoctorInfo.Templet.Dept .Name ; 
                #endregion
                #region ���ô���
                //Ʊ����Ϣ
                decimal[] FeeInfo =
                    //---------------------1-----------2------------3------------4-------------5-----------------
                    new decimal[15] { decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      //decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      //decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};
                //Ʊ����Ϣ
                string[] FeeInfoName =
                    //---------------------1-----------2------------3------------4-------------5-----------------
                    new string[15] { string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     //string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
                                     //string.Empty,string.Empty,string.Empty,string.Empty,
                                     string.Empty,string.Empty,string.Empty,string.Empty,string.Empty};

                //ͳ�ƴ�����Ŀ����ֱ��ȡ
                for (int i = 0; i < alInvoiceDetail.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                    detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];
                    if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
                    {
                        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
                        if (detail.FeeCodeStat.Name.Length > 5)
                        {
                            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name.Substring(0, 5);
                        }
                        else
                        {
                            FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name;
                        }
                        //  FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name.Substring(0,5); 
                    }
                }
                int feeInfoNameIdx = 0;
                int FeeInfoIndex = 0;
                foreach (decimal d in FeeInfo)
                {//������
                    //FeeInfo[FeeInfoIndex] = 999999.99m;
                    //����
                    Label lName = GetFeeNameLable("lblFeeName" + feeInfoNameIdx.ToString(), lblPrint);
                    //ֵ
                    Label lValue = GetFeeNameLable("lblFeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
                    if (lName != null)
                    {
                        if (FeeInfo[FeeInfoIndex] > 0)
                        {
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "��ͨ�˹����ٲ��Ϸ�";
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "";
                            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');
                            //������һ��ؼ�

                            if (FeeInfoIndex > 2)
                            {
                                alName.Add(FeeInfoName[FeeInfoIndex]);
                                alValue.Add(FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2));
                            }
                            else
                            {
                                drugTotCost += FeeInfo[FeeInfoIndex];
                            }

                        }
                    }
                    //����
                    lName = GetFeeNameLable("lbl1FeeName" + feeInfoNameIdx.ToString(), lblPrint);
                    //ֵ
                    lValue = GetFeeNameLable("lbl1FeeInfo" + feeInfoNameIdx.ToString(), lblPrint);
                    if (lName != null)
                    {
                        if (FeeInfo[FeeInfoIndex] > 0)
                        {
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "��ͨ�˹����ٲ��Ϸ�";
                            //lName.Text = FeeInfoName[FeeInfoIndex] + "";
                            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9, ' ');
                        }
                    }
                    feeInfoNameIdx++;
                    FeeInfoIndex++;
                }


                #endregion
                int otherIdx = 0;
                if (drugTotCost > 0)
                {
                    ucReceipt.lblOtherInvoiceNO0.Text = invoice.Invoice.ID;
                    ucReceipt.lblOtherName0.Text = regInfo.Name;
                    ucReceipt.lblOtherDate0.Text = invoice.PrintTime.ToShortDateString();
                    ucReceipt.lblOtherDoctorInfoTempletDeptName0.Text = regInfo.DoctorInfo.Templet.Dept.Name;
                    ucReceipt.lblOtherFeeName0.Text = "ҩ��";
                    ucReceipt.lblOtherFeeInfo0.Text = FS.FrameWork.Public.String.FormatNumberReturnString(drugTotCost, 2).PadLeft(9, ' ');
                    otherIdx++;
                }

                while (alName.Count > 0)
                {
                    if (otherIdx < 3)
                    {
                        Label lName = GetFeeNameLable("lblOtherInvoiceNO" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoice.Invoice.ID;
                        lName = GetFeeNameLable("lblOtherName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfo.Name;
                        lName = GetFeeNameLable("lblOtherDate" + otherIdx.ToString(), lblPrint);
                        lName.Text = invoice.PrintTime.ToShortDateString();
                        lName = GetFeeNameLable("lblOtherDoctorInfoTempletDeptName" + otherIdx.ToString(), lblPrint);
                        lName.Text = regInfo.DoctorInfo.Templet.Dept.Name;
                        lName = GetFeeNameLable("lblOtherFeeName" + otherIdx.ToString(), lblPrint);
                        lName.Text = alName[0].ToString();
                        lName = GetFeeNameLable("lblOtherFeeInfo" + otherIdx.ToString(), lblPrint);
                        lName.Text = FS.FrameWork.Public.String.FormatNumberReturnString(System.Convert.ToDecimal(alValue[0]), 2).PadLeft(9, ' ');
                        otherIdx++;
                        alName.RemoveAt(0);
                        alValue.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }

                #region ҽ����Ϣ



                if (invoice.Patient.Pact.PayKind.ID == "02")
                {
                    ucReceipt.lblSSN.Text = regInfo.SSN;
                    ucReceipt.lblOwnCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OwnCost, 2);
                    ucReceipt.lblPayCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PayCost, 2);
                    ucReceipt.lblPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PubCost, 2);
                    ucReceipt.lblOverCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OverCost, 2);
                    ucReceipt.lblIndividualBalance.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance, 2);


                    ucReceipt.lblSSN1.Text = regInfo.SSN;
                    ucReceipt.lblOwnCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OwnCost, 2);
                    ucReceipt.lblPayCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PayCost, 2);
                    ucReceipt.lblPubCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PubCost, 2);
                    //ucReceipt.lblOverCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OverCost, 2);
                    ucReceipt.lblIndividualBalance1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance, 2);

                    //-----1---------------------------2----------------3-----------------------4-----------------------5-----------------6----------------
                    //|ҽ�Ʒ��ܶ�TotCost|�����ʻ�֧�����PayCost|ͳ��֧�����PubCost|�����ֽ�֧��OwnCost|������֧�����OverCost|����Ա����֧�����OfficalCost
                    //-----------------7------------------8-----------------------9-----------
                    //|����������֧��BaseCost|������Աͳ��֧��PubOwnCost|ҽԺ�������HosCost
                    //-------------10-------------11----------------------12------------13------------------
                    //|�ϴν���ͳ��ҽ�Ʒ����ۼ�|���ν���ͳ��ҽ�Ʒ��ý��|�ϴθ����ʻ����|�����Էѽ��
                    //----14---------------15-------------16--------------------------------17----------
                    //|����ҩƷ��������|�𸶱�׼�Ը����|�ֶ�������|�����ⶥ�߸����Ը����|סԺ�ⶥ�����Ϲ���Ա����֧�����
                    //----18---------------------------19------20--------------21--------------------
                    //|סԺ�Ը����ֹ���Ա����֧�����|סԺ�˴�|���˻���֧�����|��������֧�����|
                    string[] temp = regInfo.SIMainInfo.Memo.Split('|');
                    ucReceipt.lblTemp20.Text = temp[20];
                    ucReceipt.lblTemp21.Text = temp[21];
                    ucReceipt.lbl1Temp20.Text = temp[20];
                    ucReceipt.lbl1Temp21.Text = temp[21];
                    ucReceipt.lblIndividualBalanceNew.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance - regInfo.SIMainInfo.PayCost , 2);
                    ucReceipt.lblIndividualBalanceNew1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.IndividualBalance - regInfo.SIMainInfo.PayCost , 2);
                }


                #endregion

                #region Сд�ܽ��
                ucReceipt.lblDownTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
                ucReceipt.lblDownTotCost1.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
                #endregion

                #region ��д�ܽ��
                ucReceipt.lblUpTotCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost).PadLeft(10,' ');
                ucReceipt.lblUpTotCost1.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost).PadLeft(10,' ');
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(IDbTransaction trans)
        {
            this.trans = trans;
        }
        /// <summary>
        /// �ַ�Ʊ���֧����ʽ
        /// </summary>
        public string SplitInvoicePayMode
        {
            set { this.splitinvoicepaymode = value; }
        }
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        #endregion

        /// <summary>
        /// ����Ϊ��ӡģʽ
        /// </summary>
        public void SetToPrintMode()
        {
            //��Ԥ������Ϊ���ɼ�
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }
        /// <summary>
        /// ����ΪԤ��ģʽ
        /// </summary>
        public void SetToPreviewMode()
        {
            //��Ԥ������Ϊ�ɼ�
            SetLableVisible(true, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        /// <summary>
        /// ��ӡ�õı�ǩ����
        /// </summary>
        public Collection<Label> lblPrint;
        /// <summary>
        /// Ԥ���õı�ǩ����
        /// </summary>
        public Collection<Label> lblPreview;

        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ���������Ҫ׷����Ʊ��ʱ
        /// </remarks>
        private void InitReceipt(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region ����ӡ�ֵĴ�ӡ��ֵ���
                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "ӡ")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }

        /// <summary>
        /// ��Ʊֻ��ӡ��д���� ��ӡ��ʮ��
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        public static string GetUpperCashByNumber(decimal Cash)
        {
            #region ��д�ܽ��
            string returnValue = string.Empty;
            string[] strMoney = new string[8];
            //---------------------------|\*/|-----������㣢��û���ã�����������
            string[] unit = { "��", "��", "��", "Ԫ", "ʰ", "��", "Ǫ", "��", "ʮ��" };
            strMoney = GetUpperCashNumberByNumber(FS.FrameWork.Public.String.FormatNumber(Cash, 2));
            bool isStart = false;
            string tempDaXie = string.Empty;
            for (int i = 0; i < strMoney.Length; i++)
            {
                #region �ӷ���λ��ʼ��ӡ
                if (!isStart)
                {
                    if (strMoney[i] != "��")
                    {
                        isStart = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                #endregion
                if (strMoney[i] != null)
                {
                    if (strMoney[i] != "��")
                    {
                        tempDaXie = strMoney[i] + unit[i] + tempDaXie;
                        returnValue = tempDaXie + returnValue;
                        tempDaXie = string.Empty;
                    }
                    else
                    {
                        tempDaXie = "��";
                    }
                }
            }
            return returnValue;
            #endregion
        }

        /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            //foreach (Control c in this.Controls[0].Controls)
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region ����ӡ�ֵĴ�ӡ��ֵ���
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "ӡ")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        } /// <summary>
        /// ��ʼ���վ�
        /// </summary>
        /// <remarks>
        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
        /// </remarks>
        private void InitReceiptOther()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            //foreach (Control c in this.Controls[0].Controls)
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region ����ӡ�ֵĴ�ӡ��ֵ���
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "ӡ")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            l.Text = "";
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        l.Text = "";
                        lblPreview.Add(l);
                    }
                    l.Text = "";
                }
            }
        }
        /// <summary>
        /// ���ñ�ǩ���ϵĿɼ���
        /// </summary>
        /// <param name="v">�Ƿ�ɼ�</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Visible = v;                
            }
        }


        /// <summary>
        /// ���ô�ӡ���ϵ�ֵ
        /// </summary>
        /// <param name="t">ֵ����</param>
        /// <param name="l">��ǩ����</param>
        private void SetLableText(string[] t, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Text = "";
            }
            if (t != null)
            {
                if (t.Length <= l.Count)
                {
                    int i = 0;
                    foreach (string s in t)
                    {
                        l[i].Text = s;
                        i++;
                    }
                }
                else
                {
                    if (t.Length > l.Count)
                    {
                        int i = 0;
                        foreach (Label lbl in l)
                        {
                            lbl.Text = t[i];
                            i++;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ���ָ�����������
        /// </summary>
        /// <param name="n">����</param>
        /// <returns>�������������ؼ�</returns>
        public System.Windows.Forms.Label GetFeeNameLable(string n, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                if (lbl.Name == n)
                {
                    return lbl;
                }
            }
            return null;
        }
        /// <summary>
        /// ���ָ�����������
        /// </summary>
        /// <param name="n">����</param>
        /// <returns>�������������ؼ�</returns>
        public System.Windows.Forms.Label GetFeeNameLable(string n, System.Windows.Forms.Control control)
        {
            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                if (c.Name == n)
                {
                    return (System.Windows.Forms.Label)c;
                }
            }
            return null;
        }
        /// <summary>
        /// ��Ʊֻ��ӡ��д���� ��ӡ��ʮ��
        /// </summary>
        /// <param name="Cash"></param>
        /// <returns></returns>
        public static string[] GetUpperCashNumberByNumber(decimal Cash)
        {
            string[] sNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
            string[] sReturn = new string[9];
            string strCash = null;
            //���λ��
            int iLen = 0;
            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
            if (strCash.Length > 9)
            {
                strCash = strCash.Substring(strCash.Length - 9);
            }

            //���λ��
            iLen = 9 - strCash.Length;
            for (int j = 0; j < iLen; j++)
            {
                int k = 0;
                k = 8 - j;
                sReturn[k] = "��";
            }
            for (int i = 0; i < strCash.Length; i++)
            {
                string Temp = null;

                Temp = strCash.Substring(strCash.Length - 1 - i, 1);

                if (Temp == ".")
                {
                    continue;
                }
                sReturn[i] = sNumber[int.Parse(Temp)];
            }
            return sReturn;
        }
        #endregion
        private string invoiceType;

        public string InvoiceType
        {
            get { return invoiceType; }
        }

        private FS.HISFC.Models.Registration.Register register;
        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                //register = value;
                //if (register.Pact.ID == "7")
                //{
                //    invoiceType = "MZ05";
                //}
                //else
                //{
                invoiceType = "MZ01";
                //}
            }
        }

        #region IInvoicePrint ��Ա


        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo, FS.HISFC.Models.Fee.Outpatient.Balance invoice, ArrayList invoiceDetails, ArrayList feeDetails, bool isPreview)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
