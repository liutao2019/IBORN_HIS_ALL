using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.HISFC.Components.OutpatientFee.Forms
{
    /// <summary>
    /// frmInvoiceLogout<br></br>
    /// [��������: ���﷢Ʊע��]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-3-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmInvoiceLogout : frmReprint
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public frmInvoiceLogout()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���۹��ķ�Ʊ
        /// </summary>
        protected Hashtable hsInvoice = new Hashtable();

        /// <summary>
        /// �ҺŻ�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #endregion

        #region ����

        /// <summary>
        /// ���滮����Ϣ
        /// </summary>
        private void SaveCharge()
        {
            DialogResult result;

            result = MessageBox.Show("�Ƿ�ȷ��Ҫ���ۣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                return;
            }

            if (this.currentBalance != null)
            {
                if (hsInvoice.Contains(this.currentBalance))
                {
                    DialogResult r = MessageBox.Show("�÷�Ʊ������Ϣ�Ѿ����۱����,�Ƿ����»���?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }

            if (this.comFeeItemLists == null || this.comFeeItemLists.Count <= 0)
            {
                MessageBox.Show("������ϸΪ��!");

                return;
            }

            ArrayList tempFeeItemLists = new ArrayList();

            foreach (FeeItemList f in this.comFeeItemLists)
            {
                tempFeeItemLists.Add(f.Clone());
            }

            string clinicCode = ((FeeItemList)this.comFeeItemLists[0]).Patient.ID;

            FS.HISFC.Models.Registration.Register temPatientInfo = registerIntegrate.GetByClinic(clinicCode);
            if (temPatientInfo == null)
            {
                MessageBox.Show("��ùҺ���Ϣʧ��!" + this.registerIntegrate.Err);

                return;
            }
            
            temPatientInfo.Pact = this.patientInfo.Pact;

            this.patientInfo = temPatientInfo.Clone();

            foreach (FeeItemList item in tempFeeItemLists)
            {
                item.FT.TotCost = item.FT.PayCost + item.FT.OwnCost + item.FT.PubCost;

                item.FT.PayCost = 0m;
                item.FT.PubCost = 0m;
                item.FT.OwnCost = item.FT.TotCost;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                item.RecipeSequence = "";
                item.RecipeNO = "";
                item.SequenceNO = -1;
                item.Invoice.ID = "";
                item.InvoiceCombNO = null;
                item.Order.ID = "";
                item.ConfirmedQty = 0;
                item.IsConfirmed = false;
                item.PayType = FS.HISFC.Models.Base.PayTypes.Charged;
                item.NoBackQty = item.Item.Qty;
                item.ConfirmedInjectCount = 0;
                item.ConfirmOper = new FS.HISFC.Models.Base.OperEnvironment();
                
                item.ChargeOper.ID = this.outpatientManager.Operator.ID;

                item.FeeOper.OperTime = System.DateTime.MinValue;

            }

            bool iReturn = false;
            DateTime dtNow = this.outpatientManager.GetDateTimeFromSysDateTime();
            string errText = "";

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            iReturn = this.feeIntegrate.SetChargeInfo(this.patientInfo, tempFeeItemLists, dtNow, ref errText);

            if (iReturn == false)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���۳���" + errText);

                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("���۳ɹ���");

                if (this.currentBalance != null)
                {
                    Balance invo = this.currentBalance;

                    hsInvoice.Add(invo, null);
                }
            }
        }

        #endregion

        #region �¼�

        private void btnCancel_Click_1(object sender, System.EventArgs e)
        {
            DialogResult result = MessageBox.Show("�Ƿ�Ҫע����Ʊ?", "��ʾ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return;
            }
            if (this.currentBalance == null)
            {
                MessageBox.Show("�÷�Ʊ�Ѿ�����!");
                this.tbInvoiceNo.Focus();
                this.tbInvoiceNo.SelectAll();

                return;
            }
            if (currentBalance.Invoice.ID == "")
            {
                MessageBox.Show("�����뷢Ʊ��Ϣ!");
                this.tbInvoiceNo.Focus();
                this.tbInvoiceNo.SelectAll();

                return;
            }
            if (currentBalance.BalanceOper.ID != outpatientManager.Operator.ID)
            {
                MessageBox.Show("��û��ȡ�����ŷ�Ʊ��Ȩ��!" + "\n���ŷ�Ʊ�Ĳ���ԱΪ" + currentBalance.BalanceOper.ID);
                this.tbInvoiceNo.Focus();
                this.tbInvoiceNo.SelectAll();

                return;
            }
            if (currentBalance.IsDayBalanced)
            {
                MessageBox.Show("�÷�Ʊ�Ѿ��սᣬ����ע����", "��ʾ");

                return;
            }
            if (this.comFeeItemLists != null && this.comFeeItemLists.Count > 0)
            {
                foreach (FeeItemList item in comFeeItemLists)
                {
                    //if (!item.Item.IsPharmacy && item.NoBackQty != item.Item.Qty)
                    if (item.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug && item.NoBackQty != item.Item.Qty)
                    {
                        MessageBox.Show("��Ʊ�д����Ѿ��ն�ȷ�ϵ�ҽ����Ŀ,���ȷ��ע������֪ͨҽ���������˷����룡", "��ʾ");

                        return;
                    }
                    //if (item.Item.IsPharmacy && item.NoBackQty != item.Item.Qty)
                    if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && item.NoBackQty != item.Item.Qty)
                    {
                        MessageBox.Show("��Ʊ�д����Ѿ���ҩ��ҩƷ,���ȷ��ע������֪ͨҩ�����˷����룡", "��ʾ");

                        return;
                    }
                }
            }

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //��ʼ����
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int returnValue = 0;
                returnValue = this.outpatientManager.LogOutInvoice(this.currentBalance.CombNO);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ע��ʧ��!" + this.outpatientManager.Err);

                    return;
                }
                else if (returnValue == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ŷ�Ʊ�Ѿ�����!�����Ѿ��ս���!");

                    return;

                }

                returnValue = 0;
                
                ArrayList tempFeeItemLists;

                tempFeeItemLists = this.outpatientManager.QueryFeeItemListsByInvoiceNO(this.currentBalance.Invoice.ID);

                if (tempFeeItemLists == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ݴ����Ż���շ���ϸʧ�ܣ�");

                    return;
                }

                foreach (FeeItemList item in tempFeeItemLists)
                {
                    //if (!item.Item.IsPharmacy)
                    if (item.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }

                    returnValue = this.pharmacyIntegrate.CancelApplyOutClinic(item.RecipeNO, item.SequenceNO);

                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ҩƷ����ʧ�ܣ�");
                     
                        return;
                    }
                }


                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ע���ɹ�!!");

                this.Clear();
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ע��ʧ��!");

                return;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            else if (keyData == Keys.F5)
            {
                this.SaveCharge();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btCharge_Click(object sender, EventArgs e)
        {
            this.SaveCharge();
        }

        #endregion

    }
}