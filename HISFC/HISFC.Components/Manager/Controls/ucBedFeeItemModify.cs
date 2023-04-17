﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucBedFeeItemModify : UserControl
    {
        public ucBedFeeItemModify()
        {
            InitializeComponent();
        }

        #region 变量定义

        /// <summary>
        ///当前的ID
        /// </summary>
        private string iD = string.Empty;

        /// <summary>
        ///固定费用实体
        /// </summary>
        private FS.HISFC.Models.Fee.BedFeeItem bedFeeItem = new FS.HISFC.Models.Fee.BedFeeItem();

        /// <summary>
        /// 代理
        /// </summary>
        /// <param name="bedFeeItem"></param>
        public delegate void ClickSave(FS.HISFC.Models.Fee.BedFeeItem bedFeeItem);

        /// <summary>
        /// 保存事件
        /// </summary>
        public event ClickSave Save;

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 费用业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.BedFeeItem bedFeeItemManager = new FS.HISFC.BizLogic.Fee.BedFeeItem();

        /// <summary>
        /// 当前Uc是保存还是增加
        /// </summary>
        private EnumSaveTypes saveType;

        /// <summary>
        /// 最小费用列表
        /// </summary>
        private ArrayList itemInfo = new ArrayList();

        #endregion

        #region 属性

        /// <summary>
        /// 项目列表
        /// </summary>
        public ArrayList ItemInfo
        {
            get
            {
                return this.itemInfo;
            }
            set
            {
                this.itemInfo = value;
            }
        }

        /// <summary>
        /// 是保存还是增加
        /// </summary>
        public EnumSaveTypes SaveType
        {
            get
            {
                return this.saveType;
            }
            set
            {
                this.saveType = value;
                if (this.saveType == EnumSaveTypes.Add)
                {

                }
            }
        }

        /// <summary>
        /// 当前的ID
        /// </summary>
        public string ID
        {
            get
            {
                return this.iD;
            }
            set
            {
                this.iD= value;
            }
        }

        /// <summary>
        /// 固定费用实体
        /// </summary>
        public FS.HISFC.Models.Fee.BedFeeItem BedFeeItem
        {
            get
            {
                return this.bedFeeItem;
            }
            set
            {
                this.bedFeeItem = value;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 增加在用、停用、废弃下拉框
        /// </summary>
        protected virtual void InitValidState()
        {
            ArrayList validStates = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "在用";
            validStates.Add(obj);

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "0";
            obj1.Name = "停用";
            validStates.Add(obj1);

            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "2";
            obj2.Name = "废弃";
            validStates.Add(obj2);

            this.cmbValidState.AddItems(validStates);
        }

        /// <summary>
        /// 修改规类属性
        /// </summary>
        protected virtual void Modify()
        {
            this.cmbItemInfo.Text = this.bedFeeItem.Name;
            this.ntxtPrice.Text = itemManager.GetValidItemByUndrugCode(this.bedFeeItem.ID).Price.ToString();
            this.ntxtQty.Text = this.bedFeeItem.Qty.ToString();
            this.dtBegin.Text = this.bedFeeItem.BeginTime.ToString();
            this.dtEnd.Text = this.bedFeeItem.EndTime.ToString();
            this.ckbBabyRelation.Checked = this.bedFeeItem.IsBabyRelation;
            this.ckbTimeRelation.Checked = this.bedFeeItem.IsTimeRelation;
            this.cmbValidState.Tag = ((int)this.bedFeeItem.ValidState).ToString();

            this.cmbItemInfo.Focus();
        }

        /// <summary>
        /// 增加规类
        /// </summary>
        protected virtual void Add()
        {
            //this.txtReportCode.Text = feeCodeStat.ID;
            //this.txtReportCode.Enabled = false;
            //this.txtReportName.Text = feeCodeStat.Name;
            //this.txtReportName.Enabled = false;
            //this.cmbReportType.Text = feeCodeStat.ReportType.Name;
            //this.cmbReportType.Tag = feeCodeStat.ReportType;
            //this.cmbReportType.Enabled = false;
            //this.cmbMinFee.Enabled = true;
            //this.cmbMinFee.Text = string.Empty;
            //this.cmbMinFee.Tag = string.Empty;
            //this.txtFeeStatCode.Text = string.Empty;
            //this.txtFeeStatName.Text = string.Empty;
            //this.txtPrintOrder.Text = maxPrintOrder;
            //this.cmbReportType.Text = string.Empty;
            //this.cmbExecDept.Tag = string.Empty;
            //this.cmbExecDept.Text = string.Empty;
            //this.txtFeeStatName.Text = string.Empty;
            //this.cmbCenterStatCode.Tag = string.Empty;
            this.cmbValidState.Tag = "0";
            this.cmbValidState.Text = "在用";
            this.cmbValidState.Enabled = true;
            //if (this.cmbMinFee.Items.Count == 0)
            //{
            //    this.ckbContinue.Checked = false;
            //}
            //else
            //{
            //    this.ckbContinue.Checked = true;
            //}
        }

        /// <summary>
        /// 有效性判断
        /// </summary>
        /// <returns>有效 True 无效 False</returns>
        protected virtual bool IsValid()
        {
            if (this.cmbItemInfo.Text == string.Empty || this.cmbItemInfo.Tag == null)
            {
                MessageBox.Show(Language.Msg("项目名称不能为空!"));
                this.cmbItemInfo.Focus();

                return false;
            }

            if (this.ntxtQty.Text == string.Empty || this.ntxtQty.Text == null)
            {
                MessageBox.Show(Language.Msg("数量不能为空!"));
                this.ntxtQty.Focus();

                return false;
            }
            
            if (this.cmbValidState.Text == string.Empty || this.cmbValidState.Text == null)
            {
                MessageBox.Show("有效性标识不能为空!");
                this.cmbValidState.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得有效性的名称
        /// </summary>
        /// <param name="id">编码</param>
        /// <returns>成功 有效性的名称 失败 null</returns>
        protected virtual string GetValidName(string id)
        {
            string name = string.Empty;

            switch (id)
            {
                case "0":
                    name = "在用";
                    break;
                case "1":
                    name = "停用";
                    break;
                case "2":
                    name = "废弃";
                    break;
            }

            return name;
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int Confirm()
        {
            //判断有效性
            if (!this.IsValid())
            {
                return -1;
            }
            
            this.bedFeeItem.ID = this.cmbItemInfo.Tag.ToString();//项目编码
            this.bedFeeItem.Name = this.cmbItemInfo.Text.ToString();//项目名称
            this.bedFeeItem.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtQty.Text);//数量
            this.bedFeeItem.BeginTime = this.dtBegin.Value;//开始时间
            this.bedFeeItem.EndTime = this.dtEnd.Value;//结束时间
            this.bedFeeItem.IsBabyRelation = this.ckbBabyRelation.Checked;//婴儿相关
            this.bedFeeItem.IsTimeRelation = this.ckbTimeRelation.Checked;//时间相关
            this.bedFeeItem.ValidState =(FS.HISFC.Models.Base.EnumValidState) FS.FrameWork.Function.NConvert.ToInt32(this.cmbValidState.Tag);//有效性标识
            
            //把修改或者增加的obj传回去
            
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new Transaction(this.bedFeeItemManager.Connection);
                //t.BeginTransaction();

                this.bedFeeItemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int returnValue = 0;

                if (this.saveType == EnumSaveTypes.Add)
                {

                    returnValue = this.bedFeeItemManager.InsertBedFeeItem(this.bedFeeItem);
                }
                else
                {
                    returnValue = this.bedFeeItemManager.UpdateBedFeeItem(this.bedFeeItem);
                }

                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("插入或更新固定费用信息出错!") + this.bedFeeItemManager.Err);
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();;

                this.Save(this.bedFeeItem);

                this.Save(bedFeeItem);

                if (!this.ckbContinue.Checked)
                {
                    this.ParentForm.Close();
                }
                {
                    this.Tag = this.itemManager.GetSequence("SEQ_FIN_COM_BEDFEEITEM");
                }
            }
            catch
            {
                return -1;
            }
            return 1;

                
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 根据传入参数初始化
        /// </summary>
        public void Init()
        {
            this.InitCmb();

            if (this.saveType == EnumSaveTypes.Add)
            {
                this.Add();
            }
            if (this.saveType == EnumSaveTypes.Modify)
            {
                this.Modify();
            }

        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        /// <returns></returns>
        protected virtual int InitCmb()
        {
            try
            {
                this.cmbItemInfo.AddItems(itemInfo);
                this.InitValidState();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return -1;
            }

            return 1;
        }

        #endregion

        #region 枚举

        /// <summary>
        /// 保存的类型
        /// </summary>
        public enum EnumSaveTypes
        {
            /// <summary>
            /// 增加
            /// </summary>
            Add = 0,

            /// <summary>
            /// 修改
            /// </summary>
            Modify = 1
        }

        #endregion

        #region 事件

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click_1(object sender, EventArgs e)
        {
            this.Confirm();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBedFeeItemModify_load(object sender, EventArgs e)
        {
            this.Init();

            try
            {
                this.FindForm().Text = "固定费用";
                this.FindForm().MinimizeBox = false;
                this.FindForm().MaximizeBox = false;
            }
            catch { }
        }
        

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        /// <summary>
        /// 窗口关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().Close();
            }
            catch { }
        }
    }
}
