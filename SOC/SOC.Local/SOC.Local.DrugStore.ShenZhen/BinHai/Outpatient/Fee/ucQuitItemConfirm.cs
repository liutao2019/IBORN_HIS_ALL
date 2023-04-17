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
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using FS.HISFC.Components.OutpatientFee;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient.Fee
{
    public partial class ucQuitItemConfirm : FS.HISFC.Components.OutpatientFee.Controls.ucQuitItemApply, FS.FrameWork.WinForms.Forms.IInterfaceContainer
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

        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// ��Ҫ�˷ѵ���Ϣ��ÿ�α���ǰ���
        /// </summary>
        private ArrayList alQuitFeeItemList = new ArrayList();

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

        /// <summary>
        /// ��ҩ��˺�ֱ���˷ѿ��Ҵ���
        /// </summary>
        string directQuitFeeDept = string.Empty;
        [Category("����"), Description("��ҩ��˺�ֱ���˷ѿ��Ҵ���,������ö��Ÿ���)")]
        public string DirectQuitFeeDept
        {
            set
            {
                this.directQuitFeeDept = value;
            }
            get
            {
                return this.directQuitFeeDept;
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
                        infoCounts++;
                    }
                }
            }

            if (infoCounts == 0) 
            {
                MessageBox.Show("û����Ҫ��˵ķ���!");

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

            #region ��ҩ��˺�ֱ���˷� By Huangd   2012/09/12

            bool isDirectQuitFee = false;
            FS.HISFC.Models.Registration.Register regObj = (new FS.HISFC.BizProcess.Integrate.Registration.Registration()).GetByClinic(this.patient.ID);
            if (regObj == null)
            {
                MessageBox.Show("��ȡ�Һ���Ϣ����, �޷�ֱ���˷�!", "��ʾ");
            }
            else
            {
                if (this.itemType == ItemTypes.Pharmarcy &&
                    !string.IsNullOrEmpty(this.directQuitFeeDept) && this.IsAllConfirm() &&
                    this.directQuitFeeDept.IndexOf(this.patient.DoctorInfo.Templet.Dept.ID.ToString()) > -1)
                {
                    if (regObj.DoctorInfo.Templet.Dept.ID == "3110" && regObj.DoctorInfo.Templet.RegLevel.ID != "5")
                    {
                        MessageBox.Show("ȫ�����ﻼ��û��ȫ�ƹҺ�, ����ֱ���˷�!", "��ʾ");
                    }
                    else
                    {
                        string recipeDept = string.Empty;
                        if (alBackFeeList.Count > 0)
                            recipeDept = ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)alBackFeeList[0]).RecipeOper.Dept.ID;
                        string[] deptList = this.directQuitFeeDept.Split(',');
                        for (int i = 0; i < deptList.Length; i++)
                        {
                            if (deptList[i] == recipeDept)
                                isDirectQuitFee = true;
                        }
                        if (isDirectQuitFee)  //ֱ���˷�
                        {
                            if (patient != null && this.isAccount)
                            {
                                if (SaveAccountQuiteFee() == 1)
                                    this.Clear();
                                else
                                    isDirectQuitFee = false;
                            }
                            else
                            {
                                if (this.SaveQuitFee() == 1)                                
                                    this.Clear();
                                else
                                    isDirectQuitFee = false;
                            }
                        }
                    }
                }
            }

            #endregion
           
            MessageBox.Show(isDirectQuitFee == true ? "��ҩ��˲�ֱ���˷ѳɹ�!" : "��˳ɹ�!");            

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

        /// <summary>
        /// �˻��˷�
        /// </summary>
        /// <param name="f"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int SaveAccountQuiteFee()
        {
            if (!feeIntegrate.CheckAccountPassWord(this.patient))
            {
                return -1;
            }

            if (!IsQuitItem())
            {
                return -1;
            }

            ArrayList alFee = new ArrayList();
            FeeItemList tempf = null;
            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn;
            FeeItemList f = null;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread1.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;

                    f = (sv.Rows[i].Tag as FeeItemList).Clone();

                    #region ������������
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (f.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(f.RecipeNO, f.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���Ϸ�ҩ�������!" + pharmacyIntegrate.Err);
                                return -1;
                            }
                        }

                        tempf = f.Clone();
                        tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                        tempf.FT.OwnCost = tempf.FT.TotCost;
                        //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun ��������ѷ�ҩ֮�������ˣ��˷�ʱȫ�˵����
                        if (f.Item.Qty > 0)
                        {
                            tempf.User03 = "HalfQuit";
                            alFee.Add(tempf);
                        }
                    }
                    else
                    {
                        //��δȷ�ϵ���ҩ��������ҩ����!
                        if (f.IsConfirmed == false)
                        {
                            iReturn = confirmIntegrate.CancelConfirmTerminal(f.Order.ID, f.Item.ID);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�����ն��������!" + confirmIntegrate.Err);

                                return -1;
                            }
                        }
                        else
                        {
                            #region �����ն�ȷ����Ϣ
                            tempf = f.Clone();
                            tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                            tempf.FT.OwnCost = tempf.FT.TotCost;

                            //{06212A22-5FD4-4db3-838C-1790F75FF286}
                            //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                            if (f.Item.Qty > 0)
                            {
                                FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                                if (unDrugTemp != null)
                                {
                                    tempf.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                                    tempf.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                                }

                                //{06212A22-5FD4-4db3-838C-1790F75FF286}
                                if (tempf.IsConfirmed == true)
                                {
                                    int row = this.FindItem(tempf.RecipeNO, tempf.SequenceNO, this.fpSpread2_Sheet2);
                                    if (row != -1)
                                    {
                                        FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                        if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(tempf.Order.ID, (int)(tempf.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(tempf.Item.Price * tempf.Item.Qty, 2)) == -1)
                                        {
                                            FS.FrameWork.Management.PublicTrans.RollBack();
                                            MessageBox.Show("�����ն�ȷ����Ϣ����!" + confirmIntegrate.Err);
                                            return -1;
                                        }
                                    }
                                }
                                tempf.User03 = "HalfQuit";
                                alFee.Add(tempf);
                            }
                            #endregion
                        }

                    }

                    #endregion

                    #region ���·����˷ѱ��
                    if (outpatientManager.UpdateFeeItemListCancelType(f.RecipeNO, f.SequenceNO, CancelTypes.Canceled) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion

                    #region �帺��¼

                    FeeItemList feeItem = outpatientManager.GetFeeItemListForFee(f.RecipeNO, f.SequenceNO);
                    if (feeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);
                        return -1;
                    }
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    feeItem.FT.OwnCost = -feeItem.FT.OwnCost;
                    feeItem.FT.PayCost = -feeItem.FT.PayCost;
                    feeItem.FT.PubCost = -feeItem.FT.PubCost;
                    feeItem.FT.TotCost = feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost;
                    feeItem.Item.Qty = -feeItem.Item.Qty;
                    feeItem.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                    feeItem.FeeOper.ID = outpatientManager.Operator.ID;
                    feeItem.FeeOper.OperTime = nowTime;
                    feeItem.ChargeOper.OperTime = nowTime;
                    feeItem.ConfirmedInjectCount = 0;
                    feeItem.InvoiceCombNO = this.outpatientManager.GetTempInvoiceComboNO();
                    iReturn = outpatientManager.InsertFeeItemList(feeItem);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������ϸ������Ϣ����!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion
                }
            }

            #region ����
            ArrayList drugList = new ArrayList();
            if (alFee.Count > 0)
            {
                foreach (FeeItemList item in alFee)
                {
                    item.FeeOper.OperTime = nowTime;
                    item.PayType = PayTypes.Balanced;
                    item.TransType = TransTypes.Positive;
                    item.InvoiceCombNO = outpatientManager.GetTempInvoiceComboNO();
                    if (outpatientManager.InsertFeeItemList(item) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���������ϸʧ�ܣ�" + outpatientManager.Err);
                        return -1;
                    }

                    //��ҩ����
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed)
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugList.Add(f);
                            }
                        }
                    }
                    else
                    {
                        //�ն�����
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.IsNeedConfirm)
                            {
                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, this.patient);
                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�����ն�����ȷ�ϱ�ʧ��!" + confirmIntegrate.Err);

                                    return -1;
                                }
                            }
                        }
                    }

                }
                if (drugList.Count > 0)
                {
                    string drugSendInfo = string.Empty;
                    iReturn = this.pharmacyIntegrate.ApplyOut(patient, drugList, string.Empty, nowTime, false, out drugSendInfo);
                    if (iReturn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ҩƷ��ϸʧ��!" + pharmacyIntegrate.Err);
                        return -1;
                    }
                }
            }
            #endregion

            #region �����˷������˷ѱ��
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            DateTime operDate = outpatientManager.GetDateTimeFromSysDateTime();
            string operCode = outpatientManager.Operator.ID;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        returnApply.CancelType = CancelTypes.Valid;
                        returnApply.CancelOper.ID = operCode;
                        returnApply.CancelOper.OperTime = operDate;
                        if (returnApplyManager.UpdateApplyCharge(returnApply) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������˷ѱ��ʧ�ܣ�" + returnApplyManager.Err);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region �����˻�
            decimal cost = 0m;
            FS.HISFC.Models.Fee.ReturnApply applyItem = null;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList fitem = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag == null) continue;
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        applyItem = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        cost += FS.FrameWork.Public.String.FormatNumber(applyItem.Item.Price * applyItem.Item.Qty / applyItem.Item.PackQty, 2);
                    }
                    if (sv.Rows[i].Tag is FeeItemList)
                    {
                        fitem = sv.Rows[i].Tag as FeeItemList;
                        cost += FS.FrameWork.Public.String.FormatNumber(fitem.Item.Price * fitem.Item.Qty / fitem.Item.PackQty, 2);
                    }
                }
            }
            if (feeIntegrate.AccountCancelPay(patient, -cost, "�����˷�", (outpatientManager.Operator as Employee).Dept.ID, string.Empty) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                return -1;
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�����˻����" + cost.ToString() + "Ԫ");
            return 1;
        }

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveQuitFee()
        {
            //�ж���Ч��
            if (!IsValid())
            {
                return -1;
            }

            long returnValue = 0;//����ֵ,��Ҫ��ҽ����

            this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn = 0;

            //��ø���Ʊ��ˮ��
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return -1;
            }
            #region ��¼���Ϸ�Ʊ�Ľ��
            decimal CancelTotCost = 0; //���Ϸ�Ʊ���ܽ��
            decimal CancelOwnCost = 0;//���Ϸ�Ʊ���Էѽ��
            decimal CancelPayCost = 0;//���Ϸ�Ʊ���Ը����
            decimal CancelPubCost = 0;//���Ϸ�Ʊ�Ĺ��ѽ��
            decimal CancelRebateCost = 0; // �����Żݼ�����
            string InvoiceNO = "";
            #endregion

            //Ϊ�˴���Ʊ������Ʊ��ϸ������ {BB77678F-A3E1-4f62-9D8D-8D52C1C17F8B}
            ArrayList alInvoiceDetails = new ArrayList();


            foreach (Balance invoice in this.quitInvoices)
            {
                #region ��Ʊ������

                InvoiceNO = invoice.Invoice.ID;
                iReturn = outpatientManager.UpdateBalanceCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ԭʼ��Ʊ��Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�÷�Ʊ�Ѿ�����!");

                    return -1;
                }

                //���븺��¼����
                Balance invoClone = invoice.Clone();

                CancelTotCost += invoClone.FT.TotCost;
                CancelOwnCost += invoClone.FT.OwnCost;
                CancelPayCost += invoClone.FT.PayCost;
                CancelPubCost += invoClone.FT.PubCost;

                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                invoClone.FT.TotCost = -invoClone.FT.TotCost;
                invoClone.FT.OwnCost = -invoClone.FT.OwnCost;
                invoClone.FT.PayCost = -invoClone.FT.PayCost;
                invoClone.FT.PubCost = -invoClone.FT.PubCost;
                invoClone.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                invoClone.CanceledInvoiceNO = invoice.ID;
                invoClone.CancelOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;//�ս���Ҫ ��Ϊ��ǰ�˷���
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CancelOper.OperTime = nowTime;
                invoClone.IsAuditing = false;
                invoClone.AuditingOper.ID = string.Empty;
                invoClone.AuditingOper.OperTime = DateTime.MinValue;
                invoClone.IsDayBalanced = false;
                invoClone.BalanceID = string.Empty;
                invoClone.DayBalanceOper.OperTime = DateTime.MinValue;

                invoClone.CombNO = invoiceSeqNegative;

                iReturn = outpatientManager.InsertBalance(invoClone);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뷢Ʊ������Ϣ����!!" + outpatientManager.Err);

                    return -1;
                }
                #endregion

                #region ��Ʊ��ϸ��Ϣ����
                //����Ʊ��ϸ����Ϣ
                ArrayList alInvoiceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, invoice.CombNO);
                if (alInvoiceDetail == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��÷�Ʊ��ϸ����!" + outpatientManager.Err);

                    return -1;
                }


                //���Ϸ�Ʊ��ϸ����Ϣ
                iReturn = outpatientManager.UpdateBalanceListCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���Ϸ�Ʊ��ϸ����!" + outpatientManager.Err);

                    return -1;
                }

                foreach (BalanceList d in alInvoiceDetail)
                {
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    d.BalanceBase.FT.OwnCost = -d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = -d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = -d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                    d.BalanceBase.IsDayBalanced = false;
                    d.BalanceBase.DayBalanceOper.ID = string.Empty;
                    d.BalanceBase.DayBalanceOper.OperTime = DateTime.MinValue;
                    //d.CombNO = invoiceSeqNegative;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqNegative;

                    iReturn = outpatientManager.InsertBalanceList(d);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���뷢Ʊ��ϸ������Ϣ����!" + outpatientManager.Err);

                        return -1;
                    }
                }
                #endregion

                //Ϊ�˴���Ʊ������Ʊ��ϸ������ {D5FA97FA-8DBB-48e7-BF5B-8DF4049EEE2B}
                alInvoiceDetails.Add(alInvoiceDetail);
            }

            Balance invoiceInfo = ((Balance)quitInvoices[0]);

            #region ����֧����Ϣ
            ArrayList payList = new ArrayList();
            string choosePayMode = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT, "1");
            ArrayList feePayMods = this.outpatientManager.QueryBalancePaysByInvoiceSequence(invoiceInfo.CombNO);

            if (feePayMods.Count >= 0)
            {
                // {4E2CD49B-1D3B-43cb-A931-C5503A68373C}
                //if (!this.IsQuitSamePayMod || invoiceInfo.IsAccount)
                if (!this.IsQuitSamePayMod)
                {
                    CancelRebateCost = 0;
                    int paySquence = 100;
                    BalancePay objPay = new BalancePay();

                    #region ���ڼ��⡢���ʻ��ߣ�������⡢��������

                    foreach (BalancePay payRebat in feePayMods)
                    {
                        if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ")
                        {
                            // ���⡢�������ݴ���

                            paySquence--;
                            CancelRebateCost += payRebat.FT.TotCost;

                            objPay = payRebat.Clone();
                            objPay.TransType = TransTypes.Negative;
                            objPay.FT.TotCost = -objPay.FT.TotCost;
                            objPay.FT.RealCost = -objPay.FT.RealCost;
                            objPay.FT.OwnCost = -objPay.FT.OwnCost;
                            objPay.InputOper.OperTime = nowTime;
                            objPay.Invoice.ID = InvoiceNO;
                            objPay.Squence = paySquence.ToString();

                            objPay.InputOper.ID = outpatientManager.Operator.ID;
                            objPay.InvoiceCombNO = invoiceSeqNegative;
                            objPay.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                            objPay.IsChecked = false;
                            objPay.CheckOper.ID = string.Empty;
                            objPay.CheckOper.OperTime = DateTime.MinValue;
                            objPay.BalanceOper.ID = string.Empty;
                            objPay.IsDayBalanced = false;
                            objPay.IsAuditing = false;
                            objPay.AuditingOper.OperTime = DateTime.MinValue;
                            objPay.AuditingOper.ID = string.Empty;
                            iReturn = outpatientManager.InsertBalancePay(objPay);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);

                                return -1;
                            }
                        }
                    }

                    #endregion

                    if (CancelRebateCost - CancelOwnCost - CancelPayCost < 0)
                    {
                        paySquence--;

                        // {B176923A-5C7E-46a9-A4C6-ED6313ACC4E5}
                        // �Ƿ�����ԭ֧����ʽ�˷� false:������ true:����
                        #region ԭ����
                        //ArrayList payList = new ArrayList();
                        objPay = new BalancePay();
                        objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        objPay.FT.TotCost = -(CancelPayCost + CancelOwnCost - CancelRebateCost);
                        objPay.FT.RealCost = FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(-(CancelPayCost + CancelOwnCost - CancelRebateCost));
                        objPay.FT.OwnCost = -(CancelOwnCost - CancelRebateCost);
                        objPay.InputOper.OperTime = nowTime;
                        objPay.Invoice.ID = InvoiceNO;
                        objPay.Squence = paySquence.ToString();
                        if (invoiceInfo.IsAccount)
                        {
                            objPay.PayType.ID = "YS";
                        }
                        else
                        {
                            objPay.PayType.ID = "CA";
                        }
                        objPay.InputOper.ID = outpatientManager.Operator.ID;
                        objPay.InvoiceCombNO = invoiceSeqNegative;
                        objPay.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                        objPay.IsChecked = false;
                        objPay.CheckOper.ID = string.Empty;
                        objPay.CheckOper.OperTime = DateTime.MinValue;
                        objPay.BalanceOper.ID = string.Empty;
                        //p.BalanceNo = 0;
                        objPay.IsDayBalanced = false;
                        objPay.IsAuditing = false;
                        objPay.AuditingOper.OperTime = DateTime.MinValue;
                        objPay.AuditingOper.ID = string.Empty;
                        if (patient.Pact.PayKind.ID == "02")
                        {
                            objPay.FT.OwnCost = -CancelOwnCost;
                        }

                        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                        #region �˻�����(�˻�����۷ѽ��)
                        if (objPay.PayType.ID == "YS")
                        {
                            if (feeIntegrate.AccountCancelPay(patient, objPay.FT.OwnCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                                return -1;
                            }
                        }
                        #endregion

                        iReturn = outpatientManager.InsertBalancePay(objPay);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);

                            return -1;
                        }
                        payList.Add(objPay);
                        #endregion
                    }
                }
                else
                {
                    #region �¼ӵ�

                    int returnJValue = this.outpatientManager.UpdateBalancePayModeCancelType(invoiceInfo.Invoice.ID, invoiceInfo.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    if (returnJValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���Ϸ�Ʊ֧����ʽ����!" + outpatientManager.Err);
                        return -1;
                    }

                    int bpIdx = 0;
                    foreach (FS.HISFC.Models.Fee.Outpatient.BalancePay bp in feePayMods)
                    {
                        if (bp != null)
                        {
                            BalancePay objPay = bp.Clone();
                            if (bp.PayType.ID == "CD" || bp.PayType.ID == "DB")
                            {
                                decimal bankTransTot = 0m;
                                bankTransTot = -objPay.FT.TotCost;
                                bool isBankTransOK = false;

                                try
                                {
                                    bankTrans.InputListInfo.Clear();
                                    bankTrans.OutputListInfo.Clear();
                                    /// 0:�������ͣ�1�����׽��
                                    bankTrans.InputListInfo.Add("1");
                                    bankTrans.InputListInfo.Add(bankTransTot);
                                    isBankTransOK = bankTrans.Do();
                                }
                                catch (Exception ex)
                                {
                                    isBankTransOK = false;
                                }
                                if (isBankTransOK == false)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(bankTrans.OutputListInfo[0].ToString());
                                    return -1;
                                }
                                if (bankTrans.OutputListInfo.Count >= 4)
                                {
                                    if (bankTransTot != NConvert.ToDecimal(bankTrans.OutputListInfo[3]))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("����������" + bankTransTot.ToString() + "�����ڽ��׽��" + NConvert.ToDecimal(bankTrans.OutputListInfo[3]) + ",����ʧ�ܣ�");
                                        return -1;
                                    }
                                    else
                                    {
                                        objPay.Bank.Name = bankTrans.OutputListInfo[0].ToString();
                                        objPay.Bank.Account = bankTrans.OutputListInfo[1].ToString();
                                        objPay.Squence = bankTrans.OutputListInfo[2].ToString();
                                    }
                                }
                            }
                            #region
                            objPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            objPay.FT.TotCost = -objPay.FT.TotCost;
                            objPay.FT.RealCost = -objPay.FT.RealCost;
                            objPay.FT.OwnCost = -objPay.FT.OwnCost;
                            objPay.InputOper.OperTime = nowTime;
                            objPay.Invoice.ID = InvoiceNO;
                            objPay.Squence = (99 - bpIdx).ToString();
                            objPay.InputOper.ID = outpatientManager.Operator.ID;
                            objPay.InvoiceCombNO = invoiceSeqNegative;
                            objPay.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                            objPay.IsChecked = false;
                            objPay.CheckOper.ID = string.Empty;
                            objPay.CheckOper.OperTime = DateTime.MinValue;
                            objPay.BalanceOper.ID = string.Empty;
                            //p.BalanceNo = 0;
                            objPay.IsDayBalanced = false;
                            objPay.IsAuditing = false;
                            objPay.AuditingOper.OperTime = DateTime.MinValue;
                            objPay.AuditingOper.ID = string.Empty;
                            #endregion
                            iReturn = outpatientManager.InsertBalancePay(objPay);
                            if (iReturn <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);
                                return -1;
                            }
                            #region �˻�����(�˻�����۷ѽ��)
                            if (objPay.PayType.ID == "YS")
                            {
                                if (feeIntegrate.AccountCancelPay(patient, objPay.FT.TotCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�˻��˷��뻧ʧ�ܣ�" + feeIntegrate.Err);
                                    return -1;
                                }
                            }
                            #endregion


                            bpIdx++;

                            #region ���ڼ��⡢���ʻ��ߣ�������⡢��������

                            if (objPay.PayType.ID != "RC" || objPay.PayType.ID != "JZ")
                            {
                                payList.Add(objPay);
                            }

                            #endregion
                        }
                    }
                    #endregion
                }
            }
            #endregion

            bool isCashPay = false;//�Ƿ��ֽ����

            #region ��¼�˷���Ϣ
            alQuitFeeItemList.Clear();
            FS.HISFC.Models.Fee.ReturnApply returnApply = null;
            foreach (FarPoint.Win.Spread.SheetView sv in fpSpread2.Sheets)
            {
                for (int i = 0; i < sv.Rows.Count; i++)
                {
                    if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        returnApply = sv.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(returnApply.RecipeNO, returnApply.SequenceNO);
                        if (feeItemList == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��ȡ�˷������Ӧ������ϸʧ�ܣ�" + returnApplyManager.Err);
                            return -1;
                        }
                        alQuitFeeItemList.Add(feeItemList);
                    }
                    else if (sv.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    {
                        alQuitFeeItemList.Add((FS.HISFC.Models.Fee.Outpatient.FeeItemList)sv.Rows[i].Tag);
                    }
                }
            }

            if (this.isQuitFeeAndOperOrder)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemTemp in alQuitFeeItemList)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderTemp = orderIntegrate.GetOneOrder(feeItemTemp.Patient.ID, feeItemTemp.Order.ID.ToString());
                    if (orderTemp != null && orderTemp.Status == 1)
                    {
                        this.orderIntegrate.UpdateOrderBeCaceled(orderTemp);
                    }
                }
            }

            #endregion

            //���������ϸ
            ArrayList alFeeDetail = outpatientManager.QueryFeeItemListsByInvoiceSequence(invoiceInfo.CombNO);
            if (alFeeDetail == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û��߷�����ϸ����!" + outpatientManager.Err);

                return -1;
            }
            iReturn = outpatientManager.UpdateFeeItemListCancelType(invoiceInfo.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
            if (iReturn <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);

                return -1;
            }

            foreach (FeeItemList f in alFeeDetail)
            {
                f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                f.FT.OwnCost = -f.FT.OwnCost;
                f.FT.PayCost = -f.FT.PayCost;
                f.FT.PubCost = -f.FT.PubCost;
                f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                f.Item.Qty = -f.Item.Qty;
                f.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.ChargeOper.OperTime = nowTime;
                f.InvoiceCombNO = invoiceSeqNegative;
                f.ConfirmedInjectCount = 0;

                iReturn = outpatientManager.InsertFeeItemList(f);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������ϸ������Ϣ����!" + outpatientManager.Err);

                    return -1;
                }
            }

            this.medcareInterfaceProxy.BeginTranscation();
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�û�����Ϣʧ��") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(this.patient, ref alFeeDetail);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��ϴ��˷���ϸʧ��") + medcareInterfaceProxy.ErrMsg);

                return -1;
            }
            this.patient.SIMainInfo.InvoiceNo = ((Balance)quitInvoices[0]).Invoice.ID;
            returnValue = medcareInterfaceProxy.CancelBalanceOutpatient(this.patient, ref alFeeDetail);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڽ���ʧ��") + medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            //���δ��׼��ҩ��Ϣ
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fQuit = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //��δȷ�ϵ���ҩ��������ҩ����!
                        if (fQuit.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(fQuit.RecipeNO, fQuit.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                medcareInterfaceProxy.Rollback();
                                MessageBox.Show("���Ϸ�ҩ�������!ҩƷ�����Ѿ���ҩ����ˢ�´�������");

                                return -1;
                            }
                        }
                    }
                }
            }
            //�����ն�����
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null && this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    //��δȷ�ϵ���ҩ��������ҩ����!
                    if (fQuit.IsConfirmed == false)
                    {
                        iReturn = confirmIntegrate.CancelConfirmTerminal(fQuit.Order.ID, fQuit.Item.ID);
                        if (iReturn < 0)
                        {
                            medcareInterfaceProxy.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�����ն��������!" + confirmIntegrate.Err);

                            return -1;
                        }
                    }
                }
            }

            #region �������˷Ѳ��ֽ����˿�
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            ArrayList alMate = new ArrayList();
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null && this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;

                    //�Ƕ��յ����� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    if (fQuit.Item.ItemType == EnumItemType.MatItem)
                    {
                        alMate.Add(fQuit);
                    }
                    else
                    {
                        if (fQuit.MateList.Count > 0)
                        {
                            alMate.Add(fQuit);
                        }
                    }
                }
            }
            if (alMate.Count > 0)
            {
                //�˿�
                if (mateIntegrate.MaterialFeeOutputBack(alMate) < 0)
                {
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("�����˿�ʧ��,\n" + mateIntegrate.Err);

                    return -1;
                }
            }
            #endregion

            //��ʣ����Ŀ�շ�!
            ArrayList feeDetails = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun ��������ѷ�ҩ֮�������ˣ��˷�ʱȫ�˵����
                    {
                        f.User03 = "HalfQuit";
                        feeDetails.Add(f);
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;

                    //{06212A22-5FD4-4db3-838C-1790F75FF286}
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, (int)UndrugList.Amount].Text) > 0)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug unDrugTemp = this.undrugManager.GetUndrugByCode(f.Item.ID);
                        if (unDrugTemp != null)
                        {
                            f.Item.IsNeedConfirm = unDrugTemp.IsNeedConfirm;
                            f.Item.IsNeedBespeak = unDrugTemp.IsNeedBespeak;
                        }

                        //{06212A22-5FD4-4db3-838C-1790F75FF286}
                        if (f.IsConfirmed == true)
                        {
                            int row = this.FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread2_Sheet2);
                            if (row != -1)
                            {
                                FeeItemList quitItem = this.fpSpread2_Sheet2.Rows[row].Tag as FeeItemList;
                                if (confirmIntegrate.UpdateOrDeleteTerminalConfirmApply(f.Order.ID, (int)(f.Item.Qty + quitItem.Item.Qty), (int)quitItem.Item.Qty, FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty, 2)) == -1)
                                {
                                    medcareInterfaceProxy.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�����ն�ȷ����Ϣ����!" + confirmIntegrate.Err);
                                    return -1;
                                }
                            }
                        }


                        f.User03 = "HalfQuit";
                        feeDetails.Add(f);
                    }
                }
            }
            string returnCostString = string.Empty;
         
            #region ȫ��

            decimal orgCost = 0;
            decimal otherCost = 0m;
            bool isHaveCard = false;

            if (isHaveCard)
            {
                if (otherCost > 0)
                {
                    returnCostString = "Ӧ�˽��:�ֽ� " + (CancelOwnCost - CancelRebateCost).ToString() + "  ����֧����ʽ:" + CancelPubCost.ToString();
                }
                else
                {
                    returnCostString = "Ӧ�˽��: " + (CancelOwnCost - CancelRebateCost).ToString();
                }
            }
            else
            {
                decimal caCost = 0m;
                decimal chCost = 0m;
                decimal bankCost = 0m;
                decimal xxCost = 0m;
                foreach (BalancePay p in feePayMods)
                {
                    if (p.PayType.ID == "CA")
                    {
                        caCost += p.FT.RealCost;
                    }
                    else if (p.PayType.ID == "CH")
                    {
                        chCost += p.FT.RealCost;
                    }
                    else if (p.PayType.ID == "CD" || p.PayType.ID == "DB")
                    {
                        bankCost += p.FT.RealCost;
                    }
                    else
                    {
                        xxCost += p.FT.RealCost;
                    }
                }
                returnCostString = "Ӧ�˽��: " + FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(CancelOwnCost - CancelRebateCost).ToString() + System.Environment.NewLine +
                    "���У�" + System.Environment.NewLine +
                    "�ֽ�{0}" + System.Environment.NewLine +
                    "����{1}" + System.Environment.NewLine +
                    "֧Ʊ{2}" + System.Environment.NewLine +
                    "����{3}" + System.Environment.NewLine
                    ;
                returnCostString = string.Format(returnCostString, caCost.ToString(), bankCost.ToString(), chCost.ToString(), xxCost.ToString());
            }

            if (InterfaceManager.GetIOrder() != null)
            {
                if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "�˷�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + InterfaceManager.GetIOrder().Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.medcareInterfaceProxy.Commit();

            tbQuitCash.Text = CancelOwnCost.ToString();

            #endregion

            return 1;
        }

        /// <summary>
        /// �Ƿ�ȫ��
        /// </summary>
        /// <returns>�ɹ�true ʧ�� false</returns>
        protected virtual bool IsAllConfirm()
        {
            decimal qty = 0;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    qty += NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 6].Text);                     
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    qty += NConvert.ToDecimal(this.fpSpread1_Sheet2.Cells[i, 6].Text);  
                }
            }
            if (qty > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
