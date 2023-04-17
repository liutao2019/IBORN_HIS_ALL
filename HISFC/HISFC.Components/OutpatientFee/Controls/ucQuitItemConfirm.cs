using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucQuitItemConfirm : ucQuitItemApply, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucQuitItemConfirm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// �˷ѵ���ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint IBackFeePrint = null;
        /// <summary>
        /// �Ƿ�����ȡ����ҩȷ��
        /// </summary>
        bool isUseCancelQuitConfirm = true;
        [Category("����"), Description("�Ƿ�����ȡ����ҩȷ��")]
        public bool IsUseCancelQuitConfirm
        {
            set
            {
                this.isUseCancelQuitConfirm = value;
            }
            get
            {
                return this.isUseCancelQuitConfirm;
            }
        }

        bool isPrintQuitDrug = true;
        [Category("�ؼ�����"), Description("�Ƿ��ӡ��ҩ���뵥")]
        public bool IsPrintQuitDrug
        {
            set
            {
                this.isPrintQuitDrug = value;
            }
            get
            {
                return this.isPrintQuitDrug;
            }
        }

        /// <summary>
        /// �Ƿ�������ҩ����
        /// </summary>
        bool isLimitBackPha = false;
        [Category("�ؼ�����"), Description("�Ƿ�������ҩ����")]
        public bool IsLimitBackPha
        {
            set
            {
                this.isLimitBackPha = value;
            }
            get
            {
                return this.isLimitBackPha;
            }
        }

        /// <summary>
        /// ������ҩ��ʱ����
        /// </summary>
        int isLimitDays = 3;
        [Category("����"), Description("���Ƶ���ҩ����")]
        public int IsLimitDays
        {
            set
            {
                this.isLimitDays = value;
            }
            get
            {
                return this.isLimitDays;
            }
        }


        #region ����

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <returns></returns>
        protected override int Save()
        {
            int infoCounts = 0;

            foreach (FarPoint.Win.Spread.SheetView sv in this.fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.RowCount; i++)
                {
                    if (sv.Rows[i].Tag is ReturnApply)
                    {
                        if (sv.Cells[i, (int)DrugListQuit.Flag].Text != "ȷ��")// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
                        {
                            infoCounts++;
                        }
                    }
                }
            }

            if (infoCounts == 0) 
            {
                MessageBox.Show("û����Ҫ��˵ķ���!");

                return -1;
            }

            if (MessageBox.Show("�Ƿ�ȷ����ҩ��", "ѯ��", MessageBoxButtons.YesNo) == DialogResult.No)// {47F1129F-CC17-41fa-9F1A-6D4E9085FD2E}
            {
                return -1;
            }

            DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int returnValue = 0;
            ArrayList alBackFeeList = new ArrayList();

            foreach (FarPoint.Win.Spread.SheetView sv in this.fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.RowCount; i++)
                {
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
                    if (sv.Rows[i].Tag is ReturnApply && sv.Cells[i, (int)DrugListQuit.Flag].Text != "ȷ��")
                    {
                        ReturnApply tempInsert = sv.Rows[i].Tag as ReturnApply;

                        FS.HISFC.Models.Pharmacy.ApplyOut applyInfoTmp = pharmacyIntegrate.GetApplyOut(tempInsert.RecipeNO, tempInsert.SequenceNO);
                        if (applyInfoTmp != null && !string.IsNullOrEmpty(applyInfoTmp.ID))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "��δ��ҩȷ�ϵ���Ŀ����������ҩ���棬лл��");
                            return -1;
                        }

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
                        tempInsert.IsConfirmed = true;
                        tempInsert.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

                        returnValue = this.returnApplyManager.InsertReturnApply(tempInsert);

                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "���ʧ��!" + this.returnApplyManager.Err);

                            return -1;
                        }

                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(tempInsert.RecipeNO, tempInsert.SequenceNO);
                        if(feeItemList == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "�����Ŀʧ��!" + this.outpatientManager.Err);

                            return -1;
                        }

                        if (feeItemList.Item.Qty < feeItemList.NoBackQty + tempInsert.Item.Qty) 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�����Ĳ���Ա�����Ѿ������" + feeItemList.Item.Name + "��ˢ��!");

                            return -1;
                        }

                        if (this.isLimitBackPha)
                        {
                            DateTime nowTimeTemp = this.returnApplyManager.GetDateTimeFromSysDateTime();
                            if((nowTime - feeItemList.FeeOper.OperTime).TotalDays >= this.isLimitDays)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�����Ĳ���Ա�����Ѿ������" + feeItemList.Item.Name + "��ˢ��!");
                                return -1;
                            }
                        }
                       
                        //���¿���������ȷ������
                        returnValue = this.outpatientManager.UpdateConfirmFlag(tempInsert.RecipeNO, tempInsert.SequenceNO, "1", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty + tempInsert.Item.Qty,
                            feeItemList.ConfirmedQty - tempInsert.Item.Qty);
                        if (returnValue <= 0) 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("������Ŀ:" + feeItemList.Item.Name + "ʧ��!" + this.outpatientManager.Err);

                            return -1;
                        }

                        //if (tempInsert.Item.IsPharmacy) 
                        if (tempInsert.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) 
                        {
                            feeItemList.Item.Qty = tempInsert.Item.Qty;
                            
                            returnValue = this.pharmacyIntegrate.OutputReturn(feeItemList, this.outpatientManager.Operator.ID, nowTime);
                            if (returnValue < 0) 
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ҩƷ�˿�ʧ��!" + this.pharmacyIntegrate.Err);

                                return -1;
                            }

                            alBackFeeList.Add(feeItemList);

                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("��˳ɹ�!");


            base.GetItemList();

            if (alBackFeeList.Count > 0)
            {
                //����ӡ��ֱ�ӷ���
                if (!this.isPrintQuitDrug) return 1;

                if (this.IBackFeePrint == null)
                {
                    this.IBackFeePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint;
                }

                if (this.IBackFeePrint != null)
                {
                    this.IBackFeePrint.Patient = this.patient;

                    this.IBackFeePrint.SetData(alBackFeeList);

                    this.IBackFeePrint.Print();
                }
            }
            return 1;
        }

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
            this.fpSpread1_Sheet1.Columns[this.fpSpread1_Sheet1.ColumnCount - 1].Visible = false;
            this.fpSpread1_Sheet2.Columns[this.fpSpread1_Sheet2.ColumnCount - 1].Visible = false;
            
            this.FindForm().Text = "�˷����";

            toolBarService.AddToolButton("�˷����", "���������Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ˢ��", "����ˢ����Ŀ���˷�������Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("���", "���¼����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ȫ��", "ȫ���˳����з���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�˷����":
                    this.Save();
                    break;
                case "ˢ��":
                    base.GetItemList();
                    break;
                case "���":
                    base.Clear();
                    break;
                case "ȫ��":
                    base.AllQuit();
                    break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
            /// <summary>
        /// ����ȡ���˷Ѳ���
        /// </summary>
        protected virtual  void DealCancelQuitOperation(bool isAllowQuitFeeHalf)
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)//ҩƷ 
            {
                int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                if (fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text == "ȷ��")
                {

                    //ȡ��ȷ��

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
                    {
                        return;
                    }

                    if (!(this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply))
                    {
                        MessageBox.Show("û�к�׼ҩƷ����ȡ��!");

                        return;
                    }

                    #region ȡ��ȷ��ǰ������ʾ��

                    if (this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text == "ȷ��")//�÷����������⣬��ȡ����������ӣ���ʱ���� {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
                    {
                        MessageBox.Show("�Ѿ�ȷ����ҩ���������·�ҩ���뵽��ҩ���ڷ�ҩ!");
                        return;
                    }
                    DialogResult dr = MessageBox.Show("�Ƿ�ȷ��ȡ��" + (this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.ReturnApply).Item.Name + "����ҩ���룿", "��ʾ", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                   
                    #endregion

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    try
                    {
                        if (isAllowQuitFeeHalf == false)
                        {
                            if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                            {
                                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;


                                this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                                //ɾ��met_nui_cancelitem���е�������Ϣ
                                ReturnApply returnApply = this.fpSpread2_Sheet1.Rows[currRow].Tag as ReturnApply;

                                if (this.returnApplyManager.DeleteReturnApply(returnApply.ID) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();

                                    return;
                                }

                                //�ָ�fin_opb_feedetail�еĿ�����������������

                                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(f.RecipeNO, f.SequenceNO);

                                if (feeItemList == null)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(feeItemList.Item.Name + "�����Ŀʧ��!" + this.outpatientManager.Err);

                                    return;
                                }

                                //if (feeItemList.Item.Qty < feeItemList.NoBackQty + f.Item.Qty)
                                //{
                                //    FS.FrameWork.Management.PublicTrans.RollBack();
                                //    MessageBox.Show("�����Ĳ���Ա�����Ѿ������" + feeItemList.Item.Name + "��ˢ��!");

                                //    return -1;
                                //}

                                //���¿���������ȷ������
                                int returnValue = this.outpatientManager.UpdateConfirmFlag(f.RecipeNO, f.SequenceNO, "0", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty - f.Item.Qty,
                                       feeItemList.ConfirmedQty);

                                if (returnValue <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("������Ŀ:" + feeItemList.Item.Name + "ʧ��!" + this.outpatientManager.Err);

                                    return;
                                }

                                //��ҩƷ����
                                ArrayList al = new ArrayList();
                                al.Add(feeItemList);
                                // public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, DateTime operDate, bool isModify,out string drugSendInfo)
                                string drugSendInfo = string.Empty;
                                pharmacyIntegrate.ApplyOut(this.patient, al, "", this.outpatientManager.GetDateTimeFromSysDateTime(), true, out drugSendInfo);

                                //������ʾ


                                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                                if (findRow == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("����δ��ҩƷʧ��!");

                                    return;
                                }
                                FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                                fFind.Item.Qty += f.Item.Qty;

                                fFind.ConfirmedQty = fFind.Item.Qty;

                                fFind.NoBackQty += f.Item.Qty;
                                fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                                fFind.FT.OwnCost = fFind.FT.TotCost;
                                //fFind.FT.TotCost += f.FT.TotCost;
                                //fFind.FT.PubCost += f.FT.PubCost;
                                //fFind.FT.PayCost += f.FT.PayCost;
                                //fFind.FT.OwnCost += f.FT.OwnCost;

                                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                    FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                                //this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                                //    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                                //    FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                                f.Item.Qty = 0;
                                if (f.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    decimal doseOnce = (fFind.NoBackQty) / fFind.Days;

                                    (this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "ÿ����:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "����:" + f.Days.ToString();
                                }
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = "0";
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                                this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                                //{433AA56A-264F-4c8c-BC7E-52DAEAFDC605}
                                this.fpSpread2_Sheet1.Rows[currRow].Tag = null;
                            }
                        }
                        else
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList fSelect = this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                            for (int i = 0; i < this.fpSpread2_Sheet1.RowCount; i++)
                            {
                                currRow = i;

                                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                                if (this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.Inpatient.FeeItemList
                                    && (this.IsPharmacySameRecipeQuitAll == false ||
                                            (this.IsPharmacySameRecipeQuitAll == true && f.RecipeNO == fSelect.RecipeNO)
                                          )
                                    )
                                {


                                    this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    this.pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                                    //ɾ��met_nui_cancelitem���е�������Ϣ
                                    ReturnApply returnApply = this.fpSpread2_Sheet1.Rows[currRow].Tag as ReturnApply;

                                    if (this.returnApplyManager.DeleteReturnApply(returnApply.ID) < 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();

                                        return;
                                    }

                                    //�ָ�fin_opb_feedetail�еĿ�����������������

                                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(f.RecipeNO, f.SequenceNO);

                                    if (feeItemList == null)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show(feeItemList.Item.Name + "�����Ŀʧ��!" + this.outpatientManager.Err);

                                        return;
                                    }

                                    //if (feeItemList.Item.Qty < feeItemList.NoBackQty + f.Item.Qty)
                                    //{
                                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                                    //    MessageBox.Show("�����Ĳ���Ա�����Ѿ������" + feeItemList.Item.Name + "��ˢ��!");

                                    //    return -1;
                                    //}

                                    //���¿���������ȷ������
                                    feeItemList.ConfirmedQty += f.Item.Qty;
                                    int returnValue = this.outpatientManager.UpdateConfirmFlag(f.RecipeNO, f.SequenceNO, "0", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty - f.Item.Qty,
                                           feeItemList.ConfirmedQty);

                                    if (returnValue <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("������Ŀ:" + feeItemList.Item.Name + "ʧ��!" + this.outpatientManager.Err);

                                        return;
                                    }

                                    //��ҩƷ����
                                    ArrayList al = new ArrayList();
                                    al.Add(feeItemList);
                                    // public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, DateTime operDate, bool isModify,out string drugSendInfo)
                                    string drugSendInfo = string.Empty;
                                    pharmacyIntegrate.ApplyOut(this.patient, al, "", this.outpatientManager.GetDateTimeFromSysDateTime(), true, out drugSendInfo);

                                    //������ʾ


                                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                                    if (findRow == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����δ��ҩƷʧ��!");

                                        return;
                                    }
                                    FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                                    fFind.Item.Qty += f.Item.Qty;

                                    fFind.ConfirmedQty = fFind.Item.Qty;

                                    fFind.NoBackQty = fFind.FeePack == "1" ?
                                        int.Parse(this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text)*fFind.Item.PackQty :
                                        int.Parse(this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text); //int.Parse(this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text);
                                   

                                    fFind.NoBackQty += f.Item.Qty;
                                    fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                                    fFind.FT.OwnCost = fFind.FT.TotCost;
                                    //fFind.FT.TotCost += f.FT.TotCost;
                                    //fFind.FT.PubCost += f.FT.PubCost;
                                    //fFind.FT.PayCost += f.FT.PayCost;
                                    //fFind.FT.OwnCost += f.FT.OwnCost;

                                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Amount].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty / fFind.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.Item.Qty, 2).ToString();
                                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();
                                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty / fFind.Item.PackQty, 2).ToString() :
                                        FS.FrameWork.Public.String.FormatNumber(fFind.NoBackQty, 2).ToString();
                                   // this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = fFind.NoBackQty.ToString();

                                    f.Item.Qty = 0;
                                    if (f.Item.SysClass.ID.ToString() == "PCC")
                                    {
                                        decimal doseOnce = (fFind.NoBackQty) / fFind.Days;

                                        (this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList).Order.DoseOnce = doseOnce;

                                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "ÿ����:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "����:" + f.Days.ToString();
                                    }
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = "0";
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = string.Empty;
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = string.Empty;
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = string.Empty;
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = string.Empty;
                                    this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = string.Empty;
                                    //{433AA56A-264F-4c8c-BC7E-52DAEAFDC605}
                                    this.fpSpread2_Sheet1.Rows[currRow].Tag = null;
                                }
                            }
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������ҩȷ�Ϻ����ȡ���쳣!" + ex.Message.ToString());
                        return;
                    }
                }
                else
                {
                    #region �޸� mad
                    //δ��׼
                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
                    {
                        return;
                    }

                    try
                    {
                        FS.HISFC.Models.Fee.ReturnApply temp = this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        int findRow = FindItem(temp.RecipeNO, temp.SequenceNO, this.fpSpread1_Sheet1);

                        if (findRow == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����δ��ҩƷʧ��!");

                            return;
                        }
                        FeeItemList fFind = this.fpSpread1_Sheet1.Rows[findRow].Tag as FeeItemList;
                        fFind.NoBackQty = int.Parse(this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text);
                        fFind.NoBackQty += temp.FeePack == "1" ?
                                        FS.FrameWork.Public.String.FormatNumber(temp.Item.Qty / temp.Item.PackQty, 2) :
                                        FS.FrameWork.Public.String.FormatNumber(temp.Item.Qty, 2);

                        fFind.ConfirmedQty = fFind.FeePack=="1"?fFind.NoBackQty*fFind.Item.PackQty:fFind.NoBackQty;

                       

                        //fFind.NoBackQty += temp.NoBackQty;
                        //fFind.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fFind.Item.Price * fFind.Item.Qty / fFind.Item.PackQty, 2) - fFind.FT.RebateCost;
                        //fFind.FT.OwnCost = fFind.FT.TotCost;

                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.NoBackQty].Text = (fFind.NoBackQty == 0 ? fFind.Item.Qty : fFind.NoBackQty).ToString();
                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.Cost].Text = fFind.FT.TotCost.ToString();

                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.ItemName].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Specs].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.PriceUnit].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Price].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Cost].Text = "";
                        this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Amount].Text = "";
                        this.fpSpread2_Sheet1.Rows[currRow].Tag = null;

                        this.fpSpread1_Sheet1.Rows[findRow].Tag = fFind;
                    #endregion

                    }
                    catch (Exception)
                    {

                        return;
                    }
                   
                   
                  

                }
            }

            return;
        }
        protected override void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} ������ҩȷ�Ϻ����ȡ��
            if (this.isUseCancelQuitConfirm)
            {
                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    //�Ƿ��������
                    if (IsAllowQuitFeeHalf == false)
                    {
                        this.DealCancelQuitOperation(false);
                        return;
                    }
                    else
                    {
                        this.DealCancelQuitOperation(true);
                    }
                }
            }
            else
            {
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.IsQuitDrugConfirm = true;
            this.ItemType = ItemTypes.Pharmarcy;
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint);

                return printType;
            }
        }

        #endregion

    }
}
