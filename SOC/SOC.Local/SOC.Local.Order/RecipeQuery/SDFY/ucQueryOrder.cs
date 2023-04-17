using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.SOC.Local.Order.RecipeQuery.SDFY
{
    /// <summary>
    /// 门诊查询历史医嘱
    /// {FF783D04-EBFE-477b-9603-4B6554B452AA}
    /// </summary>
    public partial class ucQueryOrder : Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory
    {
        public ucQueryOrder()
        {
            InitializeComponent();
        }

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 费用业务层
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Fee feeManagement = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 药品业务
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Pharmacy pManagement = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        private decimal dQuitNum = 0;
       
        private void mnuQuitApply_Click(object sender, EventArgs e)
        {
            if (this.fpOrder_Sheet1.SelectionCount > 1 || this.fpOrder_Sheet1.SelectionCount < 1)
            {
                MessageBox.Show("请选择一条记录！");
                return;
            }
            if (this.fpOrder_Sheet1.Rows.Count <= 0)
            {
                return;
            }
            
            ucQuitApplyNum ucQuitApplyNum1 = new ucQuitApplyNum();
            Neusoft.FrameWork.WinForms.Classes.Function.PopForm.Text = "退单";
            Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(ucQuitApplyNum1);
            if (ucQuitApplyNum1.QuitNum < 1)
            {
                return;
            }
            else
            {
                this.dQuitNum = ucQuitApplyNum1.QuitNum;
            }
            ArrayList list = new ArrayList();
            string errStr = string.Empty;
            Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();
            for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            {
                if (this.fpOrder_Sheet1.IsSelected(row, 0))
                {
                    order = this.orderManager.QueryOneOrder(base.myReg.ID, fpOrder_Sheet1.Cells[row, 0].Value.ToString());
                    break;
                }
            }
            if (order == null || order.ID == "")
            {
                MessageBox.Show("查找退单处方失败！");
                return;
            }
            try
            {
                if (order.Status == 1 || order.Status == 2)
                {
                    Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                    feeManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                    pManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                    if (this.QuitApply(order, ref errStr) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("退费申请时出错!\n" + errStr);
                        return;
                    }
                }
                else if (order.Status == 0)
                {
                    MessageBox.Show("该条处方还未收费，请到处方开立界面修改！");
                    return;
                }
                else
                {
                    MessageBox.Show("该条处方已经作废！");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("退单申请失败！" + ex.Message);
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("退单申请成功！");
        }

        /// <summary>
        /// 全退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuQuitApplyAll_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            string errStr = string.Empty;
            Neusoft.HISFC.Models.Order.OutPatient.Order order = new Neusoft.HISFC.Models.Order.OutPatient.Order();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            for (int row = 0; row < this.fpOrder_Sheet1.Rows.Count; row++)
            {
                if (this.fpOrder_Sheet1.IsSelected(row, 0))
                {
                    order = this.orderManager.QueryOneOrder(base.myReg.ID, fpOrder_Sheet1.Cells[row, 0].Value.ToString());
                    if (order == null || order.ID == "")
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("查找退单处方失败！");
                        return;
                    }
                    try
                    {
                        if (order.Status == 1 || order.Status == 2)
                        {
                            feeManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                            pManagement.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                            if (this.QuitApplyAll(order, ref errStr) == -1)
                            {
                                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("退费申请时出错!\n" + errStr);
                                return;
                            }
                        }
                        else if (order.Status == 0)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("该条处方还未收费，请到处方开立界面修改！");
                            return;
                        }
                        else
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("该条处方已经作废！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("退单申请失败！" + ex.Message);
                        return;
                    }
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("退单申请成功！");
        }

        protected override void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            base.fpSpread1_MouseUp(sender, e);
            ToolStripMenuItem mnuQuitApply = new ToolStripMenuItem();
            mnuQuitApply.Text = "退单";
            mnuQuitApply.Click += new EventHandler(mnuQuitApply_Click);
            this.contextMenu1.Items.Add(mnuQuitApply);
            for (int i = 0; i <= this.fpOrder_Sheet1.RowCount; i++)
            {
                if (i == this.fpOrder_Sheet1.RowCount)
                {
                    ToolStripMenuItem mnuQuitApplyAll = new ToolStripMenuItem();
                    mnuQuitApplyAll.Text = "全退";
                    mnuQuitApplyAll.Click += new EventHandler(mnuQuitApplyAll_Click);
                    this.contextMenu1.Items.Add(mnuQuitApplyAll);
                    break;
                }
                if (this.fpOrder_Sheet1.IsSelected(i, 1))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        //{5C327B2F-2B74-45bb-8435-4E5118215BD2}
        private int QuitApply(Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string errStr)
        {
            if (order.ID == null || order.ID == "")
            {
                errStr = "未找到可退记录";
                return -1;
            }
            ArrayList alFeeDetail = this.feeManagement.QueryFeeDetailFromMOOrder(order.ID);
            if (alFeeDetail == null && alFeeDetail.Count == 0)
            {
                errStr = "未找到可退记录";
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeDetail)
            {
                if (f.TransType == TransTypes.Positive)
                {//正交易的
                    #region 已发药、已确认的部分
                    Neusoft.HISFC.Models.Fee.ReturnApply returnApply = new Neusoft.HISFC.Models.Fee.ReturnApply();

                    returnApply.Item = f.Item.Clone();
                    returnApply.RecipeNO = f.RecipeNO;
                    returnApply.SequenceNO = f.SequenceNO;
                    returnApply.FeePack = f.FeePack;
                    if (f.ConfirmedQty > 0)
                    {
                        if (this.dQuitNum > f.ConfirmedQty)
                        {
                            errStr = "申请数量大于可退数量，请重新选择申请数量！";
                            return -1;
                        }
                        else
                        {
                            returnApply.Item.Qty = this.dQuitNum;
                        }
                    }
                    else
                    {
                        returnApply.Item.Qty = f.ConfirmedQty;
                    }
                    //returnApply.Item.Qty = f.ConfirmedQty;
                    returnApply.Patient = base.myReg;
                    returnApply.Days = f.Days;
                    returnApply.ExecOper = f.ExecOper.Clone();
                    returnApply.UndrugComb = f.UndrugComb.Clone();
                    returnApply.ConfirmBillNO = f.Invoice.ID;
                    DateTime now = Neusoft.FrameWork.Function.NConvert.ToDateTime(orderManager.GetDateTimeFromSysDateTime());
                    returnApply.Oper.OperTime = now;
                    returnApply.Oper.Dept = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept;

                    if (returnApply.Item.Qty > 0)
                    {
                        if (InsertIntoQuitApplyTable(returnApply, CancelTypes.Canceled, ref errStr) == -1)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        errStr = "该处方还未发药，可以直接到收费处退费！";
                        return -1;
                    }
                    #endregion
                    #region 未发药、未确认的部分
                    //if (f.Item.Qty - f.ConfirmedQty > 0)
                    //{
                    //    if (this.dQuitNum > f.Item.Qty - f.ConfirmedQty)
                    //    {
                    //        errStr = "申请数量大于可退数量，请重新选择申请数量！";
                    //        return -1;
                    //    }
                    //    else
                    //    {
                    //        returnApply.Item.Qty = this.dQuitNum;
                    //    }
                    //}
                    //else
                    //{
                    //    returnApply.Item.Qty = f.Item.Qty - f.ConfirmedQty;
                    //}
                    ////returnApply.Item.Qty = f.Item.Qty - f.ConfirmedQty;
                    //if (returnApply.Item.Qty > 0)
                    //{
                    //    if (InsertIntoQuitApplyTable(returnApply, CancelTypes.LogOut, ref errStr) == -1)
                    //    {
                    //        return -1;
                    //    }
                    //}
                    //if (f.Item.ItemType == EnumItemType.Drug)
                    //{
                    //    if (this.pManagement.CancelApplyOutClinic(f) == -1)
                    //    {
                    //        errStr = "作废发药申请失败！\n" + pManagement.Err;
                    //        return -1;
                    //    }
                    //}
                    #endregion
                }
            }
            return 1;
        }

        /// <summary>
        /// 全退
        /// </summary>
        /// <param name="order"></param>
        /// <param name="errStr"></param>
        /// <returns></returns>
        private int QuitApplyAll(Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string errStr)
        {
            if (order.ID == null || order.ID == "")
            {
                errStr = "未找到可退记录";
                return -1;
            }
            ArrayList alFeeDetail = this.feeManagement.QueryFeeDetailFromMOOrder(order.ID);
            if (alFeeDetail == null && alFeeDetail.Count == 0)
            {
                errStr = "未找到可退记录";
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in alFeeDetail)
            {
                if (f.TransType == TransTypes.Positive)
                {//正交易的
                    #region 已发药、已确认的部分
                    Neusoft.HISFC.Models.Fee.ReturnApply returnApply = new Neusoft.HISFC.Models.Fee.ReturnApply();

                    returnApply.Item = f.Item.Clone();
                    returnApply.RecipeNO = f.RecipeNO;
                    returnApply.SequenceNO = f.SequenceNO;
                    returnApply.FeePack = f.FeePack;
                    //if (f.ConfirmedQty > 0)
                    //{
                    //    if (this.dQuitNum > f.ConfirmedQty)
                    //    {
                    //        errStr = "申请数量大于可退数量，请重新选择申请数量！";
                    //        return -1;
                    //    }
                    //    else
                    //    {
                    //        returnApply.Item.Qty = this.dQuitNum;
                    //    }
                    //}
                    //else
                    //{
                    //    returnApply.Item.Qty = f.ConfirmedQty;
                    //}
                    returnApply.Item.Qty = f.ConfirmedQty;
                    returnApply.Patient = base.myReg;
                    returnApply.Days = f.Days;
                    returnApply.ExecOper = f.ExecOper.Clone();
                    returnApply.UndrugComb = f.UndrugComb.Clone();
                    returnApply.ConfirmBillNO = f.Invoice.ID;
                    DateTime now = Neusoft.FrameWork.Function.NConvert.ToDateTime(orderManager.GetDateTimeFromSysDateTime());
                    returnApply.Oper.OperTime = now;
                    returnApply.Oper.Dept = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept;

                    if (returnApply.Item.Qty > 0)
                    {
                        if (InsertIntoQuitApplyTable(returnApply, CancelTypes.Canceled, ref errStr) == -1)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        errStr = "该处方还未发药，可以直接到收费处退费！";
                        return -1;
                    }
                    #endregion
                    #region 未发药、未确认的部分
                    //if (f.Item.Qty - f.ConfirmedQty > 0)
                    //{
                    //    if (this.dQuitNum > f.Item.Qty - f.ConfirmedQty)
                    //    {
                    //        errStr = "申请数量大于可退数量，请重新选择申请数量！";
                    //        return -1;
                    //    }
                    //    else
                    //    {
                    //        returnApply.Item.Qty = this.dQuitNum;
                    //    }
                    //}
                    //else
                    //{
                    //    returnApply.Item.Qty = f.Item.Qty - f.ConfirmedQty;
                    //}
                    ////returnApply.Item.Qty = f.Item.Qty - f.ConfirmedQty;
                    //if (returnApply.Item.Qty > 0)
                    //{
                    //    if (InsertIntoQuitApplyTable(returnApply, CancelTypes.LogOut, ref errStr) == -1)
                    //    {
                    //        return -1;
                    //    }
                    //}
                    //if (f.Item.ItemType == EnumItemType.Drug)
                    //{
                    //    if (this.pManagement.CancelApplyOutClinic(f) == -1)
                    //    {
                    //        errStr = "作废发药申请失败！\n" + pManagement.Err;
                    //        return -1;
                    //    }
                    //}
                    #endregion
                }
            }
            return 1;
        }

        /// <summary>
        /// 插入退费申请表 {5C327B2F-2B74-45bb-8435-4E5118215BD2}
        /// </summary>
        /// <param name="tempInsert">退费申请实体</param>
        /// <param name="cancelType">退费状态</param>
        /// <param name="errStr">错误信息</param>
        /// <returns></returns>
        private int InsertIntoQuitApplyTable(Neusoft.HISFC.Models.Fee.ReturnApply tempInsert, Neusoft.HISFC.Models.Base.CancelTypes cancelType, ref string errStr)
        {
            #region 判断是否药房已经做过退费申请了 add by lh 10-05-24

            ArrayList al = feeManagement.QueryReturnApplysByRecipeNoSequenceNo(tempInsert.Patient.ID, tempInsert.RecipeNO, tempInsert.SequenceNO.ToString());
            if (al == null)
            {
                errStr = "查询退费申请失败！" + this.feeManagement.Err;
                return -1;
            }
            if (al.Count > 0)
            {//药房已经做过退费申请了
                return 1;
            }

            #endregion

            tempInsert.ID = this.feeManagement.GetReturnApplySequence();
            tempInsert.IsConfirmed = false;
            tempInsert.CancelType = cancelType;

            int returnValue = this.feeManagement.InsertReturnApply(tempInsert);

            if (returnValue == -1)
            {
                errStr = tempInsert.Item.Name + "申请失败!" + this.feeManagement.Err;
                return -1;
            }
            return 1;
        }
    }
}
