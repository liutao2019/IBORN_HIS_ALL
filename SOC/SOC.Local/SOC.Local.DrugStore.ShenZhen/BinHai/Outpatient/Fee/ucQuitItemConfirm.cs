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
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 退费单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint IBackFeePrint = null;

        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 需要退费的信息，每次保存前清空
        /// </summary>
        private ArrayList alQuitFeeItemList = new ArrayList();

        /// <summary>
        /// 是否启用取消退药确认
        /// </summary>
        bool isUseCancelQuitConfirm = true;
        [Category("设置"), Description("是否启用取消退药确认")]
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
        [Category("控件设置"), Description("是否打印退药申请单")]
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
        /// 是否限制退药天数
        /// </summary>
        bool isLimitBackPha = false;
        [Category("控件设置"), Description("是否限制退药天数")]
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
        /// 限制退药的时间间隔
        /// </summary>
        int isLimitDays = 3;
        [Category("设置"), Description("限制的退药天数")]
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
        /// 退药审核后直接退费科室代码
        /// </summary>
        string directQuitFeeDept = string.Empty;
        [Category("设置"), Description("退药审核后直接退费科室代码,多科室用逗号隔开)")]
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


        #region 方法

        /// <summary>
        /// 保存审核信息
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
                MessageBox.Show("没有需要审核的费用!");

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
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} 门诊退药确认后可以取消
                    if (sv.Rows[i].Tag is ReturnApply && sv.Cells[i, (int)DrugListQuit.Flag].Text != "确认")
                    {
                        ReturnApply tempInsert = sv.Rows[i].Tag as ReturnApply;

                        ReturnApply tempExist = this.returnApplyManager.GetReturnApplyByApplySequence(tempInsert.Patient.ID, tempInsert.ID);
                        //找到已经存在数据库的退费申请信息
                        if (tempExist != null)
                        {
                            //if (tempExist.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show(tempExist.Item.Name + "已经被确认或者作废,请刷新");

                            //    return -1;
                            //}
                            if (tempExist.IsConfirmed)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(tempExist.Item.Name + "已经被确认或者作废,请刷新");

                                return -1;
                            }
                        }

                        returnValue = this.returnApplyManager.DeleteReturnApply(tempInsert.ID);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempExist.Item.Name + "删除失败!" + this.returnApplyManager.Err);

                            return -1;
                        }

                        tempInsert.ID = this.returnApplyManager.GetReturnApplySequence();
                        tempInsert.IsConfirmed = true;
                        tempInsert.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

                        returnValue = this.returnApplyManager.InsertReturnApply(tempInsert);

                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "审核失败!" + this.returnApplyManager.Err);

                            return -1;
                        }

                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(tempInsert.RecipeNO, tempInsert.SequenceNO);
                        if(feeItemList == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "获得项目失败!" + this.outpatientManager.Err);

                            return -1;
                        }

                        if (feeItemList.Item.Qty < feeItemList.NoBackQty + tempInsert.Item.Qty) 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("其他的操作员可能已经审核了" + feeItemList.Item.Name + "请刷新!");

                            return -1;
                        }

                        if (this.isLimitBackPha)
                        {
                            DateTime nowTimeTemp = this.returnApplyManager.GetDateTimeFromSysDateTime();
                            if((nowTime - feeItemList.FeeOper.OperTime).TotalDays >= this.isLimitDays)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("其他的操作员可能已经审核了" + feeItemList.Item.Name + "请刷新!");
                                return -1;
                            }
                        }
                       
                        //更新可退数量和确认数量
                        returnValue = this.outpatientManager.UpdateConfirmFlag(tempInsert.RecipeNO, tempInsert.SequenceNO, "1", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty + tempInsert.Item.Qty,
                            feeItemList.ConfirmedQty - tempInsert.Item.Qty);
                        if (returnValue <= 0) 
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新项目:" + feeItemList.Item.Name + "失败!" + this.outpatientManager.Err);

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
                                MessageBox.Show("药品退库失败!" + this.pharmacyIntegrate.Err);

                                return -1;
                            }

                            alBackFeeList.Add(feeItemList);

                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            #region 退药审核后直接退费 By Huangd   2012/09/12

            bool isDirectQuitFee = false;
            FS.HISFC.Models.Registration.Register regObj = (new FS.HISFC.BizProcess.Integrate.Registration.Registration()).GetByClinic(this.patient.ID);
            if (regObj == null)
            {
                MessageBox.Show("获取挂号信息出错, 无法直接退费!", "提示");
            }
            else
            {
                if (this.itemType == ItemTypes.Pharmarcy &&
                    !string.IsNullOrEmpty(this.directQuitFeeDept) && this.IsAllConfirm() &&
                    this.directQuitFeeDept.IndexOf(this.patient.DoctorInfo.Templet.Dept.ID.ToString()) > -1)
                {
                    if (regObj.DoctorInfo.Templet.Dept.ID == "3110" && regObj.DoctorInfo.Templet.RegLevel.ID != "5")
                    {
                        MessageBox.Show("全科门诊患者没有全科挂号, 不能直接退费!", "提示");
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
                        if (isDirectQuitFee)  //直接退费
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
           
            MessageBox.Show(isDirectQuitFee == true ? "退药审核并直接退费成功!" : "审核成功!");            

            base.GetItemList();

            if (alBackFeeList.Count > 0)
            {
                //不打印，直接返回
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
        /// 账户退费
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

                    #region 作废申请数据
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (f.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(f.RecipeNO, f.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("作废发药申请出错!" + pharmacyIntegrate.Err);
                                return -1;
                            }
                        }

                        tempf = f.Clone();
                        tempf.FT.OwnCost = tempf.FT.PubCost = tempf.FT.PayCost = 0;
                        tempf.FT.OwnCost = tempf.FT.TotCost;
                        //if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun 解决门诊已发药之后做半退，退费时全退的情况
                        if (f.Item.Qty > 0)
                        {
                            tempf.User03 = "HalfQuit";
                            alFee.Add(tempf);
                        }
                    }
                    else
                    {
                        //有未确认的退药，作废退药申请!
                        if (f.IsConfirmed == false)
                        {
                            iReturn = confirmIntegrate.CancelConfirmTerminal(f.Order.ID, f.Item.ID);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("作废终端申请出错!" + confirmIntegrate.Err);

                                return -1;
                            }
                        }
                        else
                        {
                            #region 更新终端确认信息
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
                                            MessageBox.Show("更新终端确认信息出错!" + confirmIntegrate.Err);
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

                    #region 更新费用退费标记
                    if (outpatientManager.UpdateFeeItemListCancelType(f.RecipeNO, f.SequenceNO, CancelTypes.Canceled) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion

                    #region 冲负记录

                    FeeItemList feeItem = outpatientManager.GetFeeItemListForFee(f.RecipeNO, f.SequenceNO);
                    if (feeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);
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
                        MessageBox.Show("插入费用明细冲帐信息出错!" + outpatientManager.Err);
                        return -1;
                    }
                    #endregion
                }
            }

            #region 半退
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
                        MessageBox.Show("插入费用明细失败！" + outpatientManager.Err);
                        return -1;
                    }

                    //发药申请
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
                        //终端申请
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.IsNeedConfirm)
                            {
                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, this.patient);
                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("处理终端申请确认表失败!" + confirmIntegrate.Err);

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
                        MessageBox.Show("处理药品明细失败!" + pharmacyIntegrate.Err);
                        return -1;
                    }
                }
            }
            #endregion

            #region 更新退费申请退费标记
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
                            MessageBox.Show("更新申请表退费标记失败！" + returnApplyManager.Err);
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 返还账户
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
            if (feeIntegrate.AccountCancelPay(patient, -cost, "门诊退费", (outpatientManager.Operator as Employee).Dept.ID, string.Empty) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("账户退费入户失败！" + feeIntegrate.Err);
                return -1;
            }

            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("返还账户金额" + cost.ToString() + "元");
            return 1;
        }

        /// <summary>
        /// 保存退费
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int SaveQuitFee()
        {
            //判断有效性
            if (!IsValid())
            {
                return -1;
            }

            long returnValue = 0;//返回值,主要给医保用

            this.medcareInterfaceProxy.SetPactCode(this.patient.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            int iReturn = 0;

            //获得负发票流水号
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获得发票流水号失败!" + outpatientManager.Err);

                return -1;
            }
            #region 记录作废发票的金额
            decimal CancelTotCost = 0; //作废发票的总金额
            decimal CancelOwnCost = 0;//作废发票的自费金额
            decimal CancelPayCost = 0;//作废发票的自付金额
            decimal CancelPubCost = 0;//作废发票的公费金额
            decimal CancelRebateCost = 0; // 作废优惠减免金额
            string InvoiceNO = "";
            #endregion

            //为了打退票，将发票明细存起来 {BB77678F-A3E1-4f62-9D8D-8D52C1C17F8B}
            ArrayList alInvoiceDetails = new ArrayList();


            foreach (Balance invoice in this.quitInvoices)
            {
                #region 发票主表处理

                InvoiceNO = invoice.Invoice.ID;
                iReturn = outpatientManager.UpdateBalanceCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废原始发票信息出错!" + outpatientManager.Err);

                    return -1;
                }
                if (iReturn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该发票已经作废!");

                    return -1;
                }

                //插入负纪录冲账
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
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;//日结需要 改为当前退费人
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
                    MessageBox.Show("插入发票冲账信息出错!!" + outpatientManager.Err);

                    return -1;
                }
                #endregion

                #region 发票明细信息处理
                //处理发票明细表信息
                ArrayList alInvoiceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, invoice.CombNO);
                if (alInvoiceDetail == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("获得发票明细出错!" + outpatientManager.Err);

                    return -1;
                }


                //作废发票明细表信息
                iReturn = outpatientManager.UpdateBalanceListCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                if (iReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废发票明细出错!" + outpatientManager.Err);

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
                        MessageBox.Show("插入发票明细冲账信息出错!" + outpatientManager.Err);

                        return -1;
                    }
                }
                #endregion

                //为了打退票，将发票明细存起来 {D5FA97FA-8DBB-48e7-BF5B-8DF4049EEE2B}
                alInvoiceDetails.Add(alInvoiceDetail);
            }

            Balance invoiceInfo = ((Balance)quitInvoices[0]);

            #region 处理支付信息
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

                    #region 对于减免、记帐患者，处理减免、记帐数据

                    foreach (BalancePay payRebat in feePayMods)
                    {
                        if (payRebat.PayType.ID == "RC" || payRebat.PayType.ID == "JZ")
                        {
                            // 减免、记帐数据处理

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
                                MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);

                                return -1;
                            }
                        }
                    }

                    #endregion

                    if (CancelRebateCost - CancelOwnCost - CancelPayCost < 0)
                    {
                        paySquence--;

                        // {B176923A-5C7E-46a9-A4C6-ED6313ACC4E5}
                        // 是否允许按原支付方式退费 false:不允许 true:允许
                        #region 原来的
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
                        #region 账户新增(账户冲掉扣费金额)
                        if (objPay.PayType.ID == "YS")
                        {
                            if (feeIntegrate.AccountCancelPay(patient, objPay.FT.OwnCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("账户退费入户失败！" + feeIntegrate.Err);
                                return -1;
                            }
                        }
                        #endregion

                        iReturn = outpatientManager.InsertBalancePay(objPay);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);

                            return -1;
                        }
                        payList.Add(objPay);
                        #endregion
                    }
                }
                else
                {
                    #region 新加的

                    int returnJValue = this.outpatientManager.UpdateBalancePayModeCancelType(invoiceInfo.Invoice.ID, invoiceInfo.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    if (returnJValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("作废发票支付方式出错!" + outpatientManager.Err);
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
                                    /// 0:交易类型，1：交易金额
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
                                        MessageBox.Show("交易请求金额" + bankTransTot.ToString() + "不等于交易金额" + NConvert.ToDecimal(bankTrans.OutputListInfo[3]) + ",交易失败！");
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
                                MessageBox.Show("插入支付负信息出错!" + outpatientManager.Err);
                                return -1;
                            }
                            #region 账户新增(账户冲掉扣费金额)
                            if (objPay.PayType.ID == "YS")
                            {
                                if (feeIntegrate.AccountCancelPay(patient, objPay.FT.TotCost, InvoiceNO, (outpatientManager.Operator as Employee).Dept.ID, "C") < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("账户退费入户失败！" + feeIntegrate.Err);
                                    return -1;
                                }
                            }
                            #endregion


                            bpIdx++;

                            #region 对于减免、记帐患者，处理减免、记帐数据

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

            bool isCashPay = false;//是否现金冲账

            #region 记录退费信息
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
                            MessageBox.Show("获取退费申请对应费用明细失败！" + returnApplyManager.Err);
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

            //处理费用明细
            ArrayList alFeeDetail = outpatientManager.QueryFeeItemListsByInvoiceSequence(invoiceInfo.CombNO);
            if (alFeeDetail == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获得患者费用明细出错!" + outpatientManager.Err);

                return -1;
            }
            iReturn = outpatientManager.UpdateFeeItemListCancelType(invoiceInfo.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Canceled);
            if (iReturn <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("作废患者明细出错!" + outpatientManager.Err);

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
                    MessageBox.Show("插入费用明细冲帐信息出错!" + outpatientManager.Err);

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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口初始化失败") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            returnValue = medcareInterfaceProxy.GetRegInfoOutpatient(this.patient);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口或得患者信息失败") + medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = medcareInterfaceProxy.DeleteUploadedFeeDetailsOutpatient(this.patient, ref alFeeDetail);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口上传退费明细失败") + medcareInterfaceProxy.ErrMsg);

                return -1;
            }
            this.patient.SIMainInfo.InvoiceNo = ((Balance)quitInvoices[0]).Invoice.ID;
            returnValue = medcareInterfaceProxy.CancelBalanceOutpatient(this.patient, ref alFeeDetail);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口结算失败") + medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            //针对未核准退药信息
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag != null)
                {
                    if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList fQuit = this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList;
                        //有未确认的退药，作废退药申请!
                        if (fQuit.IsConfirmed == false)
                        {
                            iReturn = pharmacyIntegrate.CancelApplyOutClinic(fQuit.RecipeNO, fQuit.SequenceNO);
                            if (iReturn < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                medcareInterfaceProxy.Rollback();
                                MessageBox.Show("作废发药申请出错!药品可能已经发药，请刷新窗口重试");

                                return -1;
                            }
                        }
                    }
                }
            }
            //作废终端申请
            for (int i = 0; i < this.fpSpread1_Sheet2.RowCount; i++)
            {
                if (this.fpSpread1_Sheet2.Rows[i].Tag != null && this.fpSpread1_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread1_Sheet2.Rows[i].Tag as FeeItemList;

                    //有未确认的退药，作废退药申请!
                    if (fQuit.IsConfirmed == false)
                    {
                        iReturn = confirmIntegrate.CancelConfirmTerminal(fQuit.Order.ID, fQuit.Item.ID);
                        if (iReturn < 0)
                        {
                            medcareInterfaceProxy.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("作废终端申请出错!" + confirmIntegrate.Err);

                            return -1;
                        }
                    }
                }
            }

            #region 对物资退费部分进行退库
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            ArrayList alMate = new ArrayList();
            for (int i = 0; i < this.fpSpread2_Sheet2.RowCount; i++)
            {
                if (this.fpSpread2_Sheet2.Rows[i].Tag != null && this.fpSpread2_Sheet2.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList fQuit = this.fpSpread2_Sheet2.Rows[i].Tag as FeeItemList;

                    //非对照的物资 {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
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
                //退库
                if (mateIntegrate.MaterialFeeOutputBack(alMate) < 0)
                {
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    medcareInterfaceProxy.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    MessageBox.Show("物资退库失败,\n" + mateIntegrate.Err);

                    return -1;
                }
            }
            #endregion

            //对剩余项目收费!
            ArrayList feeDetails = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FeeItemList)
                {
                    FeeItemList f = (this.fpSpread1_Sheet1.Rows[i].Tag as FeeItemList).Clone();
                    f.FT.OwnCost = f.FT.PubCost = f.FT.PayCost = 0;
                    f.FT.OwnCost = f.FT.TotCost;
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)DrugList.Amount].Text) > 0)//by yuyun 解决门诊已发药之后做半退，退费时全退的情况
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
                                    MessageBox.Show("更新终端确认信息出错!" + confirmIntegrate.Err);
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
         
            #region 全退

            decimal orgCost = 0;
            decimal otherCost = 0m;
            bool isHaveCard = false;

            if (isHaveCard)
            {
                if (otherCost > 0)
                {
                    returnCostString = "应退金额:现金 " + (CancelOwnCost - CancelRebateCost).ToString() + "  其他支付方式:" + CancelPubCost.ToString();
                }
                else
                {
                    returnCostString = "应退金额: " + (CancelOwnCost - CancelRebateCost).ToString();
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
                returnCostString = "应退金额: " + FS.HISFC.Components.OutpatientFee.Class.Function.DealCent(CancelOwnCost - CancelRebateCost).ToString() + System.Environment.NewLine +
                    "其中：" + System.Environment.NewLine +
                    "现金{0}" + System.Environment.NewLine +
                    "银联{1}" + System.Environment.NewLine +
                    "支票{2}" + System.Environment.NewLine +
                    "其他{3}" + System.Environment.NewLine
                    ;
                returnCostString = string.Format(returnCostString, caCost.ToString(), bankCost.ToString(), chCost.ToString(), xxCost.ToString());
            }

            if (InterfaceManager.GetIOrder() != null)
            {
                if (InterfaceManager.GetIOrder().SendFeeInfo(this.patient, alQuitFeeItemList, false) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(this, "退费失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIOrder().Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 是否全退
        /// </summary>
        /// <returns>成功true 失败 false</returns>
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

        #region 事件

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
            
            this.FindForm().Text = "退费审核";

            toolBarService.AddToolButton("退费审核", "审核申请信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("刷新", "重新刷新项目和退费申请信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            toolBarService.AddToolButton("清空", "清除录入信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("全退", "全部退除所有费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全退, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "退费审核":
                    this.Save();
                    break;
                case "刷新":
                    base.GetItemList();
                    break;
                case "清空":
                    base.Clear();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
            /// <summary>
        /// 处理取消退费操作
        /// </summary>
        protected virtual  void DealCancelQuitOperation(bool isAllowQuitFeeHalf)
        {
            if (this.fpSpread2.ActiveSheet == this.fpSpread2_Sheet1)//药品 
            {
                int currRow = this.fpSpread2_Sheet1.ActiveRowIndex;

                if (fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text == "确认")
                {
                    //取消确认

                    if (this.fpSpread2_Sheet1.Rows[currRow].Tag == null)
                    {
                        return;
                    }

                    if (!(this.fpSpread2_Sheet1.Rows[currRow].Tag is FS.HISFC.Models.Fee.ReturnApply))
                    {
                        MessageBox.Show("没有核准药品不能取消!");

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

                                //删除met_nui_cancelitem表中的申请信息
                                ReturnApply returnApply = this.fpSpread2_Sheet1.Rows[currRow].Tag as ReturnApply;

                                if (this.returnApplyManager.DeleteReturnApply(returnApply.ID) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();

                                    return;
                                }

                                //恢复fin_opb_feedetail中的可退数量和已退数量

                                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(f.RecipeNO, f.SequenceNO);

                                if (feeItemList == null)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(feeItemList.Item.Name + "获得项目失败!" + this.outpatientManager.Err);

                                    return;
                                }

                                //if (feeItemList.Item.Qty < feeItemList.NoBackQty + f.Item.Qty)
                                //{
                                //    FS.FrameWork.Management.PublicTrans.RollBack();
                                //    MessageBox.Show("其他的操作员可能已经审核了" + feeItemList.Item.Name + "请刷新!");

                                //    return -1;
                                //}

                                //更新可退数量和确认数量
                                int returnValue = this.outpatientManager.UpdateConfirmFlag(f.RecipeNO, f.SequenceNO, "0", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty - f.Item.Qty,
                                       feeItemList.ConfirmedQty);

                                if (returnValue <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新项目:" + feeItemList.Item.Name + "失败!" + this.outpatientManager.Err);

                                    return;
                                }

                                //扣药品库存表
                                ArrayList al = new ArrayList();
                                al.Add(feeItemList);
                                // public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, DateTime operDate, bool isModify,out string drugSendInfo)
                                string drugSendInfo = string.Empty;
                                pharmacyIntegrate.ApplyOut(this.patient, al, "", this.outpatientManager.GetDateTimeFromSysDateTime(), true, out drugSendInfo);

                                //界面显示


                                int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                                if (findRow == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("查找未退药品失败!");

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

                                    this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "每次量:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "付数:" + f.Days.ToString();
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

                                    //删除met_nui_cancelitem表中的申请信息
                                    ReturnApply returnApply = this.fpSpread2_Sheet1.Rows[currRow].Tag as ReturnApply;

                                    if (this.returnApplyManager.DeleteReturnApply(returnApply.ID) < 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();

                                        return;
                                    }

                                    //恢复fin_opb_feedetail中的可退数量和已退数量

                                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.outpatientManager.GetFeeItemListBalanced(f.RecipeNO, f.SequenceNO);

                                    if (feeItemList == null)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show(feeItemList.Item.Name + "获得项目失败!" + this.outpatientManager.Err);

                                        return;
                                    }

                                    //if (feeItemList.Item.Qty < feeItemList.NoBackQty + f.Item.Qty)
                                    //{
                                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                                    //    MessageBox.Show("其他的操作员可能已经审核了" + feeItemList.Item.Name + "请刷新!");

                                    //    return -1;
                                    //}

                                    //更新可退数量和确认数量
                                    feeItemList.ConfirmedQty += f.Item.Qty;
                                    int returnValue = this.outpatientManager.UpdateConfirmFlag(f.RecipeNO, f.SequenceNO, "0", feeItemList.ConfirmOper.ID, feeItemList.ConfirmOper.Dept.ID, feeItemList.ConfirmOper.OperTime, feeItemList.NoBackQty - f.Item.Qty,
                                           feeItemList.ConfirmedQty);

                                    if (returnValue <= 0)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("更新项目:" + feeItemList.Item.Name + "失败!" + this.outpatientManager.Err);

                                        return;
                                    }

                                    //扣药品库存表
                                    ArrayList al = new ArrayList();
                                    al.Add(feeItemList);
                                    // public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, DateTime operDate, bool isModify,out string drugSendInfo)
                                    string drugSendInfo = string.Empty;
                                    pharmacyIntegrate.ApplyOut(this.patient, al, "", this.outpatientManager.GetDateTimeFromSysDateTime(), true, out drugSendInfo);

                                    //界面显示


                                    int findRow = FindItem(f.RecipeNO, f.SequenceNO, this.fpSpread1_Sheet1);

                                    if (findRow == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show("查找未退药品失败!");

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

                                        this.fpSpread1_Sheet1.Cells[findRow, (int)DrugList.DoseAndDays].Text = "每次量:" + FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce, 3) + f.Order.DoseUnit + " " + "付数:" + f.Days.ToString();
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
                        MessageBox.Show("门诊退药确认后可以取消异常!" + ex.Message.ToString());
                        return;
                    }
                }
                else
                {
                    #region 修改 mad
                    //未核准
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
                            MessageBox.Show("查找未退药品失败!");

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
            //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} 门诊退药确认后可以取消
            if (this.isUseCancelQuitConfirm)
            {
                if (this.fpSpread2.ActiveSheet.RowCount > 0)
                {
                    //是否允许半退
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

        #region IInterfaceContainer 成员

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
