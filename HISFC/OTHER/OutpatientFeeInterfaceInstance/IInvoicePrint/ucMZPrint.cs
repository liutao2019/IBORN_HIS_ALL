//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Collections;

//namespace ReceiptPrint
//{
//    public partial class ucMZPrint : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint
//    {
//        public ucMZPrint()
//        {
//            InitializeComponent();
//        }

//        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
//        #region ����
//        /// <summary>
//        /// �ַ�Ʊ���֧����ʽ
//        /// </summary>
//        private string setPayModeType;
//        /// <summary>
//        /// �ַ�Ʊ���֧����ʽ
//        /// </summary>
//        private string splitinvoicepaymode;
//        /// <summary>
//        /// �����Ƿ�ΪԤ��ģʽ
//        /// </summary>
//        private bool isPreView = false;
//        /// <summary>
//        /// ���ݿ�����
//        /// </summary>
//        private System.Data.IDbTransaction trans;
//        #endregion

//        #region ����
//        #region IInvoicePrint ��Ա

//        /// <summary>
//        /// �ؼ������������д��
//        /// </summary>
//        public string Description
//        {
//            get {
//                //{AF642C5F-85B8-46ea-9292-7929E54D8150}
//                string hospitalName = this.managerIntegrate.GetHospitalName();

//                return hospitalName;
//            }
//        }

//        /// <summary>
//        /// �����Ƿ�ΪԤ��ģʽ
//        /// </summary>
//        public bool IsPreView
//        {
//            set { this.isPreView = value; }
//        }
//        /// <summary>
//        /// ��ӡ����
//        /// </summary>
//        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
//        public int Print()
//        {
//            try
//            {
//                FS.FrameWork.WinForms.Classes.Print print = null;
//                try
//                {
//                    print = new FS.FrameWork.WinForms.Classes.Print();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("��ʼ����ӡ��ʧ��!" + ex.Message);

//                    return -1;
//                }
//                string paperName=string.Empty ;
//                if (this.InvoiceType == "MZ05")
//                {
//                    paperName = "MZTK";

//                }
//                else if (this.InvoiceType == "MZ01")
//                {
//                    paperName = "MZXJ";
//                }

//                FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize(paperName, 0, 0);
//                ////ֽ�ſ��
//                //ps.Width = this.Width;
//                ////ֽ�Ÿ߶�
//                //ps.Height = this.Height;
//                //�ϱ߾�
//                ps.Top = 0;
//                //��߾�
//                ps.Left = 0;               
//                print.SetPageSize(ps);
//                print.PrintPage(0, 0, this);
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//                return -1;
//            }

//            return 1;
//        }

//        /// <summary>
//        /// ��ӡ��������
//        /// </summary>
//        /// <returns>-1 ʧ�� 1 �ɹ�</returns>
//        public int PrintOtherInfomation()
//        {
//            return 1;
//        }
//        /// <summary>
//        /// �ַ�Ʊ���֧����ʽ
//        /// </summary>
//        public string SetPayModeType
//        {
//            set { this.setPayModeType = value; }
//        }
//        /// <summary>
//        /// �����Ƿ�ΪԤ��ģʽ
//        /// </summary>
//        public void SetPreView(bool isPreView)
//        {
//            this.isPreView = isPreView;
//        }
//        /// <summary>
//        /// ���ô�ӡ��������
//        /// </summary>
//        /// <param name="regInfo">�Һ���Ϣ</param>
//        /// <param name="Invoices">��������Ʊ��Ϣ</param>
//        /// <param name="invoiceDetails">���з�Ʊ��ϸ��Ϣ</param>
//        /// <param name="feeDetails">���з�����Ϣ</param>
//        /// <returns></returns>
//        public int SetPrintOtherInfomation(FS.HISFC.Models.Registration.Register regInfo, System.Collections.ArrayList Invoices, System.Collections.ArrayList invoiceDetails, System.Collections.ArrayList feeDetails)
//        {
//            return 1;
//        }

       

//        Control c;

//        /// <summary>
//        /// ���÷�Ʊ��ӡ����
//        /// </summary>
//        /// <param name="regInfo">�Һ���Ϣ</param>
//        /// <param name="invoice">��Ʊ������Ϣ</param>
//        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
//        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
//        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
//        /// <returns></returns>
//        public int SetPrintValue(FS.HISFC.Models.Registration.Register regInfo,
//            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
//            ArrayList alInvoiceDetail,
//            ArrayList alFeeItemList,
//            bool isPreview)
//        {
//           //this.isPreView = false  ;
//            try
//            {
//                this.Register = regInfo;
//                this.Controls.Clear();
//                //���������ϸΪ�գ��򷵻�
//                if (alFeeItemList.Count <= 0)
//                {
//                    return -1;
//                }
//                #region ��¡һ��������ϸ��Ϣ�б���Ϊ���������Ҫ���б�Ԫ����ɾ��������
//                ArrayList alInvoiceDetailClone = new ArrayList();
//                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList det in alInvoiceDetail)
//                {
//                    alInvoiceDetailClone.Add(det.Clone());
//                }
//                #endregion
//                if (this.InvoiceType == "MZ01")
//                {
//                    c = new ucMZXJ();
//                    while (c.Controls.Count > 0)
//                    {
//                        this.Controls.Add(c.Controls[0]);
//                    }
//                    this.Size = c.Size;
//                    this.InitReceipt();
//                    SetMZXJPrintValue (regInfo,
//                           invoice,
//                           alInvoiceDetailClone,
//                           alFeeItemList,
//                           isPreview);
//                }
//                if (this.InvoiceType == "MZ05")
//                {
//                    c = new ucMZTK();
//                    while (c.Controls.Count > 0)
//                    {
//                        this.Controls.Add(c.Controls[0]);
//                    }
//                    this.Size = c.Size;
//                    this.InitReceipt();
//                    SetMZTKPrintValue(regInfo,
//                           invoice,
//                           alInvoiceDetailClone,
//                           alFeeItemList,
//                           isPreview);
//                }
//                //���Ƹ��ݴ�ӡ��Ԥ����ʾѡ��
//                if (isPreview)
//                {
//                    SetToPreviewMode();
//                }
//                else
//                {
//                    SetToPrintMode();
//                }
//            }
//            catch (Exception ex)
//            {
//                return -1;
//            }
//            return 0;
//        }

//        /// <summary>
//        /// �����ֽ�Ʊ��ӡ����
//        /// </summary>
//        /// <param name="regInfo">�Һ���Ϣ</param>
//        /// <param name="invoice">��Ʊ������Ϣ</param>
//        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
//        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
//        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
//        /// <returns></returns>
//        private int SetMZXJPrintValue(
//            FS.HISFC.Models.Registration.Register regInfo,
//            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
//            ArrayList alInvoiceDetail,
//            ArrayList alFeeItemList,
//            bool isPreview)
//        {
//            try
//            {
//                string hospitalName = this.managerIntegrate.GetHospitalName();

               
//                #region ���÷�Ʊ��ӡ����
//                ucMZXJ   ucReceipt = (ucMZXJ)c;
//                ucReceipt.neuLabel20.Text = hospitalName;
//                #region ҽ�ƻ���
//                //ucReceipt.lblYiLiaoJiGou.Text = "�������������ڶ�����ҽԺ";
//                #endregion

//                #region �����
//                ucReceipt.lblBingli.Text = regInfo.PID.CardNO; 
//                #endregion

//                #region ��ӡʱ��
//                ucReceipt.lblDate.Text = invoice.PrintTime.ToShortDateString();
//                #endregion

//                #region ��ˮ��
//                ucReceipt.lblInvoiceNO.Text = invoice.Invoice.ID; 
//                #endregion

//                #region �շ�Ա
//                FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
//                string operUserCode = string.Empty;
//                operUserCode = invoice.BalanceOper.ID;
//                ucReceipt.lblOper.Text = operUserCode;
//                #endregion

//                #region ��ͬ��λ
//                //if (invoice.Patient.Pact.PayKind.ID == "01" || invoice.Patient.Pact.PayKind.ID == "02")
//                //{
//                ucReceipt.lblPactName.Text = regInfo.Pact.Name;
//                //} 
//                #endregion

//                #region �ֽ�֧��
//                if (invoice.Patient.Pact.PayKind.ID == "01" )
//                {
//                    ucReceipt.lblOwnCost.Text = ""; 
//                }
//                #endregion

//                #region ����
//                ucReceipt.lblName.Text = regInfo.Name;
//                #endregion

//                #region �Ʊ�
//                FS.HISFC.Models.Fee.Outpatient.FeeItemList detFeeItemList = alFeeItemList[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
//                ucReceipt.lblExeDept.Text =  detFeeItemList.ExecOper.Dept.Name;
//                #endregion
//                #region ����ҽʦ
//                ucReceipt.lblRecipeOperName.Text = regInfo.DoctorInfo.Templet.Doct.Name; 
//                #endregion
//                #region ���ô���
//                //Ʊ����Ϣ
//                decimal[] FeeInfo =
//                    //---------------------1-----------2------------3------------4-------------5-----------------
//                    new decimal[48] { decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                        decimal.Zero,decimal.Zero,decimal.Zero,
//                                      decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero};
//                //Ʊ����Ϣ
//                string[] FeeInfoName =
//                    //---------------------1-----------2------------3------------4-------------5-----------------
//                    new string[48] { string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                                string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,string.Empty,string.Empty,
//                        string.Empty,string.Empty,string.Empty,
//                                string.Empty,string.Empty,string.Empty,string.Empty,string.Empty};

//                //ͳ�ƴ�����Ŀ����ֱ��ȡ
//                for (int i = 0; i < alInvoiceDetail.Count; i++)
//                {
//                    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
//                    detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];
//                    if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
//                    {
//                        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
//                        FeeInfoName[detail.FeeCodeStat.SortID - 1] += detail.FeeCodeStat.Name; ;
//                    }
//                }
//                int feeInfoNameIdx = 0;
//                int FeeInfoIndex = 0;
//                foreach (decimal d in FeeInfo)
//                {                    
//                    //����
//                    Label lName = GetFeeNameLable("lblFeeName" + feeInfoNameIdx.ToString(), lblPrint);
//                    //ֵ
//                    Label lValue = GetFeeNameLable("lblFeeValue" + feeInfoNameIdx.ToString(), lblPrint);
//                    if (lName != null)
//                    {
//                        if (FeeInfo[FeeInfoIndex] > 0)
//                        {
//                            //lName.Text = FeeInfoName[FeeInfoIndex] + ":";
//                            lName.Text = FeeInfoName[FeeInfoIndex] + "";

//                            lValue.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2).PadLeft(9,' ');
//                            //������һ��ؼ�
//                            feeInfoNameIdx++;
//                        }
//                    }
//                    FeeInfoIndex++;
//                }


//                #endregion

//                #region ҽ����Ϣ
//                decimal GeRenCost = decimal.Zero;
//                decimal TongChouCost = decimal.Zero;
//                decimal XianJinCost = decimal.Zero;
//                decimal GongWuYuanCost = decimal.Zero;
//                decimal DaECost = decimal.Zero;
//                ucReceipt.label5.Text = "";
//                ucReceipt.label63.Text = "";
//                //�����˻�֧��
//                GeRenCost = regInfo.SIMainInfo.PayCost+regInfo.SIMainInfo.PubCost ;
//                if (GeRenCost != 0)
//                {
//                    ucReceipt.label63.Text = "ҽ���е���";
//                    ucReceipt.lblPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GeRenCost , 2).PadLeft(9, ' ');
//                }
//                ////ͳ�����֧��
//                //TongChouCost = regInfo.SIMainInfo.PubCost;
//                //if (TongChouCost != 0)
//                //{
//                //    ucReceipt.lblTongChouCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(TongChouCost , 2).PadLeft(9, ' ');
//                //}
//                //�ֽ�֧��
//                XianJinCost = regInfo.SIMainInfo.OwnCost;
//                if (XianJinCost != 0)
//                {
                   
//                    ucReceipt.lblXianJinCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(XianJinCost , 2).PadLeft(9, ' ');
//                }
//                ////����Ա����
//                //GongWuYuanCost = regInfo.SIMainInfo.OfficalCost;
//                //if (GongWuYuanCost != 0)
//                //{
//                //    ucReceipt.lblGongWuYuanCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(GongWuYuanCost  , 2).PadLeft(9, ' ');
//                //}
//                ////����
//                //DaECost = regInfo.SIMainInfo.OverCost ;
//                //if (DaECost != 0)
//                //{
//                //    ucReceipt.lblDaECost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(DaECost , 2).PadLeft(9, ' ');
//                //}
//                #endregion

//                #region Сд�ܽ��
//                //ucReceipt.lblTotCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
//                #endregion

//                #region ��д�ܽ��
//                ucReceipt.lblUpOwnCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.OwnCost);
//                #endregion
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                return -1;
//            }
//            return 0;
//        }

//        /// <summary>
//        /// �����ֽ�Ʊ��ӡ����
//        /// </summary>
//        /// <param name="regInfo">�Һ���Ϣ</param>
//        /// <param name="invoice">��Ʊ������Ϣ</param>
//        /// <param name="alInvoiceDetail">��Ʊ��ϸ��Ϣ</param>
//        /// <param name="alFeeItemList">������ϸ��Ϣ</param>
//        /// <param name="isPreview">�Ƿ�Ԥ��ģʽ</param>
//        /// <returns></returns>
//        private int SetMZTKPrintValue(
//            FS.HISFC.Models.Registration.Register regInfo,
//            FS.HISFC.Models.Fee.Outpatient.Balance invoice,
//            ArrayList alInvoiceDetail,
//            ArrayList alFeeItemList,
//            bool isPreview)
//        {
//            try
//            {
//                string hospitalName = this.managerIntegrate.GetHospitalName();

//                #region ���÷�Ʊ��ӡ����
//                ucMZTK ucReceipt = (ucMZTK)c; 
//                //����
//                ucReceipt.lblName.Text = regInfo.Name;
//                //��Ʊ��
//                ucReceipt.lblInvoiceID.Text = invoice.Invoice.ID;
//                //������
//                ucReceipt.lblCardNO.Text = regInfo.PID.CardNO;
//                //����
//                ucReceipt.lblPrintTime.Text = invoice.PrintTime.ToShortDateString();
//                //�տ�Ա��
//                ucReceipt.lblOperID.Text = invoice.BalanceOper.ID;
//                //�ֽ�ϼ�
//                ucReceipt.lblOwnCostA.Text = FS.FrameWork.Public.String.FormatNumberReturnString(invoice.FT.TotCost, 2);
//                //��д
//                ucReceipt.lblUpOwnCost.Text = FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost);
//                //����
//                ucReceipt.lblXianJinCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.OwnCost, 2);
//                //����Ľ�Ҳ���ǹ����е�pubcost
//                ucReceipt.lblPubCost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(regInfo.SIMainInfo.PubCost+regInfo.SIMainInfo.PayCost, 2);
//                //�ͱ�֤��
//                ucReceipt.lblTKNO.Text = regInfo.SIMainInfo.ICCardCode.ToString();

//                //Ʊ����Ϣ
//                decimal[] FeeInfo =
//                    //---------------------1-----------2------------3------------4-------------5-----------------
//                    new decimal[13] {decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                     decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,decimal.Zero,
//                                     decimal.Zero,decimal.Zero,decimal.Zero};


//                for (int i = 0; i < alInvoiceDetail.Count; i++)
//                {
//                    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = new FS.HISFC.Models.Fee.Outpatient.BalanceList();
//                    detail = (FS.HISFC.Models.Fee.Outpatient.BalanceList)alInvoiceDetail[i];
//                    if (detail.FeeCodeStat.SortID <= FeeInfo.Length)
//                    {
//                        FeeInfo[detail.FeeCodeStat.SortID - 1] += detail.BalanceBase.FT.TotCost;
//                    }
//                }
//                int FeeInfoIndex = 0;
//                foreach (decimal d in FeeInfo)
//                {
//                    Label l = GetFeeNameLable("lblFeeInfo" + FeeInfoIndex.ToString(), lblPrint);
//                    if (l != null)
//                    {
//                        if (FeeInfo[FeeInfoIndex] > 0)
//                        {
//                            l.Text = FS.FrameWork.Public.String.FormatNumberReturnString(FeeInfo[FeeInfoIndex], 2);
//                        }
//                    }
//                    FeeInfoIndex++;
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                return -1;
//            }
//            return 0;
//        }

//        /// <summary>
//        /// �������ݿ�����
//        /// </summary>
//        /// <param name="trans"></param>
//        public void SetTrans(IDbTransaction trans)
//        {
//            this.trans = trans;
//        }
//        /// <summary>
//        /// �ַ�Ʊ���֧����ʽ
//        /// </summary>
//        public string SplitInvoicePayMode
//        {
//            set { this.splitinvoicepaymode = value; }
//        }
//        /// <summary>
//        /// ���ݿ�����
//        /// </summary>
//        public IDbTransaction Trans
//        {
//            set { this.trans = value; }
//        }

//        #endregion

//        /// <summary>
//        /// ����Ϊ��ӡģʽ
//        /// </summary>
//        public void SetToPrintMode()
//        {
//            //��Ԥ������Ϊ���ɼ�
//            SetLableVisible(false, lblPreview);
//            foreach (Label lbl in lblPrint)
//            {
//                lbl.BorderStyle = BorderStyle.None;
//                lbl.BackColor = SystemColors.ControlLightLight;
//            }
//        }
//        /// <summary>
//        /// ����ΪԤ��ģʽ
//        /// </summary>
//        public void SetToPreviewMode()
//        {
//            //��Ԥ������Ϊ�ɼ�
//            SetLableVisible(true, lblPreview);
//            foreach (Label lbl in lblPrint)
//            {
//                lbl.BorderStyle = BorderStyle.None;
//                lbl.BackColor = SystemColors.ControlLightLight;
//            }
//        }

//        /// <summary>
//        /// ��ӡ�õı�ǩ����
//        /// </summary>
//        public Collection<Label> lblPrint;
//        /// <summary>
//        /// Ԥ���õı�ǩ����
//        /// </summary>
//        public Collection<Label> lblPreview;

//        /// <summary>
//        /// ��ʼ���վ�
//        /// </summary>
//        /// <remarks>
//        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ���������Ҫ׷����Ʊ��ʱ
//        /// </remarks>
//        private void InitReceipt(Control control)
//        {
//            foreach (Control c in control.Controls)
//            {
//                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
//                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
//                {
//                    Label l = (Label)c;
//                    if (l.Tag != null)
//                    {
//                        if (l.Tag.ToString() == "print")
//                        {
//                            #region ����ӡ�ֵĴ�ӡ��ֵ���
//                            if (!string.IsNullOrEmpty(l.Text) || l.Text == "ӡ")
//                            {
//                                l.Text = "";
//                            }
//                            #endregion
//                            lblPrint.Add(l);
//                        }
//                        else
//                        {
//                            lblPreview.Add(l);
//                        }
//                    }
//                    else
//                    {
//                        lblPreview.Add(l);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// ��Ʊֻ��ӡ��д���� ��ӡ��ʮ��
//        /// </summary>
//        /// <param name="Cash"></param>
//        /// <returns></returns>
//        public static string GetUpperCashByNumber(decimal Cash)
//        {
//            #region ��д�ܽ��
//            string returnValue = string.Empty;
//            string[] strMoney = new string[8];
//            //---------------------------|\*/|-----������㣢��û���ã�����������
//            string[] unit = { "��", "��", "��", "Ԫ", "ʰ", "��", "Ǫ", "��", "ʮ��" };
//            strMoney = GetUpperCashNumberByNumber(FS.FrameWork.Public.String.FormatNumber(Cash, 2));
//            bool isStart = false;
//            string tempDaXie = string.Empty;
//            for (int i = 0; i < strMoney.Length; i++)
//            {
//                #region �ӷ���λ��ʼ��ӡ
//                if (!isStart)
//                {
//                    if (strMoney[i] != "��")
//                    {
//                        isStart = true;
//                    }
//                    else
//                    {
//                        continue;
//                    }
//                }
//                #endregion
//                if (strMoney[i] != null)
//                {
//                    if (strMoney[i] != "��")
//                    {
//                        tempDaXie = strMoney[i] + unit[i] + tempDaXie;
//                        returnValue = tempDaXie + returnValue;
//                        tempDaXie = string.Empty;
//                    }
//                    else
//                    {
//                        tempDaXie = "��";
//                    }
//                }
//            }
//            return returnValue;
//            #endregion
//        }

//        /// <summary>
//        /// ��ʼ���վ�
//        /// </summary>
//        /// <remarks>
//        /// �Ѵ�ӡ���Ԥ������ݣ�����ǩ��ֵ���ֿ�
//        /// </remarks>
//        private void InitReceipt()
//        {
//            lblPreview = new Collection<Label>();
//            lblPrint = new Collection<Label>();
//            //foreach (Control c in this.Controls[0].Controls)
//            foreach (Control c in this.Controls)
//            {
//                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
//                    c.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuLabel")
//                {
//                    Label l = (Label)c;
//                    if (l.Tag != null)
//                    {
//                        if (l.Tag.ToString() == "print")
//                        {
//                            #region ����ӡ�ֵĴ�ӡ��ֵ���
//                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "ӡ")
//                            {
//                                l.Text = "";
//                            }
//                            #endregion
//                            lblPrint.Add(l);
//                        }
//                        else
//                        {
//                            lblPreview.Add(l);
//                        }
//                    }
//                    else
//                    {
//                        lblPreview.Add(l);
//                    }
//                }
//            }
//        }
//        /// <summary>
//        /// ���ñ�ǩ���ϵĿɼ���
//        /// </summary>
//        /// <param name="v">�Ƿ�ɼ�</param>
//        /// <param name="l">��ǩ����</param>
//        private void SetLableVisible(bool v, Collection<Label> l)
//        {
//            foreach (Label lbl in l)
//            {
//                lbl.Visible = v;                
//            }
//        }


//        /// <summary>
//        /// ���ô�ӡ���ϵ�ֵ
//        /// </summary>
//        /// <param name="t">ֵ����</param>
//        /// <param name="l">��ǩ����</param>
//        private void SetLableText(string[] t, Collection<Label> l)
//        {
//            foreach (Label lbl in l)
//            {
//                lbl.Text = "";
//            }
//            if (t != null)
//            {
//                if (t.Length <= l.Count)
//                {
//                    int i = 0;
//                    foreach (string s in t)
//                    {
//                        l[i].Text = s;
//                        i++;
//                    }
//                }
//                else
//                {
//                    if (t.Length > l.Count)
//                    {
//                        int i = 0;
//                        foreach (Label lbl in l)
//                        {
//                            lbl.Text = t[i];
//                            i++;
//                        }
//                    }
//                }
//            }
//        }
//        /// <summary>
//        /// ���ָ�����������
//        /// </summary>
//        /// <param name="n">����</param>
//        /// <returns>�������������ؼ�</returns>
//        public System.Windows.Forms.Label GetFeeNameLable(string n, Collection<Label> l)
//        {
//            foreach (Label lbl in l)
//            {
//                if (lbl.Name == n)
//                {
//                    return lbl;
//                }
//            }
//            return null;
//        }
//        /// <summary>
//        /// ���ָ�����������
//        /// </summary>
//        /// <param name="n">����</param>
//        /// <returns>�������������ؼ�</returns>
//        public System.Windows.Forms.Label GetFeeNameLable(string n, System.Windows.Forms.Control control)
//        {
//            foreach (System.Windows.Forms.Control c in control.Controls)
//            {
//                if (c.Name == n)
//                {
//                    return (System.Windows.Forms.Label)c;
//                }
//            }
//            return null;
//        }
//        /// <summary>
//        /// ��Ʊֻ��ӡ��д���� ��ӡ��ʮ��
//        /// </summary>
//        /// <param name="Cash"></param>
//        /// <returns></returns>
//        public static string[] GetUpperCashNumberByNumber(decimal Cash)
//        {
//            string[] sNumber = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
//            string[] sReturn = new string[9];
//            string strCash = null;
//            //���λ��
//            int iLen = 0;
//            strCash = FS.FrameWork.Public.String.FormatNumber(Cash, 2).ToString("############.00");
//            if (strCash.Length > 9)
//            {
//                strCash = strCash.Substring(strCash.Length - 9);
//            }

//            //���λ��
//            iLen = 9 - strCash.Length;
//            for (int j = 0; j < iLen; j++)
//            {
//                int k = 0;
//                k = 8 - j;
//                sReturn[k] = "��";
//            }
//            for (int i = 0; i < strCash.Length; i++)
//            {
//                string Temp = null;

//                Temp = strCash.Substring(strCash.Length - 1 - i, 1);

//                if (Temp == ".")
//                {
//                    continue;
//                }
//                sReturn[i] = sNumber[int.Parse(Temp)];
//            }
//            return sReturn;
//        }
//        #endregion
//        private string invoiceType;

//        public string InvoiceType
//        {
//            get { return invoiceType; }
//        }

//        private FS.HISFC.Models.Registration.Register register;
//        public FS.HISFC.Models.Registration.Register Register
//        {
//            set
//            {
//                //register = value;
//                //if (register.Pact.ID == "7")
//                //{
//                //    invoiceType = "MZ05";
//                //}
//                //else
//                //{
//                invoiceType = "MZ01";
//                //}
//            }
//        }
//    }
//}
