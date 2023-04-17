using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucQuitItemApply : ucQuitFee
    {
        public ucQuitItemApply()
        {
            InitializeComponent();
            ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
        }

        #region ����

        /// <summary>
        /// ������Ŀ��� Ĭ��Ϊȫ��
        /// </summary>
        protected ItemTypes itemType = ItemTypes.All;

        /// <summary>
        /// ���ת�ۺ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        #endregion

        /// <summary>
        /// ��������ӿ�����
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        #region ����

        /// <summary>
        /// ������Ŀ��� Ĭ��Ϊȫ��
        /// </summary>
        [Category("�ؼ�����"), Description("������Ŀ��� Ĭ��Ϊȫ��")]
        public ItemTypes ItemType 
        {
            get 
            {
                return this.itemType;
            }
            set 
            {
                this.itemType = value;

                this.SetItemType();
            }
        }

        #endregion

        #region ö��

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        public enum ItemTypes 
        {
            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug = 0,

            /// <summary>
            /// ҩƷ
            /// </summary>
            Pharmarcy,

            /// <summary>
            /// ����
            /// </summary>
            All
        }

        /// <summary>
        /// ���뻹�����״̬
        /// </summary>
        public enum ApplyTypes 
        {
            /// <summary>
            /// ����
            /// </summary>
            Apply = 0,

            /// <summary>
            /// ���
            /// </summary>
            Confirm
        }

        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// ������Ŀ���,���ÿɼ�ҳ
        /// </summary>
        protected virtual void SetItemType()
        {
            switch (this.itemType) 
            {
                case ItemTypes.All://����
                    this.fpSpread1_Sheet1.Visible = true;
                    this.fpSpread1_Sheet2.Visible = true;
                    this.fpSpread2_Sheet1.Visible = true;
                    this.fpSpread2_Sheet2.Visible = true;
                    break;
                case ItemTypes.Pharmarcy://ҩƷ
                    this.fpSpread1_Sheet1.Visible = true;
                    this.fpSpread1_Sheet2.Visible = false;
                    this.fpSpread2_Sheet1.Visible = true;
                    this.fpSpread2_Sheet2.Visible = false;
                    break;
                case ItemTypes.Undrug://ҩƷ
                    this.fpSpread1_Sheet1.Visible = false;
                    this.fpSpread1_Sheet2.Visible = true;
                    this.fpSpread2_Sheet1.Visible = false;
                    this.fpSpread2_Sheet2.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// �����Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int GetItemList()
        {
            if (this.quitInvoices == null || this.quitInvoices.Count == 0) 
            {
                return -1;
            }
            
            //��ñ����˷����з�Ʊ�ĵ�һ����Ϊ��ʱ��Ʊ��Ϣ
            Balance tempBalance = quitInvoices[0] as Balance;

            //ҩƷ�б�
            ArrayList drugItemLists = new ArrayList();

            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Pharmarcy)
            {
                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵ�ҩƷ��Ϣ
                drugItemLists = this.outpatientManager.QueryDrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (drugItemLists == null)
                {
                    MessageBox.Show("���ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            //��ҩƷ��Ϣ
            ArrayList undrugItemLists = new ArrayList();

            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Undrug)
            {
                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵķ�ҩƷ��Ϣ
                undrugItemLists = outpatientManager.QueryUndrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("��÷�ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            if (drugItemLists.Count + undrugItemLists.Count == 0)
            {
                MessageBox.Show("û�з�����Ϣ!");

                return -1;
            }

            this.invoiceFeeItemLists = outpatientManager.QueryFeeItemListsByInvoiceNO(tempBalance.Invoice.ID);

            ArrayList drugApplyedList = new ArrayList();//�Ѿ��������ҩƷ�б�
            ArrayList undrugApplyedList = new ArrayList();//�Ѿ��������ҩƷ�б�

            //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
            ArrayList drugConfirmList = new ArrayList();
           
            foreach (Balance balance in this.quitInvoices)
            {
                if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Pharmarcy)
                {
                    drugApplyedList = base.returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, false, false, "1");
                    if (drugApplyedList == null)
                    {
                        MessageBox.Show("�������ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                        return -1;
                    }
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
                    drugConfirmList = base.returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, true, false, "1");
                    if (drugConfirmList == null)
                    {
                        MessageBox.Show("���ȷ��ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                        return -1;
                    }
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��

                }
                if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Undrug)
                {
                    undrugApplyedList = base.returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, false, false, "0");
                    if (undrugApplyedList == null)
                    {
                        MessageBox.Show("��������ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                        return -1;
                    }
                }
            }

            this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;
            FeeItemList drugItemApply = null;

            for (int i = 0; i < drugItemLists.Count; i++)
            {
                drugItemApply = drugItemLists[i] as FeeItemList;

                this.fpSpread1_Sheet1.Rows[i].Tag = drugItemApply;
                //��Ϊ���ܴ���ͬһ��Ʊ�в�ͬ������ҵ����,���ҹҺ���Ϣ�еĿ�����Ϣ��һ����ʵ���շѵĿ���
                //������ͬ,��������ѹҺ�ʵ��Ŀ�����Ǹ�ֵΪ�շ���ϸʱ�Ŀ��������Ϣ.
                this.patient.DoctorInfo.Templet.Dept = drugItemApply.RecipeOper.Dept;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItemApply.Item.Name;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItemApply.Order.Combo.ID;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItemApply.Item.Specs;
                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty / drugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItemApply.Item.PriceUnit;
                //this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItemApply.FeePack == "1" ?
                //    FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty / drugItemApply.Item.PackQty, 2).ToString() :
                //    FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty, 2).ToString();

                #region �޸� mad
                //this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItemApply.FeePack == "1" ?
                //    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty / drugItemApply.Item.PackQty, 2).ToString() :
                //    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty, 2).ToString();
                if (drugItemApply.ConfirmedQty == 0)
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = "0";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItemApply.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty / drugItemApply.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty, 2).ToString();
                }

                #endregion


                if (drugItemApply.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItemApply.Order.DoseOnce.ToString() + drugItemApply.Order.DoseUnit + " " + "����:" + drugItemApply.Days.ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItemApply.Order.DoseOnce.ToString() + drugItemApply.Order.DoseUnit;
                }

                Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);

                //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                if (drugItemApply.Order.QuitFlag == 1)
                {
                    this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = SystemColors.Control;
                }
                else
                {
                    this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                }

            }

            //��ʾ��ҩƷ��Ϣ
            this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

            FeeItemList undrugItemApply = null;
            for (int i = 0; i < undrugItemLists.Count; i++)
            {
                undrugItemApply = undrugItemLists[i] as FeeItemList;
               
                this.fpSpread1_Sheet2.Rows[i].Tag = undrugItemApply;
                this.patient.DoctorInfo.Templet.Dept = undrugItemApply.RecipeOper.Dept;

                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItemApply.Item.Name;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItemApply.Order.Combo.ID;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.Item.Qty / undrugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItemApply.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.ConfirmedQty / undrugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.ConfirmedQty, 2).ToString();
            
                if (undrugItemApply.UndrugComb.ID != null && undrugItemApply.UndrugComb.ID.Length > 0)
                {
                    this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.UndrugComb.ID);
                    if (this.undrugComb == null)
                    {
                        MessageBox.Show("���������Ϣ�����޷���ʾ�����Զ����룬���ǲ�Ӱ���˷Ѳ�����");
                    }
                    else
                    {
                        undrugItemApply.UndrugComb.UserCode = this.undrugComb.UserCode;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.ID);

                    if (item == null)
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItemApply.UndrugComb.UserCode + ")" + undrugItemApply.UndrugComb.Name;
                    }
                    else
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItemApply.UndrugComb.UserCode + ")" + undrugItemApply.UndrugComb.Name + "[" + item.UserCode + "]";
                    }

                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.ID);

                    if (item != null)
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                    }
                }

                Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);

                //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                if (undrugItemApply.Order.QuitFlag == 1)
                {
                    this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = SystemColors.Control;
                }
                else
                {
                    this.fpSpread1_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                }
            }

            //��ʾ������ҩ��Ϣ    
            this.fpSpread2_Sheet1.RowCount = 0;
            //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
            //this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugApplyedList.Count;
            this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugApplyedList.Count + drugConfirmList.Count;
            FS.HISFC.Models.Fee.ReturnApply drugReturnApply = null;
            for (int i = 0; i < drugApplyedList.Count; i++)
            {
                drugReturnApply = drugApplyedList[i] as FS.HISFC.Models.Fee.ReturnApply;

                this.fpSpread2_Sheet1.Rows[i].Tag = drugReturnApply;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturnApply.Item.Name;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturnApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugReturnApply.Item.Qty / drugReturnApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugReturnApply.Item.Qty, 2).ToString();
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturnApply.Item.PriceUnit;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturnApply.Item.Specs;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "����";

                int findRow = FindItem(drugReturnApply.RecipeNO, drugReturnApply.SequenceNO, this.fpSpread1_Sheet1);
                if (findRow == -1)
                {
                    MessageBox.Show("����δ��ҩ��Ŀ����!");

                    return -1;
                }
                FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                modifyDrug.ConfirmedQty = modifyDrug.ConfirmedQty - drugReturnApply.Item.Qty;
               
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty / modifyDrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty, 2).ToString();
            }

            //��ʾ��ȷ����ҩ��Ϣ {077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
            FS.HISFC.Models.Fee.ReturnApply drugReturn = null;
            for (int i = 0; i < drugConfirmList.Count; i++)
            {

                drugReturn = drugConfirmList[i] as FS.HISFC.Models.Fee.ReturnApply;
                this.fpSpread2_Sheet1.Rows[i +  drugApplyedList.Count].Tag = drugReturn;
                this.fpSpread2_Sheet1.Cells[i +  drugApplyedList.Count, (int)DrugListQuit.ItemName].Text = drugReturn.Item.Name;
                this.fpSpread2_Sheet1.Cells[i +  drugApplyedList.Count, (int)DrugListQuit.Amount].Text = drugReturn.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty / drugReturn.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugReturn.Item.Qty, 2).ToString();
                this.fpSpread2_Sheet1.Cells[i + drugApplyedList.Count, (int)DrugListQuit.PriceUnit].Text = drugReturn.Item.PriceUnit;
                this.fpSpread2_Sheet1.Cells[i + + drugApplyedList.Count, (int)DrugListQuit.Specs].Text = drugReturn.Item.Specs;
                this.fpSpread2_Sheet1.Cells[i + drugApplyedList.Count, (int)DrugListQuit.Flag].Text = "ȷ��";

                int findRow = FindItem(drugReturn.RecipeNO, drugReturn.SequenceNO, this.fpSpread1_Sheet1);
                if (findRow == -1)
                {
                    MessageBox.Show("����δ��ҩ��Ŀ����!");

                    return -1;
                }
                FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                modifyDrug.NoBackQty = modifyDrug.NoBackQty - drugReturn.Item.Qty;
                modifyDrug.Item.Qty = modifyDrug.Item.Qty - drugReturn.Item.Qty;
                modifyDrug.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Price * modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2);
                modifyDrug.FT.OwnCost = modifyDrug.FT.TotCost;

                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = modifyDrug.FT.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = modifyDrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty / modifyDrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty / modifyDrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty, 2).ToString();
            }
            //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��

            this.fpSpread2_Sheet2.RowCount = 0;
            this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugApplyedList.Count;
            FS.HISFC.Models.Fee.ReturnApply undrugReturnApply = null;
            for (int i = 0; i < undrugApplyedList.Count; i++)
            {
                undrugReturnApply = undrugApplyedList[i] as FS.HISFC.Models.Fee.ReturnApply;
                this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturnApply;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturnApply.Item.Name;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturnApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugReturnApply.Item.Qty / undrugReturnApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugReturnApply.Item.Qty, 2).ToString();
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturnApply.Item.PriceUnit;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "����";

                int findRow = FindItem(undrugReturnApply.RecipeNO, undrugReturnApply.SequenceNO, this.fpSpread1_Sheet2);
                if (findRow == -1)
                {
                    MessageBox.Show("����δ�˷�ҩ��Ŀ����!");

                    return -1;
                }
                FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                modifyUndrug.ConfirmedQty = modifyUndrug.ConfirmedQty - undrugReturnApply.Item.Qty;

                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyUndrug.ConfirmedQty / modifyUndrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyUndrug.ConfirmedQty, 2).ToString();

            }
       
            return 1;
        }

        /// <summary>
        /// �˷��������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int QuitOperation()
        {
            #region ҩƷ
       
            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet1)//��ҩƷ
            {
                if (this.isPharmacySameRecipeQuitAll == false)
                {
                    #region MyRegion
                    int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                    if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                        //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                        if (f.Order.QuitFlag != 1)
                        {
                            string ErrInfo = "��" + f.Item.Name + "����Ӧ��ҽ��δ��ҽ����׼���������˷ѣ�";
                            FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                            return -2;
                        }

                        if (this.ckbAllQuit.Checked)
                        {
                            if (!this.isNeedAllQuit || f.Item.SysClass.ID.ToString() != "PCC")
                            {
                                if (f.ConfirmedQty <= 0)
                                {
                                    MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�����!");

                                    return -1;
                                }

                                int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                                //û���ҵ�����ô����һ��;
                                if (this.IsAllowQuitFeeHalf)
                                {
                                    findRow = -1;
                                }
                                if (findRow == -1)
                                {
                                    findRow = FindNullRow(this.fpSpread2_Sheet1);

                                    ReturnApply returnApply = new ReturnApply();

                                    returnApply.Item = f.Item.Clone();
                                    returnApply.RecipeNO = f.RecipeNO;
                                    returnApply.SequenceNO = f.SequenceNO;
                                    returnApply.FeePack = f.FeePack;
                                    returnApply.Item.Qty = f.ConfirmedQty;
                                    returnApply.Patient = f.Patient.Clone();
                                    returnApply.Days = f.Days;
                                    returnApply.ExecOper = f.ExecOper.Clone();
                                    returnApply.UndrugComb = f.UndrugComb.Clone();
                                    returnApply.ConfirmBillNO = f.Invoice.ID;

                                    this.fpSpread2_Sheet1.Rows[findRow].Tag = returnApply;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = f.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = f.Item.Specs;

                                    //���� 2012-06-27
                                    f.ConfirmedQty=f.ConfirmedQty == 0 ? f.Item.Qty : f.ConfirmedQty;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = f.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = f.Item.Price.ToString();
                                    if (ITruncFee != null)
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(f.ConfirmedQty / f.Item.PackQty * f.Item.Price)).ToString();
                                    }
                                    else
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty * f.Item.Price, 2).ToString();
                                    }

                                    


                                }
                                else //�ҵ����ۼ�����
                                {

                                    ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                                    fFind.Item.Qty = fFind.Item.Qty + f.ConfirmedQty;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.Item.Price.ToString();

                                    if (ITruncFee != null)
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Qty / f.Item.PackQty * f.Item.Price)).ToString();
                                    }
                                    else
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / f.Item.PackQty * f.Item.Price, 2).ToString();
                                    }

                                }
                                #region  �޸� mad
                                f.ConfirmedQty = f.Item.Qty - f.ConfirmedQty;

                                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                                #endregion
                            }
                            else
                            {
                                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                                {
                                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                    {
                                        FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                        if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                        {
                                            this.QuitDrugOperation(i);
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                            {
                                ArrayList alFeeItem = new ArrayList();

                                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                                {
                                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                    {
                                        FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                        if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                        {
                                            alFeeItem.Add(fTemp);
                                        }
                                    }
                                }

                                txtReturnItemName.Text = "��ҩ���";
                                txtReturnNum.Tag = alFeeItem;
                                txtRetSpecs.Text = string.Empty;
                                this.backType = "PCC";
                                txtReturnNum.Select();
                                txtReturnNum.Focus();
                            }
                            else
                            {
                                txtReturnNum.Select();
                                txtReturnNum.Focus();
                                txtReturnItemName.Text = f.Item.Name;
                                txtReturnNum.Tag = f;
                                txtRetSpecs.Text = f.Item.Specs;
                            }
                        }
                    }  
                    #endregion
                }
                else
                {
                    int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;

                    if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
                    {
                        FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                        //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                        if (f.Order.QuitFlag != 1)
                        {
                            string ErrInfo = "��" + f.Item.Name + "����Ӧ��ҽ��δ��ҽ����׼���������˷ѣ�";
                            FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                            return -2;
                        }

                        if (true)
                        {
                            if (!true )
                            {
                                if (f.ConfirmedQty <= 0)
                                {
                                    MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�����!");

                                    return -1;
                                }

                                int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                                //û���ҵ�����ô����һ��;
                                if (findRow == -1)
                                {
                                    findRow = FindNullRow(this.fpSpread2_Sheet1);

                                    ReturnApply returnApply = new ReturnApply();

                                    returnApply.Item = f.Item.Clone();
                                    returnApply.RecipeNO = f.RecipeNO;
                                    returnApply.SequenceNO = f.SequenceNO;
                                    returnApply.FeePack = f.FeePack;
                                    returnApply.Item.Qty = f.ConfirmedQty;
                                    returnApply.Patient = f.Patient.Clone();
                                    returnApply.Days = f.Days;
                                    returnApply.ExecOper = f.ExecOper.Clone();
                                    returnApply.UndrugComb = f.UndrugComb.Clone();
                                    returnApply.ConfirmBillNO = f.Invoice.ID;

                                    this.fpSpread2_Sheet1.Rows[findRow].Tag = returnApply;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = f.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = f.Item.Specs;

                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = f.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = f.Item.Price.ToString();

                                    if (ITruncFee != null)
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(f.ConfirmedQty / f.Item.PackQty * f.Item.Price)).ToString();
                                    }
                                    else
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty * f.Item.Price, 2).ToString();
                                    }

                                }
                                else //�ҵ����ۼ�����
                                {

                                    ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                                    fFind.Item.Qty = fFind.Item.Qty + f.ConfirmedQty;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.Item.Price.ToString();

                                    if (ITruncFee != null)
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(fFind.Item.Qty / f.Item.PackQty * f.Item.Price)).ToString();
                                    }
                                    else
                                    {
                                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / f.Item.PackQty * f.Item.Price, 2).ToString();
                                    }

                                }

                                f.ConfirmedQty = 0;

                                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
                            }
                            else
                            {
                                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                                {
                                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                    {
                                        FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                        if (fTemp.RecipeNO == f.RecipeNO)
                                        {
                                            this.QuitDrugOperation(i);
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (f.Item.SysClass.ID.ToString() == "PCC" && f.Order.Combo.ID.Length > 0 && this.isNeedAllQuit)
                            {
                                ArrayList alFeeItem = new ArrayList();

                                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                                {
                                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                                    {
                                        FeeItemList fTemp = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                                        if (fTemp.Item.SysClass.ID.ToString() == "PCC" && fTemp.Order.Combo.ID == f.Order.Combo.ID)
                                        {
                                            alFeeItem.Add(fTemp);
                                        }
                                    }
                                }

                                txtReturnItemName.Text = "��ҩ���";
                                txtReturnNum.Tag = alFeeItem;
                                txtRetSpecs.Text = string.Empty;
                                this.backType = "PCC";
                                txtReturnNum.Select();
                                txtReturnNum.Focus();
                            }
                            else
                            {
                                txtReturnNum.Select();
                                txtReturnNum.Focus();
                                txtReturnItemName.Text = f.Item.Name;
                                txtReturnNum.Tag = f;
                                txtRetSpecs.Text = f.Item.Specs;
                            }
                        }
                    } 
                }
            }

            #endregion

            #region ��ҩƷ

            if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)//��ҩƷ
            {
                int currRow = this.fpSpread1_Sheet2.ActiveRowIndex;

                if (this.fpSpread1_Sheet2.Rows[currRow].Tag is FeeItemList)
                {
                    FeeItemList f = this.fpSpread1_Sheet2.Rows[currRow].Tag as FeeItemList;

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                    if (f.Order.QuitFlag != 1)
                    {
                        string ErrInfo = "��" + f.Item.Name + "����Ӧ��ҽ��δ��ҽ����׼���������˷ѣ�";
                        FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                        return -2;
                    }

                    if (this.ckbAllQuit.Checked)
                    {
                        if (f.ConfirmedQty <= 0)
                        {
                            MessageBox.Show(f.Item.Name + "�Ѿ�û�п����������������˷�����!");

                            return -2;
                        }
                        int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                        //û���ҵ�����ô����һ��;
                        if (this.IsAllowQuitFeeHalf)
                        {
                            findRow = -1;
                        }
                        if (findRow == -1)
                        {
                            findRow = FindNullRow(this.fpSpread2_Sheet2);

                            ReturnApply returnApply = new ReturnApply();

                            returnApply.Item = f.Item.Clone();
                            returnApply.RecipeNO = f.RecipeNO;
                            returnApply.SequenceNO = f.SequenceNO;
                            returnApply.FeePack = f.FeePack;
                            returnApply.Item.Qty = f.ConfirmedQty;
                            returnApply.Patient = f.Patient.Clone();
                            returnApply.Days = f.Days;
                            returnApply.ExecOper = f.ExecOper.Clone();
                            returnApply.UndrugComb = f.UndrugComb.Clone();
                            returnApply.ConfirmBillNO = f.Invoice.ID;

                            this.fpSpread2_Sheet2.Rows[findRow].Tag = returnApply;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = returnApply.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = returnApply.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        else //�ҵ����ۼ�����
                        {
                            ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                            fFind.Item.Qty = fFind.Item.Qty + f.ConfirmedQty;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                            this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                        }
                        f.ConfirmedQty = 0;
                                            
                        this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = "0";
                    }
                    else
                    {
                        //������Ŀ
                        if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0 && this.isNeedAllQuit)
                        {
                            ArrayList alFeeItem = new ArrayList();

                            this.currentUndrugComb = this.undrugManager.GetValidItemByUndrugCode(f.UndrugComb.ID);
                            if (this.currentUndrugComb == null)
                            {
                                MessageBox.Show("��ø�����Ŀ����" + this.undrugManager.Err);

                                return -1;
                            }

                            this.currentUndrugCombs = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(this.currentUndrugComb.ID);

                            if (currentUndrugCombs == null)
                            {
                                MessageBox.Show("��ø�����Ŀ��ϸ����" + this.undrugPackAgeManager.Err);

                                return -1;
                            }

                            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
                            {
                                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                                {
                                    FeeItemList fTemp = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;
                                    if (fTemp.UndrugComb.ID == f.UndrugComb.ID && fTemp.Order.ID == f.Order.ID)
                                    {
                                        alFeeItem.Add(fTemp);
                                    }
                                }
                            }

                            txtReturnItemName.Text = f.UndrugComb.Name;
                            txtReturnNum.Tag = alFeeItem;
                            txtRetSpecs.Text = string.Empty;
                            this.backType = "PACKAGE";
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                        else
                        {
                            txtReturnItemName.Text = f.Item.Name;
                            txtReturnNum.Tag = f;
                            txtRetSpecs.Text = f.Item.Specs;
                            this.backType = string.Empty;
                            txtReturnNum.Select();
                            txtReturnNum.Focus();
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

        protected override int QuitDrugOperation(int currRow)
        {
            //if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
            //{
            //    FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

            //    if (this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text.Trim() == "0" || this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text.Trim() == "0.00") 
            //    {
            //        return -1;
            //    }

            //    //if (f.NoBackQty <= 0)
            //    //{
            //    //    return -2;
            //    //}
            //    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
            //    //û���ҵ�����ô����һ��;
            //    if (findRow == -1)
            //    {
            //        findRow = FindNullRow(this.fpSpread2_Sheet1);
            //        FeeItemList fClone = f.Clone();
            //        this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
            //            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
            //            FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
            //    }
            //    else //�ҵ����ۼ�����
            //    {

            //        FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
            //        fFind.Item.Qty = fFind.Item.Qty + f.Item.Qty;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
            //            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
            //            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
            //        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
            //    }
            //    f.Item.Qty = f.Item.Qty - f.NoBackQty;
            //    f.NoBackQty = 0;
            //    f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);
            //    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
            //        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
            //        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            //    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
            //    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
            //}

            if (this.fpSpread1_Sheet1.Rows[currRow].Tag is FeeItemList)
            {
                FeeItemList f = this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList;

                //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                if (f.Order.QuitFlag != 1)
                {
                    string ErrInfo = "��" + f.Item.Name + "����Ӧ��ҽ��δ��ҽ����׼���������˷ѣ�";
                    FS.FrameWork.WinForms.Classes.Function.ShowToolTip(ErrInfo, 1);
                    return -2;
                }

                if (this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text.Trim() == "0" || this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text.Trim() == "0.00")
                {
                    return -1;
                }
                int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                //û���ҵ�����ô����һ��;
                if (findRow == -1)
                {
                    findRow = FindNullRow(this.fpSpread2_Sheet1);

                    ReturnApply returnApply = new ReturnApply();

                    returnApply.Item = f.Item.Clone();
                    returnApply.RecipeNO = f.RecipeNO;
                    returnApply.SequenceNO = f.SequenceNO;
                    returnApply.FeePack = f.FeePack;
                    returnApply.Item.Qty = f.ConfirmedQty;
                    returnApply.Patient = f.Patient.Clone();
                    returnApply.Days = f.Days;
                    returnApply.ExecOper = f.ExecOper.Clone();
                    returnApply.UndrugComb = f.UndrugComb.Clone();
                    returnApply.ConfirmBillNO = f.Invoice.ID;

                    this.fpSpread2_Sheet1.Rows[findRow].Tag = returnApply;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = f.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = f.Item.Specs;

                    f.ConfirmedQty = f.ConfirmedQty == 0 ? f.Item.Qty : f.ConfirmedQty;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = f.Item.Price.ToString();

                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty * f.Item.Price, 2).ToString();

                }
                else //�ҵ����ۼ�����
                {

                    ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                    fFind.Item.Qty = fFind.Item.Qty + f.ConfirmedQty;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = fFind.Item.Price.ToString();

                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / f.Item.PackQty * f.Item.Price, 2).ToString();

                }

                f.ConfirmedQty = 0;

                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = "0";
            }
            ComputCost();

            return 1;
        }

        /// <summary>
        /// �ۺϴ����˷�����
        /// </summary>
        protected override void DealCancelQuitOperation() 
        {
            if (this.fpSpread2.ActiveSheet.RowCount == 0)
            {
                return;
            }

            int currRow = this.fpSpread2.ActiveSheet.ActiveRowIndex;

            if (this.fpSpread2.ActiveSheet.Rows[currRow].Tag is ReturnApply)
            {
               CancelQuitOperation();
            }
        }

        /// <summary>
        /// ȡ���˷�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int CancelQuitOperation()
        {
            string recipeNO = "";
			int seqNO = 0;

			if(this.fpSpread2.ActiveSheet.RowCount <= 0)
            {
   				return -1;
            }
	
			int currRow = this.fpSpread2.ActiveSheet.ActiveRowIndex;

            if (this.fpSpread2.ActiveSheet.Rows[currRow].Tag is ReturnApply)
            {
                DialogResult result = MessageBox.Show("������ҩ����,�Ƿ�ȡ������ҩ����","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return -1;
                }

                ReturnApply returnApply = this.fpSpread2.ActiveSheet.Rows[currRow].Tag as ReturnApply;
                
                //ȡ����ҩ����
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (returnApplyManager.CancelReturnApply(returnApply.ID ,this.returnApplyManager.Operator.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                this.GetItemList();
                this.txtReturnNum.Tag = null;
                this.txtReturnItemName.Text = string.Empty;
                this.txtRetSpecs.Text = string.Empty;
                this.txtUnit.Text = string.Empty;
                this.txtReturnNum.Text = string.Empty;
            }

            return 1;
        }

        /// <summary>
        /// �����˷�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int Save()
        {
            int quitCounts = 0;

            foreach (FarPoint.Win.Spread.SheetView sv in this.fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.RowCount; i++)
                {
                    if (sv.Rows[i].Tag is ReturnApply)
                    {
                        quitCounts++;
                    }
                }
            }

            if (quitCounts == 0) 
            {                
                MessageBox.Show("û��������Ŀ����!");

                return -1;
            }

            DateTime operDate = this.outpatientManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int returnValue = 0;

            foreach (FarPoint.Win.Spread.SheetView sv in this.fpSpread2.Sheets) 
            {
                for (int i = 0; i < sv.RowCount; i++) 
                {
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
                    if (sv.Rows[i].Tag is ReturnApply && sv.Cells[i, (int)DrugListQuit.Flag].Text != "ȷ��")
                    {
                        ReturnApply tempInsert = sv.Rows[i].Tag as ReturnApply;

                        ReturnApply tempExist = this.returnApplyManager.GetReturnApplyByApplySequence(tempInsert.Patient.ID, tempInsert.ID);
                        //�ҵ��Ѿ��������ݿ���˷�������Ϣ
                        if (tempExist != null)
                        {
                            //if (tempExist.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid) 
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show(tempExist.Item.Name + "�Ѿ���ȷ�ϻ�������,��ˢ��");

                            //    return -1;
                            //}
                            if (tempExist.IsConfirmed)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(tempExist.Item.Name + "�Ѿ���ȷ�ϻ�������,��ˢ��");

                                return -1;
                            }
                        }

                        returnValue = this.returnApplyManager.DeleteReturnApply(tempInsert.ID);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempExist.Item.Name + "ɾ��ʧ��!" + this.returnApplyManager.Err);

                            return -1;
                        }

                        tempInsert.ID = this.returnApplyManager.GetReturnApplySequence();
                        tempInsert.IsConfirmed = false;
                        tempInsert.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

                        returnValue = this.returnApplyManager.InsertReturnApply(tempInsert);

                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "����ʧ��!" + this.returnApplyManager.Err);

                            return -1;
                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�!");

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ������Ŀ,����TagΪReturnApply
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequence">������ˮ��</param>
        /// <param name="sv">��ǰfp</param>
        /// <returns>�ɹ� row ʧ�� 0</returns>
        protected virtual int FindReturnApplyItem(string recipeNO, int sequence, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.RowCount; i++)
            {
                if (sv.Rows[i].Tag is ReturnApply)
                {
                    ReturnApply f = sv.Rows[i].Tag as ReturnApply;
                    if (f.RecipeNO == recipeNO && f.SequenceNO == sequence)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int QuitItemByNum()
        {
            if (this.txtReturnNum.Tag == null)
            {
                MessageBox.Show("��ѡ����Ŀ!");

                return -1;
            }
            decimal quitQty = 0;
            try
            {
                quitQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtReturnNum.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������벻�Ϸ�!" + ex.Message);
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty == 0)
            {
                MessageBox.Show("��������Ϊ��");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }
            if (quitQty < 0)
            {
                MessageBox.Show("��������ΪС����");
                this.txtReturnNum.SelectAll();
                this.txtReturnNum.Focus();

                return -1;
            }

            object objQuit = this.txtReturnNum.Tag;

            #region TagΪ������Ŀʱ

            if (objQuit is FeeItemList)
            {
                FeeItemList f = objQuit as FeeItemList;
                if (f.FeePack == "1")//��װ��λ
                {
                    if (quitQty > f.ConfirmedQty / f.Item.PackQty)
                    {
                        MessageBox.Show("������������ڿ�������!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                else
                {
                    if (quitQty > f.ConfirmedQty)
                    {
                        MessageBox.Show("������������ڿ�������!");
                        this.txtReturnNum.SelectAll();
                        this.txtReturnNum.Focus();

                        return -1;
                    }
                }
                int currRow = 0;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
                    if (currRow == -1)
                    {
                        MessageBox.Show("����ҩƷʧ�ܣ�");

                        return -1;
                    }
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {
                        decimal doseOnce = (f.ConfirmedQty - quitQty) / f.Days;

                        (this.fpSpread1_Sheet1.Rows[currRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                        this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.DoseAndDays].Text = "ÿ����:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "����:" + f.Days.ToString();
                    }
                }
                else
                {
                    currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
                    if (currRow == -1)
                    {
                        MessageBox.Show("���ҷ�ҩƷʧ�ܣ�");

                        return -1;
                    }
                }

                f.ConfirmedQty = f.ConfirmedQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty);

                //if (f.Item.IsPharmacy)//ҩƷ
                if (f.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
                    if (this.IsAllowQuitFeeHalf)
                    {
                        findRow = -1;
                    }
                    //û���ҵ�����ô����һ��;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet1);

                        ReturnApply returnApply = new ReturnApply();

                        returnApply.Item = f.Item.Clone();
                        returnApply.RecipeNO = f.RecipeNO;
                        returnApply.SequenceNO = f.SequenceNO;
                        returnApply.FeePack = f.FeePack;
                        returnApply.Item.Qty = f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty;
                        returnApply.Patient = f.Patient.Clone();
                        returnApply.Days = f.Days;
                        returnApply.ExecOper = f.ExecOper.Clone();
                        returnApply.UndrugComb = f.UndrugComb.Clone();
                        returnApply.ConfirmBillNO = f.Invoice.ID;

                        this.fpSpread2_Sheet1.Rows[findRow].Tag = returnApply;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = returnApply.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = returnApply.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = f.Item.Price.ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Qty / f.Item.PackQty * f.Item.Price,2).ToString();
                    }
                    else //�ҵ����ۼ�����
                    {
                        ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Specs].Text = fFind.Item.Specs;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";

                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Price].Text = f.Item.Price.ToString();
                        this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Cost].Text = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / f.Item.PackQty * f.Item.Price, 2).ToString();
                    }

                    this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                }
                else //��ҩƷ
                {
                    int findRow = FindReturnApplyItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                    if (this.IsAllowQuitFeeHalf)
                    {
                        findRow = -1;
                    }
                    //û���ҵ�����ô����һ��;
                    if (findRow == -1)
                    {
                        findRow = FindNullRow(this.fpSpread2_Sheet2);

                        ReturnApply returnApply = new ReturnApply();

                        returnApply.Item = f.Item.Clone();
                        returnApply.RecipeNO = f.RecipeNO;
                        returnApply.SequenceNO = f.SequenceNO;
                        returnApply.FeePack = f.FeePack;
                        returnApply.Item.Qty = f.FeePack == "1" ? quitQty * f.Item.PackQty : quitQty;
                        returnApply.Patient = f.Patient.Clone();
                        returnApply.Days = f.Days;
                        returnApply.ExecOper = f.ExecOper.Clone();
                        returnApply.UndrugComb = f.UndrugComb.Clone();
                        returnApply.ConfirmBillNO = f.Invoice.ID;

                        this.fpSpread2_Sheet2.Rows[findRow].Tag = returnApply;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = f.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = f.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Qty / f.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = f.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                    }
                    else //�ҵ����ۼ�����
                    {
                        ReturnApply fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as ReturnApply;
                        fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty : quitQty);
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                            FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
                        this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
                    }

                    this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.ConfirmedQty, 2).ToString();
                }

            }

            #endregion

            #region �����Ŀ��ʱ����

            //else if (objQuit is ArrayList)
            //{
            //    ArrayList alTemp = objQuit as ArrayList;

            //    if (this.backType == "PACKAGE")
            //    {
            //        foreach (FeeItemList item in alTemp)
            //        {
            //            FS.HISFC.Models.Fee.Item.UndrugComb info = null;

            //            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in this.currentUndrugCombs)
            //            {
            //                if (undrugComb.ID == item.ID)
            //                {
            //                    info = undrugComb;

            //                    break;
            //                }
            //            }

            //            if (info == null)
            //            {
            //                MessageBox.Show("��ά����������û��" + item.Item.Name + "��ִ��ȫ��");

            //                return -1;
            //            }

            //            #region ������ϸ

            //            FeeItemList f = item;
            //            if (f.FeePack == "1")//��װ��λ
            //            {
            //                if (quitQty * info.Qty > f.NoBackQty / f.Item.PackQty)
            //                {
            //                    MessageBox.Show("������������ڿ�������!");
            //                    this.txtReturnNum.SelectAll();
            //                    this.txtReturnNum.Focus();

            //                    return -1;
            //                }
            //            }
            //            else
            //            {
            //                if (quitQty * info.Qty > f.NoBackQty)
            //                {
            //                    MessageBox.Show("������������ڿ�������!");
            //                    this.txtReturnNum.SelectAll();
            //                    this.txtReturnNum.Focus();

            //                    return -1;
            //                }
            //            }
            //            int currRow = 0;
            //            if (!f.Item.IsPharmacy)
            //            {
            //                currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet2);
            //                if (currRow == -1)
            //                {
            //                    MessageBox.Show("���ҷ�ҩƷʧ�ܣ�");

            //                    return -1;
            //                }
            //            }

            //            f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
            //            f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * info.Qty : quitQty * info.Qty);
            //            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

            //            if (!f.Item.IsPharmacy) //��ҩƷ
            //            {
            //                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
            //                //û���ҵ�����ô����һ��;
            //                if (findRow == -1)
            //                {
            //                    findRow = FindNullRow(this.fpSpread2_Sheet2);

            //                    FeeItemList fClone = f.Clone();
            //                    fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * info.Qty : quitQty * info.Qty;

            //                    this.fpSpread2_Sheet2.Rows[findRow].Tag = fClone;
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fClone.Item.Name;
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fClone.FeePack == "1" ?
            //                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
            //                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
            //                }
            //                else //�ҵ����ۼ�����
            //                {
            //                    FeeItemList fFind = this.fpSpread2_Sheet2.Rows[findRow].Tag as FeeItemList;
            //                    fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * info.Qty : quitQty * info.Qty);
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Amount].Text = fFind.FeePack == "1" ?
            //                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
            //                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.ItemName].Text = fFind.Item.Name;
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
            //                    this.fpSpread2_Sheet2.Cells[findRow, (int)UndrugListQuit.Flag].Text = "δ��׼";
            //                }

            //                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Amount].Text = f.FeePack == "1" ?
            //                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
            //                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            //                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.Cost].Text = f.FT.TotCost.ToString();
            //                this.fpSpread1_Sheet2.Cells[currRow, (int)UndrugList.NoBackQty].Text = f.FeePack == "1" ?
            //                    FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
            //                    FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
            //            }

            //            #endregion
            //        }
            //    }
            //    if (this.backType == "PCC")
            //    {
            //        foreach (FeeItemList item in alTemp)
            //        {
            //            #region ������ϸ

            //            FeeItemList f = item;
            //            if (f.FeePack == "1")//��װ��λ
            //            {
            //                if (quitQty * f.Order.DoseOnce > f.NoBackQty / f.Item.PackQty)
            //                {
            //                    MessageBox.Show("������������ڿ�������!");
            //                    this.txtReturnNum.SelectAll();
            //                    this.txtReturnNum.Focus();

            //                    return -1;
            //                }
            //            }
            //            else
            //            {
            //                if (quitQty * f.Order.DoseOnce > f.NoBackQty)
            //                {
            //                    MessageBox.Show("������������ڿ�������!");
            //                    this.txtReturnNum.SelectAll();
            //                    this.txtReturnNum.Focus();

            //                    return -1;
            //                }
            //            }
            //            int currRow = 0;
            //            if (f.Item.IsPharmacy)
            //            {
            //                currRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);
            //                if (currRow == -1)
            //                {
            //                    MessageBox.Show("����ҩƷʧ�ܣ�");

            //                    return -1;
            //                }
            //            }

            //            f.Item.Qty = f.Item.Qty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
            //            f.NoBackQty = f.NoBackQty - (f.FeePack == "1" ? quitQty * f.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce);
            //            f.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2);

            //            if (f.Item.IsPharmacy) //��ҩƷ
            //            {
            //                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet1);
            //                //û���ҵ�����ô����һ��;
            //                if (findRow == -1)
            //                {
            //                    findRow = FindNullRow(this.fpSpread2_Sheet1);

            //                    FeeItemList fClone = f.Clone();
            //                    fClone.Item.Qty = fClone.FeePack == "1" ? quitQty * fClone.Item.PackQty * f.Order.DoseOnce : quitQty * f.Order.DoseOnce;

            //                    this.fpSpread2_Sheet1.Rows[findRow].Tag = fClone;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fClone.Item.Name;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fClone.FeePack == "1" ?
            //                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty / fClone.Item.PackQty, 2).ToString() :
            //                        FS.FrameWork.Public.String.FormatNumber(fClone.Item.Qty, 2).ToString();
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fClone.Item.PriceUnit;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
            //                }
            //                else //�ҵ����ۼ�����
            //                {
            //                    FeeItemList fFind = this.fpSpread2_Sheet1.Rows[findRow].Tag as FeeItemList;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.ItemName].Text = fFind.Item.Name;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.PriceUnit].Text = fFind.Item.PriceUnit;
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Flag].Text = "δ��׼";
            //                    fFind.Item.Qty = fFind.Item.Qty + (fFind.FeePack == "1" ? quitQty * fFind.Item.PackQty * fFind.Order.DoseOnce : quitQty * fFind.Order.DoseOnce);
            //                    this.fpSpread2_Sheet1.Cells[findRow, (int)DrugListQuit.Amount].Text = fFind.FeePack == "1" ?
            //                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
            //                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
            //                }

            //                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Amount].Text = f.FeePack == "1" ?
            //                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
            //                    FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            //                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.Cost].Text = f.FT.TotCost.ToString();
            //                this.fpSpread1_Sheet1.Cells[currRow, (int)DrugList.NoBackQty].Text = f.FeePack == "1" ?
            //                    FS.FrameWork.Public.String.FormatNumber(f.NoBackQty / f.Item.PackQty, 2).ToString() :
            //                    FS.FrameWork.Public.String.FormatNumber(f.NoBackQty, 2).ToString();
            //            }

            //            #endregion
            //        }
            //    }
            //}

            #endregion

            this.fpSpread1.Select();
            this.fpSpread1.Focus();
            if (this.fpSpread1.ActiveSheet.RowCount > 0)
            {
                this.fpSpread1.ActiveSheet.ActiveRowIndex = 0;
            }

            return 1;
        }

        #endregion

        #endregion

        #region �¼�

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            base.tbQuitCost.Visible = false;
            base.tbReturnCost.Visible = false;
            base.tbQuitCash.Visible = false;
            base.lbLeftCost.Visible = false;
            base.lbQuitCash.Visible = false;
            base.lbReturnCost.Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)DrugList.Cost].Visible = false;
            this.fpSpread1_Sheet2.Columns[(int)UndrugList.Cost].Visible = false;
            this.fpSpread1_Sheet2.Columns[(int)UndrugList.CombNo].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)DrugList.CombNo].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)DrugList.Comb].Visible = false;
            this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet1.ColumnCount - 1].Visible = false;
            this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet2.ColumnCount - 1].Visible = false;
            
            this.FindForm().Text = "�˷�����";
            
            toolBarService.AddToolButton("��������", "����������Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            this.neuTabControl1.TabPages.Remove(tpFee);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Text)
            {
                case "��������":
                    this.Save();
                    break;
                case "����":
                    this.Clear();
                    break;
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void tbCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string markNO = this.tbCardNo.Text.Trim();
            if (string.IsNullOrEmpty(markNO))
            {
                MessageBox.Show("��������￨�ţ�");
                this.tbCardNo.Focus();
                return;
            }
            FS.HISFC.Models.RADT.PatientInfo p = null;
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (feeIntegrate.ValidMarkNO(markNO, ref accountCard) <= 0)
            {
                markNO = markNO.PadLeft(10, '0');
                p = radtIntegrate.QueryComPatientInfo(markNO);
            }
            else
            {
                p = accountCard.Patient;
            }
            if (p != null && !string.IsNullOrEmpty(p.PID.CardNO))
            {
                GetFeeList(p);
            }
            else
            {
                MessageBox.Show("��ѯ������Ϣʧ�ܣ�");
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }
        #endregion

        #region ֱ���շ�
        //{AB19F92E-9561-4db9-A0CF-20C1355CD5D8}
        protected override int GetFeeList(FS.HISFC.Models.RADT.PatientInfo p)
        {
            DateTime beginTime = DateTime.MinValue;
            DateTime endTime = DateTime.MinValue;
            int returnValues = FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref beginTime, ref endTime);
            if (returnValues < 0)
            {
                return -1;
            }

            this.patient.PID = p.PID;
            this.patient.Name = p.Name;
            this.patient.Pact = p.Pact;
            this.patient.Birthday = p.Birthday;
            this.patient.Sex = p.Sex;

            FT ft = new FT();
            if (GetList(p.PID.CardNO, beginTime, endTime, ref ft) < 0)
            {
                return -1;
            }
            this.tbName.Text = p.Name;
            this.tbPactName.Text = p.Pact.Name;
            this.tbPayCost.Text = ft.PayCost.ToString();
            this.tbOwnCost.Text = ft.OwnCost.ToString();
            this.tbPubCost.Text = ft.PubCost.ToString();
            this.tbTotCost.Text = ft.TotCost.ToString();
            return 1;
            
        }

        /// <summary>
        /// �����Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int GetList(string cardNO, DateTime beginDate, DateTime endDate, ref FT ft)
        {

            //ҩƷ�б�
            ArrayList drugItemLists = new ArrayList();

            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Pharmarcy)
            {
                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵ�ҩƷ��Ϣ
                drugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, true);
                if (drugItemLists == null)
                {
                    MessageBox.Show("���ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            //��ҩƷ��Ϣ
            ArrayList undrugItemLists = new ArrayList();

            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Undrug)
            {
                //ͨ����Ʊ���к�,�������Ӧ�����˷ѵķ�ҩƷ��Ϣ
                //undrugItemLists = outpatientManager.QueryUndrugFeeItemListByInvoiceSequence(tempBalance.CombNO);
                undrugItemLists = outpatientManager.GetDrugFeeByCardNODate(cardNO, beginDate, endDate, false);
                if (undrugItemLists == null)
                {
                    MessageBox.Show("��÷�ҩƷ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            if (drugItemLists.Count + undrugItemLists.Count == 0)
            {
                MessageBox.Show("û�з�����Ϣ!");

                return -1;
            }

            //this.invoiceFeeItemLists = outpatientManager.QueryFeeItemListsByInvoiceNO(tempBalance.Invoice.ID);

            ArrayList drugApplyedList = new ArrayList();//�Ѿ��������ҩƷ�б�
            ArrayList undrugApplyedList = new ArrayList();//�Ѿ��������ҩƷ�б�

            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Pharmarcy)
            {
                //drugApplyedList = base.returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, false, false, "1");
                drugApplyedList = returnApplyManager.GetApplyReturn(cardNO, false, false, true);
                if (drugApplyedList == null)
                {
                    MessageBox.Show("�������ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                    return -1;
                }
            }
            if (this.itemType == ItemTypes.All || this.itemType == ItemTypes.Undrug)
            {
                //undrugApplyedList = base.returnApplyManager.GetList(balance.Patient.ID, balance.Invoice.ID, false, false, "0");
                undrugApplyedList = returnApplyManager.GetApplyReturn(cardNO, true, false, false);
                if (undrugApplyedList == null)
                {
                    MessageBox.Show("��������ҩƷ��Ŀ�б����!" + returnApplyManager.Err);

                    return -1;
                }
            }

            this.fpSpread1_Sheet1.RowCount = drugItemLists.Count;
            FeeItemList drugItemApply = null;

            for (int i = 0; i < drugItemLists.Count; i++)
            {
                drugItemApply = drugItemLists[i] as FeeItemList;

                this.fpSpread1_Sheet1.Rows[i].Tag = drugItemApply;
                //��Ϊ���ܴ���ͬһ��Ʊ�в�ͬ������ҵ����,���ҹҺ���Ϣ�еĿ�����Ϣ��һ����ʵ���շѵĿ���
                //������ͬ,��������ѹҺ�ʵ��Ŀ�����Ǹ�ֵΪ�շ���ϸʱ�Ŀ��������Ϣ.
                this.patient.DoctorInfo.Templet.Dept = drugItemApply.RecipeOper.Dept;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.ItemName].Text = drugItemApply.Item.Name;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.CombNo].Text = drugItemApply.Order.Combo.ID;

                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Specs].Text = drugItemApply.Item.Specs;
                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text = drugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty / drugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.PriceUnit].Text = drugItemApply.Item.PriceUnit;
                this.fpSpread1_Sheet1.Cells[i, (int)DrugList.NoBackQty].Text = drugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty / drugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugItemApply.ConfirmedQty, 2).ToString();

                if (drugItemApply.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItemApply.Order.DoseOnce.ToString() + drugItemApply.Order.DoseUnit + " " + "����:" + drugItemApply.Days.ToString();
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[i, (int)DrugList.DoseAndDays].Text = "ÿ����:" + drugItemApply.Order.DoseOnce.ToString() + drugItemApply.Order.DoseUnit;
                }

                ft.TotCost += drugItemApply.FT.OwnCost + drugItemApply.FT.PubCost + drugItemApply.FT.PayCost;
                ft.OwnCost += drugItemApply.FT.OwnCost;
                ft.PubCost += drugItemApply.FT.PubCost;
                ft.PayCost += drugItemApply.FT.PayCost;

                Class.Function.DrawCombo(this.fpSpread1_Sheet1, (int)DrugList.CombNo, (int)DrugList.Comb, 0);
            }

            //��ʾ��ҩƷ��Ϣ
            this.fpSpread1_Sheet2.RowCount = undrugItemLists.Count;

            FeeItemList undrugItemApply = null;
            for (int i = 0; i < undrugItemLists.Count; i++)
            {
                undrugItemApply = undrugItemLists[i] as FeeItemList;

                this.fpSpread1_Sheet2.Rows[i].Tag = undrugItemApply;
                this.patient.DoctorInfo.Templet.Dept = undrugItemApply.RecipeOper.Dept;

                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.ItemName].Text = undrugItemApply.Item.Name;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.CombNo].Text = undrugItemApply.Order.Combo.ID;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text = undrugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.Item.Qty / undrugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.Item.Qty, 2).ToString();
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PriceUnit].Text = undrugItemApply.Item.PriceUnit;
                this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.NoBackQty].Text = undrugItemApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.ConfirmedQty / undrugItemApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugItemApply.ConfirmedQty, 2).ToString();

                if (undrugItemApply.UndrugComb.ID != null && undrugItemApply.UndrugComb.ID.Length > 0)
                {
                    this.undrugComb = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.UndrugComb.ID);
                    if (this.undrugComb == null)
                    {
                        MessageBox.Show("���������Ϣ�����޷���ʾ�����Զ����룬���ǲ�Ӱ���˷Ѳ�����");
                    }
                    else
                    {
                        undrugItemApply.UndrugComb.UserCode = this.undrugComb.UserCode;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.ID);

                    if (item == null)
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItemApply.UndrugComb.UserCode + ")" + undrugItemApply.UndrugComb.Name;
                    }
                    else
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = "(" + undrugItemApply.UndrugComb.UserCode + ")" + undrugItemApply.UndrugComb.Name + "[" + item.UserCode + "]";
                    }

                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = this.undrugManager.GetValidItemByUndrugCode(undrugItemApply.ID);

                    if (item != null)
                    {
                        this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.PackageName].Text = item.UserCode;
                    }
                }
                ft.TotCost += undrugItemApply.FT.OwnCost + undrugItemApply.FT.PubCost + undrugItemApply.FT.PayCost;
                ft.OwnCost += undrugItemApply.FT.OwnCost;
                ft.PubCost += undrugItemApply.FT.PubCost;
                ft.PayCost += undrugItemApply.FT.PayCost;
                Class.Function.DrawCombo(this.fpSpread1_Sheet2, (int)UndrugList.CombNo, (int)UndrugList.Comb, 0);
            }

            //��ʾȷ����ҩ��Ϣ
            this.fpSpread2_Sheet1.RowCount = 0;
            this.fpSpread2_Sheet1.RowCount = drugItemLists.Count + drugApplyedList.Count;
            FS.HISFC.Models.Fee.ReturnApply drugReturnApply = null;
            for (int i = 0; i < drugApplyedList.Count; i++)
            {
                drugReturnApply = drugApplyedList[i] as FS.HISFC.Models.Fee.ReturnApply;

                this.fpSpread2_Sheet1.Rows[i].Tag = drugReturnApply;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.ItemName].Text = drugReturnApply.Item.Name;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Amount].Text = drugReturnApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(drugReturnApply.Item.Qty / drugReturnApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(drugReturnApply.Item.Qty, 2).ToString();
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.PriceUnit].Text = drugReturnApply.Item.PriceUnit;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Specs].Text = drugReturnApply.Item.Specs;
                this.fpSpread2_Sheet1.Cells[i, (int)DrugListQuit.Flag].Text = "����";

                int findRow = FindItem(drugReturnApply.RecipeNO, drugReturnApply.SequenceNO, this.fpSpread1_Sheet1);
                if (findRow == -1)
                {
                    MessageBox.Show("����δ��ҩ��Ŀ����!");

                    return -1;
                }
                FeeItemList modifyDrug = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;

                modifyDrug.ConfirmedQty = modifyDrug.ConfirmedQty - drugReturnApply.Item.Qty;

                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = modifyDrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty / modifyDrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyDrug.ConfirmedQty, 2).ToString();
            }

            this.fpSpread2_Sheet2.RowCount = 0;
            this.fpSpread2_Sheet2.RowCount = undrugItemLists.Count + undrugApplyedList.Count;
            FS.HISFC.Models.Fee.ReturnApply undrugReturnApply = null;
            for (int i = 0; i < undrugApplyedList.Count; i++)
            {
                undrugReturnApply = undrugApplyedList[i] as FS.HISFC.Models.Fee.ReturnApply;
                this.fpSpread2_Sheet2.Rows[i].Tag = undrugReturnApply;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.ItemName].Text = undrugReturnApply.Item.Name;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Amount].Text = undrugReturnApply.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(undrugReturnApply.Item.Qty / undrugReturnApply.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(undrugReturnApply.Item.Qty, 2).ToString();
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.PriceUnit].Text = undrugReturnApply.Item.PriceUnit;
                this.fpSpread2_Sheet2.Cells[i, (int)UndrugListQuit.Flag].Text = "����";

                int findRow = FindItem(undrugReturnApply.RecipeNO, undrugReturnApply.SequenceNO, this.fpSpread1_Sheet2);
                if (findRow == -1)
                {
                    MessageBox.Show("����δ�˷�ҩ��Ŀ����!");

                    return -1;
                }
                FeeItemList modifyUndrug = this.fpSpread1_Sheet2.Rows[findRow].Tag as FeeItemList;

                modifyUndrug.ConfirmedQty = modifyUndrug.ConfirmedQty - undrugReturnApply.Item.Qty;

                this.fpSpread1_Sheet2.Cells[findRow, (int)UndrugList.NoBackQty].Text = modifyUndrug.FeePack == "1" ?
                    FS.FrameWork.Public.String.FormatNumber(modifyUndrug.ConfirmedQty / modifyUndrug.Item.PackQty, 2).ToString() :
                    FS.FrameWork.Public.String.FormatNumber(modifyUndrug.ConfirmedQty, 2).ToString();

            }

            return 1;
        }
        #endregion
    }
}
