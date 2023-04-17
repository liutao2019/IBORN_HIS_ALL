using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucDirectFeeInvoicePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDirectFeeInvoicePrint()
        {
            InitializeComponent();
        }

        #region ҵ���
        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// �Һ��ۺ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���ת�ۺϹ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        #endregion

        #region ����
        /// <summary>
        /// ���з�����ϸ
        /// </summary>
        ArrayList alFee = new ArrayList();
        /// <summary>
        /// ���չҺ���Ϣ�ֳ��ķ�����ϸ
        /// </summary>
        Dictionary<string, ArrayList> listFee = new Dictionary<string, ArrayList>();
        
        /// <summary>
        /// ���չҺ���Ϣ�ֳ���֧����ʽ��Ϣ
        /// </summary>
        Dictionary<string, ArrayList> listPay = new Dictionary<string, ArrayList>();
        #endregion

        #region ����

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int SetInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            SetPatientInfo(patient);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���������ȴ�.....");
            Application.DoEvents();

            #region ��ʾ������Ϣ
            alFee = this.outpatientManager.GetAccountNoPrintFeeItemList(patient.PID.CardNO, FS.HISFC.Models.Base.PayTypes.Balanced,false);
            if (alFee == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯ���߷�����ϸʧ�ܣ�" + outpatientManager.Err);
                return -1;
            }

            this.SetFeeFp();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
            return 1;
        }

 

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.txtCardNO.Text = patient.PID.CardNO;
            this.txtName.Text = patient.Name;
            this.txtSex.Text = patient.Sex.Name;
            this.txtBirthDay.Text = patient.Birthday.ToString("yyyy-MM-dd");
            this.txtAge.Text = outpatientManager.GetAge(patient.Birthday);
            
        }


        /// <summary>
        /// ��ʾ��������
        /// </summary>
        private void SetFeeFp()
        {

            string clinicCode = string.Empty;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
            {
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.SetDrugFp(f);
                }
                else
                {
                    this.SetUnDrugFp(f);
                }

                #region �����ð��չҺ���Ϣ���·�
                clinicCode = f.Patient.ID;
                if (!listFee.ContainsKey(clinicCode))
                {
                    ArrayList al = new ArrayList();
                    listFee.Add(clinicCode, al);
                }
                listFee[clinicCode].Add(f);
                #endregion
            }
        }

        /// <summary>
        /// ��ʾҩƷ������Ϣ
        /// </summary>
        /// <param name="f">ҩƷ������Ϣ</param>
        private void SetDrugFp(FeeItemList f)
        {
            int count = 0;
            count = fpFee_Drug.Rows.Count;
            this.fpFee_Drug.Rows.Add(count, 1);
            this.fpFee_Drug.Cells[count, 0].Text = f.Item.Name;
            this.fpFee_Drug.Cells[count, 1].Text = f.Item.Specs;
            this.fpFee_Drug.Cells[count, 2].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            this.fpFee_Drug.Cells[count, 3].Text = f.Item.PriceUnit;
            this.fpFee_Drug.Cells[count, 4].Text = f.Item.Price.ToString();
            this.fpFee_Drug.Cells[count, 5].Text = f.FT.OwnCost.ToString() ;
        }
        
        /// <summary>
        /// ��ʾ��ҩƷ������Ϣ
        /// </summary>
        /// <param name="f">��ҩƷ������Ϣ</param>
        private void SetUnDrugFp(FeeItemList f)
        {
            int count = 0;
            count = fpFee_Undrug.Rows.Count;
            this.fpFee_Undrug.Rows.Add(count, 1);
            this.fpFee_Undrug.Cells[count, 0].Text = f.Item.Name;
            this.fpFee_Undrug.Cells[count, 1].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            this.fpFee_Undrug.Cells[count, 2].Text = f.Item.PriceUnit;
            this.fpFee_Undrug.Cells[count, 3].Text = f.Item.Price.ToString();
            this.fpFee_Undrug.Cells[count, 4].Text = f.FT.OwnCost.ToString();
        }

        private int MakeInvoice()
        {
            if (alFee == null || alFee.Count == 0)
            {
                MessageBox.Show("�û��߲����ڷ��ã�");
                return -1;
            }
            listPay.Clear();
            ArrayList al = null;

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.outpatientManager.Operator.ID);
            if (employee == null)
            {
                MessageBox.Show("��ȡ��Ա��Ϣʧ�ܣ�" + managerIntegrate.Err);
                return -1;
            }

            string errText = string.Empty;

            #region ��ȡ��Ʊ��
            string invoiceNO = string.Empty, realInvoiceNO = string.Empty;
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
           
            //��ñ����շ���ʼ��Ʊ��
            int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C",ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return -1;
            }

            FS.FrameWork.Management.PublicTrans.RollBack();
            #endregion

            Dictionary<HISFC.Models.Registration.Register, ArrayList> listInvoice = new Dictionary<FS.HISFC.Models.Registration.Register, ArrayList>();
            ArrayList balance = new ArrayList();
            FS.HISFC.Models.Registration.Register r = null;
            //����ÿ�ιҺ������ɵķ������ɷ�Ʊ
            foreach (string key in listFee.Keys)
            {
                al = listFee[key];
                r = registerIntegrate.GetByClinic(key);
                if (r == null)
                {
                    MessageBox.Show("��ѯ���߹Һ���Ϣʧ�ܣ�");
                    return -1;
                }
                balance = Class.Function.MakeInvoice(this.feeIntegrate, r, al, invoiceNO, realInvoiceNO, ref errText);
                if (balance == null)
                {
                    return -1;
                }
                listInvoice.Add(r, balance);
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string operID = outpatientManager.Operator.ID;
            DateTime feeTime = outpatientManager.GetDateTimeFromSysDateTime();
            try
            {
                FS.HISFC.Models.Registration.Register register = null;
                ArrayList balancesList = null;
                ArrayList invoices = null;
                ArrayList invoiceDetail = null;
                ArrayList feeList = null;
                IDictionaryEnumerator ide = listInvoice.GetEnumerator();

                while (ide.MoveNext())
                {
                    register = ide.Key as FS.HISFC.Models.Registration.Register;
                    balancesList = ide.Value as ArrayList;

                    invoices = balancesList[0] as ArrayList;
                    invoiceDetail = balancesList[1] as ArrayList;
                    feeList = balancesList[2] as ArrayList;
                    //���뷢Ʊ��Ϣ
                    if (this.InsertInvoices(invoices, register, feeTime, operID, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                    //���뷢Ʊ��ϸ
                    if (this.InsertInvoiceDetails(invoiceDetail, feeTime, operID, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                    //����֧����ʽ��Ϣ
                    //if (this.InsertInvocePayMode(invoices, feeTime, operID, ref errText) < 0)
                    //{
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    MessageBox.Show(errText);
                    //    return -1;
                    //}
                    feeList = listFee[register.ID];
                    //���·�����ϸ
                    if (this.UpdateFeeItemList(feeList, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                    }
                }


                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
                // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                // 2011-08-04
                // �˴�������ʾ
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //}
                ide.Reset();

                while (ide.MoveNext())
                {
                    register = ide.Key as HISFC.Models.Registration.Register;
                    balancesList = ide.Value as ArrayList;
                    invoices = balancesList[0] as ArrayList;
                    ArrayList tempal = new ArrayList();
                    foreach (ArrayList obj in balancesList[1] as ArrayList)
                    {
                        tempal.Add(obj[0]);
                    }
                    invoiceDetail = new ArrayList();
                    invoiceDetail.Add(tempal);
                    feeList = listFee[register.ID];
                    ArrayList payModList = new ArrayList();
                    this.feeIntegrate.PrintInvoice(invoicePrintDll, register, invoices, invoiceDetail, feeList, payModList, false, ref errText);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.Clear();
            }
            catch(Exception ex) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��ӡ��Ʊʧ�ܣ�" + ex.Message);
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// ���뷢Ʊ��ϸ
        /// </summary>
        /// <param name="invoiceDetails"></param>
        /// <param name="feeTime"></param>
        /// <param name="operID"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int InsertInvoiceDetails(ArrayList invoiceDetails,DateTime feeTime,string operID,ref string errText)
        {
            int iReturn = 0;
            foreach (ArrayList tempsInvoices in invoiceDetails)
            {
                foreach (ArrayList tempDetals in tempsInvoices)
                {
                    foreach (BalanceList balanceList in tempDetals)
                    {
                        balanceList.BalanceBase.BalanceOper.ID = operID;
                        balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                        balanceList.BalanceBase.IsDayBalanced = false;
                        balanceList.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                        //balanceList.ID = balanceList.ID.PadLeft(12, '0');

                        iReturn = outpatientManager.InsertBalanceList(balanceList);
                        if (iReturn == -1)
                        {
                            errText = "���뷢Ʊ��ϸ����!" + outpatientManager.Err;
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// ���뷢Ʊ����
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        private int InsertInvoices(ArrayList invoices,FS.HISFC.Models.Registration.Register r,DateTime feeTime,string operID, ref string errText)
        {
            int iReturn = 0;
            foreach (Balance balance in invoices)
            {
                balance.BalanceOper.ID = operID;
                balance.BalanceOper.OperTime = feeTime;
                balance.Patient.Pact = r.Pact;
                //����־
                string tempExamineFlag = null;
                //�������־ 0 ��ͨ���� 1 ������� 2 �������
                //���û�и�ֵ,Ĭ��Ϊ��ͨ����

                balance.ExamineFlag = "0";
                balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                balance.IsAuditing = false;
                balance.IsDayBalanced = false;
                //balance.ID = balance.ID.PadLeft(12, '0');
                balance.IsAccount = true;
                //���뷢Ʊ����fin_opb_invoice
                iReturn = this.outpatientManager.InsertBalance(balance);
                if (iReturn == -1)
                {
                    errText = "�����������!" + outpatientManager.Err;

                    return -1;
                }
                iReturn = this.feeIntegrate.UpdateInvoiceNO(balance.Invoice.ID, balance.PrintedInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    return -1;
                }
            }            

            return 1;
        }

        /// <summary>
        /// ����֧����ϸ��
        /// </summary>
        /// <param name="invoices"></param>
        /// <param name="feeTime">�շ�ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        private int InsertInvocePayMode(ArrayList invoices,DateTime feeTime,string operID,ref string errText)
        {
            foreach (Balance b in invoices)
            {
                if (b.FT.OwnCost > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalancePay payMod = new BalancePay();
                    payMod.Invoice = b.Invoice;
                    
                    payMod.PayType.ID = "CA";
                    payMod.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    payMod.Squence = "1";
                    payMod.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    payMod.IsDayBalanced = false;
                    payMod.IsAuditing = false;
                    payMod.IsChecked = false;
                    payMod.InputOper.ID = operID;
                    payMod.InputOper.OperTime = feeTime;
                    payMod.FT.RealCost = b.FT.OwnCost;
                    payMod.FT.ReturnCost = 0;
                    payMod.FT.TotCost = b.FT.OwnCost;
                    payMod.InvoiceCombNO = b.CombNO;
                    if (outpatientManager.InsertBalancePay(payMod) < 0)
                    {
                        errText = "����֧����ʽ��Ϣʧ�ܣ�" + outpatientManager.Err;
                        return -1;
                    }
                    if (!listPay.ContainsKey(b.Patient.ID))
                    {
                        ArrayList al = new ArrayList();
                        listPay.Add(b.Patient.ID, al);
                    }
                    listPay[b.Patient.ID].Add(payMod);
                }
            }
            return 1;
        }

        /// <summary>
        /// ���·�����ϸ��Ʊ��Ϣ
        /// </summary>
        /// <param name="feeList"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int UpdateFeeItemList(ArrayList feeList,ref string errText)
        {
            int resultValue = 0;
            foreach (FeeItemList f in feeList)
            {
                resultValue = outpatientManager.UpdateFeeItemListInvoiceInfo(f);
                if (resultValue < 0)
                {
                    errText = "���·�����ϸʧ�ܣ�" + outpatientManager.Err;
                    return -1;
                }
                if (resultValue == 0)
                {
                    errText = "���ݷ����仯��ˢ�£�";
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.txtCardNO.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtBirthDay.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtCost.Text = string.Empty;
            int count = fpFee_Drug.Rows.Count;
            if (count > 0)
            {
                this.fpFee_Drug.Rows.Remove(0, count);
            }
            count = fpFee_Undrug.Rows.Count;
            if (count > 0)
            {
                this.fpFee_Undrug.Rows.Remove(0, count);
            }
            this.txtCardNO.Focus();
            alFee.Clear();
            listPay.Clear();
            listFee.Clear();
            return;
        }
        #endregion

        #region �¼�
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string cardNO = this.txtCardNO.Text.Trim();
            this.Clear();
            if (string.IsNullOrEmpty(cardNO))
            {
                MessageBox.Show("��������￨�ţ�");
                this.txtCardNO.Focus();
                return;
            }
            cardNO = cardNO.PadLeft(10, '0');
            FS.HISFC.Models.RADT.PatientInfo p = radtIntegrate.QueryComPatientInfo(cardNO);
            if (p == null || string.IsNullOrEmpty(p.PID.CardNO))
            {
                MessageBox.Show("��ѯ������Ϣʧ�ܣ�" + radtIntegrate.Err);
                this.txtCardNO.Focus();
                this.txtCardNO.SelectAll();
                return;
            }
            this.SetInfo(p);
            
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            MakeInvoice();
            return base.OnPrint(sender, neuObject);
        }

        private void ucAccountInvoicePrint_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtCardNO;
        }
        #endregion
    }
}
