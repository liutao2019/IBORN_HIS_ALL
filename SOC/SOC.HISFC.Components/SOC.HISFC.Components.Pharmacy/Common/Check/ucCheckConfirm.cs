using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    /// <summary>
    /// [功能描述: 盘点确认：结存或解封]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04]<br></br>
    /// 说明：
    /// 1、关于盘亏库存判断解释：盘点盘亏时是必须减少库存的，可能存在盘亏量即必须减少的库存已经大于结存时的库存量，
    ///    如果不判断库存，系统则将减少最大能减少的库存量，即将库存清零，这样减少的库存不等于盘亏的库存，盘点表不能作为财务账务
    ///    如果判断库存，则必须要求客户修改盘点数量以达到盘亏量小于等于结存时的库存量，这样减少的库存等于盘亏的库存，盘点表可以作为财务账务
    /// </summary>
    public partial class ucCheckConfirm : ucBaseCheck, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        
        public ucCheckConfirm()
        {
            InitializeComponent();
        }

        private uint maxProfitCostRate = 10;

        /// <summary>
        /// 盘盈金额/库存金额最大百分比
        /// </summary>
        [Description("盘盈金额/库存金额最大百分比"), Category("设置"), Browsable(true)]
        public uint MaxProfitCostRate
        {
            get { return maxProfitCostRate; }
            set { maxProfitCostRate = value; }
        }
        private uint maxLossCostRate = 10;

        /// <summary>
        /// 盘亏金额/库存金额最百分比
        /// </summary>
        [Description("盘亏金额/库存金额最百分比"), Category("设置"), Browsable(true)]
        public uint MaxLossCostRate
        {
            get { return maxLossCostRate; }
            set { maxLossCostRate = value; }
        }

        private bool isApplyProcedure = false;

        /// <summary>
        /// 是否启用存储过程结存
        /// </summary>
        [Description("是否启用存储过程结存"), Category("设置"), Browsable(true)]
        public bool IsApplyProcedure
        {
            get { return isApplyProcedure; }
            set { isApplyProcedure = value; }
        }

        private bool isCheckStoreQtyWhenLoss = false;

        /// <summary>
        /// 盘亏是否需要判断库存
        /// </summary>
        [Description("盘亏是否需要判断库存"), Category("设置"), Browsable(true)]
        public bool IsCheckStoreQtyWhenLoss
        {
            get { return isCheckStoreQtyWhenLoss; }
            set { isCheckStoreQtyWhenLoss = value; }
        }

        private bool isBackStorage = true;

        /// <summary>
        /// 是否备份结存后的库存
        /// </summary>
        [Description("是否备份结存后的库存"), Category("设置"), Browsable(true)]
        public bool IsBackStorage
        {
            get { return isBackStorage; }
            set { isBackStorage = value; }
        }

        private bool isFinStatic = true;

        /// <summary>
        /// 是否在结存后进行财务结算
        /// </summary>
        [Description("是否在结存后进行财务结算"), Category("设置"), Browsable(true)]
        public bool IsFinStatic
        {
            get { return isFinStatic; }
            set { isFinStatic = value; }
        }

        private bool isCheckUser = true;

        /// <summary>
        /// 是否需要验证用户
        /// </summary>
        [Description("结存或解封时是否需要验证用户"), Category("设置"), Browsable(true)]
        public bool IsCheckUser
        {
            get { return isCheckUser; }
            set { isCheckUser = value; }
        }

        #region 解封

        private void CancelCheck()
        {
            //如当前点击无数据则返回
            if (this.dtDetail.Rows.Count == 0)
            {
                return;
            }

            DialogResult result;
            //提示用户选择是否继续
            result = MessageBox.Show("确认进行解封操作吗", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return;
            }

            if (this.IsCheckUser)
            {
                SOC.HISFC.Components.Common.Base.frmCheckPassWord frmCheckPassWord = new FS.SOC.HISFC.Components.Common.Base.frmCheckPassWord(this.itemMgr.Operator.ID);
                frmCheckPassWord.StartPosition = FormStartPosition.CenterScreen;
                if (frmCheckPassWord.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //定义事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行解封处理.请稍候...");
            Application.DoEvents();
           
            try
            {
                int i = this.itemMgr.CancelCheck(this.curStockDept.ID, this.curCheckBillNO);
                //解封未成功返回
                if (i == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("解封失败，请与系统管理员联系并报告错误："+this.itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }
                if (i == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    Function.ShowMessage("解封失败，该盘点单可能已被解封或结存，请刷新！", MessageBoxIcon.Information);

                    this.ShowList();

                    return;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("解封失败，请与系统管理员联系并报告错误：" + ex.Message, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.ShowList();

            Function.ShowMessage("解封操作成功", MessageBoxIcon.Information);
        }
        #endregion

        #region 结存
        private void ConfirmCheck()
        {
            //如当前点击无数据则返回
            if (this.dtDetail.Rows.Count == 0)
            {
                return;
            }

            DialogResult result;
            //提示用户选择是否继续
            result = MessageBox.Show("确认进行结存操作吗", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return;
            }

            if (this.IsCheckUser)
            {
                SOC.HISFC.Components.Common.Base.frmCheckPassWord frmCheckPassWord = new FS.SOC.HISFC.Components.Common.Base.frmCheckPassWord(this.itemMgr.Operator.ID);
                frmCheckPassWord.StartPosition = FormStartPosition.CenterScreen;
                if (frmCheckPassWord.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //获取是否按批号盘点;获取当前显示的盘点单是否按批号盘点，当前通过明细表内批号字段判断，以后可在统计表内加字段
            bool isBatch;
            DataRow row = this.dtDetail.Rows[0];
            if (row["批号"].ToString().ToUpper() == "ALL")
            {
                isBatch = false;
            }
            else
            {
                isBatch = true;
            }
            if (this.CheckValid() == -1)
            {
                return;
            }

            //定义事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

          
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行结存处理.请稍候...");
            Application.DoEvents();
            try
            {
                if (this.IsApplyProcedure)
                {
                    if (this.itemMgr.ExecProcedurgCheckCStore(this.curStockDept.ID, this.curCheckBillNO, isBatch) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("结存操作失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (this.itemMgr.ConfirmCheck(this.curStockDept.ID, this.curCheckBillNO,this.isCheckStoreQtyWhenLoss, isBatch, isBatch) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        //库存不足
                        if (this.itemMgr.ErrCode == "2")
                        {
                            Function.ShowMessage("" + this.itemMgr.Err, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Function.ShowMessage("结存操作失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        }
                        return;
                    }
                }

                if (this.IsBackStorage)
                {

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行库存备份.请稍候...");
                    Application.DoEvents();
                    FS.HISFC.Models.Pharmacy.Check checkStat = this.itemMgr.GetCheckStat(this.curStockDept.ID, this.curCheckBillNO);
                    if (checkStat == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("结存操作失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                    if (this.itemMgr.BackStorage(this.curStockDept.ID, checkStat.COper.OperTime) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("结存操作失败，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                }

                //提交事务
                FS.FrameWork.Management.PublicTrans.Commit();


                if (this.IsFinStatic)
                {
                    //重新定义事务
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行账务结算.请稍候...");
                    Application.DoEvents();

                    FS.SOC.HISFC.BizLogic.Pharmacy.Financial financialMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Financial();

                    if (financialMgr.ExecMonthStatic(this.curStockDept.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("结存已经完成，但是账务结算操作失败，请与系统管理员联系并报告错误：" + financialMgr.Err, MessageBoxIcon.Error);
                        
                        this.ShowList();

                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("结存失败，请与系统管理员联系并报告错误：" + ex.Message, MessageBoxIcon.Error);
                return;
            }

            this.ShowList();
            Function.ShowMessage("结存操作成功!", MessageBoxIcon.Information);
        }

        protected int CheckValid()
        {
            decimal qty = 0;
            decimal plCost = 0;
            decimal adjustCost = 0;

            foreach (DataRow dr in this.dtDetail.Rows)
            {
                decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量1"]);

                decimal minQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点数量2"]);

                decimal otherQty = FS.FrameWork.Function.NConvert.ToDecimal(dr["发药机库存"]);

                plCost += FS.FrameWork.Function.NConvert.ToDecimal(dr["盈亏零额"]);
                adjustCost += FS.FrameWork.Function.NConvert.ToDecimal(dr["盘点零额"]);

                qty = qty + packQty + minQty + otherQty;
            }

            if (qty == 0)
            {
                DialogResult result;
                //提示用户选择是否继续
                result = MessageBox.Show("盘点数量全为0，确认进行结存操作吗", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                if (result == DialogResult.Cancel)
                {
                    return -1;
                }
            }
            if (plCost < 0)
            {
                decimal rate = plCost / (plCost + adjustCost);
                if (Math.Abs(rate) > this.maxLossCostRate)
                {
                    DialogResult result = MessageBox.Show("盘亏金额/库存金额已经超过警戒值：" + this.maxLossCostRate.ToString() + "%，确认进行结存操作吗", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.RightAlign);
                    if (result == DialogResult.Cancel)
                    {
                        return -1;
                    }
                }
            }
            else if (plCost > 0)
            {
                decimal rate = plCost / (plCost + adjustCost);
                if (rate > this.maxProfitCostRate)
                {
                    DialogResult result = MessageBox.Show("盘盈金额/库存金额已经超过警戒值：" + this.maxProfitCostRate.ToString() + "%，确认进行结存操作吗", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.RightAlign);
                    if (result == DialogResult.Cancel)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }
        #endregion

        #region 权限处理

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();
            int param = Function.ChoosePriveDept("0306", ref priveDept);
            if (param == 0 || param == -1 || priveDept == null || string.IsNullOrEmpty(priveDept.ID))
            {
                return -1;
            }

            this.nlbPriveDept.Text = "您选择的科室是【" + priveDept.Name + "】";

            this.curStockDept = priveDept;

            return 1;
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
            return this.SetPriveDept();
        }

        #endregion

        #region 工具栏
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("结存", "盘点确认", FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            toolBarService.AddToolButton("解封", "盘点取消", FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "结存")
            {
                this.ConfirmCheck();
            }
            else if (e.ClickedItem.Text == "解封")
            {
                this.CancelCheck();
 
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            base.OnLoad(e);

            this.dtDetail.Columns["盘点数量1"].ReadOnly = true;
            this.dtDetail.Columns["盘点数量2"].ReadOnly = true;
            this.dtDetail.Columns["发药机库存"].ReadOnly = true;
        }
    }
}
