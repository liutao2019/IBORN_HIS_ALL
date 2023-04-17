using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucDircQuitFee<br></br>
    /// [��������: סԺֱ���˷�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDircQuitFee : ucQuitFee
    {
        /// <summary>
        /// 
        /// </summary>
        public ucDircQuitFee()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ñ���ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.FeeReport feeReportManager = new FS.HISFC.BizLogic.Fee.FeeReport();

        /// <summary>
        /// ��ǰ�˷ѷ�Ʊ
        /// </summary>
        protected ArrayList balances = new ArrayList();

        /// <summary>
        /// ԭʼ����Ʊ��
        /// </summary>
        protected string mainOldInvoiceNO = string.Empty;

        /// <summary>
        /// �����շ�
        /// </summary>
        protected HISFC.BizProcess.Integrate.Material.Material mateInteger = new FS.HISFC.BizProcess.Integrate.Material.Material();
        #endregion

        #region ˽�з���

        /// <summary>
        /// ��÷�Ʊ��Ϣ
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private int GetInvoiceNO(string inputText)
        {
            string invoiceNO = string.Empty;

            invoiceNO = inputText.PadLeft(12, '0');

            //��֤���뷢Ʊ���Ƿ���Ч: 
            ArrayList balanceTemps  = this.inpatientManager.QueryBalancesByInvoiceNO(invoiceNO);
            if (balanceTemps == null || balanceTemps.Count == 0)
            {
                this.txtInvoiceNO.SelectAll();
                MessageBox.Show(Language.Msg("��Ʊ�Ų�����,������¼��") + this.inpatientManager.Err);
                this.txtInvoiceNO.Focus();

                return -1;
            }

            //��÷�Ʊ�б�,ͨ��һ�鷢Ʊ�е�ĳһ��,���balance_no��������Ʊ;
            FS.HISFC.Models.Fee.Inpatient.Balance balance = (FS.HISFC.Models.Fee.Inpatient.Balance)balanceTemps[0];

            ////����ñʽ���Ľ������Ա��δ���սᣬ��������������Ա�ٻ�--by Maokb
            //FS.FrameWork.Models.NeuObject currOper = this.inpatientManager.Operator;
            //if (currOper.ID != balance.BalanceOper.ID)
            //{
            //    string dayBalanceDate = feeReportManager.GetMaxTimeDayReport(balance.BalanceOper.ID);
            //    if (NConvert.ToDateTime(dayBalanceDate) < balance.BalanceOper.OperTime)
            //    {
            //        MessageBox.Show(Language.Msg("�˻��ߵ�ԭ�������Ա") + "[" + balance.BalanceOper.ID +"]" + Language.Msg("��û���ᣬ����ԭ����Ա�ٻأ�"));

            //        return -1;
            //    }
            //}

            balances = this.inpatientManager.QueryBalancesByBalanceNO(balance.Patient.ID, NConvert.ToInt32(balance.ID));
            if (balances == null)
            {
                MessageBox.Show(Language.Msg("��÷�Ʊ�б����!") + this.inpatientManager.Err);

                return -1;
            }

            //�ж��Ƿ��з�Ʊ��
            if (balances.Count > 1)
            {
                DialogResult result = MessageBox.Show(Language.Msg("�ñʽ�����") + balances.Count.ToString() + Language.Msg("�ŷ�Ʊ,�˷ѻ����´�ӡδȫ�˵ķ�Ʊ,�Ƿ����?"),
                    "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    balances = new ArrayList();

                    return -1;
                }
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance obj in balances)
                {
                    if (obj.IsMainInvoice)
                    {
                        mainOldInvoiceNO = obj.Invoice.ID;
                    }
                    if (obj.FT.DerateCost > 0)
                    {
                        MessageBox.Show(Language.Msg("���ŷ�Ʊ�Ǽ��ⷢƱ�����˷�!"));

                        return -1;
                    }
                }
            }

            if (balances.Count == 1)
            {
                mainOldInvoiceNO = balance.Invoice.ID;
            }
            //ͨ��סԺ�Ż�ȡסԺ������Ϣ
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(balance.Patient.ID);
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty) 
            {
                MessageBox.Show(Language.Msg("��û��߻�����Ϣ����!") + this.radtIntegrate.Err);

                return -1;
            }
        
            //��ֵ
            this.txtInvoiceNO.Text = invoiceNO;
            this.txtInvoiceNO.Tag = balance;
            this.dtpBeginTime.Value = this.patientInfo.PVisit.InTime;
            this.dtpEndTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime();

            return 1;
        }

        #endregion

        /// <summary>
        /// �������ͷ����Ϣ
        /// </summary>
        /// <param name="balance">����ͷ��Ϣ</param>
        /// <param name="nowTime">��ǰϵͳʱ��</param>
        /// <param name="balanceNO">����¼�������</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealBalanceHead(FS.HISFC.Models.Fee.Inpatient.Balance balance, DateTime nowTime, int balanceNO, ref string errText) 
        {
            int returnValue = 0;
            
            //����ԭʼ����ͷ��ϢΪ������Ϣ
            returnValue = this.inpatientManager.UpdateBalanceHeadWasteFlag(this.patientInfo.ID, NConvert.ToInt32(balance.ID), "0", nowTime, balance.Invoice.ID);
            if (returnValue == 0)//����
            {
                errText = Language.Msg("�÷�Ʊ�Ѿ��˷�,��ˢ����Ļ!");
                
                return -1;
            }
            else if (returnValue == -1) //����
            {
                errText = Language.Msg("����ԭʼ��Ʊ����!") + this.inpatientManager.Err;

                return -1;
            }

            FS.HISFC.Models.Fee.Inpatient.Balance balanceTemp = balance.Clone();

            balanceTemp.ID = balanceNO.ToString();

            //����¼��ֵ
            balanceTemp.FT.TotCost = - balanceTemp.FT.TotCost;
            balanceTemp.FT.OwnCost = - balanceTemp.FT.OwnCost;
            balanceTemp.FT.PayCost = - balanceTemp.FT.PayCost;
            balanceTemp.FT.PubCost = - balanceTemp.FT.PubCost;
            balanceTemp.FT.RebateCost = - balanceTemp.FT.RebateCost;
            balanceTemp.FT.DerateCost = - balanceTemp.FT.DerateCost;
            balanceTemp.FT.TransferTotCost = - balanceTemp.FT.TransferTotCost;
            balanceTemp.FT.TransferPrepayCost = - balanceTemp.FT.TransferPrepayCost;
            balanceTemp.FT.PrepayCost = - balanceTemp.FT.PrepayCost;

            decimal returnCost = balanceTemp.FT.ReturnCost;
            decimal supplyCost = balanceTemp.FT.SupplyCost;

            balanceTemp.FT.SupplyCost = returnCost;
            balanceTemp.FT.ReturnCost = supplyCost;

            balanceTemp.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
            balanceTemp.BalanceOper.ID = this.inpatientManager.Operator.ID;
            balanceTemp.BalanceOper.OperTime = nowTime;
            balanceTemp.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

            //�������ͷ������Ϣ
            returnValue = this.inpatientManager.InsertBalance(this.patientInfo, balanceTemp);
            if (returnValue <= 0) 
            {
                errText = Language.Msg("�������ͷ����Ϣ����!") + this.inpatientManager.Err;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���������ϸ��
        /// </summary>
        /// <param name="balance">����ͷ��Ϣ</param>
        /// <param name="balanceNO">����¼�������</param>
        /// <param name="nowTime">��ǰʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealBalanceList(FS.HISFC.Models.Fee.Inpatient.Balance balance, string balanceNO, DateTime nowTime, ref string errText)
        {
            ArrayList balanceLists = new ArrayList();

            balanceLists = this.inpatientManager.QueryBalanceListsByInpatientNOAndBalanceNO(this.patientInfo.ID, balance.Invoice.ID, NConvert.ToInt32(balance.ID));
            if (balanceLists == null)
            {
                errText = "��ý�����ϸ����!" + this.inpatientManager.Err;

                return -1;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList in balanceLists) 
            {
                //�γɸ���¼
                balanceList.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                balanceList.BalanceBase.ID = balanceNO;
                balanceList.ID = balanceNO;
                balanceList.BalanceBase.FT.TotCost = -balanceList.BalanceBase.FT.TotCost;
                balanceList.BalanceBase.FT.OwnCost = -balanceList.BalanceBase.FT.OwnCost;
                balanceList.BalanceBase.FT.PayCost = -balanceList.BalanceBase.FT.PayCost;
                balanceList.BalanceBase.FT.PubCost = -balanceList.BalanceBase.FT.PubCost;
                balanceList.BalanceBase.FT.RebateCost = -balanceList.BalanceBase.FT.RebateCost;
                balanceList.BalanceBase.BalanceOper.ID = this.inpatientManager.Operator.ID;
                balanceList.BalanceBase.BalanceOper.OperTime = nowTime;

                if (this.inpatientManager.InsertBalanceList(this.patientInfo, balanceList) == -1) 
                {
                    errText = "���������ϸ����!" + this.inpatientManager.Err;

                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ����֧������Ϣ
        /// </summary>
        /// <param name="orgBalance">ԭʼ������Ϣ</param>
        /// <param name="balanceNO">����¼�������</param>
        /// <param name="nowTime">��ǰʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealBalancePay(FS.HISFC.Models.Fee.Inpatient.Balance orgBalance, int balanceNO, DateTime nowTime, ref string errText)
        {
            ArrayList balancePays = new ArrayList();

            balancePays = this.inpatientManager.QueryBalancePaysByInvoiceNOAndBalanceNO(orgBalance.Invoice.ID, NConvert.ToInt32(orgBalance.ID));
            if (balancePays == null) 
            {
                errText = "���֧����Ϣ����!" + this.inpatientManager.Err;

                return -1;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePays)
            {
                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                balancePay.FT.TotCost = -balancePay.FT.TotCost;
                balancePay.Qty = -balancePay.Qty;
                balancePay.BalanceOper.ID = this.inpatientManager.Operator.ID;
                balancePay.BalanceOper.OperTime = nowTime;
                balancePay.BalanceNO = balanceNO;

                if (this.inpatientManager.InsertBalancePay(balancePay) == -1)
                {
                    errText = "����֧������Ϣ����!" + this.inpatientManager.Err;

                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ȫ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        private int AllQuit() 
        {
            foreach (FarPoint.Win.Spread.SheetView sv in base.fpUnQuit.Sheets) 
            {
                base.fpUnQuit.ActiveSheet = sv;

                for (int i = 0; i < sv.RowCount; i++) 
                {   
                    sv.ActiveRowIndex = i;

                    base.ChooseUnquitItem();
                }
            }

            return 1;
        }



        /// <summary>
        /// ���ݷ�Ʊ�Ŷ�ȡ����
        /// </summary>
        /// <returns></returns>
        protected override int Retrive(bool isRetrieveReturnApply)
        {
            if (this.mainOldInvoiceNO == null || this.mainOldInvoiceNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뷢Ʊ��Ϣ"));

                return -1;
            }

            ArrayList undrugList = this.inpatientManager.QueryFeeItemListsForDirQuit(this.patientInfo.ID, this.mainOldInvoiceNO);
            if (undrugList == null)
            {
                MessageBox.Show("��÷�ҩƷ�б����!" + this.inpatientManager.Err);

                return -1;
            }

            base.SetUndrugList(undrugList);
            base.fpUnQuit_SheetUndrug.Columns[base.GetColumnIndexFromNameForfpfpUnQuitUnDrug("����ҽʦ")].Visible = false;

            ArrayList drugList = this.inpatientManager.QueryMedItemListsForDirQuit(this.patientInfo.ID, this.mainOldInvoiceNO);
            if (drugList == null)
            {
                MessageBox.Show("���ҩƷ�б����!" + this.inpatientManager.Err);

                return -1;
            }

            base.SetDrugList(drugList);
            base.fpUnQuit_SheetDrug.Columns[base.GetColumnIndexFromNameForfpfpUnQuitDrug("ִ�п���")].Visible = false;
            base.fpUnQuit_SheetDrug.Columns[base.GetColumnIndexFromNameForfpfpUnQuitDrug("����ҽʦ")].Visible = false;

            return 1;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            base.toolBarService.AddToolButton("ȫ��", "ȫ���˵����з���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);
            
            return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ȫ��") 
            {
                this.AllQuit();
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ���������ϸ�ͷ��û��ܱ�
        /// </summary>
        /// <param name="balanceNO">����¼�������</param>
        /// <param name="nowTime">��ǰʱ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int DealFeeInfoAndFeeItemList(int balanceNO, DateTime nowTime, ref string errText)
        {
            int returnValue = 0;//��������ֵ
            
            ArrayList feeItemLists = this.QueryUnQuitItems(false);
            if (feeItemLists == null) 
            {
                errText = "���δ�˷���ϸ����!";

                return -1;
            }

            ArrayList feeItemListsQuit = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeItemLists) 
            {
                //�����Ŀ��ϸ����ϸ��Ϣ
                //FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.IsPharmacy);
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemTemp = this.inpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, feeItemList.Item.ItemType);
                if (feeItemTemp == null) 
                {
                    errText = "��÷�����ϸ����!" + this.inpatientManager.Err;

                    return -1;
                }

                //if (feeItemTemp.Item.IsPharmacy)
                if (feeItemTemp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //�ж����Ϸ�ҩ����
                    if (feeItemTemp.PayType == FS.HISFC.Models.Base.PayTypes.Balanced)
                    {
                        returnValue = base.phamarcyIntegrate.CancelApplyOut(feeItemTemp.RecipeNO, feeItemTemp.SequenceNO);
                        if (returnValue == -1)
                        {
                            errText = "���Ϸ�ҩ�������!" + base.phamarcyIntegrate.Err;

                            return -1;
                        }
                    }
                }
                else
                {
                    //���²��ҷ�ҩƷ����Ӧ��������Ϣ
                    List<HISFC.Models.FeeStuff.Output> outputList = mateInteger.QueryOutput(feeItemTemp);
                    if (outputList != null)
                    {
                        foreach (HISFC.Models.FeeStuff.Output outItem in outputList)
                        {
                            //���ɿ�������
                            outItem.StoreBase.Item.Qty = outItem.StoreBase.Quantity - outItem.StoreBase.Returns - outItem.ReturnApplyNum;
                        }
                        feeItemTemp.MateList = outputList;
                    }
                }

                 //feeItemTemp.FT.TotCost = -feeItemTemp.FT.TotCost;
                 //feeItemTemp.FT.OwnCost = -feeItemTemp.FT.OwnCost;
                 //feeItemTemp.FT.PayCost = -feeItemTemp.FT.PayCost;
                 //feeItemTemp.FT.PubCost = -feeItemTemp.FT.PubCost;
                 //feeItemTemp.Item.Qty = -feeItemTemp.Item.Qty;

                 feeItemTemp.BalanceOper.ID = this.inpatientManager.Operator.ID;
                 feeItemTemp.ChargeOper.OperTime = nowTime;
                 feeItemTemp.FeeOper.ID = this.inpatientManager.Operator.ID;
                 feeItemTemp.FeeOper.OperTime = nowTime;
                 feeItemTemp.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                 feeItemTemp.BalanceNO = balanceNO;

                 feeItemListsQuit.Add(feeItemTemp);
            }

            this.feeIntegrate.IsIgnoreInstate = true;

            returnValue = base.feeIntegrate.QuitItem(this.patientInfo, ref feeItemListsQuit);

            this.feeIntegrate.IsIgnoreInstate = false;

            if (returnValue == -1) 
            {
                errText = "������ø���¼����!" + base.feeIntegrate.Err;
                this.feeIntegrate.IsIgnoreInstate = false;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���淽��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public override int Save()
        {
            string newFirBalanceNO = ""; //����¼�������
            string newSecBalanceNO = "";//����¼�������
            //string newInvoiceNO = ""; //�·�Ʊ��
            int returnValue = 0;
            string errText = string.Empty;//������Ϣ

            if (this.patientInfo == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("���߲����ڻ��߸÷�Ʊ�����˷�!"));
                
                return -1;
            }

            FS.HISFC.Models.Fee.Inpatient.Balance orgBalance = (FS.HISFC.Models.Fee.Inpatient.Balance)this.txtInvoiceNO.Tag;
            if (orgBalance == null)
            {
                MessageBox.Show(Language.Msg("�����뷢Ʊ��"));

                return -1;
            }

            ArrayList feeItemListNoBackQtyOverZero = base.QueryUnQuitItems(true);
            if (feeItemListNoBackQtyOverZero == null)
            {
                MessageBox.Show("���δ����ϸʧ��!");

                return -1;
            }

            if (feeItemListNoBackQtyOverZero.Count > 0) 
            {
                MessageBox.Show(Language.Msg("ֱ���˷ѱ���ȫ��!"));

                return -1;
            }

            //if (NConvert.ToDecimal((this.tbQuitCost.Tag.ToString())) == orgBalance.Fee.Tot_Cost)
            //{
            //    this.isAllQuit = true;
            //}
            //else
            //{
            //    this.isAllQuit = false;
            //}
            //#region �ڿ�ʼ����ǰ����֧����ʽ������t��ʼ�󵯳����� By Maokb
            //FS.HISFC.Models.Fee.Balance Main = new FS.HISFC.Models.Fee.Balance();
            //Main = (FS.HISFC.Models.Fee.Balance)this.invoiceTextBox.Tag;
            //alBalancePay = new ArrayList();
            //alBalancePay = this.myFee.GetBalancePayByInvoiceAndBalNo(Main.Invoice.ID, Main.ID);
            //if (alBalancePay == null) return -1;
            //try
            //{
            //    //�ж��Ƿ񵯳�ѡ������֧����ʽ��һ�֣��������ֽ𣬲�������
            //    int paycount = 0;
            //    foreach (FS.HISFC.Models.Fee.BalancePay pay in alBalancePay)
            //    {//��Ϊ����ʵ�����е�Ԥ����֧����ʽ����"CA"
            //        if (pay.TransKind == "1")
            //        {
            //            paycount++;
            //        }
            //        if (pay.PayType.ID != "CA")
            //        {
            //            paycount++;
            //        }
            //    }
            //    if (paycount > 1)
            //    {
            //        Fee.ucPayTypeSelect usc = new ucPayTypeSelect();
            //        usc.AlPayType = alBalancePay;
            //        FS.neuFC.Interface.Classes.Function.PopShowControl(usc);
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("�����ٻ�֧����ʽ����");
            //    return -1;
            //}
            //#endregion

            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            base.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            base.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //������¸���¼������ˮ��
            newFirBalanceNO = this.inpatientManager.GetNewBalanceNO(this.patientInfo.ID);
            if (newFirBalanceNO == null || newFirBalanceNO == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("��÷�Ʊ��ų���!") + this.inpatientManager.Err);

                return -1;
            }
            //�������¼�������,���µĸ���¼�������+1;
            newSecBalanceNO = Convert.ToString((Convert.ToInt32(newFirBalanceNO) + 1));

            //���ϵͳ��ǰʱ��
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //�������ͷ��,����ԭʼ����ͷ��¼Ϊ����,���븺��¼
            returnValue = this.DealBalanceHead(orgBalance, nowTime, NConvert.ToInt32(newFirBalanceNO), ref errText);
            if (returnValue != 1) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);

                return -1;
            }

            //���������ϸ��Ϣ,���븺��¼
            returnValue = this.DealBalanceList(orgBalance.Clone(), newFirBalanceNO, nowTime, ref errText);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);

                return -1;
            }
            
            //����֧����Ϣ
            returnValue = this.DealBalancePay(orgBalance, NConvert.ToInt32(newFirBalanceNO), nowTime, ref errText);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);

                return -1;
            }

            //���������ϸ�ͷ��û�����Ϣ
            returnValue = this.DealFeeInfoAndFeeItemList(NConvert.ToInt32(newFirBalanceNO), nowTime, ref errText);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);

                return -1;
            }

            //����סԺ������ 0 
            returnValue = this.inpatientManager.UpdateMainInfoForDirQuitFee(this.patientInfo.ID, orgBalance.FT.TotCost, NConvert.ToInt32(newFirBalanceNO), nowTime);
            if (returnValue <= 0) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����סԺ����ʧ��!" + this.inpatientManager.Err);

                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("�˷ѳɹ�!"));

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            
            this.mainOldInvoiceNO = string.Empty;
            this.txtInvoiceNO.Tag = null;
            this.txtInvoiceNO.Text = string.Empty;

            this.txtInvoiceNO.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void ucQuitFee_Load(object sender, EventArgs e)
        {
            this.ucQueryPatientInfo.InputType = 2;
            
            base.ucQuitFee_Load(sender, e);
        }

        private void txtInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.GetInvoiceNO(this.txtInvoiceNO.Text.Trim()) == -1)
                {
                    this.txtInvoiceNO.Focus();

                    return;
                }

                //��ʾ��Ա��Ϣ
                base.SetPatientInfomation();

                //�ҵ�����Ʊ��Ϣ
                FS.HISFC.Models.Fee.Inpatient.Balance mainBalance = new FS.HISFC.Models.Fee.Inpatient.Balance();
                if (balances.Count == 1)
                {
                    mainBalance = (FS.HISFC.Models.Fee.Inpatient.Balance)balances[0];
                }
                else
                {
                    FS.HISFC.Models.Fee.Inpatient.Balance bTemp = null;
                    for (int i = 0; i < balances.Count; i++)
                    {
                        bTemp = (FS.HISFC.Models.Fee.Inpatient.Balance)balances[i];
                        if (bTemp.IsMainInvoice)
                        {
                            mainBalance = bTemp;
                        }
                    }
                }

                if (mainBalance.BalanceType.ID.ToString() != "D")
                {
                    MessageBox.Show(Language.Msg("ֻ��ֱ���շѵĻ��߲���ʹ��ֱ���˷�!"));
                    Clear();

                    return;
                }

                if (mainBalance.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid) 
                {
                    MessageBox.Show(Language.Msg("���ŷ�Ʊ�Ѿ�����!"));
                    this.Clear();

                    return;
                }

                this.txtInvoiceNO.Tag = mainBalance;

                dtpBeginTime.Value = this.patientInfo.PVisit.InTime;
                dtpEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.inpatientManager.GetSysDateTime());

                this.btnRead.Focus();
            }
        }
    }
}
