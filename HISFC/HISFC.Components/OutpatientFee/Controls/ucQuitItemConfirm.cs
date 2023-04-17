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
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 退费单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint IBackFeePrint = null;
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
                        if (sv.Cells[i, (int)DrugListQuit.Flag].Text != "确认")// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
                        {
                            infoCounts++;
                        }
                    }
                }
            }

            if (infoCounts == 0) 
            {
                MessageBox.Show("没有需要审核的费用!");

                return -1;
            }

            if (MessageBox.Show("是否确认退药？", "询问", MessageBoxButtons.YesNo) == DialogResult.No)// {47F1129F-CC17-41fa-9F1A-6D4E9085FD2E}
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
                    //{077FF0B0-466D-4d24-B3B2-DDCE4BC7F4BF} 门诊退药确认后可以取消
                    if (sv.Rows[i].Tag is ReturnApply && sv.Cells[i, (int)DrugListQuit.Flag].Text != "确认")
                    {
                        ReturnApply tempInsert = sv.Rows[i].Tag as ReturnApply;

                        FS.HISFC.Models.Pharmacy.ApplyOut applyInfoTmp = pharmacyIntegrate.GetApplyOut(tempInsert.RecipeNO, tempInsert.SequenceNO);
                        if (applyInfoTmp != null && !string.IsNullOrEmpty(applyInfoTmp.ID))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tempInsert.Item.Name + "有未发药确认的项目，请先做发药保存，谢谢！");
                            return -1;
                        }

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

            MessageBox.Show("审核成功!");


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
                case "全退":
                    base.AllQuit();
                    break;
                default:
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

                    #region 取消确认前弹出提示框

                    if (this.fpSpread2_Sheet1.Cells[currRow, (int)DrugListQuit.Flag].Text == "确认")//该方法存在问题，如取消数量会叠加，暂时屏蔽 {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
                    {
                        MessageBox.Show("已经确认退药，如需重新发药，请到发药窗口发药!");
                        return;
                    }
                    DialogResult dr = MessageBox.Show("是否确认取消" + (this.fpSpread2_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.ReturnApply).Item.Name + "的退药申请？", "提示", MessageBoxButtons.YesNo);
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
