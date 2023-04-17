using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using FS.FrameWork.Models;
using FS.HISFC.Components.OutpatientFee.Forms;
using FS.HISFC.Components.Common.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucInvoicePreview : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft
    {
        public ucInvoicePreview()
        {
            InitializeComponent();
        }

        #region ����

        #region �ӿڱ���

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        protected string invoiceType = "0";

        /// <summary>
        /// ��ƱԤ������
        /// </summary>
        protected string invoicePreviewType = "0";

        /// <summary>
        /// ����ĳЩ��Ϣ,�����Ƿ�Ʊʱ�򴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething InvoiceUpdated;

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register regInfo = null;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected string errText = string.Empty;

        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        protected string invoiceNO = string.Empty;

        #endregion

        #region һ�����

        /// <summary>
        /// ��Ʊ��Ϣ
        /// </summary>
        DataSet dsInvoice = new DataSet();

        //decimal CTFee = 0, MRIFee = 0, SXFee = 0, SYFee = 0, CBPha = 0, ZFPha = 0, CBItem = 0, ZFItem = 0;

        /// <summary>
        /// �Ƿ��һ�ν������
        /// </summary>
        private bool isFirst = true;

        #endregion

        #region ҵ������

        /// <summary>
        /// �������ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrText 
        {
            get 
            {
                return this.errText;
            }
            set 
            {
                this.errText = value;
            }
        }

        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        public string InvoiceNO 
        {
            get 
            {
                return this.tbRealInvoiceNO.Text.Trim();
            }
            set 
            {
                this.tbRealInvoiceNO.Text = value;
            }
        }


        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public string InvoiceType 
        {
            get 
            {
                return this.invoiceType;
            }
            set 
            {
                this.invoiceType = value;
            }
        }

        /// <summary>
        /// ��ƱԤ������
        /// </summary>
        public string InvoicePreviewType 
        {
            get 
            {
                return this.invoicePreviewType;
            }
            set 
            {
                this.invoicePreviewType = value;
            }
        }

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo 
        {
            get 
            {
                return this.regInfo;
            }
            set 
            {
                this.regInfo = value;
            }
        }

        #endregion

        #region ����

        

        #region ���з���

        /// <summary>
        /// ��÷�Ʊ��
        /// </summary>
        /// <returns>�ɹ�  ��Ʊ�� ʧ�� null</returns>
        public string GetInvoiceNO() 
        {
            return this.tbRealInvoiceNO.Text.Trim();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            //��ʼ��Ԥ����Ʊ��Ϣ

            if (this.outpatientManager.GetInvoiceClass("MZ01", ref dsInvoice) == -1)
            {
                MessageBox.Show("��÷�Ʊ��Ϣʧ��!" + outpatientManager.Err);

                return -1;
            }
            if (dsInvoice.Tables[0].PrimaryKey.Length == 0)
            {
                dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
            }

            return 1;
        }

        /// <summary>
        /// ��ʾ���ߵķ�Ʊ��Ϣ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int RefreshDisplayInfomation(ArrayList feeItemList)
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                {
                    this.fpSpread1_Sheet1.Cells[i, j].Text = "";
                }
            }
            
            decimal totCost = 0m;
            decimal totPay = 0m;
            decimal totPub = 0m;
            decimal totPubPha = 0m;
            decimal totPubZYPha = 0m;

            if (feeItemList == null)
            {
                MessageBox.Show("û�з�����ϸ!");

                return -1;
            }
            if (dsInvoice == null || dsInvoice.Tables[0].Rows.Count <= 0)
            {
                MessageBox.Show("û�з�Ʊ��Ϣ!��ά��!");

                return -1;
            }

            foreach (DataRow row in dsInvoice.Tables[0].Rows)
            {
                row["Tot_Cost"] = 0;
                row["Own_Cost"] = 0;
                row["Pay_Cost"] = 0;
                row["Pub_Cost"] = 0;
            }

            foreach (FeeItemList f in feeItemList)
            {
                DataRow rowFind = dsInvoice.Tables[0].Rows.Find(new object[] { f.Item.MinFee.ID });
                if (rowFind == null)
                {
                    MessageBox.Show("��С����Ϊ" + f.Item.MinFee.ID + "����С����û����MZ01�ķ�Ʊ������ά��");
                    return -1;
                }
                rowFind["Tot_Cost"] = NConvert.ToDecimal(rowFind["Tot_Cost"].ToString()) + f.FT.TotCost;
                rowFind["Own_Cost"] = NConvert.ToDecimal(rowFind["Own_Cost"].ToString()) + f.FT.OwnCost;
                rowFind["Pay_Cost"] = NConvert.ToDecimal(rowFind["Pay_Cost"].ToString()) + f.FT.PayCost;
                rowFind["Pub_Cost"] = NConvert.ToDecimal(rowFind["Pub_Cost"].ToString()) + f.FT.PubCost;

                if (this.regInfo.Pact.PayKind.ID == "03")
                {
                    totPub += f.FT.PubCost;
                    totPay += f.FT.PayCost;
                }
            }
            //by ţ��Ԫ

            if (feeItemList.Count > 0)
            {
                if (this.isPreFee)
                {
                    if (this.regInfo.Pact.PayKind.ID == "02")
                    {
                        //ҽ�����ߴ���Ԥ����
                        long returnValue = 0;
                        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                        returnValue = medcareInterfaceProxy.SetPactCode(this.regInfo.Pact.ID);
                        // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                        medcareInterfaceProxy.IsLocalProcess = false;

                        //medcareInterfaceProxy.SetTrans()
                        
                        returnValue = medcareInterfaceProxy.Connect();
                        if (returnValue != 1)
                        {
                             
                            medcareInterfaceProxy.Rollback();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + medcareInterfaceProxy.ErrMsg);
                            return -1;
                        }

                        returnValue = medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.regInfo, ref feeItemList);
                        if (returnValue < 0)
                        {
                            MessageBox.Show("�����ӿ�" + medcareInterfaceProxy.ErrMsg);
                            return -1;
                        }
                        returnValue = medcareInterfaceProxy.PreBalanceOutpatient(this.regInfo, ref feeItemList);
                        if (returnValue < 0)
                        {
                            MessageBox.Show("�����ӿ�" + medcareInterfaceProxy.ErrMsg);
                            return -1;
                        }
                        medcareInterfaceProxy.Rollback();
                        totPay = this.regInfo.SIMainInfo.PayCost;
                        totPub = this.regInfo.SIMainInfo.PubCost;

                    }
                }
            }

            for (int i = 1; i < 50; i++)
            {
                DataRow[] rowFind = dsInvoice.Tables[0].Select("SEQ = " + i.ToString(), "SEQ ASC");
                if (rowFind.Length == 0)
                {
                    //break;
                }
                else
                {
                    FS.HISFC.Models.Fee.Outpatient.BalanceList detail = new FS.HISFC.Models.Fee.Outpatient.BalanceList();

                    detail.InvoiceSquence = NConvert.ToInt32(rowFind[0]["SEQ"].ToString());
                    detail.FeeCodeStat.ID = rowFind[0]["FEE_STAT_CATE"].ToString();
                    detail.FeeCodeStat.Name = rowFind[0]["FEE_STAT_NAME"].ToString();

                    foreach (DataRow row in rowFind)
                    {
                        detail.BalanceBase.FT.PubCost += NConvert.ToDecimal(row["Pub_Cost"].ToString());
                        detail.BalanceBase.FT.OwnCost += NConvert.ToDecimal(row["Own_Cost"].ToString());
                        detail.BalanceBase.FT.PayCost += NConvert.ToDecimal(row["Pay_Cost"].ToString());
                        detail.BalanceBase.FT.TotCost += NConvert.ToDecimal(row["Tot_Cost"].ToString());
                    }
                    
                    if (invoicePreviewType == "1" && this.regInfo.Pact.PayKind.ID == "03" && detail.InvoiceSquence != 7 && detail.InvoiceSquence != 8 && detail.InvoiceSquence != 22 && detail.InvoiceSquence != 23)
                    {
                        totCost += detail.BalanceBase.FT.PubCost + detail.BalanceBase.FT.PayCost;
                    }
                    else
                    {
                        totCost += detail.BalanceBase.FT.TotCost;
                    }
                    if (detail.BalanceBase.FT.TotCost > 0)
                    {

                        int y = detail.InvoiceSquence / 8;
                        int x = detail.InvoiceSquence - 8 * y;
                        if (x == 0)
                        {
                            x = 8;
                        }
                        if (y == 3)
                        {
                            y = 2;
                        }
                        if (detail.InvoiceSquence == 16)
                        {
                            y = 1;
                        }
                        #region Changed by zuowy -- 2006/07/28

                        if (detail.InvoiceSquence % 8 == 0)
                        {
                            y = (detail.InvoiceSquence - 8) / 8;
                        }

                        #endregion
                        this.fpSpread1_Sheet1.Cells[x - 1, 2 * y].Text = detail.FeeCodeStat.Name;
                        if (invoicePreviewType == "1" && this.regInfo.Pact.PayKind.ID == "03" && detail.InvoiceSquence != 7 && detail.InvoiceSquence != 8 && detail.InvoiceSquence != 22 && detail.InvoiceSquence != 23)//��ɽģʽ������ʾ���ʲ���
                        {
                            this.fpSpread1_Sheet1.Cells[x - 1, 2 * y + 1].Text = FS.FrameWork.Public.String.FormatNumberReturnString((detail.BalanceBase.FT.PayCost + detail.BalanceBase.FT.PubCost), 2);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[x - 1, 2 * y + 1].Text = FS.FrameWork.Public.String.FormatNumberReturnString(detail.BalanceBase.FT.TotCost, 2);
                        }
                    }
                }
            }

            this.lblTotCost.Text = "��д�ܽ��: " + FS.FrameWork.Public.String.LowerMoneyToUpper(totCost);

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.Columns.Count; j++)
                {
                    this.fpSpread1_Sheet1.Cells[i, j].Text = "";
                }
            }

            //{3C04EA1F-0923-4d25-B59E-1D5F08441180}  ���ݰ�ȫ��У��
            if (dsInvoice != null && dsInvoice.Tables.Count > 0 && dsInvoice.Tables[0] != null)
            {
                foreach (DataRow row in dsInvoice.Tables[0].Rows)
                {
                    row["TOT_COST"] = 0;
                    row["OWN_COST"] = 0;
                    row["PAY_COST"] = 0;
                    row["PUB_COST"] = 0;
                }
            }
            //ˢ��ʱ����ȡ��Ʊ�� {2C2190F3-E9CE-4c0d-B439-AB47303C56AD}
            this.InitInvoice();
        }

        /// <summary>
        /// ��ò���Ա�ĵ�ǰ��Ʊ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int InitInvoice()
        {
            string invoiceNO = ""; string realInvoiceNO = ""; string errText = "";

            FS.HISFC.Models.Base.Employee oper = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            #region ���ӷ�Ʊ����ʾ

            int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
            //if (iReturn == -1)
            //{
            //    MessageBox.Show(errText);
            //}

            //���ʵ��Ϊ�գ���ʾ���÷�Ʊ�š���һ�ν������ ���� ��Ʊ�������� ��ʾ�ý��桿
            if (this.isValidFee && (string.IsNullOrEmpty(realInvoiceNO) || this.isFirst))
            {
                this.isFirst = false;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                frmUpdateInvoice frm = new frmUpdateInvoice();
                frm.InvoiceType = "C";
                frm.ShowDialog(this);
                iReturn = this.feeIntegrate.GetInvoiceNO(oper, "C", ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    MessageBox.Show(errText);
                    return -1;
                }
            }
            #endregion

            //��ʾ��ǰ��Ʊ��
            this.tbRealInvoiceNO.Text = realInvoiceNO;
            this.tbInvoiceNO.Text = invoiceNO;    
            //if (feeIntegrate.InvoiceMessage(oper.ID, "C", invoiceNO, 1, ref errText) < 0)
            //{
            //    MessageBox.Show(errText);
            //    return -1;
            //}
            return 0;
        }

        /// <summary>
        /// ���²���Ա��Ʊ����Ϣ
        /// {C075EA48-EFC2-4de0-A0F2-BFD3F119A85F}
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int UpdateInvoice(string invoiceNO,string realInvoiceNO,ref string errInfo)
        {
            if (string.IsNullOrEmpty(realInvoiceNO))
            {
                //MessageBox.Show("��¼����Чӡˢ��Ʊ�ţ�");
                errInfo = "��¼����Чӡˢ��Ʊ�ţ�";
                return 2;
            }

            realInvoiceNO = realInvoiceNO.PadLeft(10, '0');

            FS.HISFC.Models.Base.Employee oper = outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iRes = feeIntegrate.UpdateNextInvoiceNO(oper.ID, "C", invoiceNO,realInvoiceNO);
            if (iRes <= 0)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                //MessageBox.Show(feeIntegrate.Err);
                errInfo = feeIntegrate.Err;
                return 2;
            }
            else
            {
                //FS.FrameWork.Management.PublicTrans.Commit();
                //MessageBox.Show("���³ɹ���");
                errInfo = "���³ɹ���";
                return 1;
            }

            #region �ɵģ�����
            ////try
            ////{
            ////    int temp = Convert.ToInt32(this.tbInvoiceNO1.Text.Trim());
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show("��Ʊ������������,���Ҳ��ܳ���50���ַ�!" + ex.Message);
            ////    this.tbInvoiceNO1.Focus();

            ////    return -1;
            ////}
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            ////Transaction t = new Transaction(Connection.Instance);
            ////t.BeginTransaction();
            //this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //#region  ���·�Ʊ��,������ظ�,��ʾ
            //if (this.invoiceType == "1")
            //{
            //    string tmpCount = this.outpatientManager.QueryExistInvoiceCount(invoiceNO);
            //    if (tmpCount == "1")
            //    {
            //        DialogResult result = MessageBox.Show("�Ѿ�����Ʊ�ݺ�Ϊ: " + invoiceNO +
            //            " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //        if (result == DialogResult.No)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            this.tbRealInvoiceNO.Focus();
            //            this.tbRealInvoiceNO.SelectAll();

            //            return 2;
            //        }
            //    }
            //}
            //#endregion
            //FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
            ////try
            ////{
            ////    int temp = Convert.ToInt32(invoiceNO);
            ////}
            ////catch (Exception ex)
            ////{
            ////    FS.FrameWork.Management.PublicTrans.RollBack();
            ////    MessageBox.Show("��Ʊ������������,���Ҳ��ܳ���50���ַ�!" + ex.Message);
            ////    this.tbInvoiceNO1.Focus();
            ////    this.tbInvoiceNO1.SelectAll();

            ////    return -1;
            ////}
            //NeuObject objInvoice = this.managerIntegrate.GetConstansObj("MZINVOICE", this.outpatientManager.Operator.ID);
            //if (objInvoice == null || objInvoice.ID == null || objInvoice.ID == "")//û�м�¼
            //{
            //    con.ID = this.outpatientManager.Operator.ID;
            //    con.Name = invoiceNO;
            //    con.IsValid = true;
            //    con.OperEnvironment.ID = this.outpatientManager.Operator.ID;
            //    con.OperEnvironment.OperTime = this.outpatientManager.GetDateTimeFromSysDateTime();
            //    int iReturn = this.managerIntegrate.InsertConstant("MZINVOICE", con);
            //    if (iReturn <= 0)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show("�������Ա���Է�Ʊʧ��!" + managerIntegrate.Err);

            //        return -1;
            //    }
            //}
            //else
            //{
            //    con.ID = this.outpatientManager.Operator.ID;
            //    con.Name = invoiceNO;
            //    con.Memo = objInvoice.Memo;
            //    con.IsValid = true;
            //    con.OperEnvironment.ID = this.outpatientManager.Operator.ID;
            //    con.OperEnvironment.OperTime = this.outpatientManager.GetDateTimeFromSysDateTime();
            //    int iReturn = this.managerIntegrate.UpdateConstant("MZINVOICE", con);
            //    if (iReturn == -1)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show("���²���Ա���Է�Ʊʧ��!" + managerIntegrate.Err);

            //        return -1;
            //    }
            //}

            //FS.FrameWork.Management.PublicTrans.Commit();
            #endregion
        }

        /// <summary>
        /// ���ٶ�λ����Ʊ��
        /// </summary>
        public void SetFocus()
        {
            this.tbRealInvoiceNO.Focus();
            this.tbRealInvoiceNO.SelectAll();
        }
        /// <summary>
        /// ��Ʊ����Ч���ж�
        /// </summary>
        /// <returns></returns>
        public bool IsValid() 
        {
            string invoiceType = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
            if (invoiceType != "2")//Ĭ�Ϸ�Ʊģʽ,��Ҫtrans֧��
            {
                if (this.tbInvoiceNO.Text == string.Empty)
                {
                    this.errText = "�����÷�Ʊ��";

                    return false;
                }
            }
            return true;
        }

        #endregion
        
        #endregion

        #region �¼�

        private void tbInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string errInfo = "";
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int iReturn = this.UpdateInvoice(this.tbInvoiceNO.Text.Trim(),this.tbRealInvoiceNO.Text.Trim(), ref errInfo);

                if (iReturn != 2)
                {
                    this.InitInvoice();
                    if (InvoiceUpdated != null)
                        InvoiceUpdated();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errInfo);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���³ɹ���");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string errInfo = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            string invoiceNo = this.tbInvoiceNO.Text;
            if (string.IsNullOrEmpty(invoiceNo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��¼����Ч��Ʊ�ţ�");
                return;
            }

            int iReturn = this.UpdateInvoice(invoiceNo, this.tbRealInvoiceNO.Text.Trim(), ref errInfo);

            if (iReturn != 2)
            {
                this.InitInvoice();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errInfo);
                return;
            }


            ////���µ��Է�Ʊ�� 2011-6-24 houwb
            //iReturn = this.feeIntegrate.UpdateNextInvoliceNo(this.outpatientManager.Operator.ID, "INVOICE-C", invoiceNo);
            //if (iReturn == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(this.feeIntegrate.Err);
            //    return;
            //}

            InvoiceUpdated();

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("���³ɹ���");
        }

        #endregion

        #region IOutpatientOtherInfomationLeft ��Ա
        /// <summary>
        /// false:���� true:�շ�
        /// </summary>
        private bool isValidFee = false;
        /// <summary>
        /// false:���� true:�շ�
        /// </summary>
        public bool IsValidFee
        {
            get
            {
                return this.isValidFee;
            }
            set
            {
                this.isValidFee = value;
            }
        }


        private bool isPreFee = false;

        /// <summary>
        /// �Ƿ�Ԥ����
        /// </summary>
        public bool IsPreFee 
        {
            get 
            {
                return this.isPreFee;
            }
            set 
            {
                this.isPreFee = value;
            }
        }

        public int UpdateInvoice(string invoiceNO)
        {
            return 1;
        }

        #endregion
    }
}